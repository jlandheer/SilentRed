// ReSharper disable UnusedMember.Global
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace SilentRed.Infrastructure.FluentValidation
{
    public abstract class BaseFluentQueryValidator<TQuery, TResult> : AbstractValidator<FluentQueryContext<TQuery>>, IQueryValidator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<QueryResult<TResult>> IQueryValidator<TQuery, TResult>.ValidateAsync(
            TQuery query,
            IDictionary<string, object> headers,
            CancellationToken cancellation)
        {
            return ValidateAsync(new FluentQueryContext<TQuery>(query, headers), cancellation).ToQueryResult<TResult>();
        }
    }
}
