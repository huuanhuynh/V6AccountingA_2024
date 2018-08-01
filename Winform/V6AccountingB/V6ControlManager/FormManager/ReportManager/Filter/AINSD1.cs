using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AINSD1 : FilterBase
    {
        public AINSD1()
        {
            InitializeComponent();

            F3 = false;
            F5 = false;

            TxtMakho.VvarTextBox.Text = (V6Setting.M_Ma_kho ?? "").Trim();
            
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
            

            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

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
               
           
            //@ma_vt VARCHAR(50),
            //@ma_kho VARCHAR(50),
            //@ngay_ct2 SMALLDATETIME,
            //@ma_dvcs VARCHAR(50)

            if (TxtMavt.VvarTextBox.Text.Trim() == "")
            {
                throw new Exception("Chưa chọn mã vật tư!");
            }

            if (TxtMakho.VvarTextBox.Text != "")
            {
                V6Setting.M_Ma_kho = TxtMakho.VvarTextBox.Text;
            }
            V6Setting.M_ngay_ct2 = dateNgay_ct2.Date;
            

            var result = new List<SqlParameter>();

            if (TxtMavt.IsSelected)
                result.Add(new SqlParameter("@ma_vt", TxtMavt.VvarTextBox.Text.Trim()));
            else
                result.Add(new SqlParameter("@ma_vt", "%"));


            if (TxtMakho.IsSelected)
                result.Add(new SqlParameter("@ma_kho", TxtMakho.VvarTextBox.Text.Trim()+"%"));
            else
                result.Add(new SqlParameter("@ma_kho", "%"));


            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            
            if (txtMaDvcs.IsSelected)
                result.Add(new SqlParameter("@ma_dvcs", txtMaDvcs.VvarTextBox.Text.Trim() + "%"));
            else
                result.Add(new SqlParameter("@ma_dvcs", "%"));

           
          

            return result;
        }

        
    }
}
