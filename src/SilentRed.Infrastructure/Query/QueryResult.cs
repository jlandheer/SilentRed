using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public static class QueryResult
    {
        public static QueryResult<TResult> Succeeded<TResult>(TResult result) => new SuccessQueryResult<TResult>(result);
        public static Task<QueryResult<TResult>> SucceededTask<TResult>(TResult result) => Task.FromResult(Succeeded(result));

        public static QueryResult<TResult> Failed<TResult>(string error) => new FailedQueryResult<TResult>(new[] { new Error(error) });
        public static QueryResult<TResult> Failed<TResult>(Error error) => new FailedQueryResult<TResult>(new[] { error });
        public static QueryResult<TResult> Failed<TResult>(IEnumerable<string> errors) => new FailedQueryResult<TResult>(errors.Select(i => new Error(i)));
        public static QueryResult<TResult> Failed<TResult>(IEnumerable<Error> errors) => new FailedQueryResult<TResult>(errors);

        public static Task<QueryResult<TResult>> FailedTask<TResult>(string error) => Task.FromResult(Failed<TResult>(error));
        public static Task<QueryResult<TResult>> FailedTask<TResult>(Error error) => Task.FromResult(Failed<TResult>(error));
        public static Task<QueryResult<TResult>> FailedTask<TResult>(IEnumerable<string> errors) => Task.FromResult(Failed<TResult>(errors));
        public static Task<QueryResult<TResult>> FailedTask<TResult>(IEnumerable<Error> errors) => Task.FromResult(Failed<TResult>(errors));

        public class FailedQueryResult<TResult> : QueryResult<TResult>
        {
            public override TResult Value => throw new CannotAccessFailedResultValue();
            public override IEnumerable<Error> Errors { get; }

            public FailedQueryResult(IEnumerable<Error> errors) : base(false)
            {
                Errors = new ReadOnlyCollection<Error>(
                    (errors ?? new List<Error>())
                    .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                    .Select(
                        i => new Error(i.SelectMany(e => e.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                    .Where(i => i.Messages.Any())
                    .ToList());

                if (!Errors.Any())
                    throw new InvalidOperationException("Need at least one error.");
            }
        }

        public class SuccessQueryResult<TResult> : QueryResult<TResult>
        {
            public override TResult Value { get; }
            public override IEnumerable<Error> Errors { get; } = new List<Error>();

            public SuccessQueryResult(TResult result) : base(true)
            {
                Value = result;
            }
        }
    }

    public abstract class QueryResult<TResult>
    {
        public abstract TResult Value { get; }
        public bool Success { get; }
        public bool Fail => !Success;
        public abstract IEnumerable<Error> Errors { get; }

        protected QueryResult(bool success)
        {
            Success = success;
        }
    }
}
