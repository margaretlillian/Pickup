﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pickup.Data;

namespace Pickup.Models.QueryClasses
{ 
    public class GetByIDQuery
    {
        public DonorCustomer GetCustomer(ApplicationDbContext context, int id)
        {return context.DonorsCustomers.Where(d => d.ID == id).FirstOrDefault(); }

        public Address GetAddress(ApplicationDbContext context, int id)
        { return context.Addresses.Where(a => a.ID == id).FirstOrDefault();}

        public PickupOrDelivery GetPickupOrDelivery(ApplicationDbContext context, int id)
        { return context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault(); }

        public Blacklist CheckBlacklist(ApplicationDbContext context, int id)
        { return context.BlacklistedDonors.Where(b => b.DonorCustomerID == id).FirstOrDefault(); }

        public IList<FurniturePickupOrDelivery> GetItemsPD(ApplicationDbContext context, int id)
        { return context.FurnitureDonationPickups.Where(fpd => fpd.DonationPickupID == id).ToList(); }

        
    }
}