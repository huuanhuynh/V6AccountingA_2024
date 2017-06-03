using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace V6Soft.Common.Utils.Linq
{
    public static class SortExpressionBuilder
    {
        public static IQueryable<T> ApplySorting<T>(IQueryable<T> source, IList<SortDescriptor> sorts)
        {
            var query = source;
            var firstLevel = true;
            foreach (var sort in sorts)
            {
                if (firstLevel)
                {
                    firstLevel = false;
                    query = sort.IsAscending ? query.OrderBy(sort.Field) : query.OrderByDescending(sort.Field);
                }
                else
                {
                    query = sort.IsAscending ? query.ThenBy(sort.Field) : query.ThenByDescending(sort.Field);
                }
            }
            return query;
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return CreateOrderingQuery(source, propertyName, false, false);
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return CreateOrderingQuery(source, propertyName, true, false);
        }

        private static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> source, string propertyName)
        {
            return CreateOrderingQuery(source, propertyName, false, true);
        }

        private static IOrderedQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return CreateOrderingQuery(source, propertyName, true, true);
        }

        private static IOrderedQueryable<T> CreateOrderingQuery<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            MemberExpression member = ExpressionHelper.GetMemberExpression(param, propertyName);
            if (member.Type == typeof(IKeyNamePair))
            {
                member = Expression.PropertyOrField(member, "Name");
            }

            LambdaExpression sort = Expression.Lambda(member, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), member.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
