using System;

namespace SilentRed.Infrastructure
{
    public abstract class QueryBusException : Exception
    {
        protected QueryBusException() { }
        protected QueryBusException(string query) : base(query) { }
    }
}
