using System;
using System.Collections.Generic;
using System.Linq;

namespace SilentRed.Infrastructure.Core
{
    public class PropertyError
    {
        private PropertyError()
        { }

        public PropertyError(string propertyName, string error)
          : this(propertyName, error, null)
        { }

        public PropertyError(string propertyName, string error, object attemptedValue)
        {
            this.PropertyName = propertyName;
            this.ErrorMessages = new List<string> { error };
            this.AttemptedValue = attemptedValue;
        }

        public string PropertyName { get; }

        public object AttemptedValue { get; }

        public IList<string> ErrorMessages { get; }

        public virtual bool IsValid => this.ErrorMessages.Count == 0;
    }

    public class ErrorCollection
    {
        public virtual bool IsValid => this.Errors.Count == 0;

        public IList<PropertyError> Errors { get; }

        public ErrorCollection()
        {
            this.Errors = new List<PropertyError>();
        }

        public ErrorCollection(IEnumerable<PropertyError> failures)
        {
            this.Errors = failures.Where(failure => failure != null && failure.IsValid).ToList();
        }
    }

    public class Error
    {
        public static readonly IEnumerable<Error> NoErrors = new Error[0];

        public override string ToString() => string.Join(", ", Messages);

        public string PropertyName { get; }
        public object AttemptedValue { get; }

        public IEnumerable<string> Messages => _messages;

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
