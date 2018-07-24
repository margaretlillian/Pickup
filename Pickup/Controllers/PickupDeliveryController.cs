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
using Pickup.Models.SearchViewModels;
using Pickup.Services;
//using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PickupDeliveryController : Controller
    {

        private readonly ApplicationDbContext context;
        private CheckForExistingQuery query = new CheckForExistingQuery();
        private SearchQuery searchQuery = new SearchQuery();

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
                string phone = PhoneNumberFormatter.FormatPhoneNumber(model.PhoneNumber);
                DonorCustomer donor = searchQuery.SpecificCustomerSearch(context, model.FirstName, model.LastName, phone);
                if (donor != null)
                {
                    if (query.GetBlacklistedCustomerById(context, donor.ID) != null)
                        return RedirectToAction("BlacklistedCustomerTrigger", "Blacklist", new {customerId = donor.ID });

                    return RedirectToAction("Address", new { customerId=donor.ID});
                }
                        
                        DonorCustomer newPerson = new DonorCustomer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = phone,
                    PhoneNumberTwo = PhoneNumberFormatter.FormatPhoneNumber(model.PhoneNumberTwo),
                    Email = model.Email,
                    FOT = model.FOT
                };
                context.Add(newPerson);
                context.SaveChanges();

                return RedirectToAction("Address", new { customerId = newPerson.ID });
            }

            return View("PickupDelivery/Customer", model);
        }
        
        public IActionResult Address(int customerId)
        {
            ViewBag.Title = "Address Information";
            ViewBag.Button = "Add " + ViewBag.Title;

            if (query.GetCustomer(context, customerId) == null)
               return View("ErrorPage");
            
            if (query.GetBlacklistedCustomerById(context, customerId) != null)
                return RedirectToAction("BlacklistedCustomerTrigger", "Blacklist", new { customerId });

            return View("PickupDelivery/Address", new AddressViewModel());
          


                 }

        [HttpPost]
        public IActionResult Address(AddressViewModel model)
        {
            if (ModelState.IsValid && model.Street !=null && model.City != null && model.ZIP != null)
            {
                Address address = searchQuery.SpecificAddressSearch(context, model.Street, model.Apartment, model.City, model.ZIP);
                if (address != null)
                    return RedirectToAction("CreateNew", new {addressId=address.ID});

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
                return RedirectToAction("CreateNew", new { addressId = newAddress.ID });
            }
            return View("PickupDelivery/Address", model);
        }

        public IActionResult CreateNew(int addressId)
        {
            ViewBag.Title = "New Pickup/Delivery";
            ViewBag.Button = "Create " + ViewBag.Title;

            if (query.GetAddress(context, addressId) == null)
                return View("ErrorPage");

            return View("PickupDelivery/CreateNew", new CreatePickupDeliveryViewModel());

    
        }

        [HttpPost]
        public IActionResult CreateNew(CreatePickupDeliveryViewModel model)
        {
            ViewBag.Title = "New Pickup/Delivery";

            if (ModelState.IsValid)
            {
                string scheduler = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                DateTime pickupDateTime = new DateTime(model.PickupDate.Year,
                    model.PickupDate.Month,
                    model.PickupDate.Day,
                    model.PickupTime.Hour,
                    model.PickupTime.Minute,
                    0);
                if (query.GetBlackoutDay(context, pickupDateTime.ToShortDateString()))
                    return View("BlackedOutDay");

                PickupOrDelivery newPickup = new PickupOrDelivery
                {
                    Delivery = model.Delivery,
                    ScheduleDateTime = DateTime.Now,
                    PickupDateTime = pickupDateTime,
                    CallEnRoute = model.CallEnRoute,
                    SpecialInstructions = model.SpecialInstructions,
                    AddressID = model.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();

               
                return RedirectToAction("ItemPickup", new { pickupId = newPickup.ID });
            }
            return View("PickupDelivery/FormDefault", model);

        }

        public IActionResult ItemPickup(int pickupId)
        {
            ViewBag.Title = "Items Picked Up/Delivered";
            ViewBag.Button = "Add " + ViewBag.Title;

            if (query.GetPickupOrDelivery(context, pickupId) == null || query.GetItemsPD(context, pickupId).Count > 0)
                return View("ErrorPage");

            ItemPickupViewModel model = new ItemPickupViewModel();
            List<ItemDonatedSold> furnitureItems = context.ItemsDonatedSold.ToList();
            List<ItemQuantityList> quantityListItems = new List<ItemQuantityList>();
            foreach (ItemDonatedSold item in furnitureItems)
            {
                quantityListItems.Add(new ItemQuantityList()
                {
                    CategoryID = item.ItemCategoryID,
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = 0
                });
            }
            IList<CategoryBlock> itemsInCategoryBlock = (from itemCat in context.ItemCategories
                                          select new CategoryBlock
                                          {
                                              Category = itemCat,
                                              Furniture = quantityListItems.Where(item => item.CategoryID == itemCat.ID).ToList()
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
                    ItemsAndPickupOrDelivery furnitureDonationPickup = new ItemsAndPickupOrDelivery
                    {
                        PickupDeliveryID = model.PickupID,
                        ItemID = furniturePiece.ID,
                        Quantity = furniturePiece.Quantity
                    };
                    context.Add(furnitureDonationPickup);
                }
            }
            context.SaveChanges();
            return RedirectToAction("View", "Home", new { id = model.PickupID });

        }
        

    }
}
