using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6SyncLibrary2021
{
    /// <summary>
    /// Biến dùng chung cho toàn bộ
    /// </summary>
    public static class Functions
    {
        public static void ChangeColumnName(DataTable table, string oldName, string newName)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName)) return;
            if (table.Columns.Contains(newName)) return;// throw new Exception("Exist newName: " + newName);
            if (!table.Columns.Contains(oldName)) throw new Exception("NotExist oldName: " + oldName);
            table.Columns[oldName].ColumnName = newName;
        }

        public static DataTable GetAlctCt(string conString, string ma_ct)
        {
            var _alct = Select(conString, "ALCTCT", "", "ma_ct='" + ma_ct + "' and User_id_ct=99", "");
            return _alct;
        }

        public static DateTime GetServerDateTime(string conString)
        {
            return (DateTime)SqlHelper.ExecuteScalar(conString, CommandType.Text, "Select GETDATE()");
        }

        /// <summary>
        /// Lấy lên cấu trúc bảng dữ liệu.
        /// </summary>
        /// <param name="conString">Chuỗi kết nối sql</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static V6TableStruct GetTableStruct(string conString, string tableName)
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
                DataTable columnsStructInfo = SqlHelper.ExecuteDataset(conString, CommandType.Text, sql)
                    .Tables[0];

                foreach (DataRow row in columnsStructInfo.Rows)
                {
                    var columnStruct = new V6ColumnStruct
                    {
                        ColumnName = row["COLUMN_NAME"].ToString().Trim(),
                        AllowNull = "YES" == row["IS_NULLABLE"].ToString(),
                        ColumnDefault = (row["COLUMN_DEFAULT"] == null || row["COLUMN_DEFAULT"].ToString().Trim() == "" ) ? null : row["COLUMN_DEFAULT"].ToString().Trim(),
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

        public static decimal Vround(decimal number, int round)
        {
            // Tuanmh 14/09/2017 (3866,5->3866)
            //return Math.Round(number, round, MidpointRounding.ToEven);
            return Math.Round(number, round, MidpointRounding.AwayFromZero);

        }

        public static DataTable Select(string conString, string tableName, string fields, string where="", string group="", string sort = "", params SqlParameter[] prList)
        {
            if (fields == null) fields = "*";

            var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields;
            var whereClause = string.IsNullOrEmpty(where) ? "" : " WHERE " + where;
            var groupClause = string.IsNullOrEmpty(group) ? "" : " GROUP BY " + group;
            var sortOrder = string.IsNullOrEmpty(sort) ? "" : " ORDER BY " + sort;

            var sql = string.Format("Select {0} from {1} {2} {3} {4}",
                fieldsSelect, tableName, whereClause, groupClause, sortOrder);
            var ds = SqlHelper.ExecuteDataset(conString, CommandType.Text, sql,
                300, prList);
            var t = ds.Tables.Count > 0 ? ds.Tables[0] : null;

            return t;
        }

        public static string GetV6Option(string conString, string name)
        {
            var v6option = Select(conString, "V6Option", "VAL", "NAME='"+name+"'");
            if (v6option.Rows.Count > 0)
            {
                return v6option.Rows[0]["VAL"].ToString().Trim();
            }

            return "";
        }

        public static string GetNewSoCt_date(string conString, string maCt, DateTime date, string type, string maDvcs, string makho, string sttrec, int userId, out string ma_sonb)
        {
            ma_sonb = "";
            SqlParameter[] plist =
            {
                new SqlParameter("@Ma_ct", maCt),
                new SqlParameter("@Ngay_ct", date.Date),
                new SqlParameter("@Type", type),
                new SqlParameter("@ma_dvcs", maDvcs),
                new SqlParameter("@ma_kho", makho),
                new SqlParameter("@Stt_rec", sttrec),
                new SqlParameter("@User_id", userId)
            };
            var result = SqlHelper.ExecuteDataset(conString, CommandType.StoredProcedure, "VPA_GetNewSoct_Date", plist);
            if (result.Tables.Count == 0) return "";
            var data = result.Tables[0];
            if (data.Rows.Count == 0) return "";
            ma_sonb = data.Rows[0]["MA_SONB"].ToString().Trim();
            var formatText = data.Rows[0]["TRANSFORM"].ToString().Trim();
            if (formatText == "") return "";
            var value = ObjectAndString.ObjectToDecimal(data.Rows[0]["SO_CT"]);
            var sResult = String.Format(formatText, value);
            
            return sResult;
        }

        public static string GetNewSttRec(string conString, string mact)
        {
            if (mact.Length > 3) mact = mact.Substring(0, 3);
            var param = new SqlParameter("@pMa_ct", mact);
            string sttRec = SqlHelper.ExecuteScalar(conString, CommandType.StoredProcedure, "VPA_sGet_stt_rec", param).ToString();
            if (String.IsNullOrEmpty(sttRec))
            {
                throw new Exception("Không tạo mới STT_REC được.");
            }
            return sttRec;
        }

        public static void UpdateDKlist(DataTable table, string field, object value)
        {
            if (table != null && table.Columns.Contains(field))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i][field] = value;
                }
            }
        }
    }
}
