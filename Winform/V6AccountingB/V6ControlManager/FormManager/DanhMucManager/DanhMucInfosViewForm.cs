using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.DanhMucManager
{
    public partial class DanhMucInfosViewForm : V6Form
    {
        public IDictionary<string, object> ROW_DATA { get; set; }
        public string MA_DM { get; set; }
        
        public DanhMucInfosViewForm()
        {
            InitializeComponent();
        }
        public DanhMucInfosViewForm(string ma_dm, IDictionary<string, object> row_data)
        {
            InitializeComponent();
            MA_DM = ma_dm;
            ROW_DATA = row_data;
        }

        private void LoadCombobox()
        {
            try
            {
                
                var data = V6BusinessHelper.Select("V6ViewInfo", "*", "loai='2'", "", "STT").Data;
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
            string madm = "";
            try
            {
                var selectedValue = comboBox1.SelectedValue.ToString().Trim();

                if (dataGridView1.RowCount == 0)
                {
                    madm = "";
                    return;
                }
                var data = dataGridView1.CurrentRow.ToDataDictionary();
                if (data.ContainsKey("MA_DM"))
                {
                    madm = data["MA_DM"].ToString().Trim();
                }
                else
                {
                    madm = "";
                    return;
                }

                if (selectedValue.Contains("PRINT_INFOR2"))
                {
                    
                }
                else
                {
                    madm = "";
                    return;
                }


                string program = selectedValue;
                string repFile = selectedValue;
                string repTitle = "";
                string repTitle2 = "";

                if (selectedValue == "PRINT_INFOR2")
                {
                    repTitle = "THÔNG TIN NGƯỜI CẬP NHẬT";
                    repTitle2 = "VOUCHER INFORMATION";
                }

                //var c0 = new InChungTuViewBase(Invoice, program, program, repFile, repTitle, repTitle2,
                //        "", "", "", madm);
                //c0.TTT = ObjectAndString.ObjectToDecimal(data["T_PS_NO"]);// txtTongThanhToan.Value;
                //c0.TTT_NT = ObjectAndString.ObjectToDecimal(data["T_PS_NO_NT"]);// txtTongThanhToanNt.Value;
                //c0.MA_NT = ObjectAndString.ObjectToString(data["MA_NT"]);

                var c = new ReportRViewBase("Invoice.Mact", program, program, repFile,
                    repTitle, repTitle2, "", "", "");
                
                //Gán dữ liệu filter stt_rec
                //IDictionary<string, object> filterData = new SortedDictionary<string, object>();
                //filterData.Add("STT_REC", madm);
                //c.FilterControl.SetData(filterData);
                List<SqlParameter> plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("@MA_DM", madm));
                plist.Add(new SqlParameter("@sUID", ROW_DATA["UID"]));
                plist.Add(new SqlParameter("@User_id", V6Login.UserId));
                c.FilterControl.InitFilters = plist;
                
                c.btnNhan_Click(null, null);
                c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        private const string Procedure = "VPA_V6VIEW_INFOR_LIST";

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
                        new SqlParameter("@cTable", select),
                        new SqlParameter("@MA_DM", MA_DM),
                        new SqlParameter("@sUID", ROW_DATA["UID"]),
                        new SqlParameter("@User_id", V6Login.UserId),
                    };
                    var loadData = V6BusinessHelper.ExecuteProcedure(Procedure, plist).Tables[0];
                    dataGridView1.DataSource = loadData;
                    //FormatGridView();
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
