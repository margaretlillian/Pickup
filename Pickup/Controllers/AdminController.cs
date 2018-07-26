using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.AccountViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;


        public AdminController(ApplicationDbContext applicationDbContext,
                      ILogger<ManageController> logger,
          UserManager<ApplicationUser> userManager)
        {
            context = applicationDbContext;
            _userManager = userManager;
            _logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        { var roles = context.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateUser(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var roles = context.Roles.Where(r => r.Name != "SuperAdmin").ToList();
            RegisterViewModel model = new RegisterViewModel()
            {
                Roles = roles.Select(r =>
            new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleID);
                    _logger.LogInformation("User created a new account with password.");
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Success");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
