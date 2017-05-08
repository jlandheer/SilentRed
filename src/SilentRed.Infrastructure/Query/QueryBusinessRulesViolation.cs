using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryBusinessRulesViolation<TResult> : QueryFailed<TResult>
    {
        internal QueryBusinessRulesViolation(Type queryType, IEnumerable<Error> errors) : base(queryType, errors) { }
    }
}