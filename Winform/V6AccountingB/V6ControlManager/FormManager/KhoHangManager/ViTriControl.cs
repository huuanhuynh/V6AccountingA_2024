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

namespace V6ControlManager.FormManager.KhoHangManager
{
    public partial class ViTriControl : V6Control
    {
        /// <summary>
        /// Dữ liệu tạo control
        /// </summary>
        private DataRow _rowData;
        /// <summary>
        /// Dữ liệu vật tư theo vị trí?
        /// </summary>
        private DataRow _rowDataVitriVattu;
        //K5D509
        public string MA_KHO = null;   //K5
        public string CODE = null;  //K501
        public string DAY = null;   //A
        public string KE = null;    //A1
        public string HANG = null;  //09
        public string TYPE = null;  //
        
        public string MA_VITRI { get { return Name; } set { Name = value; } }
        public string MA_VT = null;   //K5

        public KhoParams KhoParams { get; set; }


        public ViTriControl()
        {
            InitializeComponent();
        }

        public ViTriControl(KhoParams kparas, DataRow row)
        {
            InitializeComponent();
            
            KhoParams = kparas;
            _rowData = row;

            MyInit();
        }

        private void MyInit()
        {
            try
            {
                if (KhoParams.CellWidth >= 40) Width = KhoParams.CellWidth;
                if (KhoParams.CellHeight >= 20) Height = KhoParams.CellHeight;

                MA_VITRI = _rowData["MA_VITRI"].ToString().Trim().ToUpper();
                label1.Text = Name;
                label2.Text = "";
                MA_KHO = _rowData["MA_KHO"].ToString().Trim();
                CODE = _rowData["CODE"].ToString().Trim();
                DAY = CODE.Substring(0, 1);
                KE = CODE.Substring(0, 2);
                HANG = CODE.Substring(2);
                TYPE = _rowData["TYPE"].ToString().Trim();
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
                label1.Click += ViTriButton_Click;
                label2.Click += ViTriButton_Click;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        void ViTriButton_Click(object sender, EventArgs e)
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

                if (KhoParams.Program == "AINVITRI03")
                {
                    SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                    plistData["MA_KHO"] = MA_KHO;
                    plistData["MA_KH"] = container._mavt;//MA_KH
                    plistData["MA_VT"] = container._mavt;
                    plistData["MA_VITRI"] = MA_VITRI;
                    plistData["CUOI_NGAY"] = container._cuoiNgay;
                    plistData["VT_TONKHO"] = "*";
                    plistData["KIEU_IN"] = "*";

                    plistData.AddRange(_rowDataVitriVattu.ToDataDictionary());

                    AINVITRI03_REPORT c = new AINVITRI03_REPORT(KhoParams.ItemId, KhoParams.Program, "AINVITRI03B",
                        KhoParams.ReportFile, MA_VITRI);
                    c.SetData(plistData);
                    c.Size = new Size(800, 600);
                    c.btnNhan_Click(null, null);
                    c.ShowToForm(MA_VITRI);
                }
                else

                if (TYPE == "0" || TYPE == "") // Hiển thị dữ liệu.
                {
                    var condition = string.Format("MA_KHO='{0}' and MA_VITRI='{1}'", MA_KHO.Replace("'", "''"),
                        MA_VITRI.Replace("'", "''"));
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@EndDate", container._cuoiNgay.ToString("yyyyMMdd")),
                        new SqlParameter("@Condition", condition),
                        new SqlParameter("@Vttonkho", "*"),
                        new SqlParameter("@Kieu_in", "*"),
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
                else if (TYPE == "1") // Hiển thị form báo cáo
                {
                    SortedDictionary<string, object> plistData = new SortedDictionary<string, object>();
                    plistData["MA_KHO"] = MA_KHO;
                    plistData["MA_VT"] = MA_VT;
                    plistData["MA_VITRI"] = MA_VITRI;
                    plistData["CUOI_NGAY"] = container._cuoiNgay;
                    plistData["VT_TONKHO"] = "*";
                    plistData["KIEU_IN"] = "*";

                    ReportRViewBase c = new ReportRViewBase(KhoParams.ItemId, KhoParams.Program, KhoParams.Program,
                        KhoParams.ReportFile,
                        "reportCaption", "caption2", "", "", "");
                    c.FilterControl.SetData(plistData);
                    c.btnNhan_Click(null, null);
                    c.ShowToForm("Vitri");
                }

                container.SetColorMavt(MA_VT, Color.Blue);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void ClearDataVitriVaTu()
        {
            MA_VT = null;
            if (KhoParams.ViewLable2)
            {
                label2.Visible = true;
                label2.Text = MA_VT;
            }
            
            if (KhoParams.ViewLable2)
            {
                label2.ForeColor = Color.Black;
            }
            else
            {
                label1.ForeColor = Color.Black;
            }
            
            _rowDataVitriVattu = null;
        }

        public void SetDataVitriVatTu(DataRow row, string cVitri, string cMavt)
        {
            MA_VT = cMavt;
            if (KhoParams.ViewLable2)
            {
                label2.Visible = true;
                label2.Text = MA_VT;
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
            _rowDataVitriVattu = row;
        }

        public void SetColorMavt(string maVt, Color color)
        {
            if (MA_VT != null && MA_VT == maVt)
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

        
    }
}
