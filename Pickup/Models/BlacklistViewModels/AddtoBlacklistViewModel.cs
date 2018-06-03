using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.BlacklistViewModels
{
    public class AddtoBlacklistViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public DonorCustomer Customer { get; set; }

        [Required]
        [Display(Name = "Reason for Blacklisting")]
        public string Reason { get; set; }

    }
}
