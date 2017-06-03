using System;
using System.Collections.Generic;


namespace V6Soft.Common.Utils.TypeExtensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Checks whether a type is numeric type or not.
        /// </summary>
        public static bool IsNumeric(this Type type)
        {
            var numericTypes = new HashSet<Type>
            {
                typeof(int), typeof(long), typeof(short),
                typeof(uint), typeof(ulong), typeof(ushort),
                typeof(float), typeof(double), typeof(decimal),
                typeof(byte), typeof(sbyte)
            };
            return numericTypes.Contains(type);
        }

        /// <summary>
        ///     Converts this Type instance to name of a XML SQL data type.
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        public static string ConvertToXmlSqlType(this Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                    return "sqltypes:bit";
                case "System.Byte":
                    return "sqltypes:tinyint";
                case "System.Int16":
                    return "sqltypes:smallint";
                case "System.Int32":
                    return "sqltypes:int";
                case "System.Int64":
                    return "sqltypes:bigint";
                case "System.Single":
                case "System.Double":
                    return "sqltypes:float";
                case "System.Decimal":
                    return "sqltypes:money";
                case "System.DateTime":
                    return "sqltypes:datetime";
                case "System.String":
                    return "sqltypes:nvarchar";
                case "System.Guid":
                    return "sqltypes:uniqueidentifier";
                default:
                    throw new NotSupportedException("No XML SQL data type is matched.");
            }
        }

        /// <summary>
        ///     Creates an instance of Type equivalent to specified SQL data type name
        ///     which is copied from model definition XML.
        /// </summary>
        public static Type ParseFromXmlSqlType(string sqlType)
        {
            switch (sqlType)
            {
                case "sqltypes:bit":
                    return typeof(bool);
                case "sqltypes:tinyint":
                    return typeof(byte);
                case "sqltypes:smallint":
                    return typeof(short);
                case "sqltypes:int":
                    return typeof(int);
                case "sqltypes:bigint":
                    return typeof(long);
                case "sqltypes:float":
                case "sqltypes:real":
                    return typeof(double);
                case "sqltypes:money":
                case "sqltypes:decimal":
                case "sqltypes:numeric":
                    return typeof(decimal);
                case "sqltypes:date":
                case "sqltypes:time":
                case "sqltypes:datetime":
                case "sqltypes:smalldatetime":
                    return typeof(DateTime);
                case "sqltypes:char":
                case "sqltypes:nchar":
                case "sqltypes:varchar":
                case "sqltypes:nvarchar":
                case "sqltypes:text":
                case "sqltypes:ntext":
                    return typeof(string);
                case "sqltypes:uniqueidentifier":
                    return typeof(Guid);
                default:
                    return typeof(object);
            }
        }
    }
}
