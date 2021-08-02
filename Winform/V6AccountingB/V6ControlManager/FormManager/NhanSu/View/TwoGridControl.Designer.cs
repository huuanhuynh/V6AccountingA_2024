namespace V6ControlManager.FormManager.NhanSu.View
{
    partial class TwoGridControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.gridView1 = new V6Controls.V6ColorDataGridView();
            this.gridView2 = new V6Controls.V6ColorDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.AutoScroll = true;
            this.panelBottom.Location = new System.Drawing.Point(0, 300);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(600, 150);
            this.panelBottom.TabIndex = 2;
            // 
            // gridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.gridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridView1.Location = new System.Drawing.Point(0, 0);
            this.gridView1.Margin = new System.Windows.Forms.Padding(0);
            this.gridView1.Name = "gridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.gridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridView1.Size = new System.Drawing.Size(600, 150);
            this.gridView1.TabIndex = 0;
            // 
            // gridView2
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.gridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridView2.Location = new System.Drawing.Point(0, 150);
            this.gridView2.Margin = new System.Windows.Forms.Padding(0);
            this.gridView2.Name = "gridView2";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.gridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridView2.Size = new System.Drawing.Size(600, 150);
            this.gridView2.TabIndex = 1;
            this.gridView2.SelectionChanged += new System.EventHandler(this.gridView2_SelectionChanged);
            // 
            // TwoGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridView2);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.gridView1);
            this.Name = "TwoGridControl";
            this.Size = new System.Drawing.Size(600, 450);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private V6Controls.V6ColorDataGridView gridView1;
        private V6Controls.V6ColorDataGridView gridView2;
    }
}
