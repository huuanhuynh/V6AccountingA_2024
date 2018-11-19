namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class Valts01AddEditForm
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
            this.dateNgayThoiKH = new V6Controls.V6DateTimePicker();
            this.txtMaTS = new V6Controls.V6VvarTextBox();
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
            this.groupBox1.Controls.Add(this.dateNgayThoiKH);
            this.groupBox1.Controls.Add(this.txtMaTS);
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
            // dateNgayThoiKH
            // 
            this.dateNgayThoiKH.AccessibleName = "NGAY_KH1";
            this.dateNgayThoiKH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateNgayThoiKH.BackColor = System.Drawing.Color.White;
            this.dateNgayThoiKH.CustomFormat = "dd/MM/yyyy";
            this.dateNgayThoiKH.EnterColor = System.Drawing.Color.PaleGreen;
            this.dateNgayThoiKH.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNgayThoiKH.HoverColor = System.Drawing.Color.Yellow;
            this.dateNgayThoiKH.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dateNgayThoiKH.LeaveColor = System.Drawing.Color.White;
            this.dateNgayThoiKH.Location = new System.Drawing.Point(171, 54);
            this.dateNgayThoiKH.Margin = new System.Windows.Forms.Padding(5);
            this.dateNgayThoiKH.Name = "dateNgayThoiKH";
            this.dateNgayThoiKH.Size = new System.Drawing.Size(135, 23);
            this.dateNgayThoiKH.TabIndex = 5;
            this.dateNgayThoiKH.TextTitle = null;
            // 
            // txtMaTS
            // 
            this.txtMaTS.AccessibleName = "SO_THE_TS";
            this.txtMaTS.BrotherFields = null;
            this.txtMaTS.Carry = false;
            this.txtMaTS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaTS.CheckNotEmpty = true;
            this.txtMaTS.EnableColorEffect = true;
            this.txtMaTS.EnableColorEffectOnMouseEnter = false;
            this.txtMaTS.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaTS.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaTS.LeaveColor = System.Drawing.Color.White;
            this.txtMaTS.LimitCharacters = null;
            this.txtMaTS.Location = new System.Drawing.Point(171, 25);
            this.txtMaTS.Name = "txtMaTS";
            this.txtMaTS.Size = new System.Drawing.Size(135, 23);
            this.txtMaTS.TabIndex = 1;
            this.txtMaTS.GrayText = "";
            this.txtMaTS.VVar = "so_the_ts";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày thôi khấu hao";
            // 
            // label2
            // 
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã tài sản";
            // 
            // Valts01AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Valts01AddEditForm";
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
        private V6VvarTextBox txtMaTS;
        private V6DateTimePicker dateNgayThoiKH;
    }
}
