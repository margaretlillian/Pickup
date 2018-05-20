using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class CustomerViewModel


    {
        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number *")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Second Phone Number")]
        public string PhoneNumberTwo { get; set; }

        [Display(Name = "FOT?")]
        public bool FOT { get; set; }
    }
}
