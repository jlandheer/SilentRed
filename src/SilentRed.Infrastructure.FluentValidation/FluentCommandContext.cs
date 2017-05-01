using System.Collections.Generic;

namespace SilentRed.Infrastructure.FluentValidation
{
    public class FluentCommandContext<TCommand>
    {
        public TCommand Command { get; }
        public IDictionary<string, object> Headers { get; }

        public FluentCommandContext(TCommand command, IDictionary<string, object> headers)
        {
            Command = command;
            Headers = headers;
        }
    }
}