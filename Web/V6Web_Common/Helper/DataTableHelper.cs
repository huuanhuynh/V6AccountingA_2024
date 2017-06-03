using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace V6Soft.Web.Common.Helper
{
    public class DataTableHelper
    {
        const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

        public static IEnumerable<TSource> ConvertToList<TSource>(DataTable dataTable) where TSource : new()
        {
            var result = new List<TSource>();
            var properties = typeof(TSource).GetProperties(FLAGS);
            foreach (var dataRow in dataTable.AsEnumerable().ToList())
            {
                var item = ConvertToObject<TSource>(dataTable.Columns, dataRow, properties);
                result.Add(item);
            }
            return result;
        }

        public static TSource ConvertToObject<TSource>(DataTable dataTable) where TSource : new()
        {
            if (dataTable.Rows.Count > 0)
            {
                var properties = typeof(TSource).GetProperties(FLAGS);

                return ConvertToObject<TSource>(dataTable.Columns, dataTable.Rows[0], properties);
            }
            return new TSource();
        }

        private static TSource ConvertToObject<TSource>(DataColumnCollection columns, DataRow dataRow, PropertyInfo[] properties) where TSource : new()
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
                    System.Diagnostics.Debug.WriteLine("--------------------------------------------------");
                    System.Diagnostics.Debug.WriteLine(result.GetType().FullName + " - " + fieldName + " : no mapping");
                }
            }
            return result;
        }

        public static void MapData<TSource>(DataTable dataTable, TSource source) where TSource : new()
        {
            var properties = typeof(TSource).GetProperties(FLAGS);
            var dataRow = dataTable.NewRow();
            MapData(dataTable.Columns, dataRow, source, properties);
            dataTable.Rows.Add(dataRow);
        }

        public static void MapDataList<TSource>(DataTable dataTable, IEnumerable<TSource> sources) where TSource : new()
        {
            var properties = typeof(TSource).GetProperties(FLAGS);
            foreach (var source in sources)
            {
                var dataRow = dataTable.NewRow();
                MapData(dataTable.Columns, dataRow, source, properties);
                dataTable.Rows.Add(dataRow);
            }
        }

        public static List<TSource> ConvertToList<TSource>(DataSet dataSet) where TSource : new()
        {
            var result = new List<TSource>();
            if (dataSet.Tables.Count == 0) return result;
            var dataTable = dataSet.Tables[0];
            if (dataTable.Rows.Count == 0) return result;

            return (List<TSource>)ConvertToList<TSource>(dataTable);
        }

        private static void MapData<TSource>(DataColumnCollection columns, DataRow dataRow, TSource source, PropertyInfo[] properties) where TSource : new()
        {
            foreach (DataColumn column in columns)
            {
                var columnName = column.ColumnName;
                var property = properties.SingleOrDefault(p => p.Name == columnName || HasMappingColumn(p, columnName));
                if (property == null)
                {
                    continue;
                }

                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    columnName = columnAttribute.Name;
                }

                var value = property.GetValue(source, null);
                if (property.PropertyType != column.DataType)
                {
                    value = ConvertToTypeValue(value, column.DataType);
                }

                dataRow[columnName] = value != null ? value : DBNull.Value;
            }
        }

        private static bool HasMappingColumn(PropertyInfo property, string columnName)
        {
            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            return columnAttribute != null && columnAttribute.Name.Equals(columnName);
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
            return value;
        }

        public static void MapDataStructure<TSource>(DataTable dataTable)
        {
            dataTable.Columns.Clear();
            var properties = typeof(TSource).GetProperties(FLAGS).Where(p => IsSimpleType(p.PropertyType));
            foreach (var property in properties)
            {
                var columnName = property.Name;
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    columnName = columnAttribute.Name;
                }
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    dataTable.Columns.Add(new DataColumn(columnName, Nullable.GetUnderlyingType(property.PropertyType)));
                }
                else
                {
                    dataTable.Columns.Add(new DataColumn(columnName, property.PropertyType));
                }

            }
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(decimal) || type == typeof(string);
        }
    }
}
