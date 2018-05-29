using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.PickupDeliveryViewModels
{
    public class ItemQuantityList
    {
        public int CategoryID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer.")]
        public int Quantity { get; set; }

    }
}
