namespace SilentRed.Infrastructure.Command
{
    public interface ICommandDecorator<in TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand { }
}
