using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryValidationFailed<TResult> : QueryFailed<TResult>
    {
        internal QueryValidationFailed(Type queryType, IEnumerable<Error> errors) : base(queryType, errors) { }
    }
}