using System;

namespace Studiekring.Business.Customers.Queries
{
    public class CustomerDoesNotExistException : Exception
    {
        public CustomerDoesNotExistException(Guid customerId)
            : base(customerId.ToString()) { }
    }
}
