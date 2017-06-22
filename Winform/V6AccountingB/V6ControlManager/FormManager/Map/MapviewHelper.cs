using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using H;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;

namespace V6ControlManager.FormManager.Map
{
    public class MapviewHelper
    {
        public string _groupID = "";
        public string _procLoadItemsColor = "";
        public Dictionary<string, Color> colorListByMaVV = new Dictionary<string, Color>();
        public Dictionary<string, MapRegionObject> regionDic = new Dictionary<string, MapRegionObject>();

        public DataTable itemData;
        public DataTable itemColorData;
        public DataTable tableGroupList;
        public DataView viewGroupList;

        public EditMode editMode = EditMode.No;
        public MoveMode moveMode = MoveMode.None;
        public string _groupType = "";

        public int editPointIndex = -1;
        public Point mouseLocation = new Point(0, 0);
        public Point downLocation = new Point(0, 0);
        public Point[] selectedPolygonClone;//Dùng làm gốc để di chuyển oldPolygon
        public Point[] editPolygon;//for edit, clone from selectedPolygon
        public List<Point> newPolygon;//Dùng khi vẽ cái mới.

        public string hoverPolygonID { get; set; }

        public string selectedPolygonID { get; set; }
        public MapRegionObject SelectedPolygon
        {
            get
            {
                if(!string.IsNullOrEmpty(selectedPolygonID))
                {
                    return regionDic[selectedPolygonID];
                }
                else
                {
                    return null;
                }
            }
        }

        public MapviewHelper()
        {
            
        }


        public void AddPolygonPoint(ref Point[] ps, int index)
        {
            if (ps.Length > 20) return;

            List<Point> lp = new List<Point>();
            foreach (Point p in ps)
            {
                lp.Add(p);
            }

            Point newPoint = new Point(10, 10);
            int nextIndex = index + 1;

            if (ps.Length == 0)
            {

            }
            else if (ps.Length == 1)
            {
                newPoint.X = ps[0].X + 10;
                newPoint.Y = ps[0].Y + 10;
            }
            else
            {
                if (nextIndex == ps.Length) nextIndex = 0;
                newPoint.X = (ps[index].X + ps[nextIndex].X) / 2;
                newPoint.Y = (ps[index].Y + ps[nextIndex].Y) / 2;
            }

            if (nextIndex == 0) lp.Add(newPoint);
            else lp.Insert(nextIndex, newPoint);

            ps = lp.ToArray();
        }

        public void DeletePolygonPoint(ref Point[] ps, int index)
        {
            if (ps.Length>3 && ps.Length > index && index >= 0)
            {
                List<Point> lp = new List<Point>();
                foreach (Point p in ps)
                {
                    lp.Add(p);
                }

                lp.RemoveAt(index);
                ps = lp.ToArray();
            }
        }

        public void DrawAllPolygon(Graphics gp)
        {
            //Pen penColor = new Pen(Color.Cyan, 2);
            foreach (var item in regionDic)
            {
                Point[] ps = item.Value.Polygon;
                FillPolygon(gp, item.Value);
                DrawPolygon(gp, item.Value);
                //HDrawing.DrawPolygon(gp, ps, GetColor(item.Value.ColorType, Color.Cyan), 2);
            }
            //Fill the hover polygon
            if (!string.IsNullOrEmpty(hoverPolygonID))
            {
                FillHoverPolygon(gp, regionDic[hoverPolygonID]);
            }
            if (!string.IsNullOrEmpty(selectedPolygonID))
            {
                FillSelectedPolygon(gp, regionDic[selectedPolygonID]);
                DrawSelectedPolygon(gp, regionDic[selectedPolygonID]);
            }
        }

        public void DrawNewPolygon(Graphics gp, Point mouseP)
        {
            Pen p = new Pen(Color.Red, 3);
            Point[] ps = newPolygon.ToArray();
            int pCount = ps.Length;

            if (pCount > 1)
            {
                for (int i = 1; i < pCount; i++)
                {
                    HDrawing.DrawLine(gp, ps[i], ps[i - 1], p);
                }
            }
            //Draw last poit with mouse
            if (pCount > 0)
                HDrawing.DrawLine(gp, ps[pCount - 1], mouseP, p);
        }

        public void DrawOldPolygon(Graphics gp)
        {
            HDrawing.DrawPolygon(gp, editPolygon, Color.Yellow, 1);
        }

        public void DrawOldPolygonMovePointStyle(Graphics gp)
        {
            Pen linePen = new Pen(Color.Yellow);
            Pen pointPen = new Pen(Color.Red, 2);
            //if(oldPoints!=null)
            Point[] ps = editPolygon;
            int pCount = ps.Length;

            //Draw lines
            if (pCount > 2)
            {
                for (int i = 1; i < pCount; i++)
                {
                    HDrawing.DrawLine(gp, ps[i], ps[i - 1], linePen);
                    gp.DrawRectangle(pointPen, ps[i].X - 1, ps[i].Y - 1, 3, 3);
                }
                //Draw last
                HDrawing.DrawLine(gp, ps[0], ps[pCount - 1], linePen);
            }

            //Draw points
            if (pCount > 2)
            {
                for (int i = 0; i < pCount; i++)
                {
                    gp.DrawRectangle(pointPen, ps[i].X - 2, ps[i].Y - 2, 5, 5);
                }
            }

            //Draw selected point
            if (editPointIndex >= 0)
            {
                pointPen = new Pen(Color.Blue, 2);
                gp.DrawRectangle(pointPen, ps[editPointIndex].X - 2, ps[editPointIndex].Y - 2, 5, 5);
            }
        }

        public void DrawOnPictureBoxPaint(Graphics gp)
        {
            if (editMode == EditMode.No)
            {
                DrawAllPolygon(gp);
            }
            else if (editMode == EditMode.EditReplace)
            {
                DrawAllPolygon(gp);
                DrawOldPolygon(gp);
                DrawNewPolygon(gp, mouseLocation);
            }
            else if (editMode == EditMode.EditMove)
            {
                DrawAllPolygon(gp);
                DrawOldPolygonMovePointStyle(gp);
            }
            else if(editMode == EditMode.CopyFrom)
            {
                DrawAllPolygon(gp);
                if(this.editPolygon != null)
                    DrawOldPolygonMovePointStyle(gp);
            }
        }

        public void DrawPolygon(Graphics gp, MapRegionObject region)
        {
            HDrawing.DrawPolygon(gp, region.Polygon, GetColor(region.ColorType, Color.Cyan), 2);
        }

        public void DrawSelectedPolygon(Graphics gp)
        {
            HDrawing.DrawPolygon(gp, regionDic[selectedPolygonID].Polygon, Color.Blue, 5);
        }
        public void DrawSelectedPolygon(Graphics gp, MapRegionObject region)
        {
            HDrawing.DrawPolygon(gp, region.Polygon, Color.Blue, 5);
        }

        public void FillPolygon(Graphics gp, MapRegionObject region)
        {
            HDrawing.FillPolygonTransparent(gp, region.Polygon, GetColor(region.ColorType, Color.Yellow));
        }

        public void FillHoverPolygon(Graphics gp, MapRegionObject region)
        {
            HDrawing.FillPolygonTransparent(gp, region.Polygon, Color.White);
        }

        public void FillSelectedPolygon(Graphics gp, MapRegionObject region)
        {
            HDrawing.FillPolygonTransparent(gp, region.Polygon, Color.Blue);
        }

        public Color GetColor(string ma_vv, Color color_if_noexist)
        {
            if (colorListByMaVV.ContainsKey(ma_vv))
                return colorListByMaVV[ma_vv];
            return color_if_noexist;
        }

        internal PartImage GetCurrentPartImage(Image image, Size newSize)
        {
            if (!string.IsNullOrEmpty(selectedPolygonID))
            {
                return GetPartImage(image, selectedPolygonID, newSize);
            }
            else return null;
        }

        public PartImage GetPartImage(Image image, string polygonID, Size newSize)
        {
            if (regionDic.ContainsKey(polygonID))
            {
                return GetPartImage(image, regionDic[polygonID], newSize);
            }
            else return null;
        }
        public PartImage GetPartImage(Image image, MapRegionObject region, Size newSize)
        {
            Point[] polygon = region.Polygon;
            PartImage partImage = new PartImage();
            //Tính toán tọa độ rect bao bọc region.
            int
                xMin = polygon[0].X,
                yMin = polygon[0].Y,
                xMax = polygon[0].X,
                yMax = polygon[0].Y;
            foreach (Point p in polygon)
            {
                if (p.X < xMin) xMin = p.X;
                if (p.X > xMax) xMax = p.X;
                if (p.Y < yMin) yMin = p.Y;
                if (p.Y > yMax) yMax = p.Y;
            }
            int x = xMin, y = yMin;
            int w = xMax - xMin, h = yMax - yMin;
            //Kiem tra ty le.
            //w=>ch
            int ch = w * newSize.Height / newSize.Width;
            int cw = h * newSize.Width / newSize.Height;
            //Neu ch>h lay ch
            //Nguoc lai lay cw
            if(ch>h)
            {
                y -= ((ch - h) / 2); if (y < 0) y = 0;
                h = ch;
            }
            else
            {
                //can lai ngay giua
                x -= ((cw - w) / 2); if (x < 0) x = 0;
                w = cw;
            }
            while (x + w > image.Width)
            {
                w--;
            }
            while (y + h > image.Height)
            {
                h--;
            }
            //Tinh lai newsize width
            newSize.Width = newSize.Height * w / h;
            
            Rectangle r = new Rectangle(x, y, w, h);
            Bitmap bImage = new Bitmap(image);
            
            partImage.PartRect = r;
            Image cloneImage = (Image)bImage.Clone(r, bImage.PixelFormat);
            Image resizeImage = HDrawing.resizeImage(cloneImage, newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(resizeImage);
            
            //Dichuyen toa do
            Point[] newPolygon = new Point[polygon.Length];
            for (int i = 0; i < polygon.Length; i++)
            {
                newPolygon[i].X = (polygon[i].X - x) * newSize.Width / w;
                newPolygon[i].Y = (polygon[i].Y - y) * newSize.Height / h;
            }

            //FillPolygon(g, newPolygon);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            HDrawing.FillPolygonTransparent(g, newPolygon, GetColor(region.ColorType, Color.Yellow));
            HDrawing.DrawPolygon(g, newPolygon, GetColor(region.ColorType, Color.Cyan), 4);
            partImage.Part_Image = resizeImage;
            g.Dispose();
            return partImage;
        }

        public string GetHoverPolygonID(Point p)
        {
            if (regionDic != null)
                foreach (var item in regionDic)
                {
                    if (HDrawing.IsInPolygon1(item.Value.Polygon, p))
                    {
                        hoverPolygonID = item.Key;
                        return hoverPolygonID;
                    }
                }
            hoverPolygonID = null;
            return null;
        }

        public Point[] GetPolygon(string strPoints)
        {
            string[] strPointArr = strPoints.Split(';');
            List<Point> ps = new List<Point>();

            foreach (string s in strPointArr)
            {
                
                Point p = GetPoint(s);
                ps.Add(p);
            }
            return ps.ToArray();
        }

        public Point GetPoint(string s)
        {
            s = s.Trim();
            if (s == "") return new Point(0, 0);

            string[] ss = s.Split(',');
            return new Point(int.Parse(ss[0]), int.Parse(ss[1]));
        }

        /// <summary>
        /// Lấy những thông tin cần thiết cho và dic
        /// tạo luôn thư mục
        /// </summary>
        public void GetPolygonInfosDictionary()
        {
            regionDic = new Dictionary<string, MapRegionObject>();
            try
            {
                foreach (DataRow row in itemData.Rows)
                {
                    string id = row["MA_HINH"].ToString().Trim();
                    string pstr = row["TOA_DO"].ToString().Trim();
                    string document_path = row["Path"].ToString().Trim();
                    Point[] polygon = GetPolygon(pstr);


                    
                    MapRegionObject newRegion = new MapRegionObject();
                    newRegion.ID = id;
                    newRegion.Polygon = polygon;
                    newRegion.RowData = row;
                    newRegion.Path = document_path;

                    regionDic.Add(newRegion.ID, newRegion);
                    //AddPolygonDic(newRegion);
                    //Tạo thư mục riêng
                    try
                    {
                        if (!string.IsNullOrEmpty(newRegion.Path) && !Directory.Exists(newRegion.Path))
                        {
                            Directory.CreateDirectory(newRegion.Path);
                        }
                    }
                    catch
                    {
                        
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPolygonDic " + ex.Message);
            }
        }
        

        public string GetString(Point[] ps)
        {
            string strPoints = "";
            foreach (Point p in ps)
            {
                strPoints += ";" + p.X + "," + p.Y;
            }
            if (strPoints.Length > 1) strPoints = strPoints.Substring(1);
            return strPoints;
        }

        public string _groupTableName = "Alkho";
        public string _groupTableName_Ct = "Alkhoct1";
        public string _groupIDfieldName = "ma_kho";
        public string _groupSelectString = "";
        public string _groupKeyFieldName = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="where">field=value [and ...]</param>
        public void LoadItemData(string groupID, string where)
        {
            groupID = groupID.Replace("'", "''");
            string sql = "Select * from ALHINH tb1 ";

            if (!string.IsNullOrEmpty(where))
            {
                sql += " Where tb1." + _groupIDfieldName + " ='" + groupID + "'" + " and  LoaiHinh='"+_groupType+"' and " + where;
            }
            else
            {
                sql += " Where tb1." + _groupIDfieldName + " = '" + groupID + "' and LoaiHinh='"+_groupType+"'";
            }
            DataTable data = V6BusinessHelper.ExecuteSqlDataset(sql).Tables[0];
            itemData = data;
        }

        internal DataTable LoadItemProcData(string id, DateTime fromDate, DateTime toDate, string convertFrom, string to)
        {
            DataRow rowData = regionDic[id].RowData;
            string procName = rowData["Proce"].ToString().Trim();
            string key1 = rowData["Key1"].ToString().Trim();
            string key2 = rowData["Key2"].ToString().Trim();
            string key3 = rowData["Key3"].ToString().Trim();
            DataTable result = V6BusinessHelper.ExecuteProcedure(procName, key1, key2, key3, fromDate, toDate).Tables[0];

            if (!string.IsNullOrEmpty(convertFrom) && !string.IsNullOrEmpty(to))
                V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, convertFrom, to);
            return result;
        }

        private DataTable LoadItemsDataColor(string groupID)
        {
            DateTime serverDate = GetServerDate();
            
            try
            {
                groupID = groupID.Replace("'", "''");
                //exec [VPA_EdItems_MA_HINH2MA_VT] 'ma_kho = ''C00''', '', '', '20150207'
                DataTable dataColor = V6BusinessHelper.ExecuteProcedure(_procLoadItemsColor,
                    _groupKeyFieldName + " = '" + groupID + "'",
                    "",
                    "",
                    this.DateFrom, serverDate
                    )
                    .Tables[0];


                //DataTable dataColor = LoadItemsDataColor();
                itemColorData = dataColor;
                return dataColor;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType + ".LoadItemsDataColor", ex, V6ControlFormHelper.LastActionListString);
                itemColorData = new DataTable();
                return itemColorData;
            }
        }

        public DateTime GetServerDate()
        {
            string sql = "Select GetDate()";
            DataTable tbl = V6BusinessHelper.ExecuteSqlDataset(sql).Tables[0];
            DateTime res = (DateTime)tbl.Rows[0][0];
            return res;
        }

        /// <summary>
        /// Load hình lên form theo group id 
        /// Load list items
        /// Get poligon
        /// LoadPolygonDicColor
        /// </summary>
        /// <param name="groupID"></param>
        public DataTable LoadItemsDataForSelectedGroup(string groupID)
        {
            _groupID = groupID;
            //Lấy ảnh
            int rowcount = tableGroupList.Rows.Count;

            LoadItemData(groupID, "");
            GetPolygonInfosDictionary();
            LoadPolygonDicColor();//.LoadItemsDataColor(MH._groupID);

            hoverPolygonID = null;
            selectedPolygonID = null;
            editPolygon = null;
            newPolygon = new List<Point>();

            return itemData;
        }

        /// <summary>
        /// Vì color lấy lên bằng Procedure riêng nên phải viết riêng.
        /// </summary>
        public void LoadPolygonDicColor()
        {
            LoadItemsDataColor(_groupID);
            //makho,margb,mahinh,   mavt        dvt ten
            //C00  	01	HAOTHUNG   	HAOTHUNG    CAI	¸o thun Galaxy ®ång phôc    DP  398.000000	18410000.00	0.00
            int rowcount = itemColorData.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                DataRow row = itemColorData.Rows[i];

                string id = row["MA_HINH"].ToString().Trim();
                string ma_mau = row["MA_RGB"].ToString().Trim();
                if (ma_mau!="" && regionDic.ContainsKey(id))
                {
                    regionDic[id].ColorType = ma_mau;
                }
            }
        }

        /// <summary>
        /// Trả về bản gồm groupID và groupName
        /// </summary>
        /// <returns></returns>
        public void LoadGroupList(string where, string convertForm="", string to="")
        {
            string sql = _groupSelectString;
            sql += " From [" + _groupTableName + "]";

            
            
            if (!string.IsNullOrEmpty(where))
                sql += " Where " + where;

            DataTable result = V6BusinessHelper.ExecuteSqlDataset(sql).Tables[0];
            if (!string.IsNullOrEmpty(convertForm) && !string.IsNullOrEmpty(to))
                V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, convertForm, to);

            result.Columns[0].ColumnName = "ID";
            result.Columns[1].ColumnName = "NAME";
            result.Columns[2].ColumnName = "NAME2";
            
            tableGroupList = result;
            //return result;
        }

        public void LoadColorList()
        {
            try
            {
                //Dictionary<string, Color> result = new Dictionary<string, Color>();
                colorListByMaVV = new Dictionary<string, Color>();
                string sql = "Select ma_rgb, R, G, B from almaurgb";
                DataTable data = V6BusinessHelper.ExecuteSqlDataset(sql)
                    .Tables[0];
                int rowcount = data.Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    DataRow row = data.Rows[i];
                    string id = row[0].ToString().Trim();
                    int r = (int)(decimal)row["R"];
                    int g = (int)(decimal)row["G"];
                    int b = (int)(decimal)row["B"];
                    Color c = Color.FromArgb(r, g, b);
                    colorListByMaVV.Add(id, c);
                }
                
                //return result;
            }
            catch (Exception ex)
            {
                throw new Exception("LoadColor " + ex.Message);
            }
        }

        public void MouseDownOnPictureBox(Point mouse)
        {
            downLocation = mouse;
            if (editMode == EditMode.No)
            {
                if (!string.IsNullOrEmpty(hoverPolygonID))
                    selectedPolygonID = hoverPolygonID;// Select(MH.hoverPolygonID);
                else
                {
                    selectedPolygonID = null;
                }
            }
            else if (editMode == EditMode.EditReplace)
            {
                newPolygon.Add(mouse);
            }
            else if (editMode == EditMode.EditMove)
            {
                editPointIndex = HDrawing.IsNearPolygonPoint(editPolygon, mouseLocation);
                if (editPointIndex >= 0)
                {
                    moveMode = MoveMode.NearPoint;
                }
                else if (HDrawing.IsInPolygon1(editPolygon, mouseLocation))
                {
                    moveMode = MoveMode.EditPolygon;
                    selectedPolygonClone = HDrawing.ClonePolygon(editPolygon);
                }
                else
                {
                    moveMode = MoveMode.None;
                }
            }
            else if (editMode == EditMode.CopyFrom)
            {
                if (editPolygon != null)
                {
                    editPointIndex = HDrawing.IsNearPolygonPoint(editPolygon, mouseLocation);
                    if (editPointIndex >= 0)
                    {
                        moveMode = MoveMode.NearPoint;
                    }
                    else if (HDrawing.IsInPolygon1(editPolygon, mouseLocation))
                    {
                        moveMode = MoveMode.EditPolygon;
                        selectedPolygonClone = HDrawing.ClonePolygon(editPolygon);
                    }
                    else
                    {
                        moveMode = MoveMode.None;
                    }
                }
            }
        }

        public void MoveOldPolygon(ref Point[] points, Point[] oldLocation, Point oldMouse, Point mouseP)
        {
            int extraX = mouseP.X - oldMouse.X;
            int extraY = mouseP.Y - oldMouse.Y;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = oldLocation[i].X + extraX;
                points[i].Y = oldLocation[i].Y + extraY;
            }
        }

        public void UpdateData(string id, Point[] ps)
        {
            string strPoints = GetString(ps);
            foreach (DataRow row in itemData.Rows)
            {
                if (row[0].ToString().Trim() == id)
                {
                    row["TOA_DO"] = strPoints;
                    UpdateRegion(id, strPoints);
                    break;
                }
            }
        }


        internal void UpdateRegion(string ma_hinh, string strPoints)
        {
            ma_hinh = ma_hinh.Replace("'", "''");
            strPoints = strPoints.Replace("'", "''");
            
            try
            {
                string sql = "Update ALHINH set TOA_DO = '"+strPoints+"' where MA_HINH = '"+ma_hinh+"'";
                V6BusinessHelper.ExecuteSqlNoneQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Update Region " + ex.Message);
            }
        }

        

        internal void UpdateImage(string groupID, string imgFile)
        {
            try
            {
                string sql = "Update ["+_groupTableName+"] set FILE_HINH = '"+imgFile+"' where "+_groupIDfieldName+" = '"+groupID+"'";
                V6BusinessHelper.ExecuteSqlNoneQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Update Image " + ex.Message);
            }
        }


        public Point[] HoverPolygon { get 
        {
            if(!string.IsNullOrEmpty (this.hoverPolygonID))
            {
                return this.regionDic[this.hoverPolygonID].Polygon;
            }
            return null;
        } }

        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Lấy dữ liệu trường PHOTOGRAPH trong bảng ct chuyển thành Image.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageData(string id)
        {
            SqlParameter[] plist = new []{new SqlParameter("@id", id), };
            var data = V6BusinessHelper.Select(_groupTableName_Ct, "*", string.Format("{0}=@id", _groupIDfieldName), "", "", plist).Data;
            if (data.Rows.Count == 1)
            {
                object imageData = data.Rows[0]["PHOTOGRAPH"];
                Image result = V6Tools.V6Convert.Picture.ToImage(imageData);
                return result;
            }
            return null;
        }
    }

    public class PartImage
    {
        public Rectangle PartRect;
        public Image Part_Image;
    }

    public enum EditMode
    {
        No, EditReplace, EditMove,
        CopyFrom
    }
    public enum MoveMode
    {
        None,NearPoint,EditPolygon
    }
    public enum GroupType
    {
        KHO,VV
    }
}
