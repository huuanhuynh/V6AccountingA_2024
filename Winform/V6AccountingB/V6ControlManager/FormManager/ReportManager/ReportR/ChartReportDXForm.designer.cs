namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    partial class ChartReportDXForm
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
            this.cboLoaiReport = new V6Controls.V6ComboBox();
            this.cboLoaiBieuDo = new V6Controls.V6ComboBox();
            this.v6Label2 = new V6Controls.V6Label();
            this.v6Label1 = new V6Controls.V6Label();
            this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.btnSuaMau = new V6Controls.Controls.V6FormButton();
            this.SuspendLayout();
            // 
            // cboLoaiReport
            // 
            this.cboLoaiReport.AccessibleName = "";
            this.cboLoaiReport.BackColor = System.Drawing.SystemColors.Window;
            this.cboLoaiReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiReport.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLoaiReport.FormattingEnabled = true;
            this.cboLoaiReport.Location = new System.Drawing.Point(102, 12);
            this.cboLoaiReport.Name = "cboLoaiReport";
            this.cboLoaiReport.Size = new System.Drawing.Size(170, 21);
            this.cboLoaiReport.TabIndex = 21;
            this.cboLoaiReport.SelectedIndexChanged += new System.EventHandler(this.cboLoaiReport_SelectedIndexChanged);
            // 
            // cboLoaiBieuDo
            // 
            this.cboLoaiBieuDo.AccessibleName = "";
            this.cboLoaiBieuDo.BackColor = System.Drawing.SystemColors.Window;
            this.cboLoaiBieuDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiBieuDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiBieuDo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboLoaiBieuDo.FormattingEnabled = true;
            this.cboLoaiBieuDo.Location = new System.Drawing.Point(468, 12);
            this.cboLoaiBieuDo.Name = "cboLoaiBieuDo";
            this.cboLoaiBieuDo.Size = new System.Drawing.Size(170, 21);
            this.cboLoaiBieuDo.TabIndex = 21;
            this.cboLoaiBieuDo.SelectedIndexChanged += new System.EventHandler(this.cboLoaiBieuDo_SelectedIndexChanged);
            // 
            // v6Label2
            // 
            this.v6Label2.AccessibleDescription = "REPORTL00012";
            this.v6Label2.AutoSize = true;
            this.v6Label2.Location = new System.Drawing.Point(12, 15);
            this.v6Label2.Name = "v6Label2";
            this.v6Label2.Size = new System.Drawing.Size(69, 13);
            this.v6Label2.TabIndex = 22;
            this.v6Label2.Text = "Loại báo cáo";
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "REPORTL00013";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(392, 15);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(66, 13);
            this.v6Label1.TabIndex = 22;
            this.v6Label1.Text = "Loại biểu đồ";
            // 
            // documentViewer1
            // 
            this.documentViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentViewer1.IsMetric = false;
            this.documentViewer1.Location = new System.Drawing.Point(0, 53);
            this.documentViewer1.LookAndFeel.SkinName = "Seven Classic";
            this.documentViewer1.Name = "documentViewer1";
            this.documentViewer1.RequestDocumentCreation = false;
            this.documentViewer1.Size = new System.Drawing.Size(784, 479);
            this.documentViewer1.TabIndex = 23;
            // 
            // btnSuaMau
            // 
            this.btnSuaMau.AccessibleDescription = "REPORTB00003";
            this.btnSuaMau.Location = new System.Drawing.Point(644, 10);
            this.btnSuaMau.Name = "btnSuaMau";
            this.btnSuaMau.Size = new System.Drawing.Size(39, 23);
            this.btnSuaMau.TabIndex = 24;
            this.btnSuaMau.Text = "Sửa";
            this.btnSuaMau.UseVisualStyleBackColor = true;
            this.btnSuaMau.Click += new System.EventHandler(this.btnSuaMau_Click);
            // 
            // ChartReportDXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 532);
            this.Controls.Add(this.btnSuaMau);
            this.Controls.Add(this.documentViewer1);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.v6Label2);
            this.Controls.Add(this.cboLoaiBieuDo);
            this.Controls.Add(this.cboLoaiReport);
            this.FilterType = "4";
            this.Name = "ChartReportDXForm";
            this.Text = "ChartReportDXForm";
            this.Load += new System.EventHandler(this.ChartReportDXForm_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.cboLoaiReport, 0);
            this.Controls.SetChildIndex(this.cboLoaiBieuDo, 0);
            this.Controls.SetChildIndex(this.v6Label2, 0);
            this.Controls.SetChildIndex(this.v6Label1, 0);
            this.Controls.SetChildIndex(this.documentViewer1, 0);
            this.Controls.SetChildIndex(this.btnSuaMau, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6ComboBox cboLoaiReport;
        private V6Controls.V6ComboBox cboLoaiBieuDo;
        private V6Controls.V6Label v6Label2;
        private V6Controls.V6Label v6Label1;
        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
        private V6Controls.Controls.V6FormButton btnSuaMau;
    }
}