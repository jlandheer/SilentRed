using System;

namespace SilentRed.Infrastructure
{
    public class NoCommandHandlerFoundException : Exception
    {
        public NoCommandHandlerFoundException(Type commandType, Exception inner)
            : base($"No CommandHandlerFound for Command {commandType}", inner) { }

        public NoCommandHandlerFoundException(Type commandType) : this(commandType, null) { }
    }
}
