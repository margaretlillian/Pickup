using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.ViewViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class ViewController : Controller
    {
        private readonly ApplicationDbContext context;

        public ViewController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            ViewInformationViewModel model = new ViewInformationViewModel();
            var furnitureItems = context.FurnitureDonationPickups.ToList();
            var listItems = new List<FurnitureListing>();
            foreach (var item in furnitureItems)
            {
                listItems.Add(new FurnitureListing()
                {
                    ID = item.FurnitureID,
                    Quantity = item.Quantity,
                    Description = item.Description

                });
            }
            var results = (from p in context.PickupsDeliveries
                           join s in context.Users on p.UserId equals s.Id
                           join a in context.Addresses on p.AddressID equals a.ID
                           join d in context.Donors on a.DonorID equals d.ID
                           where p.ID == id
                           select new ViewInformationViewModel()
                           {
                               FirstName = d.FirstName,
                               LastName = d.LastName,
                               PhoneNumber = d.PhoneNumber,
                               PhoneNumberTwo = d.PhoneNumberTwo,
                               StreetAddress = a.Street,
                               Apt = a.Apartment,
                               City = a.City,
                               ZIP = a.ZIP,
                               Neighborhood = a.Neighborhood,
                               Delivery = p.Delivery,
                               ScheduleDateTime = p.ScheduleDateTime,
                               CallEnRoute = p.CallEnRoute,
                               SpecialInstructions = p.SpecialInstructions,
                               PickupDateTime = p.PickupDateTime,
                               Scheduler = s.FullName
                           }).FirstOrDefault();
            return View(results);
        }
    }
}
