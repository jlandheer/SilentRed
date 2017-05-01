namespace SilentRed.Infrastructure
{
    public interface IQuery { }

    public interface IQuery<out TResult> : IQuery { }
}
