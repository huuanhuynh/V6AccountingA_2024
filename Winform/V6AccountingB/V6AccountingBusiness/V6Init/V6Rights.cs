using System;
using System.Data;
using System.Data.SqlClient;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Init
{
    public class V6Rights
    {
        private readonly DataRow _dataUserInfo;

        public static bool IsAdmin
        {
            get { return V6Login.IsAdmin; }
        }

        public V6Rights(DataRow dataUserInfo)
        {
            _dataUserInfo = dataUserInfo;
        }
        public bool AllowSelect(V6TableName name)
        {
            try
            {
               // if(_dataUserInfo[""])
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowSelect: " + ex.Message, "V6Init");
                return false;
            }
        }

        public bool AllowSelect(string name)
        {
            try
            {
                // if(_dataUserInfo[""])
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowSelect: " + ex.Message, "V6Init");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="code">1SOA</param>
        /// <returns></returns>
        public bool AllowRun(string itemid, string code)
        {
            return true;
        }

        public static bool CheckLevel(string Level1, int User_id2, string Xtag)
        {
            // - Tuanmh 16/02/2016 Check level Level1 < Level2 
            if (IsAdmin) return true;

            string Level2 =
                (string)
                    (SqlConnect.ExecuteScalar(CommandType.Text,
                        "Select Level from V6user  where user_id=" + Convert.ToString(User_id2)));
            if (Level2 == null) return true;
            if (Level2.Trim() == "") Level2 = "0";
            if (Xtag == null) Xtag = "5";
            // - Tuanmh 25/09/2017 Check level Level1 < Xtag (Khoa chunng tu)
            bool level_return = true;
            level_return = Convert.ToInt32(Level1.Trim()) <= Convert.ToInt32(Level2.Trim());
            if (level_return)
            {
                level_return = Convert.ToInt32(Level1.Trim()) <= Convert.ToInt32(Xtag.Trim());
            }
            return level_return;
        }

        public bool AllowDelete(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightDelete.Contains(code);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowDelete: " + ex.Message, "V6Init");
                return false;
            }
        }


        public bool AllowAdd(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightAdd.Contains(code);
            }
            catch(Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowAdd: " + ex.Message, "V6Init");
                return false;
            }
        }

        public bool AllowEdit(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightEdit.Contains(code);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowEdit: " + ex.Message, "V6Init");
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra quyền xóa hoặc sửa một chứng từ.
        /// </summary>
        /// <param name="mact"></param>
        /// <param name="stt_rec"></param>
        /// <param name="mode">Edit=S, Delete=X</param>
        /// <returns></returns>
        public bool AllowEditDeleteMact(string mact, string stt_rec, string mode)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@ma_ct", mact), 
                    new SqlParameter("@stt_rec", stt_rec), 
                    new SqlParameter("@mode", mode), 
                    new SqlParameter("@user_id", V6Login.UserId), 
                };
                var a = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_Check_RightEditDele_Voucher", plist);

                return ObjectAndString.ObjectToInt(a) == 1 ;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowEditMact: " + ex.Message, "V6Init");
            }
            return false;
        }

        public bool AllowView(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightView.Contains(code);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowView: " + ex.Message, "V6Init");
                return false;
            }
        }

        public bool AllowPrint(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightPrint.Contains(code);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowPrint: " + ex.Message, "V6Init");
                return false;
            }
        }

        public bool AllowDvcs(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightDvcs.Contains(itemid);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowDvcs: " + ex.Message, "V6Init");
                return false;
            }
        }

        public bool AllowKho(string itemid, string code)
        {
            try
            {
                return IsAdmin || RightKho.Contains(itemid);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Rights AllowKho: " + ex.Message, "V6Init");
                return false;
            }
        }


        public string Mrights
        {
            get
            {
                return _dataUserInfo["rights"].ToString().Trim();
            }
        }
        private string RightAdd
        {
            get
            {
                return _dataUserInfo["r_add"].ToString().Trim();
            }
        }
        private string RightEdit
        {
            get
            {
                return _dataUserInfo["r_edit"].ToString().Trim();
            }
        }
        private string RightDelete
        {
            get
            {
                return _dataUserInfo["r_del"].ToString().Trim();
            }
        }
        private string RightView
        {
            get
            {
                return _dataUserInfo["r_view"].ToString().Trim();
            }
        }
        private string RightPrint
        {
            get
            {
                return _dataUserInfo["r_print"].ToString().Trim();
            }
        }
        private string RightDvcs
        {
            get
            {
                return _dataUserInfo["r_dvcs"].ToString().Trim();
            }
        }
        public string RightKho
        {
            get
            {
                return _dataUserInfo["r_kho"].ToString().Trim();
            }
        }
        public string RightSonb
        {
            get
            {
                return _dataUserInfo["r_sonb"].ToString().Trim();
            }
        }
        public byte User_acc
        {
            get
            {
                var a = _dataUserInfo["User_acc"];
                return (byte) ObjectAndString.ObjectToInt(a);
            }
        }
        public byte User_inv
        {
            get
            {
                var a = _dataUserInfo["User_inv"];
                return (byte)ObjectAndString.ObjectToInt(a);
            }
        }
        public byte User_sale
        {
            get
            {
                var a = _dataUserInfo["User_sale"];
                return (byte)ObjectAndString.ObjectToInt(a);
            }
        }
    }
}
