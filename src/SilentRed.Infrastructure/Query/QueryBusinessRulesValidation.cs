using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public class QueryBusinessRulesValidation<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public async Task<QueryResult<TResult>> Handle(TQuery query, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            var tasks = _businessRules
                .Select(v => v.ValidateAsync(query, headers, cancellationToken));

            var results = await Task.WhenAll(tasks);
            var failures = results.SelectMany(result => result.Errors)
                                  .Where(f => f != null)
                                  .ToList();

            if (failures.Any())
            {
                return
                    QueryResult.Failed<TResult>(failures);
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
