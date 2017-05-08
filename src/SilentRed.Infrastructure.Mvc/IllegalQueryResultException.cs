using System;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mvc
{
    public class IllegalQueryResultException : SilentRedException
    {
        public IllegalQueryResultException(Type queryResultType) : base($"Cannot process QueryResult of type {queryResultType}")
        { }
    }
}