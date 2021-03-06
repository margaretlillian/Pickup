﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Neighborhood { get; set; }
        public string BottomFloor { get; set; }

        public int DonorCustomerID { get; set; }
        public DonorCustomer DonorCustomer { get; set; }

        public IList<Address> Addresses { get; set; }
    }
}
