using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandBusinessRulesViolationResult : CommandFailed
    {
        internal CommandBusinessRulesViolationResult(ICommand command, IEnumerable<Error> errors) : base(command, errors) { }
    }
}