using System.Collections.Generic;

namespace SilentRed.Infrastructure.FluentValidation
{
    public class FluentQueryContext<TQuery>
        where TQuery : IQuery
    {
        public IDictionary<string, object> Headers { get; }
        public TQuery Query { get; }

        public FluentQueryContext(TQuery query, IDictionary<string, object> headers)
        {
            Query = query;
            Headers = headers;
        }
    }
}
