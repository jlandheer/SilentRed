// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace SilentRed.Infrastructure.Command
{
    public abstract class CommandResult
    {
        public static readonly CommandResult Succeeded = new SuccessCommandResult();
        public static readonly Task<CommandResult> SucceededTask = Task.FromResult(Succeeded);

        public static CommandResult Failed(string at, IEnumerable<Error> errors)
            => new FailedCommandResult(at, errors);

        public static Task<CommandResult> FailedTask(string at, IEnumerable<Error> errors)
            => Task.FromResult(Failed(at, errors));

        protected abstract IEnumerable<Error> Errors { get; }
        public bool Fail => !Success;

        public bool Success { get; }

        protected CommandResult(bool success)
        {
            Success = success;
        }
    }

    public class SuccessCommandResult : CommandResult
    {
        protected sealed override IEnumerable<Error> Errors => new List<Error>();
        public SuccessCommandResult() : base(true) { }
    }

    public class FailedCommandResult : CommandResult
    {
        public string At { get; }
        protected sealed override IEnumerable<Error> Errors { get; }

        public FailedCommandResult(string at, IEnumerable<Error> errors) : base(false)
        {
            At = at;

            Errors = new ReadOnlyCollection<Error>(
                (errors ?? new List<Error>())
                .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                .Select(i => new Error(i.SelectMany(e => e.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                .Where(i => i.Messages.Any())
                .ToList());

            if (!Errors.Any())
                throw new InvalidOperationException("Need at least one error.");
        }
    }
}
