namespace V6ControlManager.FormManager.ToolManager
{
    partial class FormAmLich
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAmLich));
            this.lichViewControl1 = new V6Controls.Controls.LichView.LichViewControl();
            this.SuspendLayout();
            // 
            // lichViewControl1
            // 
            this.lichViewControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.lichViewControl1.BorderColor = System.Drawing.Color.Black;
            this.lichViewControl1.DetailColor = System.Drawing.Color.Orange;
            this.lichViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lichViewControl1.FooterHeight = 0;
            this.lichViewControl1.FooterText = null;
            this.lichViewControl1.HeaderHeight = 50;
            this.lichViewControl1.HoverBackColor = System.Drawing.Color.Aqua;
            this.lichViewControl1.Location = new System.Drawing.Point(0, 0);
            this.lichViewControl1.Name = "lichViewControl1";
            this.lichViewControl1.RowData = null;
            this.lichViewControl1.SatudayColor = System.Drawing.Color.Blue;
            this.lichViewControl1.ShowNextPrevious = true;
            this.lichViewControl1.Size = new System.Drawing.Size(598, 443);
            this.lichViewControl1.SundayColor = System.Drawing.Color.Red;
            this.lichViewControl1.TabIndex = 5;
            this.lichViewControl1.ClickNextEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichViewControl1_ClickNextEvent);
            this.lichViewControl1.ClickPreviousEvent += new System.Action<V6Controls.Controls.LichView.LichViewEventArgs>(this.lichViewControl1_ClickPreviousEvent);
            // 
            // FormAmLich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 443);
            this.Controls.Add(this.lichViewControl1);
            this.Name = "FormAmLich";
            this.Text = "AmLich";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.lblTopMessage, 0);
            this.Controls.SetChildIndex(this.lichViewControl1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private V6Controls.Controls.LichView.LichViewControl lichViewControl1;

    }
}

