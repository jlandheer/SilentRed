// ReSharper disable UnusedTypeParameter

namespace SilentRed.Infrastructure.Query
{
    public interface IQuery { }

    public interface IQuery<out TResult> : IQuery { }
}
