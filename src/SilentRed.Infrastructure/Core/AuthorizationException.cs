using System;
using System.Collections.Generic;

namespace SilentRed.Infrastructure.Core
{
    public class AuthorizationException : SilentRedException
    {
        public AuthorizationException(Type objectType, IEnumerable<Error> errors)
        {
            ObjectType = objectType;
            Errors = errors;
        }

        public Type ObjectType { get; }
        public IEnumerable<Error> Errors { get; }
    }
}

