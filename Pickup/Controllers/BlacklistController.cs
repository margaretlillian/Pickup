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
        SearchQuery searchQuery = new SearchQuery();

        public BlacklistController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            SearchToBlacklist model = new SearchToBlacklist()
            {
                Results = viewInformationQuery.ViewBlacklisted(context)
            };
           return View(model);
        }


        public IActionResult SearchAdd(SearchToBlacklist model)
        {
            model.Results = searchQuery.SearchAddToBlacklist(context, model.FirstName, model.LastName);
            return View(model);
        }

        public IActionResult AddToBlacklist(int customerId)
        {
            DonorCustomer donor = checkForExisting.GetCustomer(context, customerId);
            if (donor == null)
                return Redirect("/");

            AddtoBlacklistViewModel model = new AddtoBlacklistViewModel()
            {Customer = donor
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToBlacklist(AddtoBlacklistViewModel model)
        {
            Blacklist checkBlacklist = checkForExisting.GetBlacklistedCustomer(context, model.Customer.ID);
            if (ModelState.IsValid && checkBlacklist == null)
            {
                Blacklist blacklistedPerson = new Blacklist
                {
                    DonorCustomerID = model.Customer.ID,
                    Reason = model.Reason
                };
                context.Add(blacklistedPerson);
                context.SaveChanges();
                return Redirect("/Blacklist");
            }
            
            return View(model);
        }
        
        [Route("/Blacklisted")]
        public IActionResult BlacklistedCustomerTrigger(int customerId)
        {
            return View(checkForExisting.GetBlacklistedCustomer(context, customerId));
        }

        public IActionResult BlackoutDay()
        {
            return View();
        }
    }
}
