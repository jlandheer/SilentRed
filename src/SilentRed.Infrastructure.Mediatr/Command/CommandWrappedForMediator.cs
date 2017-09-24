using System;
using System.Collections.Generic;
using System.Reflection;
using MediatR;
using SilentRed.Infrastructure.Command;

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
        public IDictionary<string, object> Headers { get; }

        internal CommandWrappedForMediator(TCommand command, IDictionary<string, object> headers)
        {
            Headers = headers;
            Command = command;
        }
    }
}
