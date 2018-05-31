using Pickup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.QueryClasses
{
    public class CheckForExistingQuery
    {
        public DonorCustomer GetCustomer(ApplicationDbContext context, int id)
        { return context.DonorsCustomers.Where(d => d.ID == id).FirstOrDefault(); }

        public Address GetAddress(ApplicationDbContext context, int id)
        { return context.Addresses.Where(a => a.ID == id).FirstOrDefault(); }

        public PickupOrDelivery GetPickupOrDelivery(ApplicationDbContext context, int id)
        { return context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault(); }

        public Blacklist GetBlacklistedCustomer(ApplicationDbContext context, int id)
        { return context.BlacklistedDonors.Where(b => b.DonorCustomerID == id).FirstOrDefault(); }

        public IList<FurniturePickupOrDelivery> GetItemsPD(ApplicationDbContext context, int id)
        { return context.FurnitureDonationPickups.Where(fpd => fpd.DonationPickupID == id).ToList(); }


    }
}
