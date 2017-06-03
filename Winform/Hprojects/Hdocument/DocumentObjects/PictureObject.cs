using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using HaUtility.Helper;

namespace H_document.DocumentObjects
{
    public class PictureObject : DocumentObject
    {
        //public override DocumentObjectType ObjectType { get{return DocumentObjectType.Picture;} }

        public PictureObject()
        {
            ObjectSize = new SizeF(20, 20);
            ObjectType = DocumentObjectType.Picture;
        }
        public PictureObject(PictureObject copy)
            : base(copy)
        {
            Picture = (Image) copy.Picture.Clone();
        }

        public override DocumentObject Clone()
        {
            return new PictureObject(this);
        }
        public PictureObject(SortedDictionary<string, string> data):base(data)
        {
            if (data == null) return;
            if (data.ContainsKey(Field.ParameterNames))
            {
                string ss = data[Field.ParameterNames];
                try
                {
                    Image img = DOConverter.StringToImage(ss);
                    Picture = img;
                }
                catch (Exception ex)
                {
                    new Logger("","Hdocument").WriteLog0("PictureObject ctor " + ex.Message);
                }

            }
        }

        public Image Picture { get; set; }

        public override void DrawToGraphics(Graphics g, Margins margins, SortedDictionary<string, object> parameters, Mode drawMode, DocumentObject[] selectedObjects)
        {
            //Luôn luôn vẽ hình lên, không vẽ thì không còn là pictureObject nữa
            if(Picture != null) g.DrawImage(Picture, margins.Left + LeftF, margins.Top + TopF, WidthF, HeightF);
            //Vẽ khung bao quanh trong trường hợp không phải in
            if (drawMode != Mode.Print)
            {
                Pen pen = new Pen(Color.LightGreen, 0.1f);
                pen.DashStyle = DashStyle.DashDot;
                g.DrawRectangle(pen, LeftF + margins.Left, TopF + margins.Top, WidthF, HeightF);
            }

            base.DrawToGraphics(g, margins, parameters, drawMode, selectedObjects);
        }

    }
}
