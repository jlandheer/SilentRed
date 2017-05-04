using System;

namespace SilentRed.Infrastructure.Core
{
    public class NoQueryHandlerFoundException : SilentRedException
    {
        public NoQueryHandlerFoundException(Type queryType, Exception inner = null)
            : base($"No QueryHandlerFound for Query {queryType}", inner) { }
    }
}
