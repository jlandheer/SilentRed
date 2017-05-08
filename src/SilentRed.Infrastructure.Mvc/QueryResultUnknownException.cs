using System;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mvc
{
    public class QueryResultUnknownException : SilentRedException
    {
        public QueryResultUnknownException(Type resultType, bool success)
            : base($"Query {(success ? "succeeded" : "failed")}, but its result {resultType} is unknown.")
        {
            ResultType = resultType;
            Success = success;
        }

        public Type ResultType { get; }
        public bool Success { get; }
    }
}