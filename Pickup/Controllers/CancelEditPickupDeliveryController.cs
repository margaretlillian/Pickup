using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pickup.Data;
using Pickup.Models;
using Pickup.Models.QueryClasses;
using Pickup.Models.PickupDeliveryViewModels;
using Pickup.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pickup.Controllers
{
    public class CancelEditPickupDeliveryController : Controller
    {
        private readonly ApplicationDbContext context;
        private CheckForExistingQuery query = new CheckForExistingQuery();

        public CancelEditPickupDeliveryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        // GET: /<controller>/
        [Route("/EditChoice")]
        public IActionResult Index(int pid)
        {
            PickupOrDelivery pickups = context.PickupsDeliveries.Where(pickup => pickup.ID == pid).FirstOrDefault();
            if (pickups == null)
                return View("ErrorPage");
            EditCancelViewModels model = (from p in context.PickupsDeliveries
                                          where p.ID == pid
                                          join a in context.Addresses on p.AddressID equals a.ID
                                          join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                                          select new EditCancelViewModels {
                                          AddressID = a.ID,
                                          CustomerID = dc.ID,
                                          PickupID = p.ID,
                                          Delivery = p.Delivery}).FirstOrDefault();
            return View(model);
        }
        [Route("/Cancel")]
        public IActionResult Cancel(int pid)
        {
            var model = query.GetPickupOrDelivery(context, pid);

            if (model != null)
            return View(model);

            return Redirect("/");
        }

        [HttpPost]
        [Route("/Cancel")]
        public IActionResult Cancel(PickupOrDelivery model)
        {
            PickupOrDelivery pickupOrDelivery = query.GetPickupOrDelivery(context, model.ID);
            if (pickupOrDelivery != null)
            pickupOrDelivery.Cancelled = true;

            context.SaveChanges();
            return Redirect("/");
        }

        [Route("/EditCustomer")]
        public IActionResult EditCustomer(int customerId)
        {
            ViewBag.Title = "Edit Customer Information";
            ViewBag.Button = ViewBag.Title;

            DonorCustomer donorCustomer = query.GetCustomer(context, customerId);
            if (donorCustomer == null)
                return View("ErrorPage");

            CustomerViewModel model = new CustomerViewModel() { CustomerId = donorCustomer.ID,
            FirstName = donorCustomer.FirstName,
            LastName = donorCustomer.LastName,
            PhoneNumber = PhoneNumberFormatter.FormatPhoneNumber(donorCustomer.PhoneNumber),
            PhoneNumberTwo = PhoneNumberFormatter.FormatPhoneNumber(donorCustomer.PhoneNumberTwo),
            FOT = donorCustomer.FOT};
            return View("PickupDelivery/Customer", model);
        }

        [Route("/EditCustomer")]
        [HttpPost]
        public IActionResult EditCustomer(CustomerViewModel model)
        {
            DonorCustomer donorCustomer = query.GetCustomer(context, model.CustomerId);
            if (donorCustomer == null && !ModelState.IsValid)
                return View("PickupDelivery/Customer", model);

                donorCustomer.FirstName = model.FirstName;
                donorCustomer.LastName = model.LastName;
                donorCustomer.PhoneNumber = PhoneNumberFormatter.FormatPhoneNumber(model.PhoneNumber);
                donorCustomer.PhoneNumberTwo = PhoneNumberFormatter.FormatPhoneNumber(model.PhoneNumberTwo);
                donorCustomer.Email = model.Email;
                donorCustomer.FOT = model.FOT;

                context.SaveChanges();

            return Redirect("/");
        }

        [Route("/EditAddress")]
        public IActionResult EditAddress(int addressId)
        {  ViewBag.Title = "Edit Address Information";
            ViewBag.Button = ViewBag.Title;

            Address address = query.GetAddress(context, addressId);
            if (address == null)
                return View("ErrorPage");

            AddressViewModel model = new AddressViewModel()
            {
            AddressId = address.ID,
            Street = address.Street,
            Apartment = address.Apartment,
            City = address.City,
            ZIP = address.ZIP,
            Neighborhood = address.Neighborhood,
            BottomFloor = address.BottomFloor
            };
            return View("PickupDelivery/Address", model);
        }

        [Route("/EditAddress")]
        [HttpPost]
        public IActionResult EditAddress(AddressViewModel model)
        {

            Address address = query.GetAddress(context, model.AddressId);
            if (address == null && !ModelState.IsValid)
                return View("PickupDelivery/Address", model);

            address.Street = model.Street;
            address.Apartment = model.Apartment;
            address.City = model.City;
            address.ZIP = model.ZIP;
            address.Neighborhood = model.Neighborhood;
            address.BottomFloor = model.BottomFloor;

            context.SaveChanges();

            return Redirect("/");
        }


        [Route("/EditPD")]
        public IActionResult EditPickupDelivery(int pid)
        {
            ViewBag.Title = "Edit Pickup/Delivery Information";
            ViewBag.Button = ViewBag.Title;

            PickupOrDelivery pickupOrDelivery = query.GetPickupOrDelivery(context, pid);
            if (pickupOrDelivery == null)
                return View("ErrorPage");

            CreatePickupDeliveryViewModel model = new CreatePickupDeliveryViewModel()
            {PickupId = pickupOrDelivery.ID,
            CallEnRoute = pickupOrDelivery.CallEnRoute,
            Delivery = pickupOrDelivery.Delivery,
            SpecialInstructions = pickupOrDelivery.SpecialInstructions,
            PickupDate = DateTime.Parse(pickupOrDelivery.PickupDateTime.ToShortDateString()),
            PickupTime = DateTime.Parse(pickupOrDelivery.PickupDateTime.ToShortTimeString())
            };
            return View("PickupDelivery/CreateNew", model);
        }

        [Route("/EditPD")]
        [HttpPost]
        public IActionResult EditPickupDelivery(CreatePickupDeliveryViewModel model)
        {
            PickupOrDelivery pickupOrDelivery = query.GetPickupOrDelivery(context, model.PickupId);
            if (pickupOrDelivery == null && !ModelState.IsValid)
                return View("PickupDelivery/CreateNew", model);
            DateTime pickupDateTime = new DateTime(model.PickupDate.Year,
                    model.PickupDate.Month,
                    model.PickupDate.Day,
                    model.PickupTime.Hour,
                    model.PickupTime.Minute,
                    0);
            pickupOrDelivery.Delivery = model.Delivery;
            pickupOrDelivery.CallEnRoute = model.CallEnRoute;
            pickupOrDelivery.SpecialInstructions = model.SpecialInstructions;
            pickupOrDelivery.PickupDateTime = pickupDateTime;

            context.SaveChanges();

            return Redirect("/");
        }

        [Route("/EditItems")]
        public IActionResult EditItems(int pid)
        {
            ViewBag.Title = "Edit Items Picked Up/Delivered";
            ViewBag.Button = ViewBag.Title;

            PickupOrDelivery pd = query.GetPickupOrDelivery(context, pid);
            if (pd == null)
                return View("ErrorPage");

            var model = new ItemPickupViewModel() {
            PickupID = pid};
            List<ItemDonatedSold> furnitureItems = context.ItemsDonatedSold.ToList();
            List<ItemsAndPickupOrDelivery> selectedPieces = context.ItemsPickupsDeliveries.Where(f => f.PickupDeliveryID == pd.ID).ToList();
            List<ItemQuantityList> quantityListItems = new List<ItemQuantityList>();
            foreach (var item in furnitureItems)
            {
                quantityListItems.Add(new ItemQuantityList()
                {
                    CategoryID = item.ItemCategoryID,
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = (from s in selectedPieces where s.ItemID == item.ID select s.Quantity).SingleOrDefault()
                });
            }
            IList<CategoryBlock> itemsInCategoryBlock = (from fc in context.ItemCategories
                                          select new CategoryBlock
                                          {
                                              Category = fc,
                                              Furniture = quantityListItems.Where(f => f.CategoryID == fc.ID).ToList()
                                          }).ToList();
            model.FurnitureList = itemsInCategoryBlock;
            return View("PickupDelivery/ItemPickup", model);
        }

        [HttpPost]
        [Route("/EditItems")]
        public IActionResult EditItems(ItemPickupViewModel model)
        {
            foreach (var existingItem in query.GetItemsPD(context, model.PickupID)) {
                context.ItemsPickupsDeliveries.Remove(existingItem);
                context.SaveChanges();
            }
            foreach (CategoryBlock categoryBlock in model.FurnitureList)
            {
                var selectedFurniture = categoryBlock.Furniture.Where(y => y.Quantity > 0).ToList();
                foreach (var furniturePiece in selectedFurniture)
                {
                    ItemsAndPickupOrDelivery furnitureDonationPickup = new ItemsAndPickupOrDelivery
                    {
                        PickupDeliveryID = model.PickupID,
                        ItemID = furniturePiece.ID,
                        Quantity = furniturePiece.Quantity
                    };
                    context.Add(furnitureDonationPickup);
                }
            }
            context.SaveChanges();
            return RedirectToAction("View", "Home", new { id = model.PickupID });

        }
    }

    }

