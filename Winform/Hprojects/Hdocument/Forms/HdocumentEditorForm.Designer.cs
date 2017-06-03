namespace H_document.Forms
{
    partial class HdocumentEditorForm
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
            this.hdocumentEditor1 = new H_document.Forms.HdocumentEditor();
            this.SuspendLayout();
            // 
            // hdocumentEditor1
            // 
            this.hdocumentEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hdocumentEditor1.Location = new System.Drawing.Point(0, 0);
            this.hdocumentEditor1.Name = "hdocumentEditor1";
            this.hdocumentEditor1.Size = new System.Drawing.Size(705, 503);
            this.hdocumentEditor1.TabIndex = 0;
            // 
            // HdocumentEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 503);
            this.Controls.Add(this.hdocumentEditor1);
            this.Name = "HdocumentEditorForm";
            this.Text = "HdocumentEditorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private HdocumentEditor hdocumentEditor1;
    }
}