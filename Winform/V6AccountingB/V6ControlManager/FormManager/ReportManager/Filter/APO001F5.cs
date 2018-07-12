using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Controls;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class APO001F5 : FilterBase
    {
        public APO001F5()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                //TxtTk.SetInitFilter("tk_cn=1");
                F3 = true;
                F5 = false;
                SetParentRowEvent += AARCD1_F5_SetParentRowEvent;
                Txtnh_kh1.VvarTextBox.SetInitFilter("loai_nh=1");
                Txtnh_kh2.VvarTextBox.SetInitFilter("loai_nh=2");
                Txtnh_kh3.VvarTextBox.SetInitFilter("loai_nh=3");
                Txtnh_kh4.VvarTextBox.SetInitFilter("loai_nh=4");
                Txtnh_kh5.VvarTextBox.SetInitFilter("loai_nh=5");
                Txtnh_kh6.VvarTextBox.SetInitFilter("loai_nh=6");
                lineNH_KH7.VvarTextBox.SetInitFilter("loai_nh=7");
                lineNH_KH8.VvarTextBox.SetInitFilter("loai_nh=8");
                lineNH_KH9.VvarTextBox.SetInitFilter("loai_nh=9");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }


        void AARCD1_F5_SetParentRowEvent(IDictionary<string, object> row)
        {
            try
            {
                Ma_kh_filterLine.VvarTextBox.Text = "";
                ma_vt_filterLine.VvarTextBox.Text = "";

                Txtnh_kh1.VvarTextBox.Text = "";
                Txtnh_kh2.VvarTextBox.Text = "";
                Txtnh_kh3.VvarTextBox.Text = "";
                Txtnh_kh4.VvarTextBox.Text = "";
                Txtnh_kh5.VvarTextBox.Text = "";
                Txtnh_kh6.VvarTextBox.Text = "";

                Txtnh_vt1.VvarTextBox.Text = "";
                Txtnh_vt2.VvarTextBox.Text = "";
                Txtnh_vt3.VvarTextBox.Text = "";
                Txtnh_vt4.VvarTextBox.Text = "";
                Txtnh_vt5.VvarTextBox.Text = "";
                Txtnh_vt6.VvarTextBox.Text = "";

                string nhom = String1.Trim();// NH_KH1,MA_KH,MA_VT


                if (nhom.Contains("NH_KH1"))
                {
                    Txtnh_kh1.VvarTextBox.Text = row["NH_KH1"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH2"))
                {
                    Txtnh_kh2.VvarTextBox.Text = row["NH_KH2"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH3"))
                {
                    Txtnh_kh3.VvarTextBox.Text = row["NH_KH3"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH4"))
                {
                    Txtnh_kh4.VvarTextBox.Text = row["NH_KH4"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH5"))
                {
                    Txtnh_kh5.VvarTextBox.Text = row["NH_KH5"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH6"))
                {
                    Txtnh_kh6.VvarTextBox.Text = row["NH_KH6"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH7"))
                {
                    lineNH_KH7.VvarTextBox.Text = row["NH_KH7"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH8"))
                {
                    lineNH_KH8.VvarTextBox.Text = row["NH_KH8"].ToString().Trim();
                }
                if (nhom.Contains("NH_KH9"))
                {
                    lineNH_KH9.VvarTextBox.Text = row["NH_KH9"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT1"))
                {
                    Txtnh_vt1.VvarTextBox.Text = row["NH_VT1"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT2"))
                {
                    Txtnh_vt2.VvarTextBox.Text = row["NH_VT2"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT3"))
                {
                    Txtnh_vt3.VvarTextBox.Text = row["NH_VT3"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT4"))
                {
                    Txtnh_vt4.VvarTextBox.Text = row["NH_VT4"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT5"))
                {
                    Txtnh_vt5.VvarTextBox.Text = row["NH_VT5"].ToString().Trim();
                }
                if (nhom.Contains("NH_VT6"))
                {
                    Txtnh_vt6.VvarTextBox.Text = row["NH_VT6"].ToString().Trim();
                }
                if (nhom.Contains("MA_VT"))
                {
                    ma_vt_filterLine.VvarTextBox.Text = row["MA_VT"].ToString().Trim();
                }
                if (nhom.Contains("MA_KH"))
                {
                    Ma_kh_filterLine.VvarTextBox.Text = row["MA_KH"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetParent", ex);
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            //@ngay_ct1 char(8),
            //@ngay_ct2 char(8),
            //@giam_tru tinyint,
            //@advance nvarchar(max),
            //@Group  nvarchar(max) = '',
            //@advance2 nvarchar(max)

            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            var nhom1 = String1.Split(','); //NH_KH1,MA_KH,MA_VT
            var listNhom = new List<string>();
            foreach (var nhom in nhom1)
            {
                listNhom.Add(nhom);
            }
            var keyf5 = GetFilterStringByFields(listNhom, true);

            result.Add(new SqlParameter("@Advance2", keyf5));
            return result;
        }

    }
}
