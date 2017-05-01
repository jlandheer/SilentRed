using System;

namespace SilentRed.Infrastructure
{
    public class NoQueryHandlerFoundException : Exception
    {
        public NoQueryHandlerFoundException(Type queryType, Exception inner)
            : base($"No QueryHandlerFound for Query {queryType}", inner) { }

        public NoQueryHandlerFoundException(Type queryType) : this(queryType, null) { }
    }
}
