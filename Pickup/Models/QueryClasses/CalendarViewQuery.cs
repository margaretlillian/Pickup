using Pickup.Data;
using Pickup.Models.ScheduleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.QueryClasses
{
    public class CalendarViewQuery
    {
        
        public IList<WeeklyCalendarViewModel> CreateWeeklyQuery(ApplicationDbContext context, string date)
        {

            var results = (from p in context.PickupsDeliveries
                           where p.PickupDateTime.ToShortDateString() == date
                           join a in context.Addresses on p.AddressID equals a.ID
                           join dc in context.DonorsCustomers on a.DonorCustomerID equals dc.ID
                           select new WeeklyCalendarViewModel
                           {
                               City = a.City,
                               FirstName = dc.FirstName,
                               LastName = dc.LastName,
                               PickupID = p.ID,
                               Phone = dc.PhoneNumber,
                               PickupTime = p.PickupDateTime,
                               Cancelled = p.Cancelled,
                               Delivery = p.Delivery
                           }).ToList();
            return results;
        }
        public IList<MonthlyMiniCalendarViewModel> CreateMonthlyQuery(ApplicationDbContext context, string date) {
            var results = (from p in context.PickupsDeliveries
                           where p.PickupDateTime.ToShortDateString() == date
                           select new MonthlyMiniCalendarViewModel
                           {
                               PickupID = p.ID
                           }).ToList();
            return results;
        }
    }
}
