using V6Controls;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu
{
    partial class CorplanContainer 
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLanguage0 = new System.Windows.Forms.TabPage();
            this.tabLanguage1 = new System.Windows.Forms.TabPage();
            this.tabLanguage2 = new System.Windows.Forms.TabPage();
            this.btnQuayRa = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabLanguage0);
            this.tabControl1.Controls.Add(this.tabLanguage1);
            this.tabControl1.Controls.Add(this.tabLanguage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 482);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabLanguage0
            // 
            this.tabLanguage0.AccessibleDescription = "CORPMODUT00001";
            this.tabLanguage0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabLanguage0.Location = new System.Drawing.Point(4, 25);
            this.tabLanguage0.Name = "tabLanguage0";
            this.tabLanguage0.Padding = new System.Windows.Forms.Padding(3);
            this.tabLanguage0.Size = new System.Drawing.Size(779, 453);
            this.tabLanguage0.TabIndex = 0;
            this.tabLanguage0.Text = "Language";
            // 
            // tabLanguage1
            // 
            this.tabLanguage1.AccessibleDescription = "CORPMODUT00002";
            this.tabLanguage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabLanguage1.Location = new System.Drawing.Point(4, 25);
            this.tabLanguage1.Name = "tabLanguage1";
            this.tabLanguage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabLanguage1.Size = new System.Drawing.Size(779, 453);
            this.tabLanguage1.TabIndex = 1;
            this.tabLanguage1.Text = "Language1";
            // 
            // tabLanguage2
            // 
            this.tabLanguage2.AccessibleDescription = "CORPMODUT00003";
            this.tabLanguage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabLanguage2.Location = new System.Drawing.Point(4, 25);
            this.tabLanguage2.Name = "tabLanguage2";
            this.tabLanguage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabLanguage2.Size = new System.Drawing.Size(779, 453);
            this.tabLanguage2.TabIndex = 2;
            this.tabLanguage2.Text = "Language2";
            // 
            // btnQuayRa
            // 
            this.btnQuayRa.AccessibleDescription = "CORPMODUB00008";
            this.btnQuayRa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuayRa.Image = global::V6ControlManager.Properties.Resources.BackArrow24;
            this.btnQuayRa.Location = new System.Drawing.Point(675, 485);
            this.btnQuayRa.Name = "btnQuayRa";
            this.btnQuayRa.Size = new System.Drawing.Size(111, 41);
            this.btnQuayRa.TabIndex = 16;
            this.btnQuayRa.Text = "&Quay ra";
            this.btnQuayRa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuayRa.UseVisualStyleBackColor = true;
            this.btnQuayRa.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // CorplanContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnQuayRa);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CorplanContainer";
            this.Size = new System.Drawing.Size(790, 529);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLanguage0;
        private System.Windows.Forms.TabPage tabLanguage1;
        private System.Windows.Forms.Button btnQuayRa;
        private System.Windows.Forms.TabPage tabLanguage2;
    }
}
