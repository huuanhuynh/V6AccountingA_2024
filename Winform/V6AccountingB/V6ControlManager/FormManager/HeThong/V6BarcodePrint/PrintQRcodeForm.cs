using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using H;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;
using QRCoder;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	partial class PrintQRcodeForm : V6Form
	{
		public DataTable Data { get; set; }

		public PrintQRcodeForm( )
		{
			InitializeComponent( );
            MyInit();
			cboScale.SelectedIndex = 2;
		}

        public PrintQRcodeForm(DataTable data)
        {
            InitializeComponent();
            cboScale.SelectedIndex = 2;
            Data = data;
            MyInit();
        }

        public void MyInit()
        {
            try
            {
                cboECC.SelectedIndex = qrECCLever;

                radV.Checked = V6Setting.IsVietnamese;

                setting = new Setting(Path.GetFullPath("V6BarcodeSetting.ini"));
                pageWidth = Convert.ToSingle(setting.GetSetting("PageWidth"));
                pageHeight = Convert.ToSingle(setting.GetSetting("PageHeight"));

                marginleft = Convert.ToSingle(setting.GetSetting("MarginLeft"));
                marginright = Convert.ToSingle(setting.GetSetting("MarginRight"));
                margintop = Convert.ToSingle(setting.GetSetting("MarginTop"));
                marginbottom = Convert.ToSingle(setting.GetSetting("MarginBottom"));

                distanceH = Convert.ToInt32(setting.GetSetting("DistanceH"));
                distanceV = Convert.ToInt32(setting.GetSetting("DistanceV"));
                NumberFormatInfo DecimalSeparatorFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
                //scale = float.Parse(setting.GetSetting("Scale"), DecimalSeparatorFormat);
                var numStampWidthValue = ObjectAndString.ObjectToDecimal(setting.GetSetting("StampWidth"));
                if (numStampWidthValue > 0) numStampWidth.Value = numStampWidthValue;
                var numStampHeightValue = ObjectAndString.ObjectToDecimal(setting.GetSetting("StampHeight"));
                if (numStampHeightValue > 0) numStampHeight.Value = numStampHeightValue;
                float.TryParse(setting.GetSetting("Scale"),
                    NumberStyles.Float, DecimalSeparatorFormat, out scale);
                var barcodetypeString = setting.GetSetting("BarcodeType");
                
                

                float.TryParse(setting.GetSetting("CodeTextFontSize"),
                    NumberStyles.Float, DecimalSeparatorFormat, out codetextfontsize);
                float.TryParse(setting.GetSetting("NameTextFontSize"),
                    NumberStyles.Float, DecimalSeparatorFormat, out nametextfontsize);
                float.TryParse(setting.GetSetting("PriceTextFontSize"),
                    NumberStyles.Float, DecimalSeparatorFormat, out pricetextfontsize);

                nametextfontbold = "1" == setting.GetSetting("NameTextFontBold");
                codetextfontbold = "1" == setting.GetSetting("CodeTextFontBold");
                pricetextfontbold = "1" == setting.GetSetting("PriceTextFontBold");
                nametextcandrop = "1" == setting.GetSetting("NameTextCanDrop");

                ThousandGroup = setting.GetSetting("ThousandGroup");
                DecimalGroup = setting.GetSetting("DecimalGroup");

                CurrencyType = setting.GetSetting("CurrencyType");
                AnotherCurrencySymbol = setting.GetSetting("AnotherCurrencySymbol");
                LAN = setting.GetSetting("LAN");

                if (setting.GetSetting("ForcePrint") == "1")
                {
                    Print();
                }
                else
                {
                    //Application.EnableVisualStyles();
                    //Application.EnableRTLMirroring( );
                    //Application.Run(new PrintQRcodeForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

	    

	    public string DBFpath = "";
        //public DataTable data;
        public Setting setting;
        public int barcodecount = 0;

        public float pageWidth = 75, pageHeight = 20,
            marginleft = 0, marginright = 0, margintop = 0, marginbottom = 0;

        public float scale = 1f;
        public int qrECCLever = 0;
        public float codetextfontsize = 8.25f;
        public float nametextfontsize = 8.25f;
        public float pricetextfontsize = 8.25f;
        public bool nametextfontbold = false;
        public bool codetextfontbold = false;
        public bool pricetextfontbold = false;
        public bool nametextcandrop = false;
        public int distanceH = 0, distanceV = 0;

        //public bool useAnotherSymbol = false;
        public string AnotherCurrencySymbol = "$";
        public string CurrencyType = "1";
        public string LAN = "V";
        public string ThousandGroup = ",";
        public string DecimalGroup = ".";

        //public string decimalSymbol = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

	    private bool CheckData()
        {
            string errors = "";
            Dictionary<string, string> codeList = new Dictionary<string, string>();
	        foreach (DataRow row in Data.Rows)
	        {
	            var code = row[0].ToString().Trim();
	            var name = row[1].ToString().Trim();
                if(codeList.ContainsKey(code)) continue;
                codeList.Add(code, name);
               
	        }
            if (errors.Length > 0)
            {
                this.ShowErrorMessage(errors);
                return false;
            }
	        return true;
	    }


        public void Print()
        {
            //SetBarcodeGeneralValues(_barcodeLib);
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            document.DocumentName = "V6Barcode";
            PaperSize psize = new PaperSize("V6",
                    (int)(pageWidth / 25.4f * 100),
                    (int)(pageHeight / 25.4f * 100));

            document.DefaultPageSettings.PaperSize = psize;
            
            //printview = new PrintPreviewDialog();
            //printview.Document = document;
            //printview.ShowDialog(this);

            PrintDialog printdialog1 = new PrintDialog( );
            printdialog1.AllowPrintToFile = true;
            printdialog1.PrintToFile = false;
            printdialog1.AllowSelection = false;
            printdialog1.AllowSomePages = true;

            printdialog1.UseEXDialog = true;
            printdialog1.PrinterSettings.DefaultPageSettings.PaperSize = psize;
            var tongSoTrang = TongSoTrang(psize);
            printdialog1.PrinterSettings.MaximumPage = tongSoTrang;
            printdialog1.PrinterSettings.FromPage = 1;
            printdialog1.PrinterSettings.ToPage = tongSoTrang;
            

            //printdialog1.AllowSelection = true;
            //printdialog1.AllowSomePages = true;
            //printdialog1.
            document.PrintPage += document_PrintPage;

            if (printdialog1.ShowDialog(this) == DialogResult.OK)
            {
                //printdialog1.Document = document;
                document.PrinterSettings = printdialog1.PrinterSettings;
                document.DefaultPageSettings.PaperSize = psize;
                from_page = printdialog1.PrinterSettings.FromPage;
                to_page = printdialog1.PrinterSettings.ToPage;

                //MessageBox.Show("Number of copies: " + document.PrinterSettings.Copies.ToString());
                
                document.Print();
            }
        }

        public void PrintView()
        {
            //SetBarcodeGeneralValues(_barcodeLib);
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            document.DocumentName = "V6Barcode";
            document.DefaultPageSettings.PaperSize
                = new PaperSize("V6",
                    (int)(pageWidth / 25.4f * 100),
                    (int)(pageHeight / 25.4f * 100));

            PrintPreviewDialog printview = new PrintPreviewDialog();
            printview.Width = 800;
            printview.Height = 600;
            
            printview.Document = document;
            document.PrintPage += document_PrintPage;
            printview.ShowIcon = false;
            //Tắt nút in.
            ((ToolStripButton)((ToolStrip)printview.Controls[1]).Items[0]).Enabled = false;
            printview.ShowDialog(this);
        }

        

        private int TongSoTrang(PaperSize psize)
        {
            int pagecount = 0;
            float yPos = 0f; yPos = margintop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = pageWidth;// e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = pageHeight;// e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - marginleft - marginright + distanceV)
                / ((float)numStampWidth.Value * scale + distanceV));
            if (columnsPerPage == 0)
            {
                columnsPerPage = 1;
            }
            int barcodecount0 = 0;
            int lineInPageCount = 0;
            
            //Khi chưa in hết trang
            while (barcodecount0 < Data.Rows.Count)
            {
                //barcodecount++;
                pagecount++;
                lineInPageCount = 0;
                yPos = margintop;
                do
                {
                    for (int i = 0; i < columnsPerPage; i++)
                    {
                        if (barcodecount0 < Data.Rows.Count)
                        {
                            barcodecount0++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    lineInPageCount++;
                    yPos += (float)numStampHeight.Value * scale + distanceH;
                } while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)numStampHeight.Value * scale);

            }

            return pagecount;
        }

	    private int from_page = 1;
	    private int to_page = 1;

        public void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Code in trong 1 trang
            
            float yPos = 0f; yPos = margintop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - marginleft - marginright + distanceV)
                / ((float)numStampWidth.Value * scale + distanceV));
            if (columnsPerPage == 0)
            {
                columnsPerPage = 1;
            }
            int lineInPageCount = 0;

            //Khi chưa in hết trang
            do
            {
                for (int i = 0; i < columnsPerPage; i++)
                {
                    if (barcodecount < Data.Rows.Count)
                    {
                        DataRow currentdata = Data.Rows[barcodecount];//Lấy thông tin tại barcodecount xong mới ++
                        barcodecount++;
                        
                        var barcodeProductCode = currentdata[0].ToString().Trim();//Raw_data
                        var barcodeProductName = currentdata[1].ToString().Trim();
                        var barcodeProductPrice = Convert.ToDecimal(currentdata[2]);

                        //Tính vị trí x
                        xPos = marginleft +
                            i * ((float)numStampWidth.Value * scale + distanceV);

                        DrawBarcode(e.Graphics, new Point((int)xPos, (int)yPos),
                            barcodeProductCode, barcodeProductCode, barcodeProductName, barcodeProductPrice, qrECCLever);
                    }
                    else
                    {
                        break;
                    }
                }
                lineInPageCount++;
                yPos += (float)numStampHeight.Value * scale + distanceH;
            }
            while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)numStampHeight.Value * scale);

            if (barcodecount < Data.Rows.Count)
            {
                //Còn trang tiếp theo. Hàm này sẽ lặp lại.
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                //e.Cancel = true;
            }
        }
        
        private void butDraw_Click(object sender, EventArgs e)
		{
            UpdateValues();
            DrawSampleQR();
		}

        private void DrawSampleQR()
        {
            try
            {
                Graphics g = picBarcode.CreateGraphics();

                g.FillRectangle(new SolidBrush(SystemColors.Control),
                    new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));
                DrawBarcode(g, new Point(0, 0), "QRSTRING", "MA_SAN_PHAM", "TÊN SẢN PHẨM", 150000, qrECCLever);
                g.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DrawSample " + ex.Message);
            }
        }
        

        public Bitmap RenderQrCode(Bitmap icon)
        {
            //Graphics g = picBarcode.CreateGraphics();

            //g.FillRectangle(new SolidBrush(SystemColors.Control),
            //    new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));
            Bitmap result = null;
            string level = cboECC.SelectedItem.ToString(); // 0123 LMQH
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtProductCode.Text, eccLevel))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                result = qrCode.GetGraphic(20, Color.Black, Color.White, icon, 15);
                //g.DrawImage(qr_bitmap, 0, 0, picBarcode.Width, picBarcode.Height);

                //this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                //Set the SizeMode to center the image.
                //this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;

                //pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            return result;
        }

        /// <summary>
        /// Vẽ QR lên graphics cùng các thông tin khác.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pt"></param>
        /// <param name="value"></param>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="eccLevel"></param>
        public void DrawBarcode(Graphics g, Point pt, string value,
            string productCode, string productName, decimal productPrice,
            int eccLevel)
        {   
            

            float width = (float)numStampWidth.Value * (float)numericUpDownScale.Value;
            float height = (float)numStampHeight.Value * (float)numericUpDownScale.Value;


            //	EAN13 Barcode should be a total of 113 modules wide.
            //float lineWidth = width / 113f;

            // Save the GraphicsState.
            GraphicsState gs = g.Save();

            // Set the PageUnit to Inch because all of our measurements are in inches.
            g.PageUnit = GraphicsUnit.Millimeter;

            // Set the PageScale to 1, so a millimeter will represent a true millimeter.
            g.PageScale = 1;

            SolidBrush brush = new SolidBrush(Color.Black);

            float xPosition = 0;
            float yPosition = 0;

            
            
            


            float xStart = pt.X;
            float yStart = pt.Y;
            float xEnd = xStart + width;
            float yEnd = yStart + height;

            Font fontOfNameText = new Font("Arial", this.NameTextFontSize * (float)numericUpDownScale.Value,
                this.NameTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font fontOfCodeText = new Font("Arial", this._fCodeTextFontSize * (float)numericUpDownScale.Value,
                this.CodeTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font fontOfPriceText = new Font("Arial", this.PriceTextFontSize * (float)numericUpDownScale.Value,
                this.PriceTextFontBold ? FontStyle.Bold : FontStyle.Regular);

            

            SizeF textSize1 = g.MeasureString(this.ProductName, fontOfNameText);
            float nameHeight = textSize1.Height;
            if (NameTextCanDrop && textSize1.Width > width)
            {
                nameHeight *= 2;
            }
            float fTextHeight = g.MeasureString(productCode, fontOfCodeText).Height;
            //float fNameTextHeight = g.MeasureString(sTempUPC, fontOfNameText).Height;
            float fPriceTextHeight = g.MeasureString(productCode, fontOfPriceText).Height;

            // Vẽ tên sản phẩm ở dòng đầu tiên, canh giữa và không tràn ra 2 bên.
            //float nameLeft = (width - g.MeasureString(ProductName, fontOfNameText).Width)/2;
            //if (nameLeft < 0) nameLeft = 0;

            float aWidth = g.MeasureString("A", fontOfCodeText).Width;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            //g.DrawString(ProductName, fontOfNameText, brush, new PointF(xStart + nameLeft, pt.Y));
            g.DrawString(ProductName, fontOfNameText, brush, new RectangleF(xStart, yStart, width, nameHeight), stringFormat);

            yStart += nameHeight;

            // Vẽ QR vào vùng vẽ.
            Bitmap QR = RenderQrCode(null);
            // Tính toán kích thước QR
            float QR_width = width;
            float QR_height = height - nameHeight - fPriceTextHeight;
            g.DrawImage(QR, xStart, yStart, QR_width, QR_height);

            

            //Vẽ phần giá bên dưới
            //if(Program.VND)
            g.DrawString(
                PriceText
                + " " + productPrice.ToString(NumberFormatString, numberformatinfo) + " "
                + UnitText,
                fontOfPriceText, brush,
                new PointF(xStart + aWidth, pt.Y + height - fPriceTextHeight));

            //yStart += g.MeasureString(PriceText, fontOfPriceText).Height;

            // Restore the GraphicsState.
            g.Restore(gs);
        }


        private float _fCodeTextFontSize = 8.0f;
        /// <summary>
        /// Cỡ chữ cho phần tên sản phẩm.
        /// </summary>
        public float NameTextFontSize = 8f;
        /// <summary>
        /// Cỡ chữ cho phần chữ in giá.
        /// </summary>
        public float PriceTextFontSize = 8f;
        /// <summary>
        /// Có in đậm phần tên hay không?
        /// </summary>
        public bool NameTextFontBold = false;
        /// <summary>
        /// Có in đậm phần mã hay không?
        /// </summary>
        [DefaultValue(false)]
        public bool CodeTextFontBold { get; set; }
        [DefaultValue(false)]
        public bool PriceTextFontBold { get; set; }
        [DefaultValue(false)]
        public bool NameTextCanDrop { get; set; }

        public string PriceText = "Giá:", UnitText = "VND";
        public string NumberFormatString = "0,0.";
        public NumberFormatInfo numberformatinfo
            = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();


        private void frmEan13_Load(object sender, EventArgs e)
        {
            

            numericUpDownNgang.Value = (decimal)pageWidth;
            numericUpDownDoc.Value = (decimal)pageHeight;
            numericUpDownScale.Value = (decimal) scale * 100;

            numericUpDownCodeTextFontSize.Value = (decimal)codetextfontsize;
            numericUpDownNameTextFontSize.Value = (decimal)nametextfontsize;
            numericUpDownPriceTextFontSize.Value = (decimal)pricetextfontsize;
            chkNameTextBold.Checked = nametextfontbold;
            chkCodeTextBold.Checked = codetextfontbold;
            chkPriceTextBold.Checked = pricetextfontbold;
            chkNameDrop.Checked = nametextcandrop;

            numericUpDownMarginLeft.Value = (decimal)marginleft;
            numericUpDownMarginRight.Value = (decimal)marginright;
            numericUpDownMarginTop.Value = (decimal)margintop;
            numericUpDownMarginBottom.Value = (decimal)marginbottom;

            numericUpDownDistanceH.Value = distanceH;
            numericUpDownDistanceV.Value = distanceV;

            if (CurrencyType == "1") radVND.Checked = true;
            else if (CurrencyType == "0") radUSD.Checked = true;
            else radKhac.Checked = true;

            if (LAN == "V") radV.Checked = true; else radE.Checked = true;

            LoadLanguage();

            DrawSampleQR();
        }

        void LoadLanguage0()
        {
            if (radV.Checked)
            {
                //Load Vietnamese
                butDraw.Text = "Xem mẫu";
                btnPrintView.Text = "Xem trước";
                btnPrint.Text = "In barcode";

                grbPaperSize.Text = "Kích thước trang in (mm)";
                lblNgang.Text = "Ngang";
                lblDoc.Text = "Dọc";
                grbStampSize.Text = "Kích thước tem (mm)";
                lblTemNgang.Text = "Ngang";
                lblTemDoc.Text = "Dọc";
                grbPageMargin.Text = "Canh lề trang in (mm)";
                lblTrai.Text = "Trái";
                lblPhai.Text = "Phải";
                lblTren.Text = "Trên";
                lblDuoi.Text = "Dưới";
                
                lblTen.Text = "Tên";
                lblMa.Text = "Mã";
                lblGia.Text = "Giá";

                lblKhoangCachCotVaHang.Text = "Khoảng cách cột _ hàng";
                
                
                lblTyLeCoGian.Text = "Tỷ lệ co giãn (%)";
                grbFontSizeBold.Text = "Cỡ chữ  /  In đậm  /  Xuống dòng";
                chkNameTextBold.Text = "";
                grbDonViTien.Text = "Đơn vị tiền";
                radKhac.Text = AnotherCurrencySymbol;
                grbLang.Text = "Ngôn ngữ";

            }
            else
            {
                //Load English
                butDraw.Text = "Show template";
                btnPrintView.Text = "Preview";
                btnPrint.Text = "Print";

                grbPaperSize.Text = "Paper size (mm)";
                lblNgang.Text = "Horizontal";
                lblDoc.Text = "Vertical";
                grbStampSize.Text = "Stamp size (mm)";
                lblTemNgang.Text = "Horizontal";
                lblTemDoc.Text = "Vertical";
                grbPageMargin.Text = "Margin (mm)";
                lblTrai.Text = "Left";
                lblPhai.Text = "Right";
                lblTren.Text = "Top";
                lblDuoi.Text = "Bottom";

                lblTen.Text = "Name";
                lblMa.Text = "Code";
                lblGia.Text = "Price";

                lblKhoangCachCotVaHang.Text = "Columns _ rows distance";
                

                lblTyLeCoGian.Text = "Scale (%)";
                grbFontSizeBold.Text = "Font size  /  Bold  /  Drop";
                chkNameTextBold.Text = "";
                grbDonViTien.Text = "Currency unit";
                radKhac.Text = AnotherCurrencySymbol;
                grbLang.Text = "Language";
            }
        }

        void UpdateValues()
        {
            qrECCLever = cboECC.SelectedIndex;
            pageWidth = (int)numericUpDownNgang.Value;
            pageHeight = (int)numericUpDownDoc.Value;
            scale = (float)numericUpDownScale.Value / 100f;
            codetextfontsize = (float)numericUpDownCodeTextFontSize.Value;
            nametextfontsize = (float)numericUpDownNameTextFontSize.Value;
            pricetextfontsize = (float)numericUpDownPriceTextFontSize.Value;
            nametextfontbold = chkNameTextBold.Checked;
            codetextfontbold = chkCodeTextBold.Checked;
            pricetextfontbold = chkPriceTextBold.Checked;
            nametextcandrop = chkNameDrop.Checked;

            marginleft = (int)numericUpDownMarginLeft.Value;
            marginright = (int)numericUpDownMarginRight.Value;
            margintop = (int)numericUpDownMarginTop.Value;
            marginbottom = (int)numericUpDownMarginBottom.Value;

            distanceH = (int)numericUpDownDistanceH.Value;
            distanceV = (int)numericUpDownDistanceV.Value;

            CurrencyType = radVND.Checked?"1":(radUSD.Checked?"0":"2");
            
            LAN = radV.Checked ? "V" : "E";

            
            //setting.SetSetting("BarcodeType", cboBarcodeType.SelectedItem.ToString());
            setting.SetSetting("PageWidth", pageWidth.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("PageHeight", pageHeight.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("StampWidth", numStampWidth.Value.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("StampHeight", numStampHeight.Value.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("Scale", scale.ToString(CultureInfo.InvariantCulture));//format(F) -000.00
            setting.SetSetting("CodeTextFontSize", codetextfontsize.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("NameTextFontSize", nametextfontsize.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("PriceTextFontSize", pricetextfontsize.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("NameTextFontBold", nametextfontbold ? "1" : "0");
            setting.SetSetting("CodeTextFontBold", codetextfontbold ? "1" : "0");
            setting.SetSetting("PriceTextFontBold", pricetextfontbold ? "1" : "0");
            setting.SetSetting("NameTextCanDrop", nametextcandrop ? "1" : "0");

            setting.SetSetting("MarginLeft", marginleft.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("MarginRight", marginright.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("MarginTop", margintop.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("MarginBottom", marginbottom.ToString(CultureInfo.InvariantCulture));
            setting.SetSetting("DistanceH", distanceH.ToString());
            setting.SetSetting("DistanceV", distanceV.ToString());

            setting.SetSetting("CurrencyType", CurrencyType);
            //AnotherCurrencySymbol edit by user, setting load only
            setting.SetSetting("LAN", LAN);
            
            setting.SaveSetting();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            UpdateValues();
            if (CheckData())
            {
                Print();
            }
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            UpdateValues();
            if (CheckData())
            {
                PrintView();
            }
        }

        private void radV_CheckedChanged(object sender, EventArgs e)
        {
            LoadLanguage();
        }

	}
}

