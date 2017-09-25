namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AGLCTPB_F4
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.v6Label1 = new V6Controls.V6Label();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label3 = new V6Controls.V6Label();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.dateNam = new V6Controls.V6DateTimePick();
            this.dateThang1 = new V6Controls.V6DateTimePick();
            this.dateThang2 = new V6Controls.V6DateTimePick();
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
            this.btnHuy.Location = new System.Drawing.Point(94, 197);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 8;
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
            this.btnNhan.Location = new System.Drawing.Point(6, 197);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 7;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "XULYL00009";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(23, 10);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(29, 13);
            this.v6Label1.TabIndex = 0;
            this.v6Label1.Text = "Năm";
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "XULYL00054";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(23, 40);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(50, 13);
            this.v6Label2.TabIndex = 2;
            this.v6Label2.Text = "Từ tháng";
            // 
            // v6Label3
            // 
            this.v6Label3.AccessibleDescription = "XULYL00055";
            this.v6Label3.AutoSize = true;
            this.v6Label3.Location = new System.Drawing.Point(23, 67);
            this.v6Label3.Name = "v6Label3";
            this.v6Label3.Size = new System.Drawing.Size(57, 13);
            this.v6Label3.TabIndex = 4;
            this.v6Label3.Text = "Đến tháng";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "XULYL00040";
            this.txtMaDvcs.FieldCaption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(6, 96);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Operator = "=";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 6;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // dateNam
            // 
            this.dateNam.AccessibleName = "";
            this.dateNam.BackColor = System.Drawing.SystemColors.Window;
            this.dateNam.CustomFormat = "yyyy";
            this.dateNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateNam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNam.HoverColor = System.Drawing.Color.Yellow;
            this.dateNam.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNam.LeaveColor = System.Drawing.Color.White;
            this.dateNam.Location = new System.Drawing.Point(179, 10);
            this.dateNam.Name = "dateNam";
            this.dateNam.Size = new System.Drawing.Size(105, 20);
            this.dateNam.TabIndex = 1;
            // 
            // dateThang1
            // 
            this.dateThang1.AccessibleName = "";
            this.dateThang1.BackColor = System.Drawing.SystemColors.Window;
            this.dateThang1.CustomFormat = "MM";
            this.dateThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateThang1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateThang1.HoverColor = System.Drawing.Color.Yellow;
            this.dateThang1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateThang1.LeaveColor = System.Drawing.Color.White;
            this.dateThang1.Location = new System.Drawing.Point(179, 40);
            this.dateThang1.Name = "dateThang1";
            this.dateThang1.Size = new System.Drawing.Size(105, 20);
            this.dateThang1.TabIndex = 3;
            // 
            // dateThang2
            // 
            this.dateThang2.AccessibleName = "";
            this.dateThang2.BackColor = System.Drawing.SystemColors.Window;
            this.dateThang2.CustomFormat = "MM";
            this.dateThang2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateThang2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dateThang2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateThang2.HoverColor = System.Drawing.Color.Yellow;
            this.dateThang2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateThang2.LeaveColor = System.Drawing.Color.White;
            this.dateThang2.Location = new System.Drawing.Point(179, 67);
            this.dateThang2.Name = "dateThang2";
            this.dateThang2.Size = new System.Drawing.Size(105, 20);
            this.dateThang2.TabIndex = 5;
            // 
            // AGLCTPB_F4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateThang2);
            this.Controls.Add(this.dateThang1);
            this.Controls.Add(this.dateNam);
            this.Controls.Add(this.txtMaDvcs);
            this.Controls.Add(this.v6Label3);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Name = "AGLCTPB_F4";
            this.Size = new System.Drawing.Size(559, 240);
            
            this.Load += new System.EventHandler(this.FormBaoCaoHangTonTheoKho_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcel;
        private System.Windows.Forms.ToolStripMenuItem printGrid;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        private V6Controls.V6Label v6Label1;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label3;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6Controls.V6DateTimePick dateNam;
        private V6Controls.V6DateTimePick dateThang1;
        private V6Controls.V6DateTimePick dateThang2;




    }
}