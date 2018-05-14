using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ScheduleViewModels
{
    public class WeeklyCalendarViewModel
    {
        public int Week { get; set; }
        public int PickupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime PickupTime { get; set; }
    }

    }

