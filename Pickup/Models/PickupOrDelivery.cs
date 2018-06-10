using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class PickupOrDelivery
    {
        public int ID { get; set; }
        public DateTime ScheduleDateTime { get; set; }

        public DateTime PickupDateTime { get; set; }

        public bool CallEnRoute { get; set; }
        public string SpecialInstructions { get; set; }

        public bool Delivery { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public bool Cancelled { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public IList<ItemsAndPickupOrDelivery> FurnitureDonationPickups { get; set; }
    }
}
