using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Implementations;
using V6Init;
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

        public void IncreaseSl_inAm81(string sttRec)
        {
            Service.IncreaseSl_inAm81(sttRec);
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
    }
}
