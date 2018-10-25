using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Init
{
    /// <summary>
    /// Các hàm static giúp lấy các tham số lưu trong bảng V6lookup
    /// </summary>
    public static class V6Lookup
    {
        /// <summary>
        /// Lấy 1 giá trị theo vVar và fieldName
        /// </summary>
        public static Values Value = new Values();
        public static ValuesByTableName ValueByTableName = new ValuesByTableName();
        public class Values
        {
            private readonly SortedDictionary<string, DataRow> _dic = new SortedDictionary<string, DataRow>();
            /// <summary>
            /// 
            /// </summary>
            /// <param name="vVar"></param>
            /// <param name="field"></param>
            /// <returns></returns>
            public object this[string vVar, string field]
            {
                
                get
                {
                    vVar = vVar.ToUpper().Replace("'", "''");
                    if (_dic.ContainsKey(vVar))
                    {
                        var r = _dic[vVar];
                        if (r.Table.Columns.Contains(field))
                        {
                            return r[field];
                        }
                    }
                    else
                    {
                        //Đọc lên từ database
                        DataTable t = SqlConnect.Select("V6Lookup", "", "[vVar]='"+vVar+"'").Data;
                        if (t.Rows.Count == 1)
                        {
                            DataRow r = t.Rows[0];
                            _dic.Add(vVar, r);

                            if (r.Table.Columns.Contains(field))
                            {
                                return r[field];
                            }
                        }
                    }
                    return "";
                }

            }
        }
        /// <summary>
        /// [tableName, field]
        /// </summary>
        public class ValuesByTableName
        {
            private readonly SortedDictionary<string, DataRow> _dic = new SortedDictionary<string, DataRow>();
            /// <summary>
            /// 
            /// </summary>
            /// <param name="tableName"></param>
            /// <param name="field"></param>
            /// <returns></returns>
            public object this[string tableName, string field]
            {

                get
                {
                    tableName = tableName.ToUpper().Replace("'", "''");
                    if (_dic.ContainsKey(tableName))
                    {
                        var r = _dic[tableName];
                        if (r.Table.Columns.Contains(field))
                        {
                            return r[field];
                        }
                    }
                    else
                    {
                        //Đọc lên từ database
                        DataTable t = SqlConnect.Select
                            ("V6Lookup", "","[vMa_file]='" + tableName + "'").Data;
                        if (t.Rows.Count == 1)
                        {
                            DataRow r = t.Rows[0];
                            _dic.Add(tableName, r);

                            if (r.Table.Columns.Contains(field))
                            {
                                return r[field];
                            }
                        }
                    }
                    return "";
                }

            }
        }
        
        /// <summary>
        /// Lấy các trường lọc danh mục view
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string[] GetDefaultLookupFields(string tableName)
        {
            string lang = V6Setting.Language;
            string result = GetValueByTableName(tableName, lang + "Fields");
            return result.Split(',');
        }

        ///// <summary>
        ///// Lấy danh sách trường lọc cho report (in danh mục)
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public static Dictionary<string,string>  GetReportFilterFields(string tableName)
        //{
        //    string vLfScatter = GetValueByTableName(tableName, "vLfScatter");
        //    var sss = ObjectAndString.SplitString(vLfScatter);
        //    var result = new Dictionary<string,string>();
        //    foreach (string s in sss)
        //    {
        //        var ss = s.Split(new[] {':'}, 2);
        //        result[ss[0]] = ss.Length > 1 ? ss[1] : "";
        //    }
        //    return result;
        //}



        /// <summary>
        /// Lấy 1 giá trị trong bảng V6Lookup theo tên bảng
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetValueByTableName(string tableName, string field)
        {
            var sql = "Select top 1 [" + field + "] from V6lookup where vMa_file = @p";
            var pr = new SqlParameter("@p", tableName);
            var o = SqlConnect.ExecuteScalar(CommandType.Text, sql, pr) ?? "";
            return o.ToString().Trim();
        }

        /// <summary>
        /// Lấy 1 giá trị trong bảng V6Lookup theo vVar
        /// </summary>
        /// <param name="vvar"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetValueByVvar(string vvar, string field)
        {
            var sql = "Select top 1 [" + field + "] from V6lookup where vvar = @p";
            var pr = new SqlParameter("@p", vvar);
            var o = SqlConnect.ExecuteScalar(CommandType.Text, sql, pr) ?? "";
            return o.ToString().Trim();
        }

        /// <summary>
        /// Lấy config trong bảng V6lookup bằng table_name. Nếu không có dữ liệu kiểm tra config.NoInfo.
        /// </summary>
        /// <param name="vMa_file">vMa_file</param>
        /// <returns></returns>
        public static V6lookupConfig GetV6lookupConfigByTableName(string vMa_file)
        {
            V6lookupConfig lstConfig = new V6lookupConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", vMa_file) };
                var executeResult = V6BusinessHelper.Select("V6lookup", "*", "vMa_file=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6lookupConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy config trong bảng V6lookup bằng vVar. Nếu không có dữ liệu kiểm tra config.NoInfo.
        /// </summary>
        /// <param name="vVar"></param>
        /// <returns></returns>
        public static V6lookupConfig GetV6lookupConfig(string vVar)
        {
            V6lookupConfig lstConfig = new V6lookupConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", vVar) };
                var executeResult = V6BusinessHelper.Select("V6lookup", "*", "vVar=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6lookupConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }
    }
}
