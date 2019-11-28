using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.KhoHangManager.Report
{
    public partial class VitriMultiForm : V6Form
    {
        public KhoParams KhoParams { get; set; }
        private Dictionary<string, ViTriControl> _listVitri = new Dictionary<string, ViTriControl>();

        public VitriMultiForm()
        {
            InitializeComponent();
        }
        public VitriMultiForm(KhoParams kparas)
        {
            KhoParams = kparas;
            InitializeComponent();
        }

        public void AddVitriList(IList<ViTriDetail> listVitri)
        {
            try
            {
                foreach (ViTriDetail detail in listVitri)
                {
                    AddViTri(detail);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddVitriList", ex);
            }
        }

        private Point _p = new Point(3, 40);
        public string container_Report_GRDSV1;
        public string container_Report_GRDFV1;
        public string container_Report_GRDHV_V1;
        public string container_Report_GRDHE_V1;
        public DateTime container_cuoiNgay;

        private void AddViTri(ViTriDetail detail)
        {
            try
            {
                ViTriControl vitri = new ViTriControl(KhoParams, detail._rowData);
                if (vitri.Width < 80)
                {
                    vitri.Width = 80;
                }
                var vtd = vitri._listVitriDetail[0];
                vitri.label1.Text = vtd.MA_VITRI;
                _listVitri[vtd.MA_VITRI] = vitri;

                var index = ObjectAndString.ObjectToInt(vtd.HANG);
                vitri.Left = vitri.Width * index - vitri.Width;
                vitri.Text = vitri.Name;
                if (detail._rowDataVitriVattu != null)
                {
                    vitri.SetDataVitriVatTu(detail._rowDataVitriVattu, detail.MA_VITRI, detail.MA_VT);
                }
                //vitri.Click += vitri_Click;
                vitri.V6Click += vitri_V6Click;
                //_listVitri.Add(vtd.MA_VITRI_SHORT, vitri);
                vitri.Location = new Point(_p.X, _p.Y);
                _p = new Point(_p.X + vitri.Width, _p.Y);
                if (_p.X > Width - vitri.Width)
                {
                    _p.X = 3;
                    _p.Y += vitri.Height;
                }
                Controls.Add(vitri);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddViTri", ex);
            }
        }

        void vitri_V6Click(IDictionary<string, object> data)
        {
            try
            {
                if (data != null && data.ContainsKey("LISTVITRIDETAIL"))
                {
                    List<ViTriDetail> listVitriDetai = data["LISTVITRIDETAIL"] as List<ViTriDetail>;
                    if (listVitriDetai != null && listVitriDetai.Count>0)
                    {
                        ViTriDetail vtd = listVitriDetai[0];

                        if (KhoParams.Program == "AINVITRI03")
                        {
                            SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                            plistData["MA_KHO"] = vtd.MA_KHO;
                            plistData["MA_KH"] = vtd.MA_VT;//MA_KH
                            plistData["MA_VT"] = vtd.MA_VT;
                            plistData["MA_VITRI"] = vtd.MA_VITRI;
                            plistData["CUOI_NGAY"] = container_cuoiNgay;
                            plistData["VT_TONKHO"] = "1";
                            plistData["KIEU_IN"] = "1";

                            plistData.AddRange(vtd._rowDataVitriVattu.ToDataDictionary());

                            AINVITRI03_REPORT c = new AINVITRI03_REPORT(KhoParams.ItemId, KhoParams.Program, "AINVITRI03B",
                                KhoParams.ReportFile, vtd.MA_VITRI, vtd.MA_VITRI);
                            c.SetData(plistData);
                            c.Size = new Size(800, 600);
                            c.btnNhan_Click(null, null);
                            c.ShowToForm(this, vtd.MA_VITRI);
                        }
                        else if (vtd.TYPE == "0" || vtd.TYPE == "") // Hiển thị dữ liệu.
                        {
                            var condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", vtd.MA_KHO.Replace("'", "''"), vtd.MA_VITRI.Replace("'", "''"));
                                    SqlParameter[] plist =
                            {
                                new SqlParameter("@EndDate", container_cuoiNgay.ToString("yyyyMMdd")),
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
                                V6ControlFormHelper.FormatGridViewAndHeader(gv, container_Report_GRDSV1, container_Report_GRDFV1,
                                    V6Setting.IsVietnamese ? container_Report_GRDHV_V1 : container_Report_GRDHE_V1);
                            };
                            gv.ToFullForm("V6");
                        }
                        else if (vtd.TYPE == "1") // Hiển thị form báo cáo
                        {
                            SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                            plistData["MA_KHO"] = vtd.MA_KHO;
                            plistData["MA_VT"] = vtd.MA_VT;
                            plistData["MA_VITRI"] = vtd.MA_VITRI;
                            plistData["CUOI_NGAY"] = container_cuoiNgay;
                            plistData["VT_TONKHO"] = "1";
                            plistData["KIEU_IN"] = "1";

                            ReportRViewBase c = new ReportRViewBase(KhoParams.ItemId, KhoParams.Program, KhoParams.Program,
                                KhoParams.ReportFile,
                                "reportCaption", "caption2", "", "", "");
                            c.FilterControl.SetData(plistData);
                            c.AutoClickNhan = true;
                            c.ShowToForm(this, "Vitri");
                        }

                        SetColorMavt(vtd.MA_VT, Color.Blue);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void SetColorMavt(string maVt, Color color)
        {
            try
            {
                txtMavt.Text = maVt;
                txtMavt.ExistRowInTable();

                foreach (KeyValuePair<string, ViTriControl> item in _listVitri)
                {
                    item.Value.SetColorMavt(maVt, color);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void txtMavt_V6LostFocus(object sender)
        {
            //_mavt = txtMavt.Text.Trim();
            SetColorMavt(txtMavt.Text, Color.Blue);
        }
    }
}
