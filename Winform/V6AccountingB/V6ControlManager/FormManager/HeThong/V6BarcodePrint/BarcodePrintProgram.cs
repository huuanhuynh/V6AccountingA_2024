#region Using directives

using System;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using H;

#endregion

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	static class BarcodePrintProgram
	{
        public static string DBFpath = "";
        public static DataTable data;
        public static Setting setting;
        public static int barcodecount = 0;
            
        public static float pageWidth = 75, pageHeight = 20,
            marginleft = 0, marginright = 0, margintop = 0, marginbottom = 0;

        public static float scale = 1f;
        public static float nametextfontsize = 8.25f;
        public static float pricetextfontsize = 8.25f;
        public static bool nametextfontbold = false;
        public static bool pricetextfontbold = false;
        public static int distanceH = 0, distanceV=0;

        //public static bool useAnotherSymbol = false;
        public static string AnotherCurrencySymbol = "$";
        public static string CurrencyType = "1";
        public static string LAN = "V";
        public static string ThousandGroup = ",";
        public static string DecimalGroup = ".";

        //public static string decimalSymbol = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        //private static V6Barcode13 barcode = null;
		
        public static void StartMain(string[] args)
		{
            
            try
            {
                if(args.Length>0)
                    DBFpath = Path.GetFullPath(args[0]);
                else
                    DBFpath = Path.GetFullPath("test.DBF");
                data = V6Tools.ParseDBF.ReadDBF(BarcodePrintProgram.DBFpath);
                setting = new Setting(Path.GetFullPath("V6Barcode13Setting.ini"));

                pageWidth = Convert.ToSingle(setting.GetSetting("PageWidth"));
                pageHeight = Convert.ToSingle(setting.GetSetting("PageHeight"));

                marginleft = Convert.ToSingle(setting.GetSetting("MarginLeft"));
                marginright = Convert.ToSingle(setting.GetSetting("MarginRight"));
                margintop = Convert.ToSingle(setting.GetSetting("MarginTop"));
                marginbottom = Convert.ToSingle(setting.GetSetting("MarginBottom"));

                distanceH = Convert.ToInt32(setting.GetSetting("DistanceH"));
                distanceV = Convert.ToInt32(setting.GetSetting("DistanceV"));
                NumberFormatInfo DecimalSeparatorFormat = new NumberFormatInfo { NumberDecimalSeparator = "."};
                //scale = float.Parse(setting.GetSetting("Scale"), DecimalSeparatorFormat);
                float.TryParse(setting.GetSetting("Scale"),
                    NumberStyles.Float, DecimalSeparatorFormat, out scale);
                float.TryParse(setting.GetSetting("NameTextFontSize"),
                    NumberStyles.Float, DecimalSeparatorFormat, out nametextfontsize);
                float.TryParse(setting.GetSetting("PriceTextFontSize"),
                    NumberStyles.Float, DecimalSeparatorFormat, out pricetextfontsize);

                nametextfontbold = "1" == setting.GetSetting("NameTextFontBold");
                pricetextfontbold = "1" == setting.GetSetting("PriceTextFontBold");

                BarcodePrintProgram.ThousandGroup = setting.GetSetting("ThousandGroup");
                BarcodePrintProgram.DecimalGroup = setting.GetSetting("DecimalGroup");

                CurrencyType = setting.GetSetting("CurrencyType");
                AnotherCurrencySymbol = setting.GetSetting("AnotherCurrencySymbol");
                LAN = setting.GetSetting("LAN");

                if (setting.GetSetting("ForcePrint") == "1")
                {   
                    Print();
                }
                else
                {
                    
                    Application.EnableVisualStyles();
                    //Application.EnableRTLMirroring( );
                    Application.Run(new PrintBarcodeForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
		}

        public static void Print(IWin32Window owner = null)
        {
            SetBarcodeGeneralValues(barcode);
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            PaperSize psize = new PaperSize("V6",
                    (int)(pageWidth / 25.4f * 100),
                    (int)(pageHeight / 25.4f * 100));
            
            document.DefaultPageSettings.PaperSize = psize;
            
            //printview = new PrintPreviewDialog();
            //printview.Document = document;
            //printview.ShowDialog(owner);

            PrintDialog printdialog1 = new PrintDialog();
            printdialog1.AllowPrintToFile = true;
            printdialog1.PrintToFile = false;
            printdialog1.AllowSelection = false;
            printdialog1.AllowSomePages = false;
            
            printdialog1.UseEXDialog = true;
            printdialog1.PrinterSettings.DefaultPageSettings.PaperSize = psize;

            if (printdialog1.ShowDialog(owner) == DialogResult.OK)
            {
                //printdialog1.Document = document;
                document.PrinterSettings = printdialog1.PrinterSettings;
                document.DefaultPageSettings.PaperSize = psize;
                
                //MessageBox.Show("Number of copies: " + document.PrinterSettings.Copies.ToString());
                document.PrintPage += document_PrintPage;
                document.Print();
            }
        }

        public static void PrintView(IWin32Window owner = null)
        {
            SetBarcodeGeneralValues(barcode);
            BarcodePrintProgram.barcodecount = 0;
            PrintDocument document = new PrintDocument();

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

            ((ToolStripButton)((ToolStrip)printview.Controls[1]).Items[0]).Enabled = false;
            printview.ShowDialog(owner);
        }

        static V6Barcode13 barcode = new V6Barcode13();
        public static void SetBarcodeGeneralValues(V6Barcode13 b13)
        {
            b13.Scale = BarcodePrintProgram.scale;
            b13.NameTextFontSize = BarcodePrintProgram.nametextfontsize;
            b13.PriceTextFontSize = BarcodePrintProgram.pricetextfontsize;
            b13.NameTextFontBold = BarcodePrintProgram.nametextfontbold;
            b13.PriceTextFontBold = BarcodePrintProgram.pricetextfontbold;

            if (BarcodePrintProgram.CurrencyType == "0")
                b13.UnitText = "USD";
            else if (BarcodePrintProgram.CurrencyType == "1")
                b13.UnitText = "VNĐ";
            else
                b13.UnitText = BarcodePrintProgram.AnotherCurrencySymbol;
            b13.PriceText = BarcodePrintProgram.LAN == "V" ? "Giá:": "Price:";
            b13.numberformatinfo.NumberGroupSeparator = BarcodePrintProgram.ThousandGroup;
            b13.numberformatinfo.NumberDecimalSeparator = BarcodePrintProgram.DecimalGroup;
            //b13.numberformatinfo.CurrencyDecimalDigits = VND ? 0 : 2;

            b13.NumberFormatString = "0,0." + (CurrencyType=="0"? "00": "");
        }
        public static void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Code in trong 1 trang
            

            float yPos = 0f; yPos = margintop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = e.PageBounds.Height * 25.4f / 100;
            
            int columnsPerPage = (int)((pageWidthmm-marginleft-marginright + distanceV)
                / (barcode.Width*scale + distanceV));
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
                    if (barcodecount < data.Rows.Count)
                    {
                        DataRow currentdata = data.Rows[barcodecount];//Lấy thông tin tại barcodecount xong mới ++
                        barcodecount++;
                        //barcode = new V6Barcode13(currentdata[1].ToString().Trim(),
                        //    currentdata[0].ToString().Trim(),
                        //    Convert.ToDecimal(currentdata[2]));
                        barcode.ProductName = currentdata[1].ToString().Trim();
                        barcode.V6code13 = currentdata[0].ToString().Trim();
                        barcode.ProductPrice = Convert.ToDecimal(currentdata[2]);
                        
                        //Tính vị trí x
                        xPos = marginleft +
                            i * (barcode.Width * scale + distanceV);
                        
                        barcode.DrawBarcode(e.Graphics,
                            new System.Drawing.Point((int)xPos, (int)yPos));
                    }
                    else
                    {
                        break;
                    }
                }
                lineInPageCount++;
                yPos += barcode.Height * scale + distanceH;
            }
            while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - barcode.Height * scale);
            
            if (barcodecount < data.Rows.Count) e.HasMorePages = true;
            else e.HasMorePages = false;

        }
	}
}