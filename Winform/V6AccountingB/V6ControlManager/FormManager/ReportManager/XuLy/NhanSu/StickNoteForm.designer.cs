using V6Controls.Controls.LichView;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    partial class StickNoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StickNoteForm));
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.lichView1 = new V6Controls.Controls.LichView.LichViewControl();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(94, 450);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 450);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 8;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Visible = false;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // lichView1
            // 
            this.lichView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lichView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lichView1.BorderColor = System.Drawing.Color.Black;
            this.lichView1.CodeForm = null;
            this.lichView1.DataSource = ((System.Collections.Generic.IDictionary<int, V6Controls.Controls.LichView.LichViewCellData>)(resources.GetObject("lichView1.DataSource")));
            this.lichView1.DetailColor = System.Drawing.Color.Orange;
            this.lichView1.FooterHeight = 20;
            this.lichView1.FooterText = "Footer";
            this.lichView1.HeaderHeight = 40;
            this.lichView1.HoverBackColor = System.Drawing.Color.Aqua;
            this.lichView1.Location = new System.Drawing.Point(12, 12);
            this.lichView1.Name = "lichView1";
            this.lichView1.RowData = null;
            this.lichView1.SatudayColor = System.Drawing.Color.Blue;
            this.lichView1.Size = new System.Drawing.Size(759, 433);
            this.lichView1.SundayColor = System.Drawing.Color.Red;
            this.lichView1.TabIndex = 11;
            this.lichView1.ClickNextEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichView1_ClickNextEvent);
            this.lichView1.ClickPreviousEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichView1_ClickPreviousEvent);
            this.lichView1.ClickCellEvent += new System.Action<V6Controls.Controls.LichView.LichViewControl, V6Controls.Controls.LichView.LichViewEventArgs>(this.lichView1_ClickCellEvent);
            // 
            // StickNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 502);
            this.Controls.Add(this.lichView1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "StickNoteForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.lichView1, 0);
            this.ResumeLayout(false);

        }

        

        #endregion

        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private LichViewControl lichView1;




    }
}