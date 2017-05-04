using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Core
{
    public class BusinessRulesValidationException : SilentRedException
    {
        public CommandResult Result { get; set; }

        public BusinessRulesValidationException(CommandResult result)
        {
            Result = result;
        }
    }
}
