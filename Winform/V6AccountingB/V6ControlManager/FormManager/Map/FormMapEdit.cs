using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using H;
using HaUtility.Helper;
using V6AccountingBusiness;
using V6ControlManager.FormManager.KhoHangManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.Map
{
    public partial class FormMapEdit : V6FormControl
    {
        private KhoParams khoparams = null;
        private MapviewHelper MH = new MapviewHelper();

        bool loaded = false;

        public FormMapEdit()
        {
            InitializeComponent();
        }

        public FormMapEdit(KhoParams kparams)
        {
            InitializeComponent();
            khoparams = kparams;
            MyInit();
        }

        private void MyInit()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Size = panel1.Size;
            //Gán hết các setting cho MH!!!
            MH._groupType = "KH";
            MH._groupTableName = "Alkho";
            MH._groupTableName_Ct = "Alkhoct1";
            MH._groupIDfieldName = "Ma_kho";
            MH._groupKeyFieldName = "Ma_kho";
            MH._groupSelectString = "Select ma_kho, ten_kho, ten_kho2 ";
            MH._procLoadItemsColor = "VPA_DETAIL_MA_HINH2MA_VITRI";


            //if (m_XmlConfig.m_ReportInfo.Variables.ContainsKey("DateFrom"))
            {
                DateTime datetime = V6BusinessHelper.GetServerDateTime();
                MH.DateFrom = datetime.Date;
            }

            MH.LoadColorList();
            LoadGroupList();
            //MH.LoadPolygonDicColor();

            loaded = true;

            if (V6Setting.IsVietnamese)
            {
                rbtTiengViet.Checked = true;
            }
            else
            {
                rbtEnglish.Checked = true;
            }
        }


        #region ==== Image ====
        private void ChangePictureBoxImage(Image image)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = image;
        }
        private void ChooseImage()
        {
            this.ShowMessage("KhongDung");
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //UpdateImage(openFileDialog1.FileName);
            }
        }
        private void ChooseImageNone()
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UpdateImageNone(openFileDialog1.FileName);
            }
        }
        private void UpdateImage(string imgFile)
        {
            ChangePictureBoxImage(Picture.LoadCopyImage(imgFile));

            string ext = Path.GetExtension(imgFile);
            string dir = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string newFileName = "Images\\" + MH._groupID + ext;
            string newFileNameFull = dir + "\\" + MH._groupID + ext;
            if(imgFile.ToLower() != newFileNameFull.ToLower())
                File.Copy(imgFile, newFileNameFull, true);
            //Update du lieu combobox
            int length = MH.tableGroupList.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                DataRow row = MH.tableGroupList.Rows[i];
                if (row[0].ToString().Trim() == MH._groupID)
                {
                    row["FILE_ HINH"] = newFileName;
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
                if (row["ID"].ToString().Trim().ToUpper() == "A1")//m_XmlConfig.m_ReportInfo.DefaultGroup.ToUpper())
                {
                    comboBoxGroup.SelectedValue = row["ID"];

                    LoadItemsDataForSelectedGroup(row["ID"].ToString().Trim());
                    return;
                }
            }
            LoadItemsDataForSelectedGroup(comboBoxGroup.SelectedValue.ToString().Trim());
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
            MH.LoadItemData(groupID, "");
            MH.GetPolygonInfosDictionary();
            MH.LoadPolygonDicColor();

            MH.hoverPolygonID = null;
            MH.selectedPolygonID = null;
            MH.editPolygon = null;
            MH.newPolygon = new List<Point>();

            dataGridView1.DataSource = MH.itemData;
        }

        
        void SaveData()
        {
            V6Tools.V6Export.Data_Table.ToTextFile(MH.itemData, "data.txt");
        }
        
        private void MouseMoveOnPictureBox(Point mouse)
        {
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
            else if(MH.editMode == EditMode.CopyFrom)
            {
                /**
                 * Quy trình copy form
                 * Khi chuột di chuyển, nếu đã có một PClone
                 **/
                if (MH.editPolygon != null)
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
            }
            pictureBox1.Invalidate();
        }

        public void MouseDownOnPictureBox(Point mouse)
        {
            MH.downLocation = mouse;
            if (MH.editMode == EditMode.No)
            {
                if (!string.IsNullOrEmpty(MH.hoverPolygonID))
                    Select(MH.hoverPolygonID);
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
            else if (MH.editMode == EditMode.CopyFrom)
            {
                if (MH.editPolygon != null)
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
                    else if (!string.IsNullOrEmpty(MH.hoverPolygonID))
                    {
                        MH.editPolygon = HDrawing.ClonePolygon(MH.HoverPolygon);
                        MH.selectedPolygonClone = HDrawing.ClonePolygon(MH.editPolygon);
                        MH.moveMode = MoveMode.EditPolygon;
                    }
                    else
                    {
                        MH.moveMode = MoveMode.None;
                    }
                    
                }
                else
                {
                    if (!string.IsNullOrEmpty(MH.hoverPolygonID))
                    {
                        MH.editPolygon = HDrawing.ClonePolygon(MH.HoverPolygon);
                        MH.selectedPolygonClone = HDrawing.ClonePolygon(MH.editPolygon);
                        MH.moveMode = MoveMode.EditPolygon;
                    }
                    else
                    {
                        MH.moveMode = MoveMode.None;
                    }
                }
            }
        }
        private void Select(string id)
        {
            MH.selectedPolygonID = id;
            int rowCount = dataGridView1.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                string rowID =dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                if (rowID == id)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                }
            }
        }

        //================================= 90>_
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(radNo.Checked)
            {
                
            }
            else if(radReplaceNew.Checked)
            {
                
            }
            else if(radEditMove.Checked)
            {

            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!panel1.Focused) panel1.Focus();
            MouseDownOnPictureBox(e.Location);
            pictureBox1.Invalidate();
        }

       
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MH.moveMode = MoveMode.None;
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
        }

        
        private void radNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radNo.Checked)
            {
                MH.editMode = EditMode.No;
                pictureBox1.Invalidate();
            }
        }
        
        private void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (radReplaceNew.Checked && dataGridView1.SelectedRows.Count>0)
            {
                MH.editMode = EditMode.EditReplace;
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                string strPoints = dataGridView1.Rows[rowIndex].Cells["TOA_DO"].Value.ToString().Trim();
                MH.editPolygon = MH.GetPolygon(strPoints);

                MH.newPolygon = new List<Point>();
            }
            else if(radReplaceNew.Checked)
            {
                radNo.Checked = true;
                MessageBox.Show("Chưa chọn");
            }
            
            pictureBox1.Invalidate();
        }

        private void radEditMove_CheckedChanged(object sender, EventArgs e)
        {
            if (radEditMove.Checked && dataGridView1.SelectedRows.Count > 0)
            {
                MH.editMode = EditMode.EditMove;
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                string strPoints = dataGridView1.Rows[rowIndex].Cells["TOA_DO"].Value.ToString();
                MH.editPolygon = MH.GetPolygon(strPoints);

                MH.newPolygon = new List<Point>();
            }
            else if (radEditMove.Checked)
            {
                radNo.Checked = true;
                MessageBox.Show("Chưa chọn");
            }

            pictureBox1.Invalidate();
        }

        private void radCopyFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (radCopyFrom.Checked && dataGridView1.SelectedRows.Count > 0)
            {
                MH.editMode = EditMode.CopyFrom;

                MH.editPolygon = null;

                MH.newPolygon = new List<Point>();
            }
            else if (radEditMove.Checked)
            {
                radNo.Checked = true;
                MessageBox.Show("Chưa chọn");
            }

            pictureBox1.Invalidate();
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
                    radNo.Checked = true;
                    pictureBox1.Cursor = Cursors.Default;
                }
                else if (keyData == Keys.F2)
                {
                    radEditMove.Checked = true;
                }
                else if (radReplaceNew.Checked)
                {
                    if (keyData == Keys.Enter)
                    {
                        string id = MH.selectedPolygonID;
                        Point[] ps = MH.newPolygon.ToArray();
                        if (ps.Length > 2)
                        {
                            MH.regionDic[id].Polygon = ps;
                            MH.UpdateData(id, ps);
                        }
                        radNo.Checked = true;
                    }
                    //EditSelectedRow();
                }
                else if (radEditMove.Checked)
                {
                    if (keyData == Keys.Enter)
                    {
                        string id = MH.selectedPolygonID;
                        Point[] ps = HDrawing.ClonePolygon(MH.editPolygon);
                        MH.regionDic[id].Polygon = ps;
                        MH.UpdateData(id, ps);
                        radNo.Checked = true;
                    }
                    else if (keyData == Keys.Delete)
                    {
                        if (MH.editPointIndex >= 0)
                        {
                            MH.DeletePolygonPoint(ref MH.editPolygon, MH.editPointIndex);
                        }
                    }
                    else if (keyData == Keys.Add || keyData == Keys.Oemplus)
                    {
                        if (MH.editPointIndex >= 0)
                        {
                            MH.AddPolygonPoint(ref MH.editPolygon, MH.editPointIndex);
                        }
                    }
                }
                else if (radCopyFrom.Checked)
                {
                    if (keyData == Keys.Enter)
                    {
                        string id = MH.selectedPolygonID;
                        Point[] ps = HDrawing.ClonePolygon(MH.editPolygon);
                        MH.regionDic[id].Polygon = ps;
                        MH.UpdateData(id, ps);
                        //radNo.Checked = true;
                    }
                    else if (keyData == Keys.Delete)
                    {
                        if (MH.editPointIndex >= 0)
                        {
                            MH.DeletePolygonPoint(ref MH.editPolygon, MH.editPointIndex);
                        }
                    }
                    else if (keyData == Keys.Add || keyData == Keys.Oemplus)//+
                    {
                        if (MH.editPointIndex >= 0)
                        {
                            MH.AddPolygonPoint(ref MH.editPolygon, MH.editPointIndex);
                        }
                    }
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey0", ex);
            }
            return base.DoHotKey0(keyData);
        }
        
        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                MH.selectedPolygonID = dataGridView1.SelectedRows[0]
                    .Cells[0].Value.ToString().Trim();


                pictureBox1.Invalidate();
            }
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grID = comboBoxGroup.SelectedValue.ToString().Trim();
            if(loaded)
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

        private void buttonChooseImage_Click(object sender, EventArgs e)
        {
            if (chkNone.Checked)
            {
                ChooseImageNone();
            }
            else
            {
                ChooseImage();
            }
        }

        private void btnRightSize_Click(object sender, EventArgs e)
        {
            if (btnRightSize.Text == "<<")
            {
                //Mở rộng bên phải.
                splitContainer1.SplitterDistance = splitContainer1.Width / 2;
                btnRightSize.Text = ">>";
            }
            else
            {
                splitContainer1.SplitterDistance = splitContainer1.Width - 100;
                btnRightSize.Text = "<<";
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
            //MainForm.myMessage.messageLang = MainForm.CurrentLang;
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }
    }
    //end class
}
