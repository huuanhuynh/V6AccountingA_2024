using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace V6Structs
{
    /// <summary>
    /// Functions: Các hàm hỗ trợ
    /// </summary>
    public static class F
    {
        public static Type TypeFromData_Type(string dataType)
        {
            if (string.IsNullOrEmpty(dataType)) dataType = "";
            switch (dataType.ToLower())
            {
                case "date":
                case "smalldatetime":
                case "datetime":
                    return typeof(DateTime);
                case "bigint":
                    return typeof(Int64);
                case "numeric":
                    return typeof(decimal);
                case "bit":
                    return typeof(bool);
                case "smallint":
                    return typeof(Int16);
                case "decimal":
                    return typeof(decimal);
                case "smallmoney":
                    return typeof(decimal);
                case "int":
                    return typeof(int);
                case "tinyint":
                    return typeof(byte);
                case "money":
                    return typeof(decimal);
                default:
                    return typeof(string);
            }
        }

        public static SqlDbType ToSqlDbType(string sql_data_type_string)
        {
            if (string.IsNullOrEmpty(sql_data_type_string)) sql_data_type_string = "";
            switch (sql_data_type_string.ToLower())
            {
                case "date": return SqlDbType.Date;
                case "datetimeoffset": return SqlDbType.DateTimeOffset;
                case "smalldatetime": return SqlDbType.SmallDateTime;
                case "datetime": return SqlDbType.DateTime;
                case "time": return SqlDbType.Time;
                case "datetime2": return SqlDbType.DateTime2;

                case "bigint": return SqlDbType.BigInt;
                case "numeric": return SqlDbType.Decimal;
                case "bit": return SqlDbType.Bit;
                case "smallint": return SqlDbType.SmallInt;

                case "decimal": return SqlDbType.Decimal;
                case "smallmoney": return SqlDbType.SmallMoney;
                case "int": return SqlDbType.Int;
                case "tinyint": return SqlDbType.TinyInt;
                case "money": return SqlDbType.Money;
                case "float": return SqlDbType.Float;
                case "real": return SqlDbType.Real;

                case "char": return SqlDbType.Char;
                case "nchar": return SqlDbType.NChar;
                case "varchar": return SqlDbType.VarChar;
                case "nvarchar": return SqlDbType.NVarChar;
                case "text": return SqlDbType.Text;
                case "ntext": return SqlDbType.NText;

                case "binary": return SqlDbType.Binary;
                case "varbinary": return SqlDbType.VarBinary;
                case "image": return SqlDbType.Image;

                //case "cursor": return SqlDbType.Timestamp;
                case "timestamp": return SqlDbType.Timestamp;
                //case "hierarchyid": return SqlDbType.hie;
                //case "sql_variant": return SqlDbType.Xml;
                case "xml": return SqlDbType.Xml;
                //case "table": return SqlDbType.ta;

                case "uniqueidentifier": return SqlDbType.UniqueIdentifier;

                default:
                    return SqlDbType.NVarChar;
            }

        }
    }
}
