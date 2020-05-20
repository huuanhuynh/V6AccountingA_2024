using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.ThongTinDinhNghia
{
    public partial class ThongTinDinhNghiaForm : V6Form
    {
        //Gán thông tin định nghĩa theo accesibleDescription trên lable: M_MA_TD1,MA_TD1
        //Phần mã trước dấu , là mã trong bảng thông tin định nghĩa Altt
        //Phần mã sau dấu , có thể thay đổi cho field khác
        private string MA_DM { get; set; }
        public SortedDictionary<string, object> DataDic; 
        public event HandleResultData UpdateSuccessEvent;
        
        public ThongTinDinhNghiaForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Form sửa thông tin định nghĩa
        /// </summary>
        /// <param name="ma_dm">Mã danh mục (tên bảng)</param>
        public ThongTinDinhNghiaForm(string ma_dm)
        {
            InitializeComponent();
            MA_DM = ma_dm;
            
            MyInit();
        }

        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {

        }

        private void MyInit()
        {
            try
            {
                var key = new SortedDictionary<string, object> {{"MA_DM", MA_DM}};
                var selectResult = V6BusinessHelper.Select("Altt", key, "");
                if (selectResult.Data != null && selectResult.Data.Rows.Count == 1)
                {
                    var row = selectResult.Data.Rows[0];
                    //m_ma_td1, 2, 3
                    var info = row["m_ma_td1"].ToString().Trim();
                    SetInfo(info, matd1v, matd1e, matd1f, matd1u);
                    info = row["m_ma_td2"].ToString().Trim();
                    SetInfo(info, matd2v, matd2e, matd2f, matd2u);
                    info = row["m_ma_td3"].ToString().Trim();
                    SetInfo(info, matd3v, matd3e, matd3f, matd3u);
                    //m_ngay_td 1, 2, 3
                    info = row["m_ngay_td1"].ToString().Trim();
                    SetInfo(info, ngaytd1v, ngaytd1e, ngaytd1f, ngaytd1u);
                    info = row["m_ngay_td2"].ToString().Trim();
                    SetInfo(info, ngaytd2v, ngaytd2e, ngaytd2f, ngaytd2u);
                    info = row["m_ngay_td3"].ToString().Trim();
                    SetInfo(info, ngaytd3v, ngaytd3e, ngaytd3f, ngaytd3u);
                    //m_sl_td1, 2, 3
                    info = row["m_sl_td1"].ToString().Trim();
                    SetInfo(info, sltd1v, sltd1e, sltd1f, sltd1u);
                    info = row["m_sl_td2"].ToString().Trim();
                    SetInfo(info, sltd2v, sltd2e, sltd2f, sltd2u);
                    info = row["m_sl_td3"].ToString().Trim();
                    SetInfo(info, sltd3v, sltd3e, sltd3f, sltd3u);
                    //m_gc_td1, 2, 3
                    info = row["m_gc_td1"].ToString().Trim();
                    SetInfo(info, gctd1v, gctd1e, gctd1f, gctd1u);
                    info = row["m_gc_td2"].ToString().Trim();
                    SetInfo(info, gctd2v, gctd2e, gctd2f, gctd2u);
                    info = row["m_gc_td3"].ToString().Trim();
                    SetInfo(info, gctd3v, gctd3e, gctd3f, gctd3u);

                    //ms1, 2, 3
                    info = row["m_s1"].ToString().Trim();
                    SetInfo(info, ms1v, ms1e, ms1f, ms1u);
                    info = row["m_s2"].ToString().Trim();
                    SetInfo(info, ms2v, ms2e, ms2f, ms2u);
                    info = row["m_s3"].ToString().Trim();
                    SetInfo(info, ms3v, ms3e, ms3f, ms3u);
                    //ms1, 2, 3
                    info = row["m_s4"].ToString().Trim();
                    SetInfo(info, ms4v, ms4e, ms4f, ms4u);
                    info = row["m_s5"].ToString().Trim();
                    SetInfo(info, ms5v, ms5e, ms5f, ms5u);
                    info = row["m_s6"].ToString().Trim();
                    SetInfo(info, ms6v, ms6e, ms6f, ms6u);
                    //ms 1, 2, 3
                    info = row["m_s7"].ToString().Trim();
                    SetInfo(info, ms7v, ms7e, ms7f, ms7u);
                    info = row["m_s8"].ToString().Trim();
                    SetInfo(info, ms8v, ms8e, ms8f, ms8u);
                    info = row["m_s9"].ToString().Trim();
                    SetInfo(info, ms9v, ms9e, ms9f, ms9u);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("MyInit TTDN: " + ex.Message);
            }
        }

        private void SetInfo(string info, TextBox tiengViet, TextBox tiengAnh, TextBox format, CheckBox use)
        {
            //Visible=1,Caption=M· §N 1,English=User define code 1
            string[] sss = info.Split(new[] { ',' }, 4, StringSplitOptions.RemoveEmptyEntries);
            if (sss.Length == 0) return;
            //check visible
            bool visible = true;
            string[] ss = sss[0].Split('=');
            if (ss.Length == 2 && ss[1] != "1") visible = false;
            use.Checked = visible;
            if (sss.Length == 1) return;
            //tiếng Việt
            ss = sss[1].Split('=');
            if (ss.Length > 1) tiengViet.Text = ss[1];
            if (sss.Length == 2) return;
            //tiếng Anh
            ss = sss[2].Split('=');
            if (ss.Length > 1) tiengAnh.Text = ss[1];
            if (sss.Length == 3) return;
            //format
            ss = sss[3].Split('=');
            if (ss.Length > 1) format.Text = ss[1];
        }

        private string GenInfo(TextBox tiengViet, TextBox tiengAnh, TextBox format, CheckBox use)
        {
            return string.Format("Visible={0},Caption={1},English={2},Format={3}", 
                use.Checked?1:0, tiengViet.Text.Trim(), tiengAnh.Text.Trim(), format.Text.Trim());
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {

            if (DoUpdate())
            {
                DoUpdateSuccess(DataDic);
                DialogResult = DialogResult.OK;
                //Close();
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }
        }

        private bool DoUpdate()
        {
            try
            {
                var key = new SortedDictionary<string, object> {{"MA_DM", MA_DM}};
                DataDic = new SortedDictionary<string, object>();
                DataDic.Add("M_MA_TD1", GenInfo(matd1v, matd1e, matd1f, matd1u));
                DataDic.Add("M_MA_TD2", GenInfo(matd2v, matd2e, matd2f, matd2u));
                DataDic.Add("M_MA_TD3", GenInfo(matd3v, matd3e, matd3f, matd3u));

                DataDic.Add("M_NGAY_TD1", GenInfo(ngaytd1v, ngaytd1e, ngaytd1f, ngaytd1u));
                DataDic.Add("M_NGAY_TD2", GenInfo(ngaytd2v, ngaytd2e, ngaytd2f, ngaytd2u));
                DataDic.Add("M_NGAY_TD3", GenInfo(ngaytd3v, ngaytd3e, ngaytd3f, ngaytd3u));
                //m_sl_td1, 2, 3
                DataDic.Add("M_SL_TD1", GenInfo(sltd1v, sltd1e, sltd1f, sltd1u));
                DataDic.Add("M_SL_TD2", GenInfo(sltd2v, sltd2e, sltd2f, sltd2u));
                DataDic.Add("M_SL_TD3", GenInfo(sltd3v, sltd3e, sltd3f, sltd3u));
                //m_gc_td1, 2, 3
                DataDic.Add("M_GC_TD1", GenInfo(gctd1v, gctd1e, gctd1f, gctd1u));
                DataDic.Add("M_GC_TD2", GenInfo(gctd2v, gctd2e, gctd2f, gctd2u));
                DataDic.Add("M_GC_TD3", GenInfo(gctd3v, gctd3e, gctd3f, gctd3u));

                //Thêm
                DataDic.Add("M_S1", GenInfo(ms1v, ms1e, ms1f, ms1u));
                DataDic.Add("M_S2", GenInfo(ms2v, ms2e, ms2f, ms2u));
                DataDic.Add("M_S3", GenInfo(ms3v, ms3e, ms3f, ms3u));

                DataDic.Add("M_S4", GenInfo(ms4v, ms4e, ms4f, ms4u));
                DataDic.Add("M_S5", GenInfo(ms5v, ms5e, ms5f, ms5u));
                DataDic.Add("M_S6", GenInfo(ms6v, ms6e, ms6f, ms6u));

                DataDic.Add("M_S7", GenInfo(ms7v, ms7e, ms7f, ms7u));
                DataDic.Add("M_S8", GenInfo(ms8v, ms8e, ms8f, ms8u));
                DataDic.Add("M_S9", GenInfo(ms9v, ms9e, ms9f, ms9u));
                
                
                var a = V6BusinessHelper.UpdateSimple(V6TableName.Altt, DataDic, key);
                if (a > 0) return true;
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("DoUpdate: " + ex.Message);
            }
            return false;
        }

        private void DoUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(dataDic);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //Close();
            DialogResult = DialogResult.Cancel;
        }
        
    }
}
