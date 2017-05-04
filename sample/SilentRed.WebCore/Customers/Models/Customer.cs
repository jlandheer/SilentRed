// ReSharper disable UnusedAutoPropertyAccessor.Local

using System;

namespace SilentRed.WebCore.Customers.Models
{
    public class Customer
    {
        public void NewCustomer(NewCustomerCommand command)
        {
            Id = command.Id;
            Email = command.Email;
            State = CustomerState.New;
        }

        public string Email { get; private set; }
        public string Gender { get; private set; }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }

        public CustomerState State { get; private set; }
    }
}
