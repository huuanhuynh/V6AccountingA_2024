using System;
using System.ComponentModel;
using System.Data;


namespace V6Soft.Common.Utils.DatabaseExtensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        ///     Checks if specified type is nullable one.
        /// </summary>
        public static bool IsNullableType(Type valueType)
        {
            return (valueType.IsGenericType &&
                valueType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        ///     Reads column value and converts to C# type.
        /// </summary>
        public static T GetValue<T>(this IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            Type valueType = typeof(T);
            if (value != DBNull.Value)
            {
                if (!IsNullableType(valueType))
                {
                    return (T)Convert.ChangeType(value, valueType);
                }
                else
                {
                    NullableConverter nc = new NullableConverter(valueType);
                    return (T)Convert.ChangeType(value, nc.UnderlyingType);
                }
            }
            return default(T);
        }
    }
}
