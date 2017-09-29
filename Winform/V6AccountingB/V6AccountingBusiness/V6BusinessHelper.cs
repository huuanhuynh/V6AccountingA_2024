using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DataAccessLayer.Implementations;
using DataAccessLayer.Implementations.Business;
using V6AccountingBusiness.Invoices;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6AccountingBusiness
{
    public static class V6BusinessHelper
    {
        private static readonly BusinessServices Service = new BusinessServices();
        

        #region ==== CHECK ====

        /// <summary>
        /// Kiểm tra mã tồn tại.
        /// </summary>
        /// <param name="madm"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AllCheckExist(string madm, string value)
        {
            return Service.AllCheckExist(madm, value);
        }

        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// VPA_isValidOneCode_Full
        /// </summary>
        /// <returns></returns>
        public static bool IsValidOneCode_Full(string cInputTable, byte nStatus,
            string cInputField, string cpInput, string cOldItems)
        {
            return Service.IsValidOneCode_Full(cInputTable, nStatus,
                cInputField, cpInput, cOldItems);
        }
        public static bool IsValidTwoCode_Full(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2)
        {
            return Service.IsValidTwoCode_Full(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2);
        }

        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// </summary>
        /// <param name="cInputTable"></param>
        /// <param name="nStatus"></param>
        /// <param name="cInputField1"></param>
        /// <param name="cpInput1"></param>
        /// <param name="cOldItems1"></param>
        /// <param name="cInputField2"></param>
        /// <param name="cpInput2"></param>
        /// <param name="cOldItems2"></param>
        /// <param name="cInputField3"></param>
        /// <param name="cpInput3"></param>
        /// <param name="cOldItems3"></param>
        /// <returns></returns>
        public static bool IsValidThreeCode(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3)
        {
            return Service.IsValidThreeCode(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3);
        }
        public static bool IsValidThreeCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {
            return Service.IsValidThreeCode_OneNumeric(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3,
                nInputField1, npInput1, nOldItems1);
        }
        
        public static bool IsValidFourCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string cInputField4, string cpInput4, string cOldItems4,
            string nInputField1, int npInput1, int nOldItems1)
        {
            return Service.IsValidFourCode_OneNumeric(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3,
                cInputField4, cpInput4, cOldItems4,
                nInputField1, npInput1, nOldItems1);
        }
        public static bool IsValidFiveCode_TwoNumeric(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3,
           string cInputField4, string cpInput4, string cOldItems4,
            string cInputField5, string cpInput5, string cOldItems5,
           string nInputField1, int npInput1, int nOldItems1,
            string nInputField2, int npInput2, int nOldItems2)
        {
            return Service.IsValidFiveCode_TwoNumeric(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3,
                cInputField4, cpInput4, cOldItems4,
                cInputField5, cpInput5, cOldItems5,
                nInputField1, npInput1, nOldItems1,
                nInputField2, npInput2, nOldItems2);
        }

        public static bool IsValidTwoCode_OneNumeric(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {
            return Service.IsValidTwoCode_OneNumeric(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                nInputField1, npInput1, nOldItems1);
        }

        public static bool IsValidOneCode_OneDate(string cInputTable, byte nStatus,
           string cInputField1, string nInputField1, string cpInput1,
            string npInput1, string cpInput1Old, string npInput1Old)
        {
            return Service.IsValidOneCode_OneDate(cInputTable, nStatus,
                cInputField1, nInputField1, cpInput1,
                npInput1, cpInput1Old, npInput1Old);
        }

        public static bool IsValidTwoCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3)
        {
            return Service.IsValidTwoCode_OneDate(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3);
        }
        public static bool IsValidThreeCode_OneDate(string cInputTable, byte nStatus,
         string cInputField1, string cpInput1, string cOldItems1,
          string cInputField2, string cpInput2, string cOldItems2,
          string cInputField3, string cpInput3, string cOldItems3,
          string cInputField4, string cpInput4, string cOldItems4)
        {
            return Service.IsValidThreeCode_OneDate(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                cInputField3, cpInput3, cOldItems3,
                cInputField4, cpInput4, cOldItems4);
        }
        public static bool IsValidTwoCode_TwoNumeric(string cInputTable, byte nStatus,
         string cInputField1, string cpInput1, string cOldItems1,
          string cInputField2, string cpInput2, string cOldItems2,
          string nInputField1, int npInput1, int nOldItems1,
           string nInputField2, int npInput2, int nOldItems2)
        {
            return Service.IsValidTwoCode_TwoNumeric(cInputTable, nStatus,
                cInputField1, cpInput1, cOldItems1,
                cInputField2, cpInput2, cOldItems2,
                nInputField1, npInput1, nOldItems1,
                nInputField2, npInput2, nOldItems2);
        }
        /// <summary>
        /// Đã tồn tại mã trong bảng => true
        /// [VPA_isExistOneCode_List]
        /// </summary>
        /// <returns></returns>
        public static bool IsExistOneCode_List(string cInputTableList, string cInputField, string cpInput)
        {
            return Service.IsExistOneCode_List(cInputTableList, cInputField, cpInput);
        }

        public static bool IsExistTwoCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2)
        {
            return Service.IsExistTwoCode_List(cInputTableList, cInputField1, cpInput1,
                cInputField2, cpInput2);
        }
        
        public static bool IsExistThreeCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2, string cInputField3, string cpInput3)
        {
            return Service.IsExistThreeCode_List(cInputTableList, cInputField1, cpInput1,
                cInputField2, cpInput2, cInputField3, cpInput3);
        }

        /// <summary>
        /// Kiểm tra trong bảng Altt có tên bảng trong Ma_dm hay không
        /// </summary>
        /// <param name="ma_dm"></param>
        /// <returns></returns>
        public static bool CheckAltt(string ma_dm)
        {
            return Service.CheckAltt(ma_dm);
        }

        public static bool CheckDataEsist(string tableName, SortedDictionary<string, object> data)
        {
            try
            {
                var where = SqlGenerator.GenSqlWhere(data);
                var sql = "select COUNT(*) count0 from [" + tableName + "] where " + where;
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql);
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra ngày nhập hợp lệ với ngày khóa sổ, ngày đầu kỳ, ngày cuối kỳ.
        /// </summary>
        /// <param name="maCt">Mã chứng từ để lấy ngay_ks_ky</param>
        /// <param name="date">Ngày nhập - không tính thời gian.</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool CheckNgayCt(string maCt, DateTime date, out string message)
        {
            var sql = "Select ngay_ks_ky from Alct Where ma_ct=@ma_ct";
            var ngay_ks_ky = ObjectAndString.ObjectToDate(SqlConnect.ExecuteScalar(CommandType.Text, sql, new SqlParameter("@ma_ct", maCt)));
            
            date = date.Date;
            var ngayks = ngay_ks_ky ?? V6Setting.M_Ngay_ks;
            var ngaydk = V6Setting.M_Ngay_dk;
            var ngayck = V6Setting.M_Ngay_ck;
            if (date > ngayks && date >= ngaydk && date <= ngayck)
            {
                message = "ok";
                return true;
            }
            message = string.Format(
                "Ngày nhập không hợp lệ.\r\nNgày khóa sổ: {0}. Ngày đầu kỳ: {1}. Ngày cuối kỳ: {2}.",
                ngayks.ToString("dd/MM/yyyy"),
                ngaydk.ToString("dd/MM/yyyy"),
                ngayck.ToString("dd/MM/yyyy")
                );
            return false;
        }

        #endregion check

        #region ==== CHANGE ====

        public static void ChangeAll_Id(string madm, string oldCode, string newCode)
        {
            Service.ChangeAll_Id(madm, oldCode, newCode);
        }

        /// <summary>
        /// Thay đổi mã khách hàng
        /// </summary>
        /// <param name="oldCode">Mã cũ</param>
        /// <param name="newCode">Mã mới</param>
        /// <returns></returns>
        public static void ChangeCustomeId(string oldCode, string newCode)
        {
            Service.ChangeCustomeId(oldCode, newCode);
        }
        public static void ChangeItemId(string oldCode, string newCode)
        {
            Service.ChangeItemId(oldCode, newCode);
        }
        public static void ChangeWarehouseId(string oldCode, string newCode)
        {
            Service.ChangeWarehouseId(oldCode, newCode);
        }

        public static void ChangeDepartmentId(string oldCode, string newCode)
        {
            Service.ChangeDepartmentId(oldCode, newCode);
        }
        public static void ChangeUnitId(string oldCode, string newCode)
        {
            Service.ChangeUnitId(oldCode, newCode);
        }
        public static void ChangeJobId(string oldCode, string newCode)
        {
            Service.ChangeJobId(oldCode, newCode);
        }

        public static void ChangeAccountId(string oldCode, string newCode)
        {
            Service.ChangeAccountId(oldCode, newCode);
        }
        #endregion change

        #region ==== CREATE ====

        public static V6InvoiceBase CreateInvoice(string maCt)
        {
            switch (maCt)
            {
                case "SOA": return new V6Invoice81();
                case "SOC": return new V6Invoice83();
            }
            return new V6InvoiceBase(maCt);
        }
        #endregion create

        #region ==== EXPORT ====

        public static bool ExportBackupInvoice(string mact, DateTime dateFrom, DateTime dateTo)
        {
            V6InvoiceBase invoice = new V6InvoiceBase(mact);
            return Service.ExportBackupInvoice(invoice.AM, invoice.AD, invoice.AD2);
        }

        /// <summary>
        /// Lọc dữ liệu và trả về bảng dữ liệu mới.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public static DataTable Filter(DataTable data, string filterString)
        {
            DataView dv = new DataView(data);
            dv.RowFilter = filterString;

            DataTable newData = dv.ToTable();
            return newData;
        }


        #endregion export

        #region ==== GENERATOR ====

        /// <summary>
        /// Hàm tạo câu group nhóm dùng cho sql.
        /// </summary>
        /// <param name="type">Mã nhóm sử dụng. vd: NH_VT</param>
        /// <param name="groups">Danh sách đầy đủ các nhóm. vd: 2,1,0,0,0,0</param>
        /// <returns></returns>
        public static string GenGroup(string type, params string[] groups)
        {
            type = type.ToUpper();
            var result = "";
            var dic = new SortedDictionary<string, string>();
            for (int i = 0; i < groups.Length; i++)
            {
                var groupName = type + (i+1);
                var groupSort = groups[i];
                if(groupSort != "0") dic[groupSort] = groupName;
            }
            foreach (KeyValuePair<string, string> item in dic)
            {
                result += "," + item.Value;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }
        public static string GenGroupList(string[] types, params string[] groups)
        {
           
            var result = "";
            var dic = new SortedDictionary<string, string>();
            for (int i = 0; i < groups.Length; i++)
            {
                var groupName = types[i];
                var groupSort = groups[i];
                if (groupSort != "0") dic[groupSort] = groupName;
            }
            foreach (KeyValuePair<string, string> item in dic)
            {
                result += "," + item.Value;
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }
        #endregion generator    

        /// <summary>
        /// Length
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int VFV_iFsize(string tableName, string fieldName)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@p1", tableName), 
                new SqlParameter("@p2", fieldName)
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text,  "select [dbo].[VFV_iFsize](@p1,@p2)", plist);
            return ObjectAndString.ObjectToInt(result);
        }

        
        /// <summary>
        /// Thực thi Procedure.
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="plist"></param>
        /// <returns></returns>
        public static DataSet ExecuteProcedure(string procName, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, procName, plist);
        }

        /// <summary>
        /// Thực thi Procedure với các tham số theo thứ tự không cần tên.
        /// </summary>
        /// <param name="procName">Tên Procedure</param>
        /// <param name="plist">Các tham số theo thứ tự</param>
        /// <returns></returns>
        public static DataSet ExecuteProcedure(string procName, params object[] plist)
        {
            return SqlConnect.ExecuteDataset(procName, plist);
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteScalar(CommandType.Text, sql, plist);
        }
        public static object ExecuteFunctionScalar(string funcName, params SqlParameter[] plist)
        {
            var ptext = (plist == null || plist.Length == 0) ? "" : plist[0].ParameterName;
            if(plist != null)
            for (int i = 1; i < plist.Length; i++)
            {
                ptext += "," + plist[i].ParameterName;
            }
            if (funcName.Length <= 4 || funcName.Substring(0,4).ToLower() != "dbo.")
            {
                funcName = "dbo." + funcName;
            }
            var sql = "Select " + funcName + "("+ptext+")";
            return SqlConnect.ExecuteScalar(CommandType.Text, sql, plist);
        }

        public static object ExecuteProcedureScalar(string procName, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteScalar(CommandType.StoredProcedure, procName, plist);
        }

        public static DataSet ExecuteProcedure(SqlTransaction tran, string procName, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteDataset(tran, CommandType.StoredProcedure, procName, plist);
        }

        public static int ExecuteProcedureNoneQuery(string procName, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, procName, plist);
        }
        public static int ExecuteProcedureNoneQuery(SqlTransaction tran, string procName, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteNonQuery(tran, CommandType.StoredProcedure, procName, plist);
        }

        public static DataSet ExecuteSqlDataset(string sql, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteDataset(CommandType.Text, sql, plist);
        }

        public static int ExecuteSqlNoneQuery(string sql, params SqlParameter[] plist)
        {
            return SqlConnect.ExecuteNonQuery(CommandType.Text, sql, plist);
        }
        
        public static bool Insert(V6TableName tableName, IDictionary<string, object> dataDictionary)
        {
            return Insert(tableName.ToString(), dataDictionary);
        }
        public static bool Insert(SqlTransaction transaction, V6TableName tableName, SortedDictionary<string, object> dataDictionary)
        {
            return Insert(transaction, tableName.ToString(), dataDictionary);
        }
        public static bool Insert(string tableName, IDictionary<string, object> dataDictionary)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            string sql = SqlGenerator.GenInsertSql(V6Login.UserId, tableName, structTable, dataDictionary);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result > 0;
        }

        public static bool Insert(SqlTransaction transaction, string tableName, SortedDictionary<string, object> dataDictionary)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            string sql = SqlGenerator.GenInsertSql(V6Login.UserId, tableName, structTable, dataDictionary);
            int result = SqlConnect.ExecuteNonQuery(transaction, CommandType.Text, sql);
            return result > 0;
        }

        public static int Update(V6TableName tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            return Update(tableName.ToString(), dataDictionary, keys);
        }
        public static int Update(string tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSql(V6Login.UserId, tableName, structTable, dataDictionary, keys);
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        public static int Update(SqlTransaction tran, string tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSql(V6Login.UserId, tableName, structTable, dataDictionary, keys);
            var result = SqlConnect.ExecuteNonQuery(tran, CommandType.Text, sql);
            return result;
        }

        public static int UpdateSimple(V6TableName tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            return UpdateSimple(tableName.ToString(), dataDictionary, keys);
        }

        /// <summary>
        /// Cập nhập dữ liệu vào bảng. Lưu ý data gửi với với key UPPER
        /// Simple: Không có các trường tự động DATE TIME và USER_ID
        /// </summary>
        /// <param name="tableName">Tên bảng dữ liệu cần cập nhập.</param>
        /// <param name="dataDictionary">Lưu ý key UPPER</param>
        /// <param name="keys">Lưu ý key UPPER</param>
        /// <returns></returns>
        public static int UpdateSimple(string tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSqlSimple(tableName, dataDictionary, keys, structTable);
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        public static int UpdateTable(string tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            SqlParameter[] plist2;
            var sql = SqlGenerator.GenUpdateSqlParameter(V6Login.UserId, tableName, structTable, dataDictionary, keys, out plist2);
            //List<SqlParameter> plist = new List<SqlParameter>();
            //foreach (KeyValuePair<string, object> item in dataDictionary)
            //{
            //    plist.Add(new SqlParameter("@" + item.Key, item.Value));
            //}
            var result = ExecuteSqlNoneQuery(sql, plist2);
            //var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql,);
            return result;
        }



        public static int Delete(V6TableName tableName, IDictionary<string, object> keys)
        {
            return Delete(tableName.ToString(), keys);
        }

        public static int Delete(string tableName, IDictionary<string, object> keys)
        {
            V6TableStruct structTable = GetTableStruct(tableName);
            string sql = SqlGenerator.GenDeleteSql(structTable, keys);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        public static DataTable GetAlct(string Mact)
        {
            var _alct = Select(V6TableName.Alct,
                new SortedDictionary<string, object> { { "ma_ct", Mact } },
                "", "", "").Data;
            return _alct;
        }
        public static DataTable GetAlctCt(string Mact)
        {
            var _alct = Select(V6TableName.Alctct,
                new SortedDictionary<string, object> { { "ma_ct", Mact }, {"User_id_ct", V6Login.UserId} },
                "", "", "").Data;
            return _alct;
        }

        public static string GetAMname(DataRow alct)
        {
            return alct["m_phdbf"].ToString().Trim();
        }
        public static string GetADname(DataRow alct)
        {
            return alct["m_ctdbf"].ToString().Trim();
        }

        public static V6TableStruct V6Struct1
        {
            get
            {
                if (_v6Struct1 == null || _v6Struct1.Count == 0)
                    GetV6Struct1();
                return _v6Struct1;
            }
        }

        private static V6TableStruct _v6Struct1;
        private static void GetV6Struct1()
        {
            var resultStruct = new V6TableStruct("v6struct1");
            try
            {
                string sql = "Select * from v6struct1 ";
                DataTable columnsStructInfo = SqlConnect.ExecuteDataset(CommandType.Text, sql)
                    .Tables[0];

                foreach (DataRow row in columnsStructInfo.Rows)
                {
                    var columnStruct = new V6ColumnStruct
                    {
                        ColumnName = row["FIELD"].ToString().Trim(),
                        ColumnWidth = Convert.ToInt32(row["WIDTH"])
                    };
                    
                    resultStruct.Add(columnStruct.ColumnName.ToUpper(), columnStruct);
                }
                _v6Struct1 = resultStruct;
            }
            catch
            {
                _v6Struct1 = new V6TableStruct();
            }
            _v6Struct1 = resultStruct;
        }
        /// <summary>
        /// Lấy lên cấu trúc bảng dữ liệu.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static V6TableStruct GetTableStruct(string tableName)
        {
            return Service.GetTableStruct(tableName);
        }

        public static bool StartSqlConnect(string key, string iniLocation)
        {
            return Service.StartSqlConnect(key, iniLocation);
            //return SqlConnect.StartSqlConnect(key, iniLocation);
        }
        
        
        

        

        

        public static decimal GetMaxValueTable(string pTablename, string pFieldname, string pKey)
        {

            SqlParameter[] prlist =
            {
                new SqlParameter("@table_name", pTablename),
                new SqlParameter("@field_name", pFieldname),
                new SqlParameter("@key", pKey)
            };
            var maxvalue = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_GetMaxValue", prlist);
            return ObjectAndString.ObjectToDecimal(maxvalue);
        }

        /// <summary>
        /// Lấy số chứng từ theo định dạng đã cài đặt.
        /// </summary>
        /// <param name="mode">M</param>
        /// <param name="voucherNo">''</param>
        /// <param name="mact">SOA</param>
        /// <param name="maDvcs">''</param>
        /// <param name="userId">116</param>
        /// <returns></returns>
        public static string GetSoCt(string mode, string voucherNo, string mact, string maDvcs, int userId)
        {
            return Service.GetSoCt(mode, voucherNo, mact, maDvcs, userId);
        }

        public static string GetNewSoCt(string masonb)
        {
            return Service.GetNewSoCt(masonb);
        }
        

        /// <summary>
        /// Lấy mã mới cho hóa đơn, chứng từ.
        /// </summary>
        /// <param name="pMaCt"></param>
        /// <returns></returns>
        public static string GetNewSttRec(string pMaCt)
        {
            var sttRec = Service.GetNewSttRec(pMaCt);
            return sttRec;
        }

        public static string GetNewLikeSttRec(string pMaCt, string pKhoa, string pLoai)
        {
            return Service.GetNewLikeSttRec(pMaCt, pKhoa, pLoai);
        }

        public static DateTime GetServerDateTime()
        {
            return Service.GetServerDateTime();
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys">where, null nếu không dùng</param>
        /// <param name="fields">các trường dữ liệu, để trống lấy hết</param>
        /// <param name="groupby"></param>
        /// <param name="orderby"></param>
        /// <param name="prList"></param>
        /// <returns></returns>
        public static V6SelectResult Select(V6TableName name, IDictionary<string, object> keys, string fields,
            string groupby = "", string orderby = "", params SqlParameter[] prList)
        {
            if (!V6Login.UserRight.AllowSelect(name)) return new V6SelectResult();

            string where = SqlGenerator.GenSqlWhere(keys);
            return Select(name.ToString(), fields, where, groupby, orderby, prList);
        }

        public static V6SelectResult Select(string name, IDictionary<string, object> keys, string fields,
            string groupby = "", string orderby = "", params SqlParameter[] prList)
        {
            if (!V6Login.UserRight.AllowSelect(name)) return new V6SelectResult();

            string where = SqlGenerator.GenSqlWhere(keys);
            return Select(name.ToString(), fields, where, groupby, orderby, prList);
        }

        public static V6SelectResult Select(string tableName, string fields = "*",
            string where = "", string groupby = "", string orderby = "", params SqlParameter[] prList)
        {
            if (!V6Login.UserRight.AllowSelect(V6TableHelper.ToV6TableName(tableName))) return new V6SelectResult();
            return SqlConnect.Select(tableName, fields, where, groupby, orderby, prList);
        }

        public static int SelectCount(string tableName, string field = "*",
            string where = "", params SqlParameter[] prList)
        {
            var whereClause = string.IsNullOrEmpty(where) ? "" : "where " + where;
            var sql = "Select Count("+field+") as Count from ["+tableName+"] " + whereClause;
            var count = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql, prList));
            return count;
        }

        public static DataTable SelectTable(string tableName)
        {
            return Service.SelectTable(tableName);
        }

        /// <summary>
        /// Làm tròn lên
        /// </summary>
        /// <param name="number">Giá trị vào</param>
        /// <param name="round">Số chữ số phần lẽ</param>
        /// <returns></returns>
        public static decimal Vround(decimal number, int round)
        {
            // Tuanmh 14/09/2017 (3866,5->3866)
            //return Math.Round(number, round, MidpointRounding.ToEven);
            return Math.Round(number, round, MidpointRounding.AwayFromZero);

        }

        //{ Tuanmh 21/08/2016
        public static string V6_DTOC(DateTime? pdatetime)
        {
            if (pdatetime == null) return "";
            var pformat = "dd/MM/yyyy";
            return ((DateTime)pdatetime).ToString(pformat);
        }
        public static string V6_DTOS(DateTime? pdatetime)
        {
            if (pdatetime == null) return "";
            var pformat = "yyyyMMdd";
            return ((DateTime)pdatetime).ToString(pformat);
        }
        //}

        /// <summary>
        /// Tạo chuỗi mã stt_rec0
        /// </summary>
        /// <param name="tableAD">Dữ liệu chi tiết của mã stt_rec đang có.</param>
        /// <param name="returnLenght">Độ dài chuỗi trả về.</param>
        /// <returns>vd: "00001"</returns>
        public static string GetNewSttRec0(DataTable tableAD, int returnLenght = 5)
        {
            var max = 0;
            var leftString = "00000";
            while (leftString.Length < returnLenght)
            {
                leftString += "0";
            }

            try
            {
                if (tableAD != null && tableAD.Columns.Contains("STT_REC0"))
                {
                    for (var j = 0; j < tableAD.Rows.Count; j++)
                    {
                        var m = Convert.ToInt32(tableAD.Rows[j]["STT_REC0"]);
                        if (m > max) max = m;

                    }
                }
                max++;
                return (leftString + max).Right(returnLenght);
            }
            catch
            {
                return leftString.Right(returnLenght);
            }
        }
        #region ==== GetDefaultExcelField ====
        public static string GetDefaultExcelField(V6TableName name)
        {
            string result;
            switch (name)
            {
                case V6TableName.Albp: result = "ma_bp"; break;
                case V6TableName.Albpcc: result = "ma_bp"; break;
                case V6TableName.Albpht: result = "ma_bpht"; break;
                case V6TableName.Albpts: result = "ma_bp"; break;
                case V6TableName.Alcc: result = "so_the_cc"; break;
                case V6TableName.Alck: result = "ma_ck"; break;
                case V6TableName.Alckm: result = "ma_ck"; break;
                case V6TableName.Alckmct: result = "ma_ck"; break;
                case V6TableName.Alcltg: result = "stt"; break;
                case V6TableName.Alct: result = "ma_ct"; break;
                case V6TableName.Alctct: result = "ma_ct"; break;
                case V6TableName.Alcthd: result = "ma_hd"; break;
                case V6TableName.Aldmpbct: result = "stt"; break;
                case V6TableName.Aldmpbph: result = "ma_bpht"; break;
                case V6TableName.Aldmvt: result = "ma_bpht"; break;
                case V6TableName.Aldmvtct: result = "ma_bpht"; break;
                case V6TableName.Aldvcs: result = "ma_dvcs"; break;
                case V6TableName.Aldvt: result = "dvt"; break;
                case V6TableName.Algia: result = "ma_gia"; break;
                case V6TableName.Algia2: result = "ma_gia"; break;
                case V6TableName.Algia200: result = "ma_gia"; break;
                case V6TableName.Algiavon: result = "ma_gia"; break;
                case V6TableName.Algiavon3: result = "ma_kho"; break;
                case V6TableName.Algiavv: result = "ma_vv"; break;
                case V6TableName.Alhd: result = "ma_hd"; break;
                case V6TableName.Alhttt: result = "ma_httt"; break;
                case V6TableName.Alhtvc: result = "ma_htvc"; break;
                case V6TableName.Alkc: result = "STT"; break;
                case V6TableName.Alkh: result = "ma_kh"; break;
                case V6TableName.Alkho: result = "ma_kho"; break;
                case V6TableName.Alkhtg: result = "ma_khtg"; break;
                case V6TableName.Alkmb: result = "ma_km"; break;
                case V6TableName.Alkmbct: result = "ma_dvcs"; break;
                case V6TableName.Alkmm: result = "ma_km"; break;
                case V6TableName.Alkmmct: result = "ma_dvcs"; break;
                case V6TableName.Alku: result = "ma_ku"; break;
                case V6TableName.Allnx: result = "ma_lnx"; break;
                case V6TableName.Allo: result = "ma_vt"; break;
                case V6TableName.Alloaicc: result = "loai_cc0"; break;
                case V6TableName.Alloaick: result = "ma_loai"; break;
                case V6TableName.Alloaivc: result = "ma_loai"; break;
                case V6TableName.Alloaivt: result = "loai_vt"; break;
                case V6TableName.Almagd: result = "ma_ct_me"; break;
                case V6TableName.Almagia: result = "ma_gia"; break;
                case V6TableName.Almauhd: result = "ma_mauhd"; break;
                case V6TableName.Alnhcc: result = "ma_nh"; break;
                case V6TableName.Alnhdvcs: result = "ma_nh"; break;
                case V6TableName.Alnhhd: result = "ma_nh"; break;
                case V6TableName.Alnhkh: result = "ma_nh"; break;
                case V6TableName.Alnhkh2: result = "ma_nh"; break;
                case V6TableName.Alnhku: result = "ma_nh"; break;
                case V6TableName.Alnhphi: result = "ma_nh"; break;
                case V6TableName.Alnhtk: result = "ma_nh"; break;
                case V6TableName.Alnhtk0: result = "ma_nh"; break;
                case V6TableName.Alnhts: result = "ma_nh"; break;
                case V6TableName.Alnhvt: result = "ma_nh"; break;
                case V6TableName.Alnhvv: result = "ma_nh"; break;
                case V6TableName.Alnhytcp: result = "nhom"; break;
                case V6TableName.Alnk: result = "ma_nk"; break;
                case V6TableName.Alnt: result = "ma_nt"; break;
                case V6TableName.Alnv: result = "ma_nv"; break;
                case V6TableName.Alnvien: result = "ma_nvien"; break;
                case V6TableName.Alpb: result = "stt_rec"; break;
                case V6TableName.Alpb1: result = "ma_bp"; break;
                case V6TableName.Alphi: result = "ma_phi"; break;
                case V6TableName.Alphuong: result = "ma_phuong"; break;
                case V6TableName.Alplcc: result = "ma_loai"; break;
                case V6TableName.Alplts: result = "ma_loai"; break;
                case V6TableName.Alqddvt: result = "ma_vt"; break;
                case V6TableName.Alqg: result = "ma_qg"; break;
                case V6TableName.Alql: result = "nam"; break;
                case V6TableName.Alquan: result = "ma_quan"; break;
                case V6TableName.Alstt: result = "stt_rec"; break;
                case V6TableName.Altd: result = "ma_td"; break;
                case V6TableName.Altd2: result = "ma_td2"; break;
                case V6TableName.Altd3: result = "ma_td3"; break;
                case V6TableName.Altgcc: result = "ma_tg_cc"; break;
                case V6TableName.Altgnt: result = "ma_nt"; break;
                case V6TableName.Altgts: result = "ma_tg_ts"; break;
                case V6TableName.Althau: result = "ma_thau"; break;
                case V6TableName.Althauct: result = "ma_dvcs"; break;
                case V6TableName.Althue: result = "ma_thue"; break;
                case V6TableName.Altinh: result = "ma_tinh"; break;
                case V6TableName.Altk0: result = "tk"; break;
                case V6TableName.Altk1: result = "tk"; break;
                case V6TableName.Altk2: result = "tk2"; break;
                case V6TableName.Altklkku: result = "tk_lkku"; break;
                case V6TableName.Altklkvv: result = "tk_lkvv"; break;
                case V6TableName.Altknh: result = "tknh"; break;
                case V6TableName.Alts: result = "so_the_ts"; break;
                case V6TableName.Altt: result = "ma_dm"; break;
                case V6TableName.Alttvt: result = "tt_vt"; break;
                case V6TableName.Alvc: result = "ma_vc"; break;
                case V6TableName.Alvitri: result = "ma_vitri"; break;
                case V6TableName.Alvt: result = "ma_vt"; break;
                case V6TableName.Alvttg: result = "ma_vttg"; break;
                case V6TableName.Alvv: result = "ma_vv"; break;
                case V6TableName.Alytcp: result = "ma_ytcp"; break;

                case V6TableName.V6option: result = "name"; break;
                case V6TableName.V6soft: result = "name"; break;
                case V6TableName.V6user: result = "user_id"; break;
                case V6TableName.Alct1: result = "ma_ct"; break;
                case V6TableName.V6menu: result = "itemid"; break;

                case V6TableName.Abkh: result = "nam"; break;
                case V6TableName.Ablo: result = "nam"; break;
                case V6TableName.Abtk: result = "nam"; break;
                case V6TableName.Abvt: result = "nam"; break;

                case V6TableName.Althue30: result = "ma_thue"; break;
                case V6TableName.Alsonb: result = "ma_sonb"; break;

                default:
                    result = "";
                    break;
            }
            return result;
        }
        #endregion GetDefaultExcelField

        
        /// <summary>
        /// Tính tổng giá trị số trong một cột dữ liệu.
        /// </summary>
        /// <param name="data">Bảng dữ liệu</param>
        /// <param name="colName">Cột tính tổng</param>
        /// <param name="throwException">Quăng lỗi nếu tham số không hợp lệ.</param>
        /// <returns></returns>
        public static decimal TinhTong(DataTable data, string colName, bool throwException = false)
        {
            if (throwException)
            {
                if (data == null) throw new ArgumentException("DataTable is null.", "data");
                if (!data.Columns.Contains(colName))
                    throw new IndexOutOfRangeException(string.Format("No column name: \"{0}\".", colName));
            }
            var total = 0m;
            try
            {
                if (data != null && data.Columns.Contains(colName))
                {
                    for (var j = 0; j < data.Rows.Count; j++)
                    {
                        total += ObjectAndString.ObjectToDecimal(data.Rows[j][colName]);
                    }
                    return total;
                }
                return total;
            }
            catch
            {
                return total;
            }
        }

        public static decimal TinhTongOper(DataTable data, string colName, string oper_column)
        {
            var total = 0m;
            if (string.IsNullOrEmpty(oper_column) || data == null || !data.Columns.Contains(oper_column))
            {
                return total;
            }

            try
            {
                if (data.Columns.Contains(colName))
                {
                    for (var i = 0; i < data.Rows.Count; i++)
                    {
                        var row = data.Rows[i];
                        var oper = row[oper_column].ToString().Trim();
                        if(oper == "+")
                            total += Convert.ToDecimal(data.Rows[i][colName]);
                        else if(oper == "-")
                            total -= Convert.ToDecimal(data.Rows[i][colName]);
                    }
                    return total;
                }
                return total;
            }
            catch
            {
                return total;
            }

        }


        public static string GetChuoiMaHoa(string s)
        {
            string result = UtilityHelper.EnCrypt(s);
            return result;
        }

        public static bool ReflectiveEquals(object first, object second)
        {
            if (first == null && second == null)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }
            Type firstType = first.GetType();
            if (second.GetType() != firstType)
            {
                return false; // Or throw an exception
            }
            // This will only use public properties. Is that enough?
            foreach (PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(first, null);
                    object secondValue = propertyInfo.GetValue(second, null);
                    if (!Equals(firstValue, secondValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void GetFormatGridView(string codeform, string type, out string fieldv, out string operv, out object value, out string boldYn, out string colorYn, out string colorv)
        {
            SqlParameter[] prlist =
            {
                new SqlParameter("@Codeform", codeform),
                new SqlParameter("@Type", type)
            };

            //FIELDV,VALUEV,BOLD_YN,COLOR_YN,COLORV
            var result = SqlConnect.ExecuteDataset(
                CommandType.StoredProcedure,
                "VPA_GetFormatGridView",
                prlist)
                .Tables[0].Rows[0];

            fieldv = result["FIELDV"].ToString().Trim();
            operv = result["OPERV"].ToString().Trim();
            value = result["VALUEV"];
            boldYn = result["BOLD_YN"].ToString().Trim();
            colorYn = result["COLOR_YN"].ToString().Trim();
            colorv = result["COLORV"].ToString().Trim();
        }

        public static DataTable GetDefaultValueData(int loai, string ma_ct, string ma_dm, string itemId, string advance)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@loai",loai),
                new SqlParameter("@ma_ct",ma_ct),
                new SqlParameter("@ma_dm",ma_dm),
                new SqlParameter("@user_id",V6Login.UserId),
                new SqlParameter("@itemid",itemId),
                new SqlParameter("@advance",advance),
            };
            var data = ExecuteProcedure("VPA_GetDefaultvalue", plist).Tables[0];
            return data;
        }

        public static DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return Service.GetLoDate(mavt, makho, sttRec, ngayct);
        }
        
        public static DataTable GetViTri(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return Service.GetViTri(mavt, makho, sttRec, ngayct);
        }
        
        public static DataTable GetViTriLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return Service.GetViTriLoDate(mavt, makho, sttRec, ngayct);
        }

        public static DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            return Service.GetLoDate13(mavt, makho, malo, sttRec, ngayct);
        }
        public static DataTable GetLoDateAll(string mavt_in, string makho_in, string malo_in, string sttRec, DateTime ngayct)
        {
            return Service.GetLoDateAll(mavt_in, makho_in, malo_in, sttRec, ngayct);
        }
        
        public static DataTable GetViTri13(string mavt, string makho, string mavitri, string sttRec, DateTime ngayct)
        {
            return Service.GetViTri13(mavt, makho, mavitri, sttRec, ngayct);
        }
        
        public static DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct)
        {
            return Service.GetViTriLoDate13(mavt, makho, malo, mavitri, sttRec, ngayct);
        }
        public static DataTable GetViTriLoDateAll(string mavt_in, string makho_in, string malo_in, string mavitri_in, string sttRec, DateTime ngayct)
        {
            return Service.GetViTriLoDateAll(mavt_in, makho_in, malo_in, mavitri_in, sttRec, ngayct);
        }

        public static DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return Service.GetStock(mact, mavt, makho, sttRec, ngayct);
        }
        
        public static DataTable GetStockAll(string mact, string mavt_in, string makho_in, string sttRec, DateTime ngayct)
        {
            return Service.GetStockAll(mact, mavt_in, makho_in, sttRec, ngayct);
        }

        /// <summary>
        /// Đọc tiền thành chữ.
        /// </summary>
        /// <param name="money">Số tiền cần đọc</param>
        /// <param name="lang">V hoặc E</param>
        /// <param name="ma_nt"></param>
        /// <returns></returns>
        public static string MoneyToWords(decimal money, string lang, string ma_nt)
        {
            if (lang == "V")
            {
                return DocSo.DOI_SO_CHU_NEW(money, V6Alnt.begin1(ma_nt), V6Alnt.end1(ma_nt), V6Alnt.only1(ma_nt), V6Alnt.point1(ma_nt),
                    V6Alnt.endpoint1(ma_nt));
            }
            else
            {
                return DocSo.NumWordsWrapper(money, V6Alnt.begin2(ma_nt), V6Alnt.end2(ma_nt), V6Alnt.only2(ma_nt), V6Alnt.point2(ma_nt),
                    V6Alnt.endpoint2(ma_nt));
            }
        }

        /// <summary>
        /// Đọc tên máy in từ file oldPrinter
        /// </summary>
        /// <returns></returns>
        public static string ReadOldSelectPrinter()
        {
            string s = "";
            if (File.Exists("oldPrinter"))
            {

                FileStream fs = new FileStream("oldPrinter", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                try
                {
                    s = sr.ReadLine();
                    sr.Close(); fs.Close();
                }
                catch (Exception)
                {
                    try
                    {
                        sr.Close();
                    }
                    catch
                    {
                        // ignored
                    }
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            else
            {
                s = "";
            }
            return s;
        }
        /// <summary>
        /// Ghi tên máy in xuống file oldPrinter
        /// </summary>
        /// <param name="name"></param>
        public static void WriteOldSelectPrinter(string name)
        {
            FileStream fs = new FileStream("oldPrinter", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(name);
                sw.Close(); fs.Close();
            }
            catch (Exception)
            {
                try
                {
                    sw.Close();
                }
                catch
                {
                    // ignored
                }
                try
                {
                    fs.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Kiểm tra hóa đơn đã thanh toán, ko được sửa hoặc xóa (check tồn tại)
        /// </summary>
        /// <param name="sttRec"></param>
        /// <param name="tableName"></param>
        /// <returns>0:được sửa, 1: không được sửa</returns>
        public static int CheckEditVoucher(string sttRec, string tableName)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@tablename", tableName), 
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text,  "Select dbo.VFA_IsEditVoucher (@stt_rec, @tablename)", plist);
            return ObjectAndString.ObjectToInt(result);
        }
        
        public static int CheckEditVoucher_SOR(string sttRecPt, string tableName)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec_pt", sttRecPt), 
                new SqlParameter("@tablename", tableName), 
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text,  "Select dbo.VFA_IsEditVoucher_SOR (@stt_rec_pt, @tablename)", plist);
            return ObjectAndString.ObjectToInt(result);
        }

        /// <summary>
        /// Sửa 1 dòng dữ liệu trong DataTable nếu tìm thấy keyValue trong cột keyField.
        /// Nếu không tìm thấy sẽ thêm vào dòng mới.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <param name="data"></param>
        public static void UpdateRowToDataTable(DataTable table, string keyField, string keyValue, IDictionary<string, object> data)
        {
            //Find row
            DataRow findRow = null;
            foreach (DataRow row in table.Rows)
            {
                if (row[keyField].ToString().Trim() == keyValue)
                {
                    findRow = row;
                    break;
                }
            }
            if (findRow != null)
            {
                //var currentRow = AD.Rows[cIndex];
                foreach (DataColumn column in table.Columns)
                {
                    var key = column.ColumnName.ToUpper();
                    if (data.ContainsKey(key))
                    {
                        object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                        findRow[key] = value;
                    }
                }
            }
            else
            {
                AddRowToDataTable(table, data);
            }
        }

        public static void AddRowToDataTable(DataTable table, IDictionary<string, object> data)
        {
            //Tạo dòng dữ liệu mới.
            var newRow = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                var KEY = column.ColumnName.ToUpper();
                object value = ObjectAndString.ObjectTo(column.DataType,
                    data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                newRow[KEY] = value;
            }
            table.Rows.Add(newRow);
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của table hoặc view trong csdl.
        /// </summary>
        /// <param name="tableView">Tên bảng hoặc view.</param>
        /// <returns></returns>
        public static bool IsExistDatabaseTable(string tableView)
        {
            try
            {
                //SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@name
                //var i = ExecuteScalar("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@name", new[] { new SqlParameter("@name", tableView) });
                //return ObjectAndString.ObjectToBool(i);

                var result = ExecuteProcedureScalar("V6TOOLS_ISEXISTTABLE", new[] {new SqlParameter("@cInputTable_View", tableView)});
                return ObjectAndString.ObjectToBool(result);
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
    }
}
