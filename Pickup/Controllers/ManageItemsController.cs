using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.ManageItemsViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class ManageItemsController : Controller
    {

        private readonly ApplicationDbContext context;

        public ManageItemsController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCategory()
        {
            return View(new CreateCategoryViewModel());
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ItemCategory newCategory = new ItemCategory()
                {
                    Name = model.Name
                };
                context.Add(newCategory);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult AddItem()
        {
            AddItemViewModel model = new AddItemViewModel(context.ItemCategories.ToList());
            return View(model);
        }
    }
}
