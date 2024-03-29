﻿namespace V6ControlManager.FormManager.ReportManager.Filter
{
    partial class ATODCBP
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
            this.v6Label1 = new V6Controls.V6Label();
            this.TxtSo_the_cc = new V6Controls.V6VvarTextBox();
            this.txtMaDvcs = new V6ReportControls.FilterLineVvarTextBox();
            this.txtNam = new V6Controls.V6NumberTextBox();
            this.txtThang1 = new V6Controls.V6NumberTextBox();
            this.v6Label9 = new V6Controls.V6Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // v6Label1
            // 
            this.v6Label1.AccessibleDescription = "FILTERL00197";
            this.v6Label1.AutoSize = true;
            this.v6Label1.Location = new System.Drawing.Point(5, 48);
            this.v6Label1.Name = "v6Label1";
            this.v6Label1.Size = new System.Drawing.Size(54, 13);
            this.v6Label1.TabIndex = 19;
            this.v6Label1.Text = "Mã CCDC";
            // 
            // TxtSo_the_cc
            // 
            this.TxtSo_the_cc.AccessibleName = "SO_THE_CC";
            this.TxtSo_the_cc.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSo_the_cc.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.TxtSo_the_cc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSo_the_cc.CheckNotEmpty = true;
            this.TxtSo_the_cc.EnterColor = System.Drawing.Color.PaleGreen;
            this.TxtSo_the_cc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtSo_the_cc.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.TxtSo_the_cc.HoverColor = System.Drawing.Color.Yellow;
            this.TxtSo_the_cc.LeaveColor = System.Drawing.Color.White;
            this.TxtSo_the_cc.Location = new System.Drawing.Point(175, 46);
            this.TxtSo_the_cc.Name = "TxtSo_the_cc";
            this.TxtSo_the_cc.Size = new System.Drawing.Size(109, 20);
            this.TxtSo_the_cc.TabIndex = 18;
            this.TxtSo_the_cc.VVar = "SO_THE_CC";
            // 
            // txtMaDvcs
            // 
            this.txtMaDvcs.AccessibleDescription = "FILTERL00005";
            this.txtMaDvcs.AccessibleName2 = "MA_DVCS";
            this.txtMaDvcs.Caption = "Mã đơn vị";
            this.txtMaDvcs.FieldName = "MA_DVCS";
            this.txtMaDvcs.Location = new System.Drawing.Point(5, 69);
            this.txtMaDvcs.Name = "txtMaDvcs";
            this.txtMaDvcs.Size = new System.Drawing.Size(282, 22);
            this.txtMaDvcs.TabIndex = 20;
            this.txtMaDvcs.Vvar = "MA_DVCS";
            // 
            // txtNam
            // 
            this.txtNam.BackColor = System.Drawing.SystemColors.Window;
            this.txtNam.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtNam.DecimalPlaces = 0;
            this.txtNam.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNam.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtNam.HoverColor = System.Drawing.Color.Yellow;
            this.txtNam.LeaveColor = System.Drawing.Color.White;
            this.txtNam.Location = new System.Drawing.Point(175, 3);
            this.txtNam.MaxLength = 4;
            this.txtNam.MaxNumLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(109, 20);
            this.txtNam.TabIndex = 21;
            this.txtNam.Text = "0";
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNam.Visible = false;
            // 
            // txtThang1
            // 
            this.txtThang1.BackColor = System.Drawing.SystemColors.Window;
            this.txtThang1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtThang1.DecimalPlaces = 0;
            this.txtThang1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtThang1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThang1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtThang1.HoverColor = System.Drawing.Color.Yellow;
            this.txtThang1.LeaveColor = System.Drawing.Color.White;
            this.txtThang1.Location = new System.Drawing.Point(175, 24);
            this.txtThang1.MaxLength = 2;
            this.txtThang1.MaxNumLength = 2;
            this.txtThang1.Name = "txtThang1";
            this.txtThang1.Size = new System.Drawing.Size(109, 20);
            this.txtThang1.TabIndex = 23;
            this.txtThang1.Text = "0";
            this.txtThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThang1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtThang1.Visible = false;
            // 
            // v6Label9
            // 
            this.v6Label9.AccessibleDescription = "FILTERL00109";
            this.v6Label9.AutoSize = true;
            this.v6Label9.Location = new System.Drawing.Point(5, 6);
            this.v6Label9.Name = "v6Label9";
            this.v6Label9.Size = new System.Drawing.Size(29, 13);
            this.v6Label9.TabIndex = 24;
            this.v6Label9.Text = "Năm";
            this.v6Label9.Visible = false;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "FILTERL00120";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Kỳ";
            this.label1.Visible = false;
            // 
            // ATODCBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.txtThang1);
            this.Controls.Add(this.v6Label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaDvcs);
            this.Controls.Add(this.v6Label1);
            this.Controls.Add(this.TxtSo_the_cc);
            this.Name = "ATODCBP";
            this.Size = new System.Drawing.Size(594, 116);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private V6Controls.V6Label v6Label1;
        private V6Controls.V6VvarTextBox TxtSo_the_cc;
        private V6ReportControls.FilterLineVvarTextBox txtMaDvcs;
        private V6Controls.V6NumberTextBox txtNam;
        private V6Controls.V6NumberTextBox txtThang1;
        private V6Controls.V6Label v6Label9;
        private System.Windows.Forms.Label label1;

    }
}
