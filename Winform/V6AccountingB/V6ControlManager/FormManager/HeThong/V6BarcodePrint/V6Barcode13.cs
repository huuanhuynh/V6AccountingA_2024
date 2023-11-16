#region Using directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

#endregion

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	public class V6Barcode13
	{
		private string _sName = "V6BARCODE13";

        // Tỷ lệ thu phóng cho phép?
		private float _fMinimumAllowableScale = .5f;
		private float _fMaximumAllowableScale = 2.0f;

		// This is the nomimal size recommended by the EAN.
        // Dưới đây là kích thước thường có của Barcode13
		private float _fWidth = 37.29f;
		private float _fHeight = 18.93f;    // V6 cần làm nhỏ chỗ này ! :D cũ :25.93
		private float _fFontSize = 8.0f;
        
		private float _fScale = 1.0f;

		// Left Hand Digits. Số bên trái.
		private string [] _aOddLeft = { "0001101", "0011001", "0010011", "0111101", 
										  "0100011", "0110001", "0101111", "0111011", 
										  "0110111", "0001011" };

		private string [] _aEvenLeft = { "0100111", "0110011", "0011011", "0100001", 
										   "0011101", "0111001", "0000101", "0010001", 
										   "0001001", "0010111" };

		// Right Hand Digits. Số bên phải?
		private string [] _aRight = { "1110010", "1100110", "1101100", "1000010", 
										"1011100", "1001110", "1010000", "1000100", 
										"1001000", "1110100" };

		private string _sQuiteZone = "000000000";

		private string _sLeadTail = "101";

		private string _sSeparator = "01010";

        private string _sProductName = "";      // huuanv6add

        
		private string _sCountryCode = "00";
		private string _sManufacturerCode;
		private string _sProductCode;
		private string _sChecksumDigit;
        private decimal _decProductPrice = 0;   // huuanv6add
                
        public string NumberFormatString = "0,0.";

        public string V6code13{get;set;}

        public NumberFormatInfo numberformatinfo
            = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();


		public V6Barcode13( )
		{
            
		}

		public V6Barcode13( string mfgNumber, string productId )
		{
			this.CountryCode = "00";
			this.ManufacturerCode = mfgNumber;
			this.ProductCode = productId;
			this.CalculateChecksumDigit( );
		}

		public V6Barcode13( string countryCode, string mfgNumber, string productId )
		{
			this.CountryCode = countryCode;
			this.ManufacturerCode = mfgNumber;
			this.ProductCode = productId;
			this.CalculateChecksumDigit( );
		}

		public V6Barcode13( string countryCode, string mfgNumber, string productId, string checkDigit )
		{
			this.CountryCode = countryCode;
			this.ManufacturerCode = mfgNumber;
			this.ProductCode = productId;
			this.ChecksumDigit = checkDigit;
		}

        /// <summary>
        /// V6Barcode13
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="mfgNumber"></param>
        /// <param name="productId"></param>
        /// <param name="checkDigit"></param>
        public V6Barcode13(string strProductName, string countryCode, string mfgNumber, string productId, string checkDigit, decimal decProductPrice)
        {
            this.CountryCode = countryCode;
            this.ManufacturerCode = mfgNumber;
            this.ProductCode = productId;
            this.ChecksumDigit = checkDigit;
        }
        public V6Barcode13(string strProductName, string code13, decimal decProductPrice)
        {
            //while (code13.Length < 12) code13 = "0" + code13;
            this.ProductName = strProductName;
            //this.CountryCode = code13.Substring(0,2);
            //this.ManufacturerCode = code13.Substring(2,3);
            //this.ProductCode = code13.Substring(5);
            //this.ChecksumDigit = checkDigit;
            this.V6code13 = code13;
            this.ProductPrice = decProductPrice;
            
            CalculateChecksumDigit();
            //System.Console.Write(1000000.91.ToString("0,0.00", format));
        }

		public void DrawBarcode( Graphics g, Point pt )
		{
			float width = this.Width * this.Scale;
			float height = this.Height * this.Scale;
            

			//	EAN13 Barcode should be a total of 113 modules wide.
			float lineWidth = width / 113f;

			// Save the GraphicsState.
			System.Drawing.Drawing2D.GraphicsState gs = g.Save( );

			// Set the PageUnit to Milimet because all of our measurements are in inches.
			g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;

			// Set the PageScale to 1, so a millimeter will represent a true millimeter.
			g.PageScale = 1;

			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush( System.Drawing.Color.Black );

			float xPosition = 0;

			System.Text.StringBuilder strbEAN13 = new System.Text.StringBuilder( );
			System.Text.StringBuilder sbTemp = new System.Text.StringBuilder( );

			float xStart = pt.X;
			float yStart = pt.Y;
			float xEnd = 0;

			System.Drawing.Font font = new System.Drawing.Font( "Arial", this._fFontSize * this.Scale );
            System.Drawing.Font fontOfNameText = new Font("Arial", this.NameTextFontSize * this.Scale,
                this.NameTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            System.Drawing.Font fontOfPriceText = new Font("Arial", this.PriceTextFontSize * this.Scale,
                this.PriceTextFontBold? FontStyle.Bold : FontStyle.Regular);

			// Calculate the Check Digit.
			//this.CalculateChecksumDigit( );

            //sbTemp.AppendFormat( "{0}{1}{2}{3}", 
            //    this.CountryCode,
            //    this.ManufacturerCode,
            //    this.ProductCode, 
            //    this.ChecksumDigit );


			//string sTemp = sbTemp.ToString( );
            string sTemp = this.V6code13;
            if (this.V6code13.Length == 12) sTemp += this.ChecksumDigit;

			string sLeftPattern = "";

			// Convert the left hand numbers.
			sLeftPattern = ConvertLeftPattern( sTemp.Substring( 0, 7 ) );

			// Build the UPC Code.
			strbEAN13.AppendFormat( "{0}{1}{2}{3}{4}{1}{0}",
				this._sQuiteZone, this._sLeadTail,
				sLeftPattern,					
				this._sSeparator,
				ConvertToDigitPatterns( sTemp.Substring( 7 ), this._aRight ) );

			string sTempUPC = strbEAN13.ToString( );

			float fTextHeight = g.MeasureString( sTempUPC, font ).Height;
            float fNameTextHeight = g.MeasureString(sTempUPC, fontOfNameText).Height;
            float fPriceTextHeight = g.MeasureString(sTempUPC, fontOfPriceText).Height;
            float fATextWidth = g.MeasureString("A", font).Width;//Dùng để thụt đầu dòng ấy mà :P

            // Vẽ tên sản phẩm ở dòng đầu tiên
            g.DrawString(this.ProductName, fontOfNameText, brush,
                new System.Drawing.PointF(pt.X + fATextWidth, pt.Y));
            
            yStart += g.MeasureString(this.ProductName, fontOfNameText).Height;
            
			// Draw the barcode lines.
			for( int i = 0; i < strbEAN13.Length; i++ )
			{
				if( sTempUPC.Substring( i, 1 ) == "1" )
				{
					if( xStart == pt.X )
						xStart = xPosition;

					// Save room for the UPC number below the bar code.
					if( ( i > 12 && i < 55 ) || ( i > 57 && i < 101 ) )//Nếu nằm trong phần vẽ ngắn.
						// Draw space for the number. Vẽ code chừa chỗ cho số bên dưới
						g.FillRectangle( brush,pt.X + xPosition, yStart, lineWidth, height - fNameTextHeight - fPriceTextHeight - fTextHeight);
					else
						// Draw a full line.
                        g.FillRectangle(brush, pt.X + xPosition, yStart, lineWidth, height - fNameTextHeight - fPriceTextHeight);
				}

				xPosition += lineWidth;
				xEnd = xPosition;
			}

			// Draw the upc numbers below the line.
			xPosition = xStart - g.MeasureString( this.CountryCode.Substring( 0, 1 ), font ).Width;
			float yPosition = yStart + ( height -  fNameTextHeight - fPriceTextHeight - fTextHeight);

			// Draw 1st digit of the country code. Số đầu tiên của mã quốc gia.
			g.DrawString( sTemp.Substring( 0, 1 ), font, brush, new System.Drawing.PointF(pt.X + xPosition, yPosition ) );

			xPosition += ( g.MeasureString( sTemp.Substring( 0, 1 ), font ).Width + 43 * lineWidth ) -
				( g.MeasureString( sTemp.Substring( 1, 6 ), font ).Width );

			// Draw MFG Number. Phần code số bên trái
			g.DrawString( sTemp.Substring( 1, 6 ), font, brush,	new System.Drawing.PointF(pt.X + xPosition, yPosition ) );

			xPosition += g.MeasureString( sTemp.Substring( 1, 6 ), font ).Width + ( 11 * lineWidth );

			// Draw Product ID. Phần code số bên phải
			g.DrawString( sTemp.Substring( 7 ), font, brush, new System.Drawing.PointF(pt.X + xPosition, yPosition ) );

            //Vẽ phần giá bên dưới
            //if(Program.VND)
            g.DrawString( 
                this.PriceText
                + " " + this.ProductPrice.ToString(NumberFormatString, numberformatinfo) + " "
                + this.UnitText,
                fontOfPriceText, brush,
                new System.Drawing.PointF(pt.X + fATextWidth,  pt.Y + height - fPriceTextHeight));
            
            yStart += g.MeasureString(this.ProductName, fontOfPriceText).Height;
            
			// Restore the GraphicsState.
            
            brush.Dispose();
			g.Restore( gs );
		}


		public System.Drawing.Bitmap CreateBitmap( )
		{
			float tempWidth = ( this.Width * this.Scale ) * 100 ;
			float tempHeight = ( this.Height * this.Scale ) * 100;

			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( (int)tempWidth, (int)tempHeight );

			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( bmp );
			this.DrawBarcode( g, new System.Drawing.Point( 0, 0 ) );
			g.Dispose( );
			return bmp;
		}


		private string ConvertLeftPattern( string sLeft )
		{
			switch( sLeft.Substring( 0, 1 ) )
			{
				case "0":
					return CountryCode0( sLeft.Substring( 1 ) );
					
				case "1":
					return CountryCode1( sLeft.Substring( 1 ) );

				case "2":
					return CountryCode2( sLeft.Substring( 1 ) );

				case "3":
					return CountryCode3( sLeft.Substring( 1 ) );

				case "4":
					return CountryCode4( sLeft.Substring( 1 ) );

				case "5":
					return CountryCode5( sLeft.Substring( 1 ) );

				case "6":
					return CountryCode6( sLeft.Substring( 1 ) );

				case "7":
					return CountryCode7( sLeft.Substring( 1 ) );

				case "8":
					return CountryCode8( sLeft.Substring( 1 ) );

				case "9":
					return CountryCode9( sLeft.Substring( 1 ) );

				default:
					return "";
			}
		}


		private string CountryCode0( string sLeft )
		{
			// 0 Odd Odd  Odd  Odd  Odd  Odd 
			return ConvertToDigitPatterns( sLeft, this._aOddLeft );
		}


		private string CountryCode1( string sLeft )
		{
			// 1 Odd Odd  Even Odd  Even Even 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			// The two lines below could be replaced with this:
			// sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 2 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aOddLeft ) );
			// The two lines below could be replaced with this:
			// sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 2 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aEvenLeft ) );
			return sReturn.ToString( );
		}

		
		private string CountryCode2( string sLeft )
		{
			// 2 Odd Odd  Even Even Odd  Even 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aEvenLeft ) );
			return sReturn.ToString( );
		}


		private string CountryCode3( string sLeft )
		{
			// 3 Odd Odd  Even Even Even Odd 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aOddLeft ) );
			return sReturn.ToString( );
		}


		private string CountryCode4( string sLeft )
		{
			// 4 Odd Even Odd  Odd  Even Even 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aEvenLeft ) );
			return sReturn.ToString( );
		}

		
		private string CountryCode5( string sLeft )
		{
			// 5 Odd Even Even Odd  Odd  Even 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aEvenLeft ) );
			return sReturn.ToString( );
		}
		

		private string CountryCode6( string sLeft )
		{
			// 6 Odd Even Even Even Odd  Odd 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aOddLeft ) );
			return sReturn.ToString( );
		}

		
		private string CountryCode7( string sLeft )
		{
			// 7 Odd Even Odd  Even Odd  Even
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aEvenLeft ) );
			return sReturn.ToString( );
		}

 		
		private string CountryCode8( string sLeft )
		{
			// 8 Odd Even Odd  Even Even Odd 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aOddLeft ) );
			return sReturn.ToString( );
		}

		
		private string CountryCode9( string sLeft )
		{
			// 9 Odd Even Even Odd  Even Odd 
			System.Text.StringBuilder sReturn = new StringBuilder( );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 0, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 1, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 2, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 3, 1 ), this._aOddLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 4, 1 ), this._aEvenLeft ) );
			sReturn.Append( ConvertToDigitPatterns( sLeft.Substring( 5, 1 ), this._aOddLeft ) );
			return sReturn.ToString( );
		}


		private string ConvertToDigitPatterns( string inputNumber, string [] patterns )
		{
			System.Text.StringBuilder sbTemp = new StringBuilder( );
			int iIndex = 0;
			for( int i = 0; i < inputNumber.Length; i++ )
			{
				iIndex = Convert.ToInt32( inputNumber.Substring( i, 1 ) );
				sbTemp.Append( patterns[iIndex] );
			}
			return sbTemp.ToString( );
		}

        /// <summary>
        /// Consider the right-most digit of the message to be in an "odd" position, and assign odd/even to each character moving from right to left.
    ///Sum the digits in all odd positions, and multiply the result by 3.
    ///Sum the digits in all even positions.
    ///Sum the totals calculated in steps 2 and 3.
    ///The check digit is the number which, when added to the totals calculated in step 4, result in a number evenly divisible by 10.
    ///If the sum calculated in step 4 is evenly divisible by 10, the check digit is "0" (not 10).
        /// </summary>
		public void CalculateChecksumDigit( )
		{
			string sTemp = this.CountryCode + this.ManufacturerCode + this.ProductCode;
            if (sTemp.Length == 0 || sTemp.Length < 12)
            {
                if(this.V6code13.Length>=12)
                    sTemp = this.V6code13.Substring(0, 12);
            }
			int iSum = 0;
			int iDigit = 0;

			// Calculate the checksum digit here.
            // Duyệt số từ cuối tới đầu (12 số) <= chẳng liên quan đầu cuối.
            // Nếu là số chẳn (odd) lấy số đó nhân 3 + vào sum.
            // Nếu là số lẽ (even) lấy số đó (nhân 1) + vào sum.
			for( int i = sTemp.Length; i >= 1; i-- )
			{
				iDigit = Convert.ToInt32( sTemp.Substring( i - 1, 1 ) );
				if( i % 2 == 0 )
				{	// odd
					iSum += iDigit * 3;
				}
				else
				{	// even
					iSum += iDigit * 1;
				}
			}
            // số kiểm tra là phần đơn vị của số mà + với phần đơn vị của sum = 10;
            // có nghĩa sum lẽ 1 ở cuối thì check là 9, lẽ 2 thì check là 8...
            //      lẽ 0 thì check vẫn là 0 :D
			int iCheckSum = ( 10 - ( iSum % 10 )  ) % 10;
			this.ChecksumDigit = iCheckSum.ToString( );

		}


		#region -- Attributes/Properties --

		public string Name
		{
			get
			{
				return _sName;
			}
		}

		public float MinimumAllowableScale
		{
			get
			{
				return _fMinimumAllowableScale;
			}
		}

		public float MaximumAllowableScale
		{
			get
			{
				return _fMaximumAllowableScale;
			}
		}

		public float Width
		{
			get
			{
				return _fWidth;
			}
		}

		public float Height
		{
			get
			{
				return _fHeight;
			}
		}

		public float FontSize
		{
			get
			{
				return _fFontSize;
			}
		}
        /// <summary>
        /// Cỡ chữ cho phần tên sản phẩm.
        /// </summary>
        [DefaultValue(8f)]
        public float NameTextFontSize { get; set; }
        /// <summary>
        /// Cỡ chữ cho phần chữ in giá.
        /// </summary>
        [DefaultValue(8f)]
        public float PriceTextFontSize { get; set; }
        /// <summary>
        /// Có in đậm phần tên hay không?
        /// </summary>
        [DefaultValue(false)]
        public bool NameTextFontBold { get; set; }
        [DefaultValue(false)]
        public bool PriceTextFontBold { get; set; }

        public string UnitText = "VNĐ";

        public string PriceText = "Giá:";

		public float Scale
		{
			get
			{
				return _fScale;
			}

			set
			{
				if( value < this._fMinimumAllowableScale || value > this._fMaximumAllowableScale )
					throw new Exception( "Scale value out of allowable range.  Value must be between " + 
						this._fMinimumAllowableScale + " and " + 
						this._fMaximumAllowableScale );
				_fScale = value;
			}
		}


        public string ProductName
        {
            get { return _sProductName; }
            set { _sProductName = value; }
        }

		public string CountryCode
		{
			get
			{
				return _sCountryCode;
			}

			set
			{
				while( value.Length < 2 )
				{
					value = "0" + value;
				}
				_sCountryCode = value;
			}
		}

		public string ManufacturerCode
		{
			get
			{
				return _sManufacturerCode;
			}

			set
			{
				_sManufacturerCode = value;
			}
		}

		public string ProductCode
		{
			get
			{
				return _sProductCode;
			}

			set
			{
				_sProductCode = value;
			}
		}

		public string ChecksumDigit
		{
			get
			{
				return _sChecksumDigit;
			}

			set
			{
				int iValue = Convert.ToInt32( value );
				if( iValue < 0 || iValue > 9 )
					throw new Exception( "The Check Digit mst be between 0 and 9." );
				_sChecksumDigit = value;
			}
		}

        public decimal ProductPrice
        {
            get { return _decProductPrice; }
            set { _decProductPrice = value; }
        }
        public string CurrencySymbol { get; set; }

		#endregion -- Attributes/Properties --

	}
}

