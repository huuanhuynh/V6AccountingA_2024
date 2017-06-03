using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Text.RegularExpressions;
using HaUtility.Converter;

namespace H_document.DocumentObjects
{
    public class TextObject : LineObject// DocumentObject
    {
        //public override DocumentObjectType ObjectType { get{return DocumentObjectType.Text;} }

        /// <summary>
        /// Khởi tạo một đối tượng dòng chữ với vài giá trị mặc định.
        /// </summary>
        public TextObject() : base()
        {
            Text = "NewText";
            TextAlign = ContentAlignment.TopLeft;
            DrawLines = DrawLine.None;
            ObjectType = DocumentObjectType.Text;
        }

        public TextObject(TextObject copy) : base(copy)
        {
            Text = copy.Text;
            _font = new Font(copy.Font, copy.Font.Style);
            TextAlign = copy.TextAlign;
        }

        public override DocumentObject Clone()
        {
            return new TextObject(this);
        }
        public TextObject(SortedDictionary<string, string> data):base(data)
        {
            //Khởi tạo các giá trị mặc định
            if (data == null) return;
            if (data.ContainsKey("TEXT")) Text = data["TEXT"];
            if (data.ContainsKey(Field.TextAlign))
            {
                var value = PrimitiveTypes.ObjectToInt(data[Field.TextAlign]);
                if (value < 1) value = 1;
                TextAlign = (ContentAlignment) value;
            }
            else
            {
                TextAlign = ContentAlignment.TopLeft;
            }
            if (data.ContainsKey(Field.FontName))
            {
                var fName = "Arial";
                var fSize = 11.5f;
                var fStyle = FontStyle.Regular;
                var fontInfos = data[Field.FontName].Split(new[] {';'}, 3);
                
                if(fontInfos.Length>0 && fontInfos[0].Length>0) fName = fontInfos[0];
                if (fontInfos.Length > 1 && fontInfos[1].Length > 0)
                    fSize = float.Parse(fontInfos[1], CultureInfo.InvariantCulture);
                if (fontInfos.Length>2 && fontInfos[2].Length>0)
                {
                    var fStyleInfos = fontInfos[2].ToUpper();
                    if (fStyleInfos.Contains("B")) fStyle = fStyle | FontStyle.Bold;
                    if (fStyleInfos.Contains("U")) fStyle = fStyle | FontStyle.Underline;
                    if (fStyleInfos.Contains("I")) fStyle = fStyle | FontStyle.Italic;
                    if (fStyleInfos.Contains("S")) fStyle = fStyle | FontStyle.Strikeout;
                }
                Font = new Font(fName, fSize, fStyle);

            }
            
            if (data.ContainsKey(Field.ParameterNames))
                ParameterNames = data[Field.ParameterNames].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        [Localizable(true)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [DefaultValue("")]
        [DisplayName(@"Text (chữ)")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                ResetParameters();
            }
        }
        private string _text = "";

        private void ResetParameters()
        {
            if (string.IsNullOrEmpty(_text))
            {
                ParameterNames = new string[]{};
                return;
            }
            var prnames = "";
            var regex = new Regex("{(.+?)}");
            foreach (Match match in regex.Matches(_text))
            {
                var match_key = match.Groups[1].Value;
                prnames += ";" + match_key;
            }
            if (prnames.Length > 1) prnames = prnames.Substring(1);
            ParameterNames = prnames.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries);
        }

        

        public Font Font { get { return _font; } set { _font = value; } }
        private Font _font = new Font("Arial", 12f, FontStyle.Regular);
        
        [Description("Canh vị trí dòng chữ.")]
        [Localizable(true)]
        public ContentAlignment TextAlign { get; set; }
        //[Description("Các đoạn thẳng trên hình chữ nhật ABCD, quy tắc trên->dưới, trái->phải (gọi AB không gọi BA, gọi AC,BD không gọi CA,DB)")]
        //[DefaultValue(DrawLine.None)]
        //public override DrawLine DrawLines { get { return _drawLines; } set { _drawLines = value; } }

        public string[] ParameterNames = {};

        public override void DrawToGraphics(Graphics g, Margins margins, SortedDictionary<string, object> parameters, Mode drawMode, DocumentObject[] selectedObjects)
        {
            //var fontOfNameText = new Font(FontName, FontSize, FontStyle);
            var drawText = Text;
            if(drawMode == Mode.Print || drawMode == Mode.PrintPreview)
            if (ParameterNames != null && parameters != null)
            {
                foreach (string name in ParameterNames)
                {
                    if(parameters.ContainsKey(name.ToUpper()))
                        drawText = drawText.Replace("{" + name + "}", PrimitiveTypes.ObjectToString(parameters[name.ToUpper()]));
                }    
            }
            SolidBrush brush = new SolidBrush(ForceColor);
            
            //var newPointF = new PointF(LeftF + margins.Left, TopF + margins.Top);
            
            //g.DrawString(drawText, Font, brush, newPointF);

            var textAlign = (int)TextAlign;
            if (textAlign < 1) textAlign = 1;
            StringFormat cFormat = new StringFormat();
            Int32 lNum = (Int32)Math.Log((Double)textAlign, 2);
            cFormat.LineAlignment = (StringAlignment)(lNum / 4);
            cFormat.Alignment = (StringAlignment)(lNum % 4);
            var rec = new RectangleF(LeftF + margins.Left, TopF + margins.Top, WidthF, HeightF);
            g.DrawString(drawText, Font, brush, rec, cFormat);

            base.DrawToGraphics(g, margins, parameters, drawMode, selectedObjects);
            
        }

        public override string ToString()
        {
            return "" + Text + ", " + Font;
        }

        /// <summary>
        /// Thay đổi FontStyle.Bold và trả về giá trị mới.
        /// </summary>
        /// <returns></returns>
        public bool ChangeBold()
        {
            FontStyle newfontstyle = Font.Style;
            newfontstyle = newfontstyle^FontStyle.Bold;
            Font = new Font(Font, newfontstyle);
            return (newfontstyle&FontStyle.Bold) == FontStyle.Bold;
        }

        public void SetBold(bool isBold)
        {
            if (isBold)
            {
                if ((Font.Style & FontStyle.Bold) != FontStyle.Bold) ChangeBold();
            }
            else
            {
                if ((Font.Style & FontStyle.Bold) == FontStyle.Bold) ChangeBold();
            }
        }

        /// <summary>
        /// Thay đổi FontStyle.Italic và trả về giá trị mới.
        /// </summary>
        /// <returns></returns>
        public bool ChangeItalic()
        {
            return ChangeFontStyle(FontStyle.Italic);
        }

        public void SetItalic(bool isItalic)
        {
            if (isItalic)
            {
                if ((Font.Style & FontStyle.Italic) != FontStyle.Italic) ChangeItalic();
            }
            else
            {
                if ((Font.Style & FontStyle.Italic) == FontStyle.Italic) ChangeItalic();
            }
        }

        /// <summary>
        /// Change a style to use or not.
        /// </summary>
        /// <param name="change">A single style: bold or italic or underline only.</param>
        public bool ChangeFontStyle(FontStyle change)
        {
            FontStyle newfontstyle = Font.Style ^ change;
            Font = new Font(Font, newfontstyle);
            return (newfontstyle & change) == change;
        }

        /// <summary>
        /// Set a style to true or false.
        /// </summary>
        /// <param name="change">A single style: bold or italic or underline only.</param>
        /// <param name="isTrue">Set style to change to true or false</param>
        public void SetFontStyle(FontStyle change, bool isTrue)
        {
            if (isTrue)
            {
                FontStyle newfontstyle = Font.Style | change;
                Font = new Font(Font, newfontstyle);
            }
            else
            {
                FontStyle newfontstyle = Font.Style & (Font.Style ^ change);
                Font = new Font(Font, newfontstyle);
            }
        }

        //public void ChangeTextAlign(ContentAlignment change)
        //{
        //    TextAlign = TextAlign ^ change;
        //}
    }
}
