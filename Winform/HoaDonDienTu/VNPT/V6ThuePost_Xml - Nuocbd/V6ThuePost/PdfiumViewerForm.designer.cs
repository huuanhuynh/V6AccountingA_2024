﻿namespace V6ThuePost
{
    partial class PdfiumViewerForm
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
            this.pdfViewer1 = new PdfiumViewer.PdfViewer();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.ShowToolbar = false;
            this.pdfViewer1.Size = new System.Drawing.Size(784, 412);
            this.pdfViewer1.TabIndex = 1;
            this.pdfViewer1.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // PdfiumViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.pdfViewer1);
            this.Name = "PdfiumViewerForm";
            this.Text = "PdfiumViewerForm";
            this.Load += new System.EventHandler(this.PdfiumViewerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PdfiumViewer.PdfViewer pdfViewer1;
    }
}