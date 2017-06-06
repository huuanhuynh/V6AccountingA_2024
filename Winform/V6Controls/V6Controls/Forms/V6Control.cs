using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Init;
using V6Tools;

namespace V6Controls.Forms
{
    /// <summary>
    /// Dùng cho một usercontrol phụ trong form chính.
    /// Ví dụ HD_Details
    /// </summary>
    public partial class V6Control : UserControl
    {
        public bool IsReady { get { return _ready0; } }
        protected bool _ready0;
        public string ItemID { get { return m_itemId; } }
        protected string m_itemId = null;
        public V6Control()
        {
            InitializeComponent();
        }

        public string CodeForm { get; set; }

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
        /// Hàm ảo, tải dữ liệu, Cần override để sử dụng (gọi).
        /// </summary>
        /// <param name="code"></param>
        public virtual void LoadData(string code)
        {
            //throw new NotImplementedException();
        }


        private bool ApplyControlTripleClick;
        protected void LoadLanguage()
        {
            if (V6Setting.IsDesignTime) return;
            
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
        public virtual void DoHotKey (Keys keyData)
        {
            try
            {
                DoHotKey0(keyData);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.DoHotKey {1} {2}", GetType(), keyData, ex.Message));
            }
        }

        public virtual bool DoHotKey0(Keys keyData)
        {
            try
            {
                return V6ControlFormHelper.DoKeyCommand(this, keyData);
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
            ShowTopMessage(V6Text.NoDefine);
        }

        protected void Ready()
        {
            _ready0 = true;
        }

        /// <summary>
        /// Hàm hỗ trợ. Thường dùng để xử lý dữ liệu của bảng chính gửi vào.
        /// </summary>
        /// <param name="data"></param>
        public virtual void SetParentData(IDictionary<string, object> data)
        {
            
        }

        public void ShowMainMessage(string message)
        {
            V6ControlFormHelper.ShowMainMessage(message);
        }

        public void SetStatusText(string message)
        {
            V6ControlFormHelper.SetStatusText(message);
        }

        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên trái của form chứa.
        /// </summary>
        /// <param name="message"></param>
        public void ShowTopMessage(string message)
        {
            var p = FindParent<V6Form>(20) as V6Form;
            if (p != null)
            {
                p.ShowTopMessage(message);
            }
        }
    }
}
