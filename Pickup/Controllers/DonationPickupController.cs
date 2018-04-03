using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
//using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class DonationPickupController : Controller
    {
        private readonly ApplicationDbContext context;

        public DonationPickupController(ApplicationDbContext applicationDbContext)
        { context = applicationDbContext;
        }



    // GET: /<controller>/
    [Authorize]
        public IActionResult PickupDonor()
        {
            ViewBag.Title = "Donor Information";
            DonorViewModel createNewPickupViewModel = new DonorViewModel();
            return View("Index", createNewPickupViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PickupDonor(DonorViewModel donorViewModel)
        {
            ViewBag.Title = "Donor Information";
            if (ModelState.IsValid)
            {
                Donor newDonor = new Donor
                {
                    FirstName = donorViewModel.FirstName,
                    LastName = donorViewModel.LastName,
                    PhoneNumber = donorViewModel.PhoneNumber,
                    PhoneNumberTwo = donorViewModel.PhoneNumberTwo
                };
                context.Add(newDonor);
                context.SaveChanges();

                // I suspect there is a better way to do this thing here....
                return Redirect("/DonationPickup/Address?donorId=" + newDonor.ID);
            }

            return View(donorViewModel);
        }

        [Authorize]
        public IActionResult Address(int donorId)
        {
            Donor donor = context.Donors.Single(d => d.ID == donorId);
            AddressViewModel addressViewModel = new AddressViewModel();
            return View("Index", addressViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Address(AddressViewModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                Address newAddress = new Address
                {
                    Street = addressViewModel.Street,
                    Apartment = addressViewModel.Apartment,
                    City = addressViewModel.City,
                    ZIP = addressViewModel.ZIP,
                    Neighborhood = addressViewModel.Neighborhood,
                    DonorID = addressViewModel.DonorId
                };

                context.Add(newAddress);
                context.SaveChanges();
                return Redirect("/DonationPickup/CreateNewPickup?addressId=" + newAddress.ID);
            }
            return View("Index", addressViewModel);
        }

        [Authorize]
        public IActionResult CreateNewPickup(int addressId)
        {
            Address address = context.Addresses.Single(d => d.ID == addressId);
            PickupDeliveryViewModel donationViewModel = new PickupDeliveryViewModel();
            return View("Index", donationViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateNewPickup(PickupDeliveryViewModel donationPickupViewModel)
        {
            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
                string scheduler = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                PickupOrDelivery newPickup = new PickupOrDelivery               

                {
                    ScheduleDateTime = currentDate,
                    PickupDate = donationPickupViewModel.PickupDate,
                    PickupTime = donationPickupViewModel.PickupTime,
                    CallEnRoute = donationPickupViewModel.CallEnRoute,
                    SpecialInstructions = donationPickupViewModel.SpecialInstructions,
                    AddressID = donationPickupViewModel.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();
                return Redirect("/");
            }
            return View("Index", donationPickupViewModel);

        }
    }
}
