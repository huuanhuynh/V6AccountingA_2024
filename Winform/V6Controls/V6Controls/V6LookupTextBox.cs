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
    /// Lookup textBox, Dữ liệu khác với hiển thị.
    /// </summary>
    public class V6LookupTextBox : V6ColorTextBox
    {
        //constructor
        public V6LookupTextBox()
        {
            GotFocus += V6LookupTextBox_GotFocus;
            //_upper = true;
        }

        /// <summary>
        /// Dùng cho ShowTextField, làm trường dữ liệu cho ShowText khi lấy data.
        /// </summary>
        [DefaultValue(null)]
        [Description("Dùng cho ShowTextField, làm trường dữ liệu cho ShowText khi lấy data.")]
        public string AccessibleName2 { get; set; }

        void V6LookupTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                LoadAutoCompleteSource();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6LookupTextBox_GotFocus: " + ex.Message);
            }
        }

        private bool _f5 = true, _f2;
        private string _ma_dm = "";
        private string _text_data = "";
        private DataRow _data;
        
        /// <summary>
        /// Dữ liệu liên quan khi chọn mã
        /// </summary>
        public DataRow Data
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
                V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
                SetNeighborValues();
            }
        }

        private AldmConfig _lki;
        public AldmConfig LookupInfo
        {
            get
            {
                try
                {
                    return _lki ?? (_lki = V6ControlsHelper.GetAldmConfig(_ma_dm));
                }
                catch (Exception)
                {
                    return new AldmConfig()
                    {
                        NoInfo = true,Error = true
                    };
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
        
        public void SetDataRow(DataRow data)
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
                    V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
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
            get { return Data == null ? null : Data[ValueField]; }
        }

        public void SetValue(object value)
        {
            try
            {
                ExistRowInTableID(value);
                //this.WriteToLog("V6LookupTextBox", "Chưa viết SetValue");
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
            get { return _valueField; }
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
        
        //[Description("Tên trường .")]
        //[DefaultValue(null)]
        //public string NameField
        //{
        //    get { return _name_field; }
        //    set { _name_field = value; }
        //}

        private string _initFilter;
        public string InitFilter
        {
            get {
                return _initFilter ?? V6Login.GetInitFilter(LookupInfo.TABLE_NAME);
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
            if (string.IsNullOrEmpty(LookupInfo.TABLE_NAME))
            {
                base.V6ColorTextBox_KeyDown(sender, e);
                if (e.KeyData == Keys.Enter) SendKeys.Send("{TAB}");
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

                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                //{
                //    DoCharacterCasing();
                //    if (_checkOnLeave && !ReadOnly && Visible)
                //    {
                //        if (Text.Trim() != "")
                //        {
                //            if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                //            {
                //                if (ExistRowInTable(Text.Trim()))
                //                {
                //                    if (!Looking && gotfocustext != Text) CallDoV6LostFocus();
                //                    else CallDoV6LostFocusNoChange();
                //                    if (e.KeyCode == Keys.Enter)
                //                    {
                //                        e.SuppressKeyPress = true;
                //                        //SendKeys.Send("{TAB}");
                //                    }
                //                }
                //                else
                //                {
                //                    DoLookup();
                //                }

                //            }
                //        }
                //        else if (_checkNotEmpty && !string.IsNullOrEmpty(LookupInfo_F_NAME))
                //        {
                //            DoLookup();
                //        }
                //        else
                //        {
                //            base.V6ColorTextBox_KeyDown(this, e);
                //        }
                //    }
                //    else
                //    {
                //        if (e.KeyCode == Keys.Enter)
                //        {
                //            e.SuppressKeyPress = true;
                //            //SendKeys.Send("{TAB}");
                //        }
                //    }
                //}
                //else
                    if (F5 && !ReadOnly && e.KeyCode == Keys.F5 && !string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    LoadAutoCompleteSource();
                    DoLookup();
                }
                else if (!ReadOnly && e.KeyCode == Keys.F2)
                {
                    if (F2)
                    {
                        DoLookup(true);
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
            if (_checkOnLeave && !ReadOnly && Visible)
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
                            DoLookup(false);
                        }
                    }
                }
                else if (_checkNotEmpty && !string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    DoLookup(false);
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
            if (V6Setting.IsDesignTime) return;
            if (auto1 != null) return;
            if (LookupInfo.NoInfo) return;

            try
            {
                if (!string.IsNullOrEmpty(LookupInfo.TABLE_NAME) && !string.IsNullOrEmpty(LookupInfo_F_NAME) &&
                    auto1 == null)
                {

                    auto1 = new AutoCompleteStringCollection();

                    var selectTop = "";

                    if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                    {
                        var tableName = LookupInfo.TABLE_NAME;
                        var filter = InitFilter;
                        if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                        var where = " 1=1 " + filter;

                        var tbl1 = V6BusinessHelper.Select(tableName,
                            selectTop + " [" + LookupInfo_F_NAME + "]",
                            where, "", "", null).Data;

                        for (int i = 0; i < tbl1.Rows.Count; i++)
                        {
                            auto1.Add(tbl1.Rows[i][0].ToString().Trim());
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
                Logger.WriteToLog("V6LookupTextBox ChangeAutoCompleteMode " + ex.Message, Application.ProductName);
            }
        }

        /// <summary>
        /// Kiểm tra, lấy lại data, gán brothers
        /// </summary>
        /// <returns></returns>
        public bool ExistRowInTable()
        {
            return ExistRowInTable(Text.Trim());
        }

        /// <summary>
        /// Kiểm tra giá trị có tồn tại trong csdl hay không. Đồng thời gán dữ liệu liên quan (Brothers, Neighbor).
        /// </summary>
        /// <param name="text">Giá trị cần kiểm tra</param>
        /// <returns></returns>
        public bool ExistRowInTable(string text)
        {
            if (V6Setting.IsDesignTime) return false;
            try
            {
                _text_data = text;
                if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    string tableName = LookupInfo.TABLE_NAME;
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@text", text)
                    };
                    var tbl = V6BusinessHelper.Select(tableName, "*", LookupInfo_F_NAME + "=@text " + filter, "", "", plist).Data;

                    if (tbl != null && tbl.Rows.Count >= 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow;
                        V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
                        SetNeighborValues();
                        return true;
                    }
                    else
                    {
                        _data = null;
                        V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
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
        
        private bool ExistRowInTableID(object id)
        {
            if (V6Setting.IsDesignTime) return false;
            try
            {
                if (!string.IsNullOrEmpty(LookupInfo_F_NAME))
                {
                    string tableName = LookupInfo.TABLE_NAME;
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@id", id)
                    };
                    var tbl = V6BusinessHelper.Select(tableName, "*", ValueField + "=@id " + filter, "", "", plist).Data;

                    if (tbl != null && tbl.Rows.Count >= 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow;
                        FixText();
                        V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
                        SetNeighborValues();
                        return true;
                    }
                    else
                    {
                        _data = null;
                        FixText();
                        V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
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

        private void FixText()
        {
            //Fix text
            if (Data != null)
            {
                Text = Data[LookupInfo_F_NAME].ToString().Trim();
                _text_data = Text;
            }
            else
            {
                Text = "";
                _text_data = "";
            }
        }

        public void RefreshLoDateYnValue()
        {
            try
            {
                if (Data == null)
                {
                    LO_YN = false;
                    DATE_YN = false;
                    VITRI_YN = false;
                    VT_TON_KHO = false;
                    GIA_TON = 0;
                    return;
                }
                if (Data.Table.Columns.Contains("LO_YN"))
                    LO_YN = ObjectAndString.ObjectToInt(Data["LO_YN"]) == 1;
                if (Data.Table.Columns.Contains("DATE_YN"))
                    DATE_YN = ObjectAndString.ObjectToInt(Data["DATE_YN"]) == 1;
                if (Data.Table.Columns.Contains("VITRI_YN"))
                    VITRI_YN = ObjectAndString.ObjectToInt(Data["VITRI_YN"]) == 1;
                if (Data.Table.Columns.Contains("VT_TON_KHO"))
                    VT_TON_KHO = ObjectAndString.ObjectToInt(Data["VT_TON_KHO"]) == 1;
                if (Data.Table.Columns.Contains("GIA_TON"))
                    GIA_TON = ObjectAndString.ObjectToInt(Data["GIA_TON"]);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RefreshLoDateYnValue", ex);
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
                V6ControlFormHelper.SetNeighborData(this, _data, neighbor_field);
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

        protected void DoLookup(bool multi = false)
        {
            if (V6Setting.IsDesignTime) return;

            try
            {
                if (LookupInfo.NoInfo) return;
                //_frm = FindForm();
                var filter = InitFilter;
                if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                var fStand = new V6LookupTextboxForm(ParentData, this.Text, LookupInfo, " 1=1 " + filter, LookupInfo_F_NAME, multi, FilterStart);
                Looking = true;
                DialogResult dsr = fStand.ShowDialog(this);
                Looking = false;
                if (dsr == DialogResult.OK)
                {
                    Text = fStand._senderText;
                    if (!multi) Data = fStand.selectedDataRow;
                }
                else
                {
                    //Kiem tra neu gia tri khong hop le thi xoa            
                    if (!multi && !ExistRowInTable())
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

        public void Lookup(bool multi = false)
        {
            DoLookup(multi);
        }
    }
}
