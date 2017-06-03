using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows.Forms;
using H_Controls.Controls.Lookup;
using H_Utility.Converter;
using H_Utility.Helper;

namespace H_Controls.Controls
{
    /// <summary>
    /// Lookup textBox TableName, FieldName=AccessibleName, DisplayFields
    /// </summary>
    public class LookupTextBox : ColorTextBox
    {
        //constructor
        public LookupTextBox()
        {
            GotFocus += VvarTextBox_GotFocus;
            _upper = true;
        }

        void VvarTextBox_GotFocus(object sender, EventArgs e)
        {

        }

        //private bool _loadAutoCompleteSrc = false;
        private bool _f3 = true, _f2;

        private string _textData = "";
        public SortedDictionary<string,object> _data;

        /// <summary>
        /// Dữ liệu liên quan khi chọn mã
        /// </summary>
        [Category("H")]
        public SortedDictionary<string, object> Data
        {
            get
            {
                if (_data != null && _textData == Text)
                    return _data;
                else
                {
                    CheckExist(Text);
                }
                return _data;
            }
            private set
            {
                _data = value;
                SetBrotherFormData();
            }
        }

        /// <summary>
        /// Bật tắt tính năng lọc chỉ bắt đầu.
        /// </summary>
        [Category("H")]
        [Description("Lọc start trong sql (like 'abc%'")]
        [DefaultValue(false)]
        public bool FilterStart { get; set; }

        public void SetData(SortedDictionary<string, object> data)
        {
            Data = data;
        }
        [Category("H")]
        [Description("Các trường hiển thị khi lookup.")]
        public string DisplayFields { get; set; } 
        [Category("H")]
        [Description("Tên trường hiển thị tương ứng.")]
        public string DisplayHeaders { get; set; } 
        /// <summary>
        /// Tên các trường dữ liệu liên quan
        /// </summary>
        [Category("H")]
        [Description("Tên các trường liên quan khi chọn lookup value.")]
        public string BrotherFields { get; set; }
        /// <summary>
        /// Tên bảng dữ liệu lookup.
        /// </summary>
        [Category("H")]
        public string TableName { get; set; }

        /// <summary>
        /// Tên trường dữ liệu lookup.
        /// </summary>
        [Category("H")]
        public string FieldName { get; set; }
        [Category("H")]
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

        public event EventHandler AfterSetBrother;
        protected virtual void OnAfterSetBrother()
        {
            var handler = AfterSetBrother;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gán lại brothers với _data sẵn có.
        /// </summary>
        public void SetBrotherFormData()
        {
            if(BrotherFields!=null)
            HControlHelper.SetBrotherData(this, _data, BrotherFields);
            OnAfterSetBrother();
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

        [Description("Có hay không sử dụng phím F3 để dò tìm trên danh mục!")]
        [DefaultValue(true)]
        public bool F3
        {
            get { return _f3; }
            set { _f3 = value; }
        }
        
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

        private string _initFilter;
        public string InitFilter { get { return _initFilter; } }

        public void SetInitFilter(string filter)
        {
            _initFilter = filter;
        }

        #region ==== Event ====

        protected override void ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(TableName) && string.IsNullOrEmpty(FieldName))
            {
                base.ColorTextBox_KeyDown(sender, e);
            }
            else
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (_checkOnLeave && !ReadOnly && Visible)
                    {
                        if (Text.Trim() != "")
                        {
                            if (!string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName))
                            {
                                if (CheckExist(Text.Trim()))
                                {
                                    if (!Looking && gotfocustext != Text) CallDoLostFocusChange();
                                    else CallDoLostFocusNoChange();
                                    if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
                                }
                                else
                                {
                                    Lookup();
                                }

                            }
                        }
                        else if (_checkNotEmpty && !string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName))
                        {
                            Lookup();
                        }
                        else
                        {
                            base.ColorTextBox_KeyDown(this, e);
                        }
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
                    }
                }
                else if (F3 && !ReadOnly && e.KeyCode == Keys.F3 && !string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName))
                {
                    if (auto1 == null)
                        LoadAutoCompleteSource();
                    
                    {
                        Lookup();
                    }
                }
                else if (!ReadOnly && e.KeyCode == Keys.F2)
                {
                    if (F2)
                    {
                        //var formChangeVvar = new FormChangeVvar {textBox1 = {Text = ((LookupTextBox) sender).vVar}};
                        //if (formChangeVvar.ShowDialog() == DialogResult.OK)
                        //{
                        //    ((LookupTextBox) sender).vVar = formChangeVvar.textBox1.Text.Trim();
                        //}
                    }
                }
                else
                {
                    base.ColorTextBox_KeyDown(this, e);
                }
            }
        }

        /// <summary>
        /// Override hoàn toàn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (HControlHelper.DisableLookup)
            {
                HControlHelper.DisableLookup = false;
                return;
            }

            if (EnableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }

            if (_checkOnLeave && !ReadOnly && Visible)
            {
                if (textBox.Text.Trim() != "")
                {
                    if (!string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName))
                    {
                        if (CheckExist(textBox.Text.Trim()))
                        {
                            if(_upper) Text = Text.ToUpper();
                            if (!Looking && gotfocustext != Text) CallDoLostFocusChange();
                            else CallDoLostFocusNoChange();
                        }
                        else
                        {
                            Lookup();
                        }

                    }
                }
                else if (_checkNotEmpty && !string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName))
                {
                    Lookup();
                }
                else
                {
                    CheckExist(textBox.Text.Trim());
                    base.ColorTextBox_LostFocus(sender, e);
                }
            }
            else
            {
                base.ColorTextBox_LostFocus(sender, e);
            }
        }

        
        #endregion event

        protected AutoCompleteStringCollection auto1;

        public void LoadAutoCompleteSource()
        {
            if (!string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(FieldName) && auto1 == null)
            {
                try
                {
                    auto1 = new AutoCompleteStringCollection();
                    //if (!Large_YN())
                    {
                        if (!string.IsNullOrEmpty(FieldName))
                        {
                            var filter = InitFilter;
                            if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                            var where = "1=1 " + filter;
                            var strCommand = "Select top 1000 [" + FieldName + "] from [" + TableName + "] where " + where;

                            var tbl1 = HaControlSetting.DBA.ExecuteQuery(strCommand).Tables[0];

                            for (int i = 0; i < tbl1.Rows.Count; i++)
                            {
                                auto1.Add(tbl1.Rows[i][0].ToString().Trim());
                            }

                            HControlHelper.DisableLookup = true;
                            AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            AutoCompleteSource = AutoCompleteSource.CustomSource;
                            AutoCompleteCustomSource = auto1;
                            HControlHelper.DisableLookup = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    HControlHelper.ShowErrorMessage("LoadAutoCompleteSource " + TableName + " " + ex.Message);
                }
            }

        }

        ///// <summary>
        ///// Kiểm tra, lấy lại data, gán brothers
        ///// </summary>
        ///// <returns></returns>
        //public bool CheckExist()
        //{
        //    //return CheckExist(Text.Trim());
        //    return false;
        //}

        /// <summary>
        /// Kiểm tra giá trị có tồn tại trong csdl hay không.
        /// Lấy dữ liệu cho _data.
        /// Set Brother data
        /// </summary>
        /// <param name="text">Giá trị cần kiểm tra</param>
        /// <returns></returns>
        public bool CheckExist(string text)
        {
            try
            {
                _textData = text;
                if (!string.IsNullOrEmpty(FieldName))
                {
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and " + filter;
                    else filter = "";
                    string strCommand = "select * from [" + TableName + "] where [" + FieldName + "] = @text " + filter;

                    OleDbParameter[] plist =
                    {
                        new OleDbParameter("@text", text)
                    };
                    var tbl = HaControlSetting.DBA.ExecuteQuery(strCommand, plist).Tables[0];
                    
                    if (tbl != null && tbl.Rows.Count >= 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow.ToDataDictionary();
                        SetBrotherFormData();
                        return true;
                    }
                    else
                    {
                        _data = null;
                        SetBrotherFormData();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// Hiện form hiện danh sách (bảng dữ liệu) để chọn giá trị.
        /// </summary>
        public void Lookup()
        {
            try
            {
                LookupForm f = new LookupForm(TableName, FieldName, DisplayFields, DisplayHeaders, Text);
                f.AcceptDataEvent += data =>
                {
                    Looking = false;
                    var NAME = FieldName.ToUpper();
                    if (data != null && data.ContainsKey(NAME))
                    {
                        _data = data;
                        Text = _data[NAME].ToString().Trim();
                        SetBrotherFormData();
                        f.Dispose();
                    }
                };
                Looking = true;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("LookupTextBox Lookup " + ex.Message);
            }
        }

        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện LostFocus
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public override void ChangeText(string text)
        {
            var inText = Text;
            Text = text;
            CheckExist(Text.Trim());
            if (inText != Text) CallDoLostFocusChange();
            else CallDoLostFocusNoChange();
        }

        
    }
}
