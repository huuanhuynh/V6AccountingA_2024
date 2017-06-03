namespace H_Controls.Controls.Lookup.Filter
{
    partial class FilterForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new H_Controls.Controls.Lookup.Filter.PanelFilter();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "huy";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(115, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tag = "Escape";
            this.btnCancel.Text = "Đóng (Esc)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AccessibleDescription = "loc";
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(12, 257);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(97, 30);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Tag = "Return, Control";
            this.btnFilter.Text = "Lọc (Ctrl + Enter)";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 245);
            this.panel1.TabIndex = 0;
            // 
            // FilterForm
            // 
            this.AccessibleDescription = "filter_form";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 292);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "FilterForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.Text = "Lọc";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelFilter panel1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnCancel;
    }
}