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
{
    public class PickupDeliveryController : Controller
    {

        private readonly ApplicationDbContext context;

        public PickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }



        // GET: /<controller>/
        [Authorize]

        public IActionResult Customer()
        {
            ViewBag.Title = "Customer Information";
            return View(new CustomerViewModel());
        }

        [HttpPost]
        public IActionResult Customer(CustomerViewModel model)
        {
            ViewBag.Title = "Customer Information";
            if (ModelState.IsValid)
            {
                Donor existingPerson = context.Donors
                      .Where(d => d.FirstName == model.FirstName)
                      .Where(d => d.LastName == model.LastName)
                      .Where(d => d.PhoneNumber == model.PhoneNumber)
                      .FirstOrDefault();

                if (existingPerson != null)
                {
                    return Redirect("Address?customerId=" + existingPerson.ID);
                }

                Donor newPerson = new Donor
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberTwo = model.PhoneNumberTwo
                };
                context.Add(newPerson);
                context.SaveChanges();

                // I suspect there is a better way to do this thing here....
                return Redirect("Address?customerId=" + newPerson.ID);
            }

            return View(model);
        }

        public IActionResult Address(int customerId)
        {
            ViewBag.Title = "Address Information";

            Donor donor = context.Donors.Single(d => d.ID == customerId);
            return View("Index", new AddressViewModel());
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
                    DonorID = model.CustomerId
                };

                context.Add(newAddress);
                context.SaveChanges();
                return Redirect("CreateNew?addressId=" + newAddress.ID);
            }
            return View("Index", model);
        }

        public IActionResult CreateNew(int addressId)
        {
            ViewBag.Title = "New Pickup/Delivery";

            Address address = context.Addresses.Single(d => d.ID == addressId);
            return View("Index", new PickupDeliveryViewModel());
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
                    PickupDate = model.PickupDate,
                    PickupTime = model.PickupTime,
                    CallEnRoute = model.CallEnRoute,
                    SpecialInstructions = model.SpecialInstructions,
                    AddressID = model.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();

                if (newPickup.Delivery)
                    return Redirect("/");

                return Redirect("FurniturePickup?pickupId=" + newPickup.ID);
            }
            return View("Index", model);

        }

        public IActionResult FurniturePickup(int pickupId) {
            ViewBag.Title = "Furniture Donated";
            return View(new FurniturePickupViewModel(context.FurnitureCategories.ToList(), context.Furniture.ToList()));
            
        }

    }
}
