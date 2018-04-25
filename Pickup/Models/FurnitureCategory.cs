using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class FurnitureCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<Furniture> Furniture { get; set; }
    }
}
