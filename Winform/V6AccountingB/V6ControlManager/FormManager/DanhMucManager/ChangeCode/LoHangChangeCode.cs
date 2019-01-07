using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public partial class LoHangChangeCode : ChangeCodeBase0
    {
        protected IDictionary<string, object> Data;

        public string AccessibleNameOldCode
        {
            get { return txtOldCode.AccessibleName; }
            set { txtOldCode.AccessibleName = value; }
        }
        public string AccessibleNameOldCodeName
        {
            get { return txtName.AccessibleName; }
            set { txtName.AccessibleName = value; }
        }

        public LoHangChangeCode()
        {
            InitializeComponent();
        }

        public LoHangChangeCode(IDictionary<string, object> data)
        {
            Data = data;
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Gọi sau khi gán accessibleName
        /// </summary>
        protected void MyInit()
        {
            V6ControlFormHelper.SetFormDataDictionary(groupBox1, Data);
            radDoiNGay.Checked = true;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            txtNewCode.Upper();
        }

        private RadioButton lastChoose;
        private void CheckEnable(RadioButton rad)
        {
            if (rad != null && rad == lastChoose) return;
            lastChoose = rad;

            if (rad == radDoiNGay)
            {
                txtNewCode.Enabled = false;
                txtNgayHHSDmoi.Enabled = true;
            }
            else if (rad == radDoiMaLo)
            {
                txtNewCode.Enabled = true;
                txtNgayHHSDmoi.Enabled = false;
            }
            else
            {
                txtNewCode.Enabled = true;
                txtNgayHHSDmoi.Enabled = true;
            }
        }

        public virtual void Nhan(string tableName, string inputField)
        {
            if (_do_change_code_running)
            {
                this.ShowMessage(V6Text.Busy);
                return;
            }
            var newCode = txtNewCode.Text.Trim().ToUpper();
            var newDate = txtNgayHHSDmoi.Value;
            var oldCode = txtOldCode.Text.Trim().ToUpper();
            //var oldDate = txtNgayHHSD.Value;
            var oldMavt = txtMaVt.Text.Trim().ToUpper();
            var ctype = "1";
            if (radDoiNGay.Checked)
            {
                if (newDate == null)
                {
                    this.ShowWarningMessage("Chưa nhập hạn sử dụng mới.");
                    return;
                }
                ctype = "1";

            }
            else if (radDoiMaLo.Checked)
            {
                if (newCode == "" )
                {
                    this.ShowWarningMessage("Không được dùng mã rỗng!");
                    return;
                }

                if (newCode.ToUpper() == oldCode.ToUpper())
                {
                    this.ShowWarningMessage("Mã mới phải khác mã cũ!");
                    return;
                }

                ctype = "2";
                
            }
            else if (radDoiMaLoVaNgay.Checked)
            {
                if (newDate == null || newCode == "")
                {
                    this.ShowWarningMessage("Chưa nhập đủ thông tin.");
                    return;
                }
                ctype = "3";
            }


            var a = V6BusinessHelper.IsValidTwoCode_Full(tableName, 0, "MA_VT", oldMavt, oldMavt, "MA_LO", newCode, oldCode);

            if (!a)
            {
                if (this.ShowConfirmMessage(V6Text.Exist
                    + " Có muốn đổi mã không?") != DialogResult.Yes)
                    return;
            }

                //Thuc hien sua
            DoChangeCode(ctype, "MA_VT", oldMavt, oldMavt, "MA_LO", oldCode, newCode, newDate);
        }

        protected virtual void DoAfterFinish()
        {
            try
            {
                OnDoChangeCodeFinish(newData);
                ShowTopLeftMessage("Đã thực hiện xong!");
                Close();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        SortedDictionary<string, object> newData;
        protected string _ctype, _inputField1, _oldMavt, _newMavt, _inputField, _oldCode, _newCode;
        protected DateTime? _newDate;
        protected bool _do_change_code_running, _do_change_code_finish, _do_change_code_error;

        protected virtual void DoChangeCode(string ctype,string inputField1, string oldMavt, string newMavt, string inputField, string oldCode, string newCode, DateTime? newDate)
        {
            try
            {
                if (_do_change_code_running) return;

                newData = new SortedDictionary<string, object>();
                if(ctype == "1")
                   newData["NGAY_HHSD"] = newDate;
                else if (ctype == "2")
                   newData["MA_LO"] = newCode;
                else
                {
                   newData["NGAY_HHSD"] = newDate;
                   newData["MA_LO"] = newCode;
                }

                _ctype = ctype;
                _inputField1 = inputField1;
                _oldMavt = oldMavt;
                _inputField = inputField;
                _oldCode = oldCode;
                _newCode = newCode;
                _newDate = newDate;
                
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
                this.ShowErrorMessage(GetType() + ".LoHangChangeCode DoChangeCode " + ex.Message);
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

        private void DoChangeCodeThread()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@ctype", _ctype), 
                    new SqlParameter("@coItem_vt", _oldMavt), 
                    new SqlParameter("@coItem", _oldCode), 
                    new SqlParameter("@cnItem", _newCode), 
                    new SqlParameter("@dnDate", _newDate), 
                };
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_EDITLOTID_CHANGE_ALL", plist);
            }
            catch (Exception ex)
            {
                _do_change_code_error = true;
                _message = ex.Message;
                this.WriteExLog(GetType() + ".DoChangeCodeThread", ex);
            }
            _do_change_code_finish = true;
        }
        
        protected virtual void btnNhan_Click(object sender, EventArgs e)
        {
           Nhan("Allo", "Ma_lo");
        }

        protected virtual void btnHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rads_CheckedChanged(object sender, EventArgs e)
        {
            var rad = (RadioButton) sender;
            if(rad.Checked)
                CheckEnable(rad);
        }

    }
}
