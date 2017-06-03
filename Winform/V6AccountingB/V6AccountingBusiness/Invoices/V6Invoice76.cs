﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6SqlConnect;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// SOF: Phiếu nhập hàng bán bị trả lại
    /// </summary>
    public class V6Invoice76 : V6InvoiceBase
    {
        public V6Invoice76() : base("SOF") { }

        public override string PrintReportProcedure { get { return "ASOCTSOF"; } }

        public override string Name { get { return _name; } set { _name = value; } }
        private string _name = "Hàng trả lại";

        public bool InsertInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList)
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
            //Delete AM
            var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);


            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
            var j = 0;
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow);
                j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql)>0?1:0);
            }
            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
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

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_SOF_POST_MAIN", pList);

                   

                    //TRANSACTION.Commit();
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
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }
        
        public bool UpdateInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList,
            SortedDictionary<string,object> keys )
        {

            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AM81Update");
            
            //Delete AD
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            
            //Update AM
            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
            var j = 0;

            //Insert AD
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow, false);
                j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
            }
            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
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
                   
                 
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_SOF_POST_MAIN", pList);

                  
                    //TRANSACTION.Commit();
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
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM "+AM+" a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt  JOIN "
                + "\n (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";

            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            var p2Template =
                "\n--{0}{1}"//phần bỏ
                + "\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact + "' {2}"
                + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                + "\n		{4})";//" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 {4})"
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
                if (where3NhVt.Length > 0) where3NhVt = "And " + where3NhVt;
                if (where4Dvcs.Length > 0) where4Dvcs
                    = string.Format("	And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);

                p2Template = string.Format(p2Template,"","", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, p2Template);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }
        
        public DataTable SearchPhieuXuat(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            //string template =
            //    "Select d.*, d.STT_REC AS STT_REC_PX, d.STT_REC0 AS STT_REC0PX, v.ten_vt "
            //    //"Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
            //    + "\nFROM AD81 d LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
            //    + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt "
            //    + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
            //    + "\n  JOIN (SELECT Stt_rec FROM AM81 WHERE Ma_ct = 'SOA'"
            //    + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
            //    + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;

            var whereAD_Nhvt_Dvcs = "";
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
                if (where3NhVt.Length > 0) where3NhVt = "And " + where3NhVt;
                if (where4Dvcs.Length > 0)
                    where4Dvcs
                        = string.Format(" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD81 WHERE Ma_ct = 'SOA' {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , "0", "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            var sql = string.Format("Select ' ' Tag,  v.ten_vt,v.tk_tl , d.*, d.STT_REC AS STT_REC_PX, d.STT_REC0 AS STT_REC0PX "
                //"Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM AD81 d "//" LEFT JOIN Alkh b ON d.Ma_kh=b.Ma_kh "
                //+ "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt "
                + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
                + "\n  JOIN (SELECT Stt_rec FROM AM81 WHERE Ma_ct = 'SOA'"+ "\n {0} {1}) AS m ON d.Stt_rec = m.Stt_rec"
                + "\n {2}"
                + "\n ORDER BY d.ngay_ct, d.so_ct, d.stt_rec",
                where0Ngay, where1AM, whereAD_Nhvt_Dvcs);

            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }

        public DataTable LoadAD(string sttRec)
        {
            //c=AD81, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [" + AD + "]"
                + " c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }

        public DataTable LoadADs_SOA(string[] sttRecArray)
        {
            //Gen where
            string where = "";
            foreach (string sttRec in sttRecArray)
            {
                where += string.Format(" or c.stt_rec = {0}", sttRec.Replace("'", "''"));
            }
            //c=AD81, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [AD81]"
                + " c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt Where " + where + " Order by c.stt_rec0";
            
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
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
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_SOF_DELETE_MAIN", plist);
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
                throw new Exception("V6Invoice81 GetGiaBan " + ex.Message);
            }
            return null;
        }
        
    }

}
