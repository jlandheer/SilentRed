using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Query;

namespace Studiekring.Business.Customers.Queries
{
    public class CustomerDetailsHandler :
        IQueryHandler<GetCustomerDetails, GetCustomerDetails.Result>
    {
        public async Task<GetCustomerDetails.Result> Handle(
            GetCustomerDetails query,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var current = await _repository.Get(query.CustomerId);

            return  new GetCustomerDetails.Result
                    {
                        Id = current.Id,
                        Email = current.Email,
                        State = current.State.ToString(),
                        Name = current.Name,
                        Gender = current.Gender,
                        Phone = current.Phone
                    };
        }

        public CustomerDetailsHandler(CustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly CustomerRepository _repository;
    }
}
