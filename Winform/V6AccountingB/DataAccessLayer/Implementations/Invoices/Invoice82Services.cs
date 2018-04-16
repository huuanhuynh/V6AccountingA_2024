using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Transactions;
using DataAccessLayer.Interfaces.Invoices;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DataAccessLayer.Implementations.Invoices
{
    public class Invoice82Services : IInvoice82Services
    {

        public bool InsertInvoice(int userId, V6TableStruct AMStruct, V6TableStruct ADStruct, V6TableStruct AD3Struct,
            SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList3,
            bool write_log, out string message)
        {
            object stt_rec = am["STT_REC"];
            var insert_am_sql = SqlGenerator.GenInsertAMSql(userId, AMStruct, am);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AMStruct.TableName);

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

            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
            int j = 0, j3 = 0;
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(userId, ADStruct, adRow);
                int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                if (write_log)
                {
                    object stt_rec0 = adRow["STT_REC0"];
                    Logger.WriteToLog(string.Format("InsertInvoice82 {0} AD row {1} result {2}.\n{3}", stt_rec, stt_rec0, execute, adSql));
                }
                j += (execute > 0 ? 1 : 0);
            }
            foreach (SortedDictionary<string, object> adRow in adList3)
            {
                var adSql = SqlGenerator.GenInsertAMSql(userId, AD3Struct, adRow);
                int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                if (write_log)
                {
                    object stt_rec0 = adRow["STT_REC0"];
                    Logger.WriteToLog(string.Format("InsertInvoice82 {0} AD3 row {1} result {2}.\n{3}", stt_rec, stt_rec0, execute, adSql));
                }
                j3 += (execute > 0 ? 1 : 0);
            }
            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (write_log)
                {
                    Logger.WriteToLog(string.Format("InsertInvoice82 {0} TRANSACTION COMMITTED.", stt_rec));
                }
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
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@Save_voucher", "1")

                };

                try
                {
                    var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN", pList);
                    message = string.Format("Success, ({0} affected).", result);
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    if (message.Contains("Rerun the transaction."))
                    {
                        Thread.Sleep(3000);
                        try
                        {
                            pList = new[]
                            {
                                new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                                new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                                new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                                new SqlParameter("@Ma_nx", am["MA_NX"].ToString()),
                                new SqlParameter("@Loai_ck", am["LOAI_CK"].ToString()),
                                new SqlParameter("@Mode", "M"),
                                new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                                new SqlParameter("@Ap_gia", apgia),
                                new SqlParameter("@UserID", userId),
                                new SqlParameter("@Save_voucher", "1")

                            };
                            var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN",
                                pList);
                            message = string.Format("Success, ({0} affected).", result);
                            return true;
                        }
                        catch (Exception ex2)
                        {
                            message = ex2.Message;
                            message = "POST2 lỗi: " + message;
                            return false;
                        }//end catch2
                    }
                    else
                    {
                        message = "POST lỗi: " + message;
                        return false;
                    }
                }// end catch1
            }
            else
            {
                TRANSACTION.Rollback();
                message = "Rollback: "
                    + (!insert_success ? "Thêm AM không thành công." : "")
                    + (j != adList.Count ? "Thêm AD không hoàn tất." : "")
                    + (j3 != adList3.Count ? "Thêm AD3 không hoàn tất." : "");
            }
            return false;
        }


        public bool UpdateInvoice(int userId, V6TableStruct AMStruct, V6TableStruct ADStruct, V6TableStruct AD3Struct,
            SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList3,
            SortedDictionary<string, object> keys,
            bool write_log,
            out string message)
        {
            object stt_rec = am["STT_REC"];
            var amSql = SqlGenerator.GenUpdateAMSql(userId, AMStruct.TableName, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Update82");

            //Delete AD
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AD3
            var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);

            //Update AM
            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
            int j = 0, j3 = 0;

            //Insert AD
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(userId, ADStruct, adRow, false);
                int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                if (write_log)
                {
                    object stt_rec0 = adRow["STT_REC0"];
                    Logger.WriteToLog(string.Format("UpdateInvoice82 {0} AD row {1} result {2}.\n{3}", stt_rec, stt_rec0, execute, adSql));
                }
                j += (execute > 0 ? 1 : 0);
            }
            //Insert AD3
            foreach (SortedDictionary<string, object> adRow in adList3)
            {
                var ad3Sql = SqlGenerator.GenInsertAMSql(userId, AD3Struct, adRow);
                int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad3Sql);
                if (write_log)
                {
                    object stt_rec0 = adRow["STT_REC0"];
                    Logger.WriteToLog(string.Format("UpdateInvoice82 {0} AD3 row {1} result {2}.\n{3}", stt_rec, stt_rec0, execute, ad3Sql));
                }
                j3 += (execute > 0 ? 1 : 0);
            }
            if (insert_success && j == adList.Count && j3 == adList3.Count)
            {
                TRANSACTION.Commit();
                if (write_log)
                {
                    Logger.WriteToLog(string.Format("UpdateInvoice82 {0} TRANSACTION COMMITTED.", stt_rec));
                }
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
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    try
                    {
                        var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN", pList);
                        message = string.Format("Success, ({0} affected).", result);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        if (message.Contains("Rerun the transaction."))
                        {
                            Thread.Sleep(3000);
                            try
                            {
                                pList = new[]
                                {
                                    new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                                    new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                                    new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                                    new SqlParameter("@Ma_nx", am["MA_NX"].ToString()),
                                    new SqlParameter("@Loai_ck", am["LOAI_CK"].ToString()),
                                    new SqlParameter("@Mode", "S"),
                                    new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                                    new SqlParameter("@Ap_gia", apgia),
                                    new SqlParameter("@UserID", userId),
                                    new SqlParameter("@Save_voucher", "1")
                                };
                                var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_POST_MAIN",
                                    pList);
                                message = string.Format("Success, ({0} affected).", result);
                                return true;
                            }
                            catch (Exception ex2)
                            {
                                message = ex2.Message;
                                message = "POST2 lỗi: " + message;
                                return false;
                            }//end catch2
                        }
                        else
                        {
                            message = "POST lỗi: " + message;
                            return false;
                        }
                    }// end catch1
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    message = "POST lỗi: " + message;
                    //TRANSACTION.Rollback();
                    return false;
                }
            }
            else
            {
                TRANSACTION.Rollback();
                message = "Rollback: "
                          + (!insert_success ? "Thêm AM không thành công." : "")
                          + (j != adList.Count ? "Thêm AD không hoàn tất." : "")
                          + (j3 != adList3.Count ? "Thêm AD3 không hoàn tất." : "");
                return false;
            }
        }

        
        public DataTable SearchAM(string tableNameAM, string tableNameAD, string mact,
            string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Dien_thoai, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM " + tableNameAM + " a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt  JOIN "
                + "\n (SELECT Stt_rec FROM " + tableNameAM + " WHERE Ma_ct = '" + mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            var p2Template =
                "\n--{0}{1}\nAnd Stt_rec in (SELECT Stt_rec FROM " + tableNameAD + " WHERE Ma_ct = '" + mact + "' {2}"
                + " {0}"
                + (where3NhVt.Length==0?"{3}":"\n	And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})")
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
        /// <param name="AD">Tên bảng</param>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public DataTable LoadAd82(string AD, string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [" + AD
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt  Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        /// <summary>
        /// Lấy dữ liệu chi tiết 3 theo sttRec
        /// </summary>
        /// <param name="AD3">Tên bảng</param>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public DataTable LoadAD3(string AD3, string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk FROM " + AD3
                + " c LEFT JOIN Altk d ON c.Tk_i= d.Tk Where c.stt_rec = @rec Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }

        public bool DeleteInvoice(int userId, string mact, string sttrec)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Stt_rec", sttrec), 
                new SqlParameter("@Ma_ct", mact),
                new SqlParameter("@UserID", userId)
            };
            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_SOB_DELETE_MAIN", plist);
            return true;
        }

        public DataTable GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
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
                return resultData;
            }
            else
            {
                throw new Exception("GetGiaBan return null.");
            }
        }

        public DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }

        public DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }

        public DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", string.Format("a.MA_VT='"+mavt+"' AND a.MA_KHO='"+makho+"'")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
        }
    }
}
