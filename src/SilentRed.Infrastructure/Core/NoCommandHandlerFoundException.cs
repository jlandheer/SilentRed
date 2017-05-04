using System;

namespace SilentRed.Infrastructure.Core
{
    public class NoCommandHandlerFoundException : SilentRedException
    {
        public NoCommandHandlerFoundException(Type commandType, Exception inner = null)
            : base($"No CommandHandlerFound for Command {commandType}", inner) { }
    }
}
