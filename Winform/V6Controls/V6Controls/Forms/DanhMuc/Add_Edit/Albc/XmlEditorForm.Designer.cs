namespace V6Controls.Forms.DanhMuc.Add_Edit.Albc
{
    partial class XmlEditorForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnXuatXml = new System.Windows.Forms.Button();
            this.btnNhapXml = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(429, 229);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(91, 234);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(84, 29);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Location = new System.Drawing.Point(1, 234);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(84, 29);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // btnXuatXml
            // 
            this.btnXuatXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXuatXml.Location = new System.Drawing.Point(232, 234);
            this.btnXuatXml.Name = "btnXuatXml";
            this.btnXuatXml.Size = new System.Drawing.Size(84, 29);
            this.btnXuatXml.TabIndex = 3;
            this.btnXuatXml.Text = "&Xuất xml";
            this.btnXuatXml.UseVisualStyleBackColor = true;
            this.btnXuatXml.Click += new System.EventHandler(this.btnXuatXml_Click);
            // 
            // btnNhapXml
            // 
            this.btnNhapXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhapXml.Location = new System.Drawing.Point(322, 234);
            this.btnNhapXml.Name = "btnNhapXml";
            this.btnNhapXml.Size = new System.Drawing.Size(84, 29);
            this.btnNhapXml.TabIndex = 4;
            this.btnNhapXml.Text = "&Nhập xml";
            this.btnNhapXml.UseVisualStyleBackColor = true;
            this.btnNhapXml.Click += new System.EventHandler(this.btnNhapXml_Click);
            // 
            // AlbcExcel2EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 262);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhapXml);
            this.Controls.Add(this.btnXuatXml);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AlbcExcel2EditorForm";
            this.Text = "XmlEditor";
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnXuatXml, 0);
            this.Controls.SetChildIndex(this.btnNhapXml, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Button btnXuatXml;
        private System.Windows.Forms.Button btnNhapXml;
    }
}