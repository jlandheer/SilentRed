using System;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.AspNet
{
    public class IllegalCommandResultException : SilentRedException
    {
        public IllegalCommandResultException(Type commandResultType) : base($"Cannot process CommandResult of type {commandResultType}")
        { }
    }
}