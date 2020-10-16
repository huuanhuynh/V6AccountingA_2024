using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using V6AccountingBusiness.Invoices;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;

namespace V6AccountingBusiness
{
    public static class V6BusinessHelper
    {
        
        #region ==== CHANGE ====

        public static void ChangeAll_Id(string madm, string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                // Tuanmh 29/05/2016
                //@ma_dm varchar(64),
                //@old_value varchar(64),
                //@new_value varchar(64)

                new SqlParameter("@ma_dm", madm),
                new SqlParameter("@old_value", oldCode),
                new SqlParameter("@new_value", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_ChangeListId", plist);
        }

        /// <summary>
        /// Thay đổi mã khách hàng
        /// </summary>
        /// <param name="oldCode">Mã cũ</param>
        /// <param name="newCode">Mã mới</param>
        /// <returns></returns>
        public static void ChangeCustomeId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coCust", oldCode),
                new SqlParameter("@cnCust", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditCustId", plist);
        }
        public static void ChangeItemId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coItem", oldCode),
                new SqlParameter("@cnItem", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditItemId", plist);
        }
        public static void ChangeWarehouseId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coWh", oldCode),
                new SqlParameter("@cnWh", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditWhId", plist);
        }

        public static void ChangeDepartmentId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coCode", oldCode),
                new SqlParameter("@cnCode", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditDepartmentId", plist);
        }
        public static void ChangeUnitId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coWh", oldCode),
                new SqlParameter("@cnWh", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditUnitId", plist);
        }
        public static void ChangeJobId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coJob", oldCode),
                new SqlParameter("@cnJob", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditJobId", plist);
        }

        public static void ChangeAccountId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coAcc", oldCode),
                new SqlParameter("@cnAcc", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditAccId", plist);
        }
        #endregion change

        #region ==== CHECK ====

        /// <summary>
        /// Kiểm tra mã tồn tại.
        /// </summary>
        /// <param name="madm"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AllCheckExist(string madm, string value)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_dm", madm), 
                new SqlParameter("@value", value)
            };
            var result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_CheckAllListId", plist);
            return ObjectAndString.ObjectToInt(result) == 1;
        }

        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// VPA_isValidOneCode_Full
        /// </summary>
        /// <returns></returns>
        public static bool IsValidOneCode_Full(string cInputTable, byte nStatus,
            string cInputField, string cpInput, string cOldItems)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField", cInputField),
                new SqlParameter("@cpInput", cpInput),
                new SqlParameter("@cOldItems", cOldItems)
            };

            object result = 0;
            var aldmconfig = SqlConnect.Select("ALDM", "", "MA_DM='" + cInputTable + "'").Data;

            if (aldmconfig.Rows.Count == 1 && aldmconfig.Rows[0]["CHECK_LONG"].ToString() == "1")
            {
                result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode", plist);
            }
            else
            {
                result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode_Full", plist);
            }

            if (result != null && Convert.ToInt32(result) == 1) return true;
            return false;
        }

        public static bool IsValidTwoCode_Full(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2)
            };
            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_Full", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
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
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public static bool IsValidThreeCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        
        public static bool IsValidFourCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string cInputField4, string cpInput4, string cOldItems4,
            string nInputField1, int npInput1, int nOldItems1)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidFourCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
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
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@cInputField5", cInputField5),
                new SqlParameter("@nInputField1", nInputField1),
                 new SqlParameter("@nInputField2", nInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@cpInput5", cpInput5),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@npInput2", npInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@cpInput5Old", cOldItems5),
                new SqlParameter("@npInput1Old", nOldItems1),
                new SqlParameter("@npInput2Old", nOldItems2)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidFiveCode_TwoNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public static bool IsValidTwoCode_OneNumeric(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public static bool IsValidOneCode_OneDate(string cInputTable, byte nStatus,
           string cInputField1, string nInputField1, string cpInput1,
            string npInput1, string cpInput1Old, string npInput1Old)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cpInput1Old),
                new SqlParameter("@npInput1Old", npInput1Old)
            };
            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public static bool IsValidTwoCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", cInputField3),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", cpInput3),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", cOldItems3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public static bool IsValidThreeCode_OneDate(string cInputTable, byte nStatus,
         string cInputField1, string cpInput1, string cOldItems1,
          string cInputField2, string cpInput2, string cOldItems2,
          string cInputField3, string cpInput3, string cOldItems3,
          string cInputField4, string cpInput4, string cOldItems4)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@nInputField1", cInputField4),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@npInput1", cpInput4),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@npInput1Old", cOldItems4)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public static bool IsValidTwoCode_TwoNumeric(string cInputTable, byte nStatus,
         string cInputField1, string cpInput1, string cOldItems1,
          string cInputField2, string cpInput2, string cOldItems2,
          string nInputField1, int npInput1, int nOldItems1,
           string nInputField2, int npInput2, int nOldItems2)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@nInputField2", nInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@npInput2", npInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", nOldItems1),
                new SqlParameter("@npInput2Old", nOldItems2)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_TwoNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        /// <summary>
        /// Đã tồn tại mã trong bảng => true
        /// [VPA_isExistOneCode_List]
        /// </summary>
        /// <returns></returns>
        public static bool IsExistOneCode_List(string cInputTableList, string cInputField, string cpInput)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField", cInputField),
                new SqlParameter("@cpInput", cpInput)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistOneCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }

        public static bool IsExistTwoCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cInputField2", cInputField1),
                new SqlParameter("@cpInput2", cpInput1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistTwoCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }
        
        public static bool IsExistThreeCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2, string cInputField3, string cpInput3)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cInputField2", cInputField1),
                new SqlParameter("@cpInput2", cpInput1),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cpInput3", cpInput3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistThreeCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }

        /// <summary>
        /// Kiểm tra trong bảng Altt có tên bảng trong Ma_dm hay không
        /// </summary>
        /// <param name="ma_dm"></param>
        /// <returns></returns>
        public static bool CheckAltt(string ma_dm)
        {
            try
            {
                var sql = "select COUNT(ma_dm) count0 from altt where ma_dm=@ma_dm";
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql, new SqlParameter("@ma_dm", ma_dm));
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu tồn tại trong bảng. Data cần đúng trường, đúng kiểu.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckDataExist(string tableName, IDictionary<string, object> data)
        {
            return CheckDataExist(tableName, data, null);
        }
        /// <summary>
        /// Kiểm tra dữ liệu tồn tại trong bảng. Data cần đúng trường, đúng kiểu.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool CheckDataExist(string tableName, IDictionary<string, object> data, string filter)
        {
            try
            {
                var where = SqlGenerator.GenSqlWhere(data);
                var sql = "select COUNT(*) count0 from [" + tableName + "] where " + where;
                if (!string.IsNullOrEmpty(filter)) sql += " and " + filter;
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql);
                return result >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu có tồn tại hay không, dữ liệu dư được bỏ qua
        /// </summary>
        /// <param name="tableName">Bảng dữ liệu để kiểm tra</param>
        /// <param name="data">Dữ liệu được kiểm tra</param>
        /// <param name="check_long">Check lồng</param>
        /// <returns></returns>
        public static bool CheckDataExistStruct(string tableName, IDictionary<string, object> data, bool check_long = false)
        {
            try
            {
                string where = "";
                if (check_long)
                {
                    where = SqlGenerator.GenWhere_CheckLong(GetTableStruct(tableName), data);
                }
                else
                {
                    where = SqlGenerator.GenWhere(GetTableStruct(tableName), data);
                }
                var sql = "select COUNT(*) count0 from [" + tableName + "] where " + where;
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql);
                return result >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu có tồn tại hay không ngoại trừ data_not, dữ liệu dư được bỏ qua
        /// </summary>
        /// <param name="tableName">Bảng dữ liệu để kiểm tra</param>
        /// <param name="data">Dữ liệu được kiểm tra</param>
        /// <param name="data_not">Dữ liệu loại trừ</param>
        /// <param name="check_long">Check lồng</param>
        /// <returns></returns>
        public static bool CheckDataExistNotStruct(string tableName, IDictionary<string, object> data, IDictionary<string, object> data_not, bool check_long = false)
        {
            try
            {
                string where = "";
                string where_not = "";
                V6TableStruct tableStruct = GetTableStruct(tableName);
                if (check_long)
                {
                    where = SqlGenerator.GenWhere_CheckLong(tableStruct, data);
                }
                else
                {
                    where = SqlGenerator.GenWhere(tableStruct, data);
                }
                where_not = SqlGenerator.GenWhere2_oper(tableStruct, data_not, "<>");

                var sql = string.Format("select COUNT(*) count0 from [{0}] where ({1}) and ({2})", tableName, @where, where_not);
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql);
                return result >= 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra khóa số liệu. Nếu có khóa trả về 1, nếu không khóa trả về 0
        /// </summary>
        /// <param name="type">1: dùng tham số date. 2: dùng month year. 3: year</param>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int CheckDataLocked(string type, DateTime date, int month, int year)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", type),
                new SqlParameter("@Date", date.Date),
                new SqlParameter("@Month", month),
                new SqlParameter("@Year", year),
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text, "Select dbo.VFA_CheckDataLocked (@Type, @Date, @Month, @Year)", plist);
            return ObjectAndString.ObjectToInt(result);
        }

        /// <summary>
        /// Kiểm tra MST đúng định dạng.
        /// </summary>
        /// <param name="strTaxCode"></param>
        /// <returns></returns>
        public static bool CheckMST(string strTaxCode)
        {
            if (strTaxCode.Trim().Length < 10) return false;

            strTaxCode = strTaxCode.Substring(0, 10);
            
            int[] iCheck = {31, 29, 23, 19, 17, 13, 7, 5, 3};
            int ChkNumber = 0;
            for (int i = 0; i < 9; i++)
            {
                if(!char.IsNumber(strTaxCode[i]))
                {
                    return false;
                }
                ChkNumber += ObjectAndString.ObjectToInt(strTaxCode.Substring(i, 1))*iCheck[i];
            }

            return
                ObjectAndString.ObjectToInt(strTaxCode.Substring(9, 1)) == 10 - ChkNumber%11;
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
                V6Text.Text("NgayNhap0KDC012"),//"Ngày nhập không hợp lệ.\r\nNgày khóa sổ: {0}. Ngày đầu kỳ: {1}. Ngày cuối kỳ: {2}."
                ngayks.ToString("dd/MM/yyyy"),
                ngaydk.ToString("dd/MM/yyyy"),
                ngayck.ToString("dd/MM/yyyy")
                );
            return false;
        }

        /// <summary>
        /// Kiểm tra quyền nhấn phím.
        /// </summary>
        /// <param name="ma_ct"></param>
        /// <param name="key"></param>
        /// <param name="ma_dm"></param>
        /// <returns></returns>
        public static bool CheckRightKey(string ma_ct, string key, string ma_dm)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@ma_ct", ma_ct),
                new SqlParameter("@key", key),
                new SqlParameter("@ma_dm", ma_dm),
                new SqlParameter("@user_id", V6Login.UserId),
            };
            object result = ExecuteProcedureScalar("VPA_Check_Right_KEY_ALCTCT", plist);
            return ObjectAndString.ObjectToBool(result);
        }

        /// <summary>
        /// Kiểm tra công thức hợp lệ.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldFilter"></param>
        /// <param name="fieldFormula"></param>
        /// <param name="fieldCode"></param>
        /// <param name="filterValue"></param>
        /// <param name="formulaValue"></param>
        /// <returns></returns>
        public static bool CheckValidFormula(string tableName, string fieldFilter, string fieldFormula, string fieldCode, string filterValue, string formulaValue)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@fFilter", fieldFilter),
                new SqlParameter("@fFormula", fieldFormula),
                new SqlParameter("@fCode", fieldCode),
                new SqlParameter("@FilterValue", filterValue),
                new SqlParameter("@FormulaValue", formulaValue)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_CheckValid_Formula", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;
            return false;
        }

        public static bool IsValidEightCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3,
           string cInputField4, string cpInput4, string cOldItems4,
           string cInputField5, string cpInput5, string cOldItems5,
           string cInputField6, string cpInput6, string cOldItems6,
           string cInputField7, string cpInput7, string cOldItems7,
           string cInputField8, string cpInput8, string cOldItems8,
           string cInputField9, string cpInput9, string cOldItems9
            )
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@cInputField5", cInputField5),
                new SqlParameter("@cInputField6", cInputField6),
                new SqlParameter("@cInputField7", cInputField7),
                new SqlParameter("@cInputField8", cInputField8),
                new SqlParameter("@nInputField1", cInputField9),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@cpInput5", cpInput5),
                new SqlParameter("@cpInput6", cpInput6),
                new SqlParameter("@cpInput7", cpInput7),
                new SqlParameter("@cpInput8", cpInput8),
                new SqlParameter("@npInput1", cpInput9),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@cpInput5Old", cOldItems5),
                new SqlParameter("@cpInput6Old", cOldItems6),
                new SqlParameter("@cpInput7Old", cOldItems7),
                new SqlParameter("@cpInput8Old", cOldItems8),
                new SqlParameter("@npInput1Old", cOldItems9)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidEightCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        #endregion check

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
            
            try
            {
                var data = new DataTable("Name");
                var saveAs = "path\\" + invoice.AM_TableName + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                V6Tools.V6Export.ExportData.ToExcel(data, saveAs, "title", true);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Business ExportBackupInvoice", ex, "");
                return false;
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="plist"></param>
        /// <returns></returns>
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
            CheckIdentifier(procName);
            return SqlConnect.ExecuteScalar(CommandType.StoredProcedure, procName, plist);
        }

        public static DataSet ExecuteProcedure(SqlTransaction tran, string procName, params SqlParameter[] plist)
        {
            CheckIdentifier(procName);
            return SqlConnect.ExecuteDataset(tran, CommandType.StoredProcedure, procName, plist);
        }

        public static int ExecuteProcedureNoneQuery(string procName, params SqlParameter[] plist)
        {
            CheckIdentifier(procName);
            return SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, procName, plist);
        }
        public static int ExecuteProcedureNoneQuery(SqlTransaction tran, string procName, params SqlParameter[] plist)
        {
            CheckIdentifier(procName);
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
        public static bool Insert(SqlTransaction transaction, V6TableName tableName, IDictionary<string, object> dataDictionary)
        {
            return Insert(transaction, tableName.ToString(), dataDictionary);
        }
        public static bool Insert(string tableName, IDictionary<string, object> dataDictionary)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            string sql = SqlGenerator.GenInsertSql(V6Login.UserId, tableName, tableStruct, dataDictionary);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result > 0;
        }

        public static bool Insert(SqlTransaction transaction, string tableName, IDictionary<string, object> dataDictionary)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            string sql = SqlGenerator.GenInsertSql(V6Login.UserId, tableName, tableStruct, dataDictionary);
            int result = SqlConnect.ExecuteNonQuery(transaction, CommandType.Text, sql);
            return result > 0;
        }

        public static bool InsertSimple(string tableName, IDictionary<string, object> dataDictionary)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            string sql = SqlGenerator.GenInsertSqlSimple(V6Login.UserId, tableName, tableStruct, dataDictionary);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result > 0;
        }

        public static int Update(V6TableName tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            return Update(tableName.ToString(), dataDictionary, keys);
        }
        public static int Update(string tableName, IDictionary<string, object> dataDictionary,
            IDictionary<string, object> keys)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSql(V6Login.UserId, tableName, dataDictionary, keys, tableStruct);
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        public static int Update(SqlTransaction tran, string tableName, SortedDictionary<string, object> dataDictionary,
            SortedDictionary<string, object> keys)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            var sql = SqlGenerator.GenUpdateSql(V6Login.UserId, tableName, dataDictionary, keys, tableStruct);
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
        /// <returns>-2:No columns</returns>
        public static int UpdateSimple(string tableName, IDictionary<string, object> dataDictionary, IDictionary<string, object> keys)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            string sql = "";
            try
            {
                sql = SqlGenerator.GenUpdateSqlSimple(V6Login.UserId, tableName, tableStruct, dataDictionary, keys);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No columns!") return -2;
                throw;
            }
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        /// <summary>
        /// Update table parameters.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataDictionary"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static int UpdateTable(string tableName, SortedDictionary<string, object> dataDictionary, SortedDictionary<string, object> keys)
        {
            V6TableStruct tableStruct = GetTableStruct(tableName);
            SqlParameter[] plist2;
            var sql = SqlGenerator.GenUpdateSqlParameter(V6Login.UserId, tableName, tableStruct, dataDictionary, keys, out plist2);
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
            V6TableStruct tableStruct = GetTableStruct(tableName);
            string sql = SqlGenerator.GenDeleteSql(tableStruct, keys);
            int result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }

        public static DataTable GetAlct(string Mact)
        {
            var _alct = Select("ALCT", new SortedDictionary<string, object> { { "ma_ct", Mact } }, "").Data;
            return _alct;
        }
        /// <summary>
        /// Lấy thông tin Alctct (lọc theo Mact và user_id).
        /// </summary>
        /// <param name="Mact"></param>
        /// <returns></returns>
        public static DataTable GetAlctCt(string Mact)
        {
            var _alct = Select("ALCTCT",
                new SortedDictionary<string, object> { { "ma_ct", Mact }, {"User_id_ct", V6Login.UserId} }, "").Data;
            return _alct;
        }
        public static DataTable GetAlctCt_TableName(string tableName)
        {
            var _alct = Select("ALCTCT",
                new SortedDictionary<string, object> { { "TABLE_NAME", tableName }, {"User_id_ct", V6Login.UserId} }, "").Data;
            return _alct;
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
            return V6SqlconnectHelper.GetTableStruct(tableName);
        }

        public static decimal GetMaxValueTable(string pTablename, string pFieldname, string pKey)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@table_name", pTablename),
                new SqlParameter("@field_name", pFieldname),
                new SqlParameter("@key", pKey)
            };
            var maxvalue = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_GetMaxValue", plist);
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
            SqlParameter[] plist =
            {
                new SqlParameter("@Mode_VC", mode),
                new SqlParameter("@cVoucherNo", voucherNo),
                new SqlParameter("@cma_ct", mact),
                new SqlParameter("@cma_dvcs", maDvcs),
                new SqlParameter("@User_id", userId)
            };
            var result = SqlConnect.ExecuteScalar(
                CommandType.StoredProcedure,
                "VPA_GET_VoucherNo_Format",
                plist)
                .ToString().Trim();
            return result;
        }

        public static string GetNewSoCt(string masonb, DateTime ngayct)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Ma_sonb", masonb),
                new SqlParameter("@ngay_ct", ngayct.ToString("yyyyMMdd")),
            };
            var result = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GetNewSoct", plist);
            if (result.Tables.Count == 0) return "";
            var data = result.Tables[0];
            if (data.Rows.Count == 0) return "";
            var formatText = data.Rows[0]["TRANSFORM"].ToString().Trim();
            if (formatText == "") return "";
            var value = ObjectAndString.ObjectToDecimal(data.Rows[0]["SO_CT"]);
            var sResult = String.Format(formatText, value);
            return sResult;
        }

        public static string GetNewSoCt_date(string maCt, DateTime date, string type, string maDvcs, string makho, string sttrec, int userId, out string ma_sonb)
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
            var result = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GetNewSoct_Date", plist);
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
        

        /// <summary>
        /// Lấy mã mới cho hóa đơn, chứng từ.
        /// </summary>
        /// <param name="mact"></param>
        /// <returns></returns>
        public static string GetNewSttRec(string mact)
        {
            if (mact.Length > 3) mact = mact.Substring(0, 3);
            var param = new SqlParameter("@pMa_ct", mact);
            string sttRec = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_sGet_stt_rec", param).ToString();
            if (String.IsNullOrEmpty(sttRec))
            {
                throw new Exception(V6Setting.IsVietnamese ? "Không tạo mới được." : "Can not create new.");
            }
            return sttRec;
        }

        public static string GetNewLikeSttRec(string pMaCt, string pKhoa, string pLoai)
        {
            if (pMaCt.Length > 3) pMaCt = pMaCt.Substring(0, 3);
            var param = new SqlParameter("@pMa_ct", pMaCt);
            var param2 = new SqlParameter("@pKhoa", pKhoa);
            var param3 = new SqlParameter("@pLoai", pLoai);
            string sttRec = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_sGet_Key_Like_stt_rec", param, param2, param3).ToString();
            return sttRec;
        }

        public static DateTime GetServerDateTime()
        {
            return SqlConnect.GetServerDateTime();
        }

        public static decimal GetTyGia(string mant, DateTime ngayct)
        {
            try
            {
                SqlParameter[] pList =
                {
                    new SqlParameter("@ma_nt", mant),
                    new SqlParameter("@ngay_ct", ngayct.ToString("yyyyMMdd"))
                };

                var resultValue = Convert.ToDecimal(
                    SqlConnect.ExecuteScalar(CommandType.Text, "Select dbo.VFA_GetRates(@ma_nt, @ngay_ct)", pList));
                return resultValue;
            }
            catch (Exception)
            {
                // ignored
            }
            return 0;
        }

        /// <summary>
        /// Lấy chuỗi where theo phân quyền bằng proc.
        /// </summary>
        /// <returns></returns>
        public static string GetWhereAl(string tableName)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@Ma_dvcs", V6Login.Madvcs),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@Lan", V6Login.SelectedLanguage),
            };
            var o = ExecuteProcedureScalar("VPA_GET_WHERE_AL_ALL", plist);
            return o.ToString();
        }
        
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys">where, null nếu không dùng</param>
        /// <param name="fields">các trường dữ liệu, để trống lấy hết</param>
        /// <param name="groupby"></param>
        /// <param name="orderby"></param>
        /// <param name="pList"></param>
        /// <returns></returns>
        public static V6SelectResult Select(V6TableName name, IDictionary<string, object> keys, string fields,
            string groupby = "", string orderby = "", params SqlParameter[] pList)
        {
            if (!V6Login.UserRight.AllowSelect(name)) return new V6SelectResult();

            string where = SqlGenerator.GenSqlWhere(keys);
            return Select(name.ToString(), fields, @where, groupby, @orderby, pList);
        }

        public static V6SelectResult Select(string tableName, IDictionary<string, object> keys, string fields,
            string groupby = "", string orderby = "", params SqlParameter[] pList)
        {
            if (!V6Login.UserRight.AllowSelect(tableName)) return new V6SelectResult();

            var tableStruct = GetTableStruct(tableName);
            string where = SqlGenerator.GenWhere(tableStruct, keys);//.GenSqlWhere(keys);
            return Select(tableName, fields, @where, groupby, @orderby, pList);
        }

        public static V6SelectResult Select(string tableName, string fields = "*",
            string where = "", string groupby = "", string orderby = "", params SqlParameter[] pList)
        {
            if (!V6Login.UserRight.AllowSelect(V6TableHelper.ToV6TableName(tableName))) return new V6SelectResult();
            return SqlConnect.Select(tableName, fields, @where, groupby, @orderby, pList);
        }

        public static int SelectCount(string tableName, string field = "*",
            string where = "", params SqlParameter[] pList)
        {
            var whereClause = String.IsNullOrEmpty(@where) ? "" : "where " + @where;
            var sql = "Select Count("+field+") as Count from ["+tableName+"] " + whereClause;
            var count = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql, pList));
            return count;
        }

        public static DataTable SelectSimple(string tableName, string fields, string where="", string group="", string sort = "", params SqlParameter[] pList)
        {
            return SqlConnect.SelectSimple(tableName, fields, where, group, sort, pList);
        }
        public static DataTable SelectTable(string tableName)
        {
            return SqlConnect.SelectTable(tableName);
        }

        public static DataTable SelectOneRow(string tablename, IDictionary<string, object> keys)
        {
            return SqlConnect.SelectOneRow(tablename, keys);
        }

        public static object SelectOneValue(string tablename, string field, IDictionary<string, object> keys)
        {
            return SqlConnect.SelectOneValue(tablename, field, keys);
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
                case V6TableName.Algia0: result = "ma_gia"; break;
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
                    throw new IndexOutOfRangeException(String.Format("No column name: \"{0}\".", colName));
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

        /// <summary>
        /// Tính tổng theo điều kiện field=value (không kể khoảng trắng, không phân biệt hoa thường).
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sumColumn"></param>
        /// <param name="conditionColumn"></param>
        /// <param name="conditionValue"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static decimal TinhTongDieuKien(DataTable data, string sumColumn, string conditionColumn, object conditionValue, bool throwException = false)
        {
            if (throwException)
            {
                if (data == null) throw new ArgumentException("DataTable is null.", "data");
                if (!data.Columns.Contains(sumColumn))
                    throw new IndexOutOfRangeException(String.Format("No column name: \"{0}\".", sumColumn));
                if (!data.Columns.Contains(conditionColumn))
                    throw new IndexOutOfRangeException(String.Format("No column name: \"{0}\".", conditionColumn));
            }
            var total = 0m;
            try
            {
                if (data != null && data.Columns.Contains(sumColumn))
                {
                    for (var j = 0; j < data.Rows.Count; j++)
                    {
                        DataRow rowj = data.Rows[j];
                        if (ObjectAndString.ObjectToString(rowj[conditionColumn]).Trim().ToUpper() ==
                            ObjectAndString.ObjectToString(conditionValue).Trim().ToUpper())
                        {
                            total += ObjectAndString.ObjectToDecimal(data.Rows[j][sumColumn]);
                        }
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

        /// <summary>
        /// Tính tổng theo dấu oper, + thì cộng, - thì trừ.
        /// </summary>
        /// <param name="data">Bảng dữ liệu.</param>
        /// <param name="colName">Cột tính tổng.</param>
        /// <param name="oper_column">Cột dấu + hoặc -</param>
        /// <returns></returns>
        public static decimal TinhTongOper(DataTable data, string colName, string oper_column)
        {
            var total = 0m;
            if (String.IsNullOrEmpty(oper_column) || data == null || !data.Columns.Contains(oper_column))
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
            SqlParameter[] plist =
            {
                new SqlParameter("@Codeform", codeform),
                new SqlParameter("@Type", type)
            };

            //FIELDV,VALUEV,BOLD_YN,COLOR_YN,COLORV
            var result = SqlConnect.ExecuteDataset(
                CommandType.StoredProcedure,
                "VPA_GetFormatGridView",
                plist)
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

        /// <summary>
        /// GetLoDate không theo Mã Kho
        /// </summary>
        /// <param name="mavt"></param>
        /// <param name="sttRec"></param>
        /// <param name="ngayct"></param>
        /// <returns></returns>
        public static DataTable GetLoDatePriority(string mavt, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC_Priority", plist).Tables[0];
        }

        public static DataTable GetStockinVitriDatePriority(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", ""),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cMa_kho", makho),
                new SqlParameter("@cMa_vt", mavt),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_STOCKIN_VITRI_DATE_STT_REC_PRIORITY", plist).Tables[0];
        }

        public static DataTable GetVitriLoDatePriority(string mavt, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC_Priority", plist).Tables[0];
        }

        public static DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        public static DataTable GetSKSM(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_SKSM_STT_REC", plist).Tables[0];
        }

        public static DataTable GetLoDate_IXY(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC_IXY", plist).Tables[0];
        }
        public static DataTable GetLoDate_IXP(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC_IXP", plist).Tables[0];
        }
        
        public static DataTable GetViTri(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (mavt == "")
                {
                    keys = "1=1";
                }
                else
                {
                    keys = String.Format("Ma_vt = '" + mavt + "'");
                }

            }
            else
            {
                if (mavt == "")
                {
                    keys = String.Format(" Ma_kho = '" + makho + "'");
                }
                else
                {
                    keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");
                }
            }

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_STT_REC", plist).Tables[0];
        }
        
        public static DataTable GetViTriLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (mavt == "")
                {
                    keys = "1=1";
                }
                else
                {
                    keys = String.Format("Ma_vt = '" + mavt + "'");
                }
            }
            else
            {
                if (mavt == "")
                {
                    keys = String.Format(" Ma_kho = '" + makho + "'");
                }
                else
                {
                    keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");
                }
            }

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }

        public static DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        public static DataTable GetLoDateAll(string mavt_in, string makho_in, string malo_in, string sttRec, DateTime ngayct)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt in ("+mavt_in+") and Ma_kho in ("+makho_in+") and Ma_lo in ("+malo_in+")")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        
        public static DataTable GetViTri13(string mavt, string makho, string mavitri, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            mavitri = mavitri.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_vitri = '"+mavitri+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_STT_REC", plist).Tables[0];
        }
        
        public static DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");
            mavitri = mavitri.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (malo == "")
                {
                    if (mavitri == "")
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "'");
                    }
                    else
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_vitri = '" + mavitri + "'");
                    }
                }
                else
                {
                    if (mavitri == "")
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_lo = '" + malo + "'");
                    }
                    else
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_lo = '" + malo + "' and Ma_vitri = '" + mavitri + "'");
                    }
                }
            }
            else
            {
                if (malo == "")
                {
                    if (mavitri == "")
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");

                    }
                    else
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_vitri = '" + mavitri + "'");
                    }
                }
                else
                {
                    if (mavitri == "")
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_lo = '" + malo + "'");
                    }
                    else
                    {
                        keys = String.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_lo = '" + malo + "' and Ma_vitri = '" + mavitri + "'");
                    }
                }
            }

            SqlParameter[] plist = new[]
            {   
               
                //new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"' and Ma_vitri = '"+mavitri+"'")),
                new SqlParameter("@cKey1",keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }
        public static DataTable GetViTriLoDateAll(string mavt_in, string makho_in, string malo_in, string mavitri_in, string sttRec, DateTime ngayct)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", String.Format("Ma_vt in ("+mavt_in+") and Ma_kho in ("+makho_in
                    +") and Ma_lo in ("+malo_in+") and Ma_vitri in ("+mavitri_in+")")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }

        public static DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct)
        {
            //    @Type AS TINYINT, -- 0: Đầu kỳ, 1: Cuối kỳ
            //@Ngay_ct AS SmallDateTime, -- Ngày tính số dư đầu kỳ
            //@ma_ct varchar(50),
            //@Stt_rec char(13),
            //@Advance AS VARCHAR(8000) = '', -- Điều kiện lọc 
            //@OutputInsert VARCHAR(4000) = '' --Tên bảng nhận dữ liệu ra

            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", String.Format("a.MA_VT='"+mavt+"' AND a.MA_KHO='"+makho+"'")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
        }
        
        public static DataTable GetStockAll(string mact, string mavt_in, string makho_in, string sttRec, DateTime ngayct)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", String.Format("a.MA_VT in ("+mavt_in+") AND a.MA_KHO in ("+makho_in+")")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
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
            string path = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "oldPrinter");
            string s = "";
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
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
            string path = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, "oldPrinter");
            FileStream fs = new FileStream(path, FileMode.Create);
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

        public static void WriteTextFile(string fileName, string content)
        {
            try
            {
                var fs = new FileStream(fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
                sw.Close();
                fs.Close();
            }
            catch
            {
                //
            }
        }

        public static void WriteV6UserLog(string item_id, string action, string content)
        {
            try
            {
                IDictionary<string, object> data = new Dictionary<string, object>();

                data["DATE"] = GetServerDateTime();
                data["USER_ID"] = V6Login.UserId;
                data["USER_NAME"] = V6Login.UserName + " Computer:" + V6Login.ClientName;
                data["ITEM_ID"] = item_id;
                data["ACTION"] = action;
                data["CONTENT"] = content;
                InsertSimple("V6USERLOG", data);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("WriteV6UserLog " + item_id + action + content, ex);
            }
        }

        public static void WriteV6History(string item_id, string action, string type,
            string ma_ct, string stt_rec, string content,
            string ma_dm, string ma, string content2, string uid)
        {
            try
            {
                IDictionary<string, object> data = new Dictionary<string, object>();

                data["DATE"] = GetServerDateTime();
                data["USER_ID"] = V6Login.UserId;
                data["USER_NAME"] = V6Login.UserName;
                data["CLIENT_NAME"] = V6Login.ClientName;
                data["ITEM_ID"] = item_id;
                data["ACTION"] = action;
                data["TYPE"] = type;
                data["MA_CT"] = ma_ct;
                data["MA_DM"] = ma_dm;
                data["STT_REC"] = stt_rec;
                data["MA"] = ma;
                data["CONTENT"] = content;
                data["CONTENT2"] = content2;
                //data["CONTENT3"] = content;
                //data["CONTENT4"] = content;
                InsertSimple("V6HISTORY", data);
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("WriteV6History " + item_id + action + content, ex);
            }
        }

        /// <summary>
        /// Kiểm tra hóa đơn đã thanh toán, ko được sửa hoặc xóa (check tồn tại)
        /// </summary>
        /// <param name="sttRec"></param>
        /// <param name="tableName"></param>
        /// <param name="mode"></param>
        /// <param name="maCt"></param>
        /// <returns>0:được sửa, 1: không được sửa</returns>
        public static int CheckEditVoucher(string sttRec, string tableName, string mode, string maCt)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec),
                new SqlParameter("@tablename", tableName),
                new SqlParameter("@mode", mode),
                new SqlParameter("@ma_ct", maCt),
                new SqlParameter("@user_id", V6Login.UserId),
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text,  "Select dbo.VFA_IsEditVoucher_UserId (@stt_rec, @tablename, @mode, @ma_ct, @user_id)", plist);
            return ObjectAndString.ObjectToInt(result);
        }
        
        public static int CheckEditVoucher_SOR(string sttRecPt, string tableName, string mode, string maCt)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec_pt", sttRecPt),
                new SqlParameter("@tablename", tableName),
                new SqlParameter("@mode", maCt),
                new SqlParameter("@ma_ct", maCt),
                new SqlParameter("@user_id", V6Login.UserId),
            };
            var result = SqlConnect.ExecuteScalar(CommandType.Text, "Select dbo.VFA_IsEditVoucher_SOR_UserId (@stt_rec_pt, @tablename, @mode, @ma_ct, @user_id)", plist);
            return ObjectAndString.ObjectToInt(result);
        }

        private static void CheckIdentifier(string name)
        {
            if (name.Contains("'") || name.Contains(" ") || name.Contains("(")
                || name.Contains(".") || name.Contains(" "))
                throw new ArgumentException("Identifier expected.", "name");
        }

        public static bool StartSqlConnect(string key, string iniLocation)
        {
            return SqlConnect.StartSqlConnect(key, iniLocation);
        }

        public static void UpdateAlqddvt(string ma_vt_old, string ma_vt_new)
        {
            ExecuteProcedureNoneQuery("VPA_UPDATE_ALQDDVT", new SqlParameter("@cMa_vt_old", ma_vt_old), new SqlParameter("@cMa_vt_new", ma_vt_new));
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
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
