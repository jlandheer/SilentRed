using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Studiekring.Business.Customers.Models;

namespace Studiekring.Business.Customers.Queries
{
    public class CustomerRepository
    {
        public Task Add(Customer customer)
        {
            if (ExistsInternal(customer.Id))
            {
                throw new CustomerAlreadyExistsException(customer.Id);
            }

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
            {
                throw new CustomerDoesNotExistException(customerId);
            }

            return Task.FromResult(customer);
        }

        private readonly List<Customer> _customers = new List<Customer>
                                                     {
                                                         new Customer(Guid.NewGuid())
                                                         {
                                                             Name = "John Karels",
                                                             Email = "john@karels.test",
                                                             Gender = "M",
                                                             Phone = "06-11111111",
                                                             State = CustomerState.New
                                                         },
                                                         new Customer(Guid.NewGuid())
                                                         {
                                                             Name = "Jan Hoefslag",
                                                             Email = "janh333@hotmail.test",
                                                             Gender = "M",
                                                             Phone = null,
                                                             State =CustomerState.New
                                                         },
                                                         new Customer(Guid.NewGuid())
                                                         {
                                                             Name = "Wilma Uting",
                                                             Email = null,
                                                             Gender = "V",
                                                             Phone = "088-12312314",
                                                             State =CustomerState.New
                                                         },
                                                         new Customer(Guid.NewGuid())
                                                         {
                                                             Name = "Hans Freerik",
                                                             Email = "hans@gmail.test",
                                                             Gender = "M",
                                                             Phone = "06-78743242",
                                                             State =CustomerState.New
                                                         },
                                                         new Customer(Guid.NewGuid())
                                                         {
                                                             Name = "Paula Wilmink",
                                                             Email = "p.wilm@ttt.test",
                                                             Gender = "V",
                                                             Phone = "+328919233332",
                                                             State =CustomerState.New
                                                         }
                                                     };

        private bool ExistsInternal(Guid customerId)
        {
            return _customers.Any(i => i.Id == customerId);
        }
    }
}
