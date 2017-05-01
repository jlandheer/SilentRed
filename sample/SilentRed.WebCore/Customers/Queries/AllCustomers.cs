using System;
using System.Collections.Generic;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers.Queries
{
    public class AllCustomers : IQuery<AllCustomers.Result>
    {
        public class Result
        {
            public List<Customer> Customers { get; set; }

            public class Customer
            {
                public string Email { get; set; }

                public Guid Id { get; set; }
                public string Status { get; set; }
            }
        }
    }
}