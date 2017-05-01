using System;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers.Queries
{
    public class GetCustomerDetails : IQuery<GetCustomerDetails.Result>
    {
        public Guid CustomerId { get; set; }

        public class Result
        {
            public Guid Id { get; set; }

            public string Email { get; set; }
            public string State { get; set; }
            public string Gender { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
        }
    }
}
