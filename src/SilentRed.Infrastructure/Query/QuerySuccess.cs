namespace SilentRed.Infrastructure.Query
{
    public class QuerySuccess<TResult> : QueryResult<TResult>
    {
        public TResult Value { get; }

        public QuerySuccess(TResult result) : base(true)
        {
            Value = result;
        }
    }
}