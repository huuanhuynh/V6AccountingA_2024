namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    partial class TableButton
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTTT = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.labelTop = new System.Windows.Forms.Label();
            this.labelBottom = new System.Windows.Forms.Label();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelRight = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(87, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "A1";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(0, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(87, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Trống";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTTT
            // 
            this.lblTTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTTT.Location = new System.Drawing.Point(0, 37);
            this.lblTTT.Name = "lblTTT";
            this.lblTTT.Size = new System.Drawing.Size(87, 13);
            this.lblTTT.TabIndex = 2;
            this.lblTTT.Text = "0";
            this.lblTTT.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTTT.Click += new System.EventHandler(this.TableButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTTT);
            this.panel1.Controls.Add(this.lblGhiChu);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 67);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.TableButton_Click);
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.Location = new System.Drawing.Point(0, 52);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(87, 15);
            this.lblGhiChu.TabIndex = 1;
            this.lblGhiChu.Text = "GC";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblGhiChu.TextChanged += new System.EventHandler(this.lblGhiChu_TextChanged);
            this.lblGhiChu.Click += new System.EventHandler(this.TableButton_Click);
            // 
            // labelTop
            // 
            this.labelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTop.Location = new System.Drawing.Point(0, 0);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(93, 3);
            this.labelTop.TabIndex = 4;
            // 
            // labelBottom
            // 
            this.labelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelBottom.Location = new System.Drawing.Point(0, 70);
            this.labelBottom.Name = "labelBottom";
            this.labelBottom.Size = new System.Drawing.Size(93, 3);
            this.labelBottom.TabIndex = 5;
            // 
            // labelLeft
            // 
            this.labelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelLeft.Location = new System.Drawing.Point(0, 3);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(3, 67);
            this.labelLeft.TabIndex = 6;
            // 
            // labelRight
            // 
            this.labelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelRight.Location = new System.Drawing.Point(90, 3);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(3, 67);
            this.labelRight.TabIndex = 7;
            // 
            // TableButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelRight);
            this.Controls.Add(this.labelLeft);
            this.Controls.Add(this.labelBottom);
            this.Controls.Add(this.labelTop);
            this.Controls.Add(this.panel1);
            this.Name = "TableButton";
            this.Size = new System.Drawing.Size(93, 73);
            this.Click += new System.EventHandler(this.TableButton_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTTT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.Label labelBottom;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label labelRight;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
