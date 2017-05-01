using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using MediatR;

namespace SilentRed.Infrastructure.Mediatr
{
    public static class MediatorQueryWrapper
    {
        private static ConcurrentDictionary<Type, bool> _isWrappedCache = new ConcurrentDictionary<Type, bool>();
        private static ConcurrentDictionary<Type, Type> _wrapperCache = new ConcurrentDictionary<Type, Type>();

        public static bool IsWrappedMediatorQueryHandler(this Type serviceType)
        {
            return _isWrappedCache.GetOrAdd(
                serviceType,
                type =>
                {
                    if (!type.GetTypeInfo().IsGenericType)
                        return false;

                    var firstArgumentType = type.GenericTypeArguments[0];
                    if (!firstArgumentType.GetTypeInfo().IsGenericType)
                        return false;

                    var genericTypeArguments = firstArgumentType.GenericTypeArguments;
                    var wrappedType = typeof(QueryWrappedForMediator<,>).MakeGenericType(genericTypeArguments);
                    if (firstArgumentType != wrappedType)
                        return false;

                    return true;
                });
        }

        public static QueryWrappedForMediator<TQueryResult> Wrap<TQueryResult>(
            IQuery<TQueryResult> query, IDictionary<string, object> headers)
        {
            var type = query.GetType();
            var returnType = _wrapperCache.GetOrAdd(
                type,
                serviceType =>
                {
                    var baseReturnType = typeof(QueryWrappedForMediator<,>);
                    return baseReturnType.MakeGenericType(type, typeof(TQueryResult));
                });

            return (QueryWrappedForMediator<TQueryResult>)Activator.CreateInstance(returnType, query, headers);
        }
    }

    public abstract class QueryWrappedForMediator<TQueryResult> : IRequest<QueryResult<TQueryResult>>
    {
        public IDictionary<string, object> Headers { get; protected set; }

        public QueryWrappedForMediator(IQuery<TQueryResult> query, IDictionary<string, object> headers)
        {
            Headers = headers;
        }
    }

    public class QueryWrappedForMediator<TQuery, TQueryResult> : QueryWrappedForMediator<TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
        public TQuery Query { get; protected set; }

        public QueryWrappedForMediator(TQuery query, IDictionary<string, object> headers) : base(query, headers)
        {
            Query = query;
        }
    }
}
