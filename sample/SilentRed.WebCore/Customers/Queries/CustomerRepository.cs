using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SilentRed.WebCore.Customers.Models;

namespace SilentRed.WebCore.Customers.Queries
{
    public class CustomerRepository
    {
        public Task Add(Customer customer)
        {
            if (ExistsInternal(customer.Id))
                throw new CustomerAlreadyExistsExeption(customer.Id);

            _customers.Add(customer);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Customer>> All()
        {
            return Task.FromResult(_customers as IEnumerable<Customer>);
        }

        public Task<bool> Exists(Guid customerId)
        {
            return Task.FromResult(ExistsInternal(customerId));
        }

        public Task<Customer> Get(Guid customerId)
        {
            var customer = _customers.SingleOrDefault(i => i.Id == customerId);
            if (customer == null)
                throw new CustomerDoesNotExistExeption(customerId);

            return Task.FromResult(customer);
        }

        private readonly List<Customer> _customers = new List<Customer>();

        private bool ExistsInternal(Guid customerId)
        {
            return _customers.Any(i => i.Id == customerId);
        }
    }
}
