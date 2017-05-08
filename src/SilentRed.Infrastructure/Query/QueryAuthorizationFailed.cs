using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryAuthorizationFailed<TResult> : QueryFailed<TResult>
    {
        internal QueryAuthorizationFailed(Type queryType, IEnumerable<Error> errors) : base(queryType, errors) { }
    }
}