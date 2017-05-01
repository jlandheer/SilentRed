using System;

namespace SilentRed.WebCore.Customers.Queries {
    public class CustomerAlreadyExistsExeption : Exception
    {
        public CustomerAlreadyExistsExeption(Guid customerId) : base(customerId.ToString())
        {
        }
    }
}