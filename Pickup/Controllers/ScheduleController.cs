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
            List<DateTime> week = new List<DateTime>();
            for (int i = 1; i < 7; i++)
            {
                week.Add(DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i)));
            }
            return View(week);
        }

        public IActionResult Schedule(int weekId)
        {
            List<PickupOrDelivery> pickups = new List<PickupOrDelivery>();
            for (int i = 1; i < 7; i++)
            {
                DateTime theDate = DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i));
                var results = (from p in context.PickupsDeliveries
                               where p.PickupDateTime.ToString("dd-MM-yy") == theDate.ToString("dd-MM-yy")
                               select p).ToList();
                foreach (var item in results)
                {
                    pickups.Add(item);
                }
            }
            
            return View(pickups);
        }
    }
}
