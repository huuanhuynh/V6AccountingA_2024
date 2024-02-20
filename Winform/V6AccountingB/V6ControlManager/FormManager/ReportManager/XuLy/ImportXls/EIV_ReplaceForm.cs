using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class EIV_ReplaceForm : V6Form
    {
        protected IDictionary<string, object> Data;

        public string AccessibleNameOldCode
        {
            get { return cboFields.AccessibleName; }
            set { cboFields.AccessibleName = value; }
        }

        
        public EIV_ReplaceForm()
        {
            InitializeComponent();
        }
        public EIV_ReplaceForm(IDictionary<string, object> data)
        {
            Data = data;
            InitializeComponent();
            //MyInit();
        }

        /// <summary>
        /// Gọi sau khi gán accessibleName
        /// </summary>
        protected void MyInit()
        {
            //V6ControlFormHelper.SetFormDataDictionary(groupBox1, Data);
        }
        public virtual void Nhan(string tableName, string inputField)
        {
            
        }

        protected virtual void DoAfterFinish()
        {
            try
            {
                Data[_field] = _newCode;
                //OnDoChangeCodeFinish(Data);
                ShowMainMessage(V6Text.Finish);
                Close();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        protected string _oldCode, _newCode, _field;
        protected bool _do_change_code_running, _do_change_code_finish, _do_change_code_error;

        private void DoChangeCode(string oldCode, string newCode, string field)
        {
            try
            {
                if (_do_change_code_running) return;

                _oldCode = oldCode;
                _newCode = newCode;
                _field = field;
                _do_change_code_running = true;
                _do_change_code_finish = false;
                _do_change_code_error = false;
                btnNhanImage = btnNhan.Image;
                Thread T = new Thread(DoChangeCodeThread);
                T.Start();
                Timer timer = new Timer();
                timer.Tick += timer_Tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".EIV_ReplaceForm DoChangeCode " + ex.Message);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (_do_change_code_finish)
            {
                ((Timer)sender).Stop();
                _do_change_code_running = false;
                btnNhan.Image = btnNhanImage;

                if (_do_change_code_error)
                {
                    this.ShowErrorMessage(GetType() + ".DoChangeCodeError " + _message, 300);
                }
                else
                {
                    DoAfterFinish();
                }
            }
            else
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
        }

        protected virtual void DoChangeCodeThread()
        {
            try
            {
                V6BusinessHelper.ChangeCustomeId(_oldCode, _newCode);
            }
            catch (Exception ex)
            {
                _do_change_code_error = true;
                _message = ex.Message;
                this.WriteExLog(GetType() + ".DoChangeCodeThread", ex);
            }
            _do_change_code_finish = true;
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void KhachHangChangeCodeForm_Load(object sender, EventArgs e)
        {
            //txtNewCode.Upper();
        }

        protected virtual void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("Base", "Base");
        }

        protected virtual void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
