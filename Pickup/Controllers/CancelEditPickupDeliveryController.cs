using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.PickupDeliveryViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class CancelEditPickupDeliveryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CancelEditPickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        [Route("/EditChoice")]
        public IActionResult Index(int pid)
        {
            PickupOrDelivery pickups = context.PickupsDeliveries.Where(pickup => pickup.ID == pid).FirstOrDefault();
            if (pickups == null)
                return Redirect("/");
            EditCancelViewModels model = (from p in context.PickupsDeliveries
                                          where p.ID == pid
                                          join a in context.Addresses on p.AddressID equals a.ID
                                          join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                                          select new EditCancelViewModels {
                                          AddressID = a.ID,
                                          CustomerID = dc.ID,
                                          PickupID = p.ID,
                                          Delivery = p.Delivery}).FirstOrDefault();
            return View(model);
        }
        [Route("/Cancel")]
        public IActionResult Cancel(int pid)
        {
            PickupOrDelivery model = context.PickupsDeliveries.Where(pickup => pickup.ID == pid).FirstOrDefault();
            if (model != null)
            return View(model);

            return Redirect("/");
        }

        [HttpPost]
        [Route("/Cancel")]
        public IActionResult Cancel(PickupOrDelivery model)
        {
            PickupOrDelivery pickupOrDelivery = context.PickupsDeliveries.Where(pickup => pickup.ID == model.ID).FirstOrDefault();
            if (pickupOrDelivery != null)
            pickupOrDelivery.Cancelled = true;

            context.SaveChanges();
            return Redirect("/");
        }

        [Route("/EditCustomer")]
        public IActionResult EditCustomer(int customerId)
        {
            ViewBag.Title = "Edit Customer Information";
            ViewBag.Button = ViewBag.Title;

            DonorCustomer donorCustomer = context.DonorsCustomers.Where(dc => dc.ID == customerId).FirstOrDefault();
            if (donorCustomer == null)
                return Redirect("/");

            CustomerViewModel model = new CustomerViewModel() { CustomerId = donorCustomer.ID,
            FirstName = donorCustomer.FirstName,
            LastName = donorCustomer.LastName,
            PhoneNumber = donorCustomer.PhoneNumber,
            PhoneNumberTwo = donorCustomer.PhoneNumberTwo,
            FOT = donorCustomer.FOT};
            return View("PickupDelivery/Customer", model);
        }

        [Route("/EditCustomer")]
        [HttpPost]
        public IActionResult EditCustomer(CustomerViewModel model)
        {
            DonorCustomer donorCustomer = context.DonorsCustomers.Where(dc => dc.ID == model.CustomerId).FirstOrDefault();
            if (donorCustomer == null && !ModelState.IsValid)
                return View("PickupDelivery/Customer", model);

                donorCustomer.FirstName = model.FirstName;
                donorCustomer.LastName = model.LastName;
                donorCustomer.PhoneNumber = model.PhoneNumber;
                donorCustomer.PhoneNumberTwo = model.PhoneNumberTwo;
                donorCustomer.Email = model.Email;
                donorCustomer.FOT = model.FOT;

                context.SaveChanges();

            return Redirect("/");
        }

        [Route("/EditAddress")]
        public IActionResult EditAddress(int addressId)
        {  ViewBag.Title = "Edit Address Information";
            ViewBag.Button = ViewBag.Title;

            Address address = context.Addresses.Where(dc => dc.ID == addressId).FirstOrDefault();
            if (address == null)
                return Redirect("/");

            AddressViewModel model = new AddressViewModel()
            {
            AddressId = address.ID,
            Street = address.Street,
            Apartment = address.Apartment,
            City = address.City,
            ZIP = address.ZIP,
            Neighborhood = address.Neighborhood,
            BottomFloor = address.BottomFloor
            };
            return View("PickupDelivery/Address", model);
        }

        [Route("/EditAddress")]
        [HttpPost]
        public IActionResult EditAddress(AddressViewModel model)
        {

            Address address = context.Addresses.Single(a => a.ID == model.AddressId);
            if (address == null && !ModelState.IsValid)
                return View("PickupDelivery/Address", model);

            address.Street = model.Street;
            address.Apartment = model.Apartment;
            address.City = model.City;
            address.ZIP = model.ZIP;
            address.Neighborhood = model.Neighborhood;
            address.BottomFloor = model.BottomFloor;

            context.SaveChanges();

            return Redirect("/");
        }


        [Route("/EditPD")]
        public IActionResult EditPickupDelivery(int pid)
        {
            ViewBag.Title = "Edit Pickup/Delivery Information";
            ViewBag.Button = ViewBag.Title;

            PickupOrDelivery pickupOrDelivery = context.PickupsDeliveries.Where(dc => dc.ID == pid).FirstOrDefault();
            if (pickupOrDelivery == null)
                return Redirect("/");

            CreatePickupDeliveryViewModel model = new CreatePickupDeliveryViewModel()
            {PickupId = pickupOrDelivery.ID,
            CallEnRoute = pickupOrDelivery.CallEnRoute,
            Delivery = pickupOrDelivery.Delivery,
            SpecialInstructions = pickupOrDelivery.SpecialInstructions,
            PickupDate = DateTime.Parse(pickupOrDelivery.PickupDateTime.ToShortDateString()),
            PickupTime = DateTime.Parse(pickupOrDelivery.PickupDateTime.ToShortTimeString())
            };
            return View("PickupDelivery/CreateNew", model);
        }

        [Route("/EditPD")]
        [HttpPost]
        public IActionResult EditPickupDelivery(CreatePickupDeliveryViewModel model)
        {
            PickupOrDelivery pickupOrDelivery = context.PickupsDeliveries.Single(a => a.ID == model.PickupId);
            if (pickupOrDelivery == null && !ModelState.IsValid)
                return View("PickupDelivery/CreateNew", model);
            DateTime pickupDateTime = new DateTime(model.PickupDate.Year,
                    model.PickupDate.Month,
                    model.PickupDate.Day,
                    model.PickupTime.Hour,
                    model.PickupTime.Minute,
                    0);
            pickupOrDelivery.Delivery = model.Delivery;
            pickupOrDelivery.CallEnRoute = model.CallEnRoute;
            pickupOrDelivery.SpecialInstructions = model.SpecialInstructions;
            pickupOrDelivery.PickupDateTime = pickupDateTime;

            context.SaveChanges();

            return Redirect("/");
        }

        public IActionResult EditItems(int pid)
        {
            ViewBag.Title = "Edit Items Picked Up/Delivered";
            ViewBag.Button = ViewBag.Title;

            PickupOrDelivery pickupOrDelivery = context.PickupsDeliveries.Where(dc => dc.ID == pid).FirstOrDefault();
            if (pickupOrDelivery == null)
                return Redirect("/");

            List<FurniturePickupOrDelivery> fpd = context.FurnitureDonationPickups.Where(f => f.DonationPickupID == pickupOrDelivery.ID).ToList();
            CategoryBlock categoryBlock = new CategoryBlock();

            foreach (var item in fpd)
            {
                ItemQuantityList model = new ItemQuantityList()
                {ID = item.FurnitureID,
                Name = item.Furniture.Name,
                Quantity = item.Quantity
                };
                categoryBlock.Furniture.Add(model);
            }
            
            return View("PickupDelivery/ItemPickup");

        }
        
    }
}
