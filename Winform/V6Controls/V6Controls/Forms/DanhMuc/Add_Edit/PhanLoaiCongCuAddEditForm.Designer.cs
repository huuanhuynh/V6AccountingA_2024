namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    partial class PhanLoaiCongCuAddEditForm
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
            this.checkBox1 = new V6Controls.V6CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.v6ColorTextBox5 = new V6Controls.V6ColorTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTen_loai = new V6Controls.V6ColorTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txtma_loai = new V6Controls.V6VvarTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Txtma_loai);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.v6ColorTextBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTen_loai);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.groupBox1.Size = new System.Drawing.Size(687, 186);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleName = "status";
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(236, 117);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 21);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Có sử dụng ?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Trạng Thái";
            // 
            // v6ColorTextBox5
            // 
            this.v6ColorTextBox5.AccessibleName = "ten_loai2";
            this.v6ColorTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.v6ColorTextBox5.Carry = false;
            this.v6ColorTextBox5.EnableColorEffect = true;
            this.v6ColorTextBox5.EnableColorEffectOnMouseEnter = false;
            this.v6ColorTextBox5.EnterColor = System.Drawing.Color.PaleGreen;
            this.v6ColorTextBox5.HoverColor = System.Drawing.Color.Yellow;
            this.v6ColorTextBox5.LeaveColor = System.Drawing.Color.White;
            this.v6ColorTextBox5.LimitCharacters = null;
            this.v6ColorTextBox5.Location = new System.Drawing.Point(236, 85);
            this.v6ColorTextBox5.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.v6ColorTextBox5.Name = "v6ColorTextBox5";
            this.v6ColorTextBox5.Size = new System.Drawing.Size(363, 23);
            this.v6ColorTextBox5.TabIndex = 2;
            this.v6ColorTextBox5.GrayText = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 88);
            this.label5.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã loại";
            // 
            // txtTen_loai
            // 
            this.txtTen_loai.AccessibleName = "ten_loai";
            this.txtTen_loai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTen_loai.Carry = false;
            this.txtTen_loai.EnableColorEffect = true;
            this.txtTen_loai.EnableColorEffectOnMouseEnter = false;
            this.txtTen_loai.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtTen_loai.HoverColor = System.Drawing.Color.Yellow;
            this.txtTen_loai.LeaveColor = System.Drawing.Color.White;
            this.txtTen_loai.LimitCharacters = null;
            this.txtTen_loai.Location = new System.Drawing.Point(236, 55);
            this.txtTen_loai.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.txtTen_loai.Name = "txtTen_loai";
            this.txtTen_loai.Size = new System.Drawing.Size(363, 23);
            this.txtTen_loai.TabIndex = 1;
            this.txtTen_loai.GrayText = "";
            // 
            // label2
            // 
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên loại";
            // 
            // Txtma_loai
            // 
            this.Txtma_loai.AccessibleName = "ma_loai";
            this.Txtma_loai.BrotherFields = null;
            this.Txtma_loai.Carry = false;
            this.Txtma_loai.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Txtma_loai.EnableColorEffect = true;
            this.Txtma_loai.EnableColorEffectOnMouseEnter = false;
            this.Txtma_loai.EnterColor = System.Drawing.Color.PaleGreen;
            this.Txtma_loai.HoverColor = System.Drawing.Color.Yellow;
            this.Txtma_loai.LeaveColor = System.Drawing.Color.White;
            this.Txtma_loai.LimitCharacters = null;
            this.Txtma_loai.Location = new System.Drawing.Point(236, 25);
            this.Txtma_loai.Name = "Txtma_loai";
            this.Txtma_loai.Size = new System.Drawing.Size(147, 23);
            this.Txtma_loai.TabIndex = 0;
            this.Txtma_loai.GrayText = "";
            // 
            // PhanLoaiTaiSanAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PhanLoaiTaiSanAddEditForm";
            this.Size = new System.Drawing.Size(696, 186);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private V6Controls.V6CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private V6ColorTextBox v6ColorTextBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private V6ColorTextBox txtTen_loai;
        private System.Windows.Forms.Label label2;
        private V6VvarTextBox Txtma_loai;
    }
}
