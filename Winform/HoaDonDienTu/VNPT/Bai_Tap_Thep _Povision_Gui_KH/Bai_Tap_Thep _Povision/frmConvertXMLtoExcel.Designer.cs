﻿namespace Bai_Tap_Thep__Povision
{
    partial class frmConvertXMLtoExcel
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddExcel = new System.Windows.Forms.TextBox();
            this.txtAddZip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChonExcel = new System.Windows.Forms.Button();
            this.btnChonZip = new System.Windows.Forms.Button();
            this.btnNenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn (Excel)";
            // 
            // txtAddExcel
            // 
            this.txtAddExcel.Location = new System.Drawing.Point(115, 8);
            this.txtAddExcel.Name = "txtAddExcel";
            this.txtAddExcel.Size = new System.Drawing.Size(501, 20);
            this.txtAddExcel.TabIndex = 1;
            // 
            // txtAddZip
            // 
            this.txtAddZip.Location = new System.Drawing.Point(115, 34);
            this.txtAddZip.Name = "txtAddZip";
            this.txtAddZip.Size = new System.Drawing.Size(501, 20);
            this.txtAddZip.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vị trí lưu file Zip";
            // 
            // btnChonExcel
            // 
            this.btnChonExcel.Location = new System.Drawing.Point(621, 6);
            this.btnChonExcel.Name = "btnChonExcel";
            this.btnChonExcel.Size = new System.Drawing.Size(40, 23);
            this.btnChonExcel.TabIndex = 4;
            this.btnChonExcel.Text = "...";
            this.btnChonExcel.UseVisualStyleBackColor = true;
            this.btnChonExcel.Click += new System.EventHandler(this.btnChonExcel_Click);
            // 
            // btnChonZip
            // 
            this.btnChonZip.Location = new System.Drawing.Point(621, 32);
            this.btnChonZip.Name = "btnChonZip";
            this.btnChonZip.Size = new System.Drawing.Size(40, 23);
            this.btnChonZip.TabIndex = 4;
            this.btnChonZip.Text = "...";
            this.btnChonZip.UseVisualStyleBackColor = true;
            this.btnChonZip.Click += new System.EventHandler(this.btnChonZip_Click);
            // 
            // btnNenFile
            // 
            this.btnNenFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNenFile.Location = new System.Drawing.Point(665, 4);
            this.btnNenFile.Name = "btnNenFile";
            this.btnNenFile.Size = new System.Drawing.Size(138, 51);
            this.btnNenFile.TabIndex = 4;
            this.btnNenFile.Text = "Nén File";
            this.btnNenFile.UseVisualStyleBackColor = true;
            this.btnNenFile.Click += new System.EventHandler(this.btnNenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 221);
            this.dataGridView1.TabIndex = 5;
            // 
            // frmConvertXMLtoExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 282);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnNenFile);
            this.Controls.Add(this.btnChonZip);
            this.Controls.Add(this.btnChonExcel);
            this.Controls.Add(this.txtAddZip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAddExcel);
            this.Controls.Add(this.label1);
            this.Name = "frmConvertXMLtoExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmConvertXMLtoExcel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmConvertXMLtoExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddExcel;
        private System.Windows.Forms.TextBox txtAddZip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChonExcel;
        private System.Windows.Forms.Button btnChonZip;
        private System.Windows.Forms.Button btnNenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}