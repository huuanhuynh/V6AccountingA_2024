using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using V6SqlConnect;
using V6Tools;

namespace Tools
{
    public class V6BusinessHelper
    {
        public static bool Insert(string con, string tableName, IDictionary<string, object> dataDictionary)
        {
            V6TableStruct tableStruct = GetTableStruct(con, tableName);
            string sql = SqlGenerator.GenInsertSql(53, tableName, tableStruct, dataDictionary);
            int result = SqlHelper.ExecuteNonQuery(con, CommandType.Text, sql);
            return result > 0;
        }

        public static V6TableStruct GetTableStruct(string con, string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) throw new Exception("V6ERRORNOTABLE");
            var resultStruct = new V6TableStruct(tableName);

            try
            {
                string sql = "Select ORDINAL_POSITION, COLUMN_NAME" +
                             ", DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH" +
                             ", NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE" +
                             " From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = '" +
                             tableName + "'" +
                             " Order by ORDINAL_POSITION";
                DataTable columnsStructInfo = SqlHelper.ExecuteDataset(con, CommandType.Text, sql)
                    .Tables[0];

                foreach (DataRow row in columnsStructInfo.Rows)
                {
                    var columnStruct = new V6ColumnStruct
                    {
                        ColumnName = row["COLUMN_NAME"].ToString().Trim(),
                        AllowNull = "YES" == row["IS_NULLABLE"].ToString(),
                        ColumnDefault = (row["COLUMN_DEFAULT"] == null || row["COLUMN_DEFAULT"].ToString().Trim() == "") ? null : row["COLUMN_DEFAULT"].ToString().Trim(),
                        sql_data_type_string = row["DATA_TYPE"].ToString().Trim()
                    };
                    try
                    {
                        int num;
                        string stringLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        string numLength = row["NUMERIC_PRECISION"].ToString();
                        string numDecimal = row["NUMERIC_SCALE"].ToString();

                        if (stringLength != "")
                        {
                            num = (int)row["CHARACTER_MAXIMUM_LENGTH"];
                            columnStruct.MaxLength = num;
                        }
                        else if (numLength != "")
                        {
                            //columnStruct.MaxLength = -2;
                            num = Int32.Parse(numLength);
                            columnStruct.MaxNumLength = num;
                            num = Int32.Parse(numDecimal);
                            columnStruct.MaxNumDecimal = num;
                        }

                    }
                    catch
                    {
                        // ignored
                    }

                    resultStruct.Add(columnStruct.ColumnName.ToUpper(), columnStruct);
                }
                return resultStruct;
            }
            catch
            {
                // ignored
            }
            return resultStruct;
        }
    }
}
