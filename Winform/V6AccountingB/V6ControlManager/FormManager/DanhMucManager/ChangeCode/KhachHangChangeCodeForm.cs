using System;
using System.Collections.Generic;
using V6Init;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class KhachHangChangeCodeForm : ChangeCodeFormBase
    {
        public KhachHangChangeCodeForm(IDictionary<string, object> data)
            : base(data)
        {
            InitializeComponent();
            Text = (V6Setting.IsVietnamese ? "Đổi mã khách hàng" : "Change code: customer ");
            //Gọi lại hàm gán data.
            MyInit();
        }

        protected override void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan("Alkh","MA_KH");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // KhachHangChangeCodeForm
            // 
            this.AccessibleNameOldCode = "ma_kh";
            this.AccessibleNameOldCodeName = "ten_kh";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(716, 142);
            this.Name = "KhachHangChangeCodeForm";
            this.ResumeLayout(false);

        }
    }
}
