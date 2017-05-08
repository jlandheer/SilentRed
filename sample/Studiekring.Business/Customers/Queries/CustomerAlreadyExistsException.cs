using System;

namespace Studiekring.Business.Customers.Queries
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(Guid customerId) : base(customerId.ToString()) { }
    }
}
