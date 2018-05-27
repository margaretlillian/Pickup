using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.DonationPickupViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class AddEditDeleteController : Controller
    {
        private readonly ApplicationDbContext context;

        public AddEditDeleteController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddFurnitureCategory()
        {
            return View("Index", new AddFurnitureCategoryViewModel());
        }

        [HttpPost]
        public IActionResult AddFurnitureCategory(AddFurnitureCategoryViewModel model)
        {
            FurnitureCategory newFurnitureCategory = new FurnitureCategory
            {
                Name = model.Name
            };
            context.Add(newFurnitureCategory);
            context.SaveChanges();
            return View("Index", model);
        }


        public IActionResult AddFurniture()
        {
            return View(new AddFurnitureViewModel(context.FurnitureCategories.ToList()));
        }

        [HttpPost]
        public IActionResult AddFurniture(AddFurnitureViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                FurnitureCategory furnitureCategory = context.FurnitureCategories.Single(cheese => cheese.ID == model.FurnitureCategoryID);
                Furniture newFurniture = new Furniture
                {
                    Name = model.Name,
                    FurnitureCategory = furnitureCategory
                };

                context.Furniture.Add(newFurniture);
                context.SaveChanges();

                return View("Index", model);
            }

            return View("Index", model);
        }

        [Route("/EditPickup")]
        public IActionResult EditPickup(int pickupId)
        {
            PickupOrDelivery pickup = context.PickupsDeliveries.Where(p => p.ID == pickupId).FirstOrDefault();
            return View(pickup);
        }

    }
}
