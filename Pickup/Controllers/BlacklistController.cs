using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.BlacklistViewModels;
using Pickup.Models.QueryClasses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class BlacklistController : Controller
    {
        private readonly ApplicationDbContext context;
        GetByIDQuery query = new GetByIDQuery();

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
            DonorCustomer donor = query.GetCustomer(context, customerId);
            if (donor == null)
                return Redirect("/");

            AddtoBlacklistViewModel model = new AddtoBlacklistViewModel()
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToBlacklist(AddtoBlacklistViewModel model)
        {
            Blacklist checkBlacklist = query.CheckBlacklist(context, model.CustomerID);
            if (ModelState.IsValid && checkBlacklist == null)
            {
                Blacklist blacklistedPerson = new Blacklist
                {
                    DonorCustomerID = model.CustomerID,
                    Reason = model.Reason
                };
                context.Add(blacklistedPerson);
                context.SaveChanges();
                return Redirect("/Blacklist");
            }
            if (checkBlacklist != null)
                return Redirect("/");

            return View(model);
        }
    }
}
