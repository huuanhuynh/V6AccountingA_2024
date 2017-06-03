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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnQuayRa = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 482);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AccessibleDescription = "CORPMODUT00001";
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 453);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Language";
            // 
            // tabPage2
            // 
            this.tabPage2.AccessibleDescription = "CORPMODUT00002";
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 453);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Language1";
            // 
            // tabPage3
            // 
            this.tabPage3.AccessibleDescription = "CORPMODUT00003";
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(779, 453);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Language2";
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnQuayRa;
        private System.Windows.Forms.TabPage tabPage3;
    }
}
