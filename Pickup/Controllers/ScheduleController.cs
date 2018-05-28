using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
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
            IList<WeeklyCalendarViewModel> results = query.CreateWeeklyQuery(context, DateTime.Today.ToString());
            return View(results);
        }


        // GET: /<controller>/
        public IActionResult Index(int weekId, bool popup)
        {

            Dictionary<DateTime, IList<WeeklyCalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<WeeklyCalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                IList<WeeklyCalendarViewModel> results = query.CreateWeeklyQuery(context, theDate.ToShortDateString());
                
                    pickupsDates.Add(theDate, results);
                
            }

            if (popup)
                return View("SchedulePopup", pickupsDates);
            
            return View(pickupsDates);
        }
        
        public IActionResult MonthlyCalendar()
        {
            Dictionary<DateTime, IList<MonthlyMiniCalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<MonthlyMiniCalendarViewModel>>();
            for (var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); date.Month == DateTime.Now.Month; date = date.AddDays(1))
            {
                IList<MonthlyMiniCalendarViewModel> results = query.CreateMonthlyQuery(context, date.ToShortDateString());
                pickupsDates.Add(date, results);

            }
            return View(pickupsDates);
        }
    }
}