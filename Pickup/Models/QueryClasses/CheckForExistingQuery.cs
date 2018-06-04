using Pickup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.QueryClasses
{
    internal class CheckForExistingQuery
    {
        internal DonorCustomer GetCustomer(ApplicationDbContext context, int id)
        { return context.DonorsCustomers.Where(d => d.ID == id).FirstOrDefault(); }

        internal IList<DonorCustomer> GetCustomerByFullName(ApplicationDbContext context, string firstName, string lastName)
        {
            return context.DonorsCustomers
                  .Where(d => d.FirstName == firstName)
                  .Where(d => d.LastName == lastName)
                  .ToList();
        }

        internal Address GetAddress(ApplicationDbContext context, int id)
        { return context.Addresses.Where(a => a.ID == id).FirstOrDefault(); }

        internal PickupOrDelivery GetPickupOrDelivery(ApplicationDbContext context, int id)
        { return context.PickupsDeliveries.Where(p => p.ID == id).FirstOrDefault(); }

        internal Blacklist GetBlacklistedCustomerById(ApplicationDbContext context, int id)
        { return context.BlacklistedDonors.Where(b => b.DonorCustomerID == id).FirstOrDefault(); }

        internal IList<FurniturePickupOrDelivery> GetItemsPD(ApplicationDbContext context, int id)
        { return context.FurnitureDonationPickups.Where(fpd => fpd.DonationPickupID == id).ToList(); }


    }
}
