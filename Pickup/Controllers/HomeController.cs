using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.HomeViewModel;
using Pickup.Models.QueryClasses;
using Pickup.Models.ScheduleViewModels;
namespace Pickup.Controllers
    {


    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        AllPickupDeliveryInformationQuery query = new AllPickupDeliveryInformationQuery();

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;

        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("/View")]
        public IActionResult View(int id)
        {
            PickupOrDelivery individual = context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault();
            if (individual == null)
                return Redirect("/");

            ViewInformationViewModel results = query.CreateQuery(context, id);
            results.Furniture = query.CreateFurnitureListQuery(context, id);
            return View(results);
        }


        [Route("/ViewPopup")]
        public IActionResult ViewPopup(int id)
        {
            PickupOrDelivery individual = context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault();
            if (individual == null)
                return Redirect("/");

            var furnitureItems = (from fpd in context.FurnitureDonationPickups
                                  join f in context.Furniture on fpd.FurnitureID equals f.ID
                                  where fpd.DonationPickupID == id
                                  select new FurnitureListing
                                  {
                                      Name = f.Name,
                                      ID = f.ID,
                                      Quantity = fpd.Quantity,
                                      Description = fpd.Description
                                  }).ToList();
            var listItems = new List<FurnitureListing>();
            foreach (var item in furnitureItems)
            {
                listItems.Add(new FurnitureListing()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Description = item.Description

                });

            }

            ViewInformationViewModel results = (from p in context.PickupsDeliveries
                                                join s in context.Users on p.UserId equals s.Id
                                                join a in context.Addresses on p.AddressID equals a.ID
                                                join d in context.DonorsCustomers on a.DonorCustomerID equals d.ID
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
                                                    Scheduler = s.FullName,
                                                    PickupID = p.ID,
                                                    Cancelled = p.Cancelled,
                                                }).FirstOrDefault();

            results.Furniture = listItems;
            return View(results);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
