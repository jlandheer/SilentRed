namespace SilentRed.Infrastructure.Command
{
    public abstract class CommandResult
    {
        public bool Success { get; }

        protected CommandResult(bool success)
        {
            Success = success;
        }
    }
}
