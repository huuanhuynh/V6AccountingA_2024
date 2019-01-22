using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using H;
using V6BarcodeLib;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	partial class PrintBarcodeForm : V6Form
	{
		private BarcodeLib code13 = null;
        public DataTable Data { get; set; }

		public PrintBarcodeForm( )
		{
			InitializeComponent( );
            MyInit();
			cboScale.SelectedIndex = 2;
		}

        public PrintBarcodeForm(DataTable data)
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
                barcodetype = GetBarcodeType(barcodetypeString);
                cboBarcodeType.SelectedItem = barcodetype.ToString();

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
                    //Application.Run(new PrintBarcodeForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

	    private BARCODE_TYPE GetBarcodeType(string barcodetypeString)
	    {
            var barcodetype = BARCODE_TYPE.UNSPECIFIED;
            switch (barcodetypeString)
            {
                case "UPCA": barcodetype = BARCODE_TYPE.UPCA; break;
                case "UPCE": barcodetype = BARCODE_TYPE.UPCE; break;
                case "UPC_SUPPLEMENTAL_2DIGIT": barcodetype = BARCODE_TYPE.UPC_SUPPLEMENTAL_2DIGIT; break;
                case "UPC_SUPPLEMENTAL_5DIGIT": barcodetype = BARCODE_TYPE.UPC_SUPPLEMENTAL_5DIGIT; break;
                case "EAN13": barcodetype = BARCODE_TYPE.EAN13; break;
                case "EAN8": barcodetype = BARCODE_TYPE.EAN8; break;
                case "Interleaved2of5": barcodetype = BARCODE_TYPE.Interleaved2of5; break;
                case "Standard2of5": barcodetype = BARCODE_TYPE.Standard2of5; break;
                case "Industrial2of5": barcodetype = BARCODE_TYPE.Industrial2of5; break;
                case "CODE39": barcodetype = BARCODE_TYPE.CODE39; break;
                case "CODE39Extended": barcodetype = BARCODE_TYPE.CODE39Extended; break;
                case "CODE39_Mod43": barcodetype = BARCODE_TYPE.CODE39_Mod43; break;
                case "Codabar": barcodetype = BARCODE_TYPE.Codabar; break;
                case "PostNet": barcodetype = BARCODE_TYPE.PostNet; break;
                case "BOOKLAND": barcodetype = BARCODE_TYPE.BOOKLAND; break;
                case "ISBN": barcodetype = BARCODE_TYPE.ISBN; break;
                case "JAN13": barcodetype = BARCODE_TYPE.JAN13; break;
                case "MSI_Mod10": barcodetype = BARCODE_TYPE.MSI_Mod10; break;
                case "MSI_2Mod10": barcodetype = BARCODE_TYPE.MSI_2Mod10; break;
                case "MSI_Mod11": barcodetype = BARCODE_TYPE.MSI_Mod11; break;
                case "MSI_Mod11_Mod10": barcodetype = BARCODE_TYPE.MSI_Mod11_Mod10; break;
                case "Modified_Plessey": barcodetype = BARCODE_TYPE.Modified_Plessey; break;
                case "CODE11": barcodetype = BARCODE_TYPE.CODE11; break;
                case "USD8": barcodetype = BARCODE_TYPE.USD8; break;
                case "UCC12": barcodetype = BARCODE_TYPE.UCC12; break;
                case "UCC13": barcodetype = BARCODE_TYPE.UCC13; break;
                case "LOGMARS": barcodetype = BARCODE_TYPE.LOGMARS; break;
                case "CODE128": barcodetype = BARCODE_TYPE.CODE128; break;
                case "CODE128A": barcodetype = BARCODE_TYPE.CODE128A; break;
                case "CODE128B": barcodetype = BARCODE_TYPE.CODE128B; break;
                case "CODE128C": barcodetype = BARCODE_TYPE.CODE128C; break;
                case "ITF14": barcodetype = BARCODE_TYPE.ITF14; break;
                case "CODE93": barcodetype = BARCODE_TYPE.CODE39; break;
                case "TELEPEN": barcodetype = BARCODE_TYPE.TELEPEN; break;
                case "FIM": barcodetype = BARCODE_TYPE.FIM; break;
                case "PHARMACODE": barcodetype = BARCODE_TYPE.PHARMACODE; break;
                default:
                    barcodetype = BARCODE_TYPE.UNSPECIFIED;
                    break;
            }
	        return barcodetype;
	    }

	    private void CreateEan13()
		{
            code13 = new BarcodeLib();
            code13.EncodedBarcodeType = barcodetype;
	        code13.Width = (float)numStampWidth.Value;
	        code13.Height = (float)numStampHeight.Value;
            code13.EncodedBarcodeType = barcodetype;
            code13.RawData = txtProductCode.Text;
            code13.ProductName = txtProductName.Text;
			code13.CountryCode = txtCountryCode.Text;
			//code13.ManufacturerCode = txtManufacturerCode.Text;
			code13.ProductCode = txtProductCode.Text;
            code13.ProductPrice = decimal.Parse(txtPrice.Text);
            code13.Scale = scale;
            code13.CodeTextFontSize = codetextfontsize;
            code13.NameTextFontSize = nametextfontsize;
            code13.PriceTextFontSize = pricetextfontsize;
            code13.NameTextFontBold = nametextfontbold;
            code13.CodeTextFontBold = codetextfontbold;
            code13.PriceTextFontBold = pricetextfontbold;
            code13.NameTextCanDrop = nametextcandrop;
		}

        public string DBFpath = "";
        //public DataTable data;
        public Setting setting;
        public int barcodecount = 0;

        public float pageWidth = 75, pageHeight = 20,
            marginleft = 0, marginright = 0, margintop = 0, marginbottom = 0;

        public float scale = 1f;
        BARCODE_TYPE barcodetype = BARCODE_TYPE.CODE128;
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
                try
                {
                    var s = _barcodeLib.Encode(barcodetype, code);
                }
                catch (Exception ex)
                {
                    errors += string.Format("<{0}> {1}\n", code, ex.Message);
                }
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
            SetBarcodeGeneralValues(_barcodeLib);
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

            PrintDialog printdialog1 = new PrintDialog();
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
            SetBarcodeGeneralValues(_barcodeLib);
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

        BarcodeLib _barcodeLib = new BarcodeLib();
        public void SetBarcodeGeneralValues(BarcodeLib bar)
        {
            bar.Width = (float)numStampWidth.Value;
            bar.Height = (float)numStampHeight.Value;
            bar.Scale = scale;
            bar.EncodedBarcodeType = barcodetype;
            bar.CodeTextFontSize = codetextfontsize;
            bar.NameTextFontSize = nametextfontsize;
            bar.PriceTextFontSize = pricetextfontsize;
            bar.NameTextFontBold = nametextfontbold;
            bar.CodeTextFontBold = codetextfontbold;
            bar.PriceTextFontBold = pricetextfontbold;
            bar.NameTextCanDrop = nametextcandrop;

            if (CurrencyType == "0")
                bar.UnitText = "USD";
            else if (CurrencyType == "1")
                bar.UnitText = "VNĐ";
            else
                bar.UnitText = AnotherCurrencySymbol;
            bar.PriceText = LAN == "V" ? "Giá:" : "Price:";
            bar.numberformatinfo.NumberGroupSeparator = ThousandGroup;
            bar.numberformatinfo.NumberDecimalSeparator = DecimalGroup;
            //bar.numberformatinfo.CurrencyDecimalDigits = VND ? 0 : 2;

            bar.NumberFormatString = "0,0." + (CurrencyType == "0" ? "00" : "");
        }

        private int TongSoTrang(PaperSize psize)
        {
            int pagecount = 0;
            float yPos = 0f; yPos = margintop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = pageWidth;// e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = pageHeight;// e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - marginleft - marginright + distanceV)
                / (_barcodeLib.Width * scale + distanceV));
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
                    yPos += _barcodeLib.Height * scale + distanceH;
                } while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - _barcodeLib.Height * scale);

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
                / (_barcodeLib.Width * scale + distanceV));
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
                            i * (_barcodeLib.Width * scale + distanceV);

                        _barcodeLib.DrawBarcode(e.Graphics, new Point((int)xPos, (int)yPos),
                            barcodeProductCode, barcodeProductCode, barcodeProductName, barcodeProductPrice);
                    }
                    else
                    {
                        break;
                    }
                }
                lineInPageCount++;
                yPos += _barcodeLib.Height * scale + distanceH;
            }
            while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - _barcodeLib.Height * scale);

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
            DrawSample();
		}

        private void DrawSample()
        {
            try
            {
                Graphics g = picBarcode.CreateGraphics();

                g.FillRectangle(new SolidBrush(SystemColors.Control),
                    new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));

                CreateEan13();
                SetBarcodeGeneralValues(code13);
                code13.DrawBarcode(g, new Point(0, 0));
                g.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DrawSample " + ex.Message);
            }
        }

		private void butPrint_Click(object sender, EventArgs e)
		{
			PrintDocument pd = new PrintDocument();
		    pd.DocumentName = "V6Barcode";
            pd.DefaultPageSettings.PaperSize = new PaperSize("V6Barcode",
                    (int)(numericUpDownNgang.Value / 25.4m * 100),
                    (int)(numericUpDownDoc.Value / 25.4m * 100));
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
			pd.Print( );
		}

		private void pd_PrintPage( object sender, PrintPageEventArgs ev )
		{
			CreateEan13();
			code13.Scale = ( float )Convert.ToDecimal( cboScale.Items [cboScale.SelectedIndex] );
            //code13.DrawBarcode( ev.Graphics, new Point( 0, 0 ) );
            //txtChecksumDigit.Text = code13.ChecksumDigit;
            
			// Add Code here to print other information.
			ev.Graphics.Dispose( );
		}

		private void butCreateBitmap_Click(object sender, EventArgs e)
		{
			CreateEan13();
			code13.Scale = ( float )Convert.ToDecimal( cboScale.Items [cboScale.SelectedIndex] );
            DrawSample();
		}

        
        

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

            DrawSample();
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
                lblBarcodeType.Text = "Loại barcode (Chưa hỗ trợ đầy đủ)";
                
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
                lblBarcodeType.Text = "Barcode type (Not yet fully supported)";

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
            barcodetype = GetBarcodeType(cboBarcodeType.SelectedItem.ToString());
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

            
            setting.SetSetting("BarcodeType", cboBarcodeType.SelectedItem.ToString());
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

