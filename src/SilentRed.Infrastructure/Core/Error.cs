using System;
using System.Collections.Generic;
using System.Linq;

namespace SilentRed.Infrastructure.Core
{
    public class Error
    {
        public override string ToString()
        {
            return string.Join(", ", Messages);
        }

        public object AttemptedValue { get; }

        public IEnumerable<string> Messages => _messages;
        public string PropertyName { get; }
        public static readonly IEnumerable<Error> NoErrors = new List<Error>();

        public Error(string errorMessage)
            : this(new[] { errorMessage }, "", null) { }

        public Error(string errorMessage, string propertyName)
            : this(new[] { errorMessage }, propertyName, null) { }

        public Error(string errorMessage, string propertyName, object attemptedValue)
            : this(new[] { errorMessage }, propertyName, attemptedValue) { }

        public Error(IEnumerable<string> errorMessages, string propertyName, object attemptedValue)
        {
            var messages = (errorMessages ?? throw new ArgumentNullException(nameof(errorMessages)))
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .ToList();

            if (!messages.Any())
            {
                throw new InvalidOperationException("Need at least one errormessage");
            }

            _messages = messages;
            PropertyName = propertyName ?? "";
            AttemptedValue = attemptedValue;
        }

        internal void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            _messages.Add(message);
        }

        internal void AddMessages(IEnumerable<string> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));

            _messages.AddRange(messages);
        }

        private readonly List<string> _messages;
    }
}
