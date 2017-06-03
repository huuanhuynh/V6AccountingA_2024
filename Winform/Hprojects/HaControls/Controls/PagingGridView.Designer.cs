namespace H_Controls.Controls
{
    partial class PagingGridView
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefresh = new H_Controls.Controls.ButtonL();
            this.btnSearch = new H_Controls.Controls.ButtonL();
            this.txtCurrentPage = new H_Controls.Controls.ColorTextBox();
            this.btnLast = new H_Controls.Controls.ButtonL();
            this.btnNext = new H_Controls.Controls.ButtonL();
            this.btnPrevious = new H_Controls.Controls.ButtonL();
            this.btnFirst = new H_Controls.Controls.ButtonL();
            this.lblTotalPage = new H_Controls.Controls.LabelH();
            this.dataGridView1 = new H_Controls.Controls.ColorGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "20",
            "50"});
            this.comboBox1.Location = new System.Drawing.Point(265, 180);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(35, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.ClickColor = System.Drawing.Color.Blue;
            this.btnRefresh.HoverColor = System.Drawing.Color.Aqua;
            this.btnRefresh.Image = global::H_Controls.Properties.Resources.refreshgreen;
            this.btnRefresh.LeaveColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Location = new System.Drawing.Point(138, 184);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(16, 16);
            this.btnRefresh.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnRefresh, "Tải lại tất cả");
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.ClickColor = System.Drawing.Color.Blue;
            this.btnSearch.HoverColor = System.Drawing.Color.Aqua;
            this.btnSearch.Image = global::H_Controls.Properties.Resources.searchblue;
            this.btnSearch.LeaveColor = System.Drawing.Color.Transparent;
            this.btnSearch.Location = new System.Drawing.Point(116, 183);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(16, 16);
            this.btnSearch.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnSearch, "Tìm kiếm chi tiết");
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCurrentPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPage.EnableColorEffect = true;
            this.txtCurrentPage.EnableColorEffectOnMouseEnter = false;
            this.txtCurrentPage.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtCurrentPage.GrayTitle = "";
            this.txtCurrentPage.HoverColor = System.Drawing.Color.Yellow;
            this.txtCurrentPage.LeaveColor = System.Drawing.Color.White;
            this.txtCurrentPage.LimitCharacters = null;
            this.txtCurrentPage.Location = new System.Drawing.Point(40, 180);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(29, 20);
            this.txtCurrentPage.TabIndex = 4;
            this.txtCurrentPage.UseSendTabOnEnter = false;
            this.txtCurrentPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPage_KeyDown);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLast.ClickColor = System.Drawing.Color.Blue;
            this.btnLast.HoverColor = System.Drawing.Color.Aqua;
            this.btnLast.LeaveColor = System.Drawing.Color.Transparent;
            this.btnLast.Location = new System.Drawing.Point(94, 182);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(16, 16);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = ">|";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.ClickColor = System.Drawing.Color.Blue;
            this.btnNext.HoverColor = System.Drawing.Color.Aqua;
            this.btnNext.LeaveColor = System.Drawing.Color.Transparent;
            this.btnNext.Location = new System.Drawing.Point(72, 182);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(16, 16);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = ">";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.ClickColor = System.Drawing.Color.Blue;
            this.btnPrevious.HoverColor = System.Drawing.Color.Aqua;
            this.btnPrevious.LeaveColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Location = new System.Drawing.Point(23, 182);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(16, 16);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "<";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFirst.ClickColor = System.Drawing.Color.Blue;
            this.btnFirst.HoverColor = System.Drawing.Color.Aqua;
            this.btnFirst.LeaveColor = System.Drawing.Color.Transparent;
            this.btnFirst.Location = new System.Drawing.Point(1, 182);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(16, 16);
            this.btnFirst.TabIndex = 2;
            this.btnFirst.Text = "|<";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPage.Location = new System.Drawing.Point(138, 182);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(124, 18);
            this.lblTotalPage.TabIndex = 1;
            this.lblTotalPage.Text = ".";
            this.lblTotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(300, 180);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnAdded);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // PagingGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PagingGridView";
            this.Size = new System.Drawing.Size(300, 200);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelH lblTotalPage;
        private ButtonL btnFirst;
        private ButtonL btnPrevious;
        private ButtonL btnNext;
        private ButtonL btnLast;
        private System.Windows.Forms.ComboBox comboBox1;
        private ColorTextBox txtCurrentPage;
        public ColorGridView dataGridView1;
        private ButtonL btnSearch;
        private ButtonL btnRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
