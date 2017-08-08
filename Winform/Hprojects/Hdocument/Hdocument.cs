using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HaUtility.Converter;
using H_document.DocumentObjects;

namespace H_document
{
    public class Hdocument
    {
        public Hdocument()
        {
            CreateDefaultData();
        }
        public Hdocument(string sourceFile)
        {
            Load(sourceFile);
        }

        /// <summary>
        /// Bộ dữ liệu
        /// </summary>
        private DataSet _data;
        /// <summary>
        /// Bộ giá trị parameters
        /// </summary>
        private SortedDictionary<string, object> _parameters = new SortedDictionary<string, object>();
        private List<DocumentObject> _documentObjects = new List<DocumentObject>();
        //private Dictionary<string,DocumentObject> _documentObjectDic = new Dictionary<string, DocumentObject>(); 
        public DocObjectType AddObjectType = DocObjectType.None;
        public DocumentObject CopyObject = null;
        public PointF AddObjectLocationF = new PointF(1, 1);

        private void Test()
        {
            //_documentObjectDic.a
        }

        [DisplayName(@"Kích thước trang")]
        public Size PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
                OnPageSizeChanged();
            }
        }
        private Size _pageSize;
        public event EventHandler PageSizeChanged;
        protected virtual void OnPageSizeChanged()
        {
            var handler = PageSizeChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        [Description("Thay đổi kích thước trang giấy theo loại giấy.")]
        [DisplayName("Loại giấy")]
        public PageType PageType
        {
            get
            {
                var type = PageType.FreeStype;
                if (PageSize == new Size(841, 1189)) return PageType.A0;
                if (PageSize == new Size(594, 841)) return PageType.A1;
                if (PageSize == new Size(420, 594)) return PageType.A2;
                if (PageSize == new Size(297, 420)) return PageType.A3;
                if (PageSize == new Size(210, 297)) return PageType.A4;
                if (PageSize == new Size(148, 210)) return PageType.A5;
                if (PageSize == new Size(105, 148)) return PageType.A6;
                if (PageSize == new Size(74, 105)) return PageType.A7;
                if (PageSize == new Size(52, 74)) return PageType.A8;
                if (PageSize == new Size(37, 52)) return PageType.A9;
                if (PageSize == new Size(26, 37)) return PageType.A10;
                if (PageSize == new Size(216, 280)) return PageType.Letter;
                return type;
            }
            set
            {
                switch (value)
                {
                    case PageType.A0:
                        PageSize = new Size(841, 1189);
                        break;
                    case PageType.A1:
                        PageSize = new Size(594, 841);
                        break;
                    case PageType.A2:
                        PageSize = new Size(420, 594);
                        break;
                    case PageType.A3:
                        PageSize = new Size(297, 420);
                        break;
                    case PageType.A4:
                        PageSize = new Size(210, 297);
                        break;
                    case PageType.A5:
                        PageSize = new Size(148, 210);
                        break;
                    case PageType.A6:
                        PageSize = new Size(105, 148);
                        break;
                    case PageType.A7:
                        PageSize = new Size(74, 105);
                        break;
                    case PageType.A8:
                        PageSize = new Size(52, 74);
                        break;
                    case PageType.A9:
                        PageSize = new Size(37, 52);
                        break;
                    case PageType.A10:
                        PageSize = new Size(26, 37);
                        break;
                    case PageType.Letter:
                        PageSize = new Size(216, 280);
                        break;
                    case PageType.FreeStype:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
        }

        [DisplayName(@"Canh lề")]
        public Margins Margins
        {
            get { return _margins; }
            set
            {
                if (value == null) _margins = new Margins();
                else _margins = value;
            }
        }

        [DefaultValue(true)]
        [Description("Sử dụng phần chi tiết hóa đơn.")]
        [DisplayName(@"Hiện chi tiết")]
        public bool UseDetail { get; set; }

        [DisplayName(@"Phần chi tiết")]
        [Browsable(true)]
        [TypeConverter(typeof (ExpandableObjectConverter))]
        public DetailsObject Details { get; set; }

        [Browsable(false)] // Dự định làm trong tương lai.
        public DetailsObject Details2 { get; set; }

        public void SetDataSource(object data)
        {
            if (Details == null) Details = new DetailsObject();
            Details.Data = data;
        }

        /// <summary>
        /// Nếu gán sẽ gán luôn startPoint
        /// </summary>
        [Browsable(false)]
        public DocumentObject[] SelectedObjects
        {
            get { return _selectedObjects; }
            set
            {
                _selectedObjects = value;
                //if (value != null) SelecteObjectStartMovePoint = value.LocationF;
                if (value != null)
                {
                    foreach (DocumentObject o in value)
                    {
                        o.StartMovePoint = o.LocationF;
                    }
                }
            }
        }

        protected DocumentObject[] _selectedObjects;

        private Margins _margins = new Margins(10, 10, 10, 10);

        private string _file = null;

        public void Load(string file)
        {
            _file = file;
            PageSize = new Size(210, 279); //A4

            if (Details == null) Details = new DetailsObject();

            var ds = new DataSet();
            //Nếu có file thì đọc
            if (File.Exists(file))
            {
                ds.ReadXml(file);
                if (ds.Tables.Count > 0)
                {
                    Load(ds);
                }
            }
            else // Không thì tạo mới dữ liệu mẫu
            {
                CreateDefaultData();
            }
        }

        private void CreateDefaultData()
        {
            PageType = PageType.A4;
            UseDetail = true;
            Details = new DetailsObject
            {
                LocationF = new PointF(0, 100),
                ObjectSize = new SizeF(PageSize.Width - Margins.Left - Margins.Right, 5),
                Name = "Details",
                ViewLine = true
            };
            Details.Items.Add("Item1");

            _documentObjects = new List<DocumentObject>();
            
            TextObject tobj = new TextObject();
            AddDocumentObject(tobj);
            //Tạo thêm vài đối tượng mẫu khác! !!!!!
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsData">Bảng dữ liệu, mỗi dòng là một đối tượng in.</param>
        public void Load(DataSet dsData)
        {
            _documentObjects = new List<DocumentObject>();
            _data = dsData;

            #region ==== //Load document setting ====

            if (dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
            {
                var setting = dsData.Tables[0];
                var settingRow = setting.Rows[0];
                //PageWidth//PageHeight

                if (setting.Columns.Contains(Field.PAGESIZE))
                {
                    PageSize = GetSizeFromString(settingRow[Field.PAGESIZE].ToString().Trim());
                }

                if (setting.Columns.Contains(Field.MARGINS))
                {
                    Margins = GetMarginsFromString(settingRow[Field.MARGINS].ToString());
                }

                if (setting.Columns.Contains(Field.USEDETAIL))
                {
                    UseDetail = settingRow[Field.USEDETAIL].ToString().Trim() == "1";
                }
                Details = new DetailsObject();
                if (setting.Columns.Contains(Field.DETAILLINES))
                {
                    Details.ViewLine = settingRow[Field.DETAILLINES].ToString() == "1";
                }
                if (setting.Columns.Contains(Field.DETAILSECONDROW))
                {
                    Details.ViewSecondRow = settingRow[Field.DETAILSECONDROW].ToString() == "1";
                }
                if (setting.Columns.Contains(Field.DETAILLOCATION))
                {
                    string[] ss = PrimitiveTypes.ObjectToString(settingRow[Field.DETAILLOCATION]).Split(';');
                    Details.LocationF = new PointF(PrimitiveTypes.ToObject<float>(ss[0]), PrimitiveTypes.ToObject<float>(ss[1]));
                }
                if (setting.Columns.Contains(Field.DETAILSIZE))
                {
                    string[] ss = PrimitiveTypes.ObjectToString(settingRow[Field.DETAILSIZE]).Split(';');
                    Details.ObjectSize = new SizeF(PrimitiveTypes.ToObject<float>(ss[0]), PrimitiveTypes.ToObject<float>(ss[1]));
                }
                if (setting.Columns.Contains(Field.DETAILFONT))
                {
                    var fName = "Arial";
                    var fSize = 11.5f;
                    var fStyle = FontStyle.Regular;
                    var fontInfos = settingRow[Field.DETAILFONT].ToString().Split(new[] {';'}, 3);

                    if (fontInfos.Length > 0 && fontInfos[0].Length > 0) fName = fontInfos[0];
                    if (fontInfos.Length > 1 && fontInfos[1].Length > 0)
                        fSize = float.Parse(fontInfos[1], CultureInfo.InvariantCulture);
                    if (fontInfos.Length > 2 && fontInfos[2].Length > 0)
                    {
                        var fStyleInfos = fontInfos[2].ToUpper();
                        if (fStyleInfos.Contains("B")) fStyle = fStyle | FontStyle.Bold;
                        if (fStyleInfos.Contains("U")) fStyle = fStyle | FontStyle.Underline;
                        if (fStyleInfos.Contains("I")) fStyle = fStyle | FontStyle.Italic;
                        if (fStyleInfos.Contains("S")) fStyle = fStyle | FontStyle.Strikeout;
                    }
                    Details.Font = new Font(fName, fSize, fStyle);
                }
            }

            #endregion setting

            #region ==== // Load document items ====

            if (dsData.Tables.Count > 1)
            {
                var data = dsData.Tables[1];
                AddNecessaryDocumentDataColumns(data);
                foreach (DataRow row in data.Rows)
                {
                    AddDocumentObject(row);
                }
            }

            #endregion items

            #region ==== //Load document details ====

            if (dsData.Tables.Count > 2)
            {
                var data = dsData.Tables[2];
                Details.LoadTable(data);
            }

            #endregion details
        }

        private Margins GetMarginsFromString(string s)
        {
            var ssss = s.Split(new char[] {';'}, 4);
            var margins = new Margins(PrimitiveTypes.ObjectToInt(ssss[0]), PrimitiveTypes.ObjectToInt(ssss[1]), PrimitiveTypes.ObjectToInt(ssss[2]), PrimitiveTypes.ObjectToInt(ssss[3]));
            return margins;
        }

        private Size GetSizeFromString(string s)
        {
            var ss = s.Split(new char[] {';'}, 2);
            var size = new Size(PrimitiveTypes.ObjectToInt(ss[0]), PrimitiveTypes.ObjectToInt(ss[1]));
            return size;
        }

        private SizeF GetSizeFfromString(string s)
        {
            var ss = s.Split(new char[] {';'}, 2);
            var size = new SizeF(PrimitiveTypes.ObjectToFloat(ss[0]), PrimitiveTypes.ObjectToFloat(ss[1]));
            return size;
        }

        private PointF GetPointFfromString(string s)
        {
            var ss = s.Split(new char[] {';'}, 2);
            var p = new PointF(PrimitiveTypes.ObjectToFloat(ss[0]), PrimitiveTypes.ObjectToFloat(ss[1]));
            return p;
        }

        private Color GetColorFromString(string s)
        {
            if (string.IsNullOrEmpty(s)) return Color.Black;
            var splitChar = s.Contains(";") ? ';' : ',';
            var ssss = s.Split(new char[] {splitChar}, 3);
            var color = Color.FromArgb(PrimitiveTypes.ObjectToInt(ssss[0]), PrimitiveTypes.ObjectToInt(ssss[1]), PrimitiveTypes.ObjectToInt(ssss[2]));
            return color;
        }

        /// <summary>
        /// Thêm đầy đủ cột cho khỏi lỗi.
        /// </summary>
        /// <param name="table"></param>
        private void AddNecessaryDocumentDataColumns(DataTable table)
        {
            if (!table.Columns.Contains(Field.Name)) table.Columns.Add(Field.Name);
            if (!table.Columns.Contains(Field.Type)) table.Columns.Add(Field.Type);
            if (!table.Columns.Contains(Field.Location)) table.Columns.Add(Field.Location);
            if (!table.Columns.Contains(Field.Size)) table.Columns.Add(Field.Size);
            if (!table.Columns.Contains(Field.Text)) table.Columns.Add(Field.Text);
            if (!table.Columns.Contains(Field.TextAlign)) table.Columns.Add(Field.TextAlign);
            if (!table.Columns.Contains(Field.ForceColor)) table.Columns.Add(Field.ForceColor);
            if (!table.Columns.Contains(Field.BackColor)) table.Columns.Add(Field.BackColor);
            if (!table.Columns.Contains(Field.ParameterNames)) table.Columns.Add(Field.ParameterNames);
            if (!table.Columns.Contains(Field.FontName)) table.Columns.Add(Field.FontName);
            if (!table.Columns.Contains(Field.DrawLines)) table.Columns.Add(Field.DrawLines);
        }

        /// <summary>
        /// Thêm vào _data[0] một dòng dữ liệu, thiếu cột thì tự add vào.
        /// </summary>
        /// <param name="documentData">Dữ liệu các item trong Hdocument.</param>
        /// <param name="obj">Đối tượng đang thêm vào</param>
        private void AddTableDataWithObject(DataTable documentData, DocumentObject obj)
        {
            var newRow = documentData.NewRow();
            //Các thuộc tính cơ bản.
            newRow[Field.Name] = obj.Name;
            newRow[Field.Type] = obj.ObjectType.ToString();
            newRow[Field.Location] = string.Format("{0};{1}", obj.LocationF.X.ToString(CultureInfo.InvariantCulture), obj.LocationF.Y.ToString(CultureInfo.InvariantCulture));
            newRow[Field.Size] = string.Format("{0};{1}", obj.ObjectSize.Width.ToString(CultureInfo.InvariantCulture), obj.ObjectSize.Height.ToString(CultureInfo.InvariantCulture));
            
            newRow[Field.ForceColor] = string.Format("{0};{1};{2}", obj.ForceColor.R, obj.ForceColor.G, obj.ForceColor.B);
            if (obj is TextObject)
            {
                var tobj = obj as TextObject;
                newRow[Field.Text] = tobj.Text;
                newRow[Field.TextAlign] = (int) tobj.TextAlign;

                var fStyleString = "";
                if ((tobj.Font.Style & FontStyle.Bold) == FontStyle.Bold) fStyleString += "B";
                if ((tobj.Font.Style & FontStyle.Italic) == FontStyle.Italic) fStyleString += "I";
                if ((tobj.Font.Style & FontStyle.Underline) == FontStyle.Underline) fStyleString += "U";
                if ((tobj.Font.Style & FontStyle.Strikeout) == FontStyle.Strikeout) fStyleString += "S";
                if (fStyleString == "") fStyleString = "R";

                newRow[Field.FontName] = string.Format("{0};{1};{2}", tobj.Font.Name, tobj.Font.Size.ToString(CultureInfo.InvariantCulture), fStyleString);

                var content = tobj.Text;
                if (!string.IsNullOrEmpty(content))
                {
                    var prnames = "";
                    var regex = new Regex("{(.+?)}");
                    foreach (Match match in regex.Matches(content))
                    {
                        var match_key = match.Groups[1].Value;
                        prnames += ";" + match_key;
                    }
                    if (prnames.Length > 1) prnames = prnames.Substring(1);
                    newRow[Field.ParameterNames] = prnames;
                }

                //newRow[Field.ParameterNames] = string.Join(";", tobj.ParameterNames);
            }
            if (obj is LineObject)
            {
                var lobj = obj as LineObject;
                newRow[Field.DrawLines] = ((int) lobj.DrawLines).ToString();
            }
            if (obj is PictureObject)
            {
                var pobj = obj as PictureObject;
                var strImage = DOConverter.ImageToString(pobj.Picture);
                newRow[Field.ParameterNames] = strImage;
            }

            newRow[Field.BackColor] = "NoUse";


            documentData.Rows.Add(newRow);
        }

        /// <summary>
        /// Nếu mở từ xml thì lưu vào file đó. Còn chưa có thì lưu mới.
        /// </summary>
        public void Save()
        {
            if (_data == null)
            {
                _data = new DataSet("Hdocument");
            }

            #region ==== SETTING ====

            if (_data.Tables.Count == 0)
            {
                var setting = new DataTable("Setting");
                _data.Tables.Add(setting);
            }
            //Setting
            var settingTable = _data.Tables[0];
            FillSetting(settingTable);

            #endregion setting

            #region ==== DOCUMENT ITEMS ====

            if (_data.Tables.Count <= 1)
            {
                var data = new DataTable("DocumentItem");
                _data.Tables.Add(data);
            }

            var documentObjectsData = _data.Tables[1];
            documentObjectsData.Rows.Clear();
            //Tạo đầy đủ cột dữ liệu trước
            AddNecessaryDocumentDataColumns(documentObjectsData);
            foreach (DocumentObject o in _documentObjects)
            {
                AddTableDataWithObject(documentObjectsData, o);
                //documentObjectsData.Rows.Add()
            }

            #endregion details

            #region ==== DETAILS ====

            if (_data.Tables.Count <= 2)
            {
                var detail = new DataTable("Detail");
                _data.Tables.Add(detail);
            }

            var detailTable = _data.Tables[2];
            Details.FillTable(detailTable);

            #endregion details

            Save(_data);
        }

        /// <summary>
        /// Ghi thông tin setting vào DataTable
        /// </summary>
        /// <param name="settingTable"></param>
        private void FillSetting(DataTable settingTable)
        {
            //Thêm cột vào bảng dữ liệu.
            if (!settingTable.Columns.Contains(Field.PAGESIZE)) settingTable.Columns.Add(Field.PAGESIZE);
            if (!settingTable.Columns.Contains(Field.MARGINS)) settingTable.Columns.Add(Field.MARGINS);
            if (!settingTable.Columns.Contains(Field.USEDETAIL)) settingTable.Columns.Add(Field.USEDETAIL);
            if (!settingTable.Columns.Contains(Field.DETAILLOCATION)) settingTable.Columns.Add(Field.DETAILLOCATION);
            if (!settingTable.Columns.Contains(Field.DETAILSIZE)) settingTable.Columns.Add(Field.DETAILSIZE);
            if (!settingTable.Columns.Contains(Field.DETAILFONT)) settingTable.Columns.Add(Field.DETAILFONT);
            if (!settingTable.Columns.Contains(Field.DETAILLINES)) settingTable.Columns.Add(Field.DETAILLINES);
            if (!settingTable.Columns.Contains(Field.DETAILSECONDROW)) settingTable.Columns.Add(Field.DETAILSECONDROW);

            if (settingTable.Rows.Count == 0)
            {
                settingTable.Rows.Add(settingTable.NewRow());
            }
            settingTable.Rows[0][Field.PAGESIZE] = string.Format("{0};{1}", PageSize.Width, PageSize.Height);
            settingTable.Rows[0][Field.MARGINS] = string.Format("{0};{1};{2};{3}", Margins.Left, Margins.Right, Margins.Top, Margins.Bottom);
            settingTable.Rows[0][Field.USEDETAIL] = UseDetail ? "1" : "0";
            settingTable.Rows[0][Field.DETAILLOCATION] = string.Format("{0};{1}", Details.LocationF.X, Details.LocationF.Y);
            settingTable.Rows[0][Field.DETAILSIZE] = string.Format("{0};{1}", Details.ObjectSize.Width, Details.ObjectSize.Height);
            settingTable.Rows[0][Field.DETAILLINES] = Details.ViewLine ? "1" : "0";
            settingTable.Rows[0][Field.DETAILSECONDROW] = Details.ViewSecondRow ? "1" : "0";

            var fStyleString = "";
            if ((Details.Font.Style & FontStyle.Bold) == FontStyle.Bold) fStyleString += "B";
            if ((Details.Font.Style & FontStyle.Italic) == FontStyle.Italic) fStyleString += "I";
            if ((Details.Font.Style & FontStyle.Underline) == FontStyle.Underline) fStyleString += "U";
            if ((Details.Font.Style & FontStyle.Strikeout) == FontStyle.Strikeout) fStyleString += "S";
            if (fStyleString == "") fStyleString = "R";

            settingTable.Rows[0][Field.DETAILFONT] = string.Format("{0};{1};{2}", Details.Font.Name, Details.Font.Size.ToString(CultureInfo.InvariantCulture), fStyleString);
        }

        private void Save(DataSet ds)
        {
            if (string.IsNullOrEmpty(_file))
            {
                SaveAs(ds);
            }
            else
            {
                var location = Path.GetDirectoryName(_file);
                if (!string.IsNullOrEmpty(location) && !Directory.Exists(location)) Directory.CreateDirectory(location);
                ds.WriteXml(_file);
            }
        }

        public void SaveAs()
        {
        }

        private void SaveAs(DataSet ds)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = ".xml";
            sd.Filter = "Xml|*.xml|All file|*.*";
            if (sd.ShowDialog(null) == DialogResult.OK)
            {
                _file = sd.FileName;
                Save(ds);
            }
        }

        public void SaveAs(string file)
        {
        }

        private void AddDocumentObject(DataRow row)
        {
            DocumentObject dObject = GetDocumentObject(row);
            _documentObjects.Add(dObject);
        }


        public void AddDocumentObject(DocumentObject obj)
        {
            _documentObjects.Add(obj);
        }

        public void RemoveDocumentObject(DocumentObject obj)
        {
            if (_documentObjects.Contains(obj))
                _documentObjects.Remove(obj);
        }

        public void RemoveSelectedDocumentObject()
        {
            if (SelectedObjects == null) return;
            foreach (DocumentObject item in SelectedObjects)
            {
                if (_documentObjects.Contains(item))
                {
                    _documentObjects.Remove(item);
                }    
            }
        }


        public void AddParameter(string name, object value)
        {
            _parameters[name.ToUpper()] = value;
        }

        public void AddParameters(IDictionary<string, object> data)
        {
            foreach (KeyValuePair<string, object> item in data)
            {
                _parameters[item.Key.ToUpper()] = item.Value;
            }
        }

        /// <summary>
        /// Chọn máy in và in.
        /// </summary>
        public void Print(IWin32Window owner = null)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog(owner) == DialogResult.OK)
            {
                Print(pd.PrinterSettings);
            }
        }

        public void Print(PrinterSettings printerSettings, bool printLanscape = false)
        {
            try
            {
                PrintDocument document = new PrintDocument();
                PaperSize psize = new PaperSize("H", (int) (PageSize.Width/25.4f*100), (int) (PageSize.Height/25.4f*100));

                document.DefaultPageSettings.PaperSize = psize;
                document.PrinterSettings = printerSettings;
                document.DefaultPageSettings.PaperSize = psize;
                document.DefaultPageSettings.Landscape = printLanscape;
                document.PrintPage += document_PrintPage;
                document.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("In lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm thực hiện in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            SelectedObjects = null;
            DrawPage(e.Graphics, Mode.Print);
            e.HasMorePages = false;
        }

        /// <summary>
        /// Hàm vẽ document vào một graphics với mode PrintPreview
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawPage(Graphics graphics)
        {
            DrawPage(graphics, Mode.PrintPreview);
        }

        /// <summary>
        /// Hàm vẽ document với đủ các chi tiết.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="drawMode"></param>
        public void DrawPage(Graphics graphics, Mode drawMode)
        {
            try
            {
                if (_documentObjects == null || _documentObjects.Count == 0) return;
                if (drawMode == Mode.Print) SelectedObjects = null;
                // Save the GraphicsState.
                GraphicsState gs = graphics.Save();
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                //Đổi đơn vị sử dụng là mm.
                graphics.PageUnit = GraphicsUnit.Millimeter;
                //Vẽ từng object một
                foreach (DocumentObject o in _documentObjects)
                {
                    o.DrawToGraphics(graphics, Margins, _parameters, drawMode, SelectedObjects);
                }
                //Vẽ object chuẩn bị thêm
                if (AddObjectType != DocObjectType.None)
                {
                    var newObject = new DocumentObject();
                    switch (AddObjectType)
                    {
                        case DocObjectType.None:
                            break;
                        case DocObjectType.Text:
                            newObject = new TextObject();
                            break;
                        case DocObjectType.Line:
                            newObject = new LineObject();
                            break;
                        case DocObjectType.Picture:
                            newObject = new PictureObject();
                            break;
                        case DocObjectType.Copy:
                            newObject = CopyObject;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    if (newObject != null)
                    {
                        newObject.LocationF = AddObjectLocationF;
                        newObject.DrawToGraphics(graphics, Margins, _parameters, drawMode, SelectedObjects);
                    }
                }

                //Vẽ phần chi tiết
                if (UseDetail && Details != null)
                    Details.DrawToGraphics(graphics, Margins, _parameters, drawMode, SelectedObjects);

                //Vẽ đường bao margin
                if (drawMode != Mode.Print)// && drawMode != Mode.PrintPreview)
                {
                    Pen pen = new Pen(Color.LightGray, 0.1f);
                    pen.DashStyle = DashStyle.Dot;

                    graphics.DrawRectangle(pen, _margins.Left, _margins.Top, PageSize.Width - _margins.Left - _margins.Right, PageSize.Height - _margins.Top - Margins.Bottom);
                }
                graphics.Restore(gs);
                OnDrawComplete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event EventHandler DrawComplete;

        protected virtual void OnDrawComplete()
        {
            var handler = DrawComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Đọc dữ liệu thành document object.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private DocumentObject GetDocumentObject(DataRow row)
        {
            //Tạo cột trước khi lấy dữ liệu
            //AddNecessaryDocumentDataColumns(row.Table);
            DocumentObject result = new DocumentObject();
            var data = row.ToDictionary();
            if (data.ContainsKey("TYPE"))
            {
                var TYPE = data["TYPE"].ToLower();
                switch (TYPE)
                {
                    case "text":
                        result = new TextObject(data);
                        break;
                    case "line":
                        result = new LineObject(data);
                        break;
                    case "picture":
                        result = new PictureObject(data);
                        break;
                    default:
                        result = new TextObject(data);
                        break;
                }
            }
            result.Name = row[Field.Name].ToString().Trim();
            result.LocationF = GetPointFfromString(row[Field.Location].ToString().Trim());
            result.ObjectSize = GetSizeFfromString(row[Field.Size].ToString().Trim());
            //result.Text = row[Field.Text].ToString();

            if (result is TextObject)
            {
                ((TextObject)result).Text = row[Field.Text].ToString();
                ((TextObject) result).ForceColor = GetColorFromString(row[Field.ForceColor].ToString());
            }

            return result;
        }


        /// <summary>
        /// Đơn vị mm
        /// </summary>
        /// <param name="point">Vị trí trang giấy. chưa tính margin.</param>
        /// <returns></returns>
        public DocumentObject GetObject(PointF point)
        {
            var newPointF = new PointF(point.X - Margins.Left, point.Y - Margins.Top);
            DocumentObject ro = null;

            if (Details.Containt(newPointF))
            {
                return Details;
            }

            foreach (DocumentObject o in _documentObjects)
            {
                var check = o.ObjectRectangleF.Contains(newPointF);
                if (o.Containt(newPointF)) ro = o;
            }
            return ro;
        }
    }

    public enum PageType
    {
        A0,
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
        A8,
        A9,
        A10,
        Letter,
        FreeStype
    }
}
