namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    partial class AINVITRI03_REPORT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.lineMaKho = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaVatTu = new V6ReportControls.FilterLineVvarTextBox();
            this.lineMaVitri = new V6ReportControls.FilterLineVvarTextBox();
            this.txtMaKh = new V6Controls.V6VvarTextBox();
            this.v6Label10 = new V6Controls.V6Label();
            this.dateYear = new V6Controls.V6DateTimePick();
            this.dateMonth = new V6Controls.V6DateTimePick();
            this.txtma_dvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateYear);
            this.panel1.Controls.Add(this.dateMonth);
            this.panel1.Controls.Add(this.txtMaKh);
            this.panel1.Controls.Add(this.v6Label10);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "XULYL00056";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tháng";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "XULYL00009";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Năm";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(289, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(262, 343);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "FILTERG00001";
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtma_dvcs);
            this.groupBox1.Controls.Add(this.radOr);
            this.groupBox1.Controls.Add(this.radAnd);
            this.groupBox1.Controls.Add(this.lineMaKho);
            this.groupBox1.Controls.Add(this.lineMaVatTu);
            this.groupBox1.Controls.Add(this.lineMaVitri);
            this.groupBox1.Location = new System.Drawing.Point(3, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 255);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // radOr
            // 
            this.radOr.AccessibleDescription = "FILTERR00002";
            this.radOr.AutoSize = true;
            this.radOr.Location = new System.Drawing.Point(142, 16);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(95, 17);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "Điều kiện (OR)";
            this.radOr.UseVisualStyleBackColor = true;
            // 
            // radAnd
            // 
            this.radAnd.AccessibleDescription = "FILTERR00001";
            this.radAnd.AutoSize = true;
            this.radAnd.Checked = true;
            this.radAnd.Location = new System.Drawing.Point(6, 16);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(102, 17);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "Điều kiện (AND)";
            this.radAnd.UseVisualStyleBackColor = true;
            // 
            // lineMaKho
            // 
            this.lineMaKho.AccessibleDescription = "XULYL00078";
            this.lineMaKho.FieldCaption = "Mã kho";
            this.lineMaKho.FieldName = "MA_KHO";
            this.lineMaKho.Location = new System.Drawing.Point(6, 42);
            this.lineMaKho.Name = "lineMaKho";
            this.lineMaKho.Size = new System.Drawing.Size(268, 22);
            this.lineMaKho.TabIndex = 2;
            this.lineMaKho.Vvar = "MA_KHO";
            // 
            // lineMaVatTu
            // 
            this.lineMaVatTu.AccessibleDescription = "XULYL00095";
            this.lineMaVatTu.FieldCaption = "Mã vật tư";
            this.lineMaVatTu.FieldName = "MA_VT";
            this.lineMaVatTu.Location = new System.Drawing.Point(6, 88);
            this.lineMaVatTu.Name = "lineMaVatTu";
            this.lineMaVatTu.Size = new System.Drawing.Size(268, 22);
            this.lineMaVatTu.TabIndex = 3;
            this.lineMaVatTu.Vvar = "";
            // 
            // lineMaVitri
            // 
            this.lineMaVitri.AccessibleDescription = "XULYL00094";
            this.lineMaVitri.FieldCaption = "Mã vị trí";
            this.lineMaVitri.FieldName = "MA_VITRI";
            this.lineMaVitri.Location = new System.Drawing.Point(6, 65);
            this.lineMaVitri.Name = "lineMaVitri";
            this.lineMaVitri.Size = new System.Drawing.Size(268, 22);
            this.lineMaVitri.TabIndex = 3;
            this.lineMaVitri.Vvar = "";
            // 
            // txtMaKh
            // 
            this.txtMaKh.AccessibleName = "ma_kh";
            this.txtMaKh.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtMaKh.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaKh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaKh.BrotherFields = "";
            this.txtMaKh.CheckNotEmpty = true;
            this.txtMaKh.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaKh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaKh.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaKh.LeaveColor = System.Drawing.Color.White;
            this.txtMaKh.Location = new System.Drawing.Point(131, 61);
            this.txtMaKh.Name = "txtMaKh";
            this.txtMaKh.ReadOnly = true;
            this.txtMaKh.Size = new System.Drawing.Size(100, 20);
            this.txtMaKh.TabIndex = 8;
            this.txtMaKh.VVar = "ma_kh";
            // 
            // v6Label10
            // 
            this.v6Label10.AccessibleDescription = "XULYL00041";
            this.v6Label10.AutoSize = true;
            this.v6Label10.Location = new System.Drawing.Point(3, 63);
            this.v6Label10.Name = "v6Label10";
            this.v6Label10.Size = new System.Drawing.Size(82, 13);
            this.v6Label10.TabIndex = 7;
            this.v6Label10.Text = "Mã khách hàng";
            // 
            // dateYear
            // 
            this.dateYear.CustomFormat = "yyyy";
            this.dateYear.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateYear.HoverColor = System.Drawing.Color.Yellow;
            this.dateYear.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateYear.LeaveColor = System.Drawing.Color.White;
            this.dateYear.Location = new System.Drawing.Point(131, 5);
            this.dateYear.Name = "dateYear";
            this.dateYear.Size = new System.Drawing.Size(47, 20);
            this.dateYear.TabIndex = 26;
            // 
            // dateMonth
            // 
            this.dateMonth.CustomFormat = "MM";
            this.dateMonth.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateMonth.HoverColor = System.Drawing.Color.Yellow;
            this.dateMonth.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateMonth.LeaveColor = System.Drawing.Color.White;
            this.dateMonth.Location = new System.Drawing.Point(131, 31);
            this.dateMonth.Name = "dateMonth";
            this.dateMonth.Size = new System.Drawing.Size(47, 20);
            this.dateMonth.TabIndex = 25;
            // 
            // txtma_dvcs
            // 
            this.txtma_dvcs.AccessibleDescription = "XULYL00078";
            this.txtma_dvcs.FieldCaption = "Mã đvcs";
            this.txtma_dvcs.FieldName = "MA_DVCS";
            this.txtma_dvcs.Location = new System.Drawing.Point(6, 111);
            this.txtma_dvcs.Name = "txtma_dvcs";
            this.txtma_dvcs.Size = new System.Drawing.Size(268, 22);
            this.txtma_dvcs.TabIndex = 4;
            this.txtma_dvcs.Vvar = "MA_DVCS";
            // 
            // AINVITRI03_REPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AINVITRI03_REPORT";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        protected V6Controls.V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private V6ReportControls.FilterLineVvarTextBox lineMaKho;
        private V6ReportControls.FilterLineVvarTextBox lineMaVitri;
        private V6Controls.V6VvarTextBox txtMaKh;
        private V6Controls.V6Label v6Label10;
        private V6Controls.V6DateTimePick dateYear;
        private V6Controls.V6DateTimePick dateMonth;
        private V6ReportControls.FilterLineVvarTextBox lineMaVatTu;
        private V6ReportControls.FilterLineVvarTextBox txtma_dvcs;
    }
}
