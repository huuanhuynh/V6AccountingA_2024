using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINCD2 : FilterBase
    {
        public AINCD2()
        {
            InitializeComponent();

            String2 = "TEN_VT";
            String1 = "MA_VT";

            TxtVttonkho.Text = "*";

            F3 = false;
            F5 = false;

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();
            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }
            Txtnh_vt1.VvarTextBox.SetInitFilter("loai_nh=1");
            Txtnh_vt2.VvarTextBox.SetInitFilter("loai_nh=2");
            Txtnh_vt3.VvarTextBox.SetInitFilter("loai_nh=3");
            Txtnh_vt4.VvarTextBox.SetInitFilter("loai_nh=4");
            Txtnh_vt5.VvarTextBox.SetInitFilter("loai_nh=5");
            Txtnh_vt6.VvarTextBox.SetInitFilter("loai_nh=6");
            SetHideFields("V");
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string>();
                GridViewHideFields.Add("TAG", "TAG");

            }
            else
            {

            }
            GridViewHideFields.Add("TIEN_NT_N", "TIEN_NT_N");
            GridViewHideFields.Add("TIEN_NT_X", "TIEN_NT_X");
            GridViewHideFields.Add("DU_DAU_NT", "DU_DAU_NT");
            GridViewHideFields.Add("DU_CUOI_NT", "DU_CUOI_NT");
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            //@StartDate varchar(8),
            //@EndDate varchar(8),
            //@Tinh_dc int,
            //@Condition nvarchar(max),
            //@Vttonkho char(1),
            //@ConditionSD nvarchar(max) = '1=1'
            //if(String1.Trim() == "") throw new exc

            if (TxtMakho.StringValue != "")
            {
                V6Setting.M_Ma_kho = TxtMakho.StringValue;
            }
            V6Setting.M_ngay_ct1 = dateNgay_ct1.Value;
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Value;

            RptExtraParameters = new SortedDictionary<string, object>();
            RptExtraParameters.Add("NGAY_CT1", dateNgay_ct1.Value);
            RptExtraParameters.Add("NGAY_CT2", dateNgay_ct2.Value);


            RptExtraParameters.Add("MA_KHO", TxtMakho.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");
            //_parameters.Add("MA_VT", TxtMa_vt.IsSelected ? TxtMakho.VvarTextBox.Text.Trim() : "");

            var result = new List<SqlParameter>();


            result.Add(new SqlParameter("@StartDate", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@EndDate", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            int tinh_dc = 0;
            if (Chk_Tinh_dc.Checked) tinh_dc = 1;
            
            result.Add(new SqlParameter("@Tinh_dc", tinh_dc));
            
            var and = radAnd.Checked;

            var cKey = "";
            var cKey_SD = "";

            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS",
                "MA_KHO",
                "MA_VT"
            }, and);
            var key1 = GetFilterStringByFields(new List<string>()
            {
                "NH_VT1",
                "NH_VT2",
                "NH_VT3",
                "NH_VT4",
                "NH_VT5",
                "NH_VT6",
                "MA_QG",
                "MA_NSX",
                "TK_VT"
            }, and);

            if (!string.IsNullOrEmpty(key0))
            {
                if (and)
                {
                    cKey += string.Format("(1=1 AND {0})", key0);
                }
                else
                {
                    cKey += string.Format("(1=2 OR {0})", key0);
                }
            }
            else
            {
                cKey = "1=1";
            }

            if (!string.IsNullOrEmpty(key1))
            {
                cKey = cKey + string.Format(" and ma_vt in (select ma_vt from alvt where {0} )", key1);
            }

            cKey_SD += cKey;


            result.Add(new SqlParameter("@Condition", cKey));
            result.Add(new SqlParameter("@Vttonkho", TxtVttonkho.Text.Trim()));
            result.Add(new SqlParameter("@ConditionSD", cKey_SD));
            result.Add(new SqlParameter("@Group", lblGroupString.Text));

            return result;
        }

        //Thay đổi icon.
        public override void Call1(object s = null)
        {
            ImageList image_list = s as ImageList;
            var lever_count = String1.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Length;
            if (image_list != null)
            {
                if (lever_count > 0) image_list.Images[lever_count - 1] = Properties.Resources.Box16;
                if (lever_count > 1) image_list.Images[lever_count - 2] = Properties.Resources.House16;
                if (lever_count > 2) image_list.Images[lever_count - 3] = Properties.Resources.TreeFolderYellowk16;
                if (lever_count > 3) image_list.Images[lever_count - 4] = Properties.Resources.Add16;
                if (lever_count > 4) image_list.Images[lever_count - 5] = Properties.Resources.Add16;
                if (lever_count > 5) image_list.Images[lever_count - 6] = Properties.Resources.Add16;
            }
        }

        private void NH_VT1_TextChanged(object sender, System.EventArgs e)
        {
            var current = sender as TextBox;
            if (current != null)
                foreach (Control control in groupBoxNhom.Controls)
                {
                    if (control != current && control.Text == current.Text)
                    {
                        control.Text = "0";
                    }
                }

            String2 = V6BusinessHelper.GenGroup(
                "TEN_NH", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
            if (String2.Length > 0) String2 += ",";
            String2 += "TEN_VT";

            lblGroupString.Text = V6BusinessHelper.GenGroup(
                "NH_VT", NH_VT1.Text, NH_VT2.Text, NH_VT3.Text, NH_VT4.Text, NH_VT5.Text, NH_VT6.Text);
            String1 = lblGroupString.Text + (lblGroupString.Text.Length > 0 ? "," : "") + "MA_VT";

        }

        private void chkHienMa_CheckedChanged(object sender, EventArgs e)
        {
            Check1 = chkHienMa.Checked;
        }
    }
}
