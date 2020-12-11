namespace V6ControlManager.FormManager.ReportManager.Filter.Base0
{
    partial class XADVXNK01_Filter
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.menuChucNang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.thayTheMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.thayTheNhieuMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGuiDanhSach = new System.Windows.Forms.Button();
            this.dataGridView2 = new V6Controls.V6ColorDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridViewTopFilter1 = new V6Controls.Controls.GridViewTopFilter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
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
            this.dataGridView1.ContextMenuStrip = this.menuChucNang;
            this.dataGridView1.Location = new System.Drawing.Point(316, 23);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(677, 338);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.NewRowNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_NewRowNeeded);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // menuChucNang
            // 
            this.menuChucNang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thayTheMenu,
            this.thayTheNhieuMenu});
            this.menuChucNang.Name = "menuChucNang";
            this.menuChucNang.Size = new System.Drawing.Size(153, 48);
            // 
            // thayTheMenu
            // 
            this.thayTheMenu.AccessibleDescription = "INVOICEM00008";
            this.thayTheMenu.Name = "thayTheMenu";
            this.thayTheMenu.Size = new System.Drawing.Size(152, 22);
            this.thayTheMenu.Text = "Thay thế";
            this.thayTheMenu.Click += new System.EventHandler(this.thayTheMenu_Click);
            // 
            // thayTheNhieuMenu
            // 
            this.thayTheNhieuMenu.AccessibleDescription = "INVOICEM00027";
            this.thayTheNhieuMenu.Name = "thayTheNhieuMenu";
            this.thayTheNhieuMenu.Size = new System.Drawing.Size(152, 22);
            this.thayTheNhieuMenu.Text = "Thay thế nhiều";
            this.thayTheNhieuMenu.Click += new System.EventHandler(this.thayTheNhieuMenu_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 238);
            this.panel1.TabIndex = 1;
            // 
            // btnGuiDanhSach
            // 
            this.btnGuiDanhSach.AccessibleDescription = "MAILSMSB00010";
            this.btnGuiDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuiDanhSach.Enabled = false;
            this.btnGuiDanhSach.Image = global::V6ControlManager.Properties.Resources.SendEmail32;
            this.btnGuiDanhSach.Location = new System.Drawing.Point(7, 482);
            this.btnGuiDanhSach.Name = "btnGuiDanhSach";
            this.btnGuiDanhSach.Size = new System.Drawing.Size(165, 52);
            this.btnGuiDanhSach.TabIndex = 10;
            this.btnGuiDanhSach.Text = "Gửi theo danh sách chọn";
            this.btnGuiDanhSach.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGuiDanhSach.UseVisualStyleBackColor = true;
            this.btnGuiDanhSach.Click += new System.EventHandler(this.btnGuiDanhSach_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(316, 367);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView2.Size = new System.Drawing.Size(677, 170);
            this.dataGridView2.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Location = new System.Drawing.Point(3, 241);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(310, 238);
            this.panel2.TabIndex = 1;
            // 
            // gridViewTopFilter1
            // 
            this.gridViewTopFilter1.Auto = false;
            this.gridViewTopFilter1.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewTopFilter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridViewTopFilter1.DataGridView = this.dataGridView1;
            this.gridViewTopFilter1.Location = new System.Drawing.Point(316, 1);
            this.gridViewTopFilter1.Name = "gridViewTopFilter1";
            this.gridViewTopFilter1.Size = new System.Drawing.Size(677, 22);
            this.gridViewTopFilter1.TabIndex = 1;
            // 
            // XADVXNK01_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridViewTopFilter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGuiDanhSach);
            this.Name = "XADVXNK01_Filter";
            this.Size = new System.Drawing.Size(1000, 540);
            this.Load += new System.EventHandler(this.XADVXNK01_Filter_Load);
            this.SizeChanged += new System.EventHandler(this.XADVXNK01_Filter_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuChucNang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.V6ColorDataGridView dataGridView1;
        private System.Windows.Forms.Button btnGuiDanhSach;
        public V6Controls.V6ColorDataGridView dataGridView2;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip menuChucNang;
        private System.Windows.Forms.ToolStripMenuItem thayTheMenu;
        private System.Windows.Forms.ToolStripMenuItem thayTheNhieuMenu;
        private V6Controls.Controls.GridViewTopFilter gridViewTopFilter1;


    }
}
