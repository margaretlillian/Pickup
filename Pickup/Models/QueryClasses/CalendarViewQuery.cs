using Pickup.Data;
using Pickup.Models.QueryClasses;
using Pickup.Models.ScheduleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.QueryClasses
{
    internal class CalendarViewQuery
    {
        CheckForExistingQuery query = new CheckForExistingQuery();

        internal IOrderedEnumerable<CalendarViewModel> CreateScheduleQuery(ApplicationDbContext context, string date)
        { 

            var results = (from p in context.PickupsDeliveries
                           where p.PickupDateTime.ToShortDateString() == date
                           where p.Cancelled == false
                           join a in context.Addresses on p.AddressID equals a.ID
                           join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                           select new CalendarViewModel
                           {
                               DonorCustomer = dc,
                               Address = a,
                               PickupOrDelivery = p
                           }).ToList().OrderBy(p => p.PickupOrDelivery.PickupDateTime);
            return results;
        }
        internal IList<int> MiniCalendarCountQuery(ApplicationDbContext context, string date)
        {
            var blackoutDay = query.GetBlackoutDay(context, date);
            
            var results = (from p in context.PickupsDeliveries
                           where p.PickupDateTime.ToShortDateString() == date
                           where p.Cancelled == false
                           select p.ID).ToList();
            return results;
        }
    }
}
