using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ViewViewModel
{
    public class ViewInformationViewModel
    {
        public int PickupID { get; set; }
        //Donor Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberTwo { get; set; }

        //Address Information
        public string StreetAddress { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Neighborhood { get; set; }

        //Pickup/Delivery Information
        public bool Delivery { get; set; }

        public DateTime PickupDateTime { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public string Scheduler { get; set; }
        public bool CallEnRoute { get; set; }
        public string SpecialInstructions { get; set; }
    }
}
