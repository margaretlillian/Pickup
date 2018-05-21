using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;

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
            PickupOrDelivery model = context.PickupsDeliveries.Where(pickup => pickup.ID == pid).FirstOrDefault();
            return View(model);
        }
        [Route("/Cancel")]
        public IActionResult Cancel(int pid)
        {
            PickupOrDelivery model = context.PickupsDeliveries.Where(pickup => pickup.ID == pid).FirstOrDefault();
            return View(model);
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
            DonorCustomer donorCustomer = context.DonorsCustomers.Single(dc => dc.ID == customerId);
            CustomerViewModel model = new CustomerViewModel() { CustomerId = donorCustomer.ID,
            FirstName = donorCustomer.FirstName,
            LastName = donorCustomer.LastName,
            PhoneNumber = donorCustomer.PhoneNumber,
            PhoneNumberTwo = donorCustomer.PhoneNumberTwo,
            FOT = donorCustomer.FOT};
            return View("PickupDelivery/FormDefault", model);
        }

        [Route("/EditCustomer")]
        [HttpPost]
        public IActionResult EditCustomer(CustomerViewModel model)
        {
            DonorCustomer donorCustomer = context.DonorsCustomers.Single(dc => dc.ID == model.CustomerId);
            if (donorCustomer == null && !ModelState.IsValid)
                return View("PickupDelivery/FormDefault", model);
            else
            {
                donorCustomer.FirstName = model.FirstName;
                donorCustomer.LastName = model.LastName;
                donorCustomer.PhoneNumber = model.PhoneNumber;
                donorCustomer.PhoneNumberTwo = model.PhoneNumberTwo;
                donorCustomer.FOT = model.FOT;

                context.SaveChanges();

            }

            return Redirect("/");
        }
    }
}
