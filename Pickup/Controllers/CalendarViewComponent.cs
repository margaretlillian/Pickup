using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models.QueryClasses;
using Pickup.Models.ScheduleViewModels;
using Pickup.QueryClasses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class CalendarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;
        CalendarViewQuery query = new CalendarViewQuery();
        CheckForExistingQuery checkForExisting = new CheckForExistingQuery();

        public CalendarViewComponent(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IViewComponentResult Invoke()
        {
            Dictionary<DateViewModel, IList<int>> pickupsDates = new Dictionary<DateViewModel, IList<int>>();
            for (DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); date.Month == DateTime.Now.Month; date = date.AddDays(1))
            {
                IList<int> results = query.MiniCalendarCountQuery(context, date.ToShortDateString());
                DateViewModel model = new DateViewModel() {
                Date = date,
                IsBlackedOut = checkForExisting.GetBlackoutDay(context, date.ToShortDateString())};
                pickupsDates.Add(model, results);

            }
            return View("MonthlyCalendar", pickupsDates);
        }
    }
}
