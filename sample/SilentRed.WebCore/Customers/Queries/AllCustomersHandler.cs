﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers.Queries
{
    public class AllCustomersHandler :
        IQueryHandler<AllCustomers, AllCustomers.Result>
    {
        private readonly CustomerRepository _customerRepository;

        public AllCustomersHandler(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<QueryResult<AllCustomers.Result>> Handle(AllCustomers query, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            var customers = _customerRepository
                .All()
                .Result
                .Select(
                    i => new AllCustomers.Result.Customer
                    {
                        Id = i.Id,
                        Email = i.Email,
                        Status = i.State.ToString()
                    })
                .ToList();

            return QueryResult.SucceededTask(new AllCustomers.Result { Customers = customers });
        }
    }
}