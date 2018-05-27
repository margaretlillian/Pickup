using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.HomeViewModel;
using Pickup.Models.QueryClasses;
using Pickup.Models.ScheduleViewModels;
namespace Pickup.Controllers
    {


    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        AllPickupDeliveryInformationQuery query = new AllPickupDeliveryInformationQuery();

        public HomeController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;

        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("/View")]
        public IActionResult View(int id, bool popup)
        {
            PickupOrDelivery individual = context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault();
            if (individual == null)
                return Redirect("/");

            ViewInformationViewModel results = query.CreateQuery(context, id);
            results.Furniture = query.CreateFurnitureListQuery(context, id);

            if (popup)
                return View("ViewPopup", results);
            return View(results);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
