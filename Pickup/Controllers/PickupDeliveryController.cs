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
//using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class PickupDeliveryController : Controller
    {

        private readonly ApplicationDbContext context;

        public PickupDeliveryController(ApplicationDbContext applicationDbContext)
        { context = applicationDbContext;
        }



    // GET: /<controller>/
    [Authorize]

        public IActionResult PickupDonor()
        {
            ViewBag.Title = "Donor Information";
            DonorViewModel donorViewModel = new DonorViewModel();
            return View(donorViewModel);
        }

        [HttpPost]
        public IActionResult PickupDonor(DonorViewModel donorViewModel)
        {
            ViewBag.Title = "Donor Information";
            if (ModelState.IsValid)
            { Donor existingDonor = context.Donors
                    .Where(d => d.FirstName == donorViewModel.FirstName)
                    .Where(d => d.LastName == donorViewModel.LastName)
                    .Where(d => d.PhoneNumber == donorViewModel.PhoneNumber)
                    .FirstOrDefault();

                if (existingDonor != null)
                {
                    return Redirect("/PickupDelivery/Address?donorId=" + existingDonor.ID);
                }

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
                return Redirect("/PickupDelivery/Address?donorId=" + newDonor.ID);
            }

            return View(donorViewModel);
        }

        public IActionResult Address(int donorId)
        {
            Donor donor = context.Donors.Single(d => d.ID == donorId);
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
                return Redirect("/PickupDelivery/CreateNewPickup?addressId=" + newAddress.ID);
            }
            return View("Index", addressViewModel);
        }

        public IActionResult CreateNewPickup(int addressId)
        {
            Address address = context.Addresses.Single(d => d.ID == addressId);
            PickupDeliveryViewModel donationViewModel = new PickupDeliveryViewModel();
            return View("Index", donationViewModel);
        }

     
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

        public IActionResult Search()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            return View(searchViewModel);
        }

        public IActionResult SearchResults(SearchViewModel searchViewModel) {

            searchViewModel.Donors = context.Donors
                    .Where(d => d.FirstName == searchViewModel.FirstName)
                    .Where(d => d.LastName == searchViewModel.LastName)
                    .ToList();
            searchViewModel.Addresses = new List<Address>();
            foreach (Donor donor in searchViewModel.Donors) {
                List<Address> donorAddresses = context.Addresses.Where(a => a.DonorID == donor.ID).ToList();

                if (donorAddresses != null)
                { foreach (Address donorAddress in donorAddresses)
                    {
                        searchViewModel.Addresses.Add(donorAddress);
                    }
                }
            }
            return View("Search", searchViewModel);
        }
        /*
        [Authorize]
        [Route("/AddFurniture")]
        public IActionResult AddFurniture()
        {
            AddFurnitureViewModel newFurniture = new AddFurnitureViewModel();
            return View("Index", newFurniture);
        }

        [Authorize]
        [HttpPost]
        [Route("/AddFurniture")]
        public IActionResult AddFurniture(AddFurnitureViewModel addFurniture)
        {
            Furniture newFurniture = new Furniture
            {
                Name = addFurniture.Name
            };
            context.Add(newFurniture);
            context.SaveChanges();
            return View("Index", addFurniture);
        }*/
    }
}
