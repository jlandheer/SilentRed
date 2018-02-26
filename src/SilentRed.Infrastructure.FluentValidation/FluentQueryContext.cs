using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.FluentValidation
{
    public class FluentQueryContext<TQuery>
        where TQuery : IQuery
    {
        public Headers Headers { get; }
        public TQuery Query { get; }

        public FluentQueryContext(TQuery query, Headers headers)
        {
            Query = query;
            Headers = headers;
        }
    }
}
