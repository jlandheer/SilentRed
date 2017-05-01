namespace SilentRed.Infrastructure
{
    public interface ICommandDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand { }
}
