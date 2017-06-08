namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    partial class AldmvtFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AldmvtFilterForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMaSp = new V6Controls.V6VvarTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNhomVt1 = new V6Controls.V6VvarTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNhomVt2 = new V6Controls.V6VvarTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNhomVt3 = new V6Controls.V6VvarTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "huy";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "loc";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(12, 128);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 32);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "&Nhận";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNhomVt3);
            this.panel1.Controls.Add(this.txtNhomVt2);
            this.panel1.Controls.Add(this.txtNhomVt1);
            this.panel1.Controls.Add(this.txtMaSp);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 121);
            this.panel1.TabIndex = 0;
            // 
            // txtMaSp
            // 
            this.txtMaSp.AccessibleName = "MA_SP";
            this.txtMaSp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaSp.BackColor = System.Drawing.Color.White;
            this.txtMaSp.BrotherFields = null;
            this.txtMaSp.Carry = false;
            this.txtMaSp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaSp.EnableColorEffect = true;
            this.txtMaSp.EnableColorEffectOnMouseEnter = false;
            this.txtMaSp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMaSp.HoverColor = System.Drawing.Color.Yellow;
            this.txtMaSp.LeaveColor = System.Drawing.Color.White;
            this.txtMaSp.LimitCharacters = null;
            this.txtMaSp.Location = new System.Drawing.Point(111, 9);
            this.txtMaSp.Name = "txtMaSp";
            this.txtMaSp.Size = new System.Drawing.Size(202, 20);
            this.txtMaSp.TabIndex = 1;
            this.txtMaSp.GrayText = "";
            this.txtMaSp.VVar = "MA_VT";
            // 
            // label1
            // 
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sản phẩm";
            // 
            // txtNhomVt1
            // 
            this.txtNhomVt1.AccessibleName = "NH_VT1";
            this.txtNhomVt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVt1.BackColor = System.Drawing.Color.White;
            this.txtNhomVt1.BrotherFields = null;
            this.txtNhomVt1.Carry = false;
            this.txtNhomVt1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVt1.EnableColorEffect = true;
            this.txtNhomVt1.EnableColorEffectOnMouseEnter = false;
            this.txtNhomVt1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVt1.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVt1.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVt1.LimitCharacters = null;
            this.txtNhomVt1.Location = new System.Drawing.Point(111, 35);
            this.txtNhomVt1.Name = "txtNhomVt1";
            this.txtNhomVt1.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVt1.TabIndex = 3;
            this.txtNhomVt1.GrayText = "";
            this.txtNhomVt1.VVar = "NH_VT";
            // 
            // label2
            // 
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhóm sản phẩm 1";
            // 
            // txtNhomVt2
            // 
            this.txtNhomVt2.AccessibleName = "NH_VT2";
            this.txtNhomVt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVt2.BackColor = System.Drawing.Color.White;
            this.txtNhomVt2.BrotherFields = null;
            this.txtNhomVt2.Carry = false;
            this.txtNhomVt2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVt2.EnableColorEffect = true;
            this.txtNhomVt2.EnableColorEffectOnMouseEnter = false;
            this.txtNhomVt2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVt2.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVt2.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVt2.LimitCharacters = null;
            this.txtNhomVt2.Location = new System.Drawing.Point(111, 61);
            this.txtNhomVt2.Name = "txtNhomVt2";
            this.txtNhomVt2.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVt2.TabIndex = 5;
            this.txtNhomVt2.GrayText = "";
            this.txtNhomVt2.VVar = "NH_VT";
            // 
            // label3
            // 
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhóm sản phẩm 2";
            // 
            // txtNhomVt3
            // 
            this.txtNhomVt3.AccessibleName = "NH_VT3";
            this.txtNhomVt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhomVt3.BackColor = System.Drawing.Color.White;
            this.txtNhomVt3.BrotherFields = null;
            this.txtNhomVt3.Carry = false;
            this.txtNhomVt3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNhomVt3.EnableColorEffect = true;
            this.txtNhomVt3.EnableColorEffectOnMouseEnter = false;
            this.txtNhomVt3.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNhomVt3.HoverColor = System.Drawing.Color.Yellow;
            this.txtNhomVt3.LeaveColor = System.Drawing.Color.White;
            this.txtNhomVt3.LimitCharacters = null;
            this.txtNhomVt3.Location = new System.Drawing.Point(111, 87);
            this.txtNhomVt3.Name = "txtNhomVt3";
            this.txtNhomVt3.Size = new System.Drawing.Size(202, 20);
            this.txtNhomVt3.TabIndex = 7;
            this.txtNhomVt3.GrayText = "";
            this.txtNhomVt3.VVar = "NH_VT";
            // 
            // label4
            // 
            this.label4.AccessibleName = "";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nhóm sản phẩm 3";
            // 
            // AldmvtFilterForm
            // 
            this.AccessibleDescription = "filter_form";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 163);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "AldmvtFilterForm";
            this.ShowInTaskbar = false;
            this.Text = "Lọc";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private V6Controls.V6VvarTextBox txtMaSp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private V6Controls.V6VvarTextBox txtNhomVt3;
        private V6Controls.V6VvarTextBox txtNhomVt2;
        private V6Controls.V6VvarTextBox txtNhomVt1;
    }
}