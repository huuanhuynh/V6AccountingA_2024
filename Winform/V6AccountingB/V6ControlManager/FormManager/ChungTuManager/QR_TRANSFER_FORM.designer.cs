using V6Controls;

namespace V6ControlManager.FormManager.ChungTuManager
{
    partial class QR_TRANSFER_FORM
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QR_TRANSFER_FORM));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbMode = new System.Windows.Forms.GroupBox();
            this.rScan = new System.Windows.Forms.RadioButton();
            this.rInventory = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoadTemp = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtQR_INFOR = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.panel1.SuspendLayout();
            this.grbMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 111;
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.Image = global::V6ControlManager.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 560);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Tag = "Escape";
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 560);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 1;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grbMode);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnLoadTemp);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.txtQR_INFOR);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 552);
            this.panel1.TabIndex = 0;
            // 
            // grbMode
            // 
            this.grbMode.AccessibleDescription = "";
            this.grbMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbMode.Controls.Add(this.rScan);
            this.grbMode.Controls.Add(this.rInventory);
            this.grbMode.Location = new System.Drawing.Point(10, 509);
            this.grbMode.Name = "grbMode";
            this.grbMode.Size = new System.Drawing.Size(194, 35);
            this.grbMode.TabIndex = 4;
            this.grbMode.TabStop = false;
            this.grbMode.Text = "Mode";
            // 
            // rScan
            // 
            this.rScan.AutoSize = true;
            this.rScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rScan.Location = new System.Drawing.Point(83, 13);
            this.rScan.Name = "rScan";
            this.rScan.Size = new System.Drawing.Size(50, 17);
            this.rScan.TabIndex = 1;
            this.rScan.Text = "&Scan";
            this.rScan.UseVisualStyleBackColor = true;
            // 
            // rInventory
            // 
            this.rInventory.AutoSize = true;
            this.rInventory.Checked = true;
            this.rInventory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rInventory.Location = new System.Drawing.Point(6, 13);
            this.rInventory.Name = "rInventory";
            this.rInventory.Size = new System.Drawing.Size(69, 17);
            this.rInventory.TabIndex = 0;
            this.rInventory.TabStop = true;
            this.rInventory.Text = "&Inventory";
            this.rInventory.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.AccessibleDescription = "";
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Image = global::V6ControlManager.Properties.Resources.Delete32;
            this.btnClear.Location = new System.Drawing.Point(104, 463);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 40);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Xóa";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLoadTemp
            // 
            this.btnLoadTemp.AccessibleDescription = "";
            this.btnLoadTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadTemp.Image = global::V6ControlManager.Properties.Resources.History24;
            this.btnLoadTemp.Location = new System.Drawing.Point(304, 463);
            this.btnLoadTemp.Name = "btnLoadTemp";
            this.btnLoadTemp.Size = new System.Drawing.Size(88, 40);
            this.btnLoadTemp.TabIndex = 1;
            this.btnLoadTemp.Text = "&Lịch sử";
            this.btnLoadTemp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadTemp.UseVisualStyleBackColor = true;
            this.btnLoadTemp.Click += new System.EventHandler(this.btnLoadTemp_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.AccessibleDescription = "";
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Image = global::V6ControlManager.Properties.Resources.QRicon32;
            this.btnLoad.Location = new System.Drawing.Point(10, 463);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 40);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "&Thêm";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtQR_INFOR
            // 
            this.txtQR_INFOR.AcceptsTab = true;
            this.txtQR_INFOR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQR_INFOR.BackColor = System.Drawing.SystemColors.Info;
            this.txtQR_INFOR.Location = new System.Drawing.Point(3, 3);
            this.txtQR_INFOR.Name = "txtQR_INFOR";
            this.txtQR_INFOR.Size = new System.Drawing.Size(389, 456);
            this.txtQR_INFOR.TabIndex = 0;
            this.txtQR_INFOR.Text = "";
            this.txtQR_INFOR.Enter += new System.EventHandler(this.txtQR_INFOR_Enter);
            this.txtQR_INFOR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQR_INFOR_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(398, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(605, 552);
            this.dataGridView1.TabIndex = 3;
            // 
            // QR_TRANSFER_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 606);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "QR_TRANSFER_FORM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR_TRANSFER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectMultiIDForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.SizeChanged += new System.EventHandler(this.QR_TRANSFER_SOA_FORM_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.grbMode.ResumeLayout(false);
            this.grbMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Panel panel1;
        public V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoad;
        public System.Windows.Forms.RichTextBox txtQR_INFOR;
        private System.Windows.Forms.GroupBox grbMode;
        private System.Windows.Forms.RadioButton rScan;
        private System.Windows.Forms.RadioButton rInventory;
        private System.Windows.Forms.Button btnLoadTemp;
    }
}