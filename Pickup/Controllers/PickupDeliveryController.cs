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
            ViewBag.Title = "Customer Information";
            CustomerViewModel donorViewModel = new CustomerViewModel();
            return View(donorViewModel);
        }

        [HttpPost]
        public IActionResult Customer(CustomerViewModel customerViewModel)
        {
            ViewBag.Title = "Customer Information";
            if (ModelState.IsValid)
            { Donor existingDonor = context.Donors
                    .Where(d => d.FirstName == customerViewModel.FirstName)
                    .Where(d => d.LastName == customerViewModel.LastName)
                    .Where(d => d.PhoneNumber == customerViewModel.PhoneNumber)
                    .FirstOrDefault();

                if (existingDonor != null)
                {
                    return Redirect("/PickupDelivery/Address?customerId=" + existingDonor.ID);
                }

                Donor newDonor = new Donor
                {
                    FirstName = customerViewModel.FirstName,
                    LastName = customerViewModel.LastName,
                    PhoneNumber = customerViewModel.PhoneNumber,
                    PhoneNumberTwo = customerViewModel.PhoneNumberTwo
                };
                context.Add(newDonor);
                context.SaveChanges();

                // I suspect there is a better way to do this thing here....
                return Redirect("/PickupDelivery/Address?customerId=" + newDonor.ID);
            }

            return View(customerViewModel);
        }

        public IActionResult Address(int customerId)
        {
            ViewBag.Title = "Address Information";

            Donor donor = context.Donors.Single(d => d.ID == customerId);
            AddressViewModel addressViewModel = new AddressViewModel();
            return View("Index", addressViewModel);
        }

        [HttpPost]
        public IActionResult Address(AddressViewModel addressViewModel)
        {
            ViewBag.Title = "Address Information";

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
                return Redirect("/PickupDelivery/CreateNew?addressId=" + newAddress.ID);
            }
            return View("Index", addressViewModel);
        }

        public IActionResult CreateNew(int addressId)
        {
            ViewBag.Title = "Create New Pickup/Delivery";

            Address address = context.Addresses.Single(d => d.ID == addressId);
            //PickupDeliveryViewModel donationViewModel = new PickupDeliveryViewModel();
            return View("Index", new PickupDeliveryViewModel());
        }

        [HttpPost]
        public IActionResult CreateNewPickup(PickupDeliveryViewModel deliveryPickupViewModel)
        {
            ViewBag.Title = "Create New Pickup/Delivery";

            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
                string scheduler = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                PickupOrDelivery newPickup = new PickupOrDelivery               

                {
                    Delivery = deliveryPickupViewModel.Delivery,
                    ScheduleDateTime = currentDate,
                    PickupDate = deliveryPickupViewModel.PickupDate,
                    PickupTime = deliveryPickupViewModel.PickupTime,
                    CallEnRoute = deliveryPickupViewModel.CallEnRoute,
                    SpecialInstructions = deliveryPickupViewModel.SpecialInstructions,
                    AddressID = deliveryPickupViewModel.AddressId,
                    UserId = scheduler
                };

                context.Add(newPickup);
                context.SaveChanges();

                if (newPickup.Delivery)
                    return Redirect("/");

                return Redirect("/");
            }
            return View("Index", deliveryPickupViewModel);

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
