using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using V6SqlConnect;

namespace DataAccessLayer.Implementations
{
    public class InvoiceServices : IInvoiceServices
    {
        public DataTable GetAlct(string mact)
        {
            mact = mact.Replace("'", "");
            return SqlConnect.Select("Alct", "*", "ma_ct='" + mact + "'").Data;
        }

        public DataTable GetAlct1(string mact)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@list_fix", ""),
                new SqlParameter("@order_fix", ""),
                new SqlParameter("@vvar_fix", ""),
                new SqlParameter("@type_fix", ""),
                new SqlParameter("@checkvvar_fix", ""),
                new SqlParameter("@notempty_fix", ""),
                new SqlParameter("@fdecimal_fix", "")
            };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                    "VPA_GET_AUTO_COLULMN", plist).Tables[0];
        }

        public DataTable GetAlct3(string mact)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@list_fix", ""),
                new SqlParameter("@order_fix", ""),
                new SqlParameter("@vvar_fix", ""),
                new SqlParameter("@type_fix", ""),
                new SqlParameter("@checkvvar_fix", ""),
                new SqlParameter("@notempty_fix", ""),
                new SqlParameter("@fdecimal_fix", "")
            };

            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                    "VPA_GET_AUTO_COLUMN_KT", plist).Tables[0];
        }

        public DataTable GetAlnt()
        {
            return SqlConnect.Select("Alnt", null, "", "", "ma_nt").Data;
        }
        
        public DataTable GetAlPost(string mact)
        {
            return SqlConnect.Select("AlPost", "Kieu_post,Ten_post,Ten_post2,ColorV",
                "Ma_ct=@mact", "", "Kieu_post", new SqlParameter("@mact", mact)).Data;
        }

        public void PostErrorLog(string mact, string sttRec, string mode, string message)
        {
            try
            {
                SqlParameter[] plist = {
                    new SqlParameter("@ma_ct", mact),
                    new SqlParameter("@stt_rec", sttRec),
                    new SqlParameter("@mode", mode), 
                    new SqlParameter("@message", message), 
                    new SqlParameter("@message_id", 1)
                };
                SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_V6_PostErrorLog", plist);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public decimal GetTyGia(string mant, DateTime ngayct)
        {
            try
            {
                SqlParameter[] pList =
                {
                    new SqlParameter("@ma_nt", mant),
                    new SqlParameter("@ngay_ct", ngayct.ToString("yyyyMMdd"))
                };

                var resultValue = Convert.ToDecimal(
                    SqlConnect.ExecuteScalar(CommandType.Text, "Select dbo.VFA_GetRates(@ma_nt, @ngay_ct)", pList));
                return resultValue;
            }
            catch (Exception)
            {
                // ignored
            }
            return 0;
        }

        public DataSet GetCheck_VC(string status, string kieuPost, string sttRec, out string message)
        {
            try
            {
                SqlParameter[] pList =
                {
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@Stt_rec", sttRec)
                };
                message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                        "VPA_TA1_CHECK_VC", pList);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
        
        public DataTable GetCheck_VC_Save(string status, string kieuPost, string soct, string masonb, string sttrec,
            out string v6Message)
        {
            try
            {
                SqlParameter[] plist =
                {     
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@So_ct", soct),
                    new SqlParameter("@Ma_sonb", masonb), 
                    new SqlParameter("@Stt_rec",sttrec)
                };
                v6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_VC_SAVE", plist).Tables[0];
            }
            catch (Exception ex)
            {
                v6Message = ex.Message;
                return null;
            }
        }
        public DataTable GetCheck_Save_All(string status, string kieuPost, string soct, string masonb, string sttrec, string madvcs, string makh,
            string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id, out string v6Message)
        {
            try
            {
                SqlParameter[] plist =
                {     
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@So_ct", soct),
                    new SqlParameter("@Ma_sonb", masonb), 
                    new SqlParameter("@Ma_dvcs", madvcs), 
                    new SqlParameter("@Ma_kh", makh), 
                    new SqlParameter("@Ma_nx", manx), 
                    new SqlParameter("@Ngay_ct",  ngayct.ToString("yyyyMMdd")), 
                    new SqlParameter("@Ma_ct", mact), 
                    new SqlParameter("@T_tt", tongthanhtoan), 
                    new SqlParameter("@Stt_rec",sttrec),
                    new SqlParameter("@Mode",mode),
                    new SqlParameter("@User_id",user_id)
                };
                v6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_SAVE_ALL", plist).Tables[0];
            }
            catch (Exception ex)
            {
                v6Message = ex.Message;
                return null;
            }
        }
        public DataTable GetCheck_Edit_All(string status, string kieuPost, string soct, string masonb, string sttrec, string madvcs, string makh,
           string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id, out string v6Message)
        {
            try
            {
                SqlParameter[] plist =
                {     
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@So_ct", soct),
                    new SqlParameter("@Ma_sonb", masonb), 
                    new SqlParameter("@Ma_dvcs", madvcs), 
                    new SqlParameter("@Ma_kh", makh), 
                    new SqlParameter("@Ma_nx", manx), 
                    new SqlParameter("@Ngay_ct",  ngayct.ToString("yyyyMMdd")), 
                    new SqlParameter("@Ma_ct", mact), 
                    new SqlParameter("@T_tt", tongthanhtoan), 
                    new SqlParameter("@Stt_rec",sttrec),
                    new SqlParameter("@Mode",mode),
                    new SqlParameter("@User_id",user_id)
                };
                v6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_EDIT_ALL", plist).Tables[0];
            }
            catch (Exception ex)
            {
                v6Message = ex.Message;
                return null;
            }
        }
        public void IncreaseSl_inAm81(string sttRec)
        {
            var sql = "Update Am81 Set Sl_in = Sl_in+1 Where Stt_rec=@p";
            SqlConnect.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@p", sttRec));
        }
    }
}
