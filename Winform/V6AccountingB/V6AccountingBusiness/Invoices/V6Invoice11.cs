using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6AccountingBusiness.Invoices
{
    //public interface IV6Invoice11
    //{
    //    string PrintReportProcedure { get; }
    //    string Name { get; }
    //    DataTable Alct2 { get; }
    //    string Mact { get; set; }
    //    string CodeMact { get; set; }

    //    /// <summary>
    //    /// Tên bảng dữ liệu AM
    //    /// </summary>
    //    string AM { get; }

    //    /// <summary>
    //    /// Tên bảng dữ liệu AD m_ctdbf
    //    /// </summary>
    //    string AD { get; }

    //    string AD2 { get; }

    //    /// <summary>
    //    /// m_ktdbf
    //    /// </summary>
    //    string AD3 { get; }

    //    int SoLien { get; }
    //    V6TableStruct AMStruct { get; }
    //    V6TableStruct ADStruct { get; }
    //    V6TableStruct AD2Struct { get; }
    //    V6TableStruct AD3Struct { get; }
    //    DataTable Alnt { get; }
    //    DataTable Alct { get; }
    //    bool M_LOC_NSD { get; }

    //    /// <summary>
    //    /// Sử dụng ngày lập chứng từ.
    //    /// </summary>
    //    bool M_NGAY_CT { get; }

    //    DataTable Alct1 { get; }
    //    DataTable Alct3 { get; }
    //    DataTable AlPost { get; }

    //    /// <summary>
    //    /// Các trường động,, cho lọc AM
    //    /// </summary>
    //    string ADV_AM { get; }

    //    /// <summary>
    //    /// Các trường động,, cho lọc AD
    //    /// </summary>
    //    string ADV_AD { get; }

    //    string GRDS_AM { get; }
    //    string GRDS_AD { get; }
    //    string GRDF_AM { get; }
    //    string GRDF_AD { get; }
    //    string GRDHV_AM { get; }
    //    string GRDHE_AM { get; }
    //    string GRDHV_AD { get; }
    //    string GRDHE_AD { get; }
    //    string GRDS_Q1 { get; }
    //    string GRDF_Q1 { get; }
    //    string GRDHV_Q1 { get; }
    //    string GRDHE_Q1 { get; }
    //    string GRDT_AM { get; }
    //    string GRDT_AD { get; }

    //    bool InsertInvoice(SortedDictionary<string, object> am,
    //        List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList2);

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="am"></param>
    //    /// <param name="adList"></param>
    //    /// <param name="adList2"></param>
    //    /// <param name="keys">STT_REC</param>
    //    /// <returns></returns>
    //    bool UpdateInvoice(SortedDictionary<string, object> am,
    //        List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList2,
    //        SortedDictionary<string,object> keys );

    //    DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs);
    //    DataTable LoadAd(string sttRec);
    //    DataTable LoadAd2(string sttRec);
    //    bool DeleteInvoice(string sttrec);

    //    DataRow GetGiaNhap(string field, string mact, DateTime ngayct,
    //        string mant, string mavt, string dvt1, string makh, string magia);

    //    /// <summary>
    //    /// Ghi log vào csdl.
    //    /// </summary>
    //    /// <param name="sttRec"></param>
    //    /// <param name="mode"></param>
    //    /// <param name="message">null sẽ lấy mặc định V6Message</param>
    //    void PostErrorLog(string sttRec, string mode, string message = null);

    //    void PostErrorLog(string sttRec, string mode, Exception ex);
    //    decimal GetTyGia(string mant, DateTime ngayct);
    //    DataSet GetCheck_VC(string status, string kieu_post, string stt_rec);
    //    DataTable GetCheck_VC_Save(string status, string kieu_post, string soct, string masonb, string sttrec);

    //    DataTable GetCheck_Save_All(string status, string kieu_post, string soct, string masonb, string sttrec, string madvcs, string makh,
    //        string manx,DateTime ngayct, string mact,decimal tongthanhtoan,string mode, int user_id);

    //    /// <summary>
    //    /// Lấy thông tin Lo Date bằng procedure [VPA_EdItems_DATE_STT_REC]
    //    /// </summary>
    //    /// <param name="mavt"></param>
    //    /// <param name="makho"></param>
    //    /// <param name="sttRec"></param>
    //    /// <param name="ngayct"></param>
    //    /// <returns></returns>
    //    DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);

    //    DataTable GetViTri(string mavt, string makho, string sttRec, DateTime ngayct);
    //    DataTable GetViTriLoDate(string mavt, string makho, string sttRec, DateTime ngayct);

    //    /// <summary>
    //    /// VPA_EdItems_DATE_STT_REC
    //    /// </summary>
    //    /// <param name="mavt"></param>
    //    /// <param name="makho"></param>
    //    /// <param name="malo"></param>
    //    /// <param name="sttRec"></param>
    //    /// <param name="ngayct"></param>
    //    /// <returns></returns>
    //    DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct);

    //    DataTable GetViTri13(string mavt, string makho, string mavitri, string sttRec, DateTime ngayct);
    //    DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct);

    //    /// <summary>
    //    /// [VPA_CheckTonXuatAm] 1,'20160216','SOA','STTREC','a.MA_VT=''VTLO1'' AND a.MA_KHO=',''
    //    /// </summary>
    //    /// <returns></returns>
    //    DataTable GetStock (string mavt, string makho, string sttRec, DateTime ngayct);

    //    DataTable GetSoct0_All_Cust(string sttRec, string trim, string filterString);
    //    void IncreaseSl_inAm81(string sttRec);
    //    SortedDictionary<string, string> LoadDefaultData(string lang, string itemId);
    //    List<DefaultValueInfo> LoadDataReferenceInfo(string lang, string itemId);
    //    SortedDictionary<string, string> LoadTag(string itemId);
    //    string ToString();
    //}

    /// <summary>
    /// GL1: Phiếu kế toán
    /// </summary>
    public class V6Invoice11 : V6InvoiceBase
    {
        public V6Invoice11():base("GL1")
        {

        }
        public V6Invoice11(string mact):base(mact)
        {
            //_mact = mact;
        }
        
        public override string PrintReportProcedure
        {
            get { return "AGLCTGL1"; }
        }

        public override string Name { get { return "Phiếu kế toán"; } }
        private DataTable _alct2;

        public DataTable Alct2
        {
            get
            {
                _alct2 = _alct2 ?? GetAlct2();
                return _alct2;
            }
        }

        private DataTable GetAlct2()
        {
            SqlParameter[] pList = 
                {
                    new SqlParameter("@ma_ct", Mact),
                    new SqlParameter("@list_fix", ""),
                    new SqlParameter("@order_fix", ""),
                    new SqlParameter("@vvar_fix", ""),
                    new SqlParameter("@type_fix", ""),
                    new SqlParameter("@checkvvar_fix", ""),
                    new SqlParameter("@notempty_fix", ""),
                    new SqlParameter("@fdecimal_fix", "")
                };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                    "VPA_GET_AUTO_COLUMN_GT", pList).Tables[0];
        }
        
        public bool InsertInvoice(SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList2)
        {
            var insert_am_sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, am);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AM);

            //Delete AD
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
            {
                {"STT_REC",am["STT_REC"]}
            };
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AD2
            var deleteAd2Sql = SqlGenerator.GenDeleteSql(AD2Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd2Sql);
            //Delete AM
            var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);

            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
            var currentMethodName = MethodBase.GetCurrentMethod().Name;
            var j = InsertADlist(currentMethodName, TRANSACTION, adList, true);
            var j2 = InsertAD2list(currentMethodName, TRANSACTION, adList2, true);

            if (insert_success && j == adList.Count && j2 == adList2.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(am["STT_REC"]);
                try
                {
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_GL1_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = "POST lỗi: " + V6Message;
                    //TRANSACTION.Rollback();
                    return false;
                }
            }
            else//
            {
                if (!insert_success) V6Message = "Thêm AM không thành công.";
                if (j != adList.Count) V6Message += "Thêm AD không hoàn tất.";
                if (j2 != adList2.Count) V6Message += "Thêm AD2 không hoàn tất.";
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="am"></param>
        /// <param name="adList"></param>
        /// <param name="adList2"></param>
        /// <param name="keys">STT_REC</param>
        /// <returns></returns>
        public bool UpdateInvoice(SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList2,
            SortedDictionary<string,object> keys )
        {

            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AMUpdate");
            
            //Delete AD
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AD2
            var deleteAd2Sql = SqlGenerator.GenDeleteSql(AD2Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd2Sql);
            
            //Update AM
            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
            var currentMethodName = MethodBase.GetCurrentMethod().Name;
            var j = InsertADlist(currentMethodName, TRANSACTION, adList, false);
            var j2 = InsertAD2list(currentMethodName, TRANSACTION, adList2, false);

            if (insert_success && j == adList.Count && j2 == adList2.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(am["STT_REC"]);
                try
                {
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_GL1_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = "POST lỗi: " + V6Message;
                    //TRANSACTION.Rollback();
                    return false;
                }
            }
            else
            {
                if (!insert_success) V6Message = "Thêm AM không thành công.";
                if (j != adList.Count) V6Message += "Thêm AD không hoàn tất.";
                if (j2 != adList2.Count) V6Message += "Thêm AD2 không hoàn tất.";
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            string template =
                "Select a.*,f.Ten_nvien AS Ten_nvien"
                + "\nFROM "+AM+" a  LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n JOIN "
                + "\n (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            if (where0Ngay.Length > 0) where0Ngay = " And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = " And " + where1AM;
            if (where4Dvcs.Length > 0)
            {
              where1AM = " And " + where4Dvcs;
            }

            string p2Template;
            
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
               
                p2Template = string.Format("\n--{0}{1}\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact + "' {0} {2})",
                    where0Ngay, "", where2AD);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, p2Template);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }

        public DataTable LoadAd(string sttRec)
        {
            //c=AD, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk, k.Ten_kh as Ten_kh_i FROM " + AD
                + " c LEFT JOIN Altk d ON c.Tk_i= d.Tk "
                + " LEFT JOIN Alkh k ON c.MA_KH_I= k.MA_KH "
                + " Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            
            return tbl;
        }
        public DataTable LoadAd2(string sttRec)
        {
            //c=AD, d=Alvt, e=ABVT13
            string sql = "SELECT * FROM " + AD2 + " Where stt_rec = @rec Order by stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        
        public bool DeleteInvoice(string sttrec)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Stt_rec", sttrec), 
                    new SqlParameter("@Ma_ct", Mact),
                    new SqlParameter("@UserID", V6Login.UserId)
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_GL1_DELETE_MAIN", plist);
                return true;
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
        }

        public DataRow GetGiaNhap(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {
            try
            {
                //DateTime d = DateTime.ParseExact(ngayct, "d/M/yyyy", null);
                SqlParameter[] pList =
                {
                    new SqlParameter("@cField", field),
                    new SqlParameter("@cVCID", mact),
                    new SqlParameter("@dPrice", ngayct),
                    new SqlParameter("@cFC", mant),
                    new SqlParameter("@cItem", mavt),
                    new SqlParameter("@cUOM", dvt1),
                    new SqlParameter("@cCust", makh),
                    new SqlParameter("@cMaGia", magia)
                };

                var resultData = V6BusinessHelper.ExecuteProcedure("VPA_GetSOIDPrice", pList).Tables[0];
                if (resultData.Rows.Count == 1)
                {
                    return resultData.Rows[0];
                }

                //return "{gia_ban_nt:" + data.Rows[0]["gia_ban_nt"] + ",gia_nt2:" + data.Rows[0]["gia_nt2"] + ",error:0,message:''}";
            }
            catch (Exception ex)
            {
                //return "{gia_ban_nt:0,gia_nt2:0,error:1,message:\"GetPrice: " + ToJSstring(ex.Message) + "\"}";
                throw new Exception("V6Invoice GetGiaNhap " + ex.Message);
            }
            return null;
        }
    }

}
