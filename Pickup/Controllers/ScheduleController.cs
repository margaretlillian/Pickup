using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Models.ScheduleViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: /<controller>/
        [Route("/")]
        public IActionResult Index(int weekId)
        {
            List<DateTime> week = new List<DateTime>();
            for (int i = 1; i < 7; i++)
            {
                week.Add(DateTime.Today.AddDays(weekId - 1 * (int)(DateTime.Today.DayOfWeek - i)));
            }
            return View(week);
        }
    }
}
