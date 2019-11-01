using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HaUtility.Converter;

namespace H_document.DocumentObjects
{
    [Serializable]
    public class DetailsObject : DocumentObject
    {
        public DetailsObject()
        {
            ObjectType = DocumentObjectType.Details;
            ObjectSize = new SizeF(200, 10);
            _items = new DetailItems();
        }
        
        /// <summary>
        /// Danh sách đối tượng trong chi tiết. (Mã, Tên, DVT, ...)
        /// </summary>
        [Description("Danh sách đối tượng trong chi tiết. (Mã, Tên, DVT, ...)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DetailItems Items
        {
            get
            {
                return _items;
            }
        }
        DetailItems _items;

        /// <summary>
        /// Không tính margins
        /// </summary>
        [Browsable(false)]
        public PointF[] ItemResizePoints
        {
            get
            {
                var result = new List<PointF>();

                var startLeft = LeftF;
                var startLeft2 = LeftF;
                
                for (int i = 0; i < Items.Count; i++)
                {
                    DetailItem item = Items[i];
                    if (!item.Visible) continue;

                    if (item.IsSecondRow)
                    {
                        item.Left = startLeft2;
                        var drawTop = TopF + HeightF;
                        var drawRec = new RectangleF(startLeft2, drawTop, item.Width, HeightF);
                        
                        PointF rPoint = new PointF(drawRec.Right, drawTop + HeightF / 2);
                        result.Add(rPoint);
                        
                        
                        startLeft2 += item.Width;
                    }
                    else
                    {
                        item.Left = startLeft;
                        var drawTop = TopF;

                        var drawRec = new RectangleF(startLeft, drawTop, item.Width, HeightF);
                        
                        PointF rPoint = new PointF(drawRec.Right, drawTop + HeightF / 2);
                        result.Add(rPoint);
                        
                        startLeft += item.Width;
                    }
                }
                return result.ToArray();
            }
        }
        

        public Font Font { get { return _font; } set { _font = value; } }
        private Font _font = new Font("Arial", 12f, FontStyle.Regular);
        
        [Browsable(false)]
        [Description("Dữ liệu hiển thị trong chi tiết. Thường dùng DataTable. Cũng có thể dùng danh sách những đối tượng.")]
        public object Data { get; set; }
        [Description("Hiển thị đường kẻ ô cho phần chi tiết.")]
        public bool ViewLine { get; set; }
        [Description("Sử dụng dòng thứ hai trong phần chi tiết.")]
        public bool ViewSecondRow { get; set; }


        public override void DrawToGraphics(Graphics g, Margins margins, SortedDictionary<string, object> parameters, HMode drawMode, DocumentObject[] selectedObjects)
        {
            //Vẽ lặp data chi tiết khi in và preview
            Pen penLine = new Pen(Color.Black, 0.1f);
            penLine.DashStyle = DashStyle.Solid;
            Brush brush = new SolidBrush(Color.Black);
            var startTop = TopF + margins.Top;

            DrawHeader(g, margins, penLine, brush, drawMode, startTop);
            if (drawMode == HMode.Print || drawMode == HMode.PrintPreview)
            {
                // Phần vẽ chi tiết khi in hoặc preview
                if (Data is DataTable)
                {
                    DataTable tableData = Data as DataTable;
                    for (int i = 0; i < tableData.Rows.Count; i++)
                    {
                        var row = tableData.Rows[i];
                        
                        DrawOneLine(g, margins, penLine, brush, drawMode, selectedObjects, startTop, row.ToDataDictionary(), out startTop);
                    }
                }
                else if (Data is ICollection)
                {
                    ICollection dataCollection = Data as ICollection;
                    foreach (object o in dataCollection)
                    {
                        DrawOneLine(g, margins, penLine, brush, drawMode, selectedObjects, startTop, o.ToDicH(), out startTop);
                    }
                }
            }
            else // vẽ phần thiết kế
            {
                float t;
                DrawOneLine(g, margins, penLine, brush, drawMode, selectedObjects, startTop, null, out t);
                //DrawItemsResizePoint(g,);
            }

            //Vẽ khung bao quanh trong trường hợp thiết kế
            if (drawMode == HMode.Design)
            {
                Pen pen = new Pen(Color.LightGreen, 0.1f);
                pen.DashStyle = DashStyle.DashDot;
                g.DrawRectangle(pen, LeftF + margins.Left, TopF + margins.Top, WidthF, HeightF);
            }

            base.DrawToGraphics(g, margins, parameters, drawMode, selectedObjects);
        }


        /// <summary>
        /// Vẽ phần tiêu đề mỗi cột của chi tiết.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="margins"></param>
        /// <param name="penLine"></param>
        /// <param name="brush"></param>
        /// <param name="drawMode"></param>
        /// <param name="startTop"></param>
        private void DrawHeader(Graphics g, Margins margins, Pen penLine, Brush brush, HMode drawMode,
            float startTop)
        {
            var startLeft = LeftF + margins.Left;
            var lineHeight = HeightF;
            var drawTop = startTop - lineHeight;
            for (int i = 0; i < Items.Count; i++)
            {
                DetailItem item = Items[i];
                if (item.Visible && !item.IsSecondRow)
                {
                    if (item.ViewHeader)
                    {
                        if (ViewLine) g.DrawRectangle(penLine, startLeft, drawTop, item.Width, lineHeight);
                        
                        var text = item.HeaderText;
                        if (string.IsNullOrEmpty(text)) break;
                        
                        var drawRec = new RectangleF(startLeft, drawTop, item.Width, lineHeight);
                        StringFormat cFormat = new StringFormat();
                        Int32 lNum = (Int32) Math.Log((Double) item.HeaderTextAlign, 2);
                        cFormat.LineAlignment = (StringAlignment) (lNum/4);
                        cFormat.Alignment = (StringAlignment) (lNum%4);
                        g.DrawString(text, Font, brush, drawRec, cFormat);
                    }
                    startLeft += item.Width;
                }
            }
        }
        private void DrawOneLine(Graphics g, Margins margins, Pen penLine, Brush brush, HMode drawMode, DocumentObject[] selectedObjects,
            float startTop, SortedDictionary<string,object> row, out float f)
        {
            var startLeft = LeftF + margins.Left;
            var startLeft2 = LeftF + margins.Left;
            var detailLineHeight = HeightF;
            var line1MaxHeight = HeightF;
            var line2MaxHeight = HeightF;
            // Vẽ phần chữ (ruột) trước
            for (int i = 0; i < Items.Count; i++)
            {
                DetailItem item = Items[i];
                if (!item.Visible) continue;

                if (item.IsSecondRow)
                {
                    if (!ViewSecondRow) continue;
                    detailLineHeight = HeightF*2;
                    var drawTop = startTop + HeightF;
                    var drawRec = new RectangleF(startLeft2, drawTop, item.Width, HeightF);
                    if (ViewLine)
                        g.DrawRectangle(penLine, startLeft2, drawTop, item.Width, HeightF);
                    var text = "";
                    if (drawMode == HMode.Design)
                    {
                        if (!string.IsNullOrEmpty(item.Text)) text = item.Text;
                        else text = "{" + item.Name + "}";
                        
                        //DrawItemResizePoint
                        if (selectedObjects != null && selectedObjects.Contains(this))// this == selectedObjects)
                        {
                            PointF rPoint = new PointF(drawRec.Right, drawTop + HeightF/2);
                            DocumentObjectHelper.DrawMovePoint(g, penLine, rPoint);
                        }
                    }
                    else if (drawMode != HMode.Design)
                    {
                        //if (!string.IsNullOrEmpty(item.Text))
                        {
                            text = item.Text;
                            if (item.ParameterNames != null && item.ParameterNames.Length > 0)
                            {
                                foreach (string name in item.ParameterNames)
                                {
                                    if (row.ContainsKey(name.ToUpper()))
                                        text = text.Replace("{" + name + "}",
                                            PrimitiveTypes.ObjectToString(row[name.ToUpper()]));
                                }
                            }
                        }
                        //else
                        //{
                        //    text = "{" + item.Name + "}";
                        //    if (row.ContainsKey(item.Name.ToUpper()))
                        //    {
                        //        text = row[item.Name.ToUpper()].ToString().Trim();
                        //    }
                        //}
                    }
                    StringFormat cFormat = new StringFormat();
                    Int32 lNum = (Int32)Math.Log((Double)item.TextAlign, 2);
                    cFormat.LineAlignment = (StringAlignment)(lNum / 4);
                    cFormat.Alignment = (StringAlignment)(lNum % 4);
                    g.DrawString(text, Font, brush, drawRec, cFormat);

                    startLeft2 += item.Width;
                }
                else
                {
                    var drawTop = startTop;
                    

                    var drawRec = new RectangleF(startLeft, drawTop, item.Width, HeightF);// delete !!!!!
                    if (ViewLine)
                        g.DrawRectangle(penLine, startLeft, drawTop, item.Width, HeightF);
                    var text = "";
                    if (drawMode == HMode.Design)
                    {
                        text = "{" + item.Name + "}";
                        //DrawItemResizePoint
                        if (selectedObjects != null && selectedObjects.Contains(this))// this == selectedObjects)
                        {
                            PointF rPoint = new PointF(drawRec.Right, drawTop + HeightF/2);
                            DocumentObjectHelper.DrawMovePoint(g, penLine, rPoint);
                        }
                    }
                    else if (drawMode != HMode.Design)
                    {
                        if (row.ContainsKey(item.Name.ToUpper()))
                        {
                            text = row[item.Name.ToUpper()].ToString().Trim();
                        }
                    }

                    StringFormat cFormat = new StringFormat();
                    Int32 lNum = (Int32) Math.Log((Double) item.TextAlign, 2);
                    cFormat.LineAlignment = (StringAlignment) (lNum/4);
                    cFormat.Alignment = (StringAlignment) (lNum%4);

                    RectangleF textRec = new RectangleF();
                    textRec.Location = new PointF(startLeft, drawTop);
                    textRec.Size = new SizeF(item.Width, g.MeasureString(text, Font, 600, cFormat).Height);
                    if (textRec.Height > line1MaxHeight)
                    {
                        line1MaxHeight = textRec.Height;
                    }

                    g.DrawString(text, Font, brush, drawRec, cFormat);

                    startLeft += item.Width;
                }
            }
            // Rồi vẽ phần khung (vỏ) sau
            for (int i = 0; i < Items.Count; i++)
            {
                DetailItem item = Items[i];
                if (!item.Visible) continue;

                if (item.IsSecondRow) // Dòng thứ 2
                {
                    var b = line2MaxHeight;
                }
                else // Dòng thứ nhất
                {
                    var a = line1MaxHeight;
                }
            }

            f = startTop + detailLineHeight;
        }

        /// <summary>
        /// Gán trạng thái khi click chuột vào lúc thiết kế
        /// 
        /// </summary>
        /// <param name="mouseDownPoint">Đã trừ margin</param>
        public override void SetStatus(PointF mouseDownPoint)
        {
            if (GetResizePointIndex(mouseDownPoint) >= 0)
            {
                DesignMode  = HMode.Resize;
                ResizePointType = PointType.MoveItem;
            }
            else
            {
                base.SetStatus(mouseDownPoint);
            }
        }

        /// <summary>
        /// Lấy trạng thái. Move hoặc resize. hoặc chi tiết (xử lý riêng)
        /// </summary>
        /// <param name="mouseDownPoint">Đã trừ margin</param>
        /// <returns></returns>
        public override PointType GetStatus(PointF mouseDownPoint)
        {
            if (GetResizePointIndex(mouseDownPoint) >= 0)
            {
                DesignMode = HMode.Resize;
                ResizePointType = PointType.Details;
                return PointType.Details;
            }
            else
            {
                return base.GetStatus(mouseDownPoint);
            }
        }

        /// <summary>
        /// Lấy điểm resize chọn trúng theo index.
        /// </summary>
        /// <param name="mouseDownPoint"></param>
        /// <returns></returns>
        private int GetResizePointIndex(PointF mouseDownPoint)
        {
            ResizeItemIndex = -1;
            for (int i = 0; i < ItemResizePoints.Length; i++)
            {
                var point = ItemResizePoints[i];
                if (DocumentObjectHelper.IsNear(point, mouseDownPoint, 1f))
                {
                    ResizeItemIndex = i;
                    SelectedResizePointStartLocation = point;
                    break;
                }
            }
            return ResizeItemIndex;
        }

        /// <summary>
        /// Điểm resize đã chọn hoặc đang chọn.
        /// </summary>
        [Browsable(false)]
        [Description("Điểm resize đã chọn hoặc đang chọn.")]
        public int ResizeItemIndex { get; set; }

        /// <summary>
        /// Thực hiện resize bằng cách gán vị trí mới cho resizePoint.
        /// </summary>
        /// <param name="newPoint">Vị trí mới đã trừ margins.</param>
        public override void SetSelectedResizePointLocation(PointF newPoint)
        {
            switch (ResizePointType)
            {
                case PointType.Details:
                    SetItemWidth(newPoint);
                    return;
                    break;
            }
            base.SetSelectedResizePointLocation(newPoint);
        }

        /// <summary>
        /// Thay đổi kích thước của đối tượng ô chi tiết.
        /// </summary>
        /// <param name="newPoint"></param>
        private void SetItemWidth(PointF newPoint)
        {
            if (ResizeItemIndex >= 0 && ResizeItemIndex < ItemResizePoints.Length)
            {
                var point = ItemResizePoints[ResizeItemIndex];
                var item = Items[ResizeItemIndex];
                item.Width = newPoint.X-item.Left;
            }
        }
        

        #region ==== Load ====
        public void LoadTable(DataTable detailTable)
        {
            _items = new DetailItems();
            foreach (DataRow row in detailTable.Rows)
            {
                AddDetailItem(row);
            }
        }
        private void AddDetailItem(DataRow row)
        {
            DetailItem item = GetDetailItem(row);
            Items.Add(item);
        }
        private DetailItem GetDetailItem(DataRow row)
        {
            var value = "Noname";
            if (row.Table.Columns.Contains("Name")) value = row["Name"].ToString().Trim();
            var item = new DetailItem(value);
            value = row.Table.Columns.Contains("Text") ? row["Text"].ToString().Trim() : "";
            item.Text = value;
            
            value = item.Name;
            if (row.Table.Columns.Contains("HeaderText")) value = row["HeaderText"].ToString().Trim();
            item.HeaderText = value;

            item.ViewHeader = row.Table.Columns.Contains("ViewHeader") && row["ViewHeader"].ToString().Trim() == "1";
            var width = 20f;
            if (row.Table.Columns.Contains("Width")) width = PrimitiveTypes.ObjectToFloat(row["Width"]);
            item.Width = width;
            int ali = 1;
            if (row.Table.Columns.Contains("TextAlign")) ali = PrimitiveTypes.ObjectToInt(row["TextAlign"]);
            if (ali < 1) ali = 1;
            item.TextAlign = (ContentAlignment) ali;
            item.IsSecondRow = row.Table.Columns.Contains("IsSecondRow") && row["IsSecondRow"].ToString().Trim() == "1";
            return item;
        }
        #endregion load

        #region ==== Save ====
        public void FillTable(DataTable detailTable)
        {
            detailTable.Rows.Clear();
            AddMissingColumns(detailTable);

            //Add 1st row first
            foreach (DetailItem item in _items)
            {
                if (item.IsSecondRow) continue;
                var newrow = detailTable.NewRow();
                newrow["Name"] = item.Name;
                newrow["Text"] = item.Text;
                newrow["PARAMETERNAMES"] = string.Join(";", item.ParameterNames);
                newrow["TextAlign"] = Convert.ToInt32(item.TextAlign);
                newrow["ViewHeader"] = item.ViewHeader ? "1" : "0";
                newrow["HeaderText"] = item.HeaderText;
                newrow["HeaderTextAlign"] = Convert.ToInt32(item.HeaderTextAlign);
                newrow["Width"] = item.Width.ToString(CultureInfo.InvariantCulture);
                newrow["IsSecondRow"] = item.IsSecondRow ? "1" : "0";

                detailTable.Rows.Add(newrow);
            }

            //Add 2nd row
            foreach (DetailItem item in _items)
            {
                if (!item.IsSecondRow) continue;
                var newrow = detailTable.NewRow();
                newrow["Name"] = item.Name;
                newrow["Text"] = item.Text;
                newrow["PARAMETERNAMES"] = string.Join(";", item.ParameterNames);
                newrow["TextAlign"] = Convert.ToInt32(item.TextAlign);
                newrow["ViewHeader"] = item.ViewHeader ? "1" : "0";
                newrow["HeaderText"] = item.HeaderText;
                newrow["HeaderTextAlign"] = Convert.ToInt32(item.HeaderTextAlign);
                newrow["Width"] = item.Width.ToString(CultureInfo.InvariantCulture);
                newrow["IsSecondRow"] = item.IsSecondRow ? "1" : "0";

                detailTable.Rows.Add(newrow);
            }
        }

        private void AddMissingColumns(DataTable detailTable)
        {
            if (!detailTable.Columns.Contains("Name")) detailTable.Columns.Add("Name");
            if (!detailTable.Columns.Contains("Text")) detailTable.Columns.Add("Text");
            if (!detailTable.Columns.Contains("PARAMETERNAMES")) detailTable.Columns.Add("PARAMETERNAMES");
            if (!detailTable.Columns.Contains("TextAlign")) detailTable.Columns.Add("TextAlign");
            if (!detailTable.Columns.Contains("ViewHeader")) detailTable.Columns.Add("ViewHeader");
            if (!detailTable.Columns.Contains("HeaderText")) detailTable.Columns.Add("HeaderText");
            if (!detailTable.Columns.Contains("HeaderTextAlign")) detailTable.Columns.Add("HeaderTextAlign");
            if (!detailTable.Columns.Contains("Width")) detailTable.Columns.Add("Width");
            if (!detailTable.Columns.Contains("IsSecondRow")) detailTable.Columns.Add("IsSecondRow");
        }

        #endregion save

        public class DetailItems : CollectionBase
        {

            internal DetailItems()
            {

            }

            public DetailItem this[int index]
            {
                get { return (DetailItem)List[index]; }
            }

            public void Add(DetailItem item)
            {
                //if (List.Count == 0) Parent.SelectedButton = item;
                List.Add(item);
                //item.Parent = Parent;
                //Parent.ButtonlistChanged();
            }

            public DetailItem Add(string text)
            {
                DetailItem b = new DetailItem()
                {
                    Name = text
                };
                Add(b);
                return b;
            }

            public void Remove(DetailItem item)
            {
                List.Remove(item);
                //Parent.ButtonlistChanged();
            }

            public int IndexOf(object value)
            {
                return List.IndexOf(value);
            }

            #region handle CollectionBase events
            //protected override void OnInsertComplete(int index, object value)
            //{
            //    DetailItem b = (DetailItem)value;
            //    b.Parent = parenT;
            //    Parent.ButtonlistChanged();
            //    base.OnInsertComplete(index, value);
            //}

            //protected override void OnSetComplete(int index, object oldValue, object newValue)
            //{
            //    DetailItem b = (DetailItem)newValue;
            //    b.Parent = parenT;
            //    Parent.ButtonlistChanged();
            //    base.OnSetComplete(index, oldValue, newValue);
            //}

            //protected override void OnClearComplete()
            //{
            //    Parent.ButtonlistChanged();
            //    base.OnClearComplete();
            //}
            #endregion handle CollectionBase events
        }
    }

    

    public class DetailItem // : IComponent
    {
        private bool _visible = true;
        [Description("Indicates wether the button is _enabled"), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public DetailItem()
        {
            _name = "ValueName";
            Width = 30;
        }

        public DetailItem(string name)
        {
            _name = name;
        }
        
        
        [Description("Tên trường dữ liệu. Nếu thuộc tính Text không sử dụng thì hiển thị dữ liệu theo tên trường này.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        private string _name;
        
        [DefaultValue("")]
        [Description("Định dạng text hiển thị. ví dụ: \"{DonGia:0,00}đ\", dùng tên trường bất kỳ trong phần này. Nếu dùng thì thuộc tính Name không còn ý nghĩa.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(UITypeEditor))]
        [DisplayName(@"Nội dung.")]
        public string Text
        {
            get { return _text??""; }
            set
            {
                _text = value;
                ResetParameters();
            }
        }
        private string _text = "";

        /// <summary>
        /// Lấy lại danh sách parameter trong _text để tiện việc sử dụng (tăng tốc độ xử lý khi vẽ).
        /// </summary>
        private void ResetParameters()
        {
            if (string.IsNullOrEmpty(_text))
            {
                ParameterNames = new string[] { };
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
            ParameterNames = prnames.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] ParameterNames = { };

        /// <summary>
        /// Tiêu đề cột. vd "Tên".
        /// </summary>
        [Description("Tiêu đề cột")]
        public string HeaderText { get; set; }
        [DefaultValue(true)]
        public bool ViewHeader { get { return _viewHeader; } set { _viewHeader = value; } }
        private bool _viewHeader = true;

        ///
        /// Summary:
        ///     Gets or sets the alignment of text in the label.
        ///
        /// Returns:
        ///     One of the System.Drawing.ContentAlignment values. The default is System.Drawing.ContentAlignment.TopLeft.
        ///
        /// Exceptions:
        ///   System.ComponentModel.InvalidEnumArgumentException:
        ///     The value assigned is not one of the System.Drawing.ContentAlignment values.
        [Localizable(true)]
        public virtual ContentAlignment TextAlign
        {
            get
            {
                if ((Int32)_textAlign < 1) _textAlign = ContentAlignment.MiddleLeft;
                return _textAlign;
            }
            set { _textAlign = value; }
        }

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [Localizable(true)]
        public virtual ContentAlignment HeaderTextAlign
        {
            get
            {
                if ((Int32)_headerTextAlign < 1) _headerTextAlign = ContentAlignment.MiddleCenter;
                return _headerTextAlign;
            }
            set { _headerTextAlign = value; }
        }

        private ContentAlignment _headerTextAlign = ContentAlignment.MiddleCenter;

        public float Width { get; set; }

        public bool IsSecondRow { get; set; }

        public float Left { get; set; }
    }

}
