using System;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mvc
{
    public class CommandResultUnknownException : SilentRedException
    {
        public CommandResultUnknownException(Type resultType, bool success)
            : base($"Command {(success ? "succeeded" : "failed")}, but its result {resultType} is unknown.")
        {
            ResultType = resultType;
            Success = success;
        }

        public Type ResultType { get; }
        public bool Success { get; }
    }
}