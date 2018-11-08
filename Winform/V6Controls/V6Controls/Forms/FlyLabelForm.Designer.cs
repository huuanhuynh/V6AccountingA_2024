namespace V6Controls.Forms
{
    partial class FlyLabelForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlyLabelForm));
            this.lblMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblHole = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(74, 15);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Thông báo.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.ClientSizeChanged += new System.EventHandler(this.lblMessage_ClientSizeChanged);
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            this.lblMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.V6VvarNameForm_MouseDown);
            this.lblMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.V6VvarNameForm_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblHole
            // 
            this.lblHole.BackColor = System.Drawing.Color.White;
            this.lblHole.Location = new System.Drawing.Point(0, 0);
            this.lblHole.Name = "lblHole";
            this.lblHole.Size = new System.Drawing.Size(1, 1);
            this.lblHole.TabIndex = 1;
            this.lblHole.Text = "label1";
            // 
            // FlyLabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(74, 15);
            this.Controls.Add(this.lblHole);
            this.Controls.Add(this.lblMessage);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 15);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 15);
            this.Name = "FlyLabelForm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Thông báo";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.V6VvarNameForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.V6VvarNameForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.V6VvarNameForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblHole;
    }
}