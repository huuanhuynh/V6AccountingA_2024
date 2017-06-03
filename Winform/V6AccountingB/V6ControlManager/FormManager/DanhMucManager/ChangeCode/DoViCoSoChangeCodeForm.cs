using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class DonViCoSoChangeCodeForm : ChangeCodeFormBase
    {
        public DonViCoSoChangeCodeForm(SortedDictionary<string, object> data)
            : base(data)
        {
            InitializeComponent();
            this.Text = (V6Setting.IsVietnamese ? "Đổi mã đơn vị cơ sở" : "Change code: Agent ID "); 
            //Gọi lại hàm gán data.
            MyInit();
        }

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("ALDVCS","MA_DVCS");
        }

        protected override void DoChangeCodeThread()
        {
            try
            {
                V6BusinessHelper.ChangeUnitId(_oldCode, _newCode);
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
            // DonViCoSoChangeCodeForm
            // 
            this.AccessibleNameOldCode = "MA_DVCS";
            this.AccessibleNameOldCodeName = "TEN_DVCS";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 142);
            this.Name = "DonViCoSoChangeCodeForm";
            this.ResumeLayout(false);

        }
    }
}
