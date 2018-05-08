using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class QuantityList
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }

    }
}
