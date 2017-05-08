using System.Collections.Generic;
using SilentRed.Infrastructure.Query;

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
