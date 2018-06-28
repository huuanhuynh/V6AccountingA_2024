using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Init;
using V6SqlConnect;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// IXC: Phiếu xuất trả nhà cung cấp
    /// </summary>
    public class V6Invoice86 : V6InvoiceBase
    {
        public V6Invoice86() : base("IXC") { }

        public override string PrintReportProcedure
        {
            get { return "AINCTIXC"; }
        }

        public override string Name { get { return _name; } set { _name = value; } }
        private string _name = "Phiếu xuất trả nhà cung cấp";

        public bool InsertInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList)
        {
            var insert_am_sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, am);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AM_TableName);

            //Delete AD
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
            {
                {"STT_REC",am["STT_REC"]}
            };
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AM
            var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);
            
            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
            var currentMethodName = MethodBase.GetCurrentMethod().Name;
            var j = InsertADlist(currentMethodName, TRANSACTION, adList, true);
            
            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(am["STT_REC"]);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        new SqlParameter("@Ma_nx", am["MA_NX"].ToString()),
                        new SqlParameter("@Loai_ck", am["LOAI_CK"].ToString()),
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")

                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXC_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = "POST lỗi: " + V6Message;
                    
                    return false;
                }
            }
            else//
            {
                if (!insert_success) V6Message = "Thêm AM không thành công.";
                if (j != adList.Count) V6Message += "Thêm AD không hoàn tất.";
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }
        
        public bool UpdateInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList,
            SortedDictionary<string,object> keys )
        {

            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM_TableName, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AMUpdate");
            
            //Delete AD
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            
            //Update AM
            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
            var currentMethodName = MethodBase.GetCurrentMethod().Name;
            var j = InsertADlist(currentMethodName, TRANSACTION, adList, false);

            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(am["STT_REC"]);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        new SqlParameter("@Ma_nx", am["MA_NX"].ToString()),
                        new SqlParameter("@Loai_ck", am["LOAI_CK"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };
                   
                 
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXC_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = "POST lỗi: " + V6Message;
                    
                    return false;
                }
            }
            else
            {
                if (!insert_success) V6Message = "Thêm AM không thành công.";
                if (j != adList.Count) V6Message += "Thêm AD không hoàn tất.";
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            var filterKho = V6Login.GetFilterKho("MA_KHO");
            if (!string.IsNullOrEmpty(filterKho))
            {
                where4Dvcs += (string.IsNullOrEmpty(where4Dvcs) ? "" : " and ") + filterKho;
            }

            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM "+AM_TableName+" a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt  JOIN "
                + "\n (SELECT Stt_rec FROM " + AM_TableName + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            var p2Template =
                "\n--{0}{1}\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD_TableName + " WHERE Ma_ct = '" + Mact + "' {0} {2}"
                + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                + "\n		{4})";//" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 {4})"
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
                if (where3NhVt.Length > 0) where3NhVt = "And " + where3NhVt;
                if (where4Dvcs.Length > 0) where4Dvcs
                    = string.Format("	And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);

                p2Template = string.Format(p2Template, where0Ngay, "", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, p2Template);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }

        public DataTable SearchPhieuNhap(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {//!!!!!!
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;

            string whereAD_Nhvt_Dvcs, whereAD_Nhvt_Dvcs3;
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
                if (where3NhVt.Length > 0) where3NhVt = "And " + where3NhVt;
                if (where4Dvcs.Length > 0)
                    where4Dvcs
                        = string.Format(" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);


                whereAD_Nhvt_Dvcs = string.Format(
                    "\n Where d.Stt_rec in (SELECT Stt_rec FROM AD71 WHERE Ma_ct = 'POA' {0} {2}"
                    + "\n And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})"
                    + "\n {4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);

                whereAD_Nhvt_Dvcs3 = string.Format(
                   "\n Where d.Stt_rec in (SELECT Stt_rec FROM AD72 WHERE Ma_ct = 'POB' {0} {2}"
                   + "\n And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})"
                   + "\n {4})"
                   , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
                whereAD_Nhvt_Dvcs3 = "";
            }

            var sql = string.Format(
                "(Select ' ' Tag,  v.ten_vt," +
                "\n d.Stt_rec, d.Stt_rec0, d.Ma_ct, d.Ngay_ct, d.So_ct, d.Ma_vt, d.Dvt1, " +
                "\n d.He_so1, d.So_luong1, d.Tk_vt, d.Ma_kho_i, d.So_luong, d.Gia_nt," +
                "\n d.Gia, d.Tien_nt, d.Tien, d.Dvt, d.Gia_nt1, d.Gia1, d.Ma_lo, d.HSD," +
                "\n d.Ma_LNX_i, d.GG_nt, d.GG, d.Ck_nt, d.Ck," +
                "\n d.Ma_vv_i, d.Ma_bpht, d.Ma_sp, d.Ma_ku, d.Ma_phi, d.Ma_hd, d.Ma_vitri," +
                "\n d.Ma_td_i, d.Ma_td2, d.Ma_td3," +
                "\n d.STT_REC AS STT_REC_PN, d.STT_REC0 AS STT_REC0PN "
                + "\nFROM AD71 d"
                + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
                + "\n  JOIN (SELECT Stt_rec FROM AM71 WHERE Ma_ct = 'POA'" + "\n {0} {1}) AS m ON d.Stt_rec = m.Stt_rec"
                + "\n {2})"
                
                + "\n UNION ALL" +

                "\n(Select ' ' Tag,  v.ten_vt," +
                "\n d.Stt_rec, d.Stt_rec0, d.Ma_ct, d.Ngay_ct, d.So_ct, d.Ma_vt, d.Dvt1, " +
                "\n d.He_so1, d.So_luong1, d.Tk_vt, d.Ma_kho_i, d.So_luong, d.Gia_nt," +
                "\n d.Gia, d.Tien_nt, d.Tien, d.Dvt, d.Gia_nt1, d.Gia1, d.Ma_lo, d.HSD," +
                "\n d.Ma_LNX_i, d.GG_nt, d.GG, d.Ck_nt, d.Ck," +
                "\n d.Ma_vv_i, d.Ma_bpht, d.Ma_sp, d.Ma_ku, d.Ma_phi, d.Ma_hd, d.Ma_vitri," +
                "\n d.Ma_td_i, d.Ma_td2, d.Ma_td3," +
                "\n d.STT_REC AS STT_REC_PN, d.STT_REC0 AS STT_REC0PN "
                + "\nFROM AD72 d"
                + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
                + "\n  JOIN (SELECT Stt_rec FROM AM72 WHERE Ma_ct = 'POB'" + "\n {0} {1}) AS m ON d.Stt_rec = m.Stt_rec"
                + "\n {3})"

                + "\n ORDER BY ngay_ct, so_ct, stt_rec, stt_rec0"



                , where0Ngay, where1AM, whereAD_Nhvt_Dvcs, whereAD_Nhvt_Dvcs3);

            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }


        public DataTable LoadAd(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [" + AD_TableName
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt  Where c.stt_rec = @rec Order by c.stt_rec0";
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
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXC_DELETE_MAIN", plist);
                return true;
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
        }

        public DataRow GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {
            try
            {
                //DateTime d = DateTime.ParseExact(ngayct, "d/M/yyyy", null);
                SqlParameter[] plist =
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

                var resultData = V6BusinessHelper.ExecuteProcedure("VPA_GetSOIDPrice", plist).Tables[0];
                if (resultData.Rows.Count == 1)
                {
                    return resultData.Rows[0];
                }

                //return "{gia_ban_nt:" + data.Rows[0]["gia_ban_nt"] + ",gia_nt2:" + data.Rows[0]["gia_nt2"] + ",error:0,message:''}";
            }
            catch (Exception ex)
            {
                //return "{gia_ban_nt:0,gia_nt2:0,error:1,message:\"GetPrice: " + ToJSstring(ex.Message) + "\"}";
                throw new Exception("V6Invoice GetGiaBan " + ex.Message);
            }
            return null;
        }
        
    }

}
