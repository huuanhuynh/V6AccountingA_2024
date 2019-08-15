using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using V6Controls;
using V6Init;

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

            dateNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
            dateNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
        }

        private string _stt_rec;
        /// <summary>
        /// 0 mặc định, 1 stt_rec
        /// </summary>
        public int _pb_type = 0;
        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();

            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@Ma_kh", maKhachHang.StringValueCheck));
            result.Add(new SqlParameter("@Tk", taiKhoan.StringValueCheck));
            result.Add(new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValueCheck));
            result.Add(new SqlParameter("@Stt_rec", _pb_type == 0 ? "" : _stt_rec));
            
            return result;
        }

        public override void SetData(IDictionary<string, object> data)
        {
            try
            {
                _stt_rec = data["STT_REC"].ToString().Trim();
                if (data.ContainsKey("MA_KH")) maKhachHang.VvarTextBox.Text = data["MA_KH"].ToString().Trim();
                if (data.ContainsKey("TK_I")) taiKhoan.VvarTextBox.Text = data["TK_I"].ToString().Trim();
                if (data.ContainsKey("NGAY_CT"))
                {
                    var date = (DateTime)data["NGAY_CT"];
                    dateNgay_ct1.SetValue(date);
                    dateNgay_ct2.SetValue(date);
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
