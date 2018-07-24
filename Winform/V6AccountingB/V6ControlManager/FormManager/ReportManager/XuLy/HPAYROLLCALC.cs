using System;
using V6Controls;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class HPAYROLLCALC : XuLyBase0
    {
        private System.Windows.Forms.Label lblStatus;
    
        public HPAYROLLCALC(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            lblStatus.AutoSize = true;
            lblStatus.Left = btnHuy.Right + 3;
        }

        public override void SetStatus2Text()
        {
        }

        protected override void Nhan()
        {
            int thangKhoaSo;
            int thangXuLy;
            thangKhoaSo = V6Setting.M_Ngay_ks.Year * 12 + V6Setting.M_Ngay_ks.Month;
            thangXuLy = (int)FilterControl.Number2 * 12 + (int)FilterControl.Number1;
            if (thangXuLy < thangKhoaSo)
            {
                this.ShowWarningMessage(V6Text.CheckLock);
                return;
            }
            base.Nhan();
        }

        //Sửa lại hoàn toàn. Tách thành nhiều hàm trong Thread.
       
        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNhan
            // 
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click_1);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(181, 390);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(222, 18);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = ".";
            // 
            // HPAYROLLCALC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.lblStatus);
            this.Name = "HPAYROLLCALC";
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.ResumeLayout(false);

        }

        private void btnNhan_Click_1(object sender, EventArgs e)
        {

        }
    }
}
