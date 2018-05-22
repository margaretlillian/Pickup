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

            DonorCustomer donor = context.DonorsCustomers.Single(d => d.ID == customerId);
            return View("PickupDelivery/Address", new AddressViewModel());
        }

        [HttpPost]
        public IActionResult Address(AddressViewModel model)
        {
            ViewBag.Title = "Address Information";

            if (ModelState.IsValid)
            {
                Address newAddress = new Address
                {
                    Street = model.Street,
                    Apartment = model.Apartment,
                    City = model.City,
                    ZIP = model.ZIP,
                    Neighborhood = model.Neighborhood,
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

            Address address = context.Addresses.Single(d => d.ID == addressId);
            return View("PickupDelivery/CreateNewPickup", new PickupDeliveryViewModel());
        }

        [HttpPost]
        public IActionResult CreateNew(PickupDeliveryViewModel model)
        {
            ViewBag.Title = "New Pickup/Delivery";

            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
                string scheduler = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                PickupOrDelivery newPickup = new PickupOrDelivery

                {
                    Delivery = model.Delivery,
                    ScheduleDateTime = currentDate,
                    PickupDateTime = model.PickupDateTime,
                    CallEnRoute = model.CallEnRoute,
                    SpecialInstructions = model.SpecialInstructions,
                    AddressID = model.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();

                if (newPickup.Delivery)
                    return Redirect("FurnitureDelivery?deliveryId=" + newPickup.ID);

                return Redirect("FurniturePickup?pickupId=" + newPickup.ID);
            }
            return View("PickupDelivery/FormDefault", model);

        }

        public IActionResult FurniturePickup(int pickupId) {
            FurniturePickupViewModel model = new FurniturePickupViewModel();
            var furnitureItems = context.Furniture.ToList(); 
            var quantityListItems = new List<QuantityList>();
            foreach (var item in furnitureItems)
            {
                quantityListItems.Add(new QuantityList()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = 0
                });
            }
            model.FurnitureList = quantityListItems;
            return View("PickupDelivery/FurniturePickup", model);

        }

        [HttpPost]
        public IActionResult FurniturePickup(FurniturePickupViewModel model) {
            var selectedFurniture = model.FurnitureList.Where(x => x.Quantity > 0).ToList();
            foreach (var furniturePiece in selectedFurniture) {
                FurniturePickupOrDelivery furnitureDonationPickup = new FurniturePickupOrDelivery {
                    DonationPickupID = model.PickupID,
                    FurnitureID = furniturePiece.ID,
                    Quantity = furniturePiece.Quantity
                };
                context.Add(furnitureDonationPickup);
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
