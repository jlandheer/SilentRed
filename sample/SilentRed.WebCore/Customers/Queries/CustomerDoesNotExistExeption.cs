using System;

namespace SilentRed.WebCore.Customers.Queries {
    public class CustomerDoesNotExistExeption : Exception
    {
        public CustomerDoesNotExistExeption(Guid customerId)
            : base(customerId.ToString()) { }
    }
}