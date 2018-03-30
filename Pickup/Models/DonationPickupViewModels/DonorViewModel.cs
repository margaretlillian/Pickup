using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class DonorViewModel


    {
       // [HiddenInput(DisplayValue = false)]
        //public int DonorId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone #")]
        public string PhoneNumber { get; set; }

        [Display(Name = "2nd Phone #")]
        public string PhoneNumberTwo { get; set; }
    }
}
