using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ScheduleViewModels
{
    public class CalendarViewModel
    {
        public int Week { get; set; }
        public DonorCustomer DonorCustomer { get; set; }
        public Address Address { get; set; }
        public PickupOrDelivery PickupOrDelivery { get; set; }
       
    }

    }

