using System;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mvc
{
    public class UnknownQueryResultLocationException : SilentRedException
    {
        public UnknownQueryResultLocationException(string at, Type queryType) : base($"Query {queryType} failed at {at}, but {at} is unhandled.")
        { }
    }
}