using System;
using System.Reflection;
using MediatR;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mediatr
{
    public static class MediatorCommandWrapper
    {
        public static bool IsWrappedMediatorCommandHandler(this Type serviceType)
        {
            if (!serviceType.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            var firstArgumentType = serviceType.GenericTypeArguments[0];
            if (!firstArgumentType.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            var wrappedType =
                typeof(CommandWrappedForMediator<>).MakeGenericType(
                    firstArgumentType.GenericTypeArguments[0]);

            return firstArgumentType == wrappedType;
        }
    }

    public class CommandWrappedForMediator<TCommand> : IRequest
        where TCommand : ICommand
    {
        public TCommand Command { get; }
        public Headers Headers { get; }

        internal CommandWrappedForMediator(TCommand command, Headers headers)
        {
            Headers = headers;
            Command = command;
        }
    }
}
