﻿using System;
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
    /// Lookup textBox
    /// </summary>
    public class V6VvarTextBox : V6ColorTextBox
    {
        //constructor
        public V6VvarTextBox()
        {
            GotFocus += V6VvarTextBox_GotFocus;
            //_upper = true;
        }

        void V6VvarTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if (LookupInfo.LoadAutoComplete)
                {
                    LoadAutoCompleteSource();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("V6VvarTextBox_GotFocus: " + ex.Message);
            }
        }

        private string _vVar = "";
        //private bool _loadAutoCompleteSrc = false;
        private bool _f5 = true, _f2;
        /// <summary>
        /// Data_ID
        /// </summary>
        private string _text_data = "";
        private DataRow _data;
        private Form _frm;

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

        private StandardConfig _lki;
        private StandardConfig LookupInfo
        {
            get
            {
                if (_lki == null) _lki = V6ControlsHelper.LayThongTinCauHinh(VVar);
                return _lki;
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
        /// Tên các trường dữ liệu liên quan
        /// </summary>
        [DefaultValue(null)]
        [Description("Các trường dữ liệu liên quan được gán tự động khi lookup hoặc check ExistRow")]
        public string BrotherFields { get; set; }
        /// <summary>
        /// Ánh xạ với BrotherFields với tên trường khác.
        /// </summary>
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

        [Description("V6 VVar")]
        [DefaultValue("")]
        public string VVar
        {
            get { return _vVar; }
            set
            {
                _vVar = value;
                if (!string.IsNullOrEmpty(_vVar)) _upper = true;
            }
        }

        private string _initFilter;
        public string InitFilter
        {
            get {
                return _initFilter ?? V6Login.GetInitFilter(LookupInfo.TableName);
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

        protected override void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Save keyDown history();
            if (string.IsNullOrEmpty(_vVar) || LookupInfo.NoInfo)
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
                
                //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                //{
                    //if (e.KeyCode == Keys.Enter)
                    //{
                    //    e.SuppressKeyPress = true;
                    //    SendKeys.Send("{TAB}");
                    //}
                    //DoCharacterCasing();
                    //if (_checkOnLeave && !ReadOnly && Visible)
                    //{
                    //    CheckForBarcode();
                    //    if (Text.Trim() != "")
                    //    {
                    //        if (!string.IsNullOrEmpty(LookupInfo.FieldName))
                    //        {
                    //            if (ExistRowInTable(Text.Trim()))
                    //            {
                    //                if (!Looking && gotfocustext != Text)
                    //                {
                    //                    CallDoV6LostFocus();
                    //                    //CheckForBarcode();
                    //                }
                    //                else
                    //                {
                    //                    CallDoV6LostFocusNoChange();
                    //                }

                    //                if (e.KeyCode == Keys.Enter)
                    //                {
                    //                    e.SuppressKeyPress = true;
                    //                    SendKeys.Send("{TAB}");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                DoLookup();
                    //            }

                    //        }
                    //    }
                    //    else if (_checkNotEmpty && !string.IsNullOrEmpty(LookupInfo.FieldName))
                    //    {
                    //        DoLookup();
                    //    }
                    //    else
                    //    {
                    //        base.V6ColorTextBox_KeyDown(this, e);
                    //    }
                    //}
                    //else
                    //{
                    //    if (e.KeyCode == Keys.Enter)
                    //    {
                    //        e.SuppressKeyPress = true;
                    //        SendKeys.Send("{TAB}");
                    //    }
                    //}
                //}
                //else
                //if (e.KeyCode == Keys.Enter)
                //{
                //   if (!string.IsNullOrEmpty(LookupInfo.FieldName))
                //   {
                //       if (ExistRowInTable(Text.Trim()))
                //       {
                //           if (!Looking && gotfocustext != Text)
                //           {
                //               CallDoV6LostFocus();
                //               //CheckForBarcode();
                //           }
                //           else
                //           {
                //               CallDoV6LostFocusNoChange();
                //           }

                //           if (e.KeyCode == Keys.Enter)
                //           {
                //               e.SuppressKeyPress = true;
                //               SendKeys.Send("{TAB}");
                //           }
                //       }
                //       else
                //       {
                //           DoLookup();
                //       }
                //   }
                //}
                if (F5 && !ReadOnly && e.KeyCode == Keys.F5 && !string.IsNullOrEmpty(LookupInfo.FieldName))
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
            lostfocustext = Text;
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

            if (string.IsNullOrEmpty(_vVar) || LookupInfo.NoInfo)
            {
                base.V6ColorTextBox_LostFocus(sender, e);
            }
            else if (_checkOnLeave && !ReadOnly && Visible && Enabled)
            {
                CheckForBarcode();
                if (textBox.Text.Trim() != "")
                {
                    if (!string.IsNullOrEmpty(LookupInfo.FieldName))
                    {
                        if (ExistRowInTable(textBox.Text.Trim()))
                        {
                            if (!Looking && gotfocustext != lostfocustext)
                            {
                                CallDoV6LostFocus();
                                //CheckForBarcode();
                            }
                            else
                            {
                                CallDoV6LostFocusNoChange();
                            }
                        }
                        else
                        {
                            DoLookup(false);
                        }
                    }
                }
                else if (_checkNotEmpty && !string.IsNullOrEmpty(LookupInfo.FieldName))
                {
                    DoLookup(false);
                }
                else
                {
                    ExistRowInTable(textBox.Text.Trim());
                    base.V6ColorTextBox_LostFocus(sender, e);
                }
            }

            if (_lockFocus)
            {
                _lockFocus = false;
                Focus();
            }
        }

        /// <summary>
        /// Bật chức năng kiểm tra barcode và sinh sự kiện.
        /// </summary>
        [DefaultValue(false)]
        public bool CheckBarCode { get; set; }
        [DefaultValue(false)]
        public bool IsBarcode
        {
            get { return IsThisBarcode(); }
        }
        /// <summary>
        /// Khi có check barcode, có cắt bỏ ký tự phía sau theo V6Options.M_BARCODE_ELENGTH hay không.
        /// </summary>
        [DefaultValue(0)]
        public int TrimBarcode { get; set; }

        /// <summary>
        /// Sự kiện xảy ra khi người dùng nhập xong một barcode.
        /// </summary>
        public event EventHandler InputBarcode;
        protected virtual void OnInputBarcode()
        {
            var handler = InputBarcode;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private bool IsThisBarcode()
        {
            try
            {
                //return _checksum_ean13(Text);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //static bool _checksum_ean13(String data)
        //{
        //    // Test string for correct length
        //    if (data.Length != 12 && data.Length != 13)
        //        return false;

        //    // Test string for being numeric
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        if (data[i] < 0x30 || data[i] > 0x39)
        //            return false;
        //    }

        //    int sum = 0;

        //    for (int i = 11; i >= 0; i--)
        //    {
        //        int digit = data[i] - 0x30;
        //        if ((i & 0x01) == 1)
        //            sum += digit;
        //        else
        //            sum += digit * 3;
        //    }
        //    int mod = sum % 10;
        //    int cnum = mod == 0 ? 0 : 10 - mod;

        //}

        /// <summary>
        /// Kiểm tra có phải là barcode hay không? Nếu đúng sinh sự kiện.
        /// </summary>
        private void CheckForBarcode()
        {
            if (CheckBarCode && IsBarcode)
            {
                if (true || TrimBarcode>0)//bỏ true đi
                {
                    if(V6Options.M_BARCODE_ELENGTH > 0)
                        Text = Text.Substring(0, TextLength - TrimBarcode);
                }

                OnInputBarcode();
            }
        }
        

        #endregion event

        protected AutoCompleteStringCollection auto1;
        public bool _lockFocus;

        public void LoadAutoCompleteSource()
        {
            if (auto1 != null) return;

            if (!string.IsNullOrEmpty(_vVar) && !string.IsNullOrEmpty(LookupInfo.FieldName) && auto1 == null)
            {
                try
                {
                    auto1 = new AutoCompleteStringCollection();

                    var selectTop = LookupInfo.LargeYn ? "top 10000" : "";
                    
                    if (!string.IsNullOrEmpty(LookupInfo.FieldName))
                    {
                        var tableName = LookupInfo.TableName;
                        var filter = InitFilter;
                        if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                        var where = " 1=1 " + filter;
                            
                        var tbl1 = V6BusinessHelper.Select(tableName,
                            selectTop + " [" + LookupInfo.FieldName + "]",
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
                catch (Exception ex)
                {
                    V6Message.Show("LoadAutoCompleteSource " + _vVar + " " + ex.Message);
                    V6ControlsHelper.DisableLookup = false;
                }
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
                Logger.WriteToLog("V6VvarTextBox ChangeAutoCompleteMode " + ex.Message, Application.ProductName);
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
            try
            {
                _text_data = text;
                if (!string.IsNullOrEmpty(LookupInfo.FieldName))
                {
                    string tableName = LookupInfo.TableName;
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@text", text)
                    };
                    var tbl = V6BusinessHelper.Select(tableName, "*", LookupInfo.FieldName + "=@text " + filter, "", "", plist).Data;

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
                V6ControlFormHelper.ShowErrorMessage(ex.Message);
                return false;
            }
            return false;
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
            catch (Exception)
            {
                // ignored
            }
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
            catch (Exception)
            {
                // ignored
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

        public IDictionary<string, object> ParentData;

        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện V6LostFocus
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public override void ChangeText(string text)
        {
            var inText = Text;
            Text = text;
            ExistRowInTable(Text.Trim());
            if (inText != Text) CallDoV6LostFocus();
            else CallDoV6LostFocusNoChange();
        }

        protected void DoLookup(bool multi = false)
        {
            var filter = InitFilter;
            if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
            var fStand = new Standard(this, LookupInfo, " 1=1 " + filter, multi, FilterStart);
            Looking = true;
            fStand.ShowDialog(this);
        }

        public void Lookup(bool multi = false)
        {
            DoLookup(multi);
        }

        
    }
}
