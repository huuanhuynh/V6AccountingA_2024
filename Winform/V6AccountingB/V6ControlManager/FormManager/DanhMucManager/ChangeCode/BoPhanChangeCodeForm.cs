using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class BoPhanChangeCodeForm : ChangeCodeFormBase
    {
        public BoPhanChangeCodeForm(SortedDictionary<string, object> data)
            : base(data)
        {
            InitializeComponent();
            this.Text = (V6Setting.IsVietnamese ? "Đổi mã bộ phận" : "Change code: department");
            //Gọi lại hàm gán data.
            MyInit();
        }

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("Albp","MA_BP");
        }

        protected override void DoChangeCodeThread()
        {
            try
            {
                V6BusinessHelper.ChangeDepartmentId(_oldCode, _newCode);
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
            // BoPhanChangeCodeForm
            // 
            this.AccessibleNameOldCode = "ma_bp";
            this.AccessibleNameOldCodeName = "ten_bp";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 142);
            this.Name = "BoPhanChangeCodeForm";
            this.ResumeLayout(false);

        }

    }
}
