using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.BlacklistViewModels;
using Pickup.Models.HomeViewModel;
using Pickup.Models.QueryClasses;
using Pickup.Models.SearchViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class BlacklistController : Controller
    {
        private readonly ApplicationDbContext context;
        ViewInformationQuery viewInformationQuery = new ViewInformationQuery();
        CheckForExistingQuery checkForExisting = new CheckForExistingQuery();

        public BlacklistController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            
           return View(viewInformationQuery.ViewBlacklisted(context));
        }

        public IActionResult Search()
        {
            return View(new SearchViewModel());
        }

        public IActionResult AddToBlacklist(int customerId)
        {
            DonorCustomer donor = checkForExisting.GetCustomer(context, customerId);
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
            Blacklist checkBlacklist = checkForExisting.GetBlacklistedCustomer(context, model.CustomerID);
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
            
            return View(model);
        }

        public IActionResult BlackoutDay()
        {
            return View();
        }
    }
}
