// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.FluentValidation
{
    public abstract class BaseFluentQueryBusinessRuleValidator<TQuery, TResult> :
        AbstractValidator<FluentQueryContext<TQuery>>,
        IQueryBusinessRuleValidator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IEnumerable<Error>> IQueryBusinessRuleValidator<TQuery, TResult>.ValidateAsync(
            TQuery query,
            IDictionary<string, object> headers,
            CancellationToken cancellation)
        {
            return ValidateAsync(new FluentQueryContext<TQuery>(query, headers), cancellation).ToErrors();
        }
    }
}
