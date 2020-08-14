using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    partial class OnlineManager
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
            this.dataGridView1 = new V6Controls.V6ColorDataGridView();
            this.v6FormButton1 = new V6Controls.Controls.V6FormButton();
            this.v6FormButton2 = new V6Controls.Controls.V6FormButton();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(476, 249);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.v6ColorDataGridView1_CellMouseDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // v6FormButton1
            // 
            this.v6FormButton1.AccessibleDescription = "ADDEDITB00001";
            this.v6FormButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.v6FormButton1.Location = new System.Drawing.Point(3, 290);
            this.v6FormButton1.Name = "v6FormButton1";
            this.v6FormButton1.Size = new System.Drawing.Size(75, 23);
            this.v6FormButton1.TabIndex = 1;
            this.v6FormButton1.Text = "&Nhận";
            this.v6FormButton1.UseVisualStyleBackColor = true;
            this.v6FormButton1.Visible = false;
            this.v6FormButton1.Click += new System.EventHandler(this.v6FormButton1_Click);
            // 
            // v6FormButton2
            // 
            this.v6FormButton2.AccessibleDescription = "ADDEDITB00023";
            this.v6FormButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.v6FormButton2.Location = new System.Drawing.Point(84, 290);
            this.v6FormButton2.Name = "v6FormButton2";
            this.v6FormButton2.Size = new System.Drawing.Size(75, 23);
            this.v6FormButton2.TabIndex = 1;
            this.v6FormButton2.Text = "&Quay ra";
            this.v6FormButton2.UseVisualStyleBackColor = true;
            this.v6FormButton2.Visible = false;
            this.v6FormButton2.Click += new System.EventHandler(this.v6FormButton2_Click);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleDescription = "DANHMUCVIEWB00012";
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Image = global::V6ControlManager.Properties.Resources.Back2;
            this.btnBack.Location = new System.Drawing.Point(423, 258);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(56, 55);
            this.btnBack.TabIndex = 21;
            this.btnBack.Tag = "Escape";
            this.btnBack.Text = "Quay ra";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAll
            // 
            this.btnAll.AccessibleDescription = "DANHMUCVIEWB00014";
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Image = global::V6ControlManager.Properties.Resources.Refresh;
            this.btnAll.Location = new System.Drawing.Point(361, 258);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(56, 55);
            this.btnAll.TabIndex = 20;
            this.btnAll.Tag = "F10";
            this.btnAll.Text = "Tải lại";
            this.btnAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // OnlineManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.v6FormButton2);
            this.Controls.Add(this.v6FormButton1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "OnlineManager";
            this.Size = new System.Drawing.Size(482, 316);
            this.Load += new System.EventHandler(this.OnlineManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private V6ColorDataGridView dataGridView1;
        private V6FormButton v6FormButton1;
        private V6FormButton v6FormButton2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAll;
    }
}
