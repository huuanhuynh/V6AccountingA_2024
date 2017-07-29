using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.KhoHangManager
{
    /// <summary>
    /// Dãy hàng
    /// </summary>
    public class DayHangControlDraw
    {
        private Point _p;
        private SortedList<string, KeHangControlDraw> _listKeHang;
        public KhoParams KhoParams { get; set; }
        public string ID { get; set; }
        public Point Location { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }

        public string MA_KHO = null;
        public string TYPE = null;

        public DayHangControlDraw()
        {
            //InitializeComponent();
            _p = new Point(v6VeticalLable1_Right, v6VeticalLable1_Top);
            _listKeHang = new SortedList<string, KeHangControlDraw>();
        }

        public DayHangControlDraw(KhoParams kparas, DataRow row)
        {
            //InitializeComponent();
            KhoParams = kparas;
            _p = new Point(v6VeticalLable1_Right, v6VeticalLable1_Top);
            _listKeHang = new SortedList<string, KeHangControlDraw>();
            ID = row["CODE"].ToString().Substring(0, 1);
            MA_KHO = row["MA_KHO"].ToString().Trim();
            TYPE = row["TYPE"].ToString().Trim();
            v6VeticalLable1_HideText = ID;
            v6VeticalLable1_ShowText = ID;
            AddRow(row);
        }


        public void AddRow(DataRow row)
        {
            try
            {
                var ID_KE = row["CODE"].ToString().Substring(0, 2);
                if (_listKeHang.ContainsKey(ID_KE))
                {
                    var day = _listKeHang[ID_KE];
                    day.AddRow(row);
                }
                else
                {
                    AddKeHang(row);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddRow", ex);
            }
        }

        private void AddKeHang(DataRow row)
        {
            try
            {
                KeHangControlDraw keHang = new KeHangControlDraw(KhoParams, row);
                _listKeHang.Add(keHang.ID, keHang);
                keHang_Location = new Point(_p.X, _p.Y);
                _p = new Point(_p.X, _p.Y + keHang.Height);
                //Controls.Add(keHang);
                Resort(keHang.Height-4);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddKeHang", ex);
            }
        }

        private void Resort(int oneHeight)
        {
            oneHeight += 0;
            try
            {
                var maxTop = _listKeHang.Count*oneHeight - oneHeight;
                for (int i = 0; i < _listKeHang.Count; i++)
                {
                    var keHang = _listKeHang.Values[i];
                    keHang.Top = maxTop - i*oneHeight;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".Resort", ex);
            }
        }

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
                f.ShowToForm(GetType() + "_F7");
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(GetType() + ".DoPrint: " + ex.Message);
            }
        }

        private void v6VeticalLable1_Click(object sender, EventArgs e)
        {
            try
            {
                KhoHangContainer container = null;
                //V6ControlFormHelper.FindParent<KhoHangContainer>(this) as KhoHangContainer;
                if (container == null)
                {
                    V6ControlFormHelper.ShowWarningMessage(V6Text.NotRunHere);
                    return;
                }

                if (KhoParams.Program == "AINVITRI03")
                {
                    // Sửa nhiều dòng trong dãy"
                    var condition = string.Format("MA_KHO='{0}' and MA_VITRI like '__{1}%' and MA_VT ='{2}'", MA_KHO.Replace("'", "''"),
                    ID.Replace("'", "''"), container._mavt.Replace("'", "''"));

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@nam", container._cuoiNgay.Year),
                        new SqlParameter("@thang", container._cuoiNgay.Month),
                        new SqlParameter("@Ma_kh", ""),
                        new SqlParameter("@Advance", condition),
                    };

                    // Data danh cho F7
                    plistData = new SortedDictionary<string, object>();
                    plistData["MA_KHO"] = MA_KHO;
                    plistData["MA_VITRI"] = ID;
                    plistData["MA_VT"] = container._mavt;
                    plistData["NAM"] = container._cuoiNgay.Year;
                    plistData["THANG"] = container._cuoiNgay.Month;
                    plistData["MA_KH"] = "";
                    
                    var data = V6BusinessHelper.ExecuteProcedure(KhoParams.Program + "A", plist).Tables[0];
                    string keys = "UID";
                    var f = V6ControlFormHelper.MakeDataEditorForm(data, V6TableName.Abnghi.ToString(), null, keys, false, false);
                    f.HotKeyAction += keyData =>
                    {
                        if (keyData == Keys.F7)
                        {
                            DoPrint();
                        }
                        else if (keyData == Keys.Delete)
                        {
                            V6ControlFormHelper.ShowInfoMessage("Not allowed!");
                        }
                    };
                    f.ShowDialog();
                }
                else
                {
                    SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                    plistData["FLAG"] = "DAY";
                    plistData["MA_KHO"] = MA_KHO;
                    //plistData["MA_VT"] = MA_VT;
                    plistData["MA_VITRI"] = "__" + ID;
                    plistData["CUOI_NGAY"] = container._cuoiNgay;
                    plistData["VT_TONKHO"] = "*";
                    plistData["KIEU_IN"] = "*";

                    ReportRViewBase c = new ReportRViewBase(KhoParams.ItemId, KhoParams.Program, KhoParams.Program + "A",
                        KhoParams.ReportFile,
                        "reportCaption", "caption2", "", "", "");
                    c.FilterControl.SetData(plistData);
                    c.btnNhan_Click(null, null);
                    c.ShowToForm("DayHang");

                    var cSelectedRow = c.SelectedRowData;
                    if (cSelectedRow != null)
                    {
                        var MA_VT = cSelectedRow["MA_VT"].ToString().Trim();
                        container.SetColorMavt(MA_VT, Color.Blue);
                    }
                    else
                    {
                        container.SetColorMavt(null, Color.Blue);
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".v6VeticalLable1_Click", ex);
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

        
    }
}
