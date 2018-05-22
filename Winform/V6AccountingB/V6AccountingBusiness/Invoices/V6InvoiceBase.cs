using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Implementations;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;

namespace V6AccountingBusiness.Invoices
{
    public class V6InvoiceBase
    {
        protected readonly InvoiceServices Service = new InvoiceServices();

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
        public virtual string PrintReportProcedure
        {
            get { throw new NotImplementedException(); }
        }
        public string V6Message = "";

        protected V6TableStruct _amStruct, _adStruct, _ad2Struct, _ad3Struct;
        protected DataTable _alnt, _alct, _alct1, _alct3, _alpost;
        
        /// <summary>
        /// Tên bảng dữ liệu AM
        /// </summary>
        public string AM
        {
            get
            {
                return Alct.Rows[0]["m_phdbf"].ToString().Trim();
            }
        }
        /// <summary>
        /// Tên bảng dữ liệu AD m_ctdbf
        /// </summary>
        public string AD
        {
            get
            {
                return Alct.Rows[0]["m_ctdbf"].ToString().Trim();
            }
        }

        public string AD2
        {
            get
            {
                return Alct.Rows[0]["m_gtdbf"].ToString().Trim();
            }
        }
        /// <summary>
        /// m_ktdbf
        /// </summary>
        public string AD3
        {
            get
            {
                return Alct.Rows[0]["m_ktdbf"].ToString().Trim();
            }
        }

        public int SoLien
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Alct.Rows[0]["so_lien"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        
        /// <summary>
        /// Cờ đánh dấu có ghi log chi tiết khi thực hiện insert/update hay không?
        /// </summary>
        public bool WRITE_LOG
        {
            get
            {
                try
                {
                    if (Alct.Columns.Contains("WRITE_LOG"))
                    {
                        return Alct.Rows[0]["WRITE_LOG"].ToString() == "1";
                    }
                }
                catch (Exception)
                {
                    //
                }
                return false;
            }
        }

        public V6TableStruct AMStruct
        {
            get { return _amStruct ?? (_amStruct = V6BusinessHelper.GetTableStruct(AM)); }
        }

        public V6TableStruct ADStruct
        {
            get { return _adStruct ?? (_adStruct = V6BusinessHelper.GetTableStruct(AD)); }
        }

        public V6TableStruct AD2Struct
        {
            get { return _ad2Struct ?? (_ad2Struct = V6BusinessHelper.GetTableStruct(AD2)); }
        }

        public V6TableStruct AD3Struct
        {
            get { return _ad3Struct ?? (_ad3Struct = V6BusinessHelper.GetTableStruct(AD3)); }
        }

        public DataTable Alnt
        {
            get { return _alnt ?? (_alnt = GetAlnt()); }
        }

        private DataTable GetAlnt()
        {
            return Service.GetAlnt();
        }

        public DataTable Alct
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

        private DataTable GetAlct()
        {
            var data = Service.GetAlct(Mact);
            if (data != null && data.Rows.Count == 1) return data;
            return null;
        }
        
        public bool M_LOC_NSD
        {
            get
            {
                if(Alct!=null)
                return Convert.ToInt32(Alct.Rows[0]["m_loc_nsd"]) == 1;
                return false;
            }
        }

        /// <summary>
        /// Sử dụng ngày lập chứng từ.
        /// </summary>
        public bool M_NGAY_CT
        {
            get { return Convert.ToInt32(Alct.Rows[0]["m_ngay_ct"])==1; }
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
            return Service.GetAlct1(Mact);
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
            return Service.GetAlct3(Mact);
        }

        public DataTable AlPost
        {
            get { return _alpost ?? (_alpost = GetAlPost()); }
        }

        private DataTable GetAlPost()
        {
            return Service.GetAlPost(Mact);
        }

        /// <summary>
        /// Các trường động,, cho lọc AM
        /// </summary>
        public string ADV_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["ADV_AM"].ToString().Trim();
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
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["ADV_AD"].ToString().Trim();
                }
                return "";
            }
        }

        public string GRDS_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDS_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDS_AD
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDS_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDF_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDF_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDF_AD
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDF_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHV_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHE_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_AD
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHV_AD"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_AD
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHE_AD"].ToString().Trim();
                }
                return "";
            }
        }

        public string GRDS_Q1
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDS_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDF_Q1
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDF_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHV_Q1
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHV_Q1"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDHE_Q1
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDHE_Q1"].ToString().Trim();
                }
                return "";
            }
        }


        public string GRDT_AM
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDT_AM"].ToString().Trim();
                }
                return "";
            }
        }
        public string GRDT_AD
        {
            get
            {
                if (Alct != null && Alct.Rows.Count > 0)
                {
                    return _alct.Rows[0]["GRDT_AD"].ToString().Trim();
                }
                return "";
            }
        }

        #region ===== EXTRA_INFOR =====
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

        private SortedDictionary<string, string> _extraInfor = null;

        private void GetExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            string s = _alct.Rows[0]["EXTRA_INFOR"].ToString().Trim();
            if (s != "")
            {
                var sss = s.Split(';');
                foreach (string ss in sss)
                {
                    //var ss1 = ss.Split(':');
                    //if (ss1.Length > 1)
                    //{
                    //    _extraInfor[ss1[0].ToUpper()] = ss1[1];
                    //}
                    int indexOf = ss.IndexOf(":", StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        _extraInfor[ss.Substring(0, indexOf).ToUpper()] = ss.Substring(indexOf + 1);
                    }
                }
            }
        }

        public string PrintMode
        {
            get
            {
                if (EXTRA_INFOR.ContainsKey("PRINTMODE")) return EXTRA_INFOR["PRINTMODE"];
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
            Service.PostErrorLog(Mact, sttRec, mode, message);
        }

        public void PostErrorLog(string sttRec, string mode, Exception ex)
        {
            Logger.WriteToLog(string.Format("{0} PostErrorLog Mact: {1} Stt_rec: {2} Mode: {3} Message: {4}\r\nStackTrace: {5}",
                V6Login.ClientName, Mact, sttRec, mode, ex.Message, ex.StackTrace));
            Service.PostErrorLog(Mact, sttRec, mode, ex.Message);
        }

        public decimal GetTyGia(string mant, DateTime ngayct)
        {
            return Service.GetTyGia(mant, ngayct);
        }

        public DataSet GetCheck_VC(string status, string kieu_post, string stt_rec)
        {
            return Service.GetCheck_VC(status, kieu_post, stt_rec, out V6Message);
        }
        public DataTable GetCheck_VC_Save(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            return Service.GetCheck_VC_Save(status, kieu_post, soct, masonb, sttrec, out V6Message);
        }
        public DataTable GetCheck_Save_All(string status, string kieu_post, string soct, string masonb, string sttrec, string madvcs, string makh,
             string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id)
        {
            return Service.GetCheck_Save_All(status, kieu_post, soct, masonb, sttrec, madvcs, makh, manx, ngayct, mact, tongthanhtoan, mode, user_id, out V6Message);
        }
        public DataTable GetCheck_Edit_All(string status, string kieu_post, string soct, string masonb, string sttrec, string madvcs, string makh,
             string manx, DateTime ngayct, string mact, decimal tongthanhtoan, string mode, int user_id)
        {
            return Service.GetCheck_Edit_All(status, kieu_post, soct, masonb, sttrec, madvcs, makh, manx, ngayct, mact, tongthanhtoan,mode,user_id, out V6Message);
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

        //public virtual bool InsertInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool UpdateInvoice(SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList,
        //    SortedDictionary<string, object> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual DataTable GetSoct0_All_Cust(string sttRec, string trim, string filterString)
        {
            throw new NotImplementedException();
        }
        
        public virtual DataTable GetSodu0_All_Cust(string sttRec, string trim, string filterString)
        {
            throw new NotImplementedException();
        }

        public void IncreaseSl_inAM(string sttRec)
        {
            Service.IncreaseSl_inAM(AM, Mact, sttRec);
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

                dValue.Name = row["NameVal"].ToString().Trim().ToUpper();
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

        public DataTable AlLoTon;
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


        protected DataTable defaultData;
        protected DataTable defaultInfo;
        private SortedDictionary<string, string> loadTag;

        public virtual SortedDictionary<string, string> LoadTag(string itemId)
        {
            if (loadTag != null && loadTag.Count > 0) return loadTag;
            if (defaultData == null || defaultData.Rows.Count == 0)
                defaultData = V6BusinessHelper.GetDefaultValueData(1, Mact, "", itemId, "nhom='00'");
            var result = new SortedDictionary<string,string>();
            foreach (DataRow row in defaultData.Rows)
            {
                var cell = row["Tag"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            loadTag = result;
            return result;
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual string Name { get { return _base_name; } set { _base_name = value; } }
        protected string _base_name = "Chứng từ";

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
            var define = Alct.Rows[0]["AM_TEMPLATE"].ToString().Trim();
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
            var define = Alct.Rows[0]["AD_TEMPLATE"].ToString().Trim();
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
        protected int InsertADlist(string name, SqlTransaction transaction, List<SortedDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (SortedDictionary<string, object> adRow in dataList)
            {
                if (InsertIntoTransaction(name, transaction, ADStruct, adRow, false))
                {
                    j++;
                }
            }
            return j;
        }
        /// <summary>
        /// Thêm dữ liệu vào bảng AD2
        /// </summary>
        /// <param name="name">Tên hàm đang chạy.</param>
        /// <param name="transaction"></param>
        /// <param name="dataList">Dữ liệu sẽ thêm.</param>
        /// <param name="isnew">Tạo phiếu mới.</param>
        /// <returns>Số dòng thêm được.</returns>
        protected int InsertAD2list(string name, SqlTransaction transaction, List<SortedDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (SortedDictionary<string, object> adRow in dataList)
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
        protected int InsertAD3list(string name, SqlTransaction transaction, List<SortedDictionary<string, object>> dataList, bool isnew)
        {
            int j = 0;
            foreach (SortedDictionary<string, object> adRow in dataList)
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
            if (WRITE_LOG)
            {
                object stt_rec = data["STT_REC"];
                object stt_rec0 = data["STT_REC0"];
                Logger.WriteToLog(
                    string.Format(info + " Insert " + tableStruct.TableName + " Ma_ct {0} stt_rec {1} row {2} result {3}.\n{4}",
                    Mact, stt_rec, stt_rec0, execute, insertSql));
            }
            return execute > 0;
        }

        protected void WriteLogTransactionComplete(object stt_rec)
        {
            if (WRITE_LOG)
            {
                Logger.WriteToLog(string.Format("Mact {0} stt_rec {1} TRANSACTION COMMITTED.", Mact, stt_rec));
            }
        }
        
    }
}
