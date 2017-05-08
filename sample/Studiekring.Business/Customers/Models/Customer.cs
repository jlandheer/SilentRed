// ReSharper disable UnusedAutoPropertyAccessor.Local

using System;

namespace Studiekring.Business.Customers.Models
{
    public class Customer
    {
        public Customer(Guid id)
        {
            Id = id;
        }

        public string Email { get; set; }
        public string Gender { get; set; }

        public Guid Id { get; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public CustomerState State { get; set; }
    }
}
