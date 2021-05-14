namespace V6ControlManager.FormManager.ChungTuManager
{
    partial class ChucNangThayTheForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radDaoNguoc = new System.Windows.Forms.RadioButton();
            this.radThayThe = new System.Windows.Forms.RadioButton();
            this.textBox1 = new V6Controls.V6ColorTextBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = "XULYL00122";
            this.groupBox1.Controls.Add(this.radDaoNguoc);
            this.groupBox1.Controls.Add(this.radThayThe);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 44);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // radDaoNguoc
            // 
            this.radDaoNguoc.AccessibleDescription = "XULYC00013";
            this.radDaoNguoc.AutoSize = true;
            this.radDaoNguoc.Location = new System.Drawing.Point(109, 20);
            this.radDaoNguoc.Name = "radDaoNguoc";
            this.radDaoNguoc.Size = new System.Drawing.Size(78, 17);
            this.radDaoNguoc.TabIndex = 0;
            this.radDaoNguoc.TabStop = true;
            this.radDaoNguoc.Text = "Đảo ngược";
            this.radDaoNguoc.UseVisualStyleBackColor = true;
            this.radDaoNguoc.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radThayThe
            // 
            this.radThayThe.AccessibleDescription = "XULYC00012";
            this.radThayThe.AutoSize = true;
            this.radThayThe.Location = new System.Drawing.Point(6, 19);
            this.radThayThe.Name = "radThayThe";
            this.radThayThe.Size = new System.Drawing.Size(67, 17);
            this.radThayThe.TabIndex = 0;
            this.radThayThe.TabStop = true;
            this.radThayThe.Text = "Thay thế";
            this.radThayThe.UseVisualStyleBackColor = true;
            this.radThayThe.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.textBox1.EnterColor = System.Drawing.Color.PaleGreen;
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.textBox1.HoverColor = System.Drawing.Color.Yellow;
            this.textBox1.LeaveColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(18, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 20);
            this.textBox1.TabIndex = 6;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(106, 131);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 17;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 131);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 16;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "XULYL00198";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nhập giá trị mới";
            // 
            // ChucNangThayTheForm
            // 
            this.AccessibleDescription = "ASOCTSOAF00002";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 183);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChucNangThayTheForm";
            this.Text = "Thay thế";
            
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radDaoNguoc;
        private System.Windows.Forms.RadioButton radThayThe;
        private V6Controls.V6ColorTextBox textBox1;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Label label1;
    }
}