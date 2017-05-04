// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.FluentValidation
{
    public abstract class BaseFluentQueryValidator<TQuery, TResult> : AbstractValidator<FluentQueryContext<TQuery>>,
                                                                      IQueryValidator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IEnumerable<Error>> IQueryValidator<TQuery, TResult>.ValidateAsync(
            TQuery query,
            IDictionary<string, object> headers,
            CancellationToken cancellation)
        {
            return ValidateAsync(new FluentQueryContext<TQuery>(query, headers), cancellation).ToErrors();
        }
    }
}
