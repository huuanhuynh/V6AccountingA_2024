using System.Windows.Forms;

namespace V6ReportControls
{
    partial class FilterGroup
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
            this.groupBoxNhom = new System.Windows.Forms.GroupBox();
            this.toolTipFilterGroup = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // groupBoxNhom
            // 
            this.groupBoxNhom.AccessibleDescription = "FILTERG00004";
            this.groupBoxNhom.Location = new System.Drawing.Point(4, 3);
            this.groupBoxNhom.Name = "groupBoxNhom";
            this.groupBoxNhom.Size = new System.Drawing.Size(301, 112);
            this.groupBoxNhom.TabIndex = 26;
            this.groupBoxNhom.TabStop = false;
            this.groupBoxNhom.Text = "Thứ tự sắp xếp theo nhóm vật tư - khách hàng";
            // 
            // FilterGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxNhom);
            this.Name = "FilterGroup";
            this.Size = new System.Drawing.Size(308, 118);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBoxNhom;
        private ToolTip toolTipFilterGroup;

    }
}
