using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.FluentValidation
{
    public class FluentCommandContext<TCommand>
    {
        public TCommand Command { get; }
        public Headers Headers { get; }

        public FluentCommandContext(TCommand command, Headers headers)
        {
            Command = command;
            Headers = headers;
        }
    }
}
