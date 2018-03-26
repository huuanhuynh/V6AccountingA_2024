using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6AccountingBusiness
{
    public class V6Categories
    {
        protected string Name;
        public V6Categories()
        {
            Name = "Name";
        }
        
        #region === SELECT ====
        
        public V6SelectResult Select(V6TableName tableName, SortedDictionary<string, object> keys = null)
        {
            return Select(tableName.ToString(), keys);
        }

        public V6SelectResult Select(string tableName, SortedDictionary<string, object> keys = null)
        {
            if (!V6Login.UserRight.AllowSelect(tableName)) return new V6SelectResult();
            SqlParameter[] plist;
            var where = SqlGenerator.GenSqlWhereOutParameters(keys, out plist);
            string field = "", group = "", sort = "";
            return SqlConnect.Select(tableName, field, where, group, sort, plist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <param name="page"></param>
        /// <param name="size">0 hoặc nhỏ hơn = load all.</param>
        /// <param name="where"></param>
        /// <param name="sortField"></param>
        /// <param name="ascending">Mặc định sẽ là asc</param>
        /// <returns></returns>
        public V6SelectResult SelectPaging(V6TableName name, string fields, int page, int size,
            string where, string sortField, bool ascending = true)
        {
            if (V6Login.UserRight.AllowSelect(name))
            {
                if (string.IsNullOrEmpty(sortField))
                {
                    sortField = V6TableHelper.GetDefaultSortField(name);
                }
                
                V6SelectResult result = SqlConnect.SelectPaging(name.ToString(), fields, page, size, where, sortField, ascending);
                
                var listColumn = (from DataColumn column in result.Data.Columns select column.ColumnName).ToList();

                result.FieldsHeaderDictionary = CorpLan2.GetFieldsHeader(listColumn, V6Setting.Language);

                return result;
            }
            return new V6SelectResult();
        }

        public V6SelectResult SelectPaging(string tableName, string fields, int page, int size,
            string where, string sortField, bool ascending = true)
        {
            if (V6Login.UserRight.AllowSelect(tableName))
            {
                if (string.IsNullOrEmpty(sortField))
                {
                    sortField = V6TableHelper.GetDefaultSortField(tableName);
                }

                V6SelectResult result = SqlConnect.SelectPaging(tableName, fields, page, size, where, sortField, ascending);

                var listColumn = (from DataColumn column in result.Data.Columns select column.ColumnName).ToList();

                result.FieldsHeaderDictionary = CorpLan2.GetFieldsHeader(listColumn, V6Setting.Language);

                return result;
            }
            return new V6SelectResult();
        }


        public DataTable SelectTable(string tableName)
        {
            return SqlConnect.SelectTable(tableName);
        }

        #endregion select

        #region ==== ADD-INSERT ====
        public bool Insert(V6TableName tableName, IDictionary<string, object> data)
        {
            return Insert(tableName.ToString(), data);
        }

        public bool Insert(string tableName, IDictionary<string, object> data)
        {
            var structTable = V6SqlconnectHelper.GetTableStruct(tableName);
            var sql = SqlGenerator.GenInsertSql(V6Login.UserId, tableName, structTable, data);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result > 0;
        }

        #endregion add

        #region ==== EDIT-UPDATE ====

        /// <summary>
        /// Cập nhập dữ liệu vào bảng theo keys.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data">key toàn UPPER</param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Update(V6TableName tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys)
        {
            return Update(tableName.ToString(), data, keys);
        }

        /// <summary>
        /// Cập nhập dữ liệu vào bảng theo keys.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data">key toàn UPPER</param>
        /// <param name="keys"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Update(string tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys, SqlTransaction transaction = null)
        {
            var structTable = V6SqlconnectHelper.GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSql(V6Login.UserId, tableName, structTable, data, keys);
            var result = transaction == null
                ? SqlConnect.ExecuteNonQuery(CommandType.Text, sql)
                : SqlConnect.ExecuteNonQuery(transaction, CommandType.Text, sql);
            return result;
        }

        #endregion edit

        #region ==== DELETE ====
        public int Delete(V6TableName tableName, SortedDictionary<string, object> keys)
        {
            return Delete(tableName.ToString(), keys);
        }

        public int Delete(string tableName, SortedDictionary<string, object> keys)
        {
            if (keys == null || keys.Count == 0)
            {
                throw new Exception("Để tránh xóa nhầm, key không được rỗng.\n Nếu muốn xóa hết hãy gọi DeleteAll!");
            }
            return Delete(V6Login.UserId, tableName, keys);
        }

        public int Delete(SqlTransaction tran, V6TableName tableName, SortedDictionary<string, object> keys)
        {
            return Delete(tran, tableName.ToString(), keys);
        }

        public int Delete(SqlTransaction tran, string tableName, SortedDictionary<string, object> keys)
        {
            if (keys == null || keys.Count == 0)
            {
                throw new Exception("Để tránh xóa nhầm, key không được rỗng.\n Nếu muốn xóa hết hãy gọi DeleteAll!");
            }
            return Delete(V6Login.UserId, tableName, keys, tran);
        }

        public int Delete(int userId, string tableName, SortedDictionary<string, object> keys, SqlTransaction transaction = null)
        {
            var structTable = V6SqlconnectHelper.GetTableStruct(tableName);
            var sql = SqlGenerator.GenDeleteSql(structTable, keys);
            var result = transaction == null
                ? SqlConnect.ExecuteNonQuery(CommandType.Text, sql)
                : SqlConnect.ExecuteNonQuery(transaction, CommandType.Text, sql);
            return result;
        }

        public void DeleteAll(V6TableName tableName)
        {
            throw new Exception("DeleteAll!!!\nChưa có chức năng này.");
        }
        #endregion delete

        #region ==== CHECK ====
        
        /// <summary>
        /// Đã tồn tại mã trong bảng => true
        /// [VPA_isExistOneCode_List]
        /// </summary>
        /// <returns></returns>
        public bool IsExistOneCode_List(string @cInputTable_list, string @cInputField, string @cpInput)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", @cInputTable_list),
                new SqlParameter("@cInputField", @cInputField),
                new SqlParameter("@cpInput", @cpInput)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistOneCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }

        public bool IsExistTwoCode_List(string @cInputTable_list, string @cInputField1, string @cpInput1,
              string @cInputField2, string @cpInput2)

        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", @cInputTable_list),
                new SqlParameter("@cInputField1", @cInputField1),
                new SqlParameter("@cpInput1", @cpInput1),
                new SqlParameter("@cInputField2", @cInputField2),
                new SqlParameter("@cpInput2", @cpInput2)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistTwoCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }
        #endregion check
        
        public SortedDictionary<string, string> GetHideColumns(string tableName)
        {
            var result = new SortedDictionary<string, string>();
            tableName = tableName.Replace("'", "");
            var data = SqlConnect.Select("V6Lookup", "Top 1 v_hide", "vMa_File='" + tableName + "'", "", "").Data;
            if (data != null && data.Rows.Count > 0)
            {
                var hideFields = data.Rows[0][0].ToString();
                hideFields = hideFields.Trim().ToUpper();
                var sss = hideFields.Split(',');
                foreach (string s in sss)
                {
                    var f = s.Trim();
                    result.Add(f, f);
                }
                return result;
            }
            //var hideFields = new LookupService().GetValueByTableName(tableName, "v_hide");
            return new SortedDictionary<string, string>();
        }
    }
}
