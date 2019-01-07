using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class VuViecChangeCodeForm : ChangeCodeFormBase
    {
        public VuViecChangeCodeForm(IDictionary<string, object> data)
            : base(data)
        {
            InitializeComponent();
            this.Text = (V6Setting.IsVietnamese ? "Đổi mã vụ việc" : "Change code: Job id");
            //Gọi lại hàm gán data.
            MyInit();
        }

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("ALVV","MA_VV");
        }

        protected override void DoChangeCodeThread()
        {
            try
            {
                V6BusinessHelper.ChangeJobId(_oldCode, _newCode);
            }
            catch (Exception ex)
            {
                _do_change_code_error = true;
                _message = ex.Message;
                this.WriteExLog(GetType() + ".DoChangeCodeThread", ex);
            }
            _do_change_code_finish = true;
        }
      private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // VuViecChangeCodeForm
            // 
            this.AccessibleNameOldCode = "MA_VV";
            this.AccessibleNameOldCodeName = "TEN_VV";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 142);
            this.Name = "VuViecChangeCodeForm";
            this.ResumeLayout(false);

        }

    }
}
