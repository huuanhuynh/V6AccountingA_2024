using System;
using System.Reflection;
using System.ComponentModel;

using V6Soft.Common.Utils.TypeExtensions;


namespace V6Soft.Common.Test.Extensions
{
    public static class Utilities
    {
        /// <summary>
        /// Fills an object instance with mock values.
        /// </summary>
        /// <param name="seed">Different seeds help make different mock values
        /// between calls of this method.</param>
        public static void FillMockValues<T>(this T target, int seed)
        {
            Type type = typeof(T),
                propertyType;
            PropertyInfo[] publicProperties = type.GetProperties();
            foreach (var property in publicProperties)
            {
                propertyType = property.PropertyType;

                // "Nullable" types need to be treated diffrently.
                // We need to get its underlying type.
                if (propertyType.IsGenericType
                     && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propertyType = new NullableConverter(propertyType).UnderlyingType;
                }
                
                if (propertyType.IsNumeric())
                {
                    property.SetValue(target, Convert.ChangeType(seed, propertyType));
                }
                else if (propertyType.Equals(typeof(string)))
                {
                    property.SetValue(target, property.Name + seed);
                }
            }
        }

    }
}
