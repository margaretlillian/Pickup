using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pickup.Data;

namespace Pickup.Models.QueryClasses
{
    public class GetByIDQuery
    {
        public DonorCustomer GetCustomer(ApplicationDbContext context, int id)
        {
            return context.DonorsCustomers.Where(d => d.ID == id).FirstOrDefault();
        }


    }
}
