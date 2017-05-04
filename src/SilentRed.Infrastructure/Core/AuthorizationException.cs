using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Core
{
    public class AuthorizationException : SilentRedException
    {
        public CommandResult Result { get; set; }

        public AuthorizationException(CommandResult result)
        {
            Result = result;
        }
    }
}
