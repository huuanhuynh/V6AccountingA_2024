namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class Valcc01AddEditForm
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
            this.dateNgayThoiPB = new V6Controls.V6DateTimePicker();
            this.txtMaCC = new V6Controls.V6VvarTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dateNgayThoiPB);
            this.groupBox1.Controls.Add(this.txtMaCC);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(698, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dateNgayThoiPB
            // 
            this.dateNgayThoiPB.AccessibleName = "ngay_pb1";
            this.dateNgayThoiPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgayThoiPB.BackColor = System.Drawing.Color.White;
            this.dateNgayThoiPB.CustomFormat = "dd/MM/yyyy";
            this.dateNgayThoiPB.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayThoiPB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayThoiPB.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayThoiPB.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayThoiPB.LeaveColor = System.Drawing.Color.White;
            this.dateNgayThoiPB.Location = new System.Drawing.Point(171, 55);
            this.dateNgayThoiPB.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayThoiPB.Name = "dateNgayThoiPB";
            this.dateNgayThoiPB.Size = new System.Drawing.Size(135, 23);
            this.dateNgayThoiPB.TabIndex = 5;
            // 
            // txtMaCC
            // 
            this.txtMaCC.AccessibleName = "SO_THE_CC";
            this.txtMaCC.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaCC.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMaCC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaCC.CheckNotEmpty = true;
            this.txtMaCC.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaCC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaCC.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMaCC.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaCC.LeaveColor = System.Drawing.Color.White;
            this.txtMaCC.Location = new System.Drawing.Point(171, 25);
            this.txtMaCC.Name = "txtMaCC";
            this.txtMaCC.Size = new System.Drawing.Size(135, 23);
            this.txtMaCC.TabIndex = 1;
            this.txtMaCC.VVar = "so_the_cc";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = "ADDEDITL00387";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày thôi phân bổ";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ADDEDITL00386";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã công cụ";
            // 
            // Valcc01AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Valcc01AddEditForm";
            this.Size = new System.Drawing.Size(712, 197);
            this.Load += new System.EventHandler(this.Algia2AddEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private V6VvarTextBox txtMaCC;
        private V6DateTimePicker dateNgayThoiPB;
    }
}
