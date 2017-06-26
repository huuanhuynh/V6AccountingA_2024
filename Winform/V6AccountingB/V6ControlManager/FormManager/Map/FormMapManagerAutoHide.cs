using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using H;
using V6AccountingBusiness;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.ReportManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.Map
{
    public partial class FormMapManagerAutoHide : V6FormControl
    {
        public enum Panel2State
        {
            Minimum,Maximum
        }
        Panel2State panel2State = Panel2State.Minimum;

        //private string xmlFileName = "configFormMapCategoryManager.xml";
        //private XmlConfig m_XmlConfig;
        private MapviewHelper MH = new MapviewHelper();

        bool loaded = false;

        public FormMapManagerAutoHide()
        {
            InitializeComponent();
            //m_XmlConfig = new XmlConfig(xmlFileName);
            MyInit();
        }

        private KhoParams khoparams = null;
        public FormMapManagerAutoHide(KhoParams kparams)
        {
            InitializeComponent();
            //m_XmlConfig = new XmlConfig(xmlFileName);
            khoparams = kparams;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                DateTime datetime = V6BusinessHelper.GetServerDateTime();
                MH.DateFrom = datetime.Date;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Size = panel1_Image.Size;
                //Gán hết các setting cho MH!!!
                MH._groupType = "KH";
                MH._groupTableName = "Alkho";
                MH._groupTableName_Ct = "Alkhoct1";
                MH._groupIDfieldName = "Ma_kho";
                MH._groupKeyFieldName = "Ma_kho";
                MH._groupSelectString = "Select ma_kho, ten_kho, ten_kho2 ";
                MH._procLoadItemsColor = "VPA_DETAIL_MA_HINH2MA_VITRI";

                MH.LoadColorList();
                LoadGroupList();
                //MH.LoadPolygonDicColor();

                loaded = true;
                timerRefresh.Start();
                if (V6Setting.Language == "E")
                    rbtEnglish.Checked = true;
                else
                {
                    rbtTiengViet.Checked = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Load", ex);
            }
        }


        #region ==== Image ====
        private void ChangePictureBoxImage(Image image)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = image;
        }
        
        private void UpdateImage(string imgFile)
        {
            ChangePictureBoxImage(Picture.LoadCopyImage(imgFile));

            string ext = Path.GetExtension(imgFile);
            string newFileName = "Images\\" + MH._groupID + ext;
            File.Copy(imgFile, newFileName, true);
            //Update du lieu combobox
            int length = MH.tableGroupList.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                DataRow row = MH.tableGroupList.Rows[i];
                if (row[0].ToString().Trim() == MH._groupID)
                {
                    row["FILE_HINH"] = newFileName;
                    MH.UpdateImage(MH._groupID, newFileName);
                    break;
                }
            }
        }
        private void UpdateImageNone(string imgFile)
        {
            ChangePictureBoxImage(Picture.LoadCopyImage(imgFile));

            string ext = Path.GetExtension(imgFile);
            string newFileName = "Images\\" + "NoGroup.bmp";

            File.Copy(imgFile, newFileName, true);
        }
        #endregion


        void LoadGroupList()
        {
            try
            {
                string where = "MA_TD1='H'";
                MH.LoadGroupList(where);

                comboBoxGroup.DisplayMember = "NAME";
                comboBoxGroup.ValueMember = "ID";
                comboBoxGroup.DataSource = MH.tableGroupList;
                comboBoxGroup.DisplayMember = "NAME";
                comboBoxGroup.ValueMember = "ID";

                //Nếu có default setting có
                foreach (DataRow row in MH.tableGroupList.Rows)
                {
                    if (row["ID"].ToString().Trim().ToUpper() == "A1")// m_XmlConfig.m_ReportInfo.DefaultGroup.ToUpper())
                    {
                        comboBoxGroup.SelectedValue = row["ID"];

                        LoadItemsDataForSelectedGroup(row["ID"].ToString().Trim());
                        return;
                    }
                }
                LoadItemsDataForSelectedGroup(comboBoxGroup.SelectedValue.ToString().Trim());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        
        /// <summary>
        /// Load hình lên form theo group id 
        /// Load list items
        /// Get poligon
        /// LoadPolygonDicColor
        /// </summary>
        /// <param name="groupID"></param>
        public void LoadItemsDataForSelectedGroup(string groupID)
        {
            MH._groupID = groupID;

            //Lấy ảnh
            int rowcount = MH.tableGroupList.Rows.Count;

            //LoadImage
            if (!string.IsNullOrEmpty(groupID))
            {
                for (int i = 0; i < rowcount; i++)
                {
                    DataRow row = MH.tableGroupList.Rows[i];
                    if (row["ID"].ToString().Trim() == groupID)
                    {
                        try
                        {
                            var image = MH.GetImageData(groupID);
                            ChangePictureBoxImage(image);
                        }
                        catch
                        {

                        }

                        break;
                    }
                }
            }
            else
            {
                var image = Picture.LoadCopyImage("Images\\NoGroup.bmp");
                if (image == null) image = Picture.LoadCopyImage("Images\\Noimage.gif");

                ChangePictureBoxImage(image);
            }

            //Load dữ liệu
            string where = "";
            MH.LoadItemData(groupID, where);
            MH.GetPolygonInfosDictionary();//Có tạo luôn thư mục bên trong
            MH.LoadPolygonDicColor();//.LoadItemsDataColor(MH._groupID);
            //MH.CreateFolders();      // add 14/09/2015    //đã tạo trong MH.GetPolygonInfosDictionary();

            MH.hoverPolygonID = null;
            MH.selectedPolygonID = null;
            MH.editPolygon = null;
            MH.newPolygon = new List<Point>();

            //dataGridView1.DataSource = MH.itemData;
        }

        
        void SaveData()
        {
            V6Tools.V6Export.Data_Table.ToTextFile(MH.itemData, "data.txt");
        }
        
        private void MouseMoveOnPictureBox(Point mouse)
        {
            panel2State = Panel2State.Minimum;

            MH.mouseLocation = mouse;
            MH.hoverPolygonID = MH.GetHoverPolygonID(mouse);
            if(MH.editMode == EditMode.No)
            {
                pictureBox1.Cursor = Cursors.Default;
            }
            else if(MH.editMode == EditMode.EditReplace)
            {
                pictureBox1.Cursor = Cursors.Cross;
            }
            else if(MH.editMode == EditMode.EditMove)
            {
                switch (MH.moveMode)
                {
                    case MoveMode.None:
                        break;
                    case MoveMode.NearPoint:
                        MH.editPolygon[MH.editPointIndex] = MH.mouseLocation;
                        break;
                    case MoveMode.EditPolygon:
                        MH.MoveOldPolygon(
                            ref MH.editPolygon,
                            MH.selectedPolygonClone,
                            MH.downLocation, MH.mouseLocation);
                        break;
                    default:
                        break;
                }

                if (HDrawing.IsNearPolygonPoint(MH.editPolygon, MH.mouseLocation) >= 0)
                {
                    pictureBox1.Cursor = Cursors.Hand;

                }
                else if (HDrawing.IsInPolygon1(MH.editPolygon, MH.mouseLocation))
                {
                    pictureBox1.Cursor = Cursors.SizeAll;
                }
                else
                {
                    pictureBox1.Cursor = Cursors.Default;
                }
            }
            pictureBox1.Invalidate();
        }

        public void MouseDoubleClickOnPicBox(Point mouse)
        {
            if (MH.editMode == EditMode.No)
            {
                //if (!string.IsNullOrEmpty(MH.hoverPolygonID))
                    //ViewDoc(MH.hoverPolygonID);
            }
        }

        public void MouseLeftDownOnPictureBox1(Point mouse)
        {
            MH.downLocation = mouse;
            if (MH.editMode == EditMode.No)
            {
                if (!string.IsNullOrEmpty(MH.hoverPolygonID))
                {
                    if (MH.hoverPolygonID != MH.selectedPolygonID)
                    {
                        Select(MH.hoverPolygonID);
                    }

                    // Viết code hiển thị report mới.
                    ShowReport(MH.selectedPolygonID);
                }
            }
            else if (MH.editMode == EditMode.EditReplace)
            {
                MH.newPolygon.Add(mouse);
            }
            else if (MH.editMode == EditMode.EditMove)
            {
                MH.editPointIndex
                    = HDrawing.IsNearPolygonPoint(MH.editPolygon, MH.mouseLocation);
                if (MH.editPointIndex >= 0)
                {
                    MH.moveMode = MoveMode.NearPoint;
                }
                else if (HDrawing.IsInPolygon1(MH.editPolygon, MH.mouseLocation))
                {
                    MH.moveMode = MoveMode.EditPolygon;
                    MH.selectedPolygonClone = HDrawing.ClonePolygon(MH.editPolygon);
                }
                else
                {
                    MH.moveMode = MoveMode.None;
                }
            }
        }

        private void Select(string id)
        {
            MH.selectedPolygonID = id;

            
        }

        private bool showreport = false;
        private void ShowReport(string ma_hinh)
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;
                filterData["MA_HINH"] = MH.selectedPolygonID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = true,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program,
                    ReportProcedure = khoparams.ReportProcedure,
                    ReportFile = khoparams.ReportFile,
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }

        private void ShowReportKhu()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;
                //filterData["ADVANCE"] = MH.selectedPolygonID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "A",
                    ReportProcedure = khoparams.ReportProcedure + "A",
                    ReportFile = khoparams.ReportFile + "A",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }

        private void ShowReportDay()
        {
            showreport = true;
            try
            {
                //var oldKeys = new List<SqlParameter>();
                //oldKeys.Add(new SqlParameter("@MA_KHO", MH._groupID));
                //oldKeys.Add(new SqlParameter("@MA_HINH", ma_hinh));
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;
                filterData["MA_HINH"] = MH.selectedPolygonID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "B",
                    ReportProcedure = khoparams.ReportProcedure + "B",
                    ReportFile = khoparams.ReportFile + "B",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }

        private void ShowReporBaocao1()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;
                
                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "C",
                    ReportProcedure = khoparams.ReportProcedure + "C",
                    ReportFile = khoparams.ReportFile + "C",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporBaocao2()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "D",
                    ReportProcedure = khoparams.ReportProcedure + "D",
                    ReportFile = khoparams.ReportFile + "D",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporBaocao3()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "E",
                    ReportProcedure = khoparams.ReportProcedure + "E",
                    ReportFile = khoparams.ReportFile + "E",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporBaocao4()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "F",
                    ReportProcedure = khoparams.ReportProcedure + "F",
                    ReportFile = khoparams.ReportFile + "F",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporThongke1()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "G",
                    ReportProcedure = khoparams.ReportProcedure + "G",
                    ReportFile = khoparams.ReportFile + "G",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporThongke2()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "H",
                    ReportProcedure = khoparams.ReportProcedure + "H",
                    ReportFile = khoparams.ReportFile + "H",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void ShowReporThongke3()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "I",
                    ReportProcedure = khoparams.ReportProcedure + "I",
                    ReportFile = khoparams.ReportFile + "I",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }

        private void ShowReporThongke4()
        {
            showreport = true;
            try
            {
                var filterData = new SortedDictionary<string, object>();
                filterData["MA_KHO"] = MH._groupID;

                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = false,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = null,
                    Program = khoparams.Program + "K",
                    ReportProcedure = khoparams.ReportProcedure + "K",
                    ReportFile = khoparams.ReportFile + "K",
                    ReportCaption = khoparams.ReportCaption,
                    ReportCaption2 = khoparams.ReportCaption2,
                    //ReportFileF5 = khoparams.ReportFile,
                    //FilterControlInitFilters = oldKeys,
                    //FilterControlString1 = FilterControl.String1,
                    //FilterControlString2 = FilterControl.String2,
                    //FilterControlFilterData = FilterControl.FilterData,
                    FilterData = filterData,
                    FormTitle = "Chi tiết",
                };
                QuickReportManager.ShowReportR(this, quick_params);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowReport", ex);
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(MH.editMode == EditMode.No)
            {
                
            }
            else if(MH.editMode == EditMode.EditReplace)
            {
                
            }
            else if(MH.editMode == EditMode.EditMove)
            {

            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClickOnPicBox(e.Location);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!pictureBox1.Focused) pictureBox1.Focus();
                MouseLeftDownOnPictureBox1(e.Location);
                pictureBox1.Invalidate();

                if (showreport)
                {
                    showreport = false;
                }
                else
                {
                    scrolling = true;
                }
            }
        }

        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MH.moveMode = MoveMode.None;
            scrolling = false;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveOnPictureBox(e.Location);
            ScrollImage(e.Location);
        }

        bool scrolling = false;
        //int oldScrollX = 0, oldScrollY = 0;
        private void ScrollImage(Point mousePoint)
        {
            try
            {
                if (scrolling)
                {
                    Point changePoint = new Point(mousePoint.X - MH.downLocation.X,
                                  mousePoint.Y - MH.downLocation.Y);
                    panel1_Image.AutoScrollPosition
                        = new Point(-panel1_Image.AutoScrollPosition.X - changePoint.X,
                                    -panel1_Image.AutoScrollPosition.Y - changePoint.Y);
                }
            }
            catch
            {
                
            }
        }
        
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                MH.DrawOnPictureBoxPaint(e.Graphics);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (loaded)
            try
            {
                if (keyData == Keys.Escape)
                {
                    pictureBox1.Cursor = Cursors.Default;
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey0", ex);
            }
            return base.DoHotKey0(keyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(loaded)
            try
            {

                if (e.KeyCode == Keys.Escape)
                {
                    pictureBox1.Cursor = Cursors.Default;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    
                }
                

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyUpError: " + ex.Message);
            }
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        
        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grID = comboBoxGroup.SelectedValue.ToString().Trim();
            if (loaded)
                LoadItemsDataForSelectedGroup(grID);
            grbKhuVuc.Text = grID;
        }
        private void chkNone_CheckedChanged(object sender, EventArgs e)
        {
            if(chkNone.Checked)
            {
                comboBoxGroup.Enabled = false;
                LoadItemsDataForSelectedGroup("");
            }
            else
            {
                comboBoxGroup.Enabled = true;
                LoadItemsDataForSelectedGroup(comboBoxGroup.SelectedValue.ToString().Trim());
            }
        }
        
        private void rbtEnglish_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtEnglish.Checked)
            //{
            //    if (V6Library.UtilityHelper.WriteRegistry("DFLANG", "E"))
            //        MainForm.CurrentLang = "E";
            //}
            //else
            //{
            //    if (V6Library.UtilityHelper.WriteRegistry("DFLANG", "V"))
            //        MainForm.CurrentLang = "V";
            //}
            //FormHelper.SetFormControlsText(this, m_XmlConfig.m_ListControlInfo, MainForm.CurrentLang);
            //FormHelper.SetFormControlsText(this.contextMenuStrip_DataGrid, m_XmlConfig.m_ListControlInfo, MainForm.CurrentLang);
            //FormHelper.SetFormControlsText(this.contextMenuStrip_Pic1, m_XmlConfig.m_ListControlInfo, MainForm.CurrentLang);
            //FormHelper.SetFormControlsText(this.contextMenuStrip_Pic2, m_XmlConfig.m_ListControlInfo, MainForm.CurrentLang);
            //FormHelper.SetFormControlsText(this.contextMenuStrip_Pic1, m_XmlConfig.m_ListDynamicControl, MainForm.CurrentLang);
            //FormHelper.SetFormControlsText(this.contextMenuStrip_Pic2, m_XmlConfig.m_ListDynamicControl, MainForm.CurrentLang);

            //MainForm.myMessage.messageLang = MainForm.CurrentLang;
        }

        int secCount_x10 = 0;
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                if (secCount_x10 / 10 >= 60)
                {
                    secCount_x10 = 0;
                    if (loaded)
                        MH.LoadPolygonDicColor();
                    pictureBox1.Invalidate();
                }
                else
                {
                    secCount_x10++;
                }
            }
            catch
            {
                
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }
        
        
        
        private void ItemPictureReport_Click(object sender, EventArgs e)
        {
            //Bitmap bmp1 = new Bitmap(pictureBox1.Image ?? new Bitmap(1, 1));
            //Bitmap bmp2 = new Bitmap(pictureBox2.Image ?? new Bitmap(2, 1));
            //Image img1 = (Image)bmp1.Clone();
            //Image img2 = (Image)bmp2.Clone();

            //Graphics g = Graphics.FromImage(img1);
            //MH.DrawSelectedPolygon(g);

            //FormBaoCaoHinhAnh frm = new FormBaoCaoHinhAnh(
            // new Image[] { img1, img2 }, "configBaoCaoHinhAnh.xml");
            //frm.Param3 = MH.selectedPolygonID;

            //frm.MdiParent = ((MainForm)this.MdiParent);
            //((MainForm)this.MdiParent).AddWindowsMenu(frm);
            //frm.WindowState = FormWindowState.Maximized;
            //frm.Show();
        }

        

        private void panelContainer2_MouseEnter(object sender, EventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void panelContainer2_MouseHover(object sender, EventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void panelContainer2_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void panelContainer2_MouseLeave(object sender, EventArgs e)
        {
            //panel2State = Panel2State.Minimum;
        }

        private void panelContainer1_MouseHover(object sender, EventArgs e)
        {
            panel2State = Panel2State.Minimum;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void crystalReportViewer1_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Maximum;
        }

        private void panel1_Image_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Minimum;
        }

        private void panelContainer1_MouseMove(object sender, MouseEventArgs e)
        {
            panel2State = Panel2State.Minimum;
        }

        private void menuPic1ReportKhu_Click(object sender, EventArgs e)
        {
            ShowReportKhu();
        }

        private void menuPic1ReportDay_Click(object sender, EventArgs e)
        {
            ShowReportDay();
        }

        private void Baocao1_Click(object sender, EventArgs e)
        {
           ShowReporBaocao1();
        }

        private void Baocao2_Click(object sender, EventArgs e)
        {
           ShowReporBaocao2();
        }

        private void Baocao3_Click(object sender, EventArgs e)
        {
           ShowReporBaocao3();
        }

        private void Baocao4_Click(object sender, EventArgs e)
        {
            ShowReporBaocao4();
        }

        private void Thongke1_Click(object sender, EventArgs e)
        {
            ShowReporThongke1();
        }

        private void Thongke2_Click(object sender, EventArgs e)
        {
            ShowReporThongke2();
        }

        private void Thongke3_Click(object sender, EventArgs e)
        {
            ShowReporThongke3();
        }

        private void Thongke4_Click(object sender, EventArgs e)
        {
            ShowReporThongke4();
        }


    }
    //end class
}
