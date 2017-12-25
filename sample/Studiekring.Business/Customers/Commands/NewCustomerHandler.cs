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
        public async Task Handle(
            NewCustomerCommand command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var customer = new Customer(Guid.NewGuid());

            await _repository.Add(customer);
        }

        public NewCustomerHandler(CustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly CustomerRepository _repository;
    }
}
