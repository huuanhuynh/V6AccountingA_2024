using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    /// <summary>
    /// Dùng cho một usercontrol phụ trong form chính.
    /// Ví dụ HD_Details
    /// </summary>
    public partial class V6Control : UserControl
    {
        /// <summary>
        /// <para>Cờ báo form đã sẵn sàng, khi dùng trên form nhớ chạy hàm Ready() khi khởi tạo form xong.</para>
        /// <para>Thông thường sẽ gọi Ready() trong hàm load (từ form base) hoặc cuối Init.</para>
        /// </summary>
        public bool IsReady { get { return _ready0; } }
        protected bool _ready0;
        public string ItemID { get { return m_itemId; } }
        protected string m_itemId = null;
        public V6Control()
        {
            InitializeComponent();
        }

        [DefaultValue(null)]
        public string CodeForm { get; set; }
        /// <summary>
        /// Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo
        /// </summary>
        [DefaultValue(null)]
        [Description("Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo")]
        public string FilterType { get; set; }

        private void V6Control_Load(object sender, EventArgs e)
        {
            //LoadLanguage
            //V6ControlFormHelper.SetFormText(this);
        }

        /// <summary>
        /// Không làm gì cả.
        /// </summary>
        protected void DoNothing()
        {
            
        }

        /// <summary>
        /// Sau khi lấy về kiểm tra null và ép kiểu lại.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public Control FindParent<T>(int maxLevel = 10)
        {
            return V6ControlFormHelper.FindParent<T>(this, maxLevel);
        }

        /// <summary>
        /// Tìm kiếm và trả về control nằm trong control này hoặc chính nó nếu trùng tên (ko phân biệt hoa thường).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Control GetControlByName(string name)
        {
            Control c = V6ControlFormHelper.GetControlByName(this, name);
            return c;
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

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClearAll();
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// Hãy override hàm này xóa các biến ReportDocument.
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
        /// Lấy dữ liệu trên form.
        /// </summary>
        /// <returns></returns>
        public virtual SortedDictionary<string, object> GetData()
        {
            return V6ControlFormHelper.GetFormDataDictionary(this);
        }
        /// <summary>
        /// Gán dữ liệu lên form theo AccessibleName (không phân biệt hoa thường).
        /// Dữ liệu không có sẽ gán rỗng.
        /// </summary>
        /// <param name="d"></param>
        public virtual void SetData(IDictionary<string, object> d)
        {
            V6ControlFormHelper.SetFormDataDictionary(this, d);
        }
        /// <summary>
        /// Gán vài dữ liệu lên control theo AccessibleName của các control bên trong.
        /// </summary>
        /// <param name="d"></param>
        public virtual void SetSomeData(IDictionary<string, object> d)
        {
            V6ControlFormHelper.SetSomeDataDictionary(this, d);
        }

        /// <summary>
        /// Hàm ảo, tải dữ liệu, Cần override để sử dụng (gọi).
        /// </summary>
        /// <param name="code"></param>
        public virtual void LoadData(string code)
        {
            //throw new NotImplementedException();
        }


        private bool ApplyControlTripleClick;

        public void LoadLanguage()
        {
            if (V6Setting.NotLoggedIn) return;
            
            V6ControlFormHelper.SetFormText(this);
            if (!ApplyControlTripleClick)
            {
                ApplyControlTripleClick = true;
                V6ControlFormHelper.ApplyControlTripleClick(this);
            }
        }
        
        /// <summary>
        /// Khi viết override nếu không làm gì thì gọi lại DoHotKey0
        /// </summary>
        /// <param name="keyData"></param>
        public virtual void DoHotKey(Keys keyData)
        {
            try
            {
                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.DoHotKey {1} {2}", GetType(), keyData, ex.Message));
            }
        }

        /// <summary>
        /// Đếm Ctrol + Alt + I
        /// </summary>
        private int _ctrl_alt_i;
        protected bool do_hot_key;

        public virtual bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Alt | Keys.I))
                {
                    _ctrl_alt_i++;
                    if (_ctrl_alt_i >= 3)
                    {
                        Control c = V6ControlFormHelper.GetControlUnderMouse(FindForm());
                        if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return false;

                        V6ControlFormHelper.ShowControlsProperties(this, c);
                        _ctrl_alt_i = 0;
                        return true;
                    }
                }
                else
                {
                    _ctrl_alt_i = 0;
                }
                //return V6ControlFormHelper.DoKeyCommand(this, keyData);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.DoHotKey0 {1} {2}", GetType(), keyData, ex.Message));
            }
            return false;
        }

        /// <summary>
        /// Hàm ảo vô hiệu nút + của các form chứng từ.
        /// </summary>
        public virtual void DisableZoomButton()
        {
            
        }

        protected void Ready()
        {
            _ready0 = true;
        }

        protected void SetControlValue(Control control, object value)
        {
            V6ControlFormHelper.SetControlValue(control, value);
        }
        protected void SetControlValue(Control control, object value, DefineInfo config)
        {
            V6ControlFormHelper.SetControlValue(control, value, config);
        }

        protected void SetCellValue(DataGridViewCell cell, object value)
        {
            V6ControlFormHelper.SetCellValue(cell, value);
        }
        protected void SetCellValue(DataGridViewCell cell, object value, DefineInfo config)
        {
            V6ControlFormHelper.SetCellValue(cell, value, config);
        }

        /// <summary>
        /// Hàm hỗ trợ. Thường dùng để xử lý dữ liệu của bảng chính gửi vào.
        /// </summary>
        /// <param name="data"></param>
        public virtual void SetParentData(IDictionary<string, object> data)
        {
            
        }

        /// <summary>
        /// Hiển thị thông báo chính, giữa màn hình.
        /// </summary>
        /// <param name="message"></param>
        public void ShowMainMessage(string message)
        {
            //V6ControlFormHelper.ShowMainMessage(message);
            V6ControlFormHelper.ShowTopMessage(message, this);
        }

        /// <summary>
        /// Hiển thị message ở góc dưới bên trái.
        /// </summary>
        /// <param name="message"></param>
        public virtual void SetStatusText(string message)
        {
            V6ControlFormHelper.SetStatusText(message);
        }
        
        private bool _viewFormVarPass = false;
        public string ViewFormVar()
        {
            if (!_viewFormVarPass)
            {
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    _viewFormVarPass = true;
                }
            }
            if (!_viewFormVarPass) return null;

            string result = null;
            try
            {
                StringInput inputForm = new StringInput();
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    var o = V6ControlFormHelper.GetObjectProperty(this, inputForm.InputString);
                    var s = ObjectAndString.ObjectToString(o);
                    //this.ShowInfoMessage();
                    V6Message.Show(s, inputForm.InputString, 0, MessageBoxButtons.OK, MessageBoxIcon.Information, this);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                ShowMainMessage(result);
            }
            return result;
        }
    }
}
