namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class DynamicAddEditForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.v6TabControl1 = new V6Controls.V6TabControl();
            this.tabThongTinChinh = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.v6TabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(750, 550);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.v6TabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(7, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 522);
            this.panel1.TabIndex = 0;
            // 
            // v6TabControl1
            // 
            this.v6TabControl1.Controls.Add(this.tabThongTinChinh);
            this.v6TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.v6TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.v6TabControl1.ItemSize = new System.Drawing.Size(230, 24);
            this.v6TabControl1.Location = new System.Drawing.Point(0, 0);
            this.v6TabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.v6TabControl1.Name = "v6TabControl1";
            this.v6TabControl1.SelectedIndex = 0;
            this.v6TabControl1.Size = new System.Drawing.Size(736, 522);
            this.v6TabControl1.TabIndex = 2;
            // 
            // tabThongTinChinh
            // 
            this.tabThongTinChinh.AccessibleDescription = "ADDEDITT00001";
            this.tabThongTinChinh.AutoScroll = true;
            this.tabThongTinChinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabThongTinChinh.Location = new System.Drawing.Point(4, 28);
            this.tabThongTinChinh.Margin = new System.Windows.Forms.Padding(4);
            this.tabThongTinChinh.Name = "tabThongTinChinh";
            this.tabThongTinChinh.Padding = new System.Windows.Forms.Padding(4);
            this.tabThongTinChinh.Size = new System.Drawing.Size(728, 490);
            this.tabThongTinChinh.TabIndex = 0;
            this.tabThongTinChinh.Text = "Thông tin chính";
            // 
            // DynamicAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DynamicAddEditForm";
            this.Size = new System.Drawing.Size(750, 550);
            this.Load += new System.EventHandler(this.DynamicAddEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.v6TabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private V6TabControl v6TabControl1;
        private System.Windows.Forms.TabPage tabThongTinChinh;
    }
}
