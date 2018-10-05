namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class V6CHECK_U1
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.dateNgay_ct2 = new V6Controls.V6DateTimePick();
            this.label2 = new System.Windows.Forms.Label();
            this.dateNgay_ct1 = new V6Controls.V6DateTimePick();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cboProcList = new V6Controls.V6ComboBox();
            this.v6Label20 = new V6Controls.V6Label();
            this.lblTen = new V6Controls.V6Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboProcList);
            this.panel1.Controls.Add(this.lblTen);
            this.panel1.Controls.Add(this.v6Label20);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateNgay_ct2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateNgay_ct1);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "FILTERL00003";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đến ngày";
            // 
            // dateNgay_ct2
            // 
            this.dateNgay_ct2.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct2.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct2.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct2.Location = new System.Drawing.Point(61, 32);
            this.dateNgay_ct2.Name = "dateNgay_ct2";
            this.dateNgay_ct2.Size = new System.Drawing.Size(101, 20);
            this.dateNgay_ct2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00002";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Từ ngày";
            // 
            // dateNgay_ct1
            // 
            this.dateNgay_ct1.CustomFormat = "dd/MM/yyyy";
            this.dateNgay_ct1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgay_ct1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgay_ct1.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgay_ct1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgay_ct1.LeaveColor = System.Drawing.Color.White;
            this.dateNgay_ct1.Location = new System.Drawing.Point(61, 5);
            this.dateNgay_ct1.Name = "dateNgay_ct1";
            this.dateNgay_ct1.Size = new System.Drawing.Size(101, 20);
            this.dateNgay_ct1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Location = new System.Drawing.Point(6, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 288);
            this.tabControl1.TabIndex = 7;
            // 
            // cboProcList
            // 
            this.cboProcList.AccessibleName = "";
            this.cboProcList.BackColor = System.Drawing.SystemColors.Window;
            this.cboProcList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProcList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboProcList.FormattingEnabled = true;
            this.cboProcList.Items.AddRange(new object[] {
            "0 - Chưa cập nhập",
            "1 - Cập nhập tất cả",
            "2 - Chỉ cập nhập vào kho"});
            this.cboProcList.Location = new System.Drawing.Point(253, 8);
            this.cboProcList.Name = "cboProcList";
            this.cboProcList.Size = new System.Drawing.Size(304, 24);
            this.cboProcList.TabIndex = 5;
            this.cboProcList.SelectedIndexChanged += new System.EventHandler(this.cboProcList_SelectedIndexChanged);
            // 
            // v6Label20
            // 
            this.v6Label20.AccessibleDescription = "XULYL00122";
            this.v6Label20.AutoSize = true;
            this.v6Label20.Location = new System.Drawing.Point(176, 11);
            this.v6Label20.Name = "v6Label20";
            this.v6Label20.Size = new System.Drawing.Size(59, 13);
            this.v6Label20.TabIndex = 4;
            this.v6Label20.Text = "Chức năng";
            // 
            // lblTen
            // 
            this.lblTen.AccessibleDescription = "XULYL00123";
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(176, 34);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(55, 13);
            this.lblTen.TabIndex = 6;
            this.lblTen.Text = "Trạng thái";
            this.lblTen.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDataToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 26);
            // 
            // viewDataToolStripMenuItem
            // 
            this.viewDataToolStripMenuItem.Name = "viewDataToolStripMenuItem";
            this.viewDataToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.viewDataToolStripMenuItem.Text = "ViewData";
            this.viewDataToolStripMenuItem.Click += new System.EventHandler(this.viewDataToolStripMenuItem_Click);
            // 
            // V6CHECK_U1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "V6CHECK_U1";
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private V6Controls.V6DateTimePick dateNgay_ct2;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6DateTimePick dateNgay_ct1;
        private System.Windows.Forms.TabControl tabControl1;
        private V6Controls.V6ComboBox cboProcList;
        private V6Controls.V6Label v6Label20;
        private V6Controls.V6Label lblTen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewDataToolStripMenuItem;
    }
}
