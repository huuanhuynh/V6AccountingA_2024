using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace H_document
{
    /// <summary>
    /// Class chứa các properties sẽ dùng để lưu trữ HdocumentObjects.
    /// </summary>
    internal class Field
    {
        public static string Name = "NAME";
        public static string Type = "TYPE";
        public static string Location = "LOCATION";
        public static string Size = "SIZE";
        public static string Text = "TEXT";
        public static string TextAlign = "TEXTALIGN";
        public static string ForceColor = "FORCECOLOR";
        public static string BackColor = "BACKCOLOR";
        public static string ParameterNames = "PARAMETERNAMES";
        public static string FontName = "FONTNAME";
        public static string DrawLines = "DRAWLINES";
        //public static string FontStyle = "FONTSTYLE";
        
        //public static string PAGEWIDTH = "PAGEWIDTH";
        //public static string PAGEHEIGHT = "PAGEHEIGHT";
        public static string PAGESIZE = "PAGESIZE";
        public static string MARGINS = "MARGINS";
        public static string USEDETAIL = "USEDETAIL";
        public static string DETAILLOCATION = "DETAILLOCATION";
        public static string DETAILSIZE = "DETAILSIZE";
        public static string DETAILFONT = "DETAILFONT";
        public static string DETAILLINES = "DETAILLINES";
        public static string DETAILSECONDROW = "DETAILSECONDROW";
    }

    public enum DocObjectType
    {
        None, Text, Line, Picture,
        Copy
    }

    public enum ActionMode
    {
        SangSang,ThemMoi,DangChon,
        SaoChep
    }

    public enum MouseStatus
    {
        MouseDown, MouseUp
    }

    public enum Mode
    {
        None,
        Design, PrintPreview, Print,//vMode
        Move, Resize                //DesignMode
    }

    public enum LineMode
    {
        Siêu_nhỏ,Nhỏ,Vừa,To,Rất_to,Siêu_to
    }

    public enum PointType : int
    {
        None=-1,
        BottomRightResizePoint=1, BottomCenterResizePoint=2, RightCenterResizePoint=3, Details = 9,
        MoveItem=0
    }

    public static class DOConverter
    {
        public static string ImageToString(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            Image im = Image.FromFile(path);
            return ImageToString(im);
        }
        public static string ImageToString(Image im)
        {
            MemoryStream ms = new MemoryStream();
            im.Save(ms, im.RawFormat);
            byte[] array = ms.ToArray();
            var result = Convert.ToBase64String(array);
            return result;
        }

        public static Image StringToImage(string imageString)
        {
            if (imageString == null) 
                throw new ArgumentNullException("imageString");

            byte[] array = Convert.FromBase64String(imageString);
            Image image = Image.FromStream(new MemoryStream(array));
            return image;
        }
    }
}
