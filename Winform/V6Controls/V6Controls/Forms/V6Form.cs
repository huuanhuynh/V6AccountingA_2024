using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms
{
    public partial class V6Form : Form
    {
        
        /// <summary>
        /// Image index
        /// </summary>
        protected int ii = 0;
        protected bool _ready;
        public string ItemID { get { return m_itemId; } }
        protected string m_itemId = null;
        protected string _message = "";
        public bool _dataloading, _dataloaded;
        public string _sttRec { get; set; }
        /// <summary>
        /// Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo
        /// </summary>
        [DefaultValue(null)]
        [Description("Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo")]
        public string FilterType { get; set; }

        /// <summary>
        /// Lớp chứa các dynamic_method
        /// </summary>
        public Type Event_program;
        /// <summary>
        /// Các object có thể có trong chữ ký hàm dynamic_method
        /// </summary>
        public Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        /// <summary>
        /// Ánh xạ tên Event và dynamic_method tương ứng.
        /// </summary>
        public Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        public object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }

        [DefaultValue(null)]
        protected Image btnNhanImage
        {
            get { return _btnNhanImage; }
            set
            {
                if (_btnNhanImage == null) _btnNhanImage = value;
            }
        }
        private Image _btnNhanImage;

        protected ImageList waitingImages { get { return _waitingImages; } }
        public V6Form()
        {
            InitializeComponent();
        }

        public bool IsReady { get { return _ready; } }

        public void Ready()
        {
            _ready = true;
        }

        private void V6Form_Load(object sender, EventArgs e)
        {
            LoadLanguage();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClearAll();
            base.OnHandleDestroyed(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            if (!e.Cancel && this.Owner != null) this.Owner.Focus();
        }

        /// <summary>
        /// Hãy override hàm này để tối ưu bộ nhớ.
        /// <para>Xóa biến, tối ưu bộ nhớ.</para>
        /// <para>Tối ưu Crystal Report.</para>
        /// </summary>
        protected virtual void ClearMyVars()
        {

        }
        /// <summary>
        /// Xóa các biến tối ưu bộ nhớ. Code xóa được viết override trong hàm ClearMyVar.
        /// </summary>
        public void CleanUp()
        {
            try
            {
                ClearMyVars();
                GC.Collect();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ClearReportDocumentBase", ex);
            }
        }

        /// <summary>
        /// <para>Xóa biến, tối ưu bộ nhớ.</para>
        /// <para>Tối ưu Crystal Report.</para>
        /// </summary>
        private void ClearAll()
        {
            string methodLog = null;
            try
            {
                methodLog = "Clear();";
                CleanUp();
                GC.Collect();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ClearAll " + methodLog, ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu mặc định lên form.
        /// </summary>
        /// <param name="loai">1ct 2danhmuc 4report 5filter</param>
        /// <param name="mact"></param>
        /// <param name="madm"></param>
        /// <param name="itemId"></param>
        /// <param name="adv"></param>
        protected void LoadDefaultData(int loai, string mact, string madm, string itemId, string adv = "")
        {
            try
            {
                var data = V6ControlFormHelper.GetDefaultFormData(V6Setting.Language, loai, mact, madm, itemId, adv);
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
                        V6ControlFormHelper.SetFormDefaultValueInfo(this, item.Value);
                        continue;
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
                V6ControlFormHelper.SetSomeDataDictionary(this, someData);
            }
            catch (Exception ex0)
            {
                this.WriteExLog(GetType() + ".SetDefaultDataInfoToForm", ex0);
            }
        }


        protected virtual void LoadLanguage()
        {
            V6ControlFormHelper.SetFormText(this);
        }

        /// <summary>
        /// Gán MaxLength theo cấu trúc bảng.
        /// </summary>
        /// <param name="tableName"></param>
        protected void LoadStruct(string tableName)
        {
            try
            {
                var _structTable = V6BusinessHelper.GetTableStruct(tableName);
                V6ControlFormHelper.SetFormStruct(this, _structTable);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Load struct eror!", ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                //// Nếu đã thực hiện lệnh bên trên gửi xuống thì không chạy DoHotKey0
                ////if (do_hot_key)
                //{
                //    //do_hot_key = false;
                //    return base.ProcessCmdKey(ref msg, keyData);
                //}
                if (DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //protected bool do_hot_key;
        public virtual void DoHotKey(Keys keyData)
        {
            try
            {
                //do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Đếm Ctrol + Alt + I
        /// </summary>
        private int _ctrl_alt_i;
        /// <summary>
        /// Sẽ click lên button hoặc V6label có Tag = keyData.ToString()
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>true nếu có lick/ false nếu không</returns>
        public virtual bool DoHotKey0(Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Alt | Keys.I))
            {
                _ctrl_alt_i++;
                if (_ctrl_alt_i == 2)
                {
                    Control c = V6ControlFormHelper.GetControlUnderMouse(this);
                    if (c != null)
                    {
                        string c_Text = c.Text;
                        var box = c as TextBox;
                        if (box != null && box.PasswordChar != '\0')
                        {
                            c_Text = "***";
                        }
                        string s = string.Format("{0}({1}), Text({2}), Aname({3}), Adescription({4}).",
                            c.GetType(), c.Name, c_Text, c.AccessibleName, c.AccessibleDescription);
                        Clipboard.SetText(s);
                        V6ControlFormHelper.SetStatusText(s);
                    }
                }
                if (_ctrl_alt_i >= 3)
                {
                    _ctrl_alt_i = 0;
                    Control c = V6ControlFormHelper.GetControlUnderMouse(FindForm());
                    if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return false;
                    V6ControlFormHelper.ShowControlsProperties(this, c);
                    
                    return true;
                }
            }
            else
            {
                _ctrl_alt_i = 0;
            }

            bool flag1 = false;
            foreach (Control c in this.Controls)
            {
                if (c is V6FormControl)
                {
                    flag1 = true;
                    ((V6FormControl)c).DoHotKey(keyData);
                }
            }

            if (V6ControlFormHelper.DoKeyCommand(this, keyData)) return true;

            if (!flag1)
            {
                if (keyData == Keys.Escape)
                {
                    Close();
                }
            }

            return false;
        }

        public void SetControlValue(Control control, object value)
        {
            V6ControlFormHelper.SetControlValue(control, value);
        }

        /// <summary>
        /// Lấy dữ liệu trên form.
        /// </summary>
        /// <returns></returns>
        public virtual SortedDictionary<string, object> GetData()
        {
            return V6ControlFormHelper.GetFormDataDictionary(this);
        }

        /// <summary>
        /// Tìm kiếm và trả về control nằm trong control này hoặc chính nó nếu trùng tên (ko phân biệt hoa thường).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control GetControlByName(string name)
        {
            return V6ControlFormHelper.GetControlByName(this, name);
        }

        /// <summary>
        /// Tìm kiếm và trả về control nằm trong control này hoặc chính nó nếu trùng accessibleName (ko phân biệt hoa thường).
        /// </summary>
        /// <param name="accessibleName"></param>
        /// <returns></returns>
        public Control GetControlByAccessibleName(string accessibleName)
        {
            return V6ControlFormHelper.GetControlByAccessibleName(this, accessibleName);
        }


        /// <summary>
        /// Lấy Property hoặc Field trong V6Control.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetProperty(string name)
        {
            try
            {
                string NAME = name.ToUpper();

                foreach (FieldInfo fieldInfo in GetType().GetFields())
                {
                    if (fieldInfo.Name.ToUpper() == NAME)
                    {
                        return fieldInfo.GetValue(this);
                    }
                }
                foreach (PropertyInfo propertyInfo in GetType().GetProperties())
                {
                    if (propertyInfo.Name.ToUpper() == NAME && propertyInfo.CanRead)
                        return propertyInfo.GetValue(this, null);
                }
            }
            catch
            {
                //
            }

            return null;
        }


        #region ==== SHOW HIDE MESSAGE ====

        public void ShowMainMessage(string message)
        {
            V6ControlFormHelper.ShowMainMessage(message);
        }

        #endregion show hide message
    }
}
