namespace SilentRed.Infrastructure.Query
{
    public interface IQueryDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> { }
}
