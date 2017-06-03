using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    partial class ClientManager
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
            this.gridView1 = new V6Controls.V6ColorDataGridView();
            this.v6FormButton1 = new V6FormButton();
            this.v6FormButton2 = new V6FormButton();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.gridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView1.Location = new System.Drawing.Point(3, 3);
            this.gridView1.Name = "gridView1";
            this.gridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.gridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridView1.Size = new System.Drawing.Size(476, 249);
            this.gridView1.TabIndex = 0;
            this.gridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.v6ColorDataGridView1_CellMouseDoubleClick);
            // 
            // v6FormButton1
            // 
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
            // ClientManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.v6FormButton2);
            this.Controls.Add(this.v6FormButton1);
            this.Controls.Add(this.gridView1);
            this.Name = "ClientManager";
            this.Size = new System.Drawing.Size(482, 316);
            this.Load += new System.EventHandler(this.ClientManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private V6ColorDataGridView gridView1;
        private V6FormButton v6FormButton1;
        private V6FormButton v6FormButton2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAll;
    }
}
