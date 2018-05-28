using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;
using Pickup.Models.SearchViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext context;

        public SearchController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
          return View(new SearchViewModel());
        }

        public IActionResult SearchResults(SearchViewModel model)
        {
            List<CustomerSearchResults> query = (from dc in context.DonorsCustomers
                         where dc.FirstName == model.FirstName || dc.LastName == model.LastName
                         select new CustomerSearchResults {
                         DonorCustomer = dc,
                         Addresses = context.Addresses.Where(a => a.DonorCustomerID == dc.ID).ToList()}).ToList();
            model.SearchResults = query;
            return View("Index", model);
        }
    }
}
