using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Core
{
    public class ValidationException : SilentRedException
    {
        public CommandResult Result { get; set; }

        public ValidationException(CommandResult result)
        {
            Result = result;
        }
    }
}
