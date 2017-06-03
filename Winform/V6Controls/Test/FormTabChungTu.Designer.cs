namespace Test
{
    partial class FormTabChungTu
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
            this.btnNew = new System.Windows.Forms.Button();
            this.tabCacChungTu = new V6TabControlLib.V6TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabCacChungTu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(12, 463);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "Mới";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tabCacChungTu
            // 
            this.tabCacChungTu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCacChungTu.Controls.Add(this.tabPage1);
            this.tabCacChungTu.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabCacChungTu.ItemSize = new System.Drawing.Size(230, 24);
            this.tabCacChungTu.Location = new System.Drawing.Point(0, 0);
            this.tabCacChungTu.Name = "tabCacChungTu";
            this.tabCacChungTu.SelectedIndex = 0;
            this.tabCacChungTu.Size = new System.Drawing.Size(448, 456);
            this.tabCacChungTu.TabIndex = 0;
            this.tabCacChungTu.TabStop = false;
            this.tabCacChungTu.Validated += new System.EventHandler(this.tabCacChungTu_Validated);
            this.tabCacChungTu.OnAddClick += new V6TabControlLib.V6TabControl.OnAddClickDelegate(this.tabCacChungTu_OnAddClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(440, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(454, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.tabCacChungTu;
            this.propertyGrid1.Size = new System.Drawing.Size(210, 474);
            this.propertyGrid1.TabIndex = 2;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // FormTabChungTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 498);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.tabCacChungTu);
            this.Name = "FormTabChungTu";
            this.Text = "FormTabChungTu";
            this.Load += new System.EventHandler(this.FormTabChungTu_Load);
            this.tabCacChungTu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private V6TabControlLib.V6TabControl tabCacChungTu;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}