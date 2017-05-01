using System;
using System.Collections.Generic;
using System.Linq;

namespace SilentRed.Infrastructure
{
    public class Error
    {
        public override string ToString()
        {
            return string.Join(", ", Messages);
        }

        public IEnumerable<string> Messages { get; }
        public string PropertyName { get; }
        public object AttemptedValue { get; }

        public Error(string errorMessage = null, string propertyName = null, object attemptedValue = null)
            : this(new[] { errorMessage }, propertyName, attemptedValue) { }

        public Error(IEnumerable<string> errorMessages = null, string propertyName = null, object attemptedValue = null)
        {
            errorMessages = errorMessages ?? new List<string>();
            if (!errorMessages.Any()) throw new InvalidOperationException("Need at least one errormessage");

            PropertyName = propertyName ?? "";
            Messages = errorMessages;
            AttemptedValue = attemptedValue;
        }
    }
}
