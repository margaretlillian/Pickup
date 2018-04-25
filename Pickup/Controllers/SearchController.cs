using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;

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

            model.Donors = context.Donors
                    .Where(d => d.FirstName == model.FirstName)
                    .Where(d => d.LastName == model.LastName)
                    .ToList();
            model.Addresses = new List<Address>();
            foreach (Donor donor in model.Donors)
            {
                List<Address> donorAddresses = context.Addresses.Where(a => a.DonorID == donor.ID).ToList();

                if (donorAddresses != null)
                {
                    foreach (Address donorAddress in donorAddresses)
                    {
                        model.Addresses.Add(donorAddress);
                    }
                }
            }
            return View("Index", model);
        }
    }
}
