using V6Controls;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu
{
    partial class CorplanControl2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDefault = new V6Controls.V6Label();
            this.lblID = new V6Controls.V6Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkAutoCopyID = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblDefault);
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 275);
            this.panel1.TabIndex = 12;
            // 
            // lblDefault
            // 
            this.lblDefault.AccessibleDescription = "XULYL00199";
            this.lblDefault.Location = new System.Drawing.Point(159, 3);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(160, 20);
            this.lblDefault.TabIndex = 13;
            this.lblDefault.Text = "Mặc định";
            // 
            // lblID
            // 
            this.lblID.AccessibleDescription = "";
            this.lblID.Location = new System.Drawing.Point(3, 3);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(150, 20);
            this.lblID.TabIndex = 13;
            this.lblID.Text = "ID";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = "XULYB00012";
            this.btnAdd.Location = new System.Drawing.Point(7, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkAutoCopyID
            // 
            this.chkAutoCopyID.AccessibleDescription = "XULYC00014";
            this.chkAutoCopyID.AutoSize = true;
            this.chkAutoCopyID.Checked = true;
            this.chkAutoCopyID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCopyID.Location = new System.Drawing.Point(88, 7);
            this.chkAutoCopyID.Name = "chkAutoCopyID";
            this.chkAutoCopyID.Size = new System.Drawing.Size(107, 21);
            this.chkAutoCopyID.TabIndex = 16;
            this.chkAutoCopyID.Text = "Auto copy ID";
            this.chkAutoCopyID.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CorplanControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAutoCopyID);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CorplanControl2";
            this.Size = new System.Drawing.Size(693, 312);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private V6Label lblDefault;
        private V6Label lblID;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkAutoCopyID;
        private System.Windows.Forms.Timer timer1;
    }
}
