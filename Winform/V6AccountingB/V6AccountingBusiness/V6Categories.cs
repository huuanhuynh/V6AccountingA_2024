using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccessLayer.Implementations;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6AccountingBusiness
{
    public class V6Categories
    {
        protected string Name;
        private readonly CategoriesServices Service = new CategoriesServices();
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
            return Service.Select(tableName, "", where, "", "", plist);
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

        public DataTable SelectTable(string tableName)
        {
            return Service.SelectTable(tableName);
        }

        #endregion select

        #region ==== ADD-INSERT ====
        public bool Insert(V6TableName tableName, SortedDictionary<string, object> data)
        {
            return Insert(tableName.ToString(), data);
        }

        public bool Insert(string tableName, SortedDictionary<string, object> data)
        {
            return Service.Insert(V6Login.UserId, tableName, data) > 0;
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
        /// <returns></returns>
        public int Update(string tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys)
        {
            return Service.Update(V6Login.UserId, tableName, data, keys);
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
            return Service.Delete(V6Login.UserId, tableName, keys);
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
            return Service.Delete(V6Login.UserId, tableName, keys, tran);
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
            return Service.GetHideColumns(tableName);
        }
    }
}
