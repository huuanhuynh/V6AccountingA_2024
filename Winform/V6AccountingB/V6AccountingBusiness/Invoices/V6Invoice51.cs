﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6SqlConnect;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// CA!, BN1: Phiếu chi(51) - Báo nợ(56)
    /// </summary>
    public class V6Invoice51 : V6InvoiceBase
    {
        public V6Invoice51():base("CA1"){ }

        /// <summary>
        /// mact = CA1: Phiếu chi(51), BN1: Báo nợ(56)
        /// </summary>
        /// <param name="mact"></param>
        public V6Invoice51(string mact):base(mact)
        {
            //_mact = mact;
            if (mact == "CA1") Name = "Phiếu chi";
            else if (mact == "BN1") Name = "Báo nợ";
        }

        public override string PrintReportProcedure
        {
            get
            {
                if (Mact == "CA1") return "ACACTCA1";
                if (Mact == "BN1") return "ACACTBN1";
                return "";
            }
        }

        public override string Name { get { return _name; } set { _name = value; } }
        private string _name = "Phiếu chi";

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
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList2,
            List<SortedDictionary<string, object>> adList3
            )
        {
            V6Message = "InsertInvoice, begin " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var amSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AMStruct, am);
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
            //Delete AD3
            var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
            //Delete AM
            var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);

            var insert_success = (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0);
            int j = 0, j2 = 0, j3 = 0;
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow);
                j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql)>0?1:0);
            }
            foreach (SortedDictionary<string, object> adRow in adList2)
            {
                var ad2Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD2Struct, adRow);
                j2 += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad2Sql) > 0 ? 1 : 0);
            }
            foreach (SortedDictionary<string, object> adRow in adList3)
            {
                var ad3Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                j3 += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad3Sql) > 0 ? 1 : 0);
            }

            if (insert_success && j == adList.Count && j2 == adList2.Count && j3 == adList3.Count)
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
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@Tk", am["TK"].ToString()),
                        new SqlParameter("@Ma_gd", am["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };
                    V6Message = "Insert ok, begin Comit " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CA1_POST_MAIN", pList);
                    V6Message = "Insert ok, end Comit " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
                if (j2 != adList2.Count) V6Message += "Thêm AD2 không hoàn tất.";
                if (j3 != adList3.Count) V6Message += "Thêm AD3 không hoàn tất.";
                V6Message += " Bắt đầu RollBack.";
                TRANSACTION.Rollback();
                V6Message += " RollBack xong.";
                return false;
            }
        }
        
        public bool UpdateInvoice(SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList2,
            List<SortedDictionary<string, object>> adList3,
            SortedDictionary<string,object> keys )
        {
            //POST MAIN BEFORE
            int apgia0 = 0;
            SqlParameter[] pList0 =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@Tk", am["TK"].ToString()),
                        new SqlParameter("@Ma_gd", am["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia0),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };
            V6Message = "UpdateInvoice Start VPA_CA1_POST_MAIN_BEFORE " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CA1_POST_MAIN_BEFORE", pList0);
            V6Message = "UpdateInvoice Start VPA_CA1_POST_MAIN_BEFORE Finish " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            V6Message = "GenUpdateAMSql " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var amSql = SqlGenerator.GenUpdateAMSql(V6Login.UserId, AM, AMStruct, am, keys);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Invoice51Update");
            //Delete AD
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AD2
            var deleteAd2Sql = SqlGenerator.GenDeleteSql(AD2Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd2Sql);
            //Delete AD3
            var deleteAd3Sql = SqlGenerator.GenDeleteSql(AD3Struct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);

            //Update AM //??? co nen theo doi nhung thay doi tren form va truyen valueDic vua đủ.
            var insert_success = (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0 );
            int j = 0, j2 = 0, j3 = 0;
            //Insert AD
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, ADStruct, adRow);
                j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
            }
            //Insert AD2
            foreach (SortedDictionary<string, object> adRow in adList2)
            {
                var ad2Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD2Struct, adRow, false);
                j2 += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad2Sql) > 0 ? 1 : 0);
            }
            //Insert AD3
            foreach (SortedDictionary<string, object> adRow in adList3)
            {
                var ad3Sql = SqlGenerator.GenInsertAMSql(V6Login.UserId, AD3Struct, adRow);
                j3 += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, ad3Sql) > 0 ? 1 : 0);
            }

            if (insert_success && j == adList.Count && j2 == adList2.Count && j3 == adList3.Count)
            {
                V6Message = "Update ok, begin Comit " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                TRANSACTION.Commit();
                V6Message = "Update ok, end Comit " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                try
                {
                    int apgia = 0;
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "S"),
                        new SqlParameter("@Tk", am["TK"].ToString()),
                        new SqlParameter("@Ma_gd", am["MA_GD"].ToString()),
                        new SqlParameter("@nRows", adList.Count),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@Ap_gia", apgia),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };
                    V6Message = "VPA_CA1_POST_MAIN begin " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CA1_POST_MAIN", pList);
                    V6Message = "VPA_CA1_POST_MAIN end " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
                if (j2 != adList2.Count) V6Message += "Thêm AD2 không hoàn tất.";
                if (j3 != adList3.Count) V6Message += "Thêm AD3 không hoàn tất.";
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
                + "\nFROM "+AM+" a LEFT JOIN ALkh AS b ON a.Ma_kh = b.Ma_kh LEFT JOIN alnvien  AS f ON a.Ma_nvien = f.Ma_nvien"
                + "\n JOIN "
                + "\n (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2} {3}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";

            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            if (where2Dvcs.Length > 0) where2Dvcs=" And "+ where2Dvcs;


            var p2Template ="\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact + "' {0} {1})";
            
            if (where3AD.Length > 0 || where4NhVt.Length > 0 || where2Dvcs.Length > 0)
            {
                if (where3AD.Length > 0) where3AD = "And " + where3AD;
                if (where4NhVt.Length > 0) where4NhVt = "And " + where4NhVt;

                p2Template = string.Format(p2Template, where3AD, where4NhVt);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, where2Dvcs, p2Template);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return tbl;
        }

        public DataTable LoadAd(string sttRec)
        {
            string sql = "SELECT d.Ten_tk AS Ten_tk_i,  c.* FROM [" + AD
                + "] c LEFT JOIN Altk d ON c.tk_i= d.tk "
                + " LEFT JOIN Alkh k ON c.ma_kh_i= k.ma_kh "
                + " Where c.stt_rec = @rec Order by c.stt_rec0";
            SqlParameter[] listParameters = { new SqlParameter("@rec", sttRec) };
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        public DataTable LoadAd2(string sttRec)
        {
            //c=AD, d=Alvt, e=ABVT13
            string sql = "SELECT * FROM [" + AD2 + "] Where stt_rec = @rec Order by stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }
        public DataTable LoadAd3(string sttRec)
        {
            string sql = "SELECT c.*,d.Ten_tk AS Ten_tk FROM " + AD3
                + " c LEFT JOIN Altk d ON c.Tk_i= d.Tk Where c.stt_rec = @rec Order by c.stt_rec0";
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
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_CA1_DELETE_MAIN", plist);
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
            var result = V6BusinessHelper.ExecuteProcedure("ACACTCA1_InitTt", plist).Tables[0];
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
