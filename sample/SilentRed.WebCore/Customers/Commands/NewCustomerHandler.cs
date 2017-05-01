using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure;
using SilentRed.WebCore.Customers.Models;
using SilentRed.WebCore.Customers.Queries;

namespace SilentRed.WebCore.Customers.Commands
{
    public class NewCustomerHandler : ICommandHandler<NewCustomerCommand>
    {
        public async Task<CommandResult> Handle(
            NewCustomerCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var application = new Customer();
            application.NewCustomer(command);

            await _repository.Add(application);

            return CommandResult.Succeeded;
        }

        public NewCustomerHandler(CustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly CustomerRepository _repository;
    }
}
