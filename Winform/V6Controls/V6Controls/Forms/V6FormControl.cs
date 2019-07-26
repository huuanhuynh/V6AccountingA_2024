using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using V6AccountingBusiness;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    /// <summary>
    /// Dùng cho loại form chính thực hiện 1 công việc gì đó.
    /// Trong này đã có sẵn hàm DoHotKey thực hiện theo Tag.
    /// Ví dụ Hóa đơn control
    /// </summary>
    public partial class V6FormControl : V6Control
    {
        protected Image btnNhanImage = Properties.Resources.Apply;
        protected ImageList waitingImages { get { return _waitingImages; } }
        /// <summary>
        /// Waiting image index.
        /// </summary>
        protected int ii = 0;
        /// <summary>
        /// ID quản lý chứng từ đang xử lý.
        /// </summary>
        public string _sttRec { get; set; }
        protected bool _escape = false;
        /// <summary>
        /// Thông báo hoặc hiển thị trạng thái đang chạy.
        /// </summary>
        protected string _message = "";
        public bool _dataloading, _dataloaded;
        

        public V6FormControl()
        {
            InitializeComponent();
            Disposed += (sender, args) =>
            {
                V6ControlFormHelper.RemoveRunningList(_sttRec);
            };
        }

        /// <summary>
        /// Giống SetData. Dùng để override lấy dữ liệu theo khóa.
        /// </summary>
        /// <param name="keyData">Dữ liệu khóa</param>
        public virtual void SetDataKeys(SortedDictionary<string, object> keyData)
        {
            V6ControlFormHelper.SetFormDataDictionary( this, keyData );
        }
        /// <summary>
        /// Gán dữ liệu cho vài control trên form theo AccessibleName nếu có trong data.
        /// </summary>
        /// <param name="d"></param>
        public virtual void SetSomeData(SortedDictionary<string, object> d)
        {
            V6ControlFormHelper.SetSomeDataDictionary( this, d );
        }
        
        public virtual void SetStatus2Text()
        {
            //V6ControlFormHelper.SetStatusText2("");
        }

        /// <summary>
        /// Gán thông tin hướng dẫn khi rê chuột lên.
        /// </summary>
        /// <param name="control">Control trên form</param>
        /// <param name="tip">Dòng chữ hướng dẫn</param>
        protected void SetToolTip(Control control, string tip)
        {
            toolTipV6FormControl.SetToolTip(control, tip);
        }

        protected SortedList<int,int> _rowIndex = new SortedList<int, int>();
        protected SortedList<int, int> _cellIndex = new SortedList<int, int>();
        public void SaveSelectedCellLocation(DataGridView dataGridView1, int saveIndex = 0)
        {
            try
            {
                if (dataGridView1.CurrentCell != null)
                {
                    _rowIndex[saveIndex] = dataGridView1.CurrentCell.RowIndex;
                    _cellIndex[saveIndex] = dataGridView1.CurrentCell.ColumnIndex;
                }
                else
                {
                    _rowIndex[saveIndex] = 0;
                    _cellIndex[saveIndex] = 0;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SaveSelectedCellLocation " + ex.Message);
            }
        }

        public void LoadSelectedCellLocation(DataGridView dataGridView1, int saveIndex = 0)
        {
            V6ControlFormHelper.SetGridviewCurrentCellByIndex(dataGridView1, _rowIndex[saveIndex], _cellIndex[saveIndex], this);
        }

        private void V6FormControl_Load(object sender, EventArgs e)
        {
            if (V6Setting.IsDesignTime) return;
            SetStatus2Text();
            LoadLanguage();
        }

        private void V6UserControl_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible)
                SetStatus2Text();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F3 || keyData == Keys.F12)
                {
                    if (keyData == Keys.F3)
                    {
                        if (++f3count == 3)
                        {
                            var method_info = GetType().GetMethod("V6F3Execute");
                            if (method_info != null && method_info.DeclaringType == GetType() && //Kiem tra co method override
                                new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                            {
                                V6F3Execute();
                                f3count = 0;
                            }
                            else
                            {
                                f3count = 0;
                                V6F3ExecuteUndo();
                            }
                        }
                    }
                    else if (keyData == Keys.F12)
                    {
                        if (++f12count == 3)
                        {
                            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                            {
                                V6F12Execute();
                                f12count = 0;
                                return true;
                            }
                            else
                            {
                                f12count = 0;
                                //V6F12ExecuteUndo();
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    f3count = 0;
                    f12count = 0;
                }

                if (do_hot_key)
                {
                    do_hot_key = false;
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                if(DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected bool do_hot_key;
        /// <summary>
        /// Viết lệnh do_hot_key = true; trước.
        /// Các form kế thừa nếu có phím nóng quy định riêng cần override lại.
        /// Nếu không có hotkey định nghĩa thì gọi lại base.
        /// </summary>
        /// <param name="keyData"></param>
        public override void DoHotKey (Keys keyData)
        {
            try
            {
                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            return V6ControlFormHelper.DoKeyCommand(this, keyData);
        }

        protected int f3count;
        protected int f12count;
        /// <summary>
        /// Hàm thực hiện sau khi xác nhận mật khẩu V6 thành công.
        /// </summary>
        public virtual void V6F3Execute()
        {
            
        }
        public virtual void V6F12Execute()
        {
            try
            {
                string fileName = Path.Combine(V6Login.StartupPath, "V6HELP\\V6HELP.xml");
                XmlDocument xml = new XmlDocument();
                if (!File.Exists(fileName))
                {
                    var fs = new FileStream(fileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write("<V6HELP>\r\n</V6HELP>");
                    sw.Close();
                    fs.Close();
                }
                xml.Load(fileName);
                var documentElement = xml.DocumentElement;
                if (documentElement == null)
                {
                    this.ShowWarningMessage("Không có nội dung");
                    return;
                }

                var element = documentElement.GetElementsByTagName(V6ControlFormHelper.CurrentItemID)[0];
                if (element == null)
                {
                    element = xml.CreateElement(V6ControlFormHelper.CurrentItemID);
                    documentElement.AppendChild(element);
                }

                TextEditorForm editor = new TextEditorForm(element.InnerText);
                editor.Show(this);
                editor.FormClosing += (sender, e) =>
                {
                    if (editor.HaveChanged)
                    {
                        element.InnerText = editor.Content;
                        xml.Save(fileName);
                    }
                };
            }
            catch (Exception ex)
            {
                this.ShowErrorException("V6F12", ex);
            }
        }
        /// <summary>
        /// Hàm thực hiện sau khi xác nhận mật khẩu V6 sai.
        /// </summary>
        public virtual void V6F3ExecuteUndo()
        {
            
        }

        /// <summary>
        /// Gán dữ liệu mặc định lên form.
        /// </summary>
        /// <param name="loai">1ct 2danhmuc 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        protected void LoadDefaultData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            try
            {
                var data = GetDefaultData(V6Setting.Language, loai, mact, madm, itemId, adv);
                SetDefaultDataInfoToForm(data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDefaultData", ex);
            }
        }

        protected void SetDefaultDataInfoToForm(SortedDictionary<string, DefaultValueInfo> data)
        {
            try
            {
                SortedDictionary<string, object> someData = new SortedDictionary<string, object>();
                string log_key = "", errors = "";

                foreach (KeyValuePair<string, DefaultValueInfo> item in data)
                {
                    log_key = item.Key;
                    try
                    {
                        var valueInfo = item.Value;
                        switch (valueInfo.Type1)
                        {
                            case "0":
                                if (!string.IsNullOrEmpty(valueInfo.AName))
                                {
                                    someData[valueInfo.AName] = valueInfo.Value;
                                }
                                else if (!string.IsNullOrEmpty(valueInfo.CName))
                                {
                                    SetControlValue(GetControlByName(valueInfo.CName), valueInfo.Value);
                                }
                                break;
                            case "1":
                                if (!string.IsNullOrEmpty(valueInfo.Value))
                                {
                                    if (!string.IsNullOrEmpty(valueInfo.AName))
                                    {
                                        someData[valueInfo.AName] = valueInfo.Value;
                                    }
                                    else if (!string.IsNullOrEmpty(valueInfo.CName))
                                    {
                                        SetControlValue(GetControlByName(valueInfo.CName), valueInfo.Value);
                                    }
                                }
                                break;
                            case "2":
                                if (!string.IsNullOrEmpty(valueInfo.AName))
                                {
                                    var formValue = V6ControlFormHelper.GetFormValue(this, valueInfo.AName);
                                    if (formValue == null)
                                    {
                                        someData[valueInfo.AName] = valueInfo.Value;
                                    }
                                    else if (ObjectAndString.IsNumberType(formValue.GetType()))
                                    {
                                        var num = ObjectAndString.ObjectToDecimal(formValue);
                                        if (num == 0)
                                        {
                                            someData[valueInfo.AName] = valueInfo.Value;
                                        }
                                    }
                                    else if (string.IsNullOrEmpty(formValue.ToString().Trim()))
                                    {
                                        someData[valueInfo.AName] = valueInfo.Value;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(valueInfo.CName))
                                {
                                    var control = GetControlByName(valueInfo.CName);
                                    if (control != null)
                                    {
                                        var control_value = V6ControlFormHelper.GetControlValue(control);

                                        if (control_value == null)
                                        {
                                            SetControlValue(control, valueInfo.Value);
                                        }
                                        else if (ObjectAndString.IsNumberType(control_value.GetType()))
                                        {
                                            var num = ObjectAndString.ObjectToDecimal(control_value);
                                            if (num == 0)
                                            {
                                                SetControlValue(control, valueInfo.Value);
                                            }
                                        }
                                        else if (string.IsNullOrEmpty(control_value.ToString().Trim()))
                                        {
                                            SetControlValue(control, valueInfo.Value);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += string.Format("{0}: {1}\n", log_key, ex.Message);
                    }
                }

                if (errors.Length > 0)
                {
                    this.WriteToLog(GetType() + ".SetDefaultDataInfoToForm", errors);
                }
                SetSomeData(someData);
            }
            catch (Exception ex0)
            {
                this.WriteExLog(GetType() + ".SetDefaultDataInfoToForm", ex0);
            }
        }

        /// <summary>
        /// Gán Tag được lưu lên form.
        /// </summary>
        /// <param name="loai">1ct 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        protected void LoadTag(int loai, string mact, string madm, string itemId, string adv = "")
        {
            try
            {
                var data = GetTagData(loai, mact, madm, itemId, adv);
                V6ControlFormHelper.SetFormTagDictionary(this, data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadTag", ex);
            }
        }

        protected void LoadReadonly(int loai, string mact, string madm, string adv = "")
        {
            try
            {
                var data = GetReadonlyData(loai, mact, madm, adv);
                V6ControlFormHelper.SetFormTagDictionary(this, data);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadReadonly", ex);
            }
        }

        /// <summary>
        /// Tải dữ liệu và trả về DefaultData.
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="loai">1ct, 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        /// <returns></returns>
        private SortedDictionary<string, DefaultValueInfo> GetDefaultData(string lang, int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (defaultData != null && defaultData.Count > 0) return defaultData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, DefaultValueInfo>();
            foreach (DataRow row in alinitData.Rows)
            {   
                //Tuanmh 25/12/2017 - Bo sung theo kieu
                string kieu = row["kieu"].ToString().Trim();
                if (kieu == "") continue;
                
                var cell = row["Default" + lang]; if (cell == null) continue;
                var value = cell.ToString().Trim();
                var ANAME = row["NameVal"].ToString().Trim().ToUpper();
                var CNAME = row["NameTag"].ToString().Trim().ToUpper();
                DefaultValueInfo valueInfo = new DefaultValueInfo()
                {
                    AName = ANAME,
                    CName = CNAME,
                    Value = value,
                    Type1 = kieu,
                };
                result[string.IsNullOrEmpty(ANAME)?CNAME:ANAME] = valueInfo;
            }
            defaultData = result;
            return result;
        }

        protected DataTable alinitData;
        private SortedDictionary<string, DefaultValueInfo> defaultData;
        //private SortedDictionary<string, string> tagData;
        private SortedDictionary<string, string> readonlyData;
        private SortedDictionary<string, string> visibleData;

        /// <summary>
        /// Tải dữ liệu và trả về TagData.
        /// </summary>
        /// <param name="loai">1ct 4report</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        /// <returns></returns>
        private SortedDictionary<string, string> GetTagData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            //if (tagData != null && tagData.Count > 0) return tagData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Tag"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            //tagData = result;
            return result;
        }

        private SortedDictionary<string, string> GetReadonlyData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (readonlyData != null && readonlyData.Count > 0) return readonlyData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Readonly"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            readonlyData = result;
            return result;
        }

        private SortedDictionary<string, string> GetHideData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            if (visibleData != null && visibleData.Count > 0) return visibleData;
            if (alinitData == null || alinitData.Rows.Count == 0)
                alinitData = V6BusinessHelper.GetDefaultValueData(loai, mact, madm, itemId, adv);
            var result = new SortedDictionary<string, string>();
            foreach (DataRow row in alinitData.Rows)
            {
                var cell = row["Hide"]; if (cell == null) continue;
                var value = cell.ToString().Trim(); if (value == "") continue;

                var name = row["NameTag"].ToString().Trim().ToUpper();
                result[name] = value;
            }
            visibleData = result;
            return result;
        }

        /// <summary>
        /// <para>Gán event cho control, hiện status khi Enter.</para>
        /// <para>Hàm chỉ gọi 1 lần cho 1 control.</para>
        /// </summary>
        /// <param name="control"></param>
        protected void ApplyControlEnterStatus(Control control)
        {
            if (control is V6ColorTextBox)
            {
                control.Enter += delegate(object sender, EventArgs e)
                {
                    V6ColorTextBox ctb = (V6ColorTextBox)sender;
                    var s = string.Format("{0}:{1} ({2})", ctb.AccessibleName, ctb.GrayText, ctb.Text);
                    V6ControlFormHelper.SetStatusText(s);
                };
            }
            else if (control is V6ColorMaskedTextBox)
            {
                control.Enter += delegate(object sender, EventArgs e)
                {
                    V6ColorMaskedTextBox mtb = (V6ColorMaskedTextBox)sender;
                    var s = string.Format("{0}:{1} ({2})", mtb.AccessibleName, mtb.GrayText, mtb.Text);
                    V6ControlFormHelper.SetStatusText(s);
                };
            }
            else if (control is V6DateTimePicker)
            {
                control.Enter += delegate(object sender, EventArgs e)
                {
                    V6DateTimePicker mtb = (V6DateTimePicker)sender;
                    var s = string.Format("{0}:{1} ({2})", mtb.AccessibleName, mtb.TextTitle, mtb.Text);
                    V6ControlFormHelper.SetStatusText(s);
                };
            }
        }

        /// <summary>
        /// Dịch chuyển focus theo phím mũi tên. Nên cancel handled khi dịch chuyển thành công (return true).
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>true nếu dịch chuyển thành công.</returns>
        public bool Navigation(Keys keyData)
        {
            try
            {
                Control current = ActiveControl;
                if (current is DataGridView)
                {
                    var dataGridView1 = (DataGridView) current;
                    if (dataGridView1.CurrentRow != null &&
                        (keyData == Keys.Left || keyData == Keys.Right ||
                         (keyData == Keys.Up && dataGridView1.CurrentRow.Index > 0) ||
                         (keyData == Keys.Down && dataGridView1.CurrentRow.Index < dataGridView1.RowCount - 1)))
                        return false;
                }
                else if (current is TreeListView)
                {
                    var tree_list_view = (TreeListView)current;
                    if (tree_list_view.SelectedItems.Count>0 &&
                        (keyData == Keys.Left || keyData == Keys.Right ||
                         (keyData == Keys.Up && tree_list_view.SelectedItems[0].Index > 0) ||
                         (keyData == Keys.Down && tree_list_view.SelectedItems[0].Index < tree_list_view.ItemsCount - 1)))
                        return false;
                }

                Point get_point = current.Location;
                Control get_control = null;
                do
                {
                    if (current != null)
                    {
                        switch (keyData)
                        {
                            case Keys.Up:
                                get_point.X = current.Left + 10;
                                get_point.Y = current.Top - 20;
                                break;
                            case Keys.Down:
                                get_point.X = current.Left + 10;
                                get_point.Y = current.Bottom + 20;
                                break;
                            case Keys.Left:
                                get_point.X = current.Left - 20;
                                get_point.Y = current.Top + 10;
                                break;
                            case Keys.Right:
                                get_point.X = current.Right + 20;
                                get_point.Y = current.Top + 10;
                                break;
                        }
                    }
                    else
                    {
                        switch (keyData)
                        {
                            case Keys.Up:
                                get_point.Y -= 10;
                                break;
                            case Keys.Down:
                                get_point.Y += 10;
                                break;
                            case Keys.Left:
                                get_point.X -= 10;
                                break;
                            case Keys.Right:
                                get_point.X += 10;
                                break;
                        }
                    }
                    get_control = GetChildAtPoint(get_point);
                    //if (get_control != null)
                        current = get_control;

                } while ((get_control != null && (!get_control.Visible || !get_control.Enabled))
                    || (get_control == null && this.Bounds.Contains(get_point)));

                if (get_control != null)
                {
                    get_control.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Navigation", ex);
            }
            return false;
        }

    }
}
