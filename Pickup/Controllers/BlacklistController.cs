using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.BlacklistViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class BlacklistController : Controller
    {
        private readonly ApplicationDbContext context;

        public BlacklistController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToBlacklist(int customerId)
        {
            DonorCustomer donor = context.DonorsCustomers.Where(d => d.ID == customerId).SingleOrDefault();
            if (donor == null)
                return Redirect("/");
            AddtoBlacklistViewModel model = new AddtoBlacklistViewModel()
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName
            };

            return View(model);
        }
    }
}
