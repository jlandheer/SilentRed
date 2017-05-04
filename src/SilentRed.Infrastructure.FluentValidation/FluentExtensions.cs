using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.FluentValidation
{
    public static class FluentExtensions
    {
        public static async Task<IEnumerable<Error>> ToErrors(this Task<ValidationResult> fluentValidationResultTask)
        {
            var result = await fluentValidationResultTask;

            return result.IsValid
                ? Error.NoErrors
                : result.Errors.Select(i => new Error(i.ErrorMessage, i.PropertyName, i.AttemptedValue));
        }
    }
}
