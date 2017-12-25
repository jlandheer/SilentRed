using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryValidation<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public async Task<TResult> Handle(
            TQuery query,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var results = await _validators
                .Select(v => v.ValidateAsync(query, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                throw new ValidationException(query.GetType(), results);
            }

            return await _next.Handle(query, headers, cancellationToken);
        }

        public QueryValidation(IQueryHandler<TQuery, TResult> next, IQueryValidator<TQuery, TResult>[] validators)
        {
            _next = next;
            _validators = validators;
        }

        private readonly IQueryHandler<TQuery, TResult> _next;
        private readonly IQueryValidator<TQuery, TResult>[] _validators;
    }
}
