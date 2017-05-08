using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryFailed<TResult> : QueryResult<TResult>
    {
        public Type QueryType { get; }
        public IEnumerable<Error> Errors { get; }

        protected QueryFailed(Type queryType, IEnumerable<Error> errors) : base(false)
        {
            QueryType = queryType;

            Errors = new ReadOnlyCollection<Error>((errors ?? new List<Error>()).Flatten());

            if (!Errors.Any())
            {
                throw new InvalidOperationException("Need at least one error.");
            }
        }
    }
}