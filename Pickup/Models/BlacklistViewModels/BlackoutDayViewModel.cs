using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.BlacklistViewModels
{
    public class BlackoutDayViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime BlackoutDate { get; set; }
    }
}
