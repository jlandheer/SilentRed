using System;

namespace SilentRed.WebCore.Customers
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(Guid customerId) : base(customerId.ToString()) { }
    }
}
