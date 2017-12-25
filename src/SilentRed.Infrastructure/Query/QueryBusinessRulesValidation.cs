using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryBusinessRulesValidation<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public async Task<TResult> Handle(
            TQuery query,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var results = await _businessRules
                .Select(v => v.ValidateAsync(query, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                throw new BusinessRuleException(query.GetType(), results);
            }

            return await _next.Handle(query, headers, cancellationToken);
        }

        public QueryBusinessRulesValidation(
            IQueryHandler<TQuery, TResult> next,
            IQueryBusinessRuleValidator<TQuery, TResult>[] businessRules)
        {
            _next = next;
            _businessRules = businessRules;
        }

        private readonly IQueryHandler<TQuery, TResult> _next;
        private readonly IQueryBusinessRuleValidator<TQuery, TResult>[] _businessRules;
    }
}
