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
    public class CancelEditPickupDeliveryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CancelEditPickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        [Route("/EditChoice")]
        public IActionResult Index(int id)
        {
            PickupOrDelivery model = context.PickupsDeliveries.Where(pickup => pickup.ID == id).FirstOrDefault();
            return View(model);
        }

        public IActionResult Cancel(int id)
        {
            PickupOrDelivery model = context.PickupsDeliveries.Where(pickup => pickup.ID == id).FirstOrDefault();
            return View(model);
        }
    }
}
