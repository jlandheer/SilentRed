using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure;
using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace Studiekring.Business.Customers.Queries
{
    public class AllCustomersHandler :
        IQueryHandler<AllCustomers, AllCustomers.Result>
    {
        public Task<QueryResult<AllCustomers.Result>> Handle(
            AllCustomers query,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
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

            return new QuerySuccess<AllCustomers.Result>(new AllCustomers.Result { Customers = customers }).AsTask<QueryResult<AllCustomers.Result>>();
        }

        public AllCustomersHandler(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        private readonly CustomerRepository _customerRepository;
    }
}
