namespace V6Controls.Forms.Editor
{
    partial class DicEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DicEditForm));
            this.txtNewKey = new V6Controls.V6ColorTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnTimTiep = new System.Windows.Forms.Button();
            this.btnTimTatCa = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnMove2Up = new System.Windows.Forms.Button();
            this.btnMove2Down = new System.Windows.Forms.Button();
            this.txtValue = new V6Controls.V6ColorTextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtName = new V6Controls.V6ColorTextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.cboKeyWord = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewKey
            // 
            this.txtNewKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewKey.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNewKey.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNewKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewKey.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNewKey.HoverColor = System.Drawing.Color.Yellow;
            this.txtNewKey.LeaveColor = System.Drawing.Color.White;
            this.txtNewKey.Location = new System.Drawing.Point(12, 12);
            this.txtNewKey.Name = "txtNewKey";
            this.txtNewKey.Size = new System.Drawing.Size(158, 20);
            this.txtNewKey.TabIndex = 0;
            this.txtNewKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTim_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 111;
            // 
            // btnTimTiep
            // 
            this.btnTimTiep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimTiep.Location = new System.Drawing.Point(176, 10);
            this.btnTimTiep.Name = "btnTimTiep";
            this.btnTimTiep.Size = new System.Drawing.Size(75, 23);
            this.btnTimTiep.TabIndex = 1;
            this.btnTimTiep.Text = "Tìm tiếp";
            this.btnTimTiep.UseVisualStyleBackColor = true;
            this.btnTimTiep.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnTimTatCa
            // 
            this.btnTimTatCa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimTatCa.Location = new System.Drawing.Point(257, 10);
            this.btnTimTatCa.Name = "btnTimTatCa";
            this.btnTimTatCa.Size = new System.Drawing.Size(75, 23);
            this.btnTimTatCa.TabIndex = 2;
            this.btnTimTatCa.Text = "Tìm giống";
            this.btnTimTatCa.UseVisualStyleBackColor = true;
            this.btnTimTatCa.Click += new System.EventHandler(this.btnTimTatCa_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6Controls.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 472);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 11;
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
            this.btnNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(12, 472);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 10;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnMove2Up
            // 
            this.btnMove2Up.Location = new System.Drawing.Point(257, 38);
            this.btnMove2Up.Name = "btnMove2Up";
            this.btnMove2Up.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Up.TabIndex = 6;
            this.btnMove2Up.Text = "▲";
            this.btnMove2Up.UseVisualStyleBackColor = true;
            this.btnMove2Up.Click += new System.EventHandler(this.btnMove2Up_Click);
            // 
            // btnMove2Down
            // 
            this.btnMove2Down.Location = new System.Drawing.Point(257, 87);
            this.btnMove2Down.Name = "btnMove2Down";
            this.btnMove2Down.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Down.TabIndex = 7;
            this.btnMove2Down.Text = "▼";
            this.btnMove2Down.UseVisualStyleBackColor = true;
            this.btnMove2Down.Click += new System.EventHandler(this.btnMove2Down_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.BackColor = System.Drawing.Color.White;
            this.txtValue.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtValue.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValue.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtValue.HoverColor = System.Drawing.Color.Yellow;
            this.txtValue.LeaveColor = System.Drawing.Color.White;
            this.txtValue.Location = new System.Drawing.Point(298, 74);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue.Size = new System.Drawing.Size(483, 384);
            this.txtValue.TabIndex = 9;
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Location = new System.Drawing.Point(338, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtName.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtName.HoverColor = System.Drawing.Color.Yellow;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(298, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(483, 29);
            this.txtName.TabIndex = 8;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Location = new System.Drawing.Point(419, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
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
            this.dataGridView1.Control_E = true;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(236, 420);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // cboKeyWord
            // 
            this.cboKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKeyWord.DropDownHeight = 200;
            this.cboKeyWord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeyWord.DropDownWidth = 400;
            this.cboKeyWord.FormattingEnabled = true;
            this.cboKeyWord.IntegralHeight = false;
            this.cboKeyWord.Items.AddRange(new object[] {
            "Button",
            "TextBox",
            "RichTextBox",
            "LookupTextBox",
            "V6LookupProc",
            "VvarTextBox",
            "CheckBox",
            "DateTime",
            "DateTimeFull",
            "DateTimeColor",
            "MauBC",
            "MAUALL",
            "DSNS",
            "FileButton",
            "NumberMonth",
            "NumberYear"});
            this.cboKeyWord.Location = new System.Drawing.Point(500, 10);
            this.cboKeyWord.Name = "cboKeyWord";
            this.cboKeyWord.Size = new System.Drawing.Size(251, 21);
            this.cboKeyWord.TabIndex = 12;
            this.cboKeyWord.SelectedIndexChanged += new System.EventHandler(this.cboKeyWord_SelectedIndexChanged);
            // 
            // DicEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 518);
            this.Controls.Add(this.cboKeyWord);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnMove2Down);
            this.Controls.Add(this.btnMove2Up);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnTimTatCa);
            this.Controls.Add(this.btnTimTiep);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtNewKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DicEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sửa từ điển";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectMultiIDForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectMultiIDForm_Load);
            this.SizeChanged += new System.EventHandler(this.DicEditForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.DataGridView dataGridView1;
        private V6ColorTextBox txtNewKey;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimTiep;
        private System.Windows.Forms.Button btnTimTatCa;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Button btnMove2Up;
        private System.Windows.Forms.Button btnMove2Down;
        private V6ColorTextBox txtValue;
        private System.Windows.Forms.Button btnThem;
        private V6ColorTextBox txtName;
        private System.Windows.Forms.Button btnXoa;
        private V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cboKeyWord;
    }
}