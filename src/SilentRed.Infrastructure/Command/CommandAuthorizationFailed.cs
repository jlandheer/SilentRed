using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandAuthorizationFailed : CommandFailed
    {
        internal CommandAuthorizationFailed(ICommand command, IEnumerable<Error> errors) : base(command, errors) { }
    }
}