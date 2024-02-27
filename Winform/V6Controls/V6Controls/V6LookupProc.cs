using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// Lookup textBox, Dữ liệu (value) khác với hiển thị (text). Khác với lookupTextBox, dữ liệu được tải bằng procedure.
    /// </summary>
    public class V6LookupProc : V6ColorTextBox
    {
        //constructor
        public V6LookupProc()
        {
            TextChanged += V6LookupProc_TextChanged;
            GotFocus += V6LookupProc_GotFocus;
            //_upper = true;
        }

        void V6LookupProc_TextChanged(object sender, EventArgs e)
        {
            if (Focused && (_showName || _checkOnLeave))
            {
                V6ControlsHelper.ShowLookupProcName(this);
            }
        }

        /// <summary>
        /// Dùng cho ShowTextField, làm trường dữ liệu cho ShowText khi lấy data.
        /// </summary>
        [DefaultValue(null)]
        [Description("Dùng cho ShowTextField, làm trường dữ liệu cho ShowText khi lấy data.")]
        public string AccessibleName2 { get; set; }

        void V6LookupProc_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if (_showName || _checkOnLeave)
                {
                    V6ControlsHelper.ShowLookupProcName(this);
                }
                LoadAutoCompleteSource();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupProc_GotFocus: " + ex.Message);
            }
        }

        /// <summary>
        /// Giá trị tham số Proc 1.
        /// </summary>
        [Description("Giá trị tham số Proc 1.")]
        public string MA_CT { get; set; }
        private bool _showName = false;
        /// <summary>
        /// Hiển thị tên khi nhảy vào.
        /// </summary>
        [Category("V6")]
        [DefaultValue(false)]
        [Description("Bật hiển thị tên khi vào.")]
        public bool ShowName { get { return _showName; } set { _showName = value; } }
        
        private bool _f5 = true, _f2;
        private string _ma_dm = "";
        private string _text_data = "";
        private IDictionary<string, object> _data;
        
        /// <summary>
        /// Dữ liệu liên quan khi chọn mã
        /// </summary>
        public IDictionary<string, object> Data
        {
            get
            {
                if (_data != null && _text_data == Text)
                    return _data;
                else
                {
                    ExistRowInTable(Text);
                }
                return _data;
            }
            private set
            {
                _data = value;
                V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                SetNeighborValues();
            }
        }

        public List<IDictionary<string, object>> Datas;

        private AldmConfig _lki;
        public AldmConfig LookupInfo
        {
            get
            {
                try
                {
                    return _lki ?? (_lki = ConfigManager.GetAldmConfig(_ma_dm));
                }
                catch (Exception)
                {
                    return new AldmConfig();
                }
            }
        }

        /// <summary>
        /// Bật tắt tính năng lọc chỉ bắt đầu. Mặc định false sẽ lọc like '%abc%'.
        /// </summary>
        [Category("V6")]
        [Description("Lọc start trong sql (like 'abc%'")]
        [DefaultValue(false)]
        public bool FilterStart { get; set; }

        public void SetDataRaw(IDictionary<string, object> data)
        {
            Data = data;
        }

        /// <summary>
        /// Tên các trường dữ liệu liên quan (trong cùng table hoặc view trong csdl).
        /// </summary>
        [Category("V6")]
        [DefaultValue(null)]
        [Description("Các trường dữ liệu liên quan được gán tự động khi lookup hoặc check ExistRow")]
        public string BrotherFields { get; set; }
        /// <summary>
        /// Tên các trường dữ liệu liên quan khi dùng ngôn ngữ khác V
        /// </summary>
        [Category("V6")]
        [DefaultValue(null)]
        [Description("Các trường dữ liệu liên quan trường hợp ngôn ngữ khác V")]
        public string BrotherFields2 { get; set; }
        /// <summary>
        /// Ánh xạ với BrotherFields với tên trường khác.
        /// </summary>
        [Category("V6")]
        [DefaultValue(null)]
        [Description("Ánh xạ với BrotherFields để gán dữ liệu cho các control khác")]
        public string NeighborFields { get; set; }

        /// <summary>
        /// Gán lại brothers với _data sẵn có.
        /// </summary>
        public void SetBrotherFormData()
        {
            if (BrotherFields != null)
            {
                if (_data != null)
                {
                    V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                    SetNeighborValues();
                }
                else
                {
                    ExistRowInTable();
                }
            }
        }

        public int Int_Data(string field)
        {
            if (Data != null)
            {
                return Convert.ToInt32(_data[field]);
            }
            return 0;
        }

        [Description("Có hay không sử dụng phím F2 để tìm chọn nhiều mã trên danh mục!")]
        [DefaultValue(false)]
        public bool F2
        {
            get { return _f2; }
            set { _f2 = value; }
        }

        [Description("Có hay không sử dụng phím F5 để dò tìm trên danh mục!")]
        [DefaultValue(true)]
        public bool F5
        {
            get { return _f5; }
            set { _f5 = value; }
        }

        //[Description("Tải hoặc không danh sách mã danh mục theo VVar.\n(chưa sử dụng được, hãy chạy hàm Load...() trước để sử dụng chức năng autocomplete).")]
        //[DefaultValue(false)]
        //public bool LoadAutoCompleteSrc
        //{
        //    get { return _loadAutoCompleteSrc; }
        //    set { _loadAutoCompleteSrc = value; }
        //}

        /// <summary>
        /// Cờ đã xử lý khi bấm enter.
        /// </summary>
        private bool _checkOnLeave_OnEnter = false;
        private bool _checkOnLeave = true;
        [Description("Bật tắt kiểm tra tính hợp lệ của dữ liệu khi rời khỏi.")]
        [DefaultValue(true)]
        public bool CheckOnLeave
        {
            get { return _checkOnLeave; }
            set { _checkOnLeave = value; }
        }
        private bool _checkNotEmpty;
        [Description("Bật tắt kiểm tra phải có của dữ liệu khi rời khỏi.")]
        [DefaultValue(false)]
        public bool CheckNotEmpty
        {
            get { return _checkNotEmpty; }
            set { _checkNotEmpty = value; }
        }

        [Description("Mã quản lý trong Aldm.")]
        [DefaultValue("")]
        public string Ma_dm
        {
            get { return _ma_dm; }
            set { _ma_dm = value; }
        }

        [Description("Dữ liệu theo valueField.")]
        public object Value
        {
            get
            {
                if (Text.Contains(",") && Datas != null)
                {
                    string values = "";
                    foreach (IDictionary<string, object> data in Datas)
                    {
                        values += Data == null ? null : "," + data[ValueField];
                    }
                    if (values.Length > 0) values = values.Substring(1);
                    return values;
                }

                if (Data == null || _text_data != Text) ExistRowInTable();
                
                return Data == null ? null : Data[ValueField.ToUpper()];
            }
        }

        public void SetValue(object value)
        {
            try
            {
                ExistRowInTableID(value);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetValue", ex);
            }
        }

        [Category("V6")]
        [Description("Tên trường lấy dữ liệu.")]
        [DefaultValue(null)]
        public string ValueField
        {
            get {
                if (LookupInfo != null) return LookupInfo.VALUE; // đảo ưu tiên ALDM.
                return (_valueField??"").ToUpper();
            }
            set { _valueField = value; }
        }
        private string _valueField;

        [Category("V6")]
        [Description("Tên trường dữ liệu hiển thị. Bị đè bởi LookupInfo.F_NAME")]
        [DefaultValue(null)]
        public string ShowTextField
        {
            get { return _showTextField; }
            set { _showTextField = value; }
        }
        private string _showTextField;

        /// <summary>
        /// Trường lấy dữ liệu hiển thị. Ưu tiên LookupInfo.F_NAME, nếu không có sẽ lấy ShowTextField hoặc ValueField.
        /// </summary>
        public string LookupInfo_F_NAME
        {
            get
            {
                if (LookupInfo != null && !string.IsNullOrEmpty(LookupInfo.F_NAME)) return LookupInfo.F_NAME;
                if (!string.IsNullOrEmpty(_showTextField)) return _showTextField;
                if (!string.IsNullOrEmpty(_valueField)) return _valueField;
                return null;
            }
        }
        
        private string _initFilter;
        public string InitFilter
        {
            get
            {
                if (_initFilter == null)
                {
                    _initFilter = V6Login.GetInitFilter(LookupInfo.TABLE_NAME, GetFilterType());
                }
                return _initFilter;
            }
        }

        /// <summary>
        /// Nếu gán giá trị khác null thì filter mặt định ở V6Login.GetInitFilter sẽ bị bỏ qua
        /// </summary>
        /// <param name="filter"></param>
        public void SetInitFilter(string filter)
        {
            _initFilter = filter;
        }

        public string Advance2 = "";
        public string Advance3 = "";
        public string STT_REC = "";
        public DateTime NGAY_CT = DateTime.Now;
        public string KIEU_POST = "";
        public string MODE = "";
        public string MA_KH = "";

        #region ==== Event ====
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReadOnly || !Enabled)
            {
                if (e.KeyData == Keys.Enter) SendKeys.Send("{TAB}");
                return;
            }

            if (string.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                base.V6ColorTextBox_KeyDown(sender, e);
            }
            else
            {
                if (e.KeyData == (Keys.Control|Keys.Q))
                {
                    V6ControlsHelper.DisableLookup = true;
                    SwitchAutoCompleteMode();
                    return;
                }

                if (e.KeyData == Keys.Enter)
                {
                    //Do check on leave
                    Do_CheckOnLeave(new EventArgs());
                    //Flag
                    _checkOnLeave_OnEnter = true;
                    //Send Tab
                    SendKeys.Send("{TAB}");
                }
                else if (F5 && !ReadOnly && e.KeyCode == Keys.F5 && !string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    LoadAutoCompleteSource();
                    DoLookup();
                }
                else if (!ReadOnly && e.KeyCode == Keys.F2)
                {
                    if (F2)
                    {
                        DoLookup(LookupMode.Multi);
                    }
                }
                else
                {
                    base.V6ColorTextBox_KeyDown(this, e);
                }
            }
        }

        /// <summary>
        /// Override hoàn toàn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void V6ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            DoCharacterCasing();
            if (EnableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }

            var textBox = (TextBox)sender;
            if (V6ControlsHelper.DisableLookup)
            {
                V6ControlsHelper.DisableLookup = false;
                return;
            }

            if (LookupInfo.NoInfo) return;

            if (string.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                base.V6ColorTextBox_LostFocus(sender, e);
            }
            else
            {
                if (_checkOnLeave_OnEnter)
                {
                    // Đã xử lý KeyDown Enter.
                    _checkOnLeave_OnEnter = false;
                }
                else
                {
                    Do_CheckOnLeave(e);
                }
            }

            if (_lockFocus)
            {
                _lockFocus = false;
                Focus();
            }
        }

        private void Do_CheckOnLeave(EventArgs e)
        {
            if (F2 && Text.Contains(","))
            {
                ExistRowInTableID(Text);
                return;
            }

            if (_checkOnLeave && !ReadOnly && Visible && Enabled)
            {
                if (Text.Trim() != "")
                {
                    if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                    {
                        if (ExistRowInTable(Text.Trim()))
                        {
                            FixText();

                            if (!Looking && gotfocustext != Text) CallDoV6LostFocus();
                            else CallDoV6LostFocusNoChange();
                        }
                        else
                        {
                            DoLookup(LookupMode.Single);
                        }
                    }
                }
                else if (_checkNotEmpty && !string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    DoLookup(LookupMode.Single);
                }
                else
                {
                    ExistRowInTable(Text.Trim());
                    base.V6ColorTextBox_LostFocus(this, e);
                }
            }
            else if (!_checkOnLeave && !ReadOnly && Visible && Enabled)
            {
                ExistRowInTable(Text.Trim());
            }
        }
        
        #endregion event

        protected AutoCompleteStringCollection auto1;
        public bool _lockFocus;

        public void LoadAutoCompleteSource()
        {
            if (V6Setting.NotLoggedIn) return;
            //if (auto1 != null) return;
            if (LookupInfo.NoInfo) return;

            if (LookupInfo != null && LookupInfo.HaveInfo)
            {
                if (LookupInfo.EXTRA_INFOR.ContainsKey("LARGE_YN") && ObjectAndString.ObjectToBool(LookupInfo.EXTRA_INFOR["LARGE_YN"]))
                {
                    return;
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(LookupInfo.TABLE_NAME) && !string.IsNullOrEmpty(LookupInfo_F_NAME) && auto1 == null)
                {
                    auto1 = new AutoCompleteStringCollection();

                    if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                    {
                        var filter = InitFilter ?? "";
                        
                        List<SqlParameter> plist = new List<SqlParameter>();
                        plist.Add(new SqlParameter("@ma_ct", this.MA_CT));
                        plist.Add(new SqlParameter("@stt_rec", STT_REC));
                        plist.Add(new SqlParameter("@MA_KH", MA_KH));
                        plist.Add(new SqlParameter("@ngay_ct", NGAY_CT.Date));
                        plist.Add(new SqlParameter("@Kieu_post", KIEU_POST));
                        plist.Add(new SqlParameter("@MODE", MODE));
                        
                        plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                        plist.Add(new SqlParameter("@advance", filter));
                        plist.Add(new SqlParameter("@advance2", Advance2));
                        plist.Add(new SqlParameter("@advance3", Advance3));
                        var tbl1 = V6BusinessHelper.ExecuteProcedure(LookupInfo.TABLE_NAME, plist.ToArray()).Tables[0];

                        for (int i = 0; i < tbl1.Rows.Count; i++)
                        {
                            auto1.Add(tbl1.Rows[i][LookupInfo_F_NAME].ToString().Trim());
                        }
                        V6ControlsHelper.DisableLookup = true;
                        AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        //AutoCompleteMode = AutoCompleteMode.Suggest;
                        AutoCompleteCustomSource = auto1;
                        AutoCompleteSource = AutoCompleteSource.CustomSource;
                        V6ControlsHelper.DisableLookup = false;
                    }

                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAutoCompleteSource" + LookupInfo.TABLE_NAME
                    + string.Format(" LookupInfo:{0}, LookupInfo_F_NAME:{1}", LookupInfo==null?"null":"", LookupInfo_F_NAME??"null"), ex);
                V6ControlsHelper.DisableLookup = false;
            }
        }

        private AutoCompleteMode _oldAutoCompleteMode;
        public void SwitchAutoCompleteMode()
        {
            try
            {
                switch (AutoCompleteMode)
                {
                    case AutoCompleteMode.Suggest:
                        _oldAutoCompleteMode = AutoCompleteMode;
                        AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        break;
                    case AutoCompleteMode.Append:
                    case AutoCompleteMode.SuggestAppend:
                        _oldAutoCompleteMode = AutoCompleteMode;
                        AutoCompleteMode = AutoCompleteMode.Suggest;
                        break;
                    default:
                        AutoCompleteMode = AutoCompleteMode.Suggest;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6LookupProc ChangeAutoCompleteMode " + ex.Message, Application.ProductName);
            }
        }

        /// <summary>
        /// Kiểm tra, lấy lại data, gán brothers, luôn dùng InitFilter.
        /// </summary>
        /// <returns></returns>
        public bool ExistRowInTable()
        {
            return ExistRowInTable(Text.Trim());
        }

        /// <summary>
        /// Kiểm tra giá trị có tồn tại trong csdl hay không. Đồng thời gán dữ liệu liên quan (Brothers, Neighbor). luôn dùng InitFilter.
        /// </summary>
        /// <param name="text">Giá trị cần kiểm tra</param>
        /// <returns></returns>
        public bool ExistRowInTable(string text)
        {
            if (V6Setting.NotLoggedIn) return false;
            try
            {
                _text_data = text;
                if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@ma_ct", this.MA_CT));
                    plist.Add(new SqlParameter("@stt_rec", STT_REC));
                    plist.Add(new SqlParameter("@MA_KH", MA_KH));
                    plist.Add(new SqlParameter("@ngay_ct", NGAY_CT.Date));
                    plist.Add(new SqlParameter("@Kieu_post", KIEU_POST));
                    plist.Add(new SqlParameter("@MODE", MODE));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    plist.Add(new SqlParameter("@advance", LookupInfo_F_NAME + "=N'"+text+"'" + filter));
                    plist.Add(new SqlParameter("@advance2", Advance2));
                    plist.Add(new SqlParameter("@advance3", Advance3));
                    var tbl = V6BusinessHelper.ExecuteProcedure(LookupInfo.TABLE_NAME, plist.ToArray()).Tables[0];
                    
                    if (tbl != null && tbl.Rows.Count == 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow.ToDataDictionary();
                        V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                        SetNeighborValues();
                        return true;
                    }
                    else
                    {
                        _data = null;
                        V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                        SetNeighborValues();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ExistRowInTable", ex);
                return false;
            }
            return false;
        }
        
        private bool ExistRowInTableID(object id_s)
        {
            if (V6Setting.NotLoggedIn) return false;
            try
            {
                if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    string where = "";
                    if (id_s.ToString().Contains(","))
                    {
                        string[] sss = id_s.ToString().Split(',');
                        foreach (string s in sss)
                        {
                            where += string.Format(" or {3}{0} {1} {2}", ValueField, "=", "'" + s.Trim().Replace("'", "''") + "'", null);
                        }
                        if (where.Length > 4)
                        {
                            where = "(" + where.Substring(4) + ")";
                        }
                        where += filter;
                    }
                    else
                    {
                        where = ValueField + "='" + id_s + "'" + filter;
                    }

                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@ma_ct", this.MA_CT));
                    plist.Add(new SqlParameter("@stt_rec", STT_REC));
                    plist.Add(new SqlParameter("@MA_KH", MA_KH));
                    plist.Add(new SqlParameter("@ngay_ct", NGAY_CT.Date));
                    plist.Add(new SqlParameter("@Kieu_post", KIEU_POST));
                    plist.Add(new SqlParameter("@MODE", MODE));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    plist.Add(new SqlParameter("@advance", where));
                    plist.Add(new SqlParameter("@advance2", Advance2));
                    plist.Add(new SqlParameter("@advance3", Advance3));
                    var tbl = V6BusinessHelper.ExecuteProcedure(LookupInfo.TABLE_NAME, plist.ToArray()).Tables[0];
                    
                    if (tbl != null && tbl.Rows.Count == 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow.ToDataDictionary();
                        Datas = null;
                        FixText();
                        V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                        SetNeighborValues();
                        return true;
                    }
                    else if (tbl != null && tbl.Rows.Count > 1)
                    {
                        _data = null;
                        List<IDictionary<string,object>> dataList = new List<IDictionary<string, object>>();
                        foreach (DataRow row in tbl.Rows)
                        {
                            dataList.Add(row.ToDataDictionary());
                        }
                        Datas = dataList;
                        FixText();
                        V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                        SetNeighborValues();
                        return true;
                    }
                    else
                    {
                        Text = "";
                        _data = null;
                        _text_data = "";
                        FixText();
                        V6ControlFormHelper.SetBrotherDataProc(this, _data, BrotherFields, BrotherFields2);
                        SetNeighborValues();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ExistRowInTable", ex);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Sửa lại giá trị, bỏ bớt phần tử sai khi chọn nhiều mã.
        /// </summary>
        private void FixText()
        {
            //Fix text
            if (Data != null && Data.ContainsKey(LookupInfo_F_NAME.ToUpper()))
            {
                Text = Data[LookupInfo_F_NAME.ToUpper()].ToString().Trim();
                _text_data = Text;
            }
            else if (Datas != null)
            {
                //var delete_list = ObjectAndString.SplitString(Text).ToList();
                //string new_text = Text.Length>0 ? "," + Text + "," : "";
                //foreach (IDictionary<string, object> data in Datas)
                //{
                //    if (data != null && data.ContainsKey(LookupInfo_F_NAME.ToUpper()))
                //    {
                //        string ID = data[LookupInfo_F_NAME.ToUpper()].ToString().Trim();
                //        delete_list.Remove(ID);
                //        delete_list.Remove(ID);
                //        delete_list.Remove(ID);
                //        if (!new_text.Contains("," + ID + ","))
                //        {
                //            new_text = new_text + ID + ",";
                //        }
                //    }
                //}

                //foreach (string ID in delete_list)
                //{
                //    if (new_text.Contains("," + ID + ","))
                //    {
                //        new_text = new_text.Replace("," + ID, "");
                //    }
                //}

                //if (new_text.StartsWith(",")) new_text = new_text.Substring(1);
                //if (new_text.EndsWith(",")) new_text = new_text.Substring(0, new_text.Length-1);

                var new_text = "";
                foreach (IDictionary<string, object> data in Datas)
                {
                    if (data != null && data.ContainsKey(LookupInfo_F_NAME.ToUpper()))
                    {
                        string ID = data[LookupInfo_F_NAME.ToUpper()].ToString().Trim();
                        new_text += "," + ID;
                    }
                }
                if (new_text.Length > 0) new_text = new_text.Substring(1);
                Text = new_text;
                _text_data = "";
            }
            else
            {
                Text = "";
                _text_data = "";
            }
        }


        /// <summary>
        /// Gán lại các biến và giá trị về rỗng hoặc null.
        /// </summary>
        public void Reset()
        {
            Text = "";
            _text_data = "";
            Data = null;
        }

        private void SetNeighborValues()
        {
            try
            {
                if (string.IsNullOrEmpty(BrotherFields) || string.IsNullOrEmpty(NeighborFields))
                    return;

                var bList = BrotherFields.ToUpper().Split(',');
                var nList = NeighborFields.Split(',');
                var max = bList.Length > nList.Length ? bList.Length : nList.Length;
                IDictionary<string, string> neighbor_field = new Dictionary<string, string>();
                for (int i = 0; i < max; i++)
                {
                    neighbor_field.Add(nList[i].ToUpper(), bList[i].ToUpper());
                }
                V6ControlFormHelper.SetNeighborDataProc(this, _data, neighbor_field);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetNeighborValues", ex);
            }
        }

        /// <summary>
        /// AccessibleName like '%value%'
        /// </summary>
        public override string Query
        {
            get
            {
                var sValue = Text.Trim();
                var result = "";

                var oper = "like";
                //if (oper == "start") oper = "like";

                if (sValue.Contains(","))
                {
                    string[] sss = sValue.Split(',');
                    foreach (string s in sss)
                    {
                        result += string.Format(" or {0} {1} {2}", AccessibleName, oper, FormatStringValue(s));
                    }

                    if (result.Length > 4)
                    {
                        result = result.Substring(4);
                        result = string.Format("({0})", result);
                    }
                }
                else
                {
                    result = string.Format("{0} {1} {2}", AccessibleName, oper, FormatStringValue(Text));
                }
                return result;
            }
        }

        public IDictionary<string, object> ParentData {get; set; }
        
        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện V6LostFocus
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public override void ChangeText(string text)
        {
            if (LookupInfo.NoInfo) return;
            var inText = Text;
            Text = text;
            ExistRowInTable(Text.Trim());
            if (inText != Text) CallDoV6LostFocus();
            else CallDoV6LostFocusNoChange();
        }

        protected void DoLookup(LookupMode lookupMode = LookupMode.Single)
        {
            if (V6Setting.NotLoggedIn) return;

            try
            {
                if (LookupInfo.NoInfo) return;
                //_frm = FindForm();
                var filter = InitFilter;
                if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                var lookup = new V6LookupProcForm(this, ParentData, this.Text, LookupInfo, " 1=1 " + filter, LookupInfo_F_NAME, lookupMode, FilterStart);
                Looking = true;
                DialogResult dsr = lookup.ShowDialog(this);
                Looking = false;
                if (dsr == DialogResult.OK)
                {
                    Text = lookup._senderText;
                    if (lookupMode == LookupMode.Single) Data = lookup._selectedData;
                    else if (lookupMode == LookupMode.Multi) Datas = lookup._selectedDataList;
                }
                else
                {
                    //Kiem tra neu gia tri khong hop le thi xoa            
                    if (lookupMode == LookupMode.Single && !ExistRowInTable())
                    {
                        Clear();
                        if (CheckNotEmpty || CheckOnLeave)
                            _lockFocus = true;
                        else _lockFocus = false;
                    }
                    else
                    {
                        _lockFocus = false;
                    }
                    SetLooking(false);
                    //Clear();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoLookup", ex);
            }
        }

        public void Lookup(LookupMode multi = LookupMode.Single)
        {
            DoLookup(multi);
        }
    }
}
