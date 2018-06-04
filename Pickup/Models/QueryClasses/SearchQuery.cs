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
        internal IList<CustomerSearchResults> NameSearch(ApplicationDbContext context, string firstName, string lastName)
        {
            return (from dc in context.DonorsCustomers
                    where dc.FirstName == firstName || dc.LastName == lastName
                                                 select new CustomerSearchResults
                                                 {
                                                     DonorCustomer = dc,
                                                     Addresses = context.Addresses.Where(a => a.DonorCustomerID == dc.ID).ToList(),
                                                     IsBlacklisted = context.BlacklistedDonors.Any(b => b.DonorCustomerID == dc.ID)
                                                 }).ToList();
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
