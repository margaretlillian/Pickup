using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;
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

        public PickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }



        // GET: /<controller>/
        public IActionResult Customer()
        {
            ViewBag.Title = "Customer Information";
            return View("PickupDelivery/Customer", new CustomerViewModel());
        }

       
        [HttpPost]
        public IActionResult Customer(CustomerViewModel model)
        {
            ViewBag.Title = "Customer Information";
            if (ModelState.IsValid)
            {
                DonorCustomer existingPerson = context.DonorsCustomers
                      .Where(d => d.FirstName == model.FirstName)
                      .Where(d => d.LastName == model.LastName)
                      .Where(d => d.PhoneNumber == model.PhoneNumber)
                      .FirstOrDefault();

                if (existingPerson != null)
                {
                    return Redirect("Address?customerId=" + existingPerson.ID);
                }

                DonorCustomer newPerson = new DonorCustomer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberTwo = model.PhoneNumberTwo,
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

            DonorCustomer donor = context.DonorsCustomers.Where(d => d.ID == customerId).SingleOrDefault();
            if (donor != null)
            {
                return View("PickupDelivery/Address", new AddressViewModel());
            }

            else
                return Redirect("/");


                 }

        [HttpPost]
        public IActionResult Address(AddressViewModel model)
        {
            ViewBag.Title = "Address Information";
            if (ModelState.IsValid && model.Street !=null && model.City != null && model.ZIP != null)
            {
                Address newAddress = new Address
                {
                    Street = model.Street,
                    Apartment = model.Apartment,
                    City = model.City,
                    ZIP = model.ZIP,
                    Neighborhood = model.Neighborhood,
                    BottomFloor = Request.Form["BottomFloor"],
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

            Address address = context.Addresses.Where(d => d.ID == addressId).SingleOrDefault();
            if (address != null)
                return View("PickupDelivery/CreateNewPickup", new PickupDeliveryViewModel());

            else
                return Redirect("/");
        }

        [HttpPost]
        public IActionResult CreateNew(PickupDeliveryViewModel model)
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

                if (newPickup.Delivery)
                    return Redirect("FurnitureDelivery?deliveryId=" + newPickup.ID);

                return Redirect("ItemPickup?pickupId=" + newPickup.ID);
            }
            return View("PickupDelivery/FormDefault", model);

        }

        public IActionResult ItemPickup(int pickupId) {
            PickupOrDelivery pd = context.PickupsDeliveries.Where(p => p.ID == pickupId).SingleOrDefault();
            IList<FurniturePickupOrDelivery> existingItem = context.FurnitureDonationPickups.Where(fpd => fpd.DonationPickupID == pickupId).ToList();
            if (pd == null || existingItem.Count > 0)
                return Redirect("/");

            var model = new ItemPickupViewModel();
            var furnitureItems = context.Furniture.ToList(); 
            var quantityListItems = new List<QuantityList>();
            foreach (var item in furnitureItems)
            {
                quantityListItems.Add(new QuantityList()
                {   CategoryID = item.FurnitureCategoryID,
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = 0
                });
            }
            IList<CategoryBlock> query = (from fc in context.FurnitureCategories
                         select new CategoryBlock
                         {   Category = fc,
                             Furniture = quantityListItems.Where(f => f.CategoryID == fc.ID).ToList()
                         }).ToList();
            model.FurnitureList = query;
            return View("PickupDelivery/ItemPickup", model);

        }

        [HttpPost]
        public IActionResult ItemPickup(ItemPickupViewModel model) {
            foreach (var x in model.FurnitureList)
            {
                var selectedFurniture = x.Furniture.Where(y => y.Quantity > 0).ToList();
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

        public IActionResult FurnitureDelivery(int deliveryId)
        {
            FurnitureDeliveryViewModel model = new FurnitureDeliveryViewModel();
            var furnitureItems = context.Furniture.ToList();
            var descriptionListItems = new List<DescriptionList>();
            foreach (var item in furnitureItems)
            {
                descriptionListItems.Add(new DescriptionList()
                {
                    ID = item.ID,
                    Name = item.Name,
                Description = null});
            }
            model.FurnitureDelivered = descriptionListItems;
            return View("PickupDelivery/FurnitureDelivery", model);

        }

        [HttpPost]
        public IActionResult FurnitureDelivery(FurnitureDeliveryViewModel model)
        {
            var selectedFurniture = model.FurnitureDelivered.Where(x => x.Description != null).ToList();
            foreach (var furniturePiece in selectedFurniture)
            {
                FurniturePickupOrDelivery furnitureDonationPickup = new FurniturePickupOrDelivery
                {
                    DonationPickupID = model.DeliveryID,
                    FurnitureID = furniturePiece.ID,
                    Description = furniturePiece.Description
                };
                context.Add(furnitureDonationPickup);
            }
            context.SaveChanges();
            return Redirect("/View?id=" + model.DeliveryID);

        }

    }
}
