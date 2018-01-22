﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DataAccessLayer.Implementations.Invoices;
using V6Init;
using V6SqlConnect;
using V6Tools;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// IXA: Phiếu xuất kho
    /// </summary>
    public class V6Invoice84 : V6InvoiceBase
    {
        private readonly Invoice84Services SERVICE = new Invoice84Services();
        public V6Invoice84() : base("IXA") { }

        public override string PrintReportProcedure
        {
            get { return "AINCTIXA"; }
        }

        public override string Name { get { return _name; } set { _name = value; } }
        
        private string _name = "Phiếu xuất kho";

        public bool InsertInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList)
        {
            return SERVICE.InsertInvoice(V6Login.UserId, AMStruct, ADStruct,
                am, adList, WRITE_LOG, out V6Message);
        }
        
        public bool UpdateInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList,
            SortedDictionary<string,object> keys)
        {
            object stt_rec = am["STT_REC"];
            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AM84Update");
            
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
                WriteLogTransactionComplete(stt_rec);
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

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXA_POST_MAIN", pList);
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
            //where4Dvcs = "";//khong dung
            var filterKho = V6Login.GetFilterKho("MA_KHO");
            if (!string.IsNullOrEmpty(filterKho))
            {
                where4Dvcs += (string.IsNullOrEmpty(where4Dvcs) ? "" : " and ") + filterKho;
            }

            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien"
                + "\nFROM "+AM+" a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n JOIN (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            var p2Template =
                "\n--{0}{1}\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact + "' {2}"
                + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                + "\n		{4})";
            if (where2AD.Length > 0 || where3NhVt.Length > 0)// || where4Dvcs.Length > 0)
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

        public DataTable LoadAd(string sttRec)
        {
            //c=AD81, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt,c.so_luong*0 as Ton13 FROM " + AD
                + " c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt Where c.stt_rec = @rec Order by c.stt_rec0";
            SqlParameter[] listParameters = {new SqlParameter("@rec", sttRec)};
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
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXA_DELETE_MAIN", plist);
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
                throw new Exception("V6Invoice81 GetGiaBan " + ex.Message);
            }
            return null;
        }

        public DataTable SearchPhieuXuat_HoaDon(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD84 WHERE Ma_ct = 'IXA' {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , "0", "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            var sql = string.Format("Select ' ' Tag,  v.ten_vt,v.tk_dt,v.tk_gv , d.*, d.STT_REC AS STT_REC_PX, d.STT_REC0 AS STT_REC0PX "
                + "\nFROM AD84 d "
                + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
                + "\n  JOIN (SELECT Stt_rec FROM AM84 WHERE Ma_ct = 'IXA'" + "\n {0} {1}) AS m ON d.Stt_rec = m.Stt_rec"
                + "\n {2}"
                + "\n ORDER BY d.ngay_ct, d.so_ct, d.stt_rec",
                where0Ngay, where1AM, whereAD_Nhvt_Dvcs);
            //var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];

            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "I"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM84"), 
	            new SqlParameter("@cTableAD", "AD84"), 
	            new SqlParameter("@cKey1AM", where0Ngay), 
	            new SqlParameter("@cKey2AM", where1AM), 
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs), 
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", ""), 
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_STOCK_IXA", plist).Tables[0];
            return tbl;
        }

        public DataTable SearchPhieuXuat_PhieuNhapKho(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD84 WHERE Ma_ct = 'IXA' {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , "0", "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            var sql = string.Format("Select ' ' Tag,  v.ten_vt,v.tk_dt,v.tk_gv , d.*, d.STT_REC AS STT_REC_PX, d.STT_REC0 AS STT_REC0PX "
                + "\nFROM AD84 d "
                + "\n LEFT JOIN Alvt v ON v.Ma_vt = d.Ma_vt "
                + "\n  JOIN (SELECT Stt_rec FROM AM84 WHERE Ma_ct = 'IXA'" + "\n {0} {1}) AS m ON d.Stt_rec = m.Stt_rec"
                + "\n {2}"
                + "\n ORDER BY d.ngay_ct, d.so_ct, d.stt_rec",
                where0Ngay, where1AM, whereAD_Nhvt_Dvcs);
            //var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];

            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "K"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM84"), 
	            new SqlParameter("@cTableAD", "AD84"), 
	            new SqlParameter("@cKey1AM", where0Ngay), 
	            new SqlParameter("@cKey2AM", where1AM), 
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs), 
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", ""), 
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_STOCK_IXA", plist).Tables[0];
            return tbl;
        }
    }

}
