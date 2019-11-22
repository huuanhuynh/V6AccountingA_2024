using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using V6Controls.Forms;

namespace V6Controls
{
    partial class V6LookupTextboxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V6LookupTextboxForm));
            this.pBar = new System.Windows.Forms.Panel();
            this.rbtTimKiem = new System.Windows.Forms.RadioButton();
            this.rbtLocTuDau = new System.Windows.Forms.RadioButton();
            this.cbbGoiY = new System.Windows.Forms.ComboBox();
            this.rbtLocTiep = new System.Windows.Forms.RadioButton();
            this.cbbDieuKien = new System.Windows.Forms.ComboBox();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.cbbKyHieu = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTimAll = new System.Windows.Forms.Button();
            this.txtV_Search = new System.Windows.Forms.TextBox();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnVSearch = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pBar
            // 
            this.pBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBar.Controls.Add(this.rbtTimKiem);
            this.pBar.Controls.Add(this.rbtLocTuDau);
            this.pBar.Controls.Add(this.cbbGoiY);
            this.pBar.Controls.Add(this.rbtLocTiep);
            this.pBar.Controls.Add(this.cbbDieuKien);
            this.pBar.Controls.Add(this.btnTatCa);
            this.pBar.Controls.Add(this.cbbKyHieu);
            this.pBar.Controls.Add(this.btnTimKiem);
            this.pBar.Location = new System.Drawing.Point(0, 30);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(734, 24);
            this.pBar.TabIndex = 1;
            // 
            // rbtTimKiem
            // 
            this.rbtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtTimKiem.AutoSize = true;
            this.rbtTimKiem.Location = new System.Drawing.Point(656, 4);
            this.rbtTimKiem.Name = "rbtTimKiem";
            this.rbtTimKiem.Size = new System.Drawing.Size(67, 17);
            this.rbtTimKiem.TabIndex = 7;
            this.rbtTimKiem.Text = "Tìm kiếm";
            this.rbtTimKiem.UseVisualStyleBackColor = true;
            // 
            // rbtLocTuDau
            // 
            this.rbtLocTuDau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtLocTuDau.AutoSize = true;
            this.rbtLocTuDau.Checked = true;
            this.rbtLocTuDau.Location = new System.Drawing.Point(575, 5);
            this.rbtLocTuDau.Name = "rbtLocTuDau";
            this.rbtLocTuDau.Size = new System.Drawing.Size(77, 17);
            this.rbtLocTuDau.TabIndex = 6;
            this.rbtLocTuDau.TabStop = true;
            this.rbtLocTuDau.Text = "Lọc từ đầu";
            this.rbtLocTuDau.UseVisualStyleBackColor = true;
            // 
            // cbbGoiY
            // 
            this.cbbGoiY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbGoiY.FormattingEnabled = true;
            this.cbbGoiY.Location = new System.Drawing.Point(235, 3);
            this.cbbGoiY.Name = "cbbGoiY";
            this.cbbGoiY.Size = new System.Drawing.Size(123, 21);
            this.cbbGoiY.TabIndex = 2;
            this.cbbGoiY.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbbGoiY_KeyUp);
            this.cbbGoiY.Leave += new System.EventHandler(this.cbbGoiY_Leave);
            // 
            // rbtLocTiep
            // 
            this.rbtLocTiep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtLocTiep.AutoSize = true;
            this.rbtLocTiep.Location = new System.Drawing.Point(506, 5);
            this.rbtLocTiep.Name = "rbtLocTiep";
            this.rbtLocTiep.Size = new System.Drawing.Size(63, 17);
            this.rbtLocTiep.TabIndex = 5;
            this.rbtLocTiep.Text = "Lọc tiếp";
            this.rbtLocTiep.UseVisualStyleBackColor = true;
            // 
            // cbbDieuKien
            // 
            this.cbbDieuKien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDieuKien.FormattingEnabled = true;
            this.cbbDieuKien.Location = new System.Drawing.Point(3, 3);
            this.cbbDieuKien.Name = "cbbDieuKien";
            this.cbbDieuKien.Size = new System.Drawing.Size(148, 21);
            this.cbbDieuKien.TabIndex = 0;
            this.cbbDieuKien.TextChanged += new System.EventHandler(this.cbbDieuKien_TextChanged);
            // 
            // btnTatCa
            // 
            this.btnTatCa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTatCa.Location = new System.Drawing.Point(402, 2);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(98, 22);
            this.btnTatCa.TabIndex = 4;
            this.btnTatCa.Text = "Hiện tất cả";
            this.btnTatCa.UseVisualStyleBackColor = true;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // cbbKyHieu
            // 
            this.cbbKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbKyHieu.FormattingEnabled = true;
            this.cbbKyHieu.Location = new System.Drawing.Point(163, 3);
            this.cbbKyHieu.Name = "cbbKyHieu";
            this.cbbKyHieu.Size = new System.Drawing.Size(58, 21);
            this.cbbKyHieu.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Location = new System.Drawing.Point(364, 2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(34, 22);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnTimAll);
            this.panel1.Controls.Add(this.txtV_Search);
            this.panel1.Controls.Add(this.btnESC);
            this.panel1.Controls.Add(this.btnVSearch);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 24);
            this.panel1.TabIndex = 0;
            // 
            // btnTimAll
            // 
            this.btnTimAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimAll.Location = new System.Drawing.Point(404, 0);
            this.btnTimAll.Name = "btnTimAll";
            this.btnTimAll.Size = new System.Drawing.Size(96, 22);
            this.btnTimAll.TabIndex = 8;
            this.btnTimAll.Text = "Tìm tất cả";
            this.btnTimAll.UseVisualStyleBackColor = true;
            this.btnTimAll.Visible = false;
            this.btnTimAll.Click += new System.EventHandler(this.btnTimAll_Click);
            // 
            // txtV_Search
            // 
            this.txtV_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtV_Search.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtV_Search.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtV_Search.Location = new System.Drawing.Point(163, 2);
            this.txtV_Search.Name = "txtV_Search";
            this.txtV_Search.Size = new System.Drawing.Size(195, 20);
            this.txtV_Search.TabIndex = 2;
            // 
            // btnESC
            // 
            this.btnESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnESC.Location = new System.Drawing.Point(3, 0);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(72, 22);
            this.btnESC.TabIndex = 1;
            this.btnESC.Text = "ESC";
            this.btnESC.UseVisualStyleBackColor = true;
            this.btnESC.Visible = false;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // btnVSearch
            // 
            this.btnVSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVSearch.Location = new System.Drawing.Point(364, 0);
            this.btnVSearch.Name = "btnVSearch";
            this.btnVSearch.Size = new System.Drawing.Size(37, 22);
            this.btnVSearch.TabIndex = 1;
            this.btnVSearch.Text = "Tìm";
            this.btnVSearch.UseVisualStyleBackColor = true;
            this.btnVSearch.Click += new System.EventHandler(this.btnVSearch_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(732, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(319, 17);
            this.toolStripStatusLabel1.Text = "F1-Hướng dẫn, F3-Sửa, F4-Thêm, Enter-Chọn, ESC-Quay ra";
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 60);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(732, 441);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = ".";
            // 
            // V6LookupTextboxForm
            // 
            this.CancelButton = this.btnESC;
            this.ClientSize = new System.Drawing.Size(732, 526);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "V6LookupTextboxForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.V6LookupTextboxForm_Load);
            this.pBar.ResumeLayout(false);
            this.pBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Panel pBar;
        internal ComboBox cbbDieuKien;
        internal RadioButton rbtTimKiem;
        internal RadioButton rbtLocTuDau;
        internal ComboBox cbbGoiY;
        internal RadioButton rbtLocTiep;
        internal Button btnTatCa;
        internal ComboBox cbbKyHieu;
        internal Button btnTimKiem;
        internal V6ColorDataGridView dataGridView1;
        private Panel panel1;
        internal Button btnVSearch;
        internal TextBox txtV_Search;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        internal Button btnESC;
        internal Button btnTimAll;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}

