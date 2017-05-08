using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mvc
{
    public class FailedQueryResultUnknownException : SilentRedException
    {
        public FailedQueryResultUnknownException(Type resultType, Type objectType, IEnumerable<Error> errors)
            : base($"Query {objectType} failed, but its result {resultType} is unknown.")
        {
            ResultType = resultType;
            ObjectType = objectType;
            Errors = errors;
        }

        public Type ResultType { get; }
        public Type ObjectType { get; }
        public IEnumerable<Error> Errors { get; }
    }
}