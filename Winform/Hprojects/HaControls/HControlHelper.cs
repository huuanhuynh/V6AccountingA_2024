using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using H_Controls.Controls;
using H_DatabaseAccess;
using H_Utility.Converter;

namespace H_Controls
{
    public static class HControlHelper
    {
        /// <summary>
        /// ,cancel,disable,
        /// </summary>
        public static object TagHelp { get; set; }

        #region ==== RUNNING LIST ====
        public static SortedDictionary<string, string> RunningList = new SortedDictionary<string, string>();
        public static void AddRunningList(string key, string description)
        {
            RunningList[key] = description;
        }

        public static void RemoveRunningList(string key)
        {
            if (RunningList.ContainsKey(key))
            {
                RunningList.Remove(key);
            }
        }
        #endregion running list

        #region ==== SHOW HIDE MESSAGE ====

        public static Label MessageLable;
        private static Timer _mainMessageTimer;
        private static int _mainTime = -1;
        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên phải chương trình.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMainMessage(string message)
        {
            if (_mainMessageTimer != null && _mainMessageTimer.Enabled)
            {
                _mainMessageTimer.Stop();
                //MessageLable.Top 
            }

            MessageLable.Text = message;
            _mainMessageTimer = new Timer { Interval = 200 };
            _mainMessageTimer.Tick += _mainMessageTimer_Tick;
            _mainTime = -1;
            _mainMessageTimer.Start();
        }

        static void _mainMessageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _mainTime++;
                if (_mainTime < 10)//Hiện ra
                {
                    MessageLable.Top -= (MessageLable.Top) / 3;

                    if (MessageLable.Top == -1) MessageLable.Top = 0;
                    if (MessageLable.Top == -2) MessageLable.Top = -1;
                    return;
                }
                if (_mainTime < 20)//Dừng lại
                {
                    return;
                }
                if (_mainTime < 30)//Ẩn đi
                {
                    MessageLable.Top -= (MessageLable.Bottom) / 3;
                    if (MessageLable.Bottom == 1) MessageLable.Top = -MessageLable.Height;
                    if (MessageLable.Bottom == 2) MessageLable.Top = -MessageLable.Height + 1;
                    return;
                }
                _mainMessageTimer.Stop();
            }
            catch// (Exception)
            {
                // ignored
            }
        }

        #endregion show hide message

        #region ==== SetStatusText ====
        public static void HaveStatusControl_MouseEnter(object sender, EventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseHover(object sender, EventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseMove(object sender, MouseEventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseLeave(object sender, EventArgs e)
        {
            SetStatusText("");
        }

        public static Control StatusTextViewControl { get; set; }
        public static Control StatusTextViewControl2 { get; set; }
        private static int _timeCount, _timeCount2;
        private static System.Timers.Timer _timerHideMessage, _timerHideMessage2;

        public static bool DisableLookup { get; set; }

        public static void SetStatusText(string text)
        {
            if(StatusTextViewControl != null)
            StatusTextViewControl.Text = text ?? "";
            
            _timeCount = 0;
            _timerHideMessage = new System.Timers.Timer();
            _timerHideMessage.Interval = 200;
            _timerHideMessage.Elapsed += delegate
            {
                _timeCount++;
                if (_timeCount >= 25)
                {
                    try
                    {
                        Control.CheckForIllegalCrossThreadCalls = false;
                        if (StatusTextViewControl.Text.Length > 0)
                        {
                            StatusTextViewControl.Text = StatusTextViewControl.Text.Substring(1);
                        }
                        else
                        {
                            _timerHideMessage.Stop();
                        }
                    }
                    catch (Exception)
                    {
                        _timerHideMessage.Stop();
                    }
                    
                }
            };
            _timerHideMessage.Start();
        }
        public static void SetStatusText2(string text)
        {
            if (StatusTextViewControl2 != null)
                StatusTextViewControl2.Text = text ?? "";

            Control.CheckForIllegalCrossThreadCalls = false;
            _timeCount2 = 0;
            _timerHideMessage2 = new System.Timers.Timer();
            _timerHideMessage2.Interval = 200;
            _timerHideMessage2.Elapsed += delegate
            {
                _timeCount2++;
                if (_timeCount2 >= 25)
                {
                    if (StatusTextViewControl2.Text.Length > 0)
                    {
                        StatusTextViewControl2.Text = StatusTextViewControl2.Text.Substring(1);
                    }
                    else
                    {
                        _timerHideMessage2.Stop();
                    }
                }
            };
            _timerHideMessage2.Start();
        }
        #endregion
        
        #region ==== GET... ====
        /// <summary>
        /// Lấy 1 giá trị thông qua AccessibleName
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetFormValue(Control control, string name)
        {
            try
            {
                if (control == null) return null;
                object result = null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && control.AccessibleName.ToUpper() == name.ToUpper())
                {
                    //var box = control as IndexComboBox;
                    //if (box != null)
                    //{
                    //    result = box.SelectedIndex;
                    //}
                    if (control is ComboBox)
                    {
                        result = ((ComboBox)control).SelectedValue;
                    }
                    else if (control is DateTimePicker)
                    {
                        result = ((DateTimePicker)control).Value;
                    }
                    else if (control is NumberTextBox)
                    {
                        result = ((NumberTextBox)control).Value;
                    }
                    else if (control is CheckTextBox)
                    {
                        result = ((CheckTextBox)control).StringValue;
                    }
                    else if (control is CheckBox)
                    {
                        result = ((CheckBox)control).Checked;
                    }
                    else if (control is RadioButton)
                    {
                        result = ((RadioButton)control).Checked;
                    }
                    else
                    {
                        result = control.Text;
                    }
                }


                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        object o = GetFormValue(c, name);
                        if (o != null) return o;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormValue error!\n" + ex.Message);
            }
        }

        public static Control GetFormControl(Control control, string name)
        {
            try
            {
                if (control == null) return null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && control.AccessibleName.ToUpper() == name.ToUpper())
                {
                    return control;
                }


                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        Control o = GetFormControl(c, name);
                        if (o != null) return o;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormControl error!\n" + ex.Message);
            }
        }

        static List<string> _debugList = new List<string>();
        /// <summary>
        /// Lấy dữ liệu qua AccessibleName
        /// Khong biet co nen dung AccessibleDescription de danh dau data khong.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>Dic with UPPER keys</returns>
        public static SortedDictionary<string, object> GetFormDataDictionary(Control container)
        {
            string errors = "";
            _debugList = new List<string>();
            var d = GetFormDataDictionaryRecursive(container, ref errors);
            if (errors.Length > 0)
                throw new Exception("GetFormDataDictionary error:" + errors);
            return d;
        }

        private static SortedDictionary<string, object> GetFormDataDictionaryRecursive(Control container, ref string errors)
        {
            var d = new SortedDictionary<string, object>();

            var tagString = string.Format(",{0},", (container.Tag ?? ""));

            var canceldata = tagString != "" && tagString.Contains(",canceldata,");
            if (canceldata) goto CANCELALL;

            var cName = "";
            try
            {
                foreach (Control control in container.Controls)
                {
                    if (!string.IsNullOrEmpty(control.AccessibleName))
                    {
                        _debugList.Add(control.AccessibleName);
                        cName = control.AccessibleName;
                        if (control is DateTimeColor)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((DateTimeColor)control).Value);
                        }
                        else if (control is IndexComboBox)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((IndexComboBox)control).SelectedIndex);
                        }
                        else if (control is ComboBox)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((ComboBox)control).SelectedValue);
                        }
                        else if (control is NumberTextBox)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((NumberTextBox)control).Value);
                        }
                        else if (control is CheckTextBox)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((CheckTextBox)control).StringValue);
                        }
                        else if (control is CheckBox)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((CheckBox)control).Checked ? 1 : 0);
                        }
                        else if (control is RadioButton)
                        {
                            if (((RadioButton)control).Checked)
                                d.Add(control.AccessibleName.ToUpper(), control.Text);
                        }
                        else if (control is DateTimePicker)
                        {
                            d.Add(control.AccessibleName.ToUpper(), ((DateTimePicker)control).Value);
                        }
                        else
                        {
                            d.Add(control.AccessibleName.ToUpper(), control.Text);
                        }

                    }

                    if (control.Controls.Count > 0)
                    {
                        SortedDictionary<string, object> t = GetFormDataDictionaryRecursive(control, ref errors);
                        foreach (KeyValuePair<string, object> keyValuePair in t)
                        {
                            cName = keyValuePair.Key;
                            d.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errors += "\n" + cName + ": " + ex.Message;
            }
        CANCELALL:
            return d;
        }

        /// <summary>
        /// Lấy 1 đối tượng dữ liệu (model) từ form dựa theo model và AccessibleName trên form.
        /// </summary>
        /// <typeparam name="A">Kiểu model cần lấy</typeparam>
        /// <param name="container">Form chứa dữ liệu (this)</param>
        /// <returns></returns>
        public static A GetFormObjectValues<A>(Control container) where A : new()
        {
            var a = new A();
            var valDic = GetFormDataDictionary(container);
            foreach (PropertyInfo propertyInfo in a.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    //object value = GetFormValue(container, propertyInfo.Name);
                    object o = "";
                    if (valDic.ContainsKey(propertyInfo.Name.ToUpper()))
                        o = valDic[propertyInfo.Name.ToUpper()];
                    var value = PrimitiveTypes.ObjectTo(propertyInfo.PropertyType, o);
                    propertyInfo.SetValue(a, value, null);
                }
            }

            return a;
        }

        #endregion

        #region ==== SET... ====
        /// <summary>
        /// Gán value cho vài control anh em trên form có AccessibleName nằm trong list
        /// </summary>
        /// <param name="control"></param>
        /// <param name="row"></param>
        /// <param name="fields"></param>
        public static void SetBrotherData(Control control, SortedDictionary<string, object> row, string fields)
        {
            fields = "," + fields.ToLower() + ",";
            try
            {
                if (row == null) return;
                Control parent = control.Parent;
                if (parent != null)
                {
                    foreach (Control c in parent.Controls)
                    {
                        string NAME = c.AccessibleName==null?"":c.AccessibleName.ToUpper();
                        if (!string.IsNullOrEmpty(NAME)
                            && row.ContainsKey(NAME) && fields.Contains("," + NAME.ToLower() + ","))
                        {
                            if (c is DateTimeColor)
                            {
                                ((DateTimeColor)c).Value = PrimitiveTypes.ObjectToDate(row[NAME]);
                            }
                            else if (c is IndexComboBox)
                            {
                                ((IndexComboBox)control).SelectedIndex = PrimitiveTypes.ObjectToInt(row[NAME]);
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                var value = PrimitiveTypes.ObjectToString(row[NAME]);
                                if (com.Items.Contains(value))
                                {
                                    com.SelectedText = value;
                                }
                                else
                                {
                                    com.Items.Add(value);
                                    com.SelectedText = value;
                                }
                            }
                            else if (c is DateTimePicker)
                            {
                                var object_to_date = PrimitiveTypes.ObjectToDate(row[NAME]);
                                if (object_to_date != null)
                                    ((DateTimePicker)c).Value = (DateTime)object_to_date;
                            }
                            else if (c is NumberTextBox)
                            {
                                ((NumberTextBox)c).Value = PrimitiveTypes.ObjectToDecimal(row[NAME]);
                            }
                            else if (c is CheckTextBox)
                            {
                                ((CheckTextBox)c).SetStringValue(PrimitiveTypes.ObjectToString(row[NAME]));
                            }
                            else if (c is CheckBox)
                            {
                                string value = row[NAME].ToString().Trim();
                                if (value == "1" || value.ToLower() == "true")
                                {
                                    ((CheckBox)c).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                            }
                            else if (c is RadioButton)
                            {
                                if (row[NAME].ToString().Trim() == c.Text)
                                {
                                    ((RadioButton)c).Checked = true;
                                }
                            }
                            else
                            {
                                c.Text = row[NAME].ToString().Trim();
                            }
                        }
                        else if (!string.IsNullOrEmpty(c.AccessibleName) && fields.Contains("," + c.AccessibleName.ToLower() + ","))
                        {
                            //Gán rỗng hoặc mặc định
                            if (c is IndexComboBox)
                            {
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else
                                if (c is DateTimePicker)
                                {
                                    ((DateTimePicker)c).Value = DateTime.Now;
                                }
                                else if (c is NumberTextBox)
                                {
                                    ((NumberTextBox)c).Value = 0;
                                }
                                else if (c is CheckBox)
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                                else if (c is RadioButton)
                                {
                                    ((RadioButton)c).Checked = false;
                                }
                                else
                                {
                                    c.Text = "";
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetBrotherData error!\n" + (control.AccessibleName ?? "") + ": " + ex.Message);
            }
        }

        /// <summary>
        /// Set các control nhập liệu thành readonly (hoặc không)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="readonli"></param>
        public static void SetFormControlsReadOnly(Control control, bool readonli)
        {
            var tagString = string.Format(",{0},", (control.Tag ?? ""));

            var cancelall = tagString != "" && tagString.Contains(",cancelall,");
            if (cancelall) goto CANCELALL;
            var cancel = tagString != "" && tagString.Contains(",cancel,");
            if (cancel) goto CANCEL;

            var readonl2 = tagString.Contains(",readonly,");
            var disable = tagString.Contains(",disable,");
            var enable = tagString.Contains(",enable,");

            if (control is TextBox)
            {
                var txt = control as TextBox;
                txt.ReadOnly = readonli || readonl2;
                if (readonl2) txt.TabStop = false;
                if (disable) txt.Enabled = false;
                if (enable) txt.Enabled = true;
            }
            else
                if (control is DateTimePicker
                    || control is CheckBox
                    || control is RadioButton
                    || control is ComboBox
                    
                    )
                {
                    control.Enabled = !(readonli || readonl2);
                    if (readonl2) control.TabStop = false;
                    if (disable) control.Enabled = false;
                    if (enable) control.Enabled = true;
                }
                else
                    if (control is DataGridView)
                    {
                        var dgv = (DataGridView)control;
                        dgv.ReadOnly = readonli || readonl2;

                        if (disable) control.Enabled = false;
                        if (enable) control.Enabled = true;
                    }
        CANCEL:
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    SetFormControlsReadOnly(c, readonli);
                }
            }
        CANCELALL: ;
        }

        /// <summary>
        /// Gán value cho tất cả control trên form có AccessibleName
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data">data chỉ gồm 1 dòng dữ liệu</param>
        public static void SetFormDataRow(Control control, DataRow data)
        {
            try
            {
                _errors = "";
                SetFormDataRowRecursive(control, data);
            }
            catch (Exception ex)
            {
                _errors += ("\nSetFormDataRow: " + (control.AccessibleName ?? "")
                    + " " + ex.Message);
            }
            if (_errors != "")
                throw new Exception("SetFormDataRowRecursive: " + _errors);
        }

        private static string _errors = "";
        public static void SetFormDataRowRecursive(Control control, DataRow data)
        {
            try
            {
                //if (data == null) return;

                var tagString = string.Format(",{0},", (control.Tag ?? ""));
                var canceldata = tagString != "" && tagString.Contains(",canceldata,");
                if (canceldata) goto CANCELALL;

                string NAME = control.AccessibleName;
                if (data != null && !string.IsNullOrEmpty(NAME) && data.Table.Columns.Contains(NAME))
                {
                    NAME = NAME.ToUpper();
                    if (control is DateTimeColor)
                    {
                        ((DateTimeColor)control).Value = PrimitiveTypes.ObjectToDate(data[NAME]);
                    }
                    else if (control is IndexComboBox)
                    {
                        ((IndexComboBox)control).SelectedIndex = PrimitiveTypes.ObjectToInt(data[NAME]);
                    }
                    else if (control is ComboBox)
                    {
                        try
                        {
                            //!!! can mo rong nhieu loai combobox
                            var com = control as ComboBox;
                            var VALUE = PrimitiveTypes.ObjectToString(data[NAME]).Trim();
                            if (com.Items.Count > 0 && VALUE != "")
                            {
                                com.SelectedValue = VALUE;
                            }
                            else
                            {
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else if (control is DateTimePicker)
                    {
                        var object_to_date = PrimitiveTypes.ObjectToDate(data[NAME]);
                        if (object_to_date != null)
                            ((DateTimePicker)control).Value = (DateTime)object_to_date;
                    }
                    else if (control is NumberTextBox)
                    {
                        ((NumberTextBox)control).Value = PrimitiveTypes
                            .ObjectToDecimal(data[NAME]);
                    }
                    else if (control is CheckTextBox)
                    {
                        ((CheckTextBox)control).SetStringValue(PrimitiveTypes.ObjectToString(data[NAME]));
                    }
                    else if (control is CheckBox)
                    {
                        string value = data[NAME].ToString().Trim();
                        if (value == "1" || value.ToLower() == "true")
                        {
                            ((CheckBox)control).Checked = true;
                        }
                        else
                        {
                            ((CheckBox)control).Checked = false;
                        }
                    }
                    else if (control is RadioButton)
                    {
                        if (data[control.AccessibleName].ToString().Trim() == control.Text)
                        {
                            ((RadioButton)control).Checked = true;
                        }
                    }
                    else
                    {
                        control.Text = PrimitiveTypes.ObjectToString(data[control.AccessibleName.ToUpper()]).Trim();
                    }
                }
                else if (!string.IsNullOrEmpty(NAME))
                {
                    //Gán rỗng hoặc mặc định
                    if (control is ComboBox)
                    {
                        try
                        {
                            //!!! can mo rong nhieu loai combobox
                            var com = control as ComboBox;
                            if (com.Items.Count > 0)
                                com.SelectedIndex = -1;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else if (control is DateTimePicker)
                    {
                        ((DateTimePicker)control).Value = DateTime.Now;
                    }
                    else if (control is NumberTextBox)
                    {
                        ((NumberTextBox)control).Value = 0;
                    }
                    else if (control is CheckBox)
                    {
                        ((CheckBox)control).Checked = false;
                    }
                    else if (control is RadioButton)
                    {
                        ((RadioButton)control).Checked = false;
                    }
                    else
                    {
                        control.Text = "";
                    }
                }



                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormDataRow(c, data);
                    }
                }
            CANCELALL:
                ;
            }
            catch (Exception ex)
            {
                _errors += ("SetFormData error at: " + (control.AccessibleName ?? "")
                    + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// Gán value cho tất cả control trên form có AccessibleName
        /// Không có data thì sẽ set rỗng.
        /// </summary>
        /// <param name="control">Form cần điền dữ liệu, thường dùng từ khóa this</param>
        /// <param name="data">Lưu ý. nên dùng key UPPER</param>
        public static void SetFormDataDic(Control control, SortedDictionary<string, object> data)
        {
            try
            {
                _errors = "";
                SetFormDataDicRecursive(control, data);
            }
            catch (Exception ex)
            {
                _errors += ("\nSetFormDataDic: " + control.AccessibleName + ": " + ex.Message);
            }
            if (_errors != "")
                throw new Exception("SetFormDataDicRecursive: " + _errors);
        }
        private static void SetFormDataDicRecursive(Control control, SortedDictionary<string, object> data)
        {
            try
            {
                var tagString = string.Format(",{0},", (control.Tag ?? ""));
                var canceldata = tagString != "" && tagString.Contains(",canceldata,");
                if (canceldata) goto CANCELALL;

                var NAME = control.AccessibleName;
                if (data != null && !string.IsNullOrEmpty(NAME) && data.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();

                    #region === Gán giá trị ===

                    if (control is DateTimeColor)
                    {
                        ((DateTimeColor)control).Value = PrimitiveTypes.ObjectToDate(data[NAME]);
                    }
                    else if (control is IndexComboBox)
                    {
                        ((IndexComboBox)control).SelectedIndex = PrimitiveTypes.ObjectToInt(data[NAME]);
                    }
                    else if (control is ComboBox)
                    {
                        try
                        {
                            var com = control as ComboBox;
                            var VALUE = PrimitiveTypes.ObjectToString(data[NAME]).Trim();
                            if (com.Items.Count > 0 && VALUE != "")
                            {
                                com.SelectedValue = VALUE;
                            }
                            else
                            {
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else if (control is DateTimePicker)
                    {
                        var object_to_date = PrimitiveTypes.ObjectToDate(data[NAME]);
                        if (object_to_date != null)
                            ((DateTimePicker)control).Value = (DateTime)object_to_date;
                    }
                    else if (control is NumberTextBox)
                    {
                        ((NumberTextBox)control).Value = PrimitiveTypes.ObjectToDecimal(data[NAME]);
                    }
                    else if (control is CheckTextBox)
                    {
                        ((CheckTextBox)control).SetStringValue(PrimitiveTypes.ObjectToString(data[NAME]));
                    }
                    else if (control is CheckBox)
                    {
                        string value = data[NAME].ToString().Trim();
                        if (value == "1" || value.ToLower() == "true")
                        {
                            ((CheckBox)control).Checked = true;
                        }
                        else
                        {
                            ((CheckBox)control).Checked = false;
                        }
                    }
                    else if (control is RadioButton)
                    {
                        if (data[NAME].ToString().Trim() == control.Text)
                        {
                            ((RadioButton)control).Checked = true;
                        }
                    }
                    else
                    {
                        control.Text = PrimitiveTypes
                            .ObjectToString(data[NAME]).Trim();
                    }
                    #endregion gán giá trị
                }
                else if (!string.IsNullOrEmpty(NAME))
                {
                    //NAME = NAME.ToUpper();
                    #region === Gán rỗng hoặc mặc định ===

                    if (control is ComboBox)
                    {
                        try
                        {
                            //!!! can mo rong nhieu loai combobox
                            var com = control as ComboBox;
                            if (com.Items.Count > 0)
                                com.SelectedIndex = -1;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else if (control is DateTimePicker)
                    {
                        ((DateTimePicker)control).Value = DateTime.Now;
                    }
                    else if (control is NumberTextBox)
                    {
                        ((NumberTextBox)control).Value = 0;
                    }
                    else if (control is CheckBox)
                    {
                        ((CheckBox)control).Checked = false;
                    }
                    else if (control is RadioButton)
                    {
                        ((RadioButton)control).Checked = false;
                    }
                    else
                    {
                        control.Text = "";
                    }
                    #endregion gán rỗng
                }

                //CANCEL:
                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormDataDic(c, data);
                    }
                }
            CANCELALL: ;
            }
            catch (Exception ex)
            {
                throw new Exception("SetFormData error!\n" + control.AccessibleName + ": " + ex.Message);
            }
        }

        private static Control FindBrotherControlByAccessibleName(string accName, Control brother)
        {
            if (brother.Parent != null)
            {
                accName = accName.ToUpper();
                foreach (Control c in brother.Parent.Controls)
                {
                    if (c.AccessibleName != null && c.AccessibleName.ToUpper() == accName) return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Dùng cho các trường tự định nghĩa
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        /// <param name="lang"></param>
        public static void SetFormInfo(Control control, DataTable data, string lang)
        {
            //Visible=1,Caption=M· §N 1,English=User define code 1                                                                            
            try
            {
                if (data == null || data.Rows.Count == 0) return;
                DataRow row = data.Rows[0];

                if (!string.IsNullOrEmpty(control.AccessibleDescription))
                {
                    var descriptions = control.AccessibleDescription.Split(',');
                    if (descriptions.Length == 2)
                    {
                        var labelField = descriptions[0];
                        var dataField = descriptions[1];
                        if (data.Columns.Contains(labelField))
                        {
                            string s = row[labelField].ToString().Trim();
                            string[] sss = s.Split(new[] { ',' }, 3);
                            //check visible
                            bool visible = true;
                            string[] ss = sss[0].Split('=');
                            if (ss.Length == 2 && ss[1] != "1") visible = false;

                            control.Visible = visible;
                            if (!string.IsNullOrEmpty(dataField))
                            {
                                Control dataControl = FindBrotherControlByAccessibleName(dataField, control);
                                if (dataControl != null)
                                {
                                    dataControl.Visible = visible;
                                    control.Visible = visible;
                                }
                            }

                            if (visible) //Gán text
                            {
                                if (lang.ToUpper() == "E")
                                {
                                    ss = sss[2].Split('=');
                                }
                                else
                                {
                                    ss = sss[1].Split('=');
                                }
                                control.Text = ss[1];
                            }
                        }
                    }
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormInfo(c, data, lang);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("SetFormInfo error!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Chỉ mới có MaxLength cho TextBox
        /// </summary>
        /// <param name="control"></param>
        /// <param name="structTable"></param>
        public static void SetFormStruct(Control control, TableStruct structTable)
        {
            try
            {
                var tagString = string.Format(",{0},", (control.Tag ?? ""));

                var cancelall = tagString != "" && tagString.Contains(",cancelall,");
                if (cancelall) goto CANCELALL;
                var cancel = tagString != "" && tagString.Contains(",cancel,");
                if (cancel) goto CANCEL;

                var NAME = control.AccessibleName;

                if (control is TextBox && !string.IsNullOrEmpty(NAME)
                        && structTable.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();
                    var num = control as NumberTextBox;
                    if (num != null)
                    {
                        if (string.IsNullOrEmpty(num.LimitCharacters))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(num.NumberFormatName))
                                {
                                    //num.DecimalPlaces
                                    //    = Convert.ToInt32();
                                }
                                else
                                {
                                    num.MaxNumLength = structTable[NAME].MaxNumLength;
                                    num.MaxLength = num.MaxNumLength;
                                    num.MaxNumDecimal = structTable[NAME].MaxNumDecimal;
                                    num.DecimalPlaces = num.MaxNumDecimal;
                                }
                            }
                            catch
                            {
                                // ignored
                            }

                        }
                        else
                        {
                            // limit textbox
                        }
                    }
                    else
                    {

                        int ml = structTable[control.AccessibleName.ToUpper()].MaxLength;
                        if (ml > 0)
                            ((TextBox)control).MaxLength = ml < 0 ? 32767 : ml;


                    }
                }
                else if (control is TextBox)// && !string.IsNullOrEmpty(control.AccessibleName))
                {
                    //Không chứa trong cấu trúc bảng, không cần thiết accname
                    var num = control as NumberTextBox;
                    if (num != null)
                    {
                        if (string.IsNullOrEmpty(num.LimitCharacters))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(num.NumberFormatName))
                                {
                                    //num.DecimalPlaces =
                                }
                                else
                                {

                                }
                            }
                            catch
                            {
                                // ignored
                            }

                        }
                        else
                        {
                            // limit textbox
                        }
                    }
                    else// textbox thuong
                    {


                    }
                }
            CANCEL:
                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormStruct(c, structTable);
                    }
                }
            CANCELALL:
                ;
            }
            catch (Exception ex)
            {
                throw new Exception("SetFormStruct error!\n" + ex.Message);
            }
        }
        #endregion set

        #region ==== Set form text ====
        
        private static void SetFormText(Control control, SortedDictionary<string, string> textDic)
        {
            if (!string.IsNullOrEmpty(control.AccessibleDescription)
                && textDic.ContainsKey(control.AccessibleDescription.ToUpper()))
            {
                control.Text = textDic[control.AccessibleDescription.ToUpper()].Trim();
            }
            var menuControl = control as MenuControl;
            if (menuControl != null)
            {
                foreach (MenuButton b in menuControl.Buttons)
                {
                    if (!string.IsNullOrEmpty(b.AccessibleDescription)
                       && textDic.ContainsKey(b.AccessibleDescription.ToUpper()))
                    {
                        b.Text = textDic[b.AccessibleDescription.ToUpper()].Trim();
                    }
                }
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    SetFormText(c, textDic);
                }
            }
        }


        /// <summary>
        /// Lấy lên danh sách tất cả AccessibleDescription trong control (và con của nó)
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private static List<string> GetFormAccessibleDescriptions(Control control)
        {
            var result = new List<string>();
            if (!string.IsNullOrEmpty(control.AccessibleDescription))
                result.Add(control.AccessibleDescription);
            var menuControl = control as MenuControl;
            if (menuControl != null)
            {
                foreach (MenuButton b in menuControl.Buttons)
                {
                    if (!string.IsNullOrEmpty(b.AccessibleDescription))
                    {
                        result.Add(b.AccessibleDescription);
                    }
                }
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result.AddRange(GetFormAccessibleDescriptions(c));
                }
            }
            return result;
        }

        #endregion



        #region ==== Show...Message() ====

        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public static void ShowErrorMessage(string message, string lang = "V", string title = "")
        {
            ShowErrorMessage(null, message, lang, title);
        }
        public static void ShowErrorMessage(IWin32Window owner, string message, string lang = "V", string title = "")
        {
            if (title == "") title = lang == "V" ? "Lỗi!" : "Error!";
            MessageBox.Show(owner, message, title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Yes/No
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirmMessage(string message, string lang = "V", string title = null)
        {
            return MessageBox.Show(message, title ?? (lang == "V" ? "Xác nhận!" : "Confirm!"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        /// <summary>
        /// Yes/No/Cancel
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirmCancelMessage(string message, string lang = "V", string title = null)
        {
            return MessageBox.Show(message, title ?? (lang == "V" ? "Xác nhận!" : "Confirm!"),
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
        }

        public static void ShowWarningMessage(string message, string lang = "V")
        {
            MessageBox.Show(message, lang == "V" ? "Cảnh báo!" : "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void NoRightWarning()
        {
            ShowWarningMessage("NoRight");
        }

        public static void ShowInfoMessage(string message, string lang = "V")
        {
            MessageBox.Show(message, lang == "V" ? "Thông báo" : "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion showmessage


        public static void CallExe(string exe)
        {
            if (string.IsNullOrEmpty(exe)) return;
            if (File.Exists(exe))
            {
                if (!string.IsNullOrEmpty(exe) && exe.ToUpper().EndsWith(".EXE"))
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = exe;
                    
                    Process.Start(psi);
                }
            }
            else
            {
                ShowWarningMessage("Không tồn tại tập tin:\n" + exe);
            }
        }

        public static bool ClickByTag(Control control, string keyString)
        {
            if (control.Tag != null && control.Tag.ToString() == keyString)
            {
                if (control is Button) ((Button)control).PerformClick();
                if (control is LabelH) ((LabelH)control).PerformClick();
                return true;
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    if (ClickByTag(c, keyString)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Click vào control Button hoặc HLable dựa vào Tag vd: Tag="F3"
        /// </summary>
        /// <param name="container"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public static bool DoKeyCommand(Control container, Keys keyData)
        {
            try
            {
                string keyString = keyData.ToString();
                SetStatusText(keyString);//Test !!!!!!!!!!!
                return ClickByTag(container, keyString);
            }
            catch
            {
                return false;
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static Type TypeFromSqlType(string sqlType)
        {
            switch (sqlType)
            {
                case "date":
                case "smalldatetime":
                case "datetime":
                    return typeof(DateTime);
                case "bigint":
                    return typeof(Int64);
                case "numeric":
                    return typeof(decimal);
                case "bit":
                    return typeof(bool);
                case "smallint":
                    return typeof(Int16);
                case "decimal":
                    return typeof(decimal);
                case "smallmoney":
                    return typeof(decimal);
                case "int":
                    return typeof(int);
                case "tinyint":
                    return typeof(byte);
                case "money":
                    return typeof(decimal);
                default:
                    return typeof(string);
            }
        }

        /// <summary>
        /// Sắp xếp thứ tự cột cho DataGridView theo danh sách gửi vào
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="i">Vị trí bắt đầu sắp xếp, các cột trước đó để nguyên</param>
        /// <param name="orderList">Danh sách các cột đã sắp xếp</param>
        public static void ReorderDataGridViewColumns(ColorGridView dgv, List<string> orderList, int i = -1)
        {
            dgv.AutoGenerateColumns = false;
            try
            {
                var start = i;
                if (start == -1)
                    start = dgv.Columns.Cast<DataGridViewColumn>().Count(column => column.Frozen);

                foreach (string field in orderList)
                {
                    var dataGridViewColumn = dgv.Columns[field];
                    if (dataGridViewColumn != null && !dataGridViewColumn.Frozen)
                    {
                        dataGridViewColumn.DisplayIndex = start;
                        start++;
                    }
                }
            }
            catch// (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Chỉ gọi 1 lần, gọi nhiều bị lỗi.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="showFields"></param>
        /// <param name="formatStrings"></param>
        /// <param name="headerString"></param>
        public static void FormatGridViewAndHeader(DataGridView dgv, string showFields, string formatStrings, string headerString)
        {
            if (string.IsNullOrEmpty(showFields)) return;
            var fieldList = showFields.Split(showFields.Contains(";") ? ';' : ',');
            var formatList = new string[] {};
            if(!string.IsNullOrEmpty(formatStrings))
                formatList = formatStrings.Split(formatStrings.Contains(";") ? ';' : ',');
            var headerList = new string[] { };
            if (!string.IsNullOrEmpty(headerString))
                headerList = headerString.Split(headerString.Contains(";") ? ';' : ',');

            FormatGridViewColumnsShowOrder(dgv, fieldList, formatList, headerList);
            
        }

        /// <summary>
        /// Chỉ gọi 1 lần, gọi nhiều bị lỗi.
        /// Sắp xếp thứ tự và gán formatString.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="showColumns">Các cột sẽ hiện ra theo thứ tự</param>
        /// <param name="formatStrings"></param>
        /// <param name="headerStrings"></param>
        public static void FormatGridViewColumnsShowOrder(DataGridView dgv, string[] showColumns, string[] formatStrings, string[] headerStrings)
        {
            try
            {
                if (showColumns == null || showColumns.Length == 0) return;
                dgv.HideAllColumns();
                dgv.AutoGenerateColumns = false;
                var index = dgv.Columns.Cast<DataGridViewColumn>().Count(column => column.Frozen && column.Visible);
                for (int i = 0; i < showColumns.Length; i++)
                {
                    string field = showColumns[i].Trim();
                    var column = dgv.Columns[field];
                    if (column != null)
                    {
                        if (!column.Frozen)
                        {
                            column.Visible = true;
                            column.DisplayIndex = index++;
                        }

                        var dataType = column.ValueType;
                        if (formatStrings.Length > i && !string.IsNullOrEmpty(formatStrings[i]))
                        {
                            var format = formatStrings[i].Trim();

                            if (dataType == typeof(int)
                                || dataType == typeof(decimal)
                                || dataType == typeof(double)
                                || dataType == typeof(long)
                                || dataType == typeof(short)
                                || dataType == typeof(float)
                                || dataType == typeof(Int16)
                                || dataType == typeof(Int32)
                                || dataType == typeof(Int64)
                                || dataType == typeof(uint)
                                || dataType == typeof(UInt16)
                                || dataType == typeof(UInt32)
                                || dataType == typeof(UInt64)
                                || dataType == typeof(byte)
                                || dataType == typeof(sbyte)
                                || dataType == typeof(Single))
                            {
                                var ff = format.Split(':');
                                column.DefaultCellStyle.Format = ff[0];
                                if (ff.Length > 1)
                                    column.Width = PrimitiveTypes.ObjectToInt(ff[1]);
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                column.Width = PrimitiveTypes.ObjectToInt(format.Substring(1));
                            }
                            else if (column.ValueType == typeof(string))
                            {
                                column.Width = PrimitiveTypes.ObjectToInt(format.Substring(1));
                            }
                        }

                        if (headerStrings != null && headerStrings.Length > i && !string.IsNullOrEmpty(headerStrings[i]))
                        {
                            column.HeaderText = headerStrings[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteToLog("FormatGridViewColumnsShowOrder: " + ex.Message);
            }
        }
    }
}
