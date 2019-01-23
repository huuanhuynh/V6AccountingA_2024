namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    partial class PayForm
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
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIn = new System.Windows.Forms.Button();
            this.txtTongTienNt2 = new V6Controls.NumberTienNt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKhachDua = new V6Controls.NumberTienNt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTraLai = new V6Controls.NumberTienNt();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(428, 281);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.Location = new System.Drawing.Point(334, 281);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Lưu";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "ASOCTSOAL00116";
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thanh toán";
            // 
            // btnIn
            // 
            this.btnIn.AccessibleDescription = "REPORTB00006";
            this.btnIn.AccessibleName = "";
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.Image = global::V6ControlManager.Properties.Resources.Print24;
            this.btnIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnIn.Location = new System.Drawing.Point(240, 281);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(88, 40);
            this.btnIn.TabIndex = 2;
            this.btnIn.Tag = "F7";
            this.btnIn.Text = "Lưu và &In";
            this.btnIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // txtTongTienNt2
            // 
            this.txtTongTienNt2.AccessibleDescription = "";
            this.txtTongTienNt2.AccessibleName = "t_tien_nt2";
            this.txtTongTienNt2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTongTienNt2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTongTienNt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTienNt2.DecimalPlaces = 0;
            this.txtTongTienNt2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTongTienNt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTienNt2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTongTienNt2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTongTienNt2.HoverColor = System.Drawing.Color.Yellow;
            this.txtTongTienNt2.LeaveColor = System.Drawing.Color.White;
            this.txtTongTienNt2.Location = new System.Drawing.Point(240, 12);
            this.txtTongTienNt2.Name = "txtTongTienNt2";
            this.txtTongTienNt2.ReadOnly = true;
            this.txtTongTienNt2.Size = new System.Drawing.Size(276, 45);
            this.txtTongTienNt2.TabIndex = 6;
            this.txtTongTienNt2.TabStop = false;
            this.txtTongTienNt2.Tag = "readonly";
            this.txtTongTienNt2.Text = "0";
            this.txtTongTienNt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongTienNt2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "ASOCTSOAL00117";
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Khách đưa";
            // 
            // txtKhachDua
            // 
            this.txtKhachDua.AccessibleDescription = "";
            this.txtKhachDua.AccessibleName = "t_tien_nt2";
            this.txtKhachDua.BackColor = System.Drawing.SystemColors.Window;
            this.txtKhachDua.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtKhachDua.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhachDua.DecimalPlaces = 0;
            this.txtKhachDua.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtKhachDua.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhachDua.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtKhachDua.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtKhachDua.HoverColor = System.Drawing.Color.Yellow;
            this.txtKhachDua.LeaveColor = System.Drawing.Color.White;
            this.txtKhachDua.Location = new System.Drawing.Point(240, 87);
            this.txtKhachDua.Name = "txtKhachDua";
            this.txtKhachDua.Size = new System.Drawing.Size(276, 45);
            this.txtKhachDua.TabIndex = 1;
            this.txtKhachDua.Text = "0";
            this.txtKhachDua.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKhachDua.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKhachDua.StringValueChange += new V6Controls.V6NumberTextBox.StringValueChangeDelegate(this.txtKhachDua_StringValueChange);
            this.txtKhachDua.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKhachDua_KeyDown);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "ASOCTSOAL00118";
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Trả lại";
            // 
            // txtTraLai
            // 
            this.txtTraLai.AccessibleDescription = "";
            this.txtTraLai.AccessibleName = "t_tien_nt2";
            this.txtTraLai.BackColor = System.Drawing.Color.AntiqueWhite;
            this.txtTraLai.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtTraLai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTraLai.DecimalPlaces = 0;
            this.txtTraLai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTraLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTraLai.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTraLai.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtTraLai.HoverColor = System.Drawing.Color.Yellow;
            this.txtTraLai.LeaveColor = System.Drawing.Color.White;
            this.txtTraLai.Location = new System.Drawing.Point(240, 157);
            this.txtTraLai.Name = "txtTraLai";
            this.txtTraLai.ReadOnly = true;
            this.txtTraLai.Size = new System.Drawing.Size(276, 45);
            this.txtTraLai.TabIndex = 8;
            this.txtTraLai.TabStop = false;
            this.txtTraLai.Tag = "readonly";
            this.txtTraLai.Text = "0";
            this.txtTraLai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTraLai.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.AccessibleDescription = ".";
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 29);
            this.button1.TabIndex = 9;
            this.button1.TabStop = false;
            this.button1.Text = "50.000 (F1)";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.AccessibleDescription = ".";
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(106, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 29);
            this.button2.TabIndex = 10;
            this.button2.TabStop = false;
            this.button2.Text = "100.000 (F2)";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AccessibleDescription = ".";
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(3, 118);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 29);
            this.button3.TabIndex = 11;
            this.button3.TabStop = false;
            this.button3.Text = "200.000 (F3)";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.AccessibleDescription = ".";
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(106, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 29);
            this.button4.TabIndex = 12;
            this.button4.TabStop = false;
            this.button4.Text = "500.000 (F4)";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // PayForm
            // 
            this.AccessibleDescription = "ASOCTSOAF00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(528, 333);
            this.Controls.Add(this.txtTraLai);
            this.Controls.Add(this.txtKhachDua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTongTienNt2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PayForm";
            this.Text = "PayForm";
            this.Load += new System.EventHandler(this.PayForm_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnIn, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtTongTienNt2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtKhachDua, 0);
            this.Controls.SetChildIndex(this.txtTraLai, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIn;
        private V6Controls.NumberTienNt txtTongTienNt2;
        private System.Windows.Forms.Label label2;
        private V6Controls.NumberTienNt txtKhachDua;
        private System.Windows.Forms.Label label3;
        private V6Controls.NumberTienNt txtTraLai;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}