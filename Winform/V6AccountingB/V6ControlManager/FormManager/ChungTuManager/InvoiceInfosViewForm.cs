using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class InvoiceInfosViewForm : V6Form
    {
        public string STT_REC { get; set; }
        public string MA_CT { get; set; }
        /// <summary>
        /// Cờ chuyển Database2_TH
        /// </summary>
        public bool Data2_TH { get; set; }

        private V6InvoiceBase Invoice;
        public InvoiceInfosViewForm()
        {
            InitializeComponent();
        }
        public InvoiceInfosViewForm(V6InvoiceBase invoice, string sttrec, string mact)
        {
            InitializeComponent();
            Invoice = invoice;
            STT_REC = sttrec;
            MA_CT = mact;
        }

        private void LoadCombobox()
        {
            try
            {
                DataTable data;
                if (Data2_TH)
                    data = V6BusinessHelper.Select2_TH("V6ViewInfo", "*", "loai='1'", "", "STT").Data;
                else
                    data = V6BusinessHelper.Select("V6ViewInfo", "*", "loai='1'", "", "STT").Data;
                comboBox1.ValueMember = "Procedure";
                comboBox1.DisplayMember = V6Setting.IsVietnamese ? "Bar" : "Bar2";
                comboBox1.DataSource = data;
                comboBox1.ValueMember = "Procedure";
                comboBox1.DisplayMember = V6Setting.IsVietnamese ? "Bar" : "Bar2";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadCombobox: " + ex.Message, "InChungTu.ViewInfoData");
            }
        }
        
        private void Print()
        {
            try
            {
                var selectedValue = comboBox1.SelectedValue.ToString().Trim();

                if (dataGridView1.RowCount == 0)
                {
                    _sttRec = "";
                    return;
                }
                var data = dataGridView1.CurrentRow.ToDataDictionary();
                if (data.ContainsKey("STT_REC"))
                {
                    _sttRec = data["STT_REC"].ToString().Trim();
                }
                else
                {
                    _sttRec = "";
                    return;
                }

                if (selectedValue.Contains("PRINT_AMAD") || selectedValue.Contains("PRINT_INFOR"))
                {
                    
                }
                else
                {
                    _sttRec = "";
                    return;
                }


                string program = selectedValue;
                string repFile = selectedValue;
                string repTitle = "";
                string repTitle2 = "";

                if (selectedValue == "PRINT_AMAD")
                {
                    repTitle = "PHIẾU HẠCH TOÁN";
                    repTitle2 = "GENERAL VOUCHER";
                }
                else if (selectedValue == "PRINT_INFOR")
                {
                    repTitle = "THÔNG TIN NGƯỜI CẬP NHẬT";
                    repTitle2 = "VOUCHER INFORMATION";
                }

                //var c0 = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                //        "", "", "", _sttRec);
                //c0.TTT = ObjectAndString.ObjectToDecimal(data["T_PS_NO"]);// txtTongThanhToan.Value;
                //c0.TTT_NT = ObjectAndString.ObjectToDecimal(data["T_PS_NO_NT"]);// txtTongThanhToanNt.Value;
                //c0.MA_NT = ObjectAndString.ObjectToString(data["MA_NT"]);

                var c = new ReportRViewBase(Invoice.Mact, program, program, repFile,
                    repTitle, repTitle2, "", "", "");
                
                //Gán dữ liệu filter stt_rec
                //IDictionary<string, object> filterData = new SortedDictionary<string, object>();
                //filterData.Add("STT_REC", _sttRec);
                //c.FilterControl.SetData(filterData);
                List<SqlParameter> plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("@STT_REC", _sttRec));
                plist.Add(new SqlParameter("@isInvoice", "0"));
                plist.Add(new SqlParameter("@ReportFile", repFile));
                c.FilterControl.InitFilters = plist;
                
                if (selectedValue == "PRINT_AMAD")
                {
                    //Tạo Extra parameters.
                    SortedDictionary<string, object> parameterData = new SortedDictionary<string, object>();
                    decimal TTT = ObjectAndString.ObjectToDecimal(data["T_PS_NO"]);
                    decimal TTT_NT = ObjectAndString.ObjectToDecimal(data["T_PS_NO_NT"]);
                    string LAN = c.LAN;
                    string MA_NT = ObjectAndString.ObjectToString(data["MA_NT"]);
                    parameterData.Add("SOTIENVIETBANGCHU", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
                    parameterData.Add("SOTIENVIETBANGCHUNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));
                    c.FilterControl.RptExtraParameters = parameterData;
                }

                c.btnNhan_Click(null, null);
                c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, STT_REC, ex.Message));
            }
        }

        private const string Procedure = "VPA_V6VIEW_INFOR_DATA";

        private void SelectFunction()
        {
            try
            {
                if (comboBox1.SelectedItem != null) // && !(comboBox1.SelectedValue is DataRowView))
                {
                    var select = comboBox1.SelectedValue.ToString().Trim();
                    //Gen param
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@cStt_rec", STT_REC),
                        new SqlParameter("@cTable", select),
                        new SqlParameter("@cMa_ct", MA_CT),
                    };
                    DataTable loadData;
                    if (Data2_TH)
                        loadData = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString2_TH, CommandType.StoredProcedure,Procedure, plist).Tables[0];
                    else
                        loadData = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.StoredProcedure,Procedure, plist).Tables[0];

                    dataGridView1.DataSource = loadData;
                    //FormatGridView();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SelectFunction: " + ex.Message, "InChungTu.ViewInfoData");
            }
        }

        private void ViewHistory()
        {
            try
            {
                var selectedValue = comboBox1.SelectedValue.ToString().Trim();
                if (selectedValue == "PRINT_INFOR3")
                {
                    HistoryViewerForm form = new HistoryViewerForm();
                    form.Data = dataGridView1.CurrentRow.ToDataDictionary();
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "ViewHistory", ex);
            }
        }

        private void ViewInfoData_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (Data2_TH)
            {
                Text += " " + DatabaseConfig.Database2_TH;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFunction();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            ViewHistory();
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.ValueType == typeof (DateTime))
            {
                e.Column.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        
    }
}
