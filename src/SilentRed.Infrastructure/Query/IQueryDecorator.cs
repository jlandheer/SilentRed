namespace SilentRed.Infrastructure
{
    public interface IQueryDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> { }
}
