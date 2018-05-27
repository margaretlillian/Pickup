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

        public ScheduleController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        // GET: /<controller>/
        [Authorize]
        public IActionResult Index(int weekId, bool popup)
        {

            CalendarViewQuery query = new CalendarViewQuery();
            Dictionary<DateTime, IList<WeeklyCalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<WeeklyCalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                IList<WeeklyCalendarViewModel> results = query.CreateQuery(context, theDate.ToShortDateString());
                
                    pickupsDates.Add(theDate, results);
                
            }

            if (popup)
                return View("SchedulePopup", pickupsDates);
            
            return View(pickupsDates);
        }
        
    }
}
