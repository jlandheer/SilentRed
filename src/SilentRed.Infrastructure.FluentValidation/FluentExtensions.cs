using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace SilentRed.Infrastructure.FluentValidation
{
    public static class FluentExtensions
    {
        public static async Task<CommandResult> ToCommandResult(this Task<ValidationResult> fluentValidationResultTask)
        {
            var result = await fluentValidationResultTask;
            if (result.IsValid)
            {
                return CommandResult.Succeeded;
            }

            return CommandResult.Failed(result.Errors.Select(i => new Error(i.ErrorMessage, i.PropertyName, i.AttemptedValue)));
        }

        public static async Task<QueryResult<TResult>> ToQueryResult<TResult>(this Task<ValidationResult> fluentValidationResultTask)
        {
            var result = await fluentValidationResultTask;
            if (result.IsValid)
            {
                return QueryResult.Succeeded(default(TResult));
            }

            return QueryResult.Failed<TResult>(result.Errors.Select(i => new Error(i.ErrorMessage, i.PropertyName, i.AttemptedValue)));
        }
    }
}