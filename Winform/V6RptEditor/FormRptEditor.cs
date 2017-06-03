using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public partial class FormRptEditor : Form
    {
        #region ==== Setting Variable ====
        public bool allowOpen = true, allowSaveAs = true, saveWhenClose = true;
        #endregion
        public string rptPath = "";
        public static ReportDocument _rpt;
        PropertyGrid _pGrid = new PropertyGrid();
        public static ClassStatic _classStatic = new ClassStatic();
        
        public FormRptEditor()
        {
            InitializeComponent();
            //_pGrid = new PropertyGrid();
            //lblSelectedControlName = new Label();
            _pGrid.Dock = DockStyle.Fill;
            _classStatic.onObjChanged += _classStatic_onObjChanged;
            //_pGrid.HelpVisible = false;
            panelPgrid.Controls.Add(_pGrid);
        }

        void _classStatic_onObjChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();\
            _pGrid.SelectedObject = _classStatic.ObjForPGrid;

        }

        public FormRptEditor(bool canOpen, bool canSaveas, bool saveAtClose)
        {
            allowOpen = canOpen;
            allowSaveAs = canSaveas;
            saveWhenClose = saveAtClose;
            InitializeComponent();
            MyInit();
            _pGrid.Dock = DockStyle.Fill;
            _classStatic.onObjChanged += _classStatic_onObjChanged;
            //_pGrid.HelpVisible = false;
            panelPgrid.Controls.Add(_pGrid);
        }

        public void MyInit()
        {
            openToolStripMenuItem.Enabled = allowOpen;
            openToolStripMenuItem1.Enabled = allowOpen;
            saveAsToolStripMenuItem.Enabled = allowSaveAs;
            saveasToolStripMenuItem1.Enabled = allowSaveAs;
            
            //mainPanel.MouseClick += mainPanel_Click;
            //mainPanel.PreviewKeyDown += mainPanel_PreviewKeyDown;
        }

        void mainPanel_Click(object sender, EventArgs e)
        {
            mainPanel.Focus();
        }

        void mainPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("Test:"+e.KeyCode);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Beta.!!!!!");

            Twip.GetTwipRate(this.CreateGraphics());
            if (Program._args != null && Program._args.Length > 0)
            {
                LoadRpt(Program._args[0]);
            }
            else if (rptPath != "")
            {
                LoadRpt(rptPath);
            }
        }


        string openFile = "";
        private void Open()
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {   
                //status2.Text = o.FileName;
                openFile = o.FileName;
                LoadRpt(o.FileName);
            }
        }

        private DialogResult Save()
        {
            DialogResult d =MessageBox.Show("Tập tin gốc sẽ bị thay đổi.\nBạn vẫn tiếp tục lưu đè?",
                "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                try
                {
                    FormRptEditor._rpt.SaveAs(openFile);
                    MessageBox.Show("Đã lưu!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Error" + ex.Message);
                }
            }
            return d;
        }
        private void SaveAs()
        {
            if (FormRptEditor._rpt != null)
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Form1._rpt
                    //  .ExportToDisk( CrystalDecisions.Shared.ExportFormatType.CrystalReport, saveFileDialog1.FileName);

                    FormRptEditor._rpt.SaveAs(saveFileDialog1.FileName);
                    MessageBox.Show("Đã lưu!");
                }
                else MessageBox.Show("Chưa lưu!");
        }
        private void CloseForm()
        {
            Close();
        }

        public static void SetPropertySelectedObject(object o, string name)
        {
            object oldSelectObj = _classStatic.ObjForPGrid;// _pGrid.SelectedObject;
            _classStatic.ObjForPGrid = o;
            //_pGrid.SelectedObject = o;
            //lblSelectedControlName.Text = "["+ o.GetType().Name + "]: " + name;
            _classStatic.lblSelectedControlNameText = "[" + o.GetType().Name + "]: " + name;
            //if (oldSelectObj is rptText)
            //{
            //    rptText tempObj = (rptText)oldSelectObj;
            //    tempObj.Invalidate();
            //    ((vSectionPanel)tempObj.Parent).Focus();
            //}
            //else if (oldSelectObj is rptField)
            //{
            //    rptField tempObj = (rptField)oldSelectObj;
            //    tempObj.Invalidate();
            //    ((vSectionPanel)tempObj.Parent).Focus();
            //}
            //else if (oldSelectObj is rptLine)
            //{
            //    rptLine tempObj = (rptLine)oldSelectObj;
            //    tempObj.Invalidate();
            //    //((vSectionPanel)tempObj.Parent.Parent).Focus();
            //}
            //else if(oldSelectObj is rptLineDC)
            //{
            //    rptLineDC tempObj = (rptLineDC)oldSelectObj;
            //    tempObj.Invalidate();
            //    //((vSectionPanel)tempObj.Parent.Parent).Focus();
            //}
            //else if (oldSelectObj is rptPicture)
            //{
            //    rptPicture tempObj = (rptPicture)oldSelectObj;
            //    tempObj.Invalidate();
            //    ((vSectionPanel)tempObj.Parent).Focus();
            //}
        }
        
        private void ResetSectionLocation()
        {
            int x = mainPanel.DisplayRectangle.Left,
                y = mainPanel.DisplayRectangle.Top;
            foreach (Control item in mainPanel.Controls)
            {
                item.Location = new Point(x, y);
                y += item.Height;
            }
        }

        public void LoadRpt(ReportDocument rpt)
        {
            string flag = "";
            try
            {
                _rpt = rpt;
                LoadTreeView(_rpt);
                int x = 0, y = 0;
                while (mainPanel.Controls.Count > 0)
                {
                    mainPanel.Controls.RemoveAt(0);
                }
                foreach (Section section in FormRptEditor._rpt.ReportDefinition.Sections)
                {
                    //section panel
                    vSectionPanel sPanel = new vSectionPanel(section, new Point(0, 25));
                    flag = sPanel.Name;
                    try
                    {
                        LoadRptSection(sPanel, section.ReportObjects);
                    }
                    catch (Exception ex)
                    {
                    }
                    //Sự kiện này để lại đây.
                    sPanel.MouseMove += sectionPanel_MouseMove;

                    //Tiêu đề section
                    vSectionTitle sTitle = new vSectionTitle(sPanel);
                    sTitle.AutoSize = false;
                    sTitle.Height = 25;
                    sTitle.Width = sPanel.Width;
                    sTitle.Text = section.Name + " : " + section.Kind.ToString();
                    sTitle.BackColor = Color.LightGray;
                    //sectionTitle.Location = new Point(x, y);
                    sTitle.Location = new Point(0, 0);
                    sTitle.Click += sTitle_Click;

                    vSectionGroup sGroup = new vSectionGroup();
                    sGroup.Size = new Size(sPanel.Width, sPanel.Height + sTitle.Height);
                    sGroup.Location = new Point(x, y);
                    sGroup.Controls.Add(sTitle);
                    sGroup.Controls.Add(sPanel);
                    sGroup.ApplyEvents();
                    mainPanel.Controls.Add(sGroup);

                    //panel1.Controls.Add(sPanel);

                    y += sGroup.Height;
                }
                Label lableBottom = new Label();
                lableBottom.AutoSize = false;
                lableBottom.Size =
                    new Size(FormRptEditor._rpt.PrintOptions.PageContentWidth / (int)Twip.twipPerPixelX, 200);
                lableBottom.Text = "";
                lableBottom.BackColor = Color.DarkGray;
                lableBottom.Location = new Point(x, y);
                mainPanel.Controls.Add(lableBottom);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + flag);
            }
        }
        public void LoadRpt(string p)
        {
            openFile = p;
            try
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(p);
                LoadRpt(rpt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //mainPanel.Focus();
            }
        }

        void sTitle_Click(object sender, EventArgs e)
        {
            foreach (Control sGroup in mainPanel.Controls)
            {
                foreach (Control item in sGroup.Controls)
                {
                    if (item is vSectionTitle)
                    {
                        vSectionTitle temp = (vSectionTitle)item;
                        temp.ForeColor = Color.Black;
                        temp.BackColor = Color.LightGray;
                    }
                }
                
            }
            vSectionTitle send = (vSectionTitle)sender;
            send.ForeColor = Color.White;
            send.BackColor = Color.Blue;
        }

        private void LoadTreeView(ReportDocument document)
        {
            treeView1.Nodes.Clear();
            //Tao node Formular Fields
            TreeNode FormularFieldsNode = new TreeNode("Formula Fields", 1, 1);
            
            treeView1.Nodes.Add(FormularFieldsNode);
            foreach (FormulaFieldDefinition item in document.DataDefinition.FormulaFields)
            {
                int imageIndex = 8;//8 unuse, 9 used
                if (item.UseCount > 0) imageIndex = 9;
                FormularFieldsNode.Nodes.Add(item.Name, item.FormulaName, imageIndex, imageIndex);
            }
            //Parameter
            TreeNode ParameterFieldsNode = new TreeNode("Parameter Fields", 3, 3);
            treeView1.Nodes.Add(ParameterFieldsNode);
            foreach (ParameterFieldDefinition item in document.DataDefinition.ParameterFields)
            {
                int imageIndex = 8;//8 unuse, 9 used
                if (item.UseCount > 0) imageIndex = 9;
                ParameterFieldsNode.Nodes.Add(item.Name, item.FormulaName, imageIndex, imageIndex);
            }
            
        }

        void sectionPanel_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
        

        private void LoadRptSection(vSectionPanel sectionPanel, ReportObjects reportObjects)
        {
            ShapeContainer shapes = new ShapeContainer();
            shapes.Location = new System.Drawing.Point(0, 0);
            shapes.Margin = new System.Windows.Forms.Padding(0);
            shapes.Name = "shapes_" + sectionPanel.Name;
            //shapes.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {});
            //shapes.Shapes.AddRange(new rptOleDC[] {});
            shapes.Size = new System.Drawing.Size(sectionPanel.Width - 2, sectionPanel.Height-2);
            shapes.TabIndex = 0;
            shapes.TabStop = false;
            shapes.MouseDown += shapes_MouseDown;
            //shapes.MouseClick += shapes_MouseClick;
            shapes.MouseUp += shapes_MouseUp;
            shapes.MouseMove += shapes_MouseMove;     //bị lỗi khi đang chọn một line, cuộn khuất line đang chọn, click chọn một line bị khuất khác, sự kiện panel focus xảy ra làm dịch chuyển vị trí

            shapes.MouseHover += shapes_MouseHover;

            sectionPanel.Controls.Add(shapes);

            foreach (ReportObject item in reportObjects)
            {
                //string a = _rpt.DataDefinition.FormulaFields[1].Text;
                //_rpt.DataDefinition.FormulaFields[1].Text = "";

                if (item is TextObject)
                {
                    rptText text = new rptText((TextObject)item);
                    sectionPanel.Controls.Add(text);
                }   
                else if(item is FieldObject)
                {
                    FieldObject fo = (FieldObject)item;
                    switch (fo.DataSource.Kind)
                    {   
                        case CrystalDecisions.Shared.FieldKind.FormulaField:
                            rptFormulaField formulaField = new rptFormulaField(fo);
                            formulaField.ContextMenuStrip = contextMenuStripFormula;
                            sectionPanel.Controls.Add(formulaField);
                            break;
                        case CrystalDecisions.Shared.FieldKind.DatabaseField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.GroupNameField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.ParameterField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.RunningTotalField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.SQLExpressionField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.SpecialVarField:
                            //break;
                        case CrystalDecisions.Shared.FieldKind.SummaryField:
                            //break;
                        default:
                            rptField field = new rptField((FieldObject)item);
                            sectionPanel.Controls.Add(field);
                            break;
                    }
                    
                }
                else if (item is PictureObject && item.Name.StartsWith("DC"))
                {
                    rptLineDC oledc = new rptLineDC((PictureObject)item);
                    shapes.Shapes.Add(oledc);
                }
                else if (item is PictureObject)
                {
                    PictureObject p = (PictureObject)item;
                    rptPicture picture = new rptPicture((PictureObject)item);
                    //_pGrid.SelectedObject = item;
                    sectionPanel.Controls.Add(picture);
                }
                else if (item is LineObject)
                {
                    rptLine0 line0_object = new rptLine0((LineObject)item);
                    shapes.Shapes.Add(line0_object);
                }
                else if (item is BoxObject)
                {
                    MessageBox.Show("Chưa xử lý BoxObject");
                }
                else if (item is BlobFieldObject)
                {
                    //BlobFieldObject p = (BlobFieldObject)item;
                    rptPicture picture = new rptPicture(item, "BlobFieldObject");
                    //_pGrid.SelectedObject = item;
                    sectionPanel.Controls.Add(picture);
                }
                else if (item is ChartObject)
                {
                    rptPicture picture = new rptPicture(item, "ChartObject");
                    //_pGrid.SelectedObject = item;
                    sectionPanel.Controls.Add(picture);
                }
                else if (item is CrossTabObject)
                {
                    rptPicture picture = new rptPicture(item, "CrossTabObject");
                    //_pGrid.SelectedObject = item;
                    sectionPanel.Controls.Add(picture);
                }
                else if (item is MapObject || item is OlapGridObject || item is SubreportObject)
                {
                }
                else // ReportObject
                {
                    rptObj obj = new rptObj();
                    obj.Name = item.Name;
                    obj.Text = "O:" + item.Name;
                    obj.Location = new Point(Twip.TwipToPixelX(item.Left), Twip.TwipToPixelY(item.Top));
                    obj.AutoSize = false;
                    obj.Size = new Size(Twip.TwipToPixelX(item.Width), Twip.TwipToPixelY(item.Height));
                    //objobject.Border.
                    obj.ApplyEvent();
                    sectionPanel.Controls.Add(obj);
                }
            }
        }

        

        #region ===== LineShape Move =====

        

        void shapes_MouseMove(object sender, MouseEventArgs e)
        {
            int x=0, y=0;

            if (focusedLineDC != null && focusedLineDC.Name == selectedLineName)
            {
                if (dragStart)
                {
                    x = oldStart.X + e.X - oldMouseX;
                    y = oldStart.Y + e.Y - oldMouseY;
                    if (x < 0) x = 0;
                    if (y < 0) y = 0;
                    focusedLineDC.StartPoint
                        = new Point(x, y);
                }
                if (dragEnd)
                {
                    x = oldEnd.X + e.X - oldMouseX;
                    y = oldEnd.Y + e.Y - oldMouseY;
                    if (x < 1) x = 1;
                    if (y < 1) y = 1;
                    focusedLineDC.EndPoint
                        = new Point(x, y);
                }
            }
            else if(focusedLine0 != null && focusedLine0.Name == selectedLineName)
            {
                if (dragStart && dragEnd)
                {
                    x = oldStart.X + e.X - oldMouseX;
                    y = oldStart.Y + e.Y - oldMouseY;
                    if (x < 0) x = 0;
                    if (y < 0) y = 0;
                    focusedLine0.StartPoint
                        = new Point(x, y);
                
                    x = oldEnd.X + e.X - oldMouseX;
                    y = oldEnd.Y + e.Y - oldMouseY;
                    if (x < 1) x = 1;
                    if (y < 1) y = 1;
                    focusedLine0.EndPoint
                        = new Point(x, y);
                }
                else if(dragStart)
                {
                    //Nếu là Hline
                    if (focusedLine0.StartPoint.Y == focusedLine0.EndPoint.Y)
                    {
                        //Chỉ thay đổi x
                        x = oldStart.X + e.X - oldMouseX;
                        y = oldStart.Y;// +e.Y - oldMouseY;
                        if (x < 0) x = 0;
                        if (y < 0) y = 0;
                        focusedLine0.StartPoint
                            = new Point(x, y);
                    }
                    else if(focusedLine0.StartPoint.X == focusedLine0.EndPoint.X)
                    {
                        x = oldStart.X;// +e.X - oldMouseX;
                        y = oldStart.Y + e.Y - oldMouseY;
                        if (x < 0) x = 0;
                        if (y < 0) y = 0;
                        focusedLine0.StartPoint
                            = new Point(x, y);
                    }
                }
                else if (dragEnd)
                {
                    //Nếu là Hline
                    if (focusedLine0.StartPoint.Y == focusedLine0.EndPoint.Y)
                    {
                        //Chỉ thay đổi x
                        x = oldEnd.X + e.X - oldMouseX;
                        y = oldEnd.Y;// +e.Y - oldMouseY;
                    }
                    else if (focusedLine0.StartPoint.X == focusedLine0.EndPoint.X)
                    {
                        x = oldEnd.X;// +e.X - oldMouseX;
                        y = oldEnd.Y + e.Y - oldMouseY;
                    }
                    
                    if (x < 1) x = 1;
                    if (y < 1) y = 1;
                    focusedLine0.EndPoint
                        = new Point(x, y);
                    
                }
            }
        }

        int oldMouseX = 0, oldMouseY = 0;
        Point oldStart, oldEnd;
        bool dragStart = false, dragEnd = false;
        public static rptLineDC focusedLineDC = null;
        public static rptLine0 focusedLine0 = null;
        public static string selectedLineName = "";

        void shapes_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragStart || dragEnd)
            {
                if(focusedLineDC!= null)
                    focusedLineDC.UpdateChage();

                if (focusedLine0 != null)
                    focusedLine0.UpdateChage();

                dragStart = dragEnd = false;
            }
        }
        void shapes_MouseDown(object sender, MouseEventArgs e)
        {
            //set focusedLineShape in rptLine...    
            oldMouseX = e.X;
            oldMouseY = e.Y;

            if (focusedLine0 != null)
            {
                oldStart = focusedLine0.StartPoint;
                oldEnd = focusedLine0.EndPoint;
                dragStart = MouseIsNearBy(oldStart, oldMouseX, oldMouseY);
                dragEnd = MouseIsNearBy(oldEnd, oldMouseX, oldMouseY);
            }
            else if (focusedLineDC != null)
            {
                oldStart = focusedLineDC.StartPoint;
                oldEnd = focusedLineDC.EndPoint;
                dragStart = MouseIsNearBy(oldStart, oldMouseX, oldMouseY);
                dragEnd = MouseIsNearBy(oldEnd, oldMouseX, oldMouseY);
            }

            if (! dragStart && ! dragEnd){
                //'If not drag either end, then drag both.
                dragStart = true;
                dragEnd = true;
            }
        }

        void shapes_MouseHover(object sender, EventArgs e)
        {
            if (dragStart || dragEnd)
            {
                dragStart = dragEnd = false;
            }
        }

        int HitTestDelta = 3;
        bool MouseIsNearBy(Point testPoint, int mouseX, int mouseY)
        {
            //testPoint = this.PointToScreen(testPoint);
            return Math.Abs(testPoint.X - mouseX) <= HitTestDelta &&
                Math.Abs(testPoint.Y - mouseY) <= HitTestDelta;
        }

        #endregion

        

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            mainPanel.Focus();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_rpt != null)
            if (saveWhenClose)
            {
                e.Cancel = (Save() == DialogResult.Cancel);
            }
            else
            {
                if (MessageBox.Show("Có chắc bạn muốn thoát không?\n"
                    + "Hãy chắc rằng công việc đã được lưu lại.\n"
                    + "Chương trình không tự động phát hiện việc chưa lưu.",
                    "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    //this.Dispose(true);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
            }
        }

        void ShowTreeNodeContextMenu()
        {
            contextMenuStripTree1.Enabled = true;
        }

        private void toolStripMenuItem_EditFormula_Click(object sender, EventArgs e)
        {
            rptFormulaField ff;
            
            {
                ToolStripDropDownItem item = sender as ToolStripDropDownItem;
                if (item == null) // Error
                    return;
                ContextMenuStrip strip = item.Owner as ContextMenuStrip;
                ff  = strip.SourceControl as rptFormulaField;
            }
            if (ff == null) // Control wasn't a
                return;
            FormFormulaEditor f = new FormFormulaEditor(ff);
            f.ShowDialog();
            //DataObject data = grid.GetClipboardContent();
            //Clipboard.SetDataObject(data);
        }

    }
}
