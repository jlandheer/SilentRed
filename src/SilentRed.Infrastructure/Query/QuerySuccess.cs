using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Query
{
    public static class QuerySuccess
    {
        public static QueryResult<TResult> New<TResult>(TResult result)
        {
            return new QuerySuccess<TResult>(result);
        }

        public static Task<QueryResult<TResult>> NewTask<TResult>(TResult result)
        {
            return Task.FromResult(New(result));
        }
    }

    public class QuerySuccess<TResult> : QueryResult<TResult>
    {
        public TResult Value { get; }

        internal QuerySuccess(TResult result) : base(true)
        {
            Value = result;
        }
    }
}