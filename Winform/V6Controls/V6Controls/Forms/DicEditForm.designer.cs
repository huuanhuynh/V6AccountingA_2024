namespace V6Controls.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DicEditForm));
            this.txtTim = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnTimTiep = new System.Windows.Forms.Button();
            this.btnTimTatCa = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.btnMove2Up = new System.Windows.Forms.Button();
            this.btnMove2Down = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtValue = new V6Controls.V6ColorTextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            this.btnMove2Up.Location = new System.Drawing.Point(257, 38);
            this.btnMove2Up.Name = "btnMove2Up";
            this.btnMove2Up.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Up.TabIndex = 5;
            this.btnMove2Up.Text = "▲";
            this.btnMove2Up.UseVisualStyleBackColor = true;
            this.btnMove2Up.Click += new System.EventHandler(this.btnMove2Up_Click);
            // 
            // btnMove2Down
            // 
            this.btnMove2Down.Location = new System.Drawing.Point(257, 87);
            this.btnMove2Down.Name = "btnMove2Down";
            this.btnMove2Down.Size = new System.Drawing.Size(28, 43);
            this.btnMove2Down.TabIndex = 6;
            this.btnMove2Down.Text = "▼";
            this.btnMove2Down.UseVisualStyleBackColor = true;
            this.btnMove2Down.Click += new System.EventHandler(this.btnMove2Down_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(12, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(236, 412);
            this.listBox1.TabIndex = 13;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
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
            this.txtValue.Size = new System.Drawing.Size(453, 384);
            this.txtValue.TabIndex = 33;
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(338, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnTimTatCa_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(298, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(453, 29);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // DicEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 518);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnMove2Down);
            this.Controls.Add(this.btnMove2Up);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnNhan);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnTimTatCa);
            this.Controls.Add(this.btnTimTiep);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtTim);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DicEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectMultiIDForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectMultiIDForm_Load);
            this.SizeChanged += new System.EventHandler(this.DicEditForm_SizeChanged);
            this.Controls.SetChildIndex(this.txtTim, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.btnTimTiep, 0);
            this.Controls.SetChildIndex(this.btnTimTatCa, 0);
            this.Controls.SetChildIndex(this.btnThem, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnMove2Up, 0);
            this.Controls.SetChildIndex(this.btnMove2Down, 0);
            this.Controls.SetChildIndex(this.listBox1, 0);
            this.Controls.SetChildIndex(this.txtValue, 0);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimTiep;
        private System.Windows.Forms.Button btnTimTatCa;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnNhan;
        private System.Windows.Forms.Button btnMove2Up;
        private System.Windows.Forms.Button btnMove2Down;
        private System.Windows.Forms.ListBox listBox1;
        private V6ColorTextBox txtValue;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtName;
    }
}