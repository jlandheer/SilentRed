using System.Collections.Generic;

namespace SilentRed.Infrastructure.FluentValidation
{
    public class FluentQueryContext<TQuery> where TQuery: IQuery
    {
        public TQuery Query { get; }
        public IDictionary<string, object> Headers { get; }

        public FluentQueryContext(TQuery query, IDictionary<string, object> headers)
        {
            Query = query;
            Headers = headers;
        }
    }
}