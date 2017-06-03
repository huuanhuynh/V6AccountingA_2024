using System;
using System.Collections.Generic;
using System.Data;

namespace V6Structs
{
    public class V6TableStruct:Dictionary<string,V6ColumnStruct>
    {
        public V6TableStruct()
        {
            
        }
        public V6TableStruct(string name)
        {
            TableName = name;
        }
        public string TableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">Nên dùng UPPER</param>
        /// <returns></returns>
        public string GetFieldType(string fieldName)
        {
            return ContainsKey(fieldName) ? this[fieldName].sql_data_type_string : null;
        }
    }

    public class V6ColumnStruct
    {
        public bool AllowNull = true;
        public string ColumnName = "", ColumnDefault = null;
        private int maxlength = -1;
        public int MaxLength
        {
            get
            {
                return maxlength > 0 ? maxlength : MaxNumLength;
            }
            set
            {
                maxlength = value;
            }
        }

        /// <summary>
        /// lowercase
        /// </summary>
        public string sql_data_type_string { get; set; }

        public SqlDbType SqlDbType
        {
            get
            {
                return F.ToSqlDbType(sql_data_type_string);
            }
        }

        public Type DataType
        {
            get
            {
                return F.TypeFromData_Type(sql_data_type_string);
            }
        }

        

        public int MaxNumLength { get; set; }
        public int MaxNumDecimal { get; set; }
        public int ColumnWidth { get; set; }
    }
}
