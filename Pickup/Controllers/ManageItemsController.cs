using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.ManageItemsViewModels;
using Pickup.Models.PickupDeliveryViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
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

            var model = new ItemPickupViewModel();
            var furnitureItems = context.ItemsDonatedSold.ToList();
            var quantityListItems = new List<ItemQuantityList>();
            foreach (var item in furnitureItems)
            {
                quantityListItems.Add(new ItemQuantityList()
                {
                    CategoryID = item.ItemCategoryID,
                    Name = item.Name
                });
            }
            IList<CategoryBlock> itemsInCategoryBlock = (from fc in context.ItemCategories
                                                         select new CategoryBlock
                                                         {
                                                             Category = fc,
                                                             Furniture = quantityListItems.Where(f => f.CategoryID == fc.ID).ToList()
                                                         }).ToList();
            model.FurnitureList = itemsInCategoryBlock;
            return View(model);
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

        [HttpPost]
        public IActionResult Additem(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                ItemCategory itemCategory = context.ItemCategories.Single(category => category.ID == model.CategoryID);
                ItemDonatedSold item = new ItemDonatedSold()
                {
                    Name = model.Name,
                    ItemCategory = itemCategory
                };
                context.ItemsDonatedSold.Add(item);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
