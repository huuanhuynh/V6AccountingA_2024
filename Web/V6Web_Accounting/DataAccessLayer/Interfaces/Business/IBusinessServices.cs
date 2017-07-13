using System;
using System.Collections.Generic;
using System.Data;
using V6Structs;

namespace DataAccessLayer.Interfaces.Business
{
    public interface IBusinessServices
    {
        DateTime GetServerDateTime();
        DataTable SelectTable(string tableName);
        V6SelectResult Select(string tableName, string fields, string where, string group, string sort);
        V6SelectResult SelectPaging(string tableName, string fields, int page, int size, string where, string sort, bool ascending);
        DataSet ExecuteProcedure(string procName, Dictionary<string, string> parameters);
        object ExecuteProcedureScalar(string procName, Dictionary<string, string> parameters);
        int ExecuteProcedureNoneQuery(string procName, Dictionary<string, string> parameters);
        int Update(string tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys);
        Dictionary<string, string> GetHideColumns(string tableName);
        DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct);
        string GetSoCt(string mode, string voucherNo, string mact, string maDvcs, int userId);
        string GetNewSoCt(string masonb);
        string GetNewSttRec(string mact);
        string GetNewLikeSttRec(string pMaCt, string pKhoa, string pLoai);
        DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct);
        DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct);
        DataTable GetStockAll(string mact, string mavt_in, string makho_in, string sttRec, DateTime ngayct);
        V6TableStruct GetTableStruct(string tableName);
        #region ==== CHECK ====
        /// <summary>
        /// Kiểm tra mã tồn tại.
        /// </summary>
        /// <param name="madm"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AllCheckExist(string madm, string value);
        /// <summary>
        /// Kiểm tra trong bảng Altt có tên bảng trong Ma_dm hay không
        /// </summary>
        /// <param name="ma_dm"></param>
        /// <returns></returns>
        bool CheckAltt(string ma_dm);
        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// VPA_isValidOneCode_Full
        /// </summary>
        /// <returns></returns>
        bool IsValidOneCode_Full(string cInputTable, byte nStatus, string cInputField, string cpInput, string cOldItems);
        bool IsValidTwoCode_Full(string cInputTable, byte nStatus,
            string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2);
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
        bool IsValidThreeCode(string cInputTable, byte nStatus,
            string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3);
        bool IsValidThreeCode_OneNumeric(string cInputTable, byte nStatus,
            string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string nInputField1, Int32 npInput1, Int32 nOldItems1);
        bool IsValidTwoCode_OneNumeric(string cInputTable, byte nStatus,
            string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string nInputField1, Int32 npInput1, Int32 nOldItems1);
        bool IsValidOneCode_OneDate(string cInputTable, byte nStatus,
            string cInputField1, string nInputField1, string cpInput1,
            string npInput1, string cpInput1Old, string npInput1Old);
        /// <summary>
        /// Đã tồn tại mã trong bảng => true
        /// [VPA_isExistOneCode_List]
        /// </summary>
        /// <returns></returns>
        bool IsExistOneCode_List(string cInputTableList, string cInputField, string cpInput);
        bool IsExistTwoCode_List(string cInputTableList, string cInputField1, string cpInput1,
            string cInputField2, string cpInput2);

        #endregion check
        #region ==== CHANGE ====
        void ChangeAll_Id(string madm, string oldCode, string newCode);
        /// <summary>
        /// Thay đổi mã khách hàng
        /// </summary>
        /// <param name="oldCode">Mã cũ</param>
        /// <param name="newCode">Mã mới</param>
        /// <returns></returns>
        void ChangeCustomeId(string oldCode, string newCode);
        void ChangeItemId(string oldCode, string newCode);
        void ChangeWarehouseId(string oldCode, string newCode);
        void ChangeDepartmentId(string oldCode, string newCode);
        void ChangeUnitId(string oldCode, string newCode);
        void ChangeJobId(string oldCode, string newCode);
        void ChangeAccountId(string oldCode, string newCode);
        #endregion change

        bool StartSqlConnect(string key, string iniLocation);
    }
}
