using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// TA1, BC1: Phiếu thu(41), Báo có(46)
    /// </summary>
    public class V6Invoice41 : V6InvoiceBase
    {
        public V6Invoice41():base("TA1")
        {
            
        }
        /// <summary>
        /// mact="TA1":Phiếu thu(41), "BC1":Báo có(46)
        /// </summary>
        /// <param name="mact"></param>
        public V6Invoice41(string mact):base(mact)
        {
        }

        public override string PrintReportProcedure
        {
            get
            {
                if (Mact == "TA1") return "ACACTTA1";
                if (Mact == "BC1") return "ACACTBC1";
                return "";
            }
        }

        /// <summary>
        /// Ẩn đi thuộc tính này
        /// </summary>
        private DataTable Alct1
        {
            get { return null; }
        }
        
        public DataTable GetAlct1(string maGd)
        {
            SqlParameter[] pList =
            {
                new SqlParameter("@ma_ct", Mact),
                new SqlParameter("@ma_gd", maGd),
                new SqlParameter("@list_fix", ""),
                new SqlParameter("@order_fix", ""),
                new SqlParameter("@vvar_fix", ""),
                new SqlParameter("@type_fix", ""),
                new SqlParameter("@checkvvar_fix", ""),
                new SqlParameter("@notempty_fix", ""),
                new SqlParameter("@fdecimal_fix", "")
            };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                "VPA_GET_AUTO_COLULMN_MA_GD", pList).Tables[0];
        }

        public override bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3)
        {
            object stt_rec = amData["STT_REC"];
            bool insert_success = false;
            int j = 0, j3 = 0;
            var amSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, amData);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AM_TableName);

            try
            {
                //Delete AD
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
                {
                    {"STT_REC", stt_rec}
                };
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AD3
                var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
                //Delete AM
                var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);

                insert_success = (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0);
                var currentMethodName = MethodBase.GetCurrentMethod().Name;
                j = InsertADlist(currentMethodName, TRANSACTION, adList, true);
                j3 = InsertAD3list(currentMethodName, TRANSACTION, adList3, true);
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
                    Logger.WriteExLog(string.Format("{0} {1} TRANSACTION ROLLBACK_ERROR {2}", GetType(), MethodBase.GetCurrentMethod().Name, stt_rec), exRollback, "");
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
                WriteLogTransactionComplete(stt_rec);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", stt_rec),
                        new SqlParameter("@Ma_ct", amData["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", amData["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@Tk", amData["TK"].ToString()),
                        new SqlParameter("@Ma_gd", amData["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", amData["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_TA1_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = V6Text.Text("POSTLOI") + V6Message;
                    
                    return false;
                }
            }
            else
            {
                if (!insert_success) V6Message = V6Text.Text("AAMUNSUCCESS");
                if (j != adList.Count) V6Message += V6Text.Text("ADNOTCOMPLETE");
                if (j3 != adList3.Count) V6Message += V6Text.Text("AD3NOTCOMPLETE");
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        public bool UpdateInvoice(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3,
            IDictionary<string, object> keys)
        {
            object stt_rec = amData["STT_REC"];
            bool insert_success = false;
            int j = 0, j3 = 0;
            //POST MAIN BEFORE
            int apgia0 = 0;
            SqlParameter[] pList0 =
                    {
                        new SqlParameter("@Stt_rec", stt_rec),
                        new SqlParameter("@Ma_ct", amData["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", amData["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@Tk", amData["TK"].ToString()),
                        new SqlParameter("@Ma_gd", amData["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", amData["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia0),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                        
                    };
            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_TA1_POST_MAIN_BEFORE", pList0);

            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM_TableName, AMStruct, amData, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AM81Update");

            try
            {
                //Delete AD
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                //Delete AD3
                var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
                //Update AM //??? co nen theo doi nhung thay doi tren form va truyen valueDic vua đủ.
                insert_success = (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0);
                var currentMethodName = MethodBase.GetCurrentMethod().Name;
                j = InsertADlist(currentMethodName, TRANSACTION, adList, false);
                j3 = InsertAD3list(currentMethodName, TRANSACTION, adList3, false);
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
                    Logger.WriteExLog(string.Format("{0} {1} TRANSACTION ROLLBACK_ERROR {2}", GetType(), MethodBase.GetCurrentMethod().Name, stt_rec), exRollback, "");
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
                WriteLogTransactionComplete(stt_rec);
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", stt_rec),
                        new SqlParameter("@Ma_ct", amData["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", amData["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@Tk", amData["TK"].ToString()),
                        new SqlParameter("@Ma_gd", amData["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", amData["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_TA1_POST_MAIN", pList);
                    return true;
                }
                catch (Exception ex)
                {
                    V6Message = ex.Message;
                    V6Message = V6Text.Text("POSTLOI") + V6Message;
                    
                    return false;
                }
            }
            else
            {
                if (!insert_success) V6Message = V6Text.Text("AAMUNSUCCESS");
                if (j != adList.Count) V6Message += V6Text.Text("ADNOTCOMPLETE");
                if (j3 != adList3.Count) V6Message += V6Text.Text("AD3NOTCOMPLETE");
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2Dvcs,
            string where3AD, string where4NhVt)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien"
                + AMSELECTMORE
                + "\nFROM "+AM_TableName+" a LEFT JOIN ALkh AS b ON a.Ma_kh = b.Ma_kh LEFT JOIN alnvien  AS f ON a.Ma_nvien = f.Ma_nvien "
                + AMJOINMORE
                + "\n JOIN "
                + "\n (SELECT Stt_rec FROM " + AM_TableName + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2} {3}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";

            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            if (where2Dvcs.Length > 0) where2Dvcs=" And "+ where2Dvcs;


            var p2Template ="\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD_TableName + " WHERE Ma_ct = '" + Mact + "' {0} {3} {4})";
                
            if (where3AD.Length > 0 || where4NhVt.Length > 0 || where2Dvcs.Length > 0)
            {
                if (where3AD.Length > 0) where3AD = "And " + where3AD;
                if (where4NhVt.Length > 0) where4NhVt = "And " + where4NhVt;

                p2Template = string.Format(p2Template, where0Ngay, "1", "2", where3AD, where4NhVt);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, where2Dvcs, p2Template);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }

        public override DataTable LoadAD(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk_i,k.Ten_kh AS Ten_kh_i" + ADSELECTMORE + " FROM " + AD_TableName
                + " c LEFT JOIN Altk d ON c.tk_i= d.tk LEFT JOIN Alkh k ON c.ma_kh_i= k.ma_kh ";
            sql += string.IsNullOrEmpty(sttRec) ? " Where 1=0" : " Where c.stt_rec=@rec";
            sql += " Order by c.stt_rec0";
            SqlParameter[] listParameters = { new SqlParameter("@rec", sttRec) };
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }

        public DataTable LoadAd3(string sttRec)
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
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_TA1_DELETE_MAIN", plist);
                return true;
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
        }

        public DataTable Alct0;

        public DataTable GetSoct0(string sttRec, string makh, string madvcs)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@ma_kh", makh), 
                new SqlParameter("@ma_dvcs", madvcs)
            };
            var result = V6BusinessHelper.ExecuteProcedure("ACACTTA1_InitTt", plist).Tables[0];
            Alct0 = result;
            return Alct0;
        }

        public override DataTable GetSoct0_All_Cust(string sttRec, string madvcs, string filterString)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@ma_dvcs", madvcs),
                new SqlParameter("@advance", filterString)
            };
            var result = V6BusinessHelper.ExecuteProcedure("ACACTTA1_CUSTS_InitTt", plist).Tables[0];
            Alct0 = result;
            return Alct0;
        }

        public override DataTable GetSodu0_All_Cust(string sttRec, string madvcs, string filterString)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@ma_dvcs", madvcs),
                new SqlParameter("@advance", filterString)
            };
            var result = V6BusinessHelper.ExecuteProcedure("ACACTTA1_CUSTS_InitSd", plist).Tables[0];
            Alct0 = result;
            return Alct0;
        }

        public override DataTable GetSoDu0TK_All_Cust(string sttRec, string madvcs, DateTime ngay_ct, string filterString)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@ma_dvcs", madvcs),
                new SqlParameter("@ngay_ct", ngay_ct.ToString("yyyyMMdd")),
                new SqlParameter("@advance", filterString)
            };
            var result = V6BusinessHelper.ExecuteProcedure("ACACTTA1_CUSTS_InitSd_TK", plist).Tables[0];
            Alct0 = result;
            return Alct0;
        }

        public DataRow GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {
            try
            {
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
        
    }

}
