namespace V6ControlManager.FormManager.ReportManager.Filter.Sms
{
    partial class SendSmsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendSmsForm));
            this.xasendmail1 = new V6ControlManager.FormManager.ReportManager.Filter.Sms.XASENDMAIL();
            this.SuspendLayout();
            // 
            // xasendmail1
            // 
            this.xasendmail1.Advance = null;
            this.xasendmail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.xasendmail1.CodeForm = null;
            this.xasendmail1.Date1 = new System.DateTime(((long)(0)));
            this.xasendmail1.Date2 = new System.DateTime(((long)(0)));
            this.xasendmail1.Date3 = new System.DateTime(((long)(0)));
            this.xasendmail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xasendmail1.F10 = false;
            this.xasendmail1.F3 = false;
            this.xasendmail1.F4 = false;
            this.xasendmail1.F5 = false;
            this.xasendmail1.F7 = false;
            this.xasendmail1.F8 = false;
            this.xasendmail1.F9 = false;
            this.xasendmail1.FilterData = null;
            this.xasendmail1.Kieu_post = null;
            this.xasendmail1.Location = new System.Drawing.Point(0, 0);
            this.xasendmail1.Name = "xasendmail1";
            this.xasendmail1.ParentFilterData = null;
            this.xasendmail1.RptExtraParameters = null;
            this.xasendmail1.Size = new System.Drawing.Size(771, 542);
            this.xasendmail1.String1 = null;
            this.xasendmail1.TabIndex = 0;
            // 
            // SendSmsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 542);
            this.Controls.Add(this.xasendmail1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(580, 500);
            this.Name = "SendSmsForm";
            this.Text = "V6MultiSms";
            this.ResumeLayout(false);

        }

        #endregion

        private XASENDMAIL xasendmail1;



    }
}

