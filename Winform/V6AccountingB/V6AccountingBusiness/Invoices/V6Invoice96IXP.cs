using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// IXP: Phiếu duyệt xuất bán
    /// </summary>
    public class V6Invoice96IXP : V6InvoiceBase
    {
        public V6Invoice96IXP() : base("IXP") { }

        public override string PrintReportProcedure
        {
            get { return "ASOCTIXP"; }
        }

        public override bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList)
        {
            var stt_rec = amData["STT_REC"];
            var insert_success = false;
            var j = 0;
            var insert_am_sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, amData);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AM_TableName);

            try
            {
                //Delete AD
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
                {
                    {"STT_REC", amData["STT_REC"]}
                };
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AM
                var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);


                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
                var currentMethodName = MethodBase.GetCurrentMethod().Name;
                j = InsertADlist(currentMethodName, TRANSACTION, adList, true);
            }
            catch (Exception ex)
            {
                #region === Rollback ===

                try
                {
                    TRANSACTION.Rollback();
                }
                catch (Exception exRollback)
                {
                    Logger.WriteExLog(
                        string.Format("{0} {1} TRANSACTION ROLLBACK_ERROR {2}", GetType(),
                            MethodBase.GetCurrentMethod().Name, stt_rec), exRollback, "");
                }

                Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " Exception", ex, "");
                V6Message = "Rollback: "
                            + (!insert_success ? V6Text.Text("AAMUNSUCCESS") : "")
                            + (j != adList.Count ? V6Text.Text("ADNOTCOMPLETE") : "");

                #endregion Rollback

                return false;
            }

            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(stt_rec);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", stt_rec),
                        new SqlParameter("@Ma_ct", amData["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", amData["MA_NT"].ToString()),
                        new SqlParameter("@Ma_nx", amData["MA_NX"].ToString()),
                        new SqlParameter("@Loai_ck", amData["LOAI_CK"].ToString()),
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@nKieu_Post", amData["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXP_POST_MAIN", pList);

                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = V6Text.Text("POSTLOI") + V6Message;

                    return false;
                }
            }
            else // insert không đủ dòng.
            {
                TRANSACTION.Commit();
                if (!insert_success) V6Message = V6Text.Text("AAMUNSUCCESS");
                if (j != adList.Count) V6Message += V6Text.Text("ADNOTCOMPLETE");
                //if (j2 != adList2.Count) V6Message += V6Text.Text("AD2NOTCOMPLETE");
                //if (j3 != adList3.Count) V6Message += V6Text.Text("AD3NOTCOMPLETE");
                Logger.WriteToLog(string.Format("{0} Invoice81.InsertInvoice else.{1} {2}", V6Login.ClientName, stt_rec, V6Message));
            }
            return false;
        }

        public override bool UpdateInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList,
            IDictionary<string, object> keys)
        {
            var stt_rec = amData["STT_REC"];
            var insert_success = false;
            var j = 0;
            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM_TableName, AMStruct, amData, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AM81Update");

            try
            {
                //Delete AD
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                //Update AM
                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                var currentMethodName = MethodBase.GetCurrentMethod().Name;
                j = InsertADlist(currentMethodName, TRANSACTION, adList, false);
            }
            catch (Exception ex)
            {
                #region === Rollback ===

                try
                {
                    TRANSACTION.Rollback();
                }
                catch (Exception exRollback)
                {
                    Logger.WriteExLog(
                        string.Format("{0} {1} TRANSACTION ROLLBACK_ERROR {2}", GetType(),
                            MethodBase.GetCurrentMethod().Name, stt_rec), exRollback, "");
                }

                Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " Exception", ex, "");
                V6Message = "Rollback: "
                            + (!insert_success ? V6Text.Text("EAMUNSUCCESS") : "")
                            + (j != adList.Count ? V6Text.Text("ADNOTCOMPLETE") : "");

                #endregion Rollback

                return false;
            }

            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
                WriteLogTransactionComplete(stt_rec);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", stt_rec),
                        new SqlParameter("@Ma_ct", amData["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", amData["MA_NT"].ToString()),
                        new SqlParameter("@Ma_nx", amData["MA_NX"].ToString()),
                        new SqlParameter("@Loai_ck", amData["LOAI_CK"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@nKieu_Post", amData["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };


                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXP_POST_MAIN", pList);

                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = V6Text.Text("POSTLOI") + V6Message;

                    return false;
                }
            }
            
            return false;
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs, string where4Dvcs_2)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt "
                + AMSELECTMORE
                + "\nFROM "+AM_TableName+" a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien "
                + AMJOINMORE
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
                if (string.IsNullOrEmpty(where4Dvcs_2))
                {
                    if (where4Dvcs.Length > 0)
                    {
                        where4Dvcs = string.Format("	And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);
                    }
                }
                else
                {
                    where4Dvcs = string.Format("	And {0}", where4Dvcs_2);
                }
                //if (where4Dvcs.Length > 0) where4Dvcs
                //    = string.Format("	And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);

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

        public override DataTable LoadAD(string sttRec)
        {
            //c=AD81, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13, c.So_luong1*0 as Ton13Qd " + ADSELECTMORE + " FROM [" + AD_TableName
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt ";
            sql += ADJOINMORE;
            sql += string.IsNullOrEmpty(sttRec) ? " Where 1=0" : " Where c.stt_rec=@rec";
            sql += " Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        
        public override bool DeleteInvoice(string sttrec)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Stt_rec", sttrec), 
                    new SqlParameter("@Ma_ct", Mact),
                    new SqlParameter("@UserID", V6Login.UserId)
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXP_DELETE_MAIN", plist);
                return true;
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
        }

        public DataTable SearchDeNghiXuat_HD(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs, string advance, out string loai_ct_chon)
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD96 WHERE Ma_ct = 'IXP' {0} {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            if (!string.IsNullOrEmpty(where3NhVt))
            {
                if (string.IsNullOrEmpty(advance))
                {
                    advance = "Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
                else
                {
                    advance += " And Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
            }

            loai_ct_chon = "I";
            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "I"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM96"), 
	            new SqlParameter("@cTableAD", "AD96"), 
	            new SqlParameter("@cKey1AM", where0Ngay),
	            new SqlParameter("@cKey2AM", where1AM),
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs),
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", advance),
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_STOCK_IXP", plist).Tables[0];
            return tbl;
        }

        public DataTable SearchDeNghiXuat_PXK(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs, string advance, out string loai_ct_chon)
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD96 WHERE Ma_ct = 'IXP' {0} {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            if (!string.IsNullOrEmpty(where3NhVt))
            {
                if (string.IsNullOrEmpty(advance))
                {
                    advance = "Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
                else
                {
                    advance += " And Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
            }

            loai_ct_chon = "I";
            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "I"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM96"), 
	            new SqlParameter("@cTableAD", "AD96"), 
	            new SqlParameter("@cKey1AM", where0Ngay),
	            new SqlParameter("@cKey2AM", where1AM),
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs),
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", advance),
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_STOCK_IXP", plist).Tables[0];
            return tbl;
        }
        
        public DataTable SearchDeNghiXuat_PXDC(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs, string advance, out string loai_ct_chon)
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD96 WHERE Ma_ct = 'IXP' {0} {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            if (!string.IsNullOrEmpty(where3NhVt))
            {
                if (string.IsNullOrEmpty(advance))
                {
                    advance = "Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
                else
                {
                    advance += " And Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
            }

            loai_ct_chon = "I";
            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "I"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM96"), 
	            new SqlParameter("@cTableAD", "AD96"), 
	            new SqlParameter("@cKey1AM", where0Ngay),
	            new SqlParameter("@cKey2AM", where1AM),
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs),
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", advance),
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_STOCK_IXB_IXP", plist).Tables[0];
            return tbl;
        }
        
        public DataTable SearchPhieuXuatAll(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD84 WHERE Ma_ct = 'IXA' {0} {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
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



            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
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
                throw new Exception("V6Invoice96IXP GetGiaBan " + ex.Message);
            }
            return null;
        }
        public DataTable SearchDonHangBanAll(DateTime ngayCt, string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs, string advance, out string loai_ct_chon)
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


                whereAD_Nhvt_Dvcs = string.Format("\n Where d.Stt_rec in (SELECT Stt_rec FROM AD96 WHERE Ma_ct = 'IXP' {0} {2}"
                                     + (where3NhVt.Length == 0 ? "{3}" : "\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
                                     + "\n		{4})"
                    , where0Ngay, "1", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                whereAD_Nhvt_Dvcs = "";
            }

            if (!string.IsNullOrEmpty(where3NhVt))
            {
                if (string.IsNullOrEmpty(advance))
                {
                    advance = "Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
                else
                {
                    advance += " And Ma_vt IN (Select ma_vt from Alvt where 1=1 " + where3NhVt + ")";
                }
            }

            loai_ct_chon = "S";
            SqlParameter[] plist =
            {
                new SqlParameter("@sType",  "S"),
	            new SqlParameter("@dFrom",  ngayCt.ToString("yyyyMMdd")),
	            new SqlParameter("@cTableAM", "AM96"), 
	            new SqlParameter("@cTableAD", "AD96"), 
	            new SqlParameter("@cKey1AM", where0Ngay),
	            new SqlParameter("@cKey2AM", where1AM),
	            new SqlParameter("@cKey1AD", whereAD_Nhvt_Dvcs),
	            new SqlParameter("@cKey2AD", ""),
	            new SqlParameter("@Advance", advance),
	            new SqlParameter("@Advance2", "")
            };
            var tbl = V6BusinessHelper.ExecuteProcedure("VPA_GET_ALL_IXP", plist).Tables[0];
            return tbl;
        }

    }

}
