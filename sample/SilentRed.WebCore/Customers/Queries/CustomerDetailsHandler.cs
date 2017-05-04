using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers
{
    public class CustomerDetailsHandler :
        IQueryHandler<GetCustomerDetails, GetCustomerDetails.Result>
    {
        public async Task<QueryResult<GetCustomerDetails.Result>> Handle(
            GetCustomerDetails query,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var current = await _repository.Get(query.CustomerId);

            return
                QueryResult.Succeeded(
                    new GetCustomerDetails.Result
                    {
                        Id = current.Id,
                        Email = current.Email,
                        State = current.State.ToString(),
                        Name = current.Name,
                        Gender = current.Gender,
                        Phone = current.Phone
                    });
        }

        public CustomerDetailsHandler(CustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly CustomerRepository _repository;
    }
}
