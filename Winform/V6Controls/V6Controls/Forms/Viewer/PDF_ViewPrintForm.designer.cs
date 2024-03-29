﻿namespace V6Controls.Forms.Viewer
{
    partial class PDF_ViewPrintForm
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
            this.timerViewReport = new System.Windows.Forms.Timer(this.components);
            this.pdfViewer1 = new PdfiumViewer.PdfViewer();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDownloadPDF = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerViewReport
            // 
            this.timerViewReport.Interval = 333;
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pdfViewer1.DefaultPrintMode = PdfiumViewer.PdfPrintMode.ShrinkToMargin;
            this.pdfViewer1.Location = new System.Drawing.Point(1, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.ShowToolbar = false;
            this.pdfViewer1.Size = new System.Drawing.Size(906, 644);
            this.pdfViewer1.TabIndex = 4;
            this.pdfViewer1.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            this.pdfViewer1.Load += new System.EventHandler(this.pdfViewer1_Load);
            // 
            // btnHuy
            // 
            this.btnHuy.AccessibleDescription = "REPORTB00005";
            this.btnHuy.AccessibleName = "";
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = global::V6Controls.Properties.Resources.Cancel;
            this.btnHuy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHuy.Location = new System.Drawing.Point(100, 650);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 40);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDownloadPDF
            // 
            this.btnDownloadPDF.AccessibleName = "";
            this.btnDownloadPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadPDF.Image = global::V6Controls.Properties.Resources.DownloadPDF;
            this.btnDownloadPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDownloadPDF.Location = new System.Drawing.Point(715, 650);
            this.btnDownloadPDF.Name = "btnDownloadPDF";
            this.btnDownloadPDF.Size = new System.Drawing.Size(88, 40);
            this.btnDownloadPDF.TabIndex = 1;
            this.btnDownloadPDF.Text = "Tải PDF";
            this.btnDownloadPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDownloadPDF.UseVisualStyleBackColor = true;
            this.btnDownloadPDF.Click += new System.EventHandler(this.btnDownloadPDF_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleName = "";
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = global::V6Controls.Properties.Resources.Print;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(809, 650);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 40);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.AccessibleDescription = "REPORTB00004";
            this.btnNhan.AccessibleName = "";
            this.btnNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNhan.Image = global::V6Controls.Properties.Resources.Apply;
            this.btnNhan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNhan.Location = new System.Drawing.Point(6, 650);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(88, 40);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Text = "&Nhận";
            this.btnNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click);
            // 
            // PDF_ViewPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 696);
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDownloadPDF);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnNhan);
            this.Name = "PDF_ViewPrintForm";
            this.Text = "PDF viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AAPPR_SOA3_ViewPDF_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerViewReport;
        protected System.Windows.Forms.Button btnNhan;
        protected System.Windows.Forms.Button btnHuy;
        protected System.Windows.Forms.Button btnPrint;
        private PdfiumViewer.PdfViewer pdfViewer1;
        protected System.Windows.Forms.Button btnDownloadPDF;




    }
}