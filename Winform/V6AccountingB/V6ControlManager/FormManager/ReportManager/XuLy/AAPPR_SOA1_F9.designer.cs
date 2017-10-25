namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AAPPR_SOA1_F9
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.printGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.TxtMa_nvien = new V6Controls.V6VvarTextBox();
            this.TxtMa_bp = new V6Controls.V6VvarTextBox();
            this.v6Label8 = new V6Controls.V6Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcel,
            this.printGrid});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 48);
            // 
            // exportToExcel
            // 
            this.exportToExcel.Name = "exportToExcel";
            this.exportToExcel.Size = new System.Drawing.Size(153, 22);
            this.exportToExcel.Text = "Export To Excel";
            // 
            // printGrid
            // 
            this.printGrid.Name = "printGrid";
            this.printGrid.Size = new System.Drawing.Size(153, 22);
            this.printGrid.Text = "Print Grid";
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 112);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 4;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 112);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // TxtMa_nvien
            // 
            this.TxtMa_nvien.AccessibleName = "ma_nvien";
            this.TxtMa_nvien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMa_nvien.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_nvien.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_nvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMa_nvien.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_nvien.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nvien.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_nvien.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_nvien.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_nvien.Location = new System.Drawing.Point(206, 46);
            this.TxtMa_nvien.Name = "TxtMa_nvien";
            this.TxtMa_nvien.Size = new System.Drawing.Size(91, 20);
            this.TxtMa_nvien.TabIndex = 2;
            this.TxtMa_nvien.VVar = "ma_nvien";
            // 
            // TxtMa_bp
            // 
            this.TxtMa_bp.AccessibleName = "ma_bp";
            this.TxtMa_bp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMa_bp.BackColor = System.Drawing.SystemColors.Window;
            this.TxtMa_bp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtMa_bp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtMa_bp.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_bp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtMa_bp.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_bp.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_bp.Location = new System.Drawing.Point(88, 46);
            this.TxtMa_bp.Name = "TxtMa_bp";
            this.TxtMa_bp.Size = new System.Drawing.Size(97, 20);
            this.TxtMa_bp.TabIndex = 1;
            this.TxtMa_bp.VVar = "ma_bp";
            // 
            // v6Label8
            // 
            this.v6Label8.AccessibleDescription = "XULYL00046";
            this.v6Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.v6Label8.AutoSize = true;
            this.v6Label8.Location = new System.Drawing.Point(-1, 49);
            this.v6Label8.Name = "v6Label8";
            this.v6Label8.Size = new System.Drawing.Size(41, 13);
            this.v6Label8.TabIndex = 0;
            this.v6Label8.Text = "BP/NV";
            // 
            // AAPPR_SOA1_F9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 158);
            this.Controls.Add(this.TxtMa_nvien);
            this.Controls.Add(this.TxtMa_bp);
            this.Controls.Add(this.v6Label8);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AAPPR_SOA1_F9";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.v6Label8, 0);
            this.Controls.SetChildIndex(this.TxtMa_bp, 0);
            this.Controls.SetChildIndex(this.TxtMa_nvien, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        public V6Controls.V6VvarTextBox TxtMa_nvien;
        public V6Controls.V6VvarTextBox TxtMa_bp;
        private V6Controls.V6Label v6Label8;




    }
}