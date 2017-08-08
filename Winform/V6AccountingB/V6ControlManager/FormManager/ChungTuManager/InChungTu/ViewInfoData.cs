using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu
{
    public partial class ViewInfoData : V6Form
    {
        public string STT_REC { get; set; }
        public string MA_CT { get; set; }
        private V6InvoiceBase Invoice;
        public ViewInfoData()
        {
            InitializeComponent();
        }
        public ViewInfoData(V6InvoiceBase invoice, string sttrec, string mact)
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
                
                var data = V6BusinessHelper.Select("V6ViewInfo", "*", "", "", "STT").Data;
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
                if (selectedValue == "PRINT_AMAD")
                {
                    var data = v6ColorDataGridView1.CurrentRow.ToDataDictionary();
                    //Goi form in.


                    string program = selectedValue;
                    string repFile = selectedValue;
                    string repTitle = "PHIẾU HẠCH TOÁN";
                    string repTitle2 = "GENERAL VOUCHER";
                    string _sttRec = "";
                    if (data.ContainsKey("STT_REC")) _sttRec = data["STT_REC"].ToString().Trim();

                    var c = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                            "", "", "", _sttRec);
                    c.TTT = ObjectAndString.ObjectToDecimal(data["T_PS_NO"]);// txtTongThanhToan.Value;
                    c.TTT_NT = ObjectAndString.ObjectToDecimal(data["T_PS_NO_NT"]);// txtTongThanhToanNt.Value;
                    c.MA_NT = ObjectAndString.ObjectToString(data["MA_NT"]);

                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
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
                if (comboBox1.SelectedItem != null)// && !(comboBox1.SelectedValue is DataRowView))
                {
                    var select = comboBox1.SelectedValue.ToString().Trim();
                    //Gen param
                    SqlParameter[] plist =
                {
                    new SqlParameter("@cStt_rec", STT_REC),
                    new SqlParameter("@cTable", select), 
                    new SqlParameter("@cMa_ct", MA_CT), 
                };
                    var loadData = V6BusinessHelper.ExecuteProcedure(Procedure, plist).Tables[0];
                    v6ColorDataGridView1.DataSource = loadData;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SelectFunction: " + ex.Message, "InChungTu.ViewInfoData");
            }
        }

        private void ViewInfoData_Load(object sender, EventArgs e)
        {
            LoadCombobox();
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

        
    }
}
