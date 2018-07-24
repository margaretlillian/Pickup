using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.QueryClasses;
using Pickup.Models.SearchViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext context;
        private SearchQuery query = new SearchQuery();

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
            model.SearchResults = query.NameSearch(context, model.FirstName, model.LastName);
            return View("Index", model);
        }
    }
}
