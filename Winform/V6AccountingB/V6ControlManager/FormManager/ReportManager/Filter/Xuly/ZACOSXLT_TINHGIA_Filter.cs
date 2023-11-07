﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6Controls;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class ZACOSXLT_TINHGIA_Filter : FilterBase
    {
        public ZACOSXLT_TINHGIA_Filter()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgay_ct1.SetValue(V6Setting.M_SV_DATE.AddMonths(-1));
                dateNgay_ct2.SetValue(V6Setting.M_SV_DATE);
                //LoadListBoxData();
                F7 = true;
                F9 = true;
                F10 = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void ZACOSXLT_TINHGIA_Filter_Load(object sender, EventArgs e)
        {
            try
            {
                dateNgay_ct1.Focus();
                LoadListBoxData();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ZACOSXLT_TINHGIA_Filter_Load", ex);
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Ma_bpht", lineMaBpHt.StringValue));
            result.Add(new SqlParameter("@Tinhgia_dc", chkTinhGiaDC.Checked ? 1 : 0));
            result.Add(new SqlParameter("@User_id", V6Login.UserId));
            
            UpdateValues();
            return result;
        }

        public override void UpdateValues()
        {
            Date1 = dateNgay_ct1.Value;
            Date2 = dateNgay_ct2.Value;
            Number1 = 0;
            Number2 = 0;
            var selectedObject = listBox1.SelectedItem as DataRowView;
            if (selectedObject != null)
            {
                String1 = selectedObject["Proc"].ToString().Trim();
                ObjectDictionary["C_VITRI"] = selectedObject["VITRI"].ToString().Trim();
            }
            
            String2 = "";
            String3 = "";
            List<string> list_proc = new List<string>();
            List<string> list_loai_bc = new List<string>();
            List<string> list_vitri = new List<string>();
            foreach (object item in listBox1.Items)
            {
                var cRow = item as DataRowView;
                if (cRow != null)
                {
                    String2 += "~" + cRow["Proc"].ToString().Trim();
                    list_proc.Add(cRow["Proc"].ToString().Trim());
                    list_loai_bc.Add(cRow["LOAI_BC"].ToString().Trim());
                    list_vitri.Add(cRow["Vitri"].ToString().Trim());
                    String3 += "~" + cRow["Vitri"].ToString().Trim();
                }
            }
            if (String2.Length > 0) String2 = String2.Substring(1);
            if (String3.Length > 0) String3 = String3.Substring(1);
            ObjectDictionary["LIST_PROC"] = list_proc;
            ObjectDictionary["LIST_LOAI_BC"] = list_loai_bc;
            ObjectDictionary["LIST_VITRI"] = list_vitri;
            ObjectDictionary["MA_BPHT"] = lineMaBpHt.StringValue;
            ObjectDictionary["TINHGIA_DC"] = chkTinhGiaDC.Checked;
            ObjectDictionary["NGAY_CT1"] = dateNgay_ct1.Value;
            ObjectDictionary["NGAY_CT2"] = dateNgay_ct2.Value;
            ObjectDictionary["USER_ID"] = V6Login.UserId;


        }

        private void LoadListBoxData()
        {
            //acosxlt_proc
            string report_proc = "";
            XuLy48Base parent = FindParent<XuLy48Base>() as XuLy48Base;
            if (parent != null) report_proc = parent._reportProcedure;
            listBox1.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            listBox1.ValueMember = "Proc";
            listBox1.DataSource = V6BusinessHelper.Select("acosxlt_proc", "", string.Format("Status='1' and MA_BC0='{0}'", report_proc), "", "Stt").Data;
            listBox1.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            listBox1.ValueMember = "Proc";
        }


        private string ma_bpht_view = "";
        public override void Call1(object s = null) // s = "index;ma_bpht
        {
            try
            {
                var ss = ObjectAndString.SplitString(s.ToString());
                var index = ObjectAndString.ObjectToInt(ss[0]);
                if (ss.Length > 1) ma_bpht_view = ss[1];
                else ma_bpht_view = "";
                if (listBox1.Items.Count > index && listBox1.SelectedIndex != index)
                {
                    listBox1.SelectedIndex = index;
                }
                UpdateValues();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Call1", ex);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedObject = listBox1.SelectedItem as DataRowView;
                if (selectedObject != null)
                {
                    string mota = selectedObject["MO_TA"].ToString().Trim();
                    string loai_bc = selectedObject["LOAI_BC"].ToString().Trim().ToUpper();
                    string ma_bpht = loai_bc == "N" ? (" - " + ma_bpht_view) : "";
                    lblMota.Text = string.Format("{0} \n{1}{2}", listBox1.SelectedIndex + 1, mota, ma_bpht);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".listBox1_SelectedIndexChanged", ex);
            }
        }

        public override void Call2(object s = null)
        {
            //lblMota.Text = s + lblMota.Text;
        }

        private void chkTinhGiaDC_CheckedChanged(object sender, EventArgs e)
        {
            Check1 = chkTinhGiaDC.Checked;
        }

        

        
    }
}
