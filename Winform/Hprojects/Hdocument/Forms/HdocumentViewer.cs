using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HaUtility.Helper;
using H_document.DocumentObjects;

namespace H_document.Forms
{
    public partial class HdocumentViewer : UserControl
    {
        
        private bool _isMouseDown;
        Point _mouseDownPoint = new Point(0,0); // Là điểm nhấn chuột xuống trên pictureBox
        Point _mouseUpPoint = new Point(0,0);   // Là điểm thả chuột trên pictureBox
        Point _mouseMovePoint = new Point(0,0); //

        //public DocObjectType AddObjectType = DocObjectType.None;
        
        public HdocumentViewer()
        {
            try
            {
                InitializeComponent();
                MyInit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void MyInit()
        {
            _thisGraphics = this.CreateGraphics();
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        }

        public override void Refresh()
        {
            base.Refresh();
            pictureBox1.Invalidate();
        }

        private Graphics _thisGraphics;

        private Hdocument _hDocument;
        public event EventHandler DocumentChanged;

        /// <summary>
        /// Kiểu vẽ
        /// </summary>
        [DefaultValue(H_document.HMode.PrintPreview)]
        [Description("Kiểu vẽ document object ra graphics.")]
        public HMode DrawMode { get { return _drawMode; } set { _drawMode = value; } }
        private HMode _drawMode = HMode.PrintPreview;

        public delegate void DocumentObjectHandler(DocumentObject obj);
        public delegate void DocumentObjectsHandler(DocumentObject[] obj);

        /// <summary>
        /// Khi gán mới sẽ cập nhật lại startPoint
        /// </summary>
        [DefaultValue(null)]
        public DocumentObject[] SelectedDocumentObjects
        {
            get
            {
                if (_hDocument == null) return null;
                return _hDocument.SelectedObjects;
                //_selectedDocumentObject;
            }
            set
            {
                _hDocument.SelectedObjects = value;

                try
                {
                    OnSelectedObjectChanged(_hDocument.SelectedObjects);
                    Invalidate();
                }
                catch
                {
                    
                }
            }
        }
        
        ///// <summary>
        ///// Khi gán mới sẽ cập nhật lại startPoint
        ///// </summary>
        //[DefaultValue(null)]
        //public DocumentObject[] SelectedDocumentObjects
        //{
        //    get
        //    {
        //        if (_hDocument == null) return null;
        //        return _selectedObjects;
        //        //_selectedDocumentObject;
        //    }
        //    set
        //    {
        //        _selectedObjects = value;

        //        try
        //        {
        //            OnSelectedObjectChanged(_selectedObjects);
        //            Invalidate();
        //        }
        //        catch
        //        {
                    
        //        }
        //    }
        //}

        private DocumentObject[] _selectedObjects;
        
        public DocumentObject MouseHoverDocumentObject
        {
            get { return _mouseHoverDocumentObject; }
            set
            {
                _mouseHoverDocumentObject = value;
                try
                {
                    OnMouseHoverObjectChanged(_mouseHoverDocumentObject);
                    Invalidate();
                }
                catch
                {
                    
                }
            }
        }
        private DocumentObject _mouseHoverDocumentObject;
        private bool _multiSelectMode;
        public event DocumentObjectsHandler SelectedObjectChanged;
        public event DocumentObjectHandler MouseHoverObjectChanged;
        protected virtual void OnSelectedObjectChanged(DocumentObject[] obj)
        {
            var handler = SelectedObjectChanged;
            if (handler != null) handler(obj);
        }
        protected virtual void OnMouseHoverObjectChanged(DocumentObject obj)
        {
            var handler = MouseHoverObjectChanged;
            if (handler != null) handler(obj);
        }

        protected virtual void OnDocumentChanged()
        {
            var handler = DocumentChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        public Hdocument HDocumentMaster
        {
            get { return _hDocument; }
            private set
            {
                _hDocument = value;
                try
                {
                    OnDocumentChanged();
                    ResetPictureBoxSize();
                    Invalidate();
                }
                catch { }
            }
        }

        #region ===== Methods =====

        public void AddDocumentObject(DocObjectType doType, Point point)
        {
            DocumentObject obj = new DocumentObject();
            var pointF0 = PixelPointToMilimetPoint(point);
            var pointF = new PointF(pointF0.X - HDocumentMaster.Margins.Left, pointF0.Y - HDocumentMaster.Margins.Top);
            switch (doType)
            {
                case DocObjectType.None:
                    break;
                case DocObjectType.Text:
                    obj = new TextObject();
                    break;
                case DocObjectType.Line:
                    obj = new LineObject();
                    break;
                case DocObjectType.Picture:
                    obj = new PictureObject();
                    break;
                case DocObjectType.Copy:
                    obj = HDocumentMaster.CopyObject;
                    break;
                default:
                    break;
            }
            HDocumentMaster.AddObjectType = DocObjectType.None;
            if (obj != null)
            {
                obj.LocationF = pointF;
                HDocumentMaster.AddDocumentObject(obj);
            }
        }

        private void ResetPictureBoxSize()
        {
            try
            {
                if (_hDocument == null) return;
                pictureBox1.Width = (int) MilimetToPixel(_hDocument.PageSize.Width);
                pictureBox1.Height = (int) MilimetToPixel(_hDocument.PageSize.Height);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDocument(Hdocument document)
        {
            try
            {
                if (HDocumentMaster != null)
                {
                    HDocumentMaster.PageSizeChanged -= document_PageSizeChanged;
                }
                HDocumentMaster = document;
                document.PageSizeChanged += document_PageSizeChanged;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void document_PageSizeChanged(object sender, EventArgs e)
        {
            ResetPictureBoxSize();
        }

        public float PixelToMilimeter(int pixel)
        {
            return pixel/_thisGraphics.DpiX*25.4f;
        }

        public int MilimetToPixel(float mili)
        {
            return (int) (mili/25.4*_thisGraphics.DpiX);
        }

        public PointF PixelPointToMilimetPoint(Point point)
        {
            return new PointF(PixelToMilimeter(point.X), PixelToMilimeter(point.Y));
        }

        public Point MilimetPointToPixelPoint(PointF point)
        {
            return new Point(MilimetToPixel(point.X), MilimetToPixel(point.Y));
        }

        /// <summary>
        /// Point có đơn vị pixel
        /// </summary>
        /// <param name="point">MouseDownPoint</param>
        public DocumentObject[] GetDocumentObjectAt(Point point)
        {
            var pF = new PointF(PixelToMilimeter(point.X), PixelToMilimeter(point.Y));
            var o = GetDocumentObject(pF);
            return o == null ? null : new[] {o};
        }

        /// <summary>
        /// Tìm 1 document object. Nếu không có trả về null.
        /// </summary>
        /// <param name="point">Vị trí trên document tính theo mm chưa tính margin.</param>
        public DocumentObject GetDocumentObject(PointF point)
        {
            if (HDocumentMaster == null) return null;

            try
            {
                return HDocumentMaster.GetObject(point);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion methods

        //============= EVENTS ===============

        private void DoMouseDown()
        {
            try
            {
                _multiSelectMode = false;
                _resizePointType = PointType.None;
                //Tính toán điểm bấm trừ đi margins
                var newPoint = PixelPointToMilimetPoint(_mouseDownPoint);
                newPoint.X = newPoint.X - _hDocument.Margins.Left;
                newPoint.Y = newPoint.Y - _hDocument.Margins.Top;

                // Nếu đã có chọn control trước đó.
                if (_hDocument.SelectedObjects != null)
                {
                    //Chuẩn bị check resign//Neu bam trung ResizePoint
                    
                    foreach (DocumentObject o in SelectedDocumentObjects)
                    {
                        o.SetStatus(newPoint);
                        _resizePointType = o.GetStatus(newPoint);
                        if (_resizePointType > 0)
                        {
                            break; // ra khỏi for , mode resize
                        }
                        
                        o.StartMovePoint = o.LocationF;
                        

                        //if (o.DesignMode == Mode.None)//Không move hoặc resize => chon control khac
                        //{
                        //    allModeNone = false;
                        //}
                        //else // Cập nhập lại startPoint cho move
                        //{
                        //    o.StartMovePoint = o.LocationF;
                        //}
                    }
                    

                    // Code cũ
                    //SelectedDocumentObject.SetStatus(newPoint);
                    //if (SelectedDocumentObject.DesignMode == Mode.None)//Không move hoặc resize => chon control khac
                    //{
                    //    SelectedDocumentObject = GetDocumentObjectAt(_mouseDownPoint);
                    //    if (SelectedDocumentObject != null)
                    //    {
                    //        SelectedDocumentObject.SetStatus(newPoint);
                    //        _hDocument.SelecteObjectStartMovePoint = SelectedDocumentObject.LocationF;
                    //    }
                    //}
                    //else // Cập nhập lại startPoint cho move
                    //{
                    //    _hDocument.SelecteObjectStartMovePoint = SelectedDocumentObject.LocationF;
                    //}
                }

                //Lấy lại selected mới nếu bấm ngoài những cái đã chọn.
                //if (allModeNone)
                if(_resizePointType == PointType.None)
                {
                    SelectedDocumentObjects = GetDocumentObjectAt(_mouseDownPoint);
                    if (SelectedDocumentObjects != null)
                    {
                        foreach (DocumentObject o in SelectedDocumentObjects)
                        {
                            o.SetStatus(newPoint);
                            _resizePointType = o.GetStatus(newPoint);
                            if (_resizePointType > 0)
                            {
                                break; // ra khỏi for , mode resize
                            }
                            o.StartMovePoint = o.LocationF;
                        }
                    }
                    else
                    {
                        _multiSelectMode = true;
                    }
                }

                if (!_multiSelectMode && _resizePointType > 0 && _resizePointType != PointType.Details && SelectedDocumentObjects != null)
                {
                    foreach (DocumentObject o in SelectedDocumentObjects)
                    {
                        o.ResizePointType = _resizePointType;
                        o.SelectedResizePointStartLocation = o.ResizePoints[((int)_resizePointType - 1)];
                    }
                }

                //else // Nếu chưa có chọn trước đó
                //{
                //    SelectedDocumentObjects = GetDocumentObjectAt(_mouseDownPoint);
                //    if (SelectedDocumentObject != null)
                //    {
                //        SelectedDocumentObject.SetStatus(newPoint);
                //        _hDocument.SelecteObjectStartMovePoint = SelectedDocumentObject.LocationF;
                //    }
                //    else // không chọn trúng control nào. Bắt đầu quá trình multi select.
                //    {
                //        _multiSelectMode = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DoMouseMove(MouseButtons button)
        {
            try
            {
                if (_isMouseDown)
                {
                    if (_multiSelectMode) // Vẽ vùng chọn, sau khi thả ra sẽ tính toán các object được chọn.
                    {
                        
                    }
                    else
                    if (SelectedDocumentObjects != null && button == MouseButtons.Left && _drawMode == HMode.Design)
                    {
                        if (_resizePointType > 0)
                        {
                            ResizeSelectedDocumentObject();
                        }
                        else if (_resizePointType == 0)
                        {
                            MoveSelectedDocumentObject();
                        }


                        //if (SelectedDocumentObject.DesignMode == Mode.Resize)
                        //{
                        //    ResizeSelectedDocumentObject();
                        //}
                        //else if (SelectedDocumentObject.DesignMode == Mode.Move)
                        //{
                        //    MoveSelectedDocumentObject();
                        //}
                    }
                }
                else
                {
                    if (HDocumentMaster.AddObjectType == DocObjectType.None)
                    {
                        MouseHoverDocumentObject = GetDocumentObject(_mouseMovePoint);//code sai, chua dung
                    }
                    else// (HDocumentMaster.AddObjectType != DocObjectType.None)
                    {
                        var newPoint = PixelPointToMilimetPoint(_mouseMovePoint);
                        newPoint.X = newPoint.X - _hDocument.Margins.Left;
                        newPoint.Y = newPoint.Y - _hDocument.Margins.Top;
                        HDocumentMaster.AddObjectLocationF = newPoint;
                    }
                }

                


                pictureBox1.Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseMovePoint = e.Location;
            DoMouseMove(e.Button);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            _mouseDownPoint = e.Location;

            DoMouseDown();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
            _mouseUpPoint = e.Location;
            DoMouseUp();
        }

        private void DoMouseUp()
        {
            _multiSelectMode = false;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            DoMouseEnter();
        }

        private void DoMouseEnter()
        {
            Invalidate();
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            DoMouseHover();
        }

        private void DoMouseHover()
        {
            //_multiSelectMode = false;
        }

        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            OnMouseWheel(e);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DoMouseClick();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Di chuyển object một khoảng từ mouseDown đến mouseMove point.
        /// Tính từ vị trí gốc
        /// </summary>
        private void MoveSelectedDocumentObject()
        {
            try
            {
                PointF distant = new PointF(PixelToMilimeter(_mouseMovePoint.X - _mouseDownPoint.X), PixelToMilimeter(_mouseMovePoint.Y - _mouseDownPoint.Y));
                //SelectedDocumentObject.LocationF = new PointF(_hDocument.SelecteObjectStartMovePoint.X + distant.X, _hDocument.SelecteObjectStartMovePoint.Y + distant.Y);
                foreach (DocumentObject o in SelectedDocumentObjects)
                {
                    o.LocationF = new PointF(o.StartMovePoint.X + distant.X, o.StartMovePoint.Y + distant.Y);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private PointType _resizePointType = PointType.None;
        private void ResizeSelectedDocumentObject()
        {
            try
            {
                SizeF distant = new SizeF(PixelToMilimeter(_mouseMovePoint.X - _mouseDownPoint.X), PixelToMilimeter(_mouseMovePoint.Y - _mouseDownPoint.Y));
                foreach (DocumentObject o in SelectedDocumentObjects)
                {
                    o.ResizeByPoint(_resizePointType, distant);//-1 do nguoi lap trinh
                }
                //PointF newPoint = new PointF(
                //    PixelToMilimeter(_mouseMovePoint.X) - HDocumentMaster.Margins.Left,
                //    PixelToMilimeter(_mouseMovePoint.Y) - HDocumentMaster.Margins.Top);
                //SelectedDocumentObject.SetSelectedResizePointLocation(newPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_hDocument == null) return;
                //Graphics g = pictureBox1.CreateGraphics();

                _hDocument.DrawPage(e.Graphics, _drawMode);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //DoClick();
        }

        private void DoMouseClick()
        {
            try
            {
                Focus();
                if (_drawMode == HMode.Design && HDocumentMaster.AddObjectType != DocObjectType.None)
                {
                    AddDocumentObject(HDocumentMaster.AddObjectType, _mouseDownPoint);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DoBold()
        {
            try
            {
                if (SelectedDocumentObjects != null && SelectedDocumentObjects.Length > 0)
                {
                    var isBold = SelectedDocumentObjects.OfType<TextObject>().Select(tobj => tobj.ChangeBold()).FirstOrDefault();
                    foreach (var tobj in SelectedDocumentObjects.OfType<TextObject>())
                    {
                        tobj.SetBold(isBold);
                    }
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HdocumentViewer DoBold " + ex.Message);
            }
        }

        public void DoItalic()
        {
            try
            {
                if (SelectedDocumentObjects != null && SelectedDocumentObjects.Length > 0)
                {
                    bool isItalic = SelectedDocumentObjects.OfType<TextObject>().Select(tobj => tobj.ChangeItalic()).FirstOrDefault();
                    foreach (var tobj in SelectedDocumentObjects.OfType<TextObject>())
                    {
                        tobj.SetItalic(isItalic);
                    }
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HdocumentViewer DoItalic " + ex.Message);
            }
        }
        
        public void DoUnderLine()
        {
            try
            {
                if (SelectedDocumentObjects != null && SelectedDocumentObjects.Length > 0)
                {
                    bool isUnderLine = SelectedDocumentObjects.OfType<TextObject>().Select(tobj => tobj.ChangeFontStyle(FontStyle.Underline)).FirstOrDefault();
                    foreach (var tobj in SelectedDocumentObjects.OfType<TextObject>())
                    {
                        tobj.SetFontStyle(FontStyle.Underline, isUnderLine);
                    }
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HdocumentViewer DoUnderLine " + ex.Message);
            }
        }

        public void DoChangeFontStyle(FontStyle change)
        {
            try
            {
                if (SelectedDocumentObjects != null && SelectedDocumentObjects.Length > 0)
                {
                    bool isTrue = SelectedDocumentObjects.OfType<TextObject>().Select(tobj => tobj.ChangeFontStyle(change)).FirstOrDefault();
                    foreach (var tobj in SelectedDocumentObjects.OfType<TextObject>())
                    {
                        tobj.SetFontStyle(change, isTrue);
                    }
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HdocumentViewer DoChangeFontStyle " + ex.Message);
            }
        }
        
        public void DoChangeTextAlign(ContentAlignment changeTo)
        {
            try
            {
                if (SelectedDocumentObjects != null && SelectedDocumentObjects.Length > 0)
                {
                    foreach (var tobj in SelectedDocumentObjects.OfType<TextObject>())
                    {
                        tobj.TextAlign = changeTo;
                    }
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HdocumentViewer DoChangeTextAlign " + ex.Message);
            }
        }

        public void CreateNew()
        {
            _hDocument = new Hdocument();
            _hDocument.PageSizeChanged += document_PageSizeChanged;
            ResetPictureBoxSize();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = pictureBox1.Width + 9;
            panel1.Height = pictureBox1.Height + 9;
        }

        
    }
}
