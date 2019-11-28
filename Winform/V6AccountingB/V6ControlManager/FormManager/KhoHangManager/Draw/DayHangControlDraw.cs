using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using HaUtility.Helper;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.KhoHangManager.Draw
{
    /// <summary>
    /// Dãy hàng
    /// </summary>
    public class DayHangControlDraw
    {
        private Point _p0;
        private SortedList<string, KeHangControlDraw> _listKeHang = new SortedList<string, KeHangControlDraw>();
        private SortedDictionary<string, string> _columnsCode = new SortedDictionary<string, string>(); 
        public KhoParams KhoParams { get; set; }
        public DataRow DataRow { get; set; }
        /// <summary>
        /// Mã kho + Code dãy
        /// </summary>
        public string ID_DAY { get; set; }
        /// <summary>
        /// Vị trí vẽ bắt đầu của dãy hàng. BasePoint
        /// </summary>
        public Point Location { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }

        public string MA_KHO = null;
        public string TYPE = null;

        public DayHangControlDraw()
        {
            //InitializeComponent();
            //_p = new Point(v6VeticalLable1_Right, v6VeticalLable1_Top);
        }

        public DayHangControlDraw(string id_day, KhoParams kparas, DataRow row0)
        {
            KhoParams = kparas;
            //DataRow = row;
            //_p = new Point(v6VeticalLable1_Right, v6VeticalLable1_Top);
            ID_DAY = id_day;
            //var CODE_DAY = KhoHangHelper.GetCodeDay_FromCode(row["CODE"].ToString());
            //MA_KHO = row["MA_KHO"].ToString().Trim();
            //TYPE = row["TYPE"].ToString().Trim();
            //ID_DAY = MA_KHO + CODE_DAY;
            v6VeticalLable1_HideText = ID_DAY;
            v6VeticalLable1_ShowText = ID_DAY;
            //AddRow(row);
        }


        public KeHangControlDraw AddRow(DataRow row)
        {
            try
            {
                var CODE_KE = KhoHangHelper.GetCodeKe_FromCode(row["CODE"].ToString());
                KeHangControlDraw keHang = _listKeHang.ContainsKey(CODE_KE) ? _listKeHang[CODE_KE] : new KeHangControlDraw(CODE_KE, KhoParams);
                keHang.AddRow(row);
                if (!_listKeHang.ContainsKey(CODE_KE))
                {
                    _listKeHang[CODE_KE] = keHang;
                }
                return keHang;
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddRow", ex);
            }
            return null;
        }

        public void SetDataVitriVatTu(DataRow row, string cVitri, string cMavt)
        {
            try
            {
                var ma_ke = cVitri.Substring(2, 2);
                _listKeHang[ma_ke].SetDataVitriVatTu(row, cVitri, cMavt);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        //private void Resort(int oneHeight)
        //{
        //    oneHeight += 0;
        //    try
        //    {
        //        var maxTop = _listKeHang.Count*oneHeight - oneHeight;
        //        for (int i = 0; i < _listKeHang.Count; i++)
        //        {
        //            var keHang = _listKeHang.Values[i];
        //            keHang.Top = maxTop - i*oneHeight;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        V6ControlFormHelper.WriteExLog(GetType() + ".Resort", ex);
        //    }
        //}

        private SortedDictionary<string, object> plistData;
        private int v6VeticalLable1_Top;
        private int v6VeticalLable1_Right;
        private string v6VeticalLable1_ShowText;
        private string v6VeticalLable1_HideText;
        private Point keHang_Location;
        

        private void DoPrint()
        {
            try
            {
                var f = new ReportRViewBase("m_itemId",  KhoParams.Program + "AF7", KhoParams.Program + "AF7", KhoParams.ReportFile  + "AF7", "caption", "2", "", "", "");
                f.FilterControl.SetData(plistData);
                f.btnNhan_Click(null, null);
                f.ShowToForm(null, GetType() + "_F7");
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(GetType() + ".DoPrint: " + ex.Message);
            }
        }

        public void ClearDataVitriVaTu()
        {
            try
            {
                foreach (KeyValuePair<string, KeHangControlDraw> item in _listKeHang)
                {
                    item.Value.ClearDataVitriVaTu();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        

        public void SetColorMavt(string maVt, Color color)
        {
            try
            {
                foreach (KeyValuePair<string, KeHangControlDraw> item in _listKeHang)
                {
                    item.Value.SetColorMavt(maVt, color);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }


        public void DrawToGraphics(Graphics graphics, Point basePoint)
        {
            DrawBorder(graphics, basePoint);
        }

        /// <summary>
        /// Đường viền của dãy hàng. được tạo lúc khởi tạo
        /// </summary>
        Point[] borderPolygon =
            {
                new Point(1,1),
                new Point(100,1),
                new Point(100,100),
                new Point(1,100),
            };
        private void DrawBorder(Graphics graphics, Point basePoint)
        {
            Point[] drawPolygon =
            {
                new Point(basePoint.X + 1, basePoint.Y + 1),
                new Point(basePoint.X + 100, basePoint.Y + 1),
                new Point(basePoint.X + 100, basePoint.Y + 100),
                new Point(basePoint.X + 1, basePoint.Y + 100),
            };
            HDrawing.DrawPolygon(graphics, drawPolygon, Color.Blue, 5);
        }
    }
}
