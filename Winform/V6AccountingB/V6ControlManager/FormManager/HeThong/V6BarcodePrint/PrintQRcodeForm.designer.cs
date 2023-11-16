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
            this.components = new System.ComponentModel.Container();
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
            this.txtProductName2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.btnReloadSetting = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnIcon = new System.Windows.Forms.Button();
            this.lblSetting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(6, 153);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(125, 20);
            this.txtProductName.TabIndex = 5;
            this.txtProductName.Text = "TÊN SẢN PHẨM";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(6, 114);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(125, 20);
            this.txtProductCode.TabIndex = 3;
            this.txtProductCode.Text = "20231114164300";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = ".";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "QR VALUE";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = ".";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "VIEW CODE";
            // 
            // txtQRcode
            // 
            this.txtQRcode.Location = new System.Drawing.Point(6, 36);
            this.txtQRcode.Multiline = true;
            this.txtQRcode.Name = "txtQRcode";
            this.txtQRcode.Size = new System.Drawing.Size(125, 59);
            this.txtQRcode.TabIndex = 1;
            this.txtQRcode.Text = "V6QRCODE2023SP~1234567890~ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(6, 231);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(125, 20);
            this.txtPrice.TabIndex = 9;
            this.txtPrice.Text = "120000";
            // 
            // label10
            // 
            this.label10.AccessibleDescription = ".";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "VIEW PRICE";
            // 
            // btnPrintView
            // 
            this.btnPrintView.AccessibleDescription = "V6REASKB00004";
            this.btnPrintView.Image = global::V6ControlManager.Properties.Resources.ViewDetails;
            this.btnPrintView.Location = new System.Drawing.Point(570, 442);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(107, 40);
            this.btnPrintView.TabIndex = 5;
            this.btnPrintView.Text = "Xem trước";
            this.btnPrintView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintView.UseVisualStyleBackColor = true;
            this.btnPrintView.Click += new System.EventHandler(this.btnPrintView_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleDescription = "V6REASKB00003";
            this.btnPrint.Image = global::V6ControlManager.Properties.Resources.Print;
            this.btnPrint.Location = new System.Drawing.Point(683, 442);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(138, 55);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "In barcode";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // picBarcode
            // 
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Location = new System.Drawing.Point(137, 13);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(394, 402);
            this.picBarcode.TabIndex = 2;
            this.picBarcode.TabStop = false;
            // 
            // btnDrawSample
            // 
            this.btnDrawSample.AccessibleDescription = "V6REASKB00005";
            this.btnDrawSample.Image = global::V6ControlManager.Properties.Resources.Barcode24;
            this.btnDrawSample.Location = new System.Drawing.Point(10, 343);
            this.btnDrawSample.Name = "btnDrawSample";
            this.btnDrawSample.Size = new System.Drawing.Size(107, 40);
            this.btnDrawSample.TabIndex = 13;
            this.btnDrawSample.Text = "Xem mẫu";
            this.btnDrawSample.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrawSample.Click += new System.EventHandler(this.butDraw_Click);
            // 
            // labelECC
            // 
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(6, 254);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(57, 13);
            this.labelECC.TabIndex = 10;
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
            this.cboECC.Location = new System.Drawing.Point(6, 270);
            this.cboECC.Name = "cboECC";
            this.cboECC.Size = new System.Drawing.Size(125, 21);
            this.cboECC.TabIndex = 11;
            this.cboECC.SelectedIndexChanged += new System.EventHandler(this.cboECC_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(-1, 28);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(279, 461);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDrawSample);
            this.groupBox1.Controls.Add(this.txtProductName2);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.labelECC);
            this.groupBox1.Controls.Add(this.btnIcon);
            this.groupBox1.Controls.Add(this.txtProductCode);
            this.groupBox1.Controls.Add(this.cboECC);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtQRcode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.picBarcode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(284, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 421);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mẫu";
            // 
            // txtProductName2
            // 
            this.txtProductName2.Location = new System.Drawing.Point(6, 192);
            this.txtProductName2.Name = "txtProductName2";
            this.txtProductName2.Size = new System.Drawing.Size(125, 20);
            this.txtProductName2.TabIndex = 7;
            this.txtProductName2.Text = "ABC/123/XYZ";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = ".";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "VIEW NAME 2";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = ".";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "VIEW NAME";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.AccessibleDescription = "";
            this.btnSaveSetting.Image = global::V6ControlManager.Properties.Resources.Apply;
            this.btnSaveSetting.Location = new System.Drawing.Point(289, 442);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(107, 40);
            this.btnSaveSetting.TabIndex = 3;
            this.btnSaveSetting.Text = "Lưu Setting";
            this.btnSaveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // btnReloadSetting
            // 
            this.btnReloadSetting.AccessibleDescription = "";
            this.btnReloadSetting.Image = global::V6ControlManager.Properties.Resources.Refresh;
            this.btnReloadSetting.Location = new System.Drawing.Point(402, 442);
            this.btnReloadSetting.Name = "btnReloadSetting";
            this.btnReloadSetting.Size = new System.Drawing.Size(107, 40);
            this.btnReloadSetting.TabIndex = 4;
            this.btnReloadSetting.Text = "Tải Setting";
            this.btnReloadSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReloadSetting.UseVisualStyleBackColor = true;
            this.btnReloadSetting.Click += new System.EventHandler(this.btnReloadSetting_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnIcon
            // 
            this.btnIcon.AccessibleDescription = "";
            this.btnIcon.Image = global::V6ControlManager.Properties.Resources.Add;
            this.btnIcon.Location = new System.Drawing.Point(10, 297);
            this.btnIcon.Name = "btnIcon";
            this.btnIcon.Size = new System.Drawing.Size(107, 40);
            this.btnIcon.TabIndex = 12;
            this.btnIcon.Text = "Chọn Logo";
            this.btnIcon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIcon.UseVisualStyleBackColor = true;
            this.btnIcon.Click += new System.EventHandler(this.btnIcon_Click);
            // 
            // lblSetting
            // 
            this.lblSetting.AutoSize = true;
            this.lblSetting.Location = new System.Drawing.Point(12, 9);
            this.lblSetting.Name = "lblSetting";
            this.lblSetting.Size = new System.Drawing.Size(40, 13);
            this.lblSetting.TabIndex = 0;
            this.lblSetting.Text = "Setting";
            // 
            // PrintQRcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(824, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.lblSetting);
            this.Controls.Add(this.btnReloadSetting);
            this.Controls.Add(this.btnSaveSetting);
            this.Controls.Add(this.btnPrintView);
            this.Controls.Add(this.btnPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintQRcodeForm";
            this.Text = "V6Barcode";
            this.Load += new System.EventHandler(this.Form_Load);
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
        private System.Windows.Forms.Button btnReloadSetting;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnIcon;
        private System.Windows.Forms.TextBox txtProductName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSetting;
    }
}

