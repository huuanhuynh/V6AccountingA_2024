namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	partial class PrintQRcodeForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintQRcodeForm));
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQRcode = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPrintView = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.btnDrawSample = new System.Windows.Forms.Button();
            this.labelECC = new System.Windows.Forms.Label();
            this.cboECC = new System.Windows.Forms.ComboBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(5, 120);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(121, 20);
            this.txtProductName.TabIndex = 17;
            this.txtProductName.Text = "TÊN SẢN PHẨM";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(6, 77);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(121, 20);
            this.txtProductCode.TabIndex = 19;
            this.txtProductCode.Text = "201620141249";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = ".";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "QR VALUE";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = ".";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "VIEW CODE";
            // 
            // txtQRcode
            // 
            this.txtQRcode.Location = new System.Drawing.Point(6, 36);
            this.txtQRcode.Name = "txtQRcode";
            this.txtQRcode.Size = new System.Drawing.Size(125, 20);
            this.txtQRcode.TabIndex = 13;
            this.txtQRcode.Text = "V6BARCODE2016SP";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(6, 160);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 20);
            this.txtPrice.TabIndex = 23;
            this.txtPrice.Text = "120000";
            // 
            // label10
            // 
            this.label10.AccessibleDescription = ".";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "VIEW PRICE";
            // 
            // btnPrintView
            // 
            this.btnPrintView.AccessibleDescription = "V6REASKB00004";
            this.btnPrintView.Image = global::V6ControlManager.Properties.Resources.ViewDetails;
            this.btnPrintView.Location = new System.Drawing.Point(402, 449);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(107, 40);
            this.btnPrintView.TabIndex = 11;
            this.btnPrintView.Text = "Xem trước";
            this.btnPrintView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintView.UseVisualStyleBackColor = true;
            this.btnPrintView.Click += new System.EventHandler(this.btnPrintView_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleDescription = "V6REASKB00003";
            this.btnPrint.Image = global::V6ControlManager.Properties.Resources.Print;
            this.btnPrint.Location = new System.Drawing.Point(515, 434);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(138, 55);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "In barcode";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // picBarcode
            // 
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Location = new System.Drawing.Point(284, 39);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(369, 389);
            this.picBarcode.TabIndex = 2;
            this.picBarcode.TabStop = false;
            // 
            // btnDrawSample
            // 
            this.btnDrawSample.AccessibleDescription = "V6REASKB00005";
            this.btnDrawSample.Image = global::V6ControlManager.Properties.Resources.Barcode24;
            this.btnDrawSample.Location = new System.Drawing.Point(6, 200);
            this.btnDrawSample.Name = "btnDrawSample";
            this.btnDrawSample.Size = new System.Drawing.Size(107, 29);
            this.btnDrawSample.TabIndex = 26;
            this.btnDrawSample.Text = "Xem mẫu";
            this.btnDrawSample.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawSample.Click += new System.EventHandler(this.butDraw_Click);
            // 
            // labelECC
            // 
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(321, 15);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(57, 13);
            this.labelECC.TabIndex = 35;
            this.labelECC.Text = "ECC-Level";
            // 
            // cboECC
            // 
            this.cboECC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboECC.FormattingEnabled = true;
            this.cboECC.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.cboECC.Location = new System.Drawing.Point(387, 12);
            this.cboECC.Name = "cboECC";
            this.cboECC.Size = new System.Drawing.Size(120, 21);
            this.cboECC.TabIndex = 34;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(-1, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(279, 477);
            this.propertyGrid1.TabIndex = 36;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDrawSample);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtQRcode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(659, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 389);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mẫu";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = ".";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "VIEW NAME";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.AccessibleDescription = "";
            this.btnSaveSetting.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnSaveSetting.Location = new System.Drawing.Point(289, 449);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(107, 40);
            this.btnSaveSetting.TabIndex = 11;
            this.btnSaveSetting.Text = "Lưu Setting";
            this.btnSaveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // PrintQRcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(824, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.labelECC);
            this.Controls.Add(this.cboECC);
            this.Controls.Add(this.btnSaveSetting);
            this.Controls.Add(this.btnPrintView);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.picBarcode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintQRcodeForm";
            this.Text = "V6Barcode";
            this.Load += new System.EventHandler(this.frmEan13_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDrawSample;
		private System.Windows.Forms.PictureBox picBarcode;
		private System.Windows.Forms.TextBox txtProductName;
		private System.Windows.Forms.TextBox txtProductCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQRcode;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintView;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelECC;
        private System.Windows.Forms.ComboBox cboECC;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveSetting;
    }
}

