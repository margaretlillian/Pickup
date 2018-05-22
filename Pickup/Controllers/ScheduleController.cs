using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.ScheduleViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext context;

        public ScheduleController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        // GET: /<controller>/
        [Authorize]
        public IActionResult Index(int weekId)
        {
            Dictionary<DateTime, IList<WeeklyCalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<WeeklyCalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                var results = (from p in context.PickupsDeliveries
                               where p.PickupDateTime.ToShortDateString() == theDate.ToShortDateString()
                               join a in context.Addresses on p.AddressID equals a.ID
                               join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                               select new WeeklyCalendarViewModel {
                               City = a.City,
                               FirstName = dc.FirstName,
                               LastName = dc.LastName,
                               PickupID = p.ID,
                               Phone = dc.PhoneNumber,
                               PickupTime = p.PickupDateTime,
                               Cancelled = p.Cancelled,
                               Delivery = p.Delivery}).ToList();
                
                    pickupsDates.Add(theDate, results);
                
            }
            
            return View(pickupsDates);
        }

        public IActionResult SchedulePopup(int weekId)
        {
            Dictionary<DateTime, IList<WeeklyCalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<WeeklyCalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                var results = (from p in context.PickupsDeliveries
                               where p.PickupDateTime.ToShortDateString() == theDate.ToShortDateString()
                               join a in context.Addresses on p.AddressID equals a.ID
                               join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                               select new WeeklyCalendarViewModel
                               {
                                   City = a.City,
                                   FirstName = dc.FirstName,
                                   LastName = dc.LastName,
                                   PickupID = p.ID,
                                   Phone = dc.PhoneNumber,
                                   PickupTime = p.PickupDateTime,
                                   Cancelled = p.Cancelled,
                                   Delivery = p.Delivery
                               }).ToList();

                pickupsDates.Add(theDate, results);

            }

            return View(pickupsDates);
        }
    }
}
