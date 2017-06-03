using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class APDMO_APF9 : FilterBase
    {
        public APDMO_APF9()
        {
            InitializeComponent();

            F3 = true;
            F8 = true;
            
            txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
            if (V6Login.MadvcsCount <= 1)
            {
                txtMaDvcs.Enabled = false;
            }

            dateNgay_ct1.Value = V6Setting.M_ngay_ct1;
            dateNgay_ct2.Value = V6Setting.M_ngay_ct2;
        }

        private DataSet _ds;
        private string _stt_rec;
        
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@Ma_kh", maKhachHang.StringValueCheck));
            result.Add(new SqlParameter("@Tk", taiKhoan.StringValueCheck));
            result.Add(new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValueCheck));
            result.Add(new SqlParameter("@Stt_rec", ""));
            
            return result;
        }

        public override void SetData(SortedDictionary<string, object> data)
        {
            try
            {
                _stt_rec = data["STT_REC"].ToString().Trim();
                if (data.ContainsKey("MA_KH")) maKhachHang.VvarTextBox.Text = data["MA_KH"].ToString().Trim();
                if (data.ContainsKey("TK_I")) taiKhoan.VvarTextBox.Text = data["TK_I"].ToString().Trim();
                if (data.ContainsKey("NGAY_CT"))
                {
                    var date = (DateTime)data["NGAY_CT"];
                    dateNgay_ct1.Value = date;
                    dateNgay_ct2.Value = date;
                }
                if (data.ContainsKey("MA_DVCS")) txtMaDvcs.VvarTextBox.Text = data["MA_DVCS"].ToString().Trim();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Filter_Load(object sender, EventArgs e)
        {

        }

        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
        }

       
    }
}
