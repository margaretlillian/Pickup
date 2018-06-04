using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pickup.Data;
using Pickup.Models.BlacklistViewModels;
using Pickup.Models.SearchViewModels;

namespace Pickup.Models.QueryClasses
{
    internal class SearchQuery
    {
        internal List<CustomerSearchResults> NameSearch(ApplicationDbContext context, string firstName, string lastName)
        {
            CheckForExistingQuery query = new CheckForExistingQuery();
            IList<DonorCustomer> donorCustomers = context.DonorsCustomers
                 .Where(d => d.FirstName == firstName || d.LastName == lastName).ToList();
            List<CustomerSearchResults> searchResults = new List<CustomerSearchResults>();
            foreach (var person in donorCustomers)
            {
                if (query.GetBlacklistedCustomerById(context, person.ID) != null)
                {
                    searchResults.Add(new CustomerSearchResults
                    {
                        DonorCustomer = person,
                        Addresses = null,
                        IsBlacklisted = true
                    });
                }
                else
                {
                    searchResults.Add(new CustomerSearchResults
                    {
                        DonorCustomer = person,
                        Addresses = context.Addresses.Where(a => a.DonorCustomerID == person.ID).ToList()
                    });
                }
            }
            return searchResults;
        }

        internal IList<ViewBlacklistedViewModel> SearchAddToBlacklist(ApplicationDbContext context, string firstName, string lastName)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName && dc.LastName == lastName
                    select new ViewBlacklistedViewModel
                    {
                        DonorCustomer = dc,
                        Address = context.Addresses.Where(a => a.DonorCustomerID == dc.ID).FirstOrDefault()
                    }).ToList();
        }

        internal DonorCustomer SpecificCustomerSearch(ApplicationDbContext context, string firstName, string lastName, string phone)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName && dc.LastName == lastName && dc.PhoneNumber == phone
                    select dc).FirstOrDefault();
        }

        internal Address SpecificAddressSearch(ApplicationDbContext context, string street, string apartment, string city, string zip)
        {
            return (from a in context.Addresses
                    where a.Street == street && a.Apartment == apartment && a.City == city && a.ZIP == zip
                    select a).FirstOrDefault();
        }

    }
}
