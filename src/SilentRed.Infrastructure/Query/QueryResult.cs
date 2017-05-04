// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure
{
    public static class QueryResult
    {
        public static QueryResult<TResult> Succeeded<TResult>(TResult result)
            => new SuccessQueryResult<TResult>(result);

        public static Task<QueryResult<TResult>> SucceededTask<TResult>(TResult result)
            => Task.FromResult(Succeeded(result));

        public static QueryResult<TResult> Failed<TResult>(string at, IEnumerable<Error> errors)
            => new FailedQueryResult<TResult>(at, errors);

        public static Task<QueryResult<TResult>> FailedTask<TResult>(string at, IEnumerable<Error> errors)
            => Task.FromResult(Failed<TResult>(at, errors));

        private class FailedQueryResult<TResult> : QueryResult<TResult>
        {
            public string At { get; }
            public sealed override IEnumerable<Error> Errors { get; }
            public override TResult Value => throw new CannotAccessFailedResultValueException();

            public FailedQueryResult(string at, IEnumerable<Error> errors) : base(false)
            {
                At = at;
                Errors = new ReadOnlyCollection<Error>((errors ?? new List<Error>()).Flatten());

                if (!Errors.Any())
                    throw new InvalidOperationException("Need at least one error.");
            }
        }

        public class SuccessQueryResult<TResult> : QueryResult<TResult>
        {
            public override IEnumerable<Error> Errors { get; } = new List<Error>();
            public override TResult Value { get; }

            public SuccessQueryResult(TResult result) : base(true)
            {
                Value = result;
            }
        }
    }

    public abstract class QueryResult<TResult>
    {
        public abstract IEnumerable<Error> Errors { get; }
        public bool Fail => !Success;
        public bool Success { get; }
        public abstract TResult Value { get; }

        protected QueryResult(bool success)
        {
            Success = success;
        }
    }
}
