﻿namespace V6Controls.Forms
{
    partial class FieldSelectorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldSelectorForm));
            this.btnAddSelect = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnRemoveSelect = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnTimTiep = new System.Windows.Forms.Button();
            this.lbldsdc = new System.Windows.Forms.Label();
            this.btnTimTatCa = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnMove2Up = new System.Windows.Forms.Button();
            this.btnMove2Down = new System.Windows.Forms.Button();
            this.dataGridView2 = new V6Controls.V6ColorDataGridView();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddSelect
            // 
            this.btnAddSelect.Location = new System.Drawing.Point(368, 38);
            this.btnAddSelect.Name = "btnAddSelect";
            this.btnAddSelect.Size = new System.Drawing.Size(28, 43);
            this.btnAddSelect.TabIndex = 5;
            this.btnAddSelect.Text = ">";
            this.btnAddSelect.UseVisualStyleBackColor = true;
            this.btnAddSelect.Click += new System.EventHandler(this.btnAddSelect_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(368, 87);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(28, 43);
            this.btnAddAll.TabIndex = 6;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnRemoveSelect
            // 
            this.btnRemoveSelect.Location = new System.Drawing.Point(368, 136);
            this.btnRemoveSelect.Name = "btnRemoveSelect";
            this.btnRemoveSelect.Size = new System.Drawing.Size(28, 43);
            this.btnRemoveSelect.TabIndex = 7;
            this.btnRemoveSelect.Text = "<";
            this.btnRemoveSelect.UseVisualStyleBackColor = true;
            this.btnRemoveSelect.Click += new System.EventHandler(this.btnRemoveSelect_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(368, 185);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(28, 43);
            this.btnRemoveAll.TabIndex = 8;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(12, 12);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(158, 20);
            this.txtTim.TabIndex = 0;
            this.txtTim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTim_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 111;
            // 
            // btnTimTiep
            // 
            this.btnTimTiep.Location = new System.Drawing.Point(176, 10);
            this.btnTimTiep.Name = "btnTimTiep";
            this.btnTimTiep.Size = new System.Drawing.Size(75, 23);
            this.btnTimTiep.TabIndex = 1;
            this.btnTimTiep.Text = "Tìm tiếp";
            this.btnTimTiep.UseVisualStyleBackColor = true;
            this.btnTimTiep.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // lbldsdc
            // 
            this.lbldsdc.AutoSize = true;
            this.lbldsdc.Location = new System.Drawing.Point(399, 22);
            this.lbldsdc.Name = "lbldsdc";
            this.lbldsdc.Size = new System.Drawing.Size(117, 13);
            this.lbldsdc.TabIndex = 3;
            this.lbldsdc.Text = "Danh sách được chọn:";
            // 
            // btnTimTatCa
            // 
            this.btnTimTatCa.Location = new System.Drawing.Point(257, 10);
            this.btnTimTatCa.Name = "btnTimTatCa";
            this.btnTimTatCa.Size = new System.Drawing.Size(75, 23);
            this.btnTimTatCa.TabIndex = 1;
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
            this.btnHuy.TabIndex = 12;
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
            this.btnNhan.TabIndex = 11;
            this.btnNhan.Tag = "Return, Control";
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnMove2Up
            // 
            this.btnMove2Up.Location = new System.Drawing.Point(758, 38);
            this.btnMove2Up.Name = "btnMove2Up";
            this.btnMove2Up.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Up.TabIndex = 5;
            this.btnMove2Up.Text = "▲";
            this.btnMove2Up.UseVisualStyleBackColor = true;
            this.btnMove2Up.Click += new System.EventHandler(this.btnMove2Up_Click);
            // 
            // btnMove2Down
            // 
            this.btnMove2Down.Location = new System.Drawing.Point(758, 87);
            this.btnMove2Down.Name = "btnMove2Down";
            this.btnMove2Down.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Down.TabIndex = 6;
            this.btnMove2Down.Text = "▼";
            this.btnMove2Down.UseVisualStyleBackColor = true;
            this.btnMove2Down.Click += new System.EventHandler(this.btnMove2Down_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(402, 38);
            this.dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(350, 428);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView2_ColumnAdded);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(350, 428);
            this.dataGridView1.TabIndex = 2;
            // 
            // FieldSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 518);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveSelect);
            this.Controls.Add(this.btnMove2Down);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnMove2Up);
            this.Controls.Add(this.btnAddSelect);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.lbldsdc);
            this.Controls.Add(this.btnTimTatCa);
            this.Controls.Add(this.btnTimTiep);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FieldSelectorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectMultiIDForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectMultiIDForm_Load);
            this.SizeChanged += new System.EventHandler(this.FieldSelectorForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddSelect;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnRemoveSelect;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimTiep;
        private System.Windows.Forms.Label lbldsdc;
        private System.Windows.Forms.Button btnTimTatCa;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        public V6ColorDataGridView dataGridView1;
        public V6ColorDataGridView dataGridView2;
        private System.Windows.Forms.Button btnMove2Up;
        private System.Windows.Forms.Button btnMove2Down;
    }
}