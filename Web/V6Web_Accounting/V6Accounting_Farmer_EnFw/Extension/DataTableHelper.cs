using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace V6Soft.Accounting.Farmers.EnFw.Extension
{
    public static class DataTableHelper
    {
        const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

        public static IEnumerable<TSource> ConvertToList<TSource>(DataTable dataTable) where TSource : new()
        {
            var result = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var properties = typeof(TSource).GetProperties(flags);

            foreach (var dataRow in dataTable.AsEnumerable().ToList())
            {
                var item = ConvertToObject<TSource>(dataTable.Columns, dataRow, properties);
                result.Add(item);
            }

            return result;
        }

        public static TSource ConvertToObject<TSource>(this DataTable dataTable) where TSource : new()
        {
            if (dataTable.Rows.Count > 0)
            {
                var properties = typeof(TSource).GetProperties(FLAGS);
                return ConvertToObject<TSource>(dataTable.Columns, dataTable.Rows[0], properties);
            }
            return new TSource();
        }

        private static TSource ConvertToObject<TSource>(DataColumnCollection columns, DataRow dataRow, IEnumerable<PropertyInfo> properties) where TSource : new()
        {
            var result = new TSource();
            foreach (var property in properties)
            {
                var fieldName = property.Name;
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    fieldName = columnAttribute.Name;
                }

                var column = columns[fieldName];
                if (column != null)
                {
                    if (dataRow[fieldName] != DBNull.Value)
                    {
                        var value = dataRow[fieldName];
                        if (property.PropertyType != column.DataType)
                        {
                            value = ConvertToTypeValue(value, property.PropertyType);
                        }
                        property.SetValue(result, value, null);
                    }
                    else
                    {
                        property.SetValue(result, null, null);
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("--------------------------------------------------");
                    //System.Diagnostics.Debug.WriteLine(result.GetType().FullName + " - " + fieldName + " : no mapping");
                }
            }
            return result;
        }

        private static object ConvertToTypeValue(object value, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (value == null)
            {
                return value;
            }

            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }

            converter = TypeDescriptor.GetConverter(value.GetType());
            if (converter.CanConvertTo(type))
            {
                return converter.ConvertTo(value, type);
            }

            if (value is bool && (type == typeof(Int16) || type == typeof(Int32)))
            {
                return Convert.ToInt16(value);
            }

            return value;
        }
    }
}
