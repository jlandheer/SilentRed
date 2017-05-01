using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public class CommandResult
    {
        public static CommandResult Succeeded = new CommandResult();
        public static Task<CommandResult> SucceededTask = Task.FromResult(Succeeded);

        public static CommandResult Failed(string error) => new CommandResult(new[] { new Error(error) });
        public static CommandResult Failed(Error error) => new CommandResult(new[] { error });
        public static CommandResult Failed(IEnumerable<string> errors) => new CommandResult(errors.Select(i => new Error(i)));
        public static CommandResult Failed(IEnumerable<Error> errors) => new CommandResult(errors);

        public static Task<CommandResult> FailedTask(string error) => Task.FromResult(Failed(error));
        public static Task<CommandResult> FailedTask(Error error) => Task.FromResult(Failed(error));
        public static Task<CommandResult> FailedTask(IEnumerable<string> errors) => Task.FromResult(Failed(errors));
        public static Task<CommandResult> FailedTask(IEnumerable<Error> errors) => Task.FromResult(Failed(errors));

        public bool Success { get; }
        public bool Fail => !Success;
        public IEnumerable<Error> Errors { get; }

        private CommandResult()
        {
            Errors = new ReadOnlyCollection<Error>(new List<Error>());
            Success = true;
        }

        private CommandResult(IEnumerable<Error> errors)
        {
            Errors = new ReadOnlyCollection<Error>(
                (errors ?? new List<Error>())
                .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                .Select(i => new Error(i.SelectMany(e => e.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                .Where(i => i.Messages.Any())
                .ToList());

            if (!Errors.Any())
                throw new InvalidOperationException("Need at least one error.");

            Success = false;
        }
    }
}
