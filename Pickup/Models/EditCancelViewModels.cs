using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class EditCancelViewModels
    {
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public int PickupID { get; set; }
        public bool Delivery { get; set; }
    }
}
