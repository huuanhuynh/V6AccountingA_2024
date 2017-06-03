using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace V6Soft.Common.Utils.Linq
{
    public static class FilterExpressionBuilder
    {
        public static IQueryable<T> ApplyFiltering<T>(IQueryable<T> source, IList<FilterDescriptor> filters)
        {
            var predicate = GetExpression<T>(filters).Compile();
            return source.Where(predicate).AsQueryable();
        }

        private static Expression<Func<T, bool>> GetExpression<T>(IList<FilterDescriptor> filters)
        {
            if (filters.Count == 0)
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            Expression exp = null;

            if (filters.Count == 1)
            {
                exp = GetExpression<T>(param, filters[0]);
            }
            else if (filters.Count == 2)
            {
                exp = GetExpression<T>(param, filters[0], filters[1]);
            }
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                    {
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        //TODO: refer later
        //private static MethodInfo indexOfMethod = typeof(string).GetMethod("IndexOf", new Type[] { typeof(string) });
        //private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        //private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        private static BinaryExpression GetExpression<T>(ParameterExpression param, FilterDescriptor filter1, FilterDescriptor filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(bin1, bin2);
        }

        private static Expression GetExpression<T>(ParameterExpression param, FilterDescriptor filter)
        {
            MemberExpression member = ExpressionHelper.GetMemberExpression(param, filter.Field);

            var filterValue = ConvertToTypeValue(filter.Value, member.Type);
            ConstantExpression constant = Expression.Constant(filterValue);

            if (member.Type == typeof(double) || member.Type == typeof(int) || member.Type == typeof(float)
                || member.Type == typeof(Single) || member.Type == typeof(short) || member.Type == typeof(long)
                || member.Type == typeof(double?) || member.Type == typeof(int?) || member.Type == typeof(float?)
                || member.Type == typeof(Single?) || member.Type == typeof(short?) || member.Type == typeof(long?))
            {
                return GetNumericExpression(param, filter, member, constant);
            }
            else if (member.Type == typeof(string))
            {
                var left = Expression.NotEqual(member, Expression.Constant(null, member.Type));
                var right = GetStringExpression(param, filter, member, constant);
                return Expression.AndAlso(left, right);
            }
            else if (member.Type == typeof(bool) || member.Type == typeof(bool?))
            {
                return GetBooleanExpression(param, filter, member, constant);
            }
            else if (member.Type == typeof(DateTime))
            {
                var navigationProperty = ExpressionHelper.GetProperTypeInfo(param.Type, filter.Field);
                var navigationMember = Expression.MakeMemberAccess(param, navigationProperty);

                var name = typeof(DateTime).GetProperty("Date");
                member = Expression.MakeMemberAccess(navigationMember, name);

                return GetDateTimeExpression(param, filter, member, constant);
            }
            else if (member.Type == typeof(DateTime?))
            {
                var left = Expression.NotEqual(member, Expression.Constant(null, member.Type));

                var navigationProperty = ExpressionHelper.GetProperTypeInfo(param.Type, filter.Field);
                var navigationMember = Expression.MakeMemberAccess(param, navigationProperty);

                var name = typeof(DateTime?).GetProperty("Value");
                navigationMember = Expression.MakeMemberAccess(navigationMember, name);

                name = typeof(DateTime).GetProperty("Date");
                member = Expression.MakeMemberAccess(navigationMember, name);

                var right = GetDateTimeExpression(param, filter, member, constant);

                return Expression.AndAlso(left, right);
            }
            else if (member.Type == typeof(IKeyNamePair))
            {
                var left = Expression.NotEqual(member, Expression.Constant(null, member.Type));

                var navigationProperty = ExpressionHelper.GetProperTypeInfo(param.Type, filter.Field);
                var name = typeof(IKeyNamePair).GetProperty("Name");
                var navigationMember = Expression.MakeMemberAccess(param, navigationProperty);
                member = Expression.MakeMemberAccess(navigationMember, name);

                var right = GetStringExpression(param, filter, member, constant);

                return Expression.AndAlso(left, right);
            }
            return null;
        }

        private static Expression GetStringExpression(ParameterExpression param, FilterDescriptor filter,
            MemberExpression member, ConstantExpression constant)
        {
            ConstantExpression comparisonConstant = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));

            switch (filter.Operator)
            {
                case FilterOperator.Equal:
                    return Expression.Equal(member, constant);

                case FilterOperator.Contains:
                    //return Expression.Call(member, containsMethod, constant);
                    var indexOf = Expression.Call(member, "IndexOf", null, constant, comparisonConstant);
                    return Expression.GreaterThanOrEqual(indexOf, Expression.Constant(0));

                case FilterOperator.StartsWith:
                    //return Expression.Call(member, startsWithMethod, constant);
                    var startsWith = Expression.Call(member, "StartsWith", null, constant, comparisonConstant);
                    return Expression.Equal(startsWith, Expression.Constant(true));

                case FilterOperator.EndsWith:
                    //return Expression.Call(member, endsWithMethod, constant);
                    var endsWith = Expression.Call(member, "EndsWith", null, constant, comparisonConstant);
                    return Expression.Equal(endsWith, Expression.Constant(true));
            }

            return null;
        }

        private static Expression GetDateTimeExpression(ParameterExpression param, FilterDescriptor filter,
            MemberExpression member, ConstantExpression constant)
        {
            switch (filter.Operator)
            {
                case FilterOperator.Equal:
                    return Expression.Equal(member, constant);

                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case FilterOperator.LessThan:
                    return Expression.LessThan(member, constant);

                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

            }

            return null;
        }

        private static Expression GetBooleanExpression(ParameterExpression param, FilterDescriptor filter,
            MemberExpression member, ConstantExpression constant)
        {
            //return Expression.Equal(member, constant);
            var converted = Expression.Convert(constant, member.Type);
            return Expression.Equal(member, converted);
        }

        private static Expression GetNumericExpression(ParameterExpression param, FilterDescriptor filter,
            MemberExpression member, ConstantExpression constant)
        {
            var converted = Expression.Convert(constant, member.Type);
            switch (filter.Operator)
            {
                case FilterOperator.Equal:
                    return Expression.Equal(member, converted);

                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(member, converted);

                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, converted);

                case FilterOperator.LessThan:
                    return Expression.LessThan(member, converted);

                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, converted);
            }

            return null;
        }

        private static object ConvertToTypeValue(object value, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type == typeof(IKeyNamePair))
            {
                return value;
            }

            //Hack: the TypeConverter is not working for Boolean somehow.
            if (type == typeof(bool))
            {
                return Convert.ToBoolean(value);
            }

            if (value == null)
            {
                if ((Nullable.GetUnderlyingType(type) != null) || !type.IsValueType)
                {
                    if (type == typeof(string))
                    {
                        return string.Empty;
                    }
                    return null;
                }
                else
                {
                    return Convert.ChangeType(value, type);
                }
            }

            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type == value.GetType())
            {
                return value;
            }

            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }

            TypeConverter converter2 = TypeDescriptor.GetConverter(value.GetType());
            if (!converter2.CanConvertTo(type))
            {
                throw new InvalidOperationException(String.Format("Unable to convert type '{0}' to '{1}'",
                    new object[] { value.GetType(), type }));
            }
            return converter2.ConvertTo(value, type);
        }

        // TODO: to refer later
        //private static Expression GetExpression<T>(ParameterExpression param, FilterDescriptor filter)
        //{
        //    MemberExpression member = Expression.PropertyOrField(param, filter.Field);
        //    var filterValue = ConvertToTypeValue(filter.Value, member.Type);
        //    ConstantExpression constant = Expression.Constant(filterValue);
        //    if (member.Type == typeof(IKeyNamePair))
        //    {
        //        var navigationProperty = GetProperTypeInfo(param.Type, filter.Field);
        //        var name = typeof(IKeyNamePair).GetProperty("Name");

        //        var navigationMember = Expression.MakeMemberAccess(param, navigationProperty);
        //        member = Expression.MakeMemberAccess(navigationMember, name);
        //    }

        //    ConstantExpression constant1 = null;
        //    MethodCallExpression indexOf = null;

        //    switch (filter.Operator)
        //    {
        //        case FilterOperator.Equal:
        //            return Expression.Equal(member, constant);

        //        case FilterOperator.GreaterThan:
        //            return Expression.GreaterThan(member, constant);

        //        case FilterOperator.GreaterThanOrEqual:
        //            return Expression.GreaterThanOrEqual(member, constant);

        //        case FilterOperator.LessThan:
        //            return Expression.LessThan(member, constant);

        //        case FilterOperator.LessThanOrEqual:
        //            return Expression.LessThanOrEqual(member, constant);

        //        case FilterOperator.Contains:
        //            //return Expression.Call(member, containsMethod, constant);

        //            constant1 = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));
        //            indexOf = Expression.Call(member, "IndexOf", null, constant, constant1);
        //            return Expression.GreaterThanOrEqual(indexOf, Expression.Constant(0));

        //        case FilterOperator.StartsWith:
        //            //return Expression.Call(member, startsWithMethod, constant);
        //            constant1 = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));
        //            indexOf = Expression.Call(member, "IndexOf", null, constant, constant1);
        //            return Expression.Equal(indexOf, Expression.Constant(0));

        //        case FilterOperator.EndsWith:
        //            return Expression.Call(member, endsWithMethod, constant);
        //    }

        //    return null;
        //}
    }
}
