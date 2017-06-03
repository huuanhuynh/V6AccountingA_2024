using System;
using System.Linq.Expressions;
using System.Reflection;

namespace V6Soft.Common.Utils.Linq
{
    internal static class ExpressionHelper
    {
        public static MemberExpression GetMemberExpression(ParameterExpression param, string field)
        {
            if (!field.Contains("."))
            {
                return Expression.PropertyOrField(param, field);
            }

            MemberExpression navigationMember = null;
            var propertyNames = field.Split('.');
            for (var i = 0; i < propertyNames.Length; i++)
            {
                var proppertyName = propertyNames[i];
                PropertyInfo navigationProperty = null;
                if (navigationMember == null)
                {
                    navigationProperty = GetProperTypeInfo(param.Type, proppertyName);
                    navigationMember = Expression.MakeMemberAccess(param, navigationProperty);
                }
                else
                {
                    navigationProperty = GetProperTypeInfo(navigationMember.Type, proppertyName);
                    navigationMember = Expression.MakeMemberAccess(navigationMember, navigationProperty);
                }
            }
            return navigationMember;
        }

        public static PropertyInfo GetProperTypeInfo(Type type, string name)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.Name.ToLower() == name.ToLower())
                {
                    return property;
                }
            }
            return null;
        }
    }
}
