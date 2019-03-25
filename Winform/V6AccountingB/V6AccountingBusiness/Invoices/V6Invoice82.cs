using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using V6Init;
using V6SqlConnect;
using V6Tools;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// SOB: Hóa đơn dịch vụ có số lượng
    /// </summary>
    public class V6Invoice82 : V6InvoiceBase
    {
        /// <summary>
        /// SOB: Hóa đơn dịch vụ có số lượng
        /// </summary>
        public V6Invoice82():base("SOB", "00SOB")
        {
            
        }

        public override string PrintReportProcedure
        {
            get { return "ASOCTSOB"; }
        }

        public override bool InsertInvoice(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList3)
        {
            object stt_rec = amData["STT_REC"];
            bool insert_success = false;
            int j = 0, j3 = 0;
            var insert_am_sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, amData);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AMStruct.TableName);
            
            try
            {
                //Delete AD
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
                {
                    {"STT_REC", stt_rec}
                };
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                deleteAdSql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AM
                var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);

                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;

                foreach (IDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("InsertInvoice82 {0} AD row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, adSql));
                    }
                    j += (execute > 0 ? 1 : 0);
                }
                foreach (IDictionary<string, object> adRow in adList3)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("InsertInvoice82 {0} AD3 row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, adSql));
                    }
                    j3 += (execute > 0 ? 1 : 0);
                }
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
                    Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " TRANSACTION ROLLBACK_ERROR " + stt_rec, exRollback, "");
                }

                Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " Exception", ex, "");
                V6Message = "Rollback: "
                    + (!insert_success ? V6Text.Text("AAMUNSUCCESS") : "")
                    + (j != adList.Count ? V6Text.Text("ADNOTCOMPLETE") : "")
                    + (j3 != adList3.Count ? V6Text.Text("AD3NOTCOMPLETE") : "");
                #endregion Rollback

                return false;
            }

            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (V6Setting.WriteExtraLog)
                {
                    Logger.WriteToLog(string.Format("InsertInvoice82 {0} TRANSACTION COMMITTED.", stt_rec));
                }
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

                try
                {
                    var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN", pList);
                    V6Message = string.Format("Success, ({0} affected).", result);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    if (V6Message.Contains("Rerun the transaction."))
                    {
                        Thread.Sleep(3000);
                        try
                        {
                            pList = new[]
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
                            var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN",
                                pList);
                            V6Message = string.Format("Success, ({0} affected).", result);
                            return true;
                        }
                        catch (Exception ex2)
                        {
                            V6Message = ex2.Message;
                            V6Message = V6Text.Text("POST2LOI") + V6Message;
                            return false;
                        }//end catch2
                    }
                    else
                    {
                        V6Message = V6Text.Text("POSTLOI") + V6Message;
                        return false;
                    }
                }// end catch1
            }
            
            return false;
        }

        public bool UpdateInvoice(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList3,
            IDictionary<string, object> keys)
        {
            object stt_rec = amData["STT_REC"];
            bool insert_success = false;
            int j = 0, j3 = 0;
            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AMStruct.TableName, AMStruct, amData, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Update82");

            try
            {
                //Delete AD
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AD3
                var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);

                //Update AM
                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                
                //Insert AD
                foreach (IDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow, false);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format(GetType() + " " + MethodBase.GetCurrentMethod().Name + " {0} AD row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, adSql));
                    }
                    j += (execute > 0 ? 1 : 0);
                }
                //Insert AD3
                foreach (IDictionary<string, object> adRow in adList3)
                {
                    var ad3Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad3Sql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format(GetType() + " " + MethodBase.GetCurrentMethod().Name + " {0} AD3 row {1} result {2}.\n{3}",
                            stt_rec, stt_rec0, execute, ad3Sql));
                    }
                    j3 += (execute > 0 ? 1 : 0);
                }
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
                    Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " TRANSACTION ROLLBACK_ERROR " + stt_rec, exRollback, "");
                }

                Logger.WriteExLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " Exception", ex, "");
                V6Message = "Rollback: "
                          + (!insert_success ? V6Text.Text("EAMUNSUCCESS") : "")
                          + (j != adList.Count ? V6Text.Text("ADNOTCOMPLETE") : "")
                          + (j3 != adList3.Count ? V6Text.Text("AD3NOTCOMPLETE") : "");
                #endregion Rollback
                return false;
            }

            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (V6Setting.WriteExtraLog)
                {
                    Logger.WriteToLog(GetType() + " " + MethodBase.GetCurrentMethod().Name + " TRANSACTION COMMITTED " + stt_rec);
                }
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

                    try
                    {
                        var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN", pList);
                        V6Message = string.Format("Success, ({0} affected).", result);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        V6Message = ex.Message;
                        if (V6Message.Contains("Rerun the transaction."))
                        {
                            Thread.Sleep(3000);
                            try
                            {
                                pList = new[]
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
                                var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN",
                                    pList);
                                V6Message = string.Format("Success, ({0} affected).", result);
                                return true;
                            }
                            catch (Exception ex2)
                            {
                                V6Message = ex2.Message;
                                V6Message = V6Text.Text("POST2LOI") + V6Message;
                                return false;
                            }//end catch2
                        }
                        else
                        {
                            V6Message = V6Text.Text("POSTLOI") + V6Message;
                            return false;
                        }
                    }// end catch1
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

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            var filterKho = V6Login.GetFilterKho("MA_KHO");
            if (!string.IsNullOrEmpty(filterKho))
            {
                where4Dvcs += (string.IsNullOrEmpty(where4Dvcs) ? "" : " and ") + filterKho;
            }
            
            string template =
                "Select a.*, b.Ma_so_thue, b.Dien_thoai, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + AMSELECTMORE
                + "\nFROM " + AM_TableName + " a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien "
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
                + "\n	{4})";//" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 {4})"

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

        public override DataTable LoadAD(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13" + ADSELECTMORE + " FROM [" + AD_TableName
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt ";
            sql += string.IsNullOrEmpty(sttRec) ? " Where 1=0" : " Where c.stt_rec=@rec";
            sql += " Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        public DataTable LoadAD3(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk FROM " + AD3_TableName
                + " c LEFT JOIN Altk d ON c.Tk_i= d.Tk ";
            sql += string.IsNullOrEmpty(sttRec) ? " Where 1=0" : " Where c.stt_rec=@rec";
            sql += " Order by c.stt_rec0";
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
                SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_DELETE_MAIN", plist);
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

                var resultData = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GetSOIDPrice", plist).Tables[0];
                if (resultData != null && resultData.Rows.Count >= 1)
                {
                    return resultData.Rows[0];
                }
                else
                {
                    throw new Exception("GetGiaBan return null.");
                }
            }
            catch (Exception ex)
            {
                //return "{gia_ban_nt:0,gia_nt2:0,error:1,message:\"GetPrice: " + ToJSstring(ex.Message) + "\"}";
                throw new Exception("V6Invoice82 GetGiaBan " + ex.Message);
            }
        }
        
    }

}
