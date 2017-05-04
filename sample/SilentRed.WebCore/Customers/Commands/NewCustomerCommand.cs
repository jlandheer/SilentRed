using System;
using SilentRed.Infrastructure;

namespace SilentRed.WebCore.Customers
{
    public class NewCustomerCommand : ICommand
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
