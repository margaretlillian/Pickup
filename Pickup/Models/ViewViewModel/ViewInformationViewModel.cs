using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ViewViewModel
{
    public class ViewInformationViewModel
    {
        public int PickupID { get; set; }
        public string FirstName { get; set; }
        public string StreetAddress { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Scheduler { get; set; }

    }
}
