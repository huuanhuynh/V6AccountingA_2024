namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class MaGiaAddEditForm
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
            this.TxtMa_gia = new V6Controls.V6VvarTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.v6ColorTextBox4 = new V6Controls.V6ColorTextBox();
            this.v6ColorTextBox3 = new V6Controls.V6ColorTextBox();
            this.txtten_gia = new V6Controls.V6ColorTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Txtma_gia0 = new V6Controls.V6VvarTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPT_GIA = new V6Controls.V6NumberTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtPT_GIA);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Txtma_gia0);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TxtMa_gia);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.v6ColorTextBox4);
            this.groupBox1.Controls.Add(this.v6ColorTextBox3);
            this.groupBox1.Controls.Add(this.txtten_gia);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(585, 264);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // TxtMa_gia
            // 
            this.TxtMa_gia.AccessibleName = "ma_gia";
            this.TxtMa_gia.BrotherFields = null;
            this.TxtMa_gia.Carry = false;
            this.TxtMa_gia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtMa_gia.EnableColorEffect = true;
            this.TxtMa_gia.EnableColorEffectOnMouseEnter = false;
            this.TxtMa_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtMa_gia.HoverColor = System.Drawing.Color.Yellow;
            this.TxtMa_gia.LeaveColor = System.Drawing.Color.White;
            this.TxtMa_gia.LimitCharacters = null;
            this.TxtMa_gia.Location = new System.Drawing.Point(169, 37);
            this.TxtMa_gia.Name = "TxtMa_gia";
            this.TxtMa_gia.Size = new System.Drawing.Size(130, 23);
            this.TxtMa_gia.TabIndex = 0;
            this.TxtMa_gia.GrayText = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 172);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "0 - Không chuẩn, 1- Giá chuẩn";
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 217);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Có sử dụng?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // v6ColorTextBox4
            // 
            this.v6ColorTextBox4.AccessibleName = "Loai";
            this.v6ColorTextBox4.Carry = false;
            this.v6ColorTextBox4.EnableColorEffect = true;
            this.v6ColorTextBox4.EnableColorEffectOnMouseEnter = false;
            this.v6ColorTextBox4.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox4.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox4.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox4.LimitCharacters = "012";
            this.v6ColorTextBox4.Location = new System.Drawing.Point(169, 173);
            this.v6ColorTextBox4.Margin = new System.Windows.Forms.Padding(4);
            this.v6ColorTextBox4.MaxLength = 1;
            this.v6ColorTextBox4.Name = "v6ColorTextBox4";
            this.v6ColorTextBox4.Size = new System.Drawing.Size(52, 23);
            this.v6ColorTextBox4.TabIndex = 5;
            this.v6ColorTextBox4.Text = "0";
            this.v6ColorTextBox4.GrayText = "";
            // 
            // v6ColorTextBox3
            // 
            this.v6ColorTextBox3.Carry = false;
            this.v6ColorTextBox3.EnableColorEffect = true;
            this.v6ColorTextBox3.EnableColorEffectOnMouseEnter = false;
            this.v6ColorTextBox3.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox3.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox3.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox3.LimitCharacters = null;
            this.v6ColorTextBox3.Location = new System.Drawing.Point(169, 95);
            this.v6ColorTextBox3.Margin = new System.Windows.Forms.Padding(4);
            this.v6ColorTextBox3.Name = "v6ColorTextBox3";
            this.v6ColorTextBox3.Size = new System.Drawing.Size(379, 23);
            this.v6ColorTextBox3.TabIndex = 2;
            this.v6ColorTextBox3.GrayText = "";
            // 
            // txtten_gia
            // 
            this.txtten_gia.AccessibleName = "ten_gia";
            this.txtten_gia.Carry = false;
            this.txtten_gia.EnableColorEffect = true;
            this.txtten_gia.EnableColorEffectOnMouseEnter = false;
            this.txtten_gia.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtten_gia.HoverColor = System.Drawing.Color.Yellow;
            this.txtten_gia.LeaveColor = System.Drawing.Color.White;
            this.txtten_gia.LimitCharacters = null;
            this.txtten_gia.Location = new System.Drawing.Point(169, 66);
            this.txtten_gia.Margin = new System.Windows.Forms.Padding(4);
            this.txtten_gia.Name = "txtten_gia";
            this.txtten_gia.Size = new System.Drawing.Size(379, 23);
            this.txtten_gia.TabIndex = 1;
            this.txtten_gia.GrayText = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 217);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Trạng thái";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 173);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Loại giá";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Diễn giải";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã giá ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(261, 195);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "2 - Theo khách ";
            // 
            // Txtma_gia0
            // 
            this.Txtma_gia0.AccessibleName = "MA_GIA0";
            this.Txtma_gia0.BrotherFields = null;
            this.Txtma_gia0.Carry = false;
            this.Txtma_gia0.EnableColorEffect = true;
            this.Txtma_gia0.EnableColorEffectOnMouseEnter = false;
            this.Txtma_gia0.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtma_gia0.HoverColor = System.Drawing.Color.Yellow;
            this.Txtma_gia0.LeaveColor = System.Drawing.Color.White;
            this.Txtma_gia0.LimitCharacters = null;
            this.Txtma_gia0.Location = new System.Drawing.Point(169, 121);
            this.Txtma_gia0.Name = "Txtma_gia0";
            this.Txtma_gia0.Size = new System.Drawing.Size(130, 23);
            this.Txtma_gia0.TabIndex = 3;
            this.Txtma_gia0.GrayText = "";
            this.Txtma_gia0.VVar = "MA_GIA";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 124);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Theo mã giá chuẩn";
            // 
            // txtPT_GIA
            // 
            this.txtPT_GIA.AccessibleName = "PT_GIA";
            this.txtPT_GIA.BackColor = System.Drawing.Color.White;
            this.txtPT_GIA.Carry = false;
            this.txtPT_GIA.EnableColorEffect = true;
            this.txtPT_GIA.EnableColorEffectOnMouseEnter = false;
            this.txtPT_GIA.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtPT_GIA.HoverColor = System.Drawing.Color.Yellow;
            this.txtPT_GIA.LeaveColor = System.Drawing.Color.White;
            this.txtPT_GIA.LimitCharacters = null;
            this.txtPT_GIA.Location = new System.Drawing.Point(419, 121);
            this.txtPT_GIA.MaxNumDecimal = 0;
            this.txtPT_GIA.MaxNumLength = 0;
            this.txtPT_GIA.Name = "txtPT_GIA";
            this.txtPT_GIA.Size = new System.Drawing.Size(129, 23);
            this.txtPT_GIA.TabIndex = 4;
            this.txtPT_GIA.Text = "0,000";
            this.txtPT_GIA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPT_GIA.GrayText = "";
            this.txtPT_GIA.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(324, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "Giảm %";
            // 
            // MaGiaAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MaGiaAddEditForm";
            this.Size = new System.Drawing.Size(611, 285);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private V6Controls.V6CheckBox checkBox1;
        private V6ColorTextBox v6ColorTextBox4;
        private V6ColorTextBox v6ColorTextBox3;
        private V6ColorTextBox txtten_gia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6VvarTextBox TxtMa_gia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private V6VvarTextBox Txtma_gia0;
        private System.Windows.Forms.Label label9;
        private V6NumberTextBox txtPT_GIA;
    }
}
