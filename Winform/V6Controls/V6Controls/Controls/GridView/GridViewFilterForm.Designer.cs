namespace V6Controls.Controls.GridView
{
    partial class GridViewFilterForm
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
            this.comboBox1 = new V6Controls.V6ComboBox();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.txtValue = new V6Controls.V6VvarTextBox();
            this.numValue = new V6Controls.V6NumberTextBox();
            this.numValue2 = new V6Controls.V6NumberTextBox();
            this.dateValue = new V6Controls.V6DateTimePicker();
            this.dateValue2 = new V6Controls.V6DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.chkFindNext = new System.Windows.Forms.CheckBox();
            this.chkFindOr = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "=",
            "like",
            "<>",
            ">",
            ">=",
            "<",
            "<=",
            "is null",
            "is not null"});
            this.comboBox1.Location = new System.Drawing.Point(161, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(44, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.TabStop = false;
            this.comboBox1.Visible = false;
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleDescription = "SEARCHB00003";
            this.btnThoat.AccessibleName = "";
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Image = global::V6Controls.Properties.Resources.Cancel;
            this.btnThoat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnThoat.Location = new System.Drawing.Point(102, 69);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(90, 40);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "&Hủy";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "SEARCHB00002";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 69);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(90, 40);
            this.btnNhan.TabIndex = 9;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // txtValue
            // 
            this.txtValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtValue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtValue.CheckOnLeave = false;
            this.txtValue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtValue.F2 = true;
            this.txtValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtValue.HoverColor = System.Drawing.Color.Yellow;
            this.txtValue.LeaveColor = System.Drawing.Color.White;
            this.txtValue.Location = new System.Drawing.Point(212, 12);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(136, 20);
            this.txtValue.TabIndex = 3;
            this.txtValue.Visible = false;
            // 
            // numValue
            // 
            this.numValue.BackColor = System.Drawing.SystemColors.Window;
            this.numValue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.numValue.DecimalPlaces = 2;
            this.numValue.EnterColor = System.Drawing.Color.PaleGreen;
            this.numValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numValue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.numValue.HoverColor = System.Drawing.Color.Yellow;
            this.numValue.LeaveColor = System.Drawing.Color.White;
            this.numValue.Location = new System.Drawing.Point(212, 38);
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(136, 20);
            this.numValue.TabIndex = 5;
            this.numValue.Text = "0,00";
            this.numValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numValue.Visible = false;
            // 
            // numValue2
            // 
            this.numValue2.BackColor = System.Drawing.SystemColors.Window;
            this.numValue2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.numValue2.DecimalPlaces = 2;
            this.numValue2.EnterColor = System.Drawing.Color.PaleGreen;
            this.numValue2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numValue2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.numValue2.HoverColor = System.Drawing.Color.Yellow;
            this.numValue2.LeaveColor = System.Drawing.Color.White;
            this.numValue2.Location = new System.Drawing.Point(400, 38);
            this.numValue2.Name = "numValue2";
            this.numValue2.Size = new System.Drawing.Size(136, 20);
            this.numValue2.TabIndex = 6;
            this.numValue2.Text = "0,00";
            this.numValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numValue2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numValue2.Visible = false;
            // 
            // dateValue
            // 
            this.dateValue.CustomFormat = "dd/MM/yyyy";
            this.dateValue.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue.HoverColor = System.Drawing.Color.Yellow;
            this.dateValue.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateValue.LeaveColor = System.Drawing.Color.White;
            this.dateValue.Location = new System.Drawing.Point(212, 64);
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(136, 20);
            this.dateValue.TabIndex = 7;
            this.dateValue.TextTitle = "";
            this.dateValue.Visible = false;
            // 
            // dateValue2
            // 
            this.dateValue2.CustomFormat = "dd/MM/yyyy";
            this.dateValue2.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateValue2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateValue2.HoverColor = System.Drawing.Color.Yellow;
            this.dateValue2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateValue2.LeaveColor = System.Drawing.Color.White;
            this.dateValue2.Location = new System.Drawing.Point(400, 64);
            this.dateValue2.Name = "dateValue2";
            this.dateValue2.Size = new System.Drawing.Size(136, 20);
            this.dateValue2.TabIndex = 8;
            this.dateValue2.TextTitle = "";
            this.dateValue2.Visible = false;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "V6REASKL00012";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Từ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "V6REASKL00013";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // lblField
            // 
            this.lblField.AccessibleDescription = "V6REASKL00011";
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(12, 15);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(29, 13);
            this.lblField.TabIndex = 0;
            this.lblField.Text = "Field";
            this.lblField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFindNext
            // 
            this.chkFindNext.AccessibleDescription = "V6REASKC00001";
            this.chkFindNext.AutoSize = true;
            this.chkFindNext.Location = new System.Drawing.Point(12, 40);
            this.chkFindNext.Name = "chkFindNext";
            this.chkFindNext.Size = new System.Drawing.Size(96, 17);
            this.chkFindNext.TabIndex = 11;
            this.chkFindNext.Text = "Lọc tiếp (AND)";
            this.chkFindNext.UseVisualStyleBackColor = true;
            this.chkFindNext.CheckedChanged += new System.EventHandler(this.chkFindNext_CheckedChanged);
            // 
            // chkFindOr
            // 
            this.chkFindOr.AccessibleDescription = "V6REASKC00002";
            this.chkFindOr.AutoSize = true;
            this.chkFindOr.Location = new System.Drawing.Point(111, 40);
            this.chkFindOr.Name = "chkFindOr";
            this.chkFindOr.Size = new System.Drawing.Size(95, 17);
            this.chkFindOr.TabIndex = 11;
            this.chkFindOr.Text = "Lọc thêm (OR)";
            this.chkFindOr.UseVisualStyleBackColor = true;
            this.chkFindOr.CheckedChanged += new System.EventHandler(this.chkFindOr_CheckedChanged);
            // 
            // GridViewFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(563, 121);
            this.Controls.Add(this.chkFindOr);
            this.Controls.Add(this.chkFindNext);
            this.Controls.Add(this.lblField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateValue2);
            this.Controls.Add(this.dateValue);
            this.Controls.Add(this.numValue2);
            this.Controls.Add(this.numValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridViewFilterForm";
            this.Text = "GridViewFilterForm";
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnThoat, 0);
            this.Controls.SetChildIndex(this.txtValue, 0);
            this.Controls.SetChildIndex(this.numValue, 0);
            this.Controls.SetChildIndex(this.numValue2, 0);
            this.Controls.SetChildIndex(this.dateValue, 0);
            this.Controls.SetChildIndex(this.dateValue2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblField, 0);
            this.Controls.SetChildIndex(this.chkFindNext, 0);
            this.Controls.SetChildIndex(this.chkFindOr, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected V6ComboBox comboBox1;
        protected System.Windows.Forms.Button btnThoat;
        protected System.Windows.Forms.Button btnNhan;
        private V6VvarTextBox txtValue;
        private V6NumberTextBox numValue;
        private V6NumberTextBox numValue2;
        private V6DateTimePicker dateValue;
        private V6DateTimePicker dateValue2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.CheckBox chkFindNext;
        private System.Windows.Forms.CheckBox chkFindOr;
    }
}