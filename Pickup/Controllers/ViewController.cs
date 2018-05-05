using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class ViewController : Controller
    {
        private readonly ApplicationDbContext context;

        public ViewController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            PickupOrDelivery pickup = context.PickupsDeliveries.Where(p => p.ID == id).SingleOrDefault();
            return View(pickup);
        }
    }
}
