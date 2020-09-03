namespace V6Controls.Forms
{
    partial class DateSelectForm
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
            this.lichViewControl1 = new V6Controls.Controls.LichView.LichViewControl();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lichViewControl1
            // 
            this.lichViewControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lichViewControl1.BorderColor = System.Drawing.Color.Black;
            this.lichViewControl1.DetailColor = System.Drawing.Color.Orange;
            this.lichViewControl1.FocusBackColor = System.Drawing.Color.Green;
            this.lichViewControl1.FooterHeight = 0;
            this.lichViewControl1.FooterText = null;
            this.lichViewControl1.HeaderHeight = 50;
            this.lichViewControl1.HoverBackColor = System.Drawing.Color.Aqua;
            this.lichViewControl1.Location = new System.Drawing.Point(0, 0);
            this.lichViewControl1.MouseLocation = new System.Drawing.Point(0, 0);
            this.lichViewControl1.Name = "lichViewControl1";
            this.lichViewControl1.RowData = null;
            this.lichViewControl1.SatudayColor = System.Drawing.Color.Blue;
            this.lichViewControl1.ShowNextPrevious = true;
            this.lichViewControl1.Size = new System.Drawing.Size(550, 261);
            this.lichViewControl1.SundayColor = System.Drawing.Color.Red;
            this.lichViewControl1.TabIndex = 5;
            this.lichViewControl1.ClickNextEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichViewControl1_ClickNextEvent);
            this.lichViewControl1.ClickPreviousEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichViewControl1_ClickPreviousEvent);
            this.lichViewControl1.DoubleClick += new System.EventHandler(this.lichViewControl1_DoubleClick);
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleDescription = "REPORTB00005";
            this.btnThoat.AccessibleName = "";
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::V6Controls.Properties.Resources.Cancel;
            this.btnThoat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnThoat.Location = new System.Drawing.Point(88, 267);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 40);
            this.btnThoat.TabIndex = 16;
            this.btnThoat.Tag = "Escape";
            this.btnThoat.Text = "&Hủy";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(0, 267);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 15;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // btnToday
            // 
            this.btnToday.AccessibleName = "";
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnToday.Location = new System.Drawing.Point(426, 267);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(112, 28);
            this.btnToday.TabIndex = 16;
            this.btnToday.Text = "Hôm n&ay";
            this.btnToday.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // DateSelectForm
            // 
            this.AcceptButton = this.btnNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(550, 325);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.lichViewControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateSelectForm";
            this.ShowInTaskbar = false;
            this.Text = "AmLich";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.lichViewControl1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnThoat, 0);
            this.Controls.SetChildIndex(this.btnToday, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.Controls.LichView.LichViewControl lichViewControl1;
        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnToday;

    }
}

