using System;
using SilentRed.WebCore.Customers.Commands;

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

        public Guid Id { get; private set; }

        public CustomerState State { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
    }
}
