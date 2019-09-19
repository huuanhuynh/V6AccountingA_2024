﻿namespace V6ControlManager.FormManager.ChungTuManager
{
    partial class ChungTuChungContainer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tsNew = new System.Windows.Forms.Button();
            this.tsFull = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 578);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tsNew
            // 
            this.tsNew.FlatAppearance.BorderSize = 0;
            this.tsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tsNew.Image = global::V6ControlManager.Properties.Resources.Add24;
            this.tsNew.Location = new System.Drawing.Point(3, 0);
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(26, 26);
            this.tsNew.TabIndex = 3;
            this.tsNew.UseVisualStyleBackColor = true;
            this.tsNew.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tsFull
            // 
            this.tsFull.FlatAppearance.BorderSize = 0;
            this.tsFull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tsFull.Image = global::V6ControlManager.Properties.Resources.ZoomIn24;
            this.tsFull.Location = new System.Drawing.Point(35, 0);
            this.tsFull.Name = "tsFull";
            this.tsFull.Size = new System.Drawing.Size(26, 26);
            this.tsFull.TabIndex = 3;
            this.tsFull.UseVisualStyleBackColor = true;
            this.tsFull.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::V6ControlManager.Properties.Resources.CloseXbox24;
            this.btnClose.Location = new System.Drawing.Point(829, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tsMessage);
            this.panel1.Location = new System.Drawing.Point(67, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 33);
            this.panel1.TabIndex = 4;
            // 
            // tsMessage
            // 
            this.tsMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMessage.ForeColor = System.Drawing.Color.Red;
            this.tsMessage.Location = new System.Drawing.Point(0, 0);
            this.tsMessage.Name = "tsMessage";
            this.tsMessage.Size = new System.Drawing.Size(758, 33);
            this.tsMessage.TabIndex = 0;
            this.tsMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsMessage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tsMessage_MouseDoubleClick);
            // 
            // ChungTuChungContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tsFull);
            this.Controls.Add(this.tsNew);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FilterType = "1";
            this.Name = "ChungTuChungContainer";
            this.Size = new System.Drawing.Size(860, 609);
            this.Load += new System.EventHandler(this.ChungTuChungContainer_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button tsNew;
        private System.Windows.Forms.Button tsFull;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tsMessage;
        public System.Windows.Forms.Button btnClose;
    }
}
