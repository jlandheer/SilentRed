﻿using System;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers
{
    public class GetCustomerDetails : IQuery<GetCustomerDetails.Result>
    {
        public Guid CustomerId { get; set; }

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
