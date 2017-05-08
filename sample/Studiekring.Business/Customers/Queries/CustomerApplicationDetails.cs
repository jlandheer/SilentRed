using System;
using SilentRed.Infrastructure.Query;

namespace Studiekring.Business.Customers.Queries
{
    public class GetCustomerDetails : IQuery<GetCustomerDetails.Result>
    {
        public GetCustomerDetails(Guid customerId)
        {
            CustomerId = customerId;
        }
        public Guid CustomerId { get; }

        public class Result
        {
            public string Email { get; set; }
            public string Gender { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string State { get; set; }
        }
    }
}
