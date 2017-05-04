using System;

namespace SilentRed.WebCore.Customers
{
    public class CustomerDoesNotExistException : Exception
    {
        public CustomerDoesNotExistException(Guid customerId)
            : base(customerId.ToString()) { }
    }
}
