using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using Studiekring.Business.Customers.Models;
using Studiekring.Business.Customers.Queries;

namespace Studiekring.Business.Customers.Commands
{
    public class NewCustomerHandler : ICommandHandler<NewCustomerCommand>
    {
        public async Task<CommandResult> Handle(
            NewCustomerCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var customer = new Customer(Guid.NewGuid());
            customer.Name=command.;

            await _repository.Add(customer);

            return CommandResult.Succeeded;
        }

        public NewCustomerHandler(CustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly CustomerRepository _repository;
    }
}
