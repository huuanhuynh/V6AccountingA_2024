using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;

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
        public bool Data_Loading, _load_data_success;
        public string _sttRec { get; set; }
        /// <summary>
        /// Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo
        /// </summary>
        [DefaultValue(null)]
        [Description("Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo")]
        public string FilterType { get; set; }

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
            lblTopMessage.Top -= lblTopMessage.Height;
            MessageLable = lblTopMessage;
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

        protected bool do_hot_key;
        public virtual void DoHotKey (Keys keyData)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                //Nếu đã thực hiện lệnh bên trên gửi xuống thì không chạy DoHotKey0
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

        /// <summary>
        /// Sẽ click lên button hoặc V6label có Tag = keyData.ToString()
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>true nếu có lick/ false nếu không</returns>
        public virtual bool DoHotKey0(Keys keyData)
        {
            return V6ControlFormHelper.DoKeyCommand(this, keyData);
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

        #region ==== SHOW HIDE MESSAGE ====

        public void ShowMainMessage(string message)
        {
            V6ControlFormHelper.ShowMainMessage(message);
        }

        public static V6Label MessageLable;
        private static Timer _topMessageTimer;
        private static int _topTime = -1;
        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên trái của form.
        /// </summary>
        /// <param name="message"></param>
        public void ShowTopLeftMessage(string message)
        {
            if (_topMessageTimer != null && _topMessageTimer.Enabled)
            {
                _topMessageTimer.Stop();
                //MessageLable.Top 
            }
            MessageLable.BringToFront();
            MessageLable.Text = message;
            
            _topMessageTimer = new Timer { Interval = 200 };
            _topMessageTimer.Tick += topMessageTimer_Tick;
            _topTime = -1;
            _topMessageTimer.Start();
            MessageLable.Visible = true;
        }

        static void topMessageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _topTime++;
                
                if (_topTime < 10)//Hiện ra
                {
                    MessageLable.Top -= MessageLable.Top / 3;

                    if (MessageLable.Top == -1) MessageLable.Top = 0;
                    if (MessageLable.Top == -2) MessageLable.Top = -1;
                    return;
                }
                if (_topTime < 20)//Dừng lại
                {
                    return;
                }
                if (_topTime < 30)//Ẩn đi
                {
                    MessageLable.Top -= MessageLable.Bottom / 3;
                    if (MessageLable.Bottom == 1) MessageLable.Top = -MessageLable.Height;
                    if (MessageLable.Bottom == 2) MessageLable.Top = -MessageLable.Height + 1;
                    return;
                }
                _topMessageTimer.Stop();
            }
            catch// (Exception)
            {
                // ignored
            }
        }

        #endregion show hide message
    }
}
