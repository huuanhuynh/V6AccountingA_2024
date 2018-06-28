using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using V6Init;
using V6SqlConnect;
using V6Tools;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// SOA: Hóa đơn bán hàng kiêm phiếu xuất
    /// </summary>
    public class V6Invoice81 : V6InvoiceBase
    {
        //private readonly Invoice81Services Service81 = new Invoice81Services();

        public V6Invoice81():base("SOA", "00SOA")
        {
            
        }

        public override string PrintReportProcedure
        {
            get { return "ASOCTSOA"; }
        }

        public override string Name { get { return "Hóa đơn"; } }

        
        public bool InsertInvoice(
            SortedDictionary<string, object> amData,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList3)
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
                foreach (SortedDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("InsertInvoice81 {0} AD row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, adSql));
                    }
                    j += (execute > 0 ? 1 : 0);
                }
                foreach (SortedDictionary<string, object> adRow in adList3)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("InsertInvoice81 {0} AD3 row {1} result {2}.\n{3}",
                            stt_rec, stt_rec0, execute, adSql));
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
                    Logger.WriteExLog("UpdateInvoice81 TRANSACTION ROLLBACK_ERROR " + stt_rec, exRollback, "");
                }

                Logger.WriteExLog("InsertInvoice81 Exception", ex, "");
                V6Message = "Rollback: "
                    + (!insert_success ? "Thêm AM không thành công." : "")
                    + (j != adList.Count ? "Thêm AD không hoàn tất." : "")
                    + (j3 != adList3.Count ? "Thêm AD3 không hoàn tất." : "");
                #endregion Rollback

                return false;
            }

            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (V6Setting.WriteExtraLog)
                {
                    Logger.WriteToLog(string.Format("InsertInvoice81 {0} TRANSACTION COMMITTED.", stt_rec));
                }
                int apgia = 0;
                SqlParameter[] pList =
                {
                    new SqlParameter("@Stt_rec", amData["STT_REC"].ToString()),
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
                    var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOA_POST_MAIN", pList);
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
                                new SqlParameter("@Stt_rec", amData["STT_REC"].ToString()),
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
                            var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOA_POST_MAIN",
                                pList);
                            V6Message = string.Format("Success, ({0} affected).", result);
                            return true;
                        }
                        catch (Exception ex2)
                        {
                            V6Message = ex2.Message;
                            V6Message = "POST2 lỗi: " + V6Message;
                            return false;
                        }//end catch2
                    }
                    else
                    {
                        V6Message = "POST lỗi: " + V6Message;
                        return false;
                    }
                }// end catch1
            }
            else
            {
            
            }
            return false;
        }


        public bool UpdateInvoice(
            SortedDictionary<string, object> amData,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList3,
            SortedDictionary<string, object> keys)
        {
            #region === Local variable ===
            object stt_rec = amData["STT_REC"];
            bool insert_success = false;
            int j = 0, j3 = 0;
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Update81");
            #endregion local variable

            #region === Insert ===
            
            try
            {
                #region === Delete AD, Update AM, Insert AD ===

                var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AMStruct.TableName, AMStruct, amData, keys);
                //Delete AD
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AD3
                var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
                //Update AM
                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                //Insert AD
                foreach (SortedDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow, false);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("UpdateInvoice81 {0} AD row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, adSql));
                    }
                    j += (execute > 0 ? 1 : 0);
                }
                //Insert AD3
                foreach (SortedDictionary<string, object> adRow in adList3)
                {
                    var ad3Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad3Sql);
                    if (V6Setting.WriteExtraLog)
                    {
                        object stt_rec0 = adRow["STT_REC0"];
                        Logger.WriteToLog(string.Format("UpdateInvoice81 {0} AD3 row {1} result {2}.\n{3}", stt_rec,
                            stt_rec0, execute, ad3Sql));
                    }
                    j3 += (execute > 0 ? 1 : 0);
                }

                #endregion Delete AD, Update AM, Insert AD
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
                    Logger.WriteExLog("UpdateInvoice81 TRANSACTION ROLLBACK_ERROR " + stt_rec, exRollback, "");
                }

                Logger.WriteExLog("UpdateInvoice81 Exception", ex, "");
                V6Message = "Rollback: "
                            + (!insert_success ? "Sửa AM không thành công." : "")
                            + (j != adList.Count ? "Thêm AD không hoàn tất." : "")
                            + (j3 != adList3.Count ? "Thêm AD3 không hoàn tất." : "");
                #endregion Rollback
                return false;
            }
            #endregion Insert

            #region === POST ===
            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (V6Setting.WriteExtraLog)
                {
                    Logger.WriteToLog(string.Format("UpdateInvoice81 {0} TRANSACTION COMMITTED.", stt_rec));
                }

                try
                {
                    int apgia = 0;
                    SqlParameter[] pList;

                    try
                    {
                        #region === POST1 ===
                        pList = new[]
                        {
                            new SqlParameter("@Stt_rec", amData["STT_REC"].ToString()),
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
                        var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOA_POST_MAIN", pList);
                        V6Message = string.Format("Success, ({0} affected).", result);
                        #endregion POST
                        return true;
                    }
                    catch (Exception exPost1)
                    {
                        Logger.WriteExLog("UpdateInvoice81 POST1", exPost1, "");
                        V6Message = exPost1.Message;
                        if (V6Message.Contains("Rerun the transaction."))
                        {
                            Thread.Sleep(3000);
                            
                            try
                            {
                                #region === POST2 ===
                                pList = new[]
                                {
                                    new SqlParameter("@Stt_rec", amData["STT_REC"].ToString()),
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
                                var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOA_POST_MAIN",
                                    pList);
                                V6Message = string.Format("Success, ({0} affected).", result);
                                #endregion RePOST
                                return true;
                            }
                            catch (Exception exPost2)
                            {
                                Logger.WriteExLog("UpdateInvoice81 POST2", exPost1, "");
                                V6Message = exPost2.Message;
                                V6Message = "POST2 lỗi: " + V6Message;
                            } //end catch2
                        }
                        else
                        {
                            V6Message = "POST lỗi: " + V6Message;
                        }
                    } // end catch1
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = "POST lỗi: " + V6Message;
                }
            }
            #endregion POST
            return false;
        }


        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Dien_thoai, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM " + AM_TableName + " a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
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

        /// <summary>
        /// Lấy dữ liệu chi tiết theo sttRec
        /// </summary>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public DataTable LoadAd81(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [" + AD_TableName
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt  Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        /// <summary>
        /// Lấy dữ liệu chi tiết 3 theo sttRec
        /// </summary>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public DataTable LoadAD3(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk FROM " + AD3_TableName
                + " c LEFT JOIN Altk d ON c.Tk_i= d.Tk Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        
        public bool DeleteInvoice(string sttrec)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Stt_rec", sttrec), 
                new SqlParameter("@Ma_ct", Mact),
                new SqlParameter("@UserID", V6Login.UserId)
            };
            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOA_DELETE_MAIN", plist);
            return true;
        }

        public DataRow GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {
            try
            {
                //return "{gia_ban_nt:" + data.Rows[0]["gia_ban_nt"] + ",gia_nt2:" + data.Rows[0]["gia_nt2"] + ",error:0,message:''}";
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
                throw new Exception("V6Invoice81 GetGiaBan " + ex.Message);
            }
        }
        
    }

}
