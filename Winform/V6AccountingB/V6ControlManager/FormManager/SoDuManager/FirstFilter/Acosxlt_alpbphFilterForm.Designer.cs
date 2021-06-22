namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    partial class Acosxlt_alpbphFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acosxlt_alpbphFilterForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMa_ytcp = new V6Controls.V6VvarTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMA_BPHT = new V6Controls.V6VvarTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "FILTERB00008";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tag = "Escape";
            this.btnCancel.Text = "Đóng";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "FILTERB00007";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnFilter.Location = new System.Drawing.Point(12, 120);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(88, 40);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Tag = "Return, Control";
            this.btnFilter.Text = "&Nhận";
            this.btnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMa_ytcp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMA_BPHT);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 113);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "FILTERL00256";
            this.label2.AccessibleName = "";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã YTCP";
            // 
            // txtMa_ytcp
            // 
            this.txtMa_ytcp.AccessibleName = "MA_YTCP";
            this.txtMa_ytcp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMa_ytcp.BackColor = System.Drawing.Color.White;
            this.txtMa_ytcp.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMa_ytcp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_ytcp.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMa_ytcp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMa_ytcp.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMa_ytcp.HoverColor = System.Drawing.Color.Yellow;
            this.txtMa_ytcp.LeaveColor = System.Drawing.Color.White;
            this.txtMa_ytcp.Location = new System.Drawing.Point(111, 35);
            this.txtMa_ytcp.Name = "txtMa_ytcp";
            this.txtMa_ytcp.Size = new System.Drawing.Size(202, 20);
            this.txtMa_ytcp.TabIndex = 3;
            this.txtMa_ytcp.VVar = "MA_YTCP ";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00078";
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã BPHT";
            // 
            // txtMA_BPHT
            // 
            this.txtMA_BPHT.AccessibleName = "MA_BPHT";
            this.txtMA_BPHT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMA_BPHT.BackColor = System.Drawing.Color.White;
            this.txtMA_BPHT.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMA_BPHT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMA_BPHT.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMA_BPHT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMA_BPHT.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMA_BPHT.HoverColor = System.Drawing.Color.Yellow;
            this.txtMA_BPHT.LeaveColor = System.Drawing.Color.White;
            this.txtMA_BPHT.Location = new System.Drawing.Point(111, 9);
            this.txtMA_BPHT.Name = "txtMA_BPHT";
            this.txtMA_BPHT.Size = new System.Drawing.Size(202, 20);
            this.txtMA_BPHT.TabIndex = 1;
            this.txtMA_BPHT.VVar = "MA_BPHT";
            // 
            // Acosxlt_alpbphFilterForm
            // 
            this.AccessibleDescription = "FILTERF00001";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "Acosxlt_alpbphFilterForm";
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
        private V6Controls.V6VvarTextBox txtMA_BPHT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private V6Controls.V6VvarTextBox txtMa_ytcp;
    }
}