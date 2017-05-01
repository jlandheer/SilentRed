using System;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers.Commands
{
    public class NewCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
