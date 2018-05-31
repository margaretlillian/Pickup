using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pickup.Data;
using Pickup.Models.SearchViewModels;

namespace Pickup.Models.QueryClasses
{
    public class SearchQuery
    {
        public IList<CustomerSearchResults> NameSearch(ApplicationDbContext context, string firstName, string lastName)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName || dc.LastName == lastName
                                                 select new CustomerSearchResults
                                                 {
                                                     DonorCustomer = dc,
                                                     Addresses = context.Addresses.Where(a => a.DonorCustomerID == dc.ID).ToList()
                                                 }).ToList();
        }

        public IList<CustomerSearchResults> FullNameSearch(ApplicationDbContext context, string firstName, string lastName)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName && dc.LastName == lastName
                    select new CustomerSearchResults
                    {
                        DonorCustomer = dc,
                        Addresses = context.Addresses.Where(a => a.DonorCustomerID == dc.ID).ToList()
                    }).ToList();
        }

        public DonorCustomer SpecificCustomerSearch(ApplicationDbContext context, string firstName, string lastName, string phone)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName && dc.LastName == lastName && dc.PhoneNumber == phone
                    select dc).FirstOrDefault();
        }

    }
}
