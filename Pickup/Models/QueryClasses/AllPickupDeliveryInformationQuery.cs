using Pickup.Models.HomeViewModel;
using Pickup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.QueryClasses
{
    public class AllPickupDeliveryInformationQuery
    {
        public List<FurnitureListing> CreateFurnitureListQuery(ApplicationDbContext context, int id) {
            List<FurnitureListing> furnitureItems = (from fpd in context.FurnitureDonationPickups
                                  join f in context.Furniture on fpd.FurnitureID equals f.ID
                                  where fpd.DonationPickupID == id
                                  select new FurnitureListing
                                  {
                                      Name = f.Name,
                                      ID = f.ID,
                                      Quantity = fpd.Quantity,
                                      Description = fpd.Description
                                  }).ToList();

            var listItems = new List<FurnitureListing>();
            foreach (var item in furnitureItems)
            {
                listItems.Add(new FurnitureListing()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Description = item.Description

                });

            }
            return listItems;
        }

        public IQueryable CreateQuery(ApplicationDbContext context)
{
   IQueryable results =
   (from p in context.PickupsDeliveries
             join s in context.Users on p.UserId equals s.Id
             join a in context.Addresses on p.AddressID equals a.ID
             join d in context.DonorsCustomers on a.DonorCustomerID equals d.ID
             select new ViewInformationViewModel()
             {
                 FirstName = d.FirstName,
                 LastName = d.LastName,
                 PhoneNumber = d.PhoneNumber,
                 PhoneNumberTwo = d.PhoneNumberTwo,
                 StreetAddress = a.Street,
                 Apt = a.Apartment,
                 City = a.City,
                 ZIP = a.ZIP,
                 Neighborhood = a.Neighborhood,
                 Delivery = p.Delivery,
                 ScheduleDateTime = p.ScheduleDateTime,
                 CallEnRoute = p.CallEnRoute,
                 SpecialInstructions = p.SpecialInstructions,
                 PickupDateTime = p.PickupDateTime,
                 Scheduler = s.FullName,
                 PickupID = p.ID,
                 Cancelled = p.Cancelled,
             });
            return results;
        }

    }
}
