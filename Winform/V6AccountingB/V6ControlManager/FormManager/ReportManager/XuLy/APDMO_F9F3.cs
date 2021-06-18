using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class APDMO_F9F3 : V6Form
    {
        #region Biến toàn cục

        private IDictionary<string, object> _data1, _data2;

        public event HandleResultData UpdateSuccessEvent;
        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

        public string So_ctx
        {
            get { return txtTienPhaiTra.Text; }
            set { txtTienPhaiTra.Text = value; }
        }
        #endregion properties
        public APDMO_F9F3()
        {
            InitializeComponent();
        }

        public APDMO_F9F3(IDictionary<string, object> data1, IDictionary<string, object> data2)
        {
            InitializeComponent();
            _data1 = data1;
            _data2 = data2;
            //MyInit();
            Text = V6Text.PhanBoTrucTiep;
        }

        public string ma_nt_1, ma_nt_2;
        public decimal ty_gia_1, ty_gia_2;

        public decimal tien_nt_1 = 0;    // Tiền nhận trên phiếu thu
        public decimal tien_1 = 0;       // Tiền nhận quy đổi mant0
        public decimal da_pb_1 = 0;    // Tiền đã phân bổ
        public decimal tien_cl_nt_1 = 0; // Tiền còn lại trên phiếu thu
        /// <summary>
        /// Tiền còn phải trả trên hóa đơn
        /// </summary>
        public decimal cl_tt_nt_2 = 0;

        private void MyInit()
        {
            try
            {
                tien_nt_1 = ObjectAndString.ObjectToDecimal(_data1["TIEN_NT"]);//Tiền nhận trên phiếu thu
                da_pb_1 = ObjectAndString.ObjectToDecimal(_data1["DA_PB"]);//   Tiền đã phân bổ
                tien_cl_nt_1 = tien_nt_1-da_pb_1;                           // Tiền còn lại trên phiếu thu
                tien_1 = ObjectAndString.ObjectToDecimal(_data1["TIEN"]);//quy đổi
                
                ma_nt_1 = _data1["MA_NT"].ToString().Trim();
                ty_gia_1 = ObjectAndString.ObjectToDecimal(_data1["TY_GIA"]);

                cl_tt_nt_2 = ObjectAndString.ObjectToDecimal(_data2["CL_TT_NT"]);//Tiền còn phải trả
                ma_nt_2 = _data2["MA_NT"].ToString().Trim();
                ty_gia_2 = ObjectAndString.ObjectToDecimal(_data2["TY_GIA"]);
                if (ty_gia_2 <= 0 || ma_nt_2 == V6Options.M_MA_NT0)
                {
                    ty_gia_2 = 1;
                }
                if (ty_gia_1 <= 0 || ma_nt_1 == V6Options.M_MA_NT0)
                {
                    ty_gia_1 = 1;
                }
                
                

                //Tiền tt đổi ra theo tỷ giá trên hóa đơn.
                var tt_1_nt_hd = tien_1/ty_gia_2;
                // khoảng tiền thanh toán cho hóa đơn + vào đã phân bổ
                var tt_nt = tt_1_nt_hd > cl_tt_nt_2 ? cl_tt_nt_2/ty_gia_1 : tt_1_nt_hd/ty_gia_1;
                //quy đổi
                var tt = tt_nt*ty_gia_1;
                //quy đổi ngoại tệ hóa đơn
                var tt_nt_hdqd = tt/ty_gia_2;

                if (ma_nt_1 == ma_nt_2)
                {
                    tt_nt = tien_nt_1 > cl_tt_nt_2 ? cl_tt_nt_2 : tien_nt_1;
                    tt = tt_nt*ty_gia_2;
                    tt_nt_hdqd = tt;
                }

                txtTienPhaiTra.Value = cl_tt_nt_2;
                txtMaNtTienPhaiTra.Text = ma_nt_2;
                
                txtTienThanhToanNt.Value = tt_nt;
                txtMaNtTienThanhToanNt.Text = ma_nt_1;
                txtTienThanhToan.Value = tt;
                txtMaNtTienThanhToan.Text = V6Options.M_MA_NT0;
                
                txtTienNtQd.Value = tt_nt_hdqd;
                txtMaNtTienNtQd.Text = ma_nt_2;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("" + GetType(), ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Ready();
        }

        private bool flag = false;
        public int isAuto;

        private void XuLyThayDoiTienTT(object sender)
        {
            try
            {
                if (!IsReady) return;
                if (flag) return;
                flag = true;

                decimal tempMinNt = Math.Min(tien_cl_nt_1, cl_tt_nt_2 * ty_gia_2 / ty_gia_1);
                decimal tempMin = Math.Min(tien_cl_nt_1 * ty_gia_1, cl_tt_nt_2 * ty_gia_2);

                decimal tt_nt = 0;
                decimal tt = 0;
                decimal t_qd_nt = 0;
                if (sender == txtTienThanhToanNt)
                {
                    tt_nt = txtTienThanhToanNt.Value;
                    tt = tt_nt * ty_gia_1;
                    if (tt_nt > tempMinNt)
                    {
                        tt_nt = tempMinNt;
                        tt = tempMin;
                        if (ma_nt_2 == V6Options.M_MA_NT0)
                        {
                            tt = cl_tt_nt_2;
                        }
                        txtTienThanhToanNt.Value = tt_nt;
                    }
                    
                    txtTienThanhToan.Value = tt;
                }
                else
                {
                    // Cần giữ nguyên giá trị gõ vào. Tính lại ngoại tệ.
                    tt = txtTienThanhToan.Value;
                    tt_nt = tt / ty_gia_1;
                    if (tt > tempMin)
                    {
                        tt = tempMin;
                        if (ma_nt_2 == V6Options.M_MA_NT0)
                        {
                            tt = cl_tt_nt_2;
                        }
                        tt_nt = tempMinNt;
                        txtTienThanhToan.Value = tt;
                    }

                    txtTienThanhToanNt.Value = tt_nt;
                }

                t_qd_nt = tt / ty_gia_2;
                txtTienNtQd.Value = t_qd_nt;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyThaiDoiTienTT", ex);
            }
            finally
            {
                flag = false;
            }
        }

        private void txtTienThanhToanNt_V6LostFocus(object sender)
        {
            XuLyThayDoiTienTT(sender);
        }

        private void txtTienThanhToan_V6LostFocus(object sender)
        {
            XuLyThayDoiTienTT(sender);
        }

        private void APDMO_F9F3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAuto == 1 && DialogResult!= DialogResult.OK)
            {
                e.Cancel = true;
                this.ShowWarningMessage("Không được hủy!");
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtMaNt_TextChanged(object sender, EventArgs e)
        {
            var input = ((TextBox) sender).Text;
            int decimals = input == "VND" ? 0 : 2;
            if (sender == txtMaNtTienThanhToan)
            {
                txtTienThanhToan.DecimalPlaces = decimals;
            }
            else if (sender == txtMaNtTienNtQd)
            {
                txtTienNtQd.DecimalPlaces = decimals;
            }
            else if (sender == txtMaNtTienPhaiTra)
            {
                txtTienPhaiTra.DecimalPlaces = decimals;
            }
            else if (sender == txtMaNtTienThanhToanNt)
            {
                txtTienThanhToanNt.DecimalPlaces = decimals;
            }
        }

        private void chkSuaTienQD_CheckedChanged(object sender, EventArgs e)
        {
            txtTienNtQd.ReadOnly = !chkSuaTienQD.Checked;
        }
        
    }
}
