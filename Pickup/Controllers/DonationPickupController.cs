using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class DonationPickupController : Controller
    {
        private readonly ApplicationDbContext context;

        public DonationPickupController(ApplicationDbContext applicationDbContext)
        { context = applicationDbContext; }

        // GET: /<controller>/
        public IActionResult PickupDonor()
        {
            ViewBag.Title = "Donor Information";
            DonorViewModel createNewPickupViewModel = new DonorViewModel();
            return View("Index", createNewPickupViewModel);
        }

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

        public IActionResult Address(int donorId)
        { Donor donor = context.Donors.Single(d => d.ID == donorId);
            AddressViewModel addressViewModel = new AddressViewModel();
            return View("Index", addressViewModel);
        }

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
                return Redirect("/");
            }
            return View("Index", addressViewModel);
        }
    }
}
