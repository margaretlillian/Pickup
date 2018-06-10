﻿using Pickup.Data;
using Pickup.Models.BlacklistViewModels;
using Pickup.Models.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.QueryClasses
{
    internal class ViewInformationQuery
    {
        internal List<FurnitureListing> CreateFurnitureListQuery(ApplicationDbContext context, int id)
        {
            List<FurnitureListing> furnitureItems = (from fpd in context.ItemsPickupsDeliveries
                                                     join f in context.ItemsDonatedSold on fpd.ItemID equals f.ID
                                                     where fpd.PickupDeliveryID == id
                                                     select new FurnitureListing
                                                     {
                                                         Name = f.Name,
                                                         ID = f.ID,
                                                         Quantity = fpd.Quantity,
                                                         Description = fpd.Description
                                                     }).ToList();

            List<FurnitureListing> listItems = new List<FurnitureListing>();
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

        internal IQueryable CreateQuery(ApplicationDbContext context)
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

        internal IList<ViewBlacklistedViewModel> ViewBlacklisted(ApplicationDbContext context)
        {
            return (from b in context.BlacklistedDonors
                    join dc in context.DonorsCustomers on b.DonorCustomerID equals dc.ID
                    select new ViewBlacklistedViewModel()
                    {
                        DonorCustomer = dc,
                        Reason = b.Reason
                    }).ToList();
        }

      

    }
}

