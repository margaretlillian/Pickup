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
        private CalendarViewQuery query = new CalendarViewQuery();
        private CheckForExistingQuery checkForExisting = new CheckForExistingQuery();

        public ScheduleController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Route("/")]
        public IActionResult HomePage()
        {
            IOrderedEnumerable<CalendarViewModel> results = query.CreateScheduleQuery(context, DateTime.Today.ToShortDateString());
            return View(results);
        }


        // GET: /<controller>/
        public IActionResult Index(int weekId, bool popup)
        {

            Dictionary<DateViewModel, IOrderedEnumerable<CalendarViewModel>> pickupsDates = new Dictionary<DateViewModel, IOrderedEnumerable<CalendarViewModel>>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                IOrderedEnumerable<CalendarViewModel> results = query.CreateScheduleQuery(context, theDate.ToShortDateString());
                DateViewModel model = new DateViewModel
                {
                    Date = theDate,
                    IsBlackedOut = checkForExisting.GetBlackoutDay(context, theDate.ToShortDateString())
                };
                    pickupsDates.Add(model, results);
                
            }

            if (popup)
                return View("SchedulePopup", pickupsDates);
            
            return View(pickupsDates);
        }

        public IActionResult DaySchedule(string date)
        {
            ViewInformationQuery dayQuery = new ViewInformationQuery();
           var model = dayQuery.CreateQuery(context)
                .Cast<ViewInformationViewModel>()
                .Where(day => day.PickupOrDelivery.PickupDateTime.ToShortDateString() == date).ToList();
            foreach (var item in model) {
                item.Furniture = dayQuery.CreateFurnitureListQuery(context, item.PickupOrDelivery.ID);
            }
            return View(model);
        }
        
    }
}