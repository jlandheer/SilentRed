namespace SilentRed.Infrastructure.Query
{
    public interface IQueryDecorator<in TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> { }
}
