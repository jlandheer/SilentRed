using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandFailed : CommandResult
    {
        public Type CommandType { get; }
        public IEnumerable<Error> Errors { get; }

        protected CommandFailed(ICommand command, IEnumerable<Error> errors) : base(false)
        {
            CommandType = command.GetType();

            Errors = new ReadOnlyCollection<Error>(
                (errors ?? new List<Error>())
                .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                .Select(i => new Error(i.SelectMany(e => e.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                .Where(i => i.Messages.Any())
                .ToList());

            if (!Errors.Any())
            {
                throw new InvalidOperationException("Need at least one error.");
            }
        }
    }
}