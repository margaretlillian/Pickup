using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.SearchViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int CustomerID { get; set; }

        public List<CustomerSearchResults> SearchResults { get; set; }
    }
}
