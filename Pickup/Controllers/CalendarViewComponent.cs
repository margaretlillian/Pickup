using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models.ScheduleViewModels;
using Pickup.QueryClasses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;
        CalendarViewQuery query = new CalendarViewQuery();

        public CalendarViewComponent(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IViewComponentResult Invoke()
        {
            Dictionary<DateTime, IList<CalendarViewModel>> pickupsDates = new Dictionary<DateTime, IList<CalendarViewModel>>();
            for (var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); date.Month == DateTime.Now.Month; date = date.AddDays(1))
            {
                IList<CalendarViewModel> results = query.CreateScheduleQuery(context, date.ToShortDateString());
                pickupsDates.Add(date, results);

            }
            return View("MonthlyCalendar", pickupsDates);
        }
    }
}
