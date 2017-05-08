using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandValidationFailed : CommandFailed
    {
        internal CommandValidationFailed(ICommand command, IEnumerable<Error> errors) : base(command, errors) { }
    }
}