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
    public class AddDeleteController : Controller
    {
        private readonly ApplicationDbContext context;

        public AddDeleteController(ApplicationDbContext applicationDbContext)
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
            Models.DonationPickupViewModels.AddFurnitureCategoryViewModel newFurnitureCategory = new AddFurnitureCategoryViewModel();
            return View("Index", newFurnitureCategory);
        }

        [HttpPost]
        public IActionResult AddFurnitureCategory(AddFurnitureCategoryViewModel addFurnitureCategory)
        {
            FurnitureCategory newFurnitureCategory = new FurnitureCategory
            {
                Name = addFurnitureCategory.Name
            };
            context.Add(newFurnitureCategory);
            context.SaveChanges();
            return View(addFurnitureCategory);
        }


        public IActionResult AddFurniture()
        {
            AddFurnitureViewModel addFurnitureViewModel = new AddFurnitureViewModel(context.FurnitureCategories.ToList());
            return View(addFurnitureViewModel);
        }

        [HttpPost]
        public IActionResult AddFurniture(AddFurnitureViewModel addFurnitureViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                FurnitureCategory furnitureCategory = context.FurnitureCategories.Single(cheese => cheese.ID == addFurnitureViewModel.FurnitureCategoryID);
                Furniture newFurniture = new Furniture
                {
                    Name = addFurnitureViewModel.Name,
                    FurnitureCategory = furnitureCategory
                };

                context.Furniture.Add(newFurniture);
                context.SaveChanges();

                return Redirect("/");
            }

            return View("Index", addFurnitureViewModel);
        }

    }
}
