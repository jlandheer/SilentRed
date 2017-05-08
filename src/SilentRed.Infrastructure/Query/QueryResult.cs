namespace SilentRed.Infrastructure.Query
{
    public abstract class QueryResult<TResult>
    {
        public bool Fail => !Success;
        public bool Success { get; }

        protected QueryResult(bool success)
        {
            Success = success;
        }
    }
}
