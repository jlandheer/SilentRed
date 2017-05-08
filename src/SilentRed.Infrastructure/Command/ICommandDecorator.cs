namespace SilentRed.Infrastructure.Command
{
    public interface ICommandDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand { }
}
