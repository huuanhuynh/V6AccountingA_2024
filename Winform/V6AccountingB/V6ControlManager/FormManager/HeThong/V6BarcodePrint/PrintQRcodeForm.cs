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
using System.Reflection;
using V6AccountingBusiness;
using V6Tools.V6Objects;

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	partial class PrintQRcodeForm : V6Form
	{
        QRcodePrintSetting _qrSetting;
		public DataTable Data { get; set; }
        public AlbcConfig _albcConfig;
        public string DBFpath = "";
        //public DataTable data;
        //public Setting setting;

        public PrintQRcodeForm( )
		{
			InitializeComponent( );
            MyInit();
		}

        public PrintQRcodeForm(DataTable data)
        {
            InitializeComponent();
            Data = data;
            MyInit();
        }

        public PrintQRcodeForm(DataTable data, AlbcConfig _albcConfig)
        {
            InitializeComponent();
            Data = data;
            this._albcConfig = _albcConfig;
            MyInit();
        }

        public void MyInit()
        {
            try
            {
                _qrSetting = new QRcodePrintSetting();
                if (_albcConfig.EXTRA_INFOR == null || _albcConfig.EXTRA_INFOR.Count == 0)
                {
                    GenDefaultSetting();
                }
                else
                {
                    LoadSetting();
                }
                

                cboECC.SelectedIndex = 0;
                propertyGrid1.SelectedObject = _qrSetting;

                if (_qrSetting.ForcePrint)
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

        private void Form_Load(object sender, EventArgs e)
        {
            LoadLanguage();
            timer1.Start();
        }


        private void LoadSetting()
        {
            try
            {
                _qrSetting.LoadSetting(_albcConfig.EXTRA_INFOR);
                // Load icon
                LoadIconFromSetting();
            }
            catch (Exception ex)
            {
                V6Message.ShowErrorMessage("LoadSetting " + ex.Message, this);
            }
        }

        public void LoadIconFromSetting()
        {
            string file = Path.Combine(V6Login.StartupPath, "Pictures\\QRLOGO\\" + _qrSetting.LogoFileName);
            if (!string.IsNullOrEmpty(_qrSetting.LogoFileName) && File.Exists(file))
            {
                LoadIcon(file);
            }
            else
            {
                _icon = null;
                btnIcon.Image = new Bitmap(32,32);
            }
        }

        private void GenDefaultSetting()
        {
            try
            {
                _qrSetting.PageWidth = 120;
                _qrSetting.PageHeight = 80;
                _qrSetting.Rotate = false;
                _qrSetting.MarginLeft = 1;
                _qrSetting.MarginRight = 1;
                _qrSetting.MarginTop = 1;
                _qrSetting.MarginBottom = 1;

                _qrSetting.DistanceH = 3;
                _qrSetting.DistanceV = 3;

                _qrSetting.StampWidth = 50;
                _qrSetting.StampHeight = 50;
                _qrSetting.Scale = 1;

                _qrSetting.CodeTextFontSize = 8;
                _qrSetting.NameTextFontSize = 8;
                _qrSetting.PriceTextFontSize = 8;
                _qrSetting.NameTextFontBold = true;
                _qrSetting.CodeTextFontBold = true;
                _qrSetting.PriceTextFontBold = true;
                _qrSetting.NameTextCanDrop = true;
                _qrSetting.ShowCode = false;
                _qrSetting.ShowName = false;
                _qrSetting.ShowName2 = false;
                _qrSetting.ShowPrice = false;
                _qrSetting.QRSquare = false;

                _qrSetting.ForcePrint = false;

                _qrSetting.ThousandGroup = " ";
                _qrSetting.DecimalGroup = ",";
                _qrSetting.PriceDecimals = 2;

                _qrSetting.CurrencyType = "1";
                //_qrSetting.AnotherCurrencySymbol = "$";
                //_qrSetting.LAN = "V";
                _qrSetting.UnitText = "VND";

                string EXTRA_INFOR = _qrSetting.ToStringDICTIONARY();
                _albcConfig.DATA["EXTRA_INFOR"] = EXTRA_INFOR;
                _albcConfig.LoadExtraInfor();

                //_albcConfig.UpdateSimple("EXTRA_INFOR", EXTRA_INFOR);
                SaveSetting();
                
            }
            catch (Exception ex)
            {
                V6Message.ShowErrorMessage("GenDefaultSetting " + ex.Message, this);
            }
        }

        private void btnReloadSetting_Click(object sender, EventArgs e)
        {
            ReLoadSetting();
            DrawSampleQR();
        }

        private void SaveSetting()
        {
            if (_albcConfig != null && _albcConfig.HaveInfo)
            {
                Dictionary<string, object> settingDic = _qrSetting.ToDICTIONARY();
                //Ghép thêm EXTRA_INFOR không thuộc setting.
                foreach (var item in _albcConfig.EXTRA_INFOR)
                {
                    if (!settingDic.ContainsKey(item.Key))
                    {
                        settingDic[item.Key] = item.Value;
                    }
                }
                string EXTRA_INFOR = ObjectAndString.DictionaryToString(settingDic);
                _albcConfig.DATA["EXTRA_INFOR"] = EXTRA_INFOR;
                _albcConfig.LoadExtraInfor();
                var data = new Dictionary<string, object>();
                data["EXTRA_INFOR"] = EXTRA_INFOR;
                var key = new Dictionary<string, object>();
                key["MA_FILE"] = _albcConfig.MA_FILE;
                V6BusinessHelper.UpdateSimple("ALBC", data, key);
            }
            else
            {
                this.ShowInfoMessage(V6Text.NoDefine + " ALBC");
            }
        }

        private void ReLoadSetting()
        {
            try
            {
                var key = new Dictionary<string, object>();
                key["MA_FILE"] = _albcConfig.MA_FILE;
                string EXTRA_INFOR = "" + V6BusinessHelper.SelectOneValue("ALBC", "EXTRA_INFOR", key);
                _albcConfig.DATA["EXTRA_INFOR"] = EXTRA_INFOR;
                _albcConfig.LoadExtraInfor();

                LoadSetting();
            }
            catch (Exception ex)
            {
                V6Message.ShowErrorMessage("ReLoadSetting " + ex.Message, this);
            }
        }

        private bool CheckData()
        {
            string errors = "";
            Dictionary<string, string> qrList = new Dictionary<string, string>();
	        foreach (DataRow row in Data.Rows)
	        {
	            var qr = row[0].ToString().Trim();
	            var code = row[1].ToString().Trim();
                if(qrList.ContainsKey(qr)) continue;
                qrList.Add(qr, code);
               
	        }
            if (errors.Length > 0)
            {
                this.ShowErrorMessage(errors);
                return false;
            }
	        return true;
	    }


        int barcodecount = 0;
        public void Print()
        {
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            document.DocumentName = "V6QRcode";
            PaperSize psize = new PaperSize("V6",
                    (int)(_qrSetting.PageWidth / 25.4 * 100),
                    (int)(_qrSetting.PageHeight / 25.4 * 100));

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
                    (int)(_qrSetting.PageWidth / 25.4 * 100),
                    (int)(_qrSetting.PageHeight / 25.4 * 100));
            if (_qrSetting.Rotate)
            {
                document.DefaultPageSettings.Landscape = !document.DefaultPageSettings.Landscape;
            }
            
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
            float yPos = 0f; yPos = _qrSetting.MarginTop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = _qrSetting.PageWidth;// e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = _qrSetting.PageHeight;// e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - _qrSetting.MarginLeft - _qrSetting.MarginRight + _qrSetting.DistanceV)
                / ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV));
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
                yPos = _qrSetting.MarginTop;
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
                    yPos += (float)_qrSetting.StampHeight * _qrSetting.Scale + _qrSetting.DistanceH;
                } while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)_qrSetting.StampHeight * _qrSetting.Scale);

            }

            return pagecount;
        }

	    private int from_page = 1;
	    private int to_page = 1;

        public void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Code in trong 1 trang
            
            float yPos = 0f; yPos = _qrSetting.MarginTop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - _qrSetting.MarginLeft - _qrSetting.MarginRight + _qrSetting.DistanceV)
                / ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV));
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

                        var qr = currentdata[0].ToString().Trim();//QR value
                        var barcodeProductCode = currentdata[1].ToString().Trim();
                        var barcodeProductName = currentdata[2].ToString().Trim();
                        var barcodeProductName2 = currentdata[3].ToString().Trim();
                        var barcodeProductPrice = Convert.ToDecimal(currentdata[4]);

                        //Tính vị trí x
                        xPos = _qrSetting.MarginLeft +
                            i * ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV);

                        DrawBarcode(e.Graphics, new Point((int)xPos, (int)yPos),
                            qr, barcodeProductCode, barcodeProductName, barcodeProductName2, barcodeProductPrice, _qrSetting.ECCLever);
                    }
                    else
                    {
                        break;
                    }
                }
                lineInPageCount++;
                yPos += (float)_qrSetting.StampHeight * _qrSetting.Scale + _qrSetting.DistanceH;
            }
            while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)_qrSetting.StampHeight * _qrSetting.Scale);

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
            try
            {
                DrawSampleQR();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".butDraw_Click " + ex.Message);
            }
		}

        private void DrawSampleQR()
        {
            try
            {
                Graphics g = picBarcode.CreateGraphics();
                g.Clear(this.BackColor);
                g.PageUnit = GraphicsUnit.Millimeter;

                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, (int)_qrSetting.StampWidth, (int)_qrSetting.StampHeight));

                DrawBarcode(g, new Point(0, 0), txtQRcode.Text, txtProductCode.Text, txtProductName.Text, txtProductName2.Text,
                    ObjectAndString.ObjectToDecimal(txtPrice.Text), _qrSetting.ECCLever);
                //g.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DrawSample " + ex.Message);
            }
        }

        Bitmap _icon = null;
        public Bitmap RenderQrCode(string qr_code, Bitmap icon)
        {
            Bitmap result = null;
            string level = cboECC.SelectedItem.ToString(); // 0123 LMQH
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr_code, eccLevel))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                result = qrCode.GetGraphic(20, Color.Black, Color.White, icon, _qrSetting.LogoPercent);
            }
            return result;
        }

        /// <summary>
        /// Vẽ QR lên graphics cùng các thông tin khác.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pt"></param>
        /// <param name="value">Giá trị QR</param>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="eccLevel"></param>
        public void DrawBarcode(Graphics g, Point pt, string value,
            string productCode, string productName, string productName2, decimal productPrice,
            int eccLevel)
        {
            float width = (float)_qrSetting.StampWidth * (float)_qrSetting.Scale;
            float height = (float)_qrSetting.StampHeight * (float)_qrSetting.Scale;

            // Save the GraphicsState.
            GraphicsState gs = g.Save();

            // Set the PageUnit to Milimet because all of our measurements are in inches.
            g.PageUnit = GraphicsUnit.Millimeter;

            // Set the PageScale to 1, so a millimeter will represent a true millimeter.
            g.PageScale = 1;

            SolidBrush brush = new SolidBrush(Color.Black);
            
            float xStart = pt.X;
            float yStart = pt.Y;
            float xEnd = xStart + width;
            float yEnd = yStart + height;

            Font name_font = new Font("Arial", _qrSetting.NameTextFontSize * _qrSetting.Scale,
                _qrSetting.NameTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font code_font = new Font("Arial", _qrSetting.CodeTextFontSize * _qrSetting.Scale,
                _qrSetting.CodeTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font price_font = new Font("Arial", _qrSetting.PriceTextFontSize * _qrSetting.Scale,
                _qrSetting.PriceTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            

            SizeF code_size = g.MeasureString(productCode, code_font);
            float code_Height = code_size.Height;
            if (_qrSetting.CodeTextCanDrop && code_size.Width > width)
            {
                code_Height *= 2;
            }
            if (!_qrSetting.ShowCode)
            {
                code_Height = 0;
            }
            SizeF name_size = g.MeasureString(productName, name_font);
            float name_Height = name_size.Height;
            if (_qrSetting.NameTextCanDrop && name_size.Width > width)
            {
                name_Height *= 2;
            }
            if (!_qrSetting.ShowName)
            {
                name_Height = 0;
            }

            float price_Height = g.MeasureString(productCode, price_font).Height;
            if (!_qrSetting.ShowPrice)
            {
                price_Height = 0;
            }

            float aWidth = g.MeasureString("A", code_font).Width;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            yStart += (_qrSetting.ShowCode ? (code_size.Height - 2f) : 0);
            // Vẽ QR vào vùng vẽ.
            Bitmap QR = RenderQrCode(value, _icon);
            // Tính toán kích thước QR
            float QR_width = width;
            float QR_height = height
                - (_qrSetting.ShowCode ? (code_size.Height - 1.5f) : 0)
                - (_qrSetting.ShowName ? (name_Height - 1.5f) : 0)
                - (_qrSetting.ShowPrice ? (price_Height - 1.5f) : 0);
            float QR_x = xStart - (_qrSetting.ShowName2 ? 1 : 0);
            if (_qrSetting.QRSquare)
            {
                if (QR_width > QR_height)
                {
                    QR_x = QR_x + (QR_width - QR_height) / 2;
                    QR_width = QR_height;
                }
                
            }
            g.DrawImage(QR, QR_x, yStart, QR_width, QR_height);

            // Vẽ mã sản phẩm trên cùng
            yStart = pt.Y;
            if (code_Height > 0) g.DrawString(productCode, code_font, brush, new RectangleF(xStart, yStart, width, code_Height), stringFormat);
            // Vẽ tên sản phẩm bên dưới.
            yStart += (_qrSetting.ShowCode ? (code_size.Height - 1.5f) : 0) + QR_height-2;
            if (name_Height > 0) g.DrawString(productName, name_font, brush, new RectangleF(xStart, yStart, width, name_Height), stringFormat);
            // Vẽ tên sản phẩm bên phải.
            if (_qrSetting.ShowName2)
            {
                SizeF name2_size = g.MeasureString(productName2, name_font);
                g.RotateTransform(90);
                // xx là điểm bắt đầu (trên xuống)
                // yy trái qua phải (âm)
                // ww độ dài khung vẽ chữ (trên xuống)
                // hh độ cao chữ (phải => trái)
                float
                    xx = pt.Y,
                    yy = -(width - 0) - pt.X,
                    ww = height - (_qrSetting.ShowName || _qrSetting.ShowPrice ? name2_size.Height : 0),
                    hh = name2_size.Height;
                g.DrawString(productName2, name_font, Brushes.Black, new RectangleF(xx, yy, ww, hh), stringFormat);
            }
            g.ResetTransform();
            //Vẽ phần giá bên dưới
            if (price_Height > 0) g.DrawString(_qrSetting.PriceText + " " +
                ObjectAndString.NumberToString(productPrice, _qrSetting.PriceDecimals, _qrSetting.DecimalGroup, _qrSetting.ThousandGroup, true)
                + " " + _qrSetting.UnitText,
                price_font, brush,
                new RectangleF(xStart, pt.Y + height - price_Height, width, price_Height), stringFormat);
            
            // Restore the GraphicsState.
            g.Restore(gs);
        }
        
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                Print();
            }
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                PrintView();
            }
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void cboECC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change_ecc_on_g)
            {
                change_ecc_on_g = false;
                return;
            }
            _qrSetting.ECCLever = cboECC.SelectedIndex;
        }

        bool change_ecc_on_g;
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "ECCLever")
            {
                change_ecc_on_g = true;
                cboECC.SelectedIndex = _qrSetting.ECCLever;
            }
            else if (e.ChangedItem.Label == "LogoFileName")
            {
                LoadIconFromSetting();
            }
        }

        int timer_count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer_count++;
                if (timer_count == 1)
                {
                    DrawSampleQR();
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".timer1_Tick " + ex.Message);
            }
        }

        public void LoadIcon(string file)
        {
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                _icon = new Bitmap(V6ControlFormHelper.LoadCopyImage(file));
                btnIcon.Image = _icon.Clone(new Rectangle(0, 0, 32, 32), System.Drawing.Imaging.PixelFormat.DontCare);
            }
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var file = V6ControlFormHelper.ChooseOpenFile(this, "Hình ảnh|*.jpg;*.png;*.gif;*.bmp");
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    try
                    {
                        string file_name = Path.GetFileName(file);
                        _qrSetting.LogoFileName = file_name;
                        string directory = Path.Combine(V6Login.StartupPath, "Pictures\\QRLOGO");
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        string file2 = Path.Combine(directory, file_name);
                        if (file2.ToUpper() != file.ToUpper())
                        {
                            File.Copy(file, file2, true);
                        }
                    }
                    catch { }
                    LoadIcon(file);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnIcon_Click " + ex.Message);
            }
        }
    }

    class QRcodePrintSetting : V6Object
    {
        #region Properties
        ///// <summary>
        ///// $
        ///// </summary>
        //[Category("TEXT")]
        //[Description("Đơn vị khác.")]
        //public string AnotherCurrencySymbol { get; set; }
        [Category("TEXT")]
        [Description("In đậm mã sản phẩm.")]
        public bool CodeTextFontBold { get; set; }
        [Category("TEXT")]
        [Description("Cỡ chữ mã sản phẩm.")]
        public float CodeTextFontSize { get; set; }
        [Category("TEXT")]
        [Description("In mã xuống dòng nếu quá dài.")]
        public bool CodeTextCanDrop { get; set; }
        [Category("TEXT")]
        [Description("In tên xuống dòng nếu quá dài.")]
        public bool NameTextCanDrop { get; set; }
        [Category("TEXT")]
        [Description("In đậm tên sản phẩm.")]
        public bool NameTextFontBold { get; set; }
        [Category("TEXT")]
        [Description("Cỡ chữ tên sản phẩm.")]
        public float NameTextFontSize { get; set; }
        [Category("TEXT")]
        [Description("In đậm giá sản phẩm.")]
        public bool PriceTextFontBold { get; set; }
        [Category("TEXT")]
        [Description("Cỡ chữ giá.")]
        public float PriceTextFontSize { get; set; }
        [Category("TEXT")]
        [Description("Ký tự cách phần nghìn.")]
        public string ThousandGroup { get; set; }
        [Category("TEXT")]
        [Description("Ký tự phần thập phân.")]
        public string DecimalGroup { get; set; }
        /// <summary>
        /// Giá: Price:
        /// </summary>
        [Category("TEXT")]
        [Description("Đoạn chữ trước giá tiền. Ví dụ [Giá:]")]
        public string PriceText { get; set; }

        [Category("TEXT")]
        [DefaultValue(2)]
        [Description("Số chữ số thập phân. Ví dụ 2 => 100 000,00")]
        public int PriceDecimals { get; set; }
        /// <summary>
        /// VND
        /// </summary>
        [Category("TEXT")]
        [Description("Đơn vị tiền.")]
        public string UnitText { get; set; }
        [Category("TEXT")]
        [Description("Hiện mã sản phẩm hoặc không.")]
        public bool ShowCode { get; set; }
        [Category("TEXT")]
        [Description("Hiện tên sản phẩm hoặc không.")]
        public bool ShowName { get; set; }
        [Category("TEXT")]
        [Description("Hiện tên sản phẩm 2 bên phải hoặc không.")]
        public bool ShowName2 { get; set; }
        [Category("TEXT")]
        [Description("Hiện giá sản phẩm hoặc không.")]
        public bool ShowPrice { get; set; }
        //[Description("Ngôn ngữ")]
        //public string LAN { get; set; }
        /// <summary>
        /// 1 VND 2 USD
        /// </summary>
        [Category("TEXT")]
        [DefaultValue("VND")]
        [Description("Loại tiền 1 VND 2 USD")]
        public string CurrencyType { get; set; }
        [Category("PAGE")]
        [DefaultValue(3.0f)]
        [Description("Khoảng cách hàng nếu trang in lớn.")]
        public float DistanceH { get; set; }
        [Category("PAGE")]
        [DefaultValue(3.0f)]
        [Description("Khoảng cách cột nếu trang in lớn.")]
        public float DistanceV { get; set; }
        [Category("PAGE")]
        [DefaultValue(1.0f)]
        [Description("Canh lề dưới trang.")]
        public float MarginBottom { get; set; }
        [Category("PAGE")]
        [DefaultValue(1.0f)]
        [Description("Canh lề trái trang.")]
        public float MarginLeft { get; set; }
        [Category("PAGE")]
        [DefaultValue(1.0f)]
        [Description("Canh lề phải trang.")]
        public float MarginRight { get; set; }
        [Category("PAGE")]
        [DefaultValue(1.0f)]
        [Description("Canh lề trên trang.")]
        public float MarginTop { get; set; }
        
        [Category("PAGE")]
        [Description("Chiều cao trang in.")]
        public float PageHeight { get; set; }
        [Category("PAGE")]
        [Description("Chiều rộng trang in.")]
        public float PageWidth { get; set; }
        [Category("PAGE")]
        [DefaultValue(false)]
        [Description("Xoay trang in.")]
        public bool Rotate { get; set; }

        [Category("STAMP")]
        [DefaultValue(1.0f)]
        [Description("Co giãn them so với kích thước định nghĩa.")]
        public float Scale { get; set; }
        [Category("STAMP")]
        [Description("Chiều cao tem.")]
        public float StampHeight { get; set; }
        [Category("STAMP")]
        [Description("Chiều rộng tem.")]
        public float StampWidth { get; set; }
        [Category("STAMP")]
        [DefaultValue(0)]
        [Description("Cấp độ mã hóa 0123")]
        public int ECCLever { get; set; }
        [Category("STAMP")]
        [DefaultValue("")]
        [Description("Logo giữa QRcode.")]
        public string LogoFileName { get; set; }
        [Category("STAMP")]
        [DefaultValue(15)]
        [Description("Kích thước phần trăm Logo so với QRcode.")]
        public int LogoPercent { get; set; }

        [Category("STAMP")]
        [DefaultValue(false)]
        [Description("Làm vuông QR.")]
        public bool QRSquare { get; set; }

        [Category("PRINT")]
        [DefaultValue(false)]
        [Description("In luôn không hiện form tùy chỉnh.")]
        public bool ForcePrint { get; set; }
        



        #endregion Properties

        public QRcodePrintSetting()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }

        public void LoadSetting(IDictionary<string, string> dic)
        {
            base.SetPropertiesValue(dic);
        }
    }
}

