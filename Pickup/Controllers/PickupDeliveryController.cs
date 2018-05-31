using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.QueryClasses;
using Pickup.Models.PickupDeliveryViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{   [Authorize]
    public class PickupDeliveryController : Controller
    {

        private readonly ApplicationDbContext context;
        private CheckForExistingQuery query = new CheckForExistingQuery();

        public PickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }



        // GET: /<controller>/
        public IActionResult Customer()
        {
            ViewBag.Title = "Customer Information";
            ViewBag.Button = "Add " + ViewBag.Title;
            return View("PickupDelivery/Customer", new CustomerViewModel());
        }

       
        [HttpPost]
        public IActionResult Customer(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (query.GetCustomerByFullName(context, model.FirstName, model.LastName).Count > 0)
                  return Redirect(String.Format("Existing?firstName={0}&lastName={1}", model.FirstName, model.LastName));
            
                DonorCustomer newPerson = new DonorCustomer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberTwo = model.PhoneNumberTwo,
                    Email = model.Email,
                    FOT = model.FOT
                };
                context.Add(newPerson);
                context.SaveChanges();

                // I suspect there is a better way to do this thing here....
                return Redirect("Address?customerId=" + newPerson.ID);
            }

            return View("PickupDelivery/Customer", model);
        }

        public IActionResult Address(int customerId)
        {
            ViewBag.Title = "Address Information";
            ViewBag.Button = "Add " + ViewBag.Title;

            if (query.GetCustomer(context, customerId) == null)
                return Redirect("/");

            return View("PickupDelivery/Address", new AddressViewModel());
          


                 }

        [HttpPost]
        public IActionResult Address(AddressViewModel model)
        {
            if (ModelState.IsValid && model.Street !=null && model.City != null && model.ZIP != null)
            {
                Address newAddress = new Address
                {
                    Street = model.Street,
                    Apartment = model.Apartment,
                    City = model.City,
                    ZIP = model.ZIP,
                    Neighborhood = model.Neighborhood,
                    BottomFloor = model.BottomFloor,
                    DonorCustomerID = model.CustomerId
                };

                context.Add(newAddress);
                context.SaveChanges();
                return Redirect("CreateNew?addressId=" + newAddress.ID);
            }
            return View("PickupDelivery/Address", model);
        }

        public IActionResult CreateNew(int addressId)
        {
            ViewBag.Title = "New Pickup/Delivery";
            ViewBag.Button = "Create " + ViewBag.Title;

            if (query.GetAddress(context, addressId) == null)
                return Redirect("/");

            return View("PickupDelivery/CreateNew", new CreatePickupDeliveryViewModel());

    
        }

        [HttpPost]
        public IActionResult CreateNew(CreatePickupDeliveryViewModel model)
        {
            ViewBag.Title = "New Pickup/Delivery";

            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
                string scheduler = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                DateTime pickupDateTime = new DateTime(model.PickupDate.Year,
                    model.PickupDate.Month,
                    model.PickupDate.Day,
                    model.PickupTime.Hour,
                    model.PickupTime.Minute,
                    0);

                PickupOrDelivery newPickup = new PickupOrDelivery

                {
                    Delivery = model.Delivery,
                    ScheduleDateTime = currentDate,
                    PickupDateTime = pickupDateTime,
                    CallEnRoute = model.CallEnRoute,
                    SpecialInstructions = model.SpecialInstructions,
                    AddressID = model.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();

               
                return Redirect("ItemPickup?pickupId=" + newPickup.ID);
            }
            return View("PickupDelivery/FormDefault", model);

        }

        public IActionResult ItemPickup(int pickupId)
        {
            ViewBag.Title = "Items Picked Up/Delivered";
            ViewBag.Button = "Add " + ViewBag.Title;

            if (query.GetPickupOrDelivery(context, pickupId) == null || query.GetItemsPD(context, pickupId).Count > 0)
                return Redirect("/");

            var model = new ItemPickupViewModel();
            var furnitureItems = context.Furniture.ToList();
            var quantityListItems = new List<ItemQuantityList>();
            foreach (var item in furnitureItems)
            {
                quantityListItems.Add(new ItemQuantityList()
                {
                    CategoryID = item.FurnitureCategoryID,
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = 0
                });
            }
            IList<CategoryBlock> itemsInCategoryBlock = (from fc in context.FurnitureCategories
                                          select new CategoryBlock
                                          {
                                              Category = fc,
                                              Furniture = quantityListItems.Where(f => f.CategoryID == fc.ID).ToList()
                                          }).ToList();
            model.FurnitureList = itemsInCategoryBlock;
            return View("PickupDelivery/ItemPickup", model);

        }

        [HttpPost]
        public IActionResult ItemPickup(ItemPickupViewModel model) {
            foreach (CategoryBlock categoryBlock in model.FurnitureList)
            {
                var selectedFurniture = categoryBlock.Furniture.Where(y => y.Quantity > 0).ToList();
                foreach (var furniturePiece in selectedFurniture)
                {
                    FurniturePickupOrDelivery furnitureDonationPickup = new FurniturePickupOrDelivery
                    {
                        DonationPickupID = model.PickupID,
                        FurnitureID = furniturePiece.ID,
                        Quantity = furniturePiece.Quantity
                    };
                    context.Add(furnitureDonationPickup);
                }
            }
            context.SaveChanges();
            return Redirect("/View?id=" + model.PickupID);

        }
        
        public IActionResult Existing(string firstName, string lastName)
        {
            return View();
        }

    }
}
