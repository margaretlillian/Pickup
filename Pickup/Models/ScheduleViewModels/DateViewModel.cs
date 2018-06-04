using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ScheduleViewModels
{
    public class DateViewModel
    {
        public DateTime Date { get; set; }
        public bool IsBlackedOut { get; set; }
    }
}
