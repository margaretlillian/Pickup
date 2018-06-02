using Pickup.Data;
using Pickup.Models.BlacklistViewModels;
using Pickup.Models.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.QueryClasses
{
    public class ViewInformationQuery
    {
        public List<FurnitureListing> CreateFurnitureListQuery(ApplicationDbContext context, int id)
        {
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
                 DonorCustomer = d,
                 Address = a,
                 PickupOrDelivery = p,
                 Scheduler = s.FullName
             });
            return results;
        }

        public List<ViewBlacklistedViewModel> ViewBlacklisted(ApplicationDbContext context)
        {
            return (from b in context.BlacklistedDonors
                    join dc in context.DonorsCustomers on b.DonorCustomerID equals dc.ID
                    join a in context.Addresses on dc.ID equals a.DonorCustomerID
                    select new ViewBlacklistedViewModel()
                    {
                        DonorCustomer = dc,
                        Address = a,
                        Reason = b.Reason
                    }).ToList();
        }

      

    }
}

