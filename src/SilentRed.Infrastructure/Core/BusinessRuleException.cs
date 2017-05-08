using System;
using System.Collections.Generic;

namespace SilentRed.Infrastructure.Core
{
    public class BusinessRuleException : SilentRedException
    {
        public BusinessRuleException(Type objectType, IEnumerable<Error> errors)
        {
            ObjectType = objectType;
            Errors = errors;
        }

        public Type ObjectType { get; }
        public IEnumerable<Error> Errors { get; }
    }
}

