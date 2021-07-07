using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6AccountingBusiness.Invoices
{
    public class V6InvoiceBase
    {
        public static V6InvoiceBase GetInvoiceBase(string ma_ct)
        {
            ma_ct = ma_ct.ToUpper().Trim();
            switch (ma_ct)
            {
                #region ==== Phải thu ====
                case "SOA":// Hóa đơn bán hàng kiêm phiếu xuất
                    return new V6Invoice81();
                case "SOB":// Hóa đơn dịch vụ có số lượng
                    return new V6Invoice82();
                case "SOC":// Hóa đơn bán lẻ CAFE
                    return new V6Invoice83();
                case "SOH":// Đơn đặt hàng bán
                    return new V6Invoice91();
                case "SOR":// Báo giá.
                    return new V6Invoice93();
                case "AR1":// Hóa đơn dịch vụ
                    return new V6Invoice21();
                case "SOF":// Phiếu nhập hàng bán bị trả lại
                    return new V6Invoice76();
                #endregion phải thu
                #region ==== Tồn kho ====
                case "IXA":// Phiếu xuất kho
                    return new V6Invoice84();
                case "IXB":// Phiếu xuất điều chuyển
                    return new V6Invoice85();
                case "IND":// Phiếu NHẬP kho
                    return new V6Invoice74();
                case "INY":// Đề nghị nhập kho
                    return new V6Invoice94INY();
                case "IXY":// Đề nghị xuất kho
                    return new V6Invoice95IXY();
                case "IXP":// Phiếu duyệt xuất bán
                    return new V6Invoice96IXP();
                #endregion tồn kho
                #region ==== Tiền mặt ====
                case "BC1":// Bao co
                case "TA1":// Phieu thu
                    return new V6Invoice41(ma_ct);
                case "BN1":// Bao no
                case "CA1":// Phieu chi
                    return new V6Invoice51(ma_ct);
                #endregion tiền mặt
                #region ==== Phải trả ====
                case "POA":// PhieuNhapMua
                    return new V6Invoice71();
                case "POH":// DonDatHangMua
                    return new V6Invoice92();
                case "POB":// PhieuNhapKhau
                    return new V6Invoice72();
                case "POC":// PhieuNhapChiPhiMuaHang
                    return new V6Invoice73();
                case "AP1":// HoaDonMuaHangDichVu
                    return new V6Invoice31();
                case "AP2":// PhieuThanhToanTamUng
                    return new V6Invoice32();
                case "IXC":// PhieuXuatTraLaiNCC
                    return new V6Invoice86();
                #endregion phải trả
                #region ==== Tổng hợp ====
                // Phiếu kế toán dùng chung
                case "GL1":
                case "AP9":
                case "AR9":
                    return new V6Invoice11(ma_ct);
                #endregion tổng hợp
                default:
                    return new V6InvoiceBase(ma_ct, "00" + ma_ct);
            }
        }

        public V6InvoiceBase(string maCt)
        {
            Mact = maCt;
            CodeMact = "00" + maCt;
        }

        public V6InvoiceBase(string maCt, string codeMaCt)
        {
            Mact = maCt;
            CodeMact = codeMaCt;
        }
        public string Mact { get; set; }
        public string CodeMact { get; set; }
        public string PrintTitle { get { return V6Setting.IsVietnamese ? "In " + Alct["TEN_CT"].ToString().Trim() : "Print " + Alct["TEN_CT2"].ToString().Trim(); } }
        public virtual string PrintReportProcedure
        {
            get { throw new NotImplementedException(); }
        }
        public string V6Message = "";

        protected V6TableStruct _amStruct, _adStruct, _ad2Struct, _ad3Struct;
        protected DataTable _alnt, _alct1, _alct3, _alpost, _alimtype;
        protected DataRow _alct, _alctct;
        protected AlctConfig _alctConfig;
        
        /// <summary>
        /// Tên bảng dữ liệu AM
        /// </summary>
        public string AM_TableName
        {
            get
            {
                return Alct["m_phdbf"].ToString().Trim();
            }
        }
        /// <summary>
        /// Tên bảng dữ liệu AD m_ctdbf
        /// </summary>
        public string AD_TableName
        {
            get
            {
                return Alct["m_ctdbf"].ToString().Trim();
            }
        }

        public string AD2_TableName
        {
            get
            {
                return Alct["m_gtdbf"].ToString().Trim();
            }
        }

        public AldmConfig Config2
        {
            get
            {
                if (_config2 == null) GetConfig2();
                return _config2;
            }
        }

        private void GetConfig2()
        {
            _config2 = ConfigManager.GetAldmConfig(AD2_TableName);
        }

        private AldmConfig _config2;

        public AldmConfig Config3
        {
            get
            {
                if (_config3 == null) GetConfig3();
                return _config3;
            }
        }

        private void GetConfig3()
        {
            _config3 = ConfigManager.GetAldmConfig(AD3_TableName);
        }

        private AldmConfig _config3;

        public AldmConfig Config3ChiPhi
        {
            get
            {
                if (_config3ChiPhi == null) GetConfig3ChiPhi();
                return _config3ChiPhi;
            }
        }

        private void GetConfig3ChiPhi()
        {
            _config3ChiPhi = ConfigManager.GetAldmConfig(AD_TableName + "CP");
        }

        private AldmConfig _config3ChiPhi;

        /// <summary>
        /// m_ktdbf
        /// </summary>
        public string AD3_TableName
        {
            get
            {
                return Alct["m_ktdbf"].ToString().Trim();
            }
        }

        /// <summary>
        /// Cộng thêm vào phần search select trong hàm SearchAM
        /// </summary>
        public string AMSELECTMORE
        {
            get
            {
                string m_gc_td2 = AlctConfig.M_GC_TD2;
                string[] ss = m_gc_td2.Split(';');
                return ss[0];
            }
        }
        
        /// <summary>
        /// Cộng thêm vào phần search select JOIN trong hàm SearchAM
        /// </summary>
        public string AMJOINMORE
        {
            get
            {
                string m_gc_td2 = AlctConfig.M_GC_TD2;
                var ss = m_gc_td2.Split(';');
                return ss.Length>1 ? ss[1] : null;
            }
        }
        
        /// <summary>
        /// Cộng thêm vào phần select trong hàm LoadAD
        /// </summary>
        public string ADSELECTMORE
        {
            get
            {
                string m_gc_td1 = AlctConfig.M_GC_TD1;
                string[] ss = m_gc_td1.Split(';');
                return ss[0];
            }
        }

        public string ADJOINMORE
        {
            get
            {
                string m_gc_td1 = AlctConfig.M_GC_TD1;
                var ss = m_gc_td1.Split(';');
                return ss.Length > 1 ? ss[1] : null;
            }
        }

        public int SoLien
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Alct["so_lien"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        
        /// <summary>
        /// Lưu lại lịch sử sửa chứng từ.
        /// </summary>
        public bool WRITE_LOG
        {
            get { return ObjectAndString.ObjectToBool(AlctConfig.WRITE_LOG); }
        }

        public V6TableStruct AMStruct
        {
            get { return _amStruct ?? (_amStruct = V6BusinessHelper.GetTableStruct(AM_TableName)); }
        }

        public V6TableStruct ADStruct
        {
            get { return _adStruct ?? (_adStruct = V6BusinessHelper.GetTableStruct(AD_TableName)); }
        }

        public V6TableStruct AD2Struct
        {
            get { return _ad2Struct ?? (_ad2Struct = V6BusinessHelper.GetTableStruct(AD2_TableName)); }
        }

        public V6TableStruct AD3Struct
        {
            get { return _ad3Struct ?? (_ad3Struct = V6BusinessHelper.GetTableStruct(AD3_TableName)); }
        }

        public DataTable Alnt
        {
            get { return _alnt ?? (_alnt = GetAlnt()); }
        }

        private DataTable GetAlnt()
        {
            return SqlConnect.Select("Alnt", null, "", "", "ma_nt").Data;
        }

        public DataRow Alct
        {
            get
            {
                try
                {
                    _alct = _alct ?? (_alct = GetAlct());
                }
                catch
                {
                    // ignored
                }
                return _alct;
            }
        }

        public AlctConfig AlctConfig
        {
            get
            {
                try
                {
                    if (_alctConfig == null || _alctConfig.NoInfo)
                    {
                        _alctConfig = new AlctConfig(Alct.ToDataDictionary());
                    }
                }
                catch
                {
                    // ignored
                }
                return _alctConfig;
            }
        }
        
        public DataRow Alctct
        {
            get
            {
                try
                {
                    _alctct = _alctct ?? (_alctct = GetAlctct());
                }
                catch
                {
                    // ignored
                }
                return _alctct;
            }
        }

        private DataRow GetAlct()
        {
            var data = SqlConnect.Select("Alct", "*", "ma_ct=@mact", "", "", new SqlParameter("@mact", Mact)).Data;
            if (data != null && data.Rows.Count == 1) return data.Rows[0];
            return null;
        }

        private DataRow GetAlctct()
        {
            var data = V6BusinessHelper.GetAlctCt(Mact);
            if (data != null && data.Rows.Count == 1) return data.Rows[0];
            return null;
        }
        
        public bool M_LOC_NSD
        {
            get
            {
                if(Alct!=null)
                return Convert.ToInt32(Alct["m_loc_nsd"]) == 1;
                return false;
            }
        }

        /// <summary>
        /// Sử dụng ngày lập chứng từ.
        /// </summary>
        public bool M_NGAY_CT
        {
            get { return Convert.ToInt32(Alct["m_ngay_ct"])==1; }
        }

        public virtual DataTable LoadAD(string sttRec)
        {
            //c=AD81, d=Alvt, e=ABVT13
            string sql = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13, c.So_luong1*0 as Ton13Qd" + ADSELECTMORE + " FROM [" + AD_TableName
                + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt ";
            sql += ADJOINMORE;
            sql += string.IsNullOrEmpty(sttRec) ? " Where 1=0" : " Where c.stt_rec=@rec";
            sql += " Order by c.stt_rec0";
            var listParameters = new SqlParameter("@rec", sttRec);
            var tbl = SqlConnect.ExecuteDataset(CommandType.Text, sql, listParameters).Tables[0];
            return tbl;
        }

        public virtual DataTable Alct1
        {
            get
            {
                _alct1 = _alct1 ?? GetAlct1();
                return _alct1;
            }
        }

        private DataTable GetAlct1()
        {
            SqlParameter[] plist =
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
                    "VPA_GET_AUTO_COLULMN", plist).Tables[0];
        }

        public virtual DataTable Alct3
        {
            get
            {
                _alct3 = _alct3 ?? GetAlct3();
                return _alct3;
            }
        }

        private DataTable GetAlct3()
        {
            SqlParameter[] plist =
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
                    "VPA_GET_AUTO_COLUMN_KT", plist).Tables[0];
        }

        public DataTable AlImtype
        {
            get { return _alimtype ?? (_alimtype = GetAlImtype()); }
        }

        private DataTable GetAlImtype()
        {
            return SqlConnect.Select("AlImtype", "IMTYPE, [default], ten, ten2",
                "Ma_ct=@mact", "", "[Stt_in]", new SqlParameter("@mact", Mact)).Data;
        }
        
        public DataTable AlPost
        {
            get { return _alpost ?? (_alpost = GetAlPost()); }
        }

        private DataTable GetAlPost()
        {
            return SqlConnect.Select("AlPost", "Kieu_post,Ten_post,Ten_post2,ColorV",
                "Ma_ct=@mact and [Status]='1'", "", "Kieu_post", new SqlParameter("@mact", Mact)).Data;
        }

        public DataTable GetDinhMucVatTu(string sttRec, string makh, string madvcs, string loai_pb)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@stt_rec", sttRec), 
                new SqlParameter("@ma_kh", makh), 
                new SqlParameter("@ma_dvcs", madvcs),
                new SqlParameter("@loai_pb", loai_pb),
            };
            var result = V6BusinessHelper.ExecuteProcedure("AINCTIXA_INIT_ALDMVT", plist).Tables[0];
            return result;
        }

        /// <summary>
        /// Các trường động,, cho lọc AM
        /// </summary>
        public string ADV_AM
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["ADV_AM"].ToString().Trim();
                }
                return "";
            }
        }

        /// <summary>
        /// Các trường động,, cho lọc AD
        /// </summary>
        public string ADV_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["ADV_AD"].ToString().Trim();
                }
                return "";
            }
        }

        public string GRDS_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDS_AD"].ToString().Trim();
                }
                return "";
            }
        }
        
        public string GRDF_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDF_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_AM
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHV_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_AM
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHE_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHV_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHE_AD"].ToString().Trim();
                }
                return "";
            }
        }

        public string GRDS_Q1
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDS_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDF_Q1
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDF_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_Q1
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHV_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_Q1
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDHE_Q1"].ToString().Trim();
                }
                return "";
            }
        }


        public string GRDT_AM
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDT_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDT_AD
        {
            get
            {
                if (Alct != null)
                {
                    return _alct["GRDT_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string EXTRA_INFOR_CT_STRING
        {
            get
            {
                if (Alctct != null)
                {
                    return Alctct["EXTRA_INFOR"].ToString().Trim();
                }
                return "";
            }
        }
        public SortedDictionary<string, string> EXTRA_INFOR_CT
        {
            get
            {
                if (_extraInforCT == null || _extraInforCT.Count == 0)
                {
                    GetExtraInforCT();
                }
                return _extraInforCT;
            }
        }

        /// <summary>
        /// Các trường ẩn [Alctct]
        /// </summary>
        public string[] GRD_HIDE
        {
            get
            {
                string[] result = { };
                if (!V6Login.IsAdmin)
                {
                    if (Alctct != null)
                    {
                        var ss = Alctct["GRD_HIDE"].ToString().ToUpper();
                        result = ObjectAndString.SplitString(ss);
                    }
                }
                return result;
            }
        }
        
        /// <summary>
        /// Các trường chỉ đọc [Alctct] FIELD:MSI MSI=Mới Sửa In
        /// </summary>
        public string[] GRD_READONLY
        {
            get
            {
                string[] result = { };
                if (!V6Login.IsAdmin)
                {
                    if (Alctct != null)
                    {
                        var ss = Alctct["GRD_READONLY"].ToString().ToUpper();
                        result = ObjectAndString.SplitString(ss);
                    }
                }
                return result;
            }
        }

        #region ===== EXTRA_INFOR =====
        /// <summary>
        /// <para></para>
        /// <para>ADFIELDS FIELD:R/E:FORMAT</para>
        /// </summary>
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    GetExtraInfor();
                }
                return _extraInfor;
            }
        }

        /// <summary>
        /// Tự thay đổi mẫu ngoại tệ.
        /// </summary>
        public bool ExtraInfo_PrintNT
        {
            get
            {
                bool result = false;
                if (EXTRA_INFOR.ContainsKey("PRINTNT")) result = ObjectAndString.ObjectToBool(EXTRA_INFOR["PRINTNT"]);
                return result;
            }
        }

        /// <summary>
        /// Zoom mặc định cho crystalreport khi in chứng từ.
        /// <para>Crystal: 1 PageWidth, 2 AllPage, 50 is 50%</para>
        /// <para>Xtra: 1 = 100%, >50 is this %</para>
        /// </summary>
        public int ExtraInfo_PrintVCzoom
        {
            get
            {
                int result = 2;
                if (EXTRA_INFOR.ContainsKey("PRINTVCZOOM")) result = ObjectAndString.ObjectToInt(EXTRA_INFOR["PRINTVCZOOM"]);
                return result;
            }
        }

        public float ExtraInfo_PrintVCzoom_DX
        {
            get
            {
                int result = 100;
                if (EXTRA_INFOR.ContainsKey("PRINTVCZOOM")) result = ObjectAndString.ObjectToInt(EXTRA_INFOR["PRINTVCZOOM"]);
                if (result < 50) result = 100;
                return (float) result / 100;
            }
        }

        private SortedDictionary<string, string> _extraInfor = null;
        private SortedDictionary<string, string> _extraInforCT = null;

        private void GetExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            string s = Alct["EXTRA_INFOR"].ToString().Trim();
            if (s != "")
            {
                var sss = s.Split(';');
                foreach (string ss in sss)
                {
                    int indexOf = ss.IndexOf(":", StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        _extraInfor[ss.Substring(0, indexOf).ToUpper()] = ss.Substring(indexOf + 1);
                    }
                }
            }
        }
        private void GetExtraInforCT()
        {
            _extraInforCT = new SortedDictionary<string, string>();
            string s = EXTRA_INFOR_CT_STRING;
            if (s != "")
            {
                var sss = s.Split(';');
                foreach (string ss in sss)
                {
                    int indexOf = ss.IndexOf(":", StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        _extraInforCT[ss.Substring(0, indexOf).ToUpper()] = ss.Substring(indexOf + 1);
                    }
                }
            }
        }

        /// <summary>
        /// //Tuanmh 30/08/2018 // Chuyển xuống từ Invoice71 16/01/2020
        /// </summary>
        /// <param name="field"></param>
        /// <param name="mact"></param>
        /// <param name="ngayct"></param>
        /// <param name="mant"></param>
        /// <param name="mavt"></param>
        /// <param name="dvt1"></param>
        /// <param name="makh"></param>
        /// <param name="magia"></param>
        /// <returns></returns>
        public DataRow GetGiaMua(string field, string mact, DateTime ngayct,
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

                var resultData = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GetPOIDPrice", plist).Tables[0];
                if (resultData != null && resultData.Rows.Count >= 1)
                {
                    return resultData.Rows[0];
                }
                else
                {
                    throw new Exception("GetGiaMua return null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("V6Invoice71 GetGiaMua " + ex.Message);
            }
        }

        /// <summary>
        /// 0 DoNoThing 1 AutoPrint 2 AutoClick 3 AutoExportT
        /// </summary>
        public string PrintMode
        {
            get
            {
                if (EXTRA_INFOR.ContainsKey("PRINTMODE")) return EXTRA_INFOR["PRINTMODE"];
                return "0";
            }
        }
        public string PrintModeCT
        {
            get
            {
                if (EXTRA_INFOR_CT.ContainsKey("PRINTMODE")) return EXTRA_INFOR_CT["PRINTMODE"];
                return "";
            }
        }

        /// <summary>
        /// 0 DoNoThing 1 AutoClickNew
        /// </summary>
        public string SaveMode
        {
            get
            {
                if (EXTRA_INFOR.ContainsKey("SAVEMODE")) return EXTRA_INFOR["SAVEMODE"];
                return "0";
            }
        }
        #endregion EXTRA_INFOR

        /// <summary>
        /// Ghi log vào csdl.
        /// </summary>
        /// <param name="sttRec"></param>
        /// <param name="mode"></param>
        /// <param name="message">null sẽ lấy mặc định V6Message</param>
        public void PostErrorLog(string sttRec, string mode, string message = null)
        {
            message = message ?? V6Message;
            Logger.WriteToLog(string.Format("{0} PostErrorLog Mact: {1} Stt_rec: {2} Mode: {3} Message: {4}",
                V6Login.ClientName, Mact, sttRec, mode, message));
            PostErrorLog(Mact, sttRec, mode, message);
        }

        public void PostErrorLog(string sttRec, string mode, Exception ex)
        {
            Logger.WriteToLog(string.Format("{0} PostErrorLog Mact: {1} Stt_rec: {2} Mode: {3} Message: {4}\r\nStackTrace: {5}\r\nEx type: {6}",
                V6Login.ClientName, Mact, sttRec, mode, ex.Message, ex.StackTrace, ex.GetType()));
            PostErrorLog(Mact, sttRec, mode, ex.Message);
        }

        private void PostErrorLog(string mact, string sttRec, string mode, string message)
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
                return V6BusinessHelper.GetTyGia(mant, ngayct);
            }
            catch (Exception)
            {
                // ignored
            }
            return 0;
        }

        public DataSet GetCheck_VC(string status, string kieuPost, string sttRec)
        {
            try
            {
                SqlParameter[] pList =
                {
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@Stt_rec", sttRec)
                };
                V6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure,
                        "VPA_TA1_CHECK_VC", pList);
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }
        public DataTable GetCheck_VC_Save(string status, string kieuPost, string soct, string masonb, string sttrec)
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
                V6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_VC_SAVE", plist).Tables[0];
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }
        public DataTable GetCheck_Save_All(string status, string kieuPost, string soct, string masonb, string sttrec, string madvcs, string makh,
             string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id)
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
                V6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_SAVE_ALL", plist).Tables[0];
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }
        public DataTable GetCheck_Edit_All(string status, string kieuPost, string soct, string masonb, string sttrec, string madvcs, string makh,
             string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id)
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
                V6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_EDIT_ALL", plist).Tables[0];
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }

        public DataTable GetCheck_Print_All(string status, string kieuPost, string soct, string sttrec,
           DateTime ngayct, string mact, int user_id)//, out string v6Message)
        {
            try
            {
                SqlParameter[] plist =
                {     
                    new SqlParameter("@status", status),
                    new SqlParameter("@kieu_post", kieuPost),
                    new SqlParameter("@So_ct", soct),
                    new SqlParameter("@Ngay_ct",  ngayct.ToString("yyyyMMdd")),
                    new SqlParameter("@Ma_ct", mact),
                    new SqlParameter("@Stt_rec",sttrec),
                    new SqlParameter("@User_id",user_id)
                };
                V6Message = "Success";
                return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CHECK_PRINT_ALL", plist).Tables[0];
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin Lo Date bằng procedure [VPA_EdItems_DATE_STT_REC]
        /// </summary>
        /// <param name="mavt"></param>
        /// <param name="makho"></param>
        /// <param name="sttRec"></param>
        /// <param name="ngayct"></param>
        /// <returns></returns>
        public DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetLoDate(mavt, makho, sttRec, ngayct);
        }
        public DataTable GetLoDate_IXY(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetLoDate_IXY(mavt, makho, sttRec, ngayct);
        }
        public DataTable GetLoDate_IXP(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetLoDate_IXP(mavt, makho, sttRec, ngayct);
        }
        
        public DataTable GetViTri(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetViTri(mavt, makho, sttRec, ngayct);
        }
        
        public DataTable GetViTriLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetViTriLoDate(mavt, makho, sttRec, ngayct);
        }

        /// <summary>
        /// VPA_EdItems_DATE_STT_REC
        /// </summary>
        /// <param name="mavt"></param>
        /// <param name="makho"></param>
        /// <param name="malo"></param>
        /// <param name="sttRec"></param>
        /// <param name="ngayct"></param>
        /// <returns></returns>
        public DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetLoDate13(mavt, makho, malo, sttRec, ngayct);
        }

        public DataTable GetLoDateAll(string mavt_in, string makho_in, string malo_in, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetLoDateAll(mavt_in, makho_in, malo_in, sttRec, ngayct);
        }
        
        public DataTable GetViTri13(string mavt, string makho, string mavitri, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetViTri13(mavt, makho, mavitri, sttRec, ngayct);
        }
        
        public DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetViTriLoDate13(mavt, makho, malo, mavitri, sttRec, ngayct);
        }
        public DataTable GetViTriLoDateAll(string mavt_in, string makho_in, string malo_in, string mavitri_in, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetViTriLoDateAll(mavt_in, makho_in, malo_in, mavitri_in, sttRec, ngayct);
        }

        /// <summary>
        /// [VPA_CheckTonXuatAm] 1,'20160216','SOA','STTREC','a.MA_VT=''VTLO1'' AND a.MA_KHO=',''
        /// </summary>
        /// <returns></returns>
        public DataTable GetStock(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetStock(Mact, mavt, makho, sttRec, ngayct);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mavt_in">'a','b'</param>
        /// <param name="makho_in"></param>
        /// <param name="sttRec"></param>
        /// <param name="ngayct"></param>
        /// <returns></returns>
        public DataTable GetStockAll(string mavt_in, string makho_in, string sttRec, DateTime ngayct)
        {
            return V6BusinessHelper.GetStockAll(Mact, mavt_in, makho_in, sttRec, ngayct);
        }

        public virtual DataTable GetSoct0_All_Cust(string sttRec, string madvcs, string filterString)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetSodu0_All_Cust(string sttRec, string madvcs, string filterString)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetCuocCont_All_Cust(string sttRec, string madvcs, DateTime ngay_ct1, DateTime ngay_ct2, string filterString)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetSoDu0TK_All_Cust(string sttRec, string madvcs, DateTime ngay_ct, string filterString)
        {
            throw new NotImplementedException();
        }
        public virtual DataTable GetSoDu0PT_All_Cust(string sttRec, string madvcs, DateTime ngay_ct, string filterString)
        {
            throw new NotImplementedException();
        }
        public virtual DataTable GetSoDu0TK_TienLe(string sttRec, string madvcs, DateTime ngay_ct, decimal soTienLe, string type, string tk_du, string filterString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhập số lượng in.
        /// </summary>
        /// <param name="sttRec"></param>
        /// <param name="am"></param>
        public void IncreaseSl_inAM(string sttRec, DataRow am)// = null)
        {
            var sql = string.Format("Update {0} Set Sl_in = Sl_in+1 Where Stt_rec=@p", AM_TableName);
            var result = SqlConnect.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@p", sttRec));
            if (am != null && am.Table.Columns.Contains("SL_IN") && result > 0)
            {
                am["Sl_in"] = ObjectAndString.ObjectToInt(am["Sl_in"]) + 1;
            }
        }

        public virtual SortedDictionary<string, string> LoadDefaultData(string lang, string itemId)
        {
            if (defaultData == null || defaultData.Rows.Count == 0)
                defaultData = V6BusinessHelper.GetDefaultValueData(1, Mact, "", itemId, "nhom='00'");
            var result = new SortedDictionary<string, string>();
            var dataField = "Default" + lang;
            if (!defaultData.Columns.Contains(dataField))
            {
                Logger.WriteToLog(V6Login.ClientName + " V6InvoiceBase.LoadDefaultData VPA_GetDefaultvalue return no field{"+dataField+"}");
                //return result; // exception: var cell = row[dataField];
            }
            foreach (DataRow row in defaultData.Rows)
            {
                var cell = row[dataField];
                if (cell == null) continue;
                var value = cell.ToString().Trim();
                if (value == "") continue;

                var name = row["NameVal"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            return result;
        }

        public virtual List<DefaultValueInfo> LoadDataReferenceInfo(string lang, string itemId)
        {
            if (defaultInfo == null || defaultInfo.Rows.Count == 0)
                defaultInfo = V6BusinessHelper.GetDefaultValueData(1, Mact, "", itemId, "nhom='02'");
            var result = new List<DefaultValueInfo>();
            foreach (DataRow row in defaultInfo.Rows)
            {
                var dValue = new DefaultValueInfo();
                var cell = row["Default" + lang];
                if (cell == null) continue;

                dValue.AName = row["NameVal"].ToString().Trim().ToUpper();
                dValue.CName = row["NameTag"].ToString().Trim().ToUpper();
                dValue.Value = ("" + cell).Trim();
                dValue.Type1 = row["Kieu"].ToString().Trim();
                
                result.Add(dValue);
            }
            return result;
        }

        public DataTable AlVitriTon;
        public DataTable GetAlVitriTon(DateTime ngay_ct, string sttRec, string mavt, string makho)// string makh, string madvcs)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt='{0}' and Ma_kho='{1}'", mavt, makho)), 
                new SqlParameter("@cKey2", ""), 
                new SqlParameter("@cKey3", ""), 
                new SqlParameter("@cStt_rec", sttRec), 
                new SqlParameter("@dBg", ngay_ct.Date)
            };
            var result = V6BusinessHelper.ExecuteProcedure("VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
            AlVitriTon = result;
            return AlVitriTon;
        }

        public DataTable AlLoTon { get; set; }
        public DataTable GetAlLoTon(DateTime ngay_ct, string sttRec, string mavt, string makho)// string makh, string madvcs)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt='{0}' and Ma_kho='{1}'", mavt, makho)), 
                new SqlParameter("@cKey2", ""), 
                new SqlParameter("@cKey3", ""), 
                new SqlParameter("@cStt_rec", sttRec), 
                new SqlParameter("@dBg", ngay_ct.Date)
            };
            var result = V6BusinessHelper.ExecuteProcedure("VPA_EdItems_LOT_DATE_STT_REC", plist).Tables[0];
            AlLoTon = result;
            return AlLoTon;
        }
        
        public DataTable GetAlLoTon_IXY(DateTime ngay_ct, string sttRec, string mavt, string makho)// string makh, string madvcs)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt='{0}' and Ma_kho='{1}'", mavt, makho)), 
                new SqlParameter("@cKey2", ""), 
                new SqlParameter("@cKey3", ""), 
                new SqlParameter("@cStt_rec", sttRec), 
                new SqlParameter("@dBg", ngay_ct.Date)
            };
            var result = V6BusinessHelper.ExecuteProcedure("VPA_EdItems_LOT_DATE_STT_REC_IXY", plist).Tables[0];
            AlLoTon = result;
            return AlLoTon;
        }
        
        public DataTable GetAlLoTon_IXP(DateTime ngay_ct, string sttRec, string mavt, string makho)// string makh, string madvcs)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt='{0}' and Ma_kho='{1}'", mavt, makho)), 
                new SqlParameter("@cKey2", ""), 
                new SqlParameter("@cKey3", ""), 
                new SqlParameter("@cStt_rec", sttRec), 
                new SqlParameter("@dBg", ngay_ct.Date)
            };
            var result = V6BusinessHelper.ExecuteProcedure("VPA_EdItems_LOT_DATE_STT_REC_IXP", plist).Tables[0];
            AlLoTon = result;
            return AlLoTon;
        }


        protected DataTable defaultData;
        protected DataTable defaultInfo;
        private SortedDictionary<string, string> tagData;
        public SortedDictionary<string, string> textData;

        public virtual SortedDictionary<string, string> LoadTag(string itemId)
        {
            if (tagData != null && tagData.Count > 0) return tagData;
            if (defaultData == null || defaultData.Rows.Count == 0)
                defaultData = V6BusinessHelper.GetDefaultValueData(1, Mact, "", itemId, "nhom='00'");
            var result = new SortedDictionary<string,string>();
            textData = new SortedDictionary<string, string>();
            foreach (DataRow row in defaultData.Rows)
            {
                var nameTag = row["NameTag"].ToString().Trim().ToUpper();
                var text = V6Setting.IsVietnamese ? row["defaultV"].ToString().Trim() : row["defaultE"].ToString().Trim();
                if (nameTag != "" && text != "") textData[nameTag] = text;
                var cellTag = row["Tag"]; if (cellTag == null) continue;
                var tagValue = cellTag.ToString().Trim(); if (tagValue == "") continue;

                
                result[nameTag] = tagValue;
            }
            tagData = result;
            return result;
        }

        public override string ToString()
        {
            return Mact + ": " + Name;
        }

        public virtual string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_base_name))
                {
                    try
                    {
                        _base_name = Alct[V6Setting.IsVietnamese ? "Ten_ct" : "Ten_ct2"].ToString().Trim();
                    }
                    catch
                    {
                        //
                    }
                }
                return _base_name;
            }
            set { _base_name = value; }
        }
        protected string _base_name = null;

        private SortedDictionary<string, DefineInfo> _templateSettingAM = null;
        /// <summary>
        /// Lấy định nghĩa, nếu không có là mặc định.
        /// </summary>
        /// <param name="KEY"></param>
        /// <returns></returns>
        public DefineInfo GetTemplateSettingAM(string KEY)
        {
            KEY = KEY.ToUpper();
            if (TemplateSettingAM.ContainsKey(KEY))
                return _templateSettingAM[KEY];
            return new DefineInfo("");
        }
        private SortedDictionary<string, DefineInfo> TemplateSettingAM
        {
            get
            {
                if (_templateSettingAM == null || _templateSettingAM.Count == 0)
                {
                    GetAllTemplateSettingAM();
                }
                return _templateSettingAM;
            }
        }

        private void GetAllTemplateSettingAM()
        {
            _templateSettingAM = new SortedDictionary<string, DefineInfo>();
            var define = Alct["AM_TEMPLATE"].ToString().Trim();
            if (string.IsNullOrEmpty(define)) return;

            string[] sss = define.Split('~');
            foreach (string s in sss)
            {
                var defineInfo = new DefineInfo(s);
                _templateSettingAM[defineInfo.Name.ToUpper()] = defineInfo;
            }
        }

        private SortedDictionary<string, DefineInfo> _templateSettingAD = null;
        /// <summary>
        /// Lấy định nghĩa, nếu không có là mặc định.
        /// </summary>
        /// <param name="KEY"></param>
        /// <returns></returns>
        public DefineInfo GetTemplateSettingAD(string KEY)
        {
            KEY = KEY.ToUpper();
            if (TemplateSettingAD.ContainsKey(KEY))
                return _templateSettingAD[KEY];
            return new DefineInfo("");
        }
        private SortedDictionary<string, DefineInfo> TemplateSettingAD
        {
            get
            {
                if (_templateSettingAD == null || _templateSettingAD.Count == 0)
                {
                    GetAllTemplateSettingAD();
                }
                return _templateSettingAD;
            }
        }

        

        private void GetAllTemplateSettingAD()
        {
            _templateSettingAD = new SortedDictionary<string, DefineInfo>();
            var define = Alct["AD_TEMPLATE"].ToString().Trim();
            string[] sss = define.Split(new []{'~'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in sss)
            {
                var defineInfo = new DefineInfo(s);
                _templateSettingAD[defineInfo.Name.ToUpper()] = defineInfo;
            }
        }

        /// <summary>
        /// Thêm dữ liệu vào bảng AD
        /// </summary>
        /// <param name="name">Tên hàm đang chạy. MethodBase.GetCurrentMethod().Name</param>
        /// <param name="transaction"></param>
        /// <param name="dataList">Dữ liệu sẽ thêm.</param>
        /// <param name="isnew">Tạo phiếu mới.</param>
        /// <returns>Số dòng thêm được.</returns>
        protected int InsertADlist(string name, SqlTransaction transaction, List<IDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (IDictionary<string, object> adRow in dataList)
            {
                if (InsertIntoTransaction(name, transaction, ADStruct, adRow, false))
                {
                    j++;
                }
            }
            return j;
        }
        /// <summary>
        /// Thêm dữ liệu vào bảng AD2_TableName
        /// </summary>
        /// <param name="name">Tên hàm đang chạy.</param>
        /// <param name="transaction"></param>
        /// <param name="dataList">Dữ liệu sẽ thêm.</param>
        /// <param name="isnew">Tạo phiếu mới.</param>
        /// <returns>Số dòng thêm được.</returns>
        protected int InsertAD2list(string name, SqlTransaction transaction, List<IDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (IDictionary<string, object> adRow in dataList)
            {
                if (InsertIntoTransaction(name, transaction, AD2Struct, adRow, false))
                {
                    j++;
                }
            }
            return j;
        }
        /// <summary>
        /// Thêm dữ liệu vào bảng AD3
        /// </summary>
        /// <param name="name">Tên hàm đang chạy.</param>
        /// <param name="transaction"></param>
        /// <param name="dataList">Dữ liệu sẽ thêm.</param>
        /// <param name="isnew">Tạo phiếu mới.</param>
        /// <returns>Số dòng thêm được.</returns>
        protected int InsertAD3list(string name, SqlTransaction transaction, List<IDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (IDictionary<string, object> adRow in dataList)
            {
                if (InsertIntoTransaction(name, transaction, AD3Struct, adRow, false))
                {
                    j++;
                }
            }
            return j;
        }

        protected bool InsertIntoTransaction(string info, SqlTransaction TRANSACTION, V6TableStruct tableStruct, IDictionary<string, object> data, bool isnew)
        {
            var insertSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, tableStruct, data, isnew);
            int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insertSql);
            if (false)
            {
                object stt_rec = data["STT_REC"];
                object stt_rec0 = data["STT_REC0"];
                Logger.WriteToLog(
                    string.Format(info + " Insert " + tableStruct.TableName + " Ma_ct {0} stt_rec {1} row {2} result {3}.\n{4}",
                    Mact, stt_rec, stt_rec0, execute, insertSql));
            }
            return execute > 0;
        }

        /// <summary>
        /// Thêm chứng từ.
        /// </summary>
        /// <param name="amData">Dữ liệu thông tin chính.</param>
        /// <param name="ad1List">Dữ liệu thông tin chi tiết<para>Đối với invoie chưa có hàm nhiều bảng chi tiết thì sẽ gọi lại hàm có nhiều chi tiết nhưng với dữ liệu rỗng.</para></param>
        /// <returns></returns>
        public virtual bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> ad1List)
        {
            throw new NotImplementedException("InsertInvoice No override method.");
        }
        public virtual bool InsertInvoice2_TH(IDictionary<string, object> amData, List<IDictionary<string, object>> ad1List)
        {
            throw new NotImplementedException("InsertInvoice2_TH No override method.");
        }
        /// <summary>
        /// Thêm chứng từ.
        /// </summary>
        /// <param name="amData">Dữ liệu thông tin chính.</param>
        /// <param name="ad1List">Dữ liệu thông tin chi tiết</param>
        /// <param name="ad2List">Tùy vào thể hiện mà đây có thể là dữ liệu của AD2_TableName (gt) hoặc AD3(kt)</param>
        /// <returns></returns>
        public virtual bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> ad1List, List<IDictionary<string, object>> ad2List)
        {
            throw new NotImplementedException("InsertInvoice No override method.");
        }

        /// <summary>
        /// Thêm chứng từ vào kết nối 2_TH
        /// </summary>
        /// <param name="amData"></param>
        /// <param name="ad1List"></param>
        /// <param name="ad2List"></param>
        /// <returns></returns>
        public virtual bool InsertInvoice2_TH(IDictionary<string, object> amData, List<IDictionary<string, object>> ad1List, List<IDictionary<string, object>> ad2List)
        {
            throw new NotImplementedException("InsertInvoice2_TH No override method.");
        }

        public virtual bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3, bool post)
        {
            throw new NotImplementedException("InsertInvoice No override method.");
        }
        public virtual bool InsertInvoice2_TH(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3, bool post)
        {
            throw new NotImplementedException("InsertInvoice2_TH No override method.");
        }

        public virtual bool InsertInvoice(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList2,
            List<IDictionary<string, object>> adList3)
        {
            throw new NotImplementedException("InsertInvoice No override method.");
        }
        public virtual bool InsertInvoice2_TH(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList2,
            List<IDictionary<string, object>> adList3)
        {
            throw new NotImplementedException("InsertInvoice2_TH No override method.");
        }

        public virtual bool UpdateInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice1detail No override method.");
        }
        public virtual bool UpdateInvoice2_TH(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice1detail No override method.");
        }
        public virtual bool UpdateInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3, IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice13details No override method.");
        }
        
        public virtual bool UpdateInvoice2_TH(IDictionary<string, object> amData, List<IDictionary<string, object>> adList, List<IDictionary<string, object>> adList3, IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice13details2_TH No override method.");
        }

        public virtual bool UpdateInvoice(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList2,
            List<IDictionary<string, object>> adList3,
            IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice123details No override method.");
        }
        public virtual bool UpdateInvoice2_TH(IDictionary<string, object> amData,
            List<IDictionary<string, object>> adList,
            List<IDictionary<string, object>> adList2,
            List<IDictionary<string, object>> adList3,
            IDictionary<string, object> keys)
        {
            throw new NotImplementedException("UpdateInvoice123details2_TH No override method.");
        }

        public virtual bool DeleteInvoice(string sttrec)
        {
            throw new NotImplementedException("DeleteInvoice No override method.");
        }
        public virtual bool DeleteInvoice2_TH(string sttrec)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Stt_rec", sttrec), 
                    new SqlParameter("@Ma_ct", Mact),
                    new SqlParameter("@UserID", V6Login.UserId)
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_" + Mact + "_DELETE_MAIN", plist);
                return true;
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
            throw new NotImplementedException("DeleteInvoice2TH No override method.");
        }

        protected void WriteLogTransactionComplete(object stt_rec)
        {
            if (false)
            {
                Logger.WriteToLog(string.Format("Mact {0} stt_rec {1} TRANSACTION COMMITTED.", Mact, stt_rec));
            }
        }

        public bool CheckRightKey(string key)
        {
            return V6BusinessHelper.CheckRightKey(Mact, key, "");
        }

        public string GetMaVtFilterByMaKH(string ma_kh, string ma_dvcs)
        {
            SqlParameter[] plist=
            {
                new SqlParameter("@Ma_ct", Mact),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Ma_kh", ma_kh),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@Lan", V6Login.SelectedLanguage),
            };
            object where = V6BusinessHelper.ExecuteProcedureScalar("VPA_GET_WHERE_INVOICE_MAKH2MAVT", plist);

            return "" + where;
        }
        
        public string GetMaDvcsFilterByMaKho(string ma_kh, string ma_dvcs)
        {
            SqlParameter[] plist=
            {
                new SqlParameter("@Ma_ct", Mact),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Ma_kh", ma_kh),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@Lan", V6Login.SelectedLanguage),
            };
            object where = V6BusinessHelper.ExecuteProcedureScalar("VPA_GET_WHERE_INVOICE_MADVCS2MAKHO", plist);

            return "" + where;
        }

        public string GetMaKhoFilterByMaVt(string mact1, string ma_vt, string ma_dvcs)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Ma_ct", Mact),
                new SqlParameter("@Ma_ct1", mact1),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Ma_vt", ma_vt),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@Lan", V6Login.SelectedLanguage),
            };
            object where = V6BusinessHelper.ExecuteProcedureScalar("VPA_GET_WHERE_INVOICE_MAVT2MAKHO", plist);

            return "" + where;
        }

        public string GetFilterMaViTriTon(string sttRec, DateTime ngayct, string makho_x, string makho_n, string maloai_x, string maloai_n)
        {
            try
            {
                //DateTime d = DateTime.ParseExact(ngayct, "d/M/yyyy", null);
                SqlParameter[] pList =
                {
                    new SqlParameter("@cMa_ct", Mact),
                    new SqlParameter("@cStt_rec", sttRec),
                    new SqlParameter("@dBg", ngayct),
                    new SqlParameter("@cMa_khox", makho_x),
                    new SqlParameter("@cMa_khon", makho_n),
                    new SqlParameter("@cMa_lnxphX", maloai_x),
                    new SqlParameter("@cMa_lnxphN", maloai_n),
                    new SqlParameter("@User_id", V6Login.UserId)
                };

                var resultData = V6BusinessHelper.ExecuteProcedure("VPA_GET_VITRITON_FILTER", pList).Tables[0];
                if (resultData.Rows.Count == 1)
                {
                    return resultData.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InvoiceBase GetFilterMaViTriTon " + ex.Message);
            }
            return null;
        }

        public bool Have_KEY_AM2TH
        {
            get { return !string.IsNullOrEmpty(_alctConfig.KEY_AM2TH); }
        }

        /// <summary>
        /// Xét đúng kiều kiện hay không. Không có đk tính là sai.
        /// </summary>
        /// <param name="readyDataAm"></param>
        /// <returns></returns>
        public bool IS_AM2TH(IDictionary<string, object> readyDataAm)
        {
            try
            {
                if (string.IsNullOrEmpty(_alctConfig.KEY_AM2TH)) return false;
                if (DatabaseConfig.ConnectionString2_TH == "") return false;
                var dic = ObjectAndString.StringToDictionary(_alctConfig.KEY_AM2TH, ';', '=');
                foreach (KeyValuePair<string, object> item in dic)
                {
                    if (!readyDataAm.ContainsKey(item.Key)) return false;
                    string a = ("" + readyDataAm[item.Key]).Trim();
                    string b = ObjectAndString
                        .ObjectTo(readyDataAm[item.Key] == null ? typeof(string) : readyDataAm[item.Key].GetType(),
                            item.Value).ToString().Trim();
                    if (a != b) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(string.Format("{0} {1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _alctConfig.KEY_AM2TH), ex);
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra đã có dữ liệu dở Database2_TH hay chưa?
        /// </summary>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        public bool Exist2_TH(string sttRec)
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data["STT_REC"] = sttRec;
            return V6BusinessHelper.CheckDataExist2_TH(AM_TableName, data, null);
        }

    }
}
