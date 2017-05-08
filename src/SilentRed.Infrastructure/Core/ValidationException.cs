using System;
using System.Collections.Generic;

namespace SilentRed.Infrastructure.Core
{
    public class ValidationException : SilentRedException
    {
        public ValidationException(Type objectType, IEnumerable<Error> errors)
        {
            ObjectType = objectType;
            Errors = errors;
        }

        public Type ObjectType { get; }
        public IEnumerable<Error> Errors { get; }
    }
}

