using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.KhoHangManager.Report;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.KhoHangManager
{
    public partial class ViTriControl : V6Control
    {
        
        
        /// <summary>
        /// 1A1-14 của 1A1-14.01 (config 0,1;1,1;2,1;4,2)
        /// </summary>
        //public string maVitriShort { get { return Name; } set { Name = value; } }
        public List<ViTriDetail> _listVitriDetail = new List<ViTriDetail>();
        

        public event HandleData V6Click;
        protected virtual void OnV6Click(IDictionary<string, object> data)
        {
            var handler = V6Click;
            if (handler != null) handler(data);
        }

        public KhoParams KhoParams { get; set; }


        public ViTriControl()
        {
            InitializeComponent();
        }

        public ViTriControl(KhoParams kparas, DataRow row)
        {
            InitializeComponent();
            
            KhoParams = kparas;
            //_rowData = row;

            MyInit(row);
        }

        private void MyInit(DataRow _rowData)
        {
            try
            {
                if (KhoParams.CellWidth >= 40) Width = KhoParams.CellWidth;
                if (KhoParams.CellHeight >= 20) Height = KhoParams.CellHeight;
                
                ViTriDetail vitri_detail = new ViTriDetail(_rowData);
                label1.Text = vitri_detail.MA_VITRI_SHORT;
                //label2.Text = ""; // ban đầu.

                _listVitriDetail.Add(vitri_detail);

                var RGB = _rowData["MAU_RGB"].ToString().Trim();
                var rgb = ObjectAndString.SplitString(RGB);
                if (rgb.Length >= 3)
                {
                    int r = ObjectAndString.ObjectToInt(rgb[0]);
                    int g = ObjectAndString.ObjectToInt(rgb[1]);
                    int b = ObjectAndString.ObjectToInt(rgb[2]);
                    BackColor = Color.FromArgb(r, g, b);
                }

                MouseClick += ViTriButton_Click;
                label1.MouseClick += ViTriButton_Click;
                label2.MouseClick += ViTriButton_Click;

                MouseMove += ViTriControl_MouseMove;
                label1.MouseMove += ViTriControl_MouseMove;
                label2.MouseMove += ViTriControl_MouseMove;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        void ViTriControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                V6ControlsHelper.FlyLabel_Form.TargetControl = this;
                V6ControlsHelper.FlyLabel_Form.Message = label1.Text;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        void ViTriButton_Click(object sender, MouseEventArgs e)
        {
            try
            {
                KhoHangContainer container =
                        V6ControlFormHelper.FindParent<KhoHangContainer>(this) as KhoHangContainer;
                if (container == null)
                {
                    this.ShowWarningMessage(V6Text.NotRunHere, 300);
                    return;
                }

                if (_listVitriDetail.Count > 1 && (e.Button == MouseButtons.Right))
                {
                    // Xử lý nhiều;

                    foreach (ViTriDetail detail in _listVitriDetail)
                    {
                        //detail._cuoiNgay = container._cuoiNgay;
                    }

                    VitriMultiForm form = new VitriMultiForm(KhoParams);
                    form.container_cuoiNgay = container._cuoiNgay;
                    form.container_Report_GRDSV1 = container.Report_GRDSV1;
                    form.container_Report_GRDFV1 = container.Report_GRDFV1;
                    form.container_Report_GRDHV_V1 = container.Report_GRDHV_V1;
                    form.container_Report_GRDHE_V1 = container.Report_GRDHE_V1;

                    form.AddVitriList(_listVitriDetail);
                    form.ShowDialog(this);

                    var data0 = new Dictionary<string, object>();
                    data0.Add("LISTVITRIDETAIL", _listVitriDetail);
                    OnV6Click(data0);
                    return;
                }

                ViTriDetail vtd = _listVitriDetail[0];

                if (KhoParams.Program == "AINVITRI03")
                {
                    SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                    plistData["MA_KHO"] = vtd.MA_KHO;
                    plistData["MA_KH"] = container._mavt;//MA_KH
                    plistData["MA_VT"] = container._mavt;
                    plistData["MA_VITRI"] = vtd.MA_VITRI_SHORT;   // List<vitri>
                    plistData["CUOI_NGAY"] = container._cuoiNgay;
                    plistData["VT_TONKHO"] = "1";
                    plistData["KIEU_IN"] = "1";

                    plistData.AddRange(vtd._rowDataVitriVattu.ToDataDictionary());

                    AINVITRI03_REPORT c = new AINVITRI03_REPORT(KhoParams.ItemId, KhoParams.Program, "AINVITRI03B",
                        KhoParams.ReportFile, vtd.MA_VITRI_SHORT, vtd.MA_VITRI_SHORT);
                    c.SetData(plistData);
                    c.Size = new Size(800, 600);
                    c.btnNhan_Click(null, null);
                    c.ShowToForm(this, vtd.MA_VITRI_SHORT);
                }
                else if (vtd.TYPE == "0" || vtd.TYPE == "") // Hiển thị dữ liệu.
                {
                    var condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", vtd.MA_KHO.Replace("'", "''"), vtd.MA_VITRI_SHORT.Replace("'", "''"));
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@EndDate", container._cuoiNgay.ToString("yyyyMMdd")),
                        new SqlParameter("@Condition", condition),
                        new SqlParameter("@Vttonkho", "1"),
                        new SqlParameter("@Kieu_in", "1"),
                    };
                    var table = V6BusinessHelper.ExecuteProcedure(KhoParams.Program, plist).Tables[0];
                    V6ColorDataGridView gv = new V6ColorDataGridView();
                    gv.Control_S = true;
                    gv.ReadOnly = true;
                    gv.AllowUserToAddRows = false;
                    gv.AllowUserToDeleteRows = false;
                    gv.DataSource = table;
                    gv.ColumnAdded += delegate(object o, DataGridViewColumnEventArgs args)
                    {
                        V6ControlFormHelper.FormatGridViewAndHeader(gv, container.Report_GRDSV1, container.Report_GRDFV1,
                            V6Setting.IsVietnamese ? container.Report_GRDHV_V1 : container.Report_GRDHE_V1);
                    };
                    gv.ToFullForm("V6");
                }
                else if (vtd.TYPE == "1") // Hiển thị form báo cáo
                {
                    SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                    plistData["MA_KHO"] = vtd.MA_KHO;
                    plistData["MA_VT"] = vtd.MA_VT;
                    plistData["MA_VITRI"] = vtd.MA_VITRI_SHORT;   // List<vitri>
                    plistData["CUOI_NGAY"] = container._cuoiNgay;
                    plistData["VT_TONKHO"] = "1";
                    plistData["KIEU_IN"] = "1";

                    ReportRViewBase c = new ReportRViewBase(KhoParams.ItemId, KhoParams.Program, KhoParams.Program,
                        KhoParams.ReportFile,
                        "reportCaption", "caption2", "", "", "");
                    c.FilterControl.SetData(plistData);
                    c.AutoClickNhan = true;
                    c.ShowToForm(this, "Vitri");
                }

                container.SetColorMavt(vtd.MA_VT, Color.Blue);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
            var data = new Dictionary<string, object>();
            data.Add("LISTVITRIDETAIL", _listVitriDetail);
            OnV6Click(data);
        }

        public void ClearDataVitriVaTu()
        {
            //string MA_VT = "";

            if (_listVitriDetail.Count > 0)
            {
                //MA_VT += string.Format("(+{0})", _listVitriDetail.Count - 1);
                foreach (ViTriDetail detail in _listVitriDetail)
                {
                    detail.MA_VT = null;
                    detail._rowDataVitriVattu = null;
                }
            }

            if (_listVitriDetail.Count == 1)
            {
                if (KhoParams.ViewLable2)
                {
                    label2.Visible = true;
                    //label2.Text = "";
                    label2.ForeColor = Color.Black;
                }
                else
                {
                    label1.ForeColor = Color.Black;
                }
            }
        }

        public void SetDataVitriVatTu(DataRow row, string cVitri, string cMavt)
        {
            if (_listVitriDetail.Count > 1)
            {
                DoNothing();
            }

            if (_listVitriDetail.Count > 0)
            {
                foreach (ViTriDetail detail in _listVitriDetail)
                {
                    if (detail.MA_VITRI == cVitri)
                    {
                        detail._rowDataVitriVattu = row;
                        detail.MA_VT = row["MA_VT"].ToString().Trim();
                        break;
                    }
                }
            }

            if (KhoParams.ViewLable2 && _listVitriDetail.Count == 1)
            {
                label2.Visible = true;
                label2.Text = cMavt;
            }

            var RGB = row["RGB"].ToString().Trim();
            var rgb = ObjectAndString.SplitString(RGB);
            if (rgb.Length >= 3)
            {
                int r = ObjectAndString.ObjectToInt(rgb[0]);
                int g = ObjectAndString.ObjectToInt(rgb[1]);
                int b = ObjectAndString.ObjectToInt(rgb[2]);
                if (KhoParams.ViewLable2)
                {
                    label2.ForeColor = Color.FromArgb(r, g, b);
                }
                else
                {
                    label1.ForeColor = Color.FromArgb(r, g, b);
                }
            }
            //_rowDataVitriVattu = row;
        }

        public void SetColorMavt(string maVt, Color color)
        {
            bool have = false;
            foreach (ViTriDetail detail in _listVitriDetail)
            {
                if (!string.IsNullOrEmpty(detail.MA_VT) && detail.MA_VT == maVt)
                {
                    have = true;
                    break;
                }
            }

            if (have)
            {
                //label1.ForeColor = color;
                //label2.Visible = true;
                label3.BackColor = color;
            }
            else
            {
                //label1.ForeColor = Color.Black;
                label3.BackColor = Color.Transparent;
            }
        }

        private void ViTriControl_SizeChanged(object sender, EventArgs e)
        {
            label2.Height = Height - label1.Height - label3.Height;
            label2.Top = label1.Bottom;
        }


        public void AddDetail(ViTriDetail vitriDetail)
        {
            _listVitriDetail.Add(vitriDetail);
            
            //var vtd0 = _listVitriDetail[0];
            string showtext = string.Format("({0})", _listVitriDetail.Count);
            label2.Visible = true;
            label2.Text = showtext;
        }
    }
}
