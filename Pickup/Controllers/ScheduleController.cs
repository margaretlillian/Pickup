using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.HomeViewModel;
using Pickup.Models.QueryClasses;
using Pickup.Models.ScheduleViewModels;
using Pickup.QueryClasses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext context;
        CalendarViewQuery query = new CalendarViewQuery();

        public ScheduleController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month)) 
                             .Select(day => new DateTime(year, month, day))
                             .ToList();
        }
        [Route("/")]
        public IActionResult HomePage()
        {
            IList<CalendarViewModel> results = query.CreateScheduleQuery(context, DateTime.Today.ToShortDateString());
            return View(results);
        }


        // GET: /<controller>/
        public IActionResult Index(int weekId, bool popup)
        {

            Dictionary<DateTime, IList<CalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<CalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                IList<CalendarViewModel> results = query.CreateScheduleQuery(context, theDate.ToShortDateString());
                
                    pickupsDates.Add(theDate, results);
                
            }

            if (popup)
                return View("SchedulePopup", pickupsDates);
            
            return View(pickupsDates);
        }

        public IActionResult DaySchedule(string date)
        {
            AllPickupDeliveryInformationQuery dayQuery = new AllPickupDeliveryInformationQuery();
           var model = dayQuery.CreateQuery(context)
                .Cast<ViewInformationViewModel>()
                .Where(day => day.PickupDateTime.ToShortDateString() == date).ToList();
            foreach (var item in model) {
                item.Furniture = dayQuery.CreateFurnitureListQuery(context, item.PickupID);
            }
            return View(model);
        }
        
    }
}