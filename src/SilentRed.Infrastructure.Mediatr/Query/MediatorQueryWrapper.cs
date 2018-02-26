using System;
using System.Collections.Concurrent;
using System.Reflection;
using MediatR;
using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.Mediatr
{
    public static class MediatorQueryWrapper
    {
        public static bool IsWrappedMediatorQueryHandler(this Type serviceType)
        {
            return IsWrappedCache.GetOrAdd(
                serviceType,
                type =>
                {
                    if (!type.GetTypeInfo().IsGenericType)
                    {
                        return false;
                    }

                    var firstArgumentType = type.GenericTypeArguments[0];
                    if (!firstArgumentType.GetTypeInfo().IsGenericType)
                    {
                        return false;
                    }

                    var genericTypeArguments = firstArgumentType.GenericTypeArguments;
                    var wrappedType = typeof(QueryWrappedForMediator<,>).MakeGenericType(genericTypeArguments);

                    return firstArgumentType == wrappedType;
                });
        }

        public static QueryWrappedForMediator<TQueryResult> Wrap<TQueryResult>(
            IQuery<TQueryResult> query,
            Headers headers)
        {
            var type = query.GetType();
            var returnType = WrapperCache.GetOrAdd(
                type,
                serviceType =>
                {
                    var baseReturnType = typeof(QueryWrappedForMediator<,>);
                    return baseReturnType.MakeGenericType(type, typeof(TQueryResult));
                });

            return (QueryWrappedForMediator<TQueryResult>)Activator.CreateInstance(returnType, query, headers);
        }

        private static readonly ConcurrentDictionary<Type, bool> IsWrappedCache =
            new ConcurrentDictionary<Type, bool>();

        private static readonly ConcurrentDictionary<Type, Type> WrapperCache = new ConcurrentDictionary<Type, Type>();
    }

    public abstract class QueryWrappedForMediator<TQueryResult> : IRequest<TQueryResult>
    {
        public Headers Headers { get; }

        protected QueryWrappedForMediator(Headers headers)
        {
            Headers = headers;
        }
    }

    public class QueryWrappedForMediator<TQuery, TQueryResult> : QueryWrappedForMediator<TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
        public TQuery Query { get; }

        public QueryWrappedForMediator(TQuery query, Headers headers) : base(headers)
        {
            Query = query;
        }
    }
}
