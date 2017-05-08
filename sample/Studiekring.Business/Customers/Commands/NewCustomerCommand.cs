using System;
using SilentRed.Infrastructure.Command;
using Studiekring.Business.Customers.Models;

namespace Studiekring.Business.Customers.Commands
{
    public class NewCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public CustomerState State { get; set; }
    }
}
