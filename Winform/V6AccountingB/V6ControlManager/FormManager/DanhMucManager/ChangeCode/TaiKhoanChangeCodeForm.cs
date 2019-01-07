using System;
using System.Collections.Generic;
using System.Reflection;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class TaiKhoanChangeCodeForm : ChangeCodeFormBase
    {
        public TaiKhoanChangeCodeForm(IDictionary<string, object> data)
            : base(data)
        {
            InitializeComponent();
            Text = (V6Setting.IsVietnamese ? "Đổi mã tài khoản" : "Change code: Account ");
            //Gọi lại hàm gán data.
            MyInit();
        }

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("ALTK0","TK");
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TaiKhoanChangeCodeForm
            // 
            this.AccessibleNameOldCode = "TK";
            this.AccessibleNameOldCodeName = "ten_tk";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 142);
            this.Name = "TaiKhoanChangeCodeForm";
            this.ResumeLayout(false);

        }

        protected override void DoChangeCodeThread()
        {
            try
            {
                V6BusinessHelper.ChangeAccountId(_oldCode, _newCode);
            }
            catch (Exception ex)
            {
                _do_change_code_error = true;
                _message = ex.Message;
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
            _do_change_code_finish = true;
        }
    }
}
