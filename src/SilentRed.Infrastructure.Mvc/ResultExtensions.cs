using System;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.Mvc
{
    public static class ResultExtensions
    {
        public static void ThrowIfNeeded(this CommandResult result)
        {
            switch (result)
            {
                case null:
                    {
                        throw new ArgumentNullException(nameof(result));
                    }
                case CommandSuccess _:
                    return;
                case CommandValidationFailed res:
                    {
                        throw new ValidationException(res.CommandType, res.Errors);
                    }
                case CommandAuthorizationFailed res:
                    {
                        throw new AuthorizationException(res.CommandType, res.Errors);
                    }
                case CommandBusinessRulesViolationResult res:
                    {
                        throw new BusinessRuleException(res.CommandType, res.Errors);
                    }
                case CommandFailed res:
                    {
                        throw new FailedCommandResultUnknownException(res.GetType(), res.CommandType, res.Errors);
                    }
                default:
                    {
                        throw new CommandResultUnknownException(result.GetType(), result.Success);
                    }
            }
        }

        public static TResult ValueOrThrow<TResult>(this QueryResult<TResult> result)
        {
            switch (result)
            {
                case null:
                    {
                        throw new ArgumentNullException(nameof(result));
                    }
                case QuerySuccess<TResult> res:
                    return res.Value;
                case QueryValidationFailed<TResult> res:
                    {
                        throw new ValidationException(res.QueryType, res.Errors);
                    }
                case QueryAuthorizationFailed<TResult> res:
                    {
                        throw new AuthorizationException(res.QueryType, res.Errors);
                    }
                case QueryBusinessRulesViolation<TResult> res:
                    {
                        throw new BusinessRuleException(res.QueryType, res.Errors);
                    }
                case QueryFailed<TResult> res:
                    {
                        throw new FailedQueryResultUnknownException(res.GetType(), res.QueryType, res.Errors);
                    }
                default:
                    {
                        throw new QueryResultUnknownException(result.GetType(), result.Success);
                    }
            }
        }
    }
}