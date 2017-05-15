using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Command
{
    public class CommandSuccess : CommandResult
    {
        public static CommandResult New() => new CommandSuccess();
        public static Task<CommandResult> NewTask() => Task.FromResult(New());

        protected CommandSuccess() : base(true) { }
    }
}