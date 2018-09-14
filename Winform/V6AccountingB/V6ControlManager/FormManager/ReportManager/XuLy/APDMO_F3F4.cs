using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class APDMO_F3F4 : V6FormControl
    {
        #region Biến toàn cục

        protected SortedDictionary<string, object> _data;
        protected V6Mode _mode;
        protected string _stt_rec, _text,_uid;
        protected string _tableName = "APDMO";

        public event HandleResultData InsertSuccessEvent;
        protected virtual void OnInsertSuccessEvent(SortedDictionary<string, object> datadic)
        {
            var handler = InsertSuccessEvent;
            if (handler != null) handler(datadic);
        }
        public event HandleResultData UpdateSuccessEvent;
        protected virtual void OnUpdateSuccessEvent(SortedDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

        
        #endregion properties
        public APDMO_F3F4()
        {
            InitializeComponent();
        }

        public APDMO_F3F4(V6Mode mode, string stt_rec, SortedDictionary<string, object> data)
        {
            _mode = mode;
            _stt_rec = stt_rec;
            if (mode == V6Mode.Edit)
            {
                _uid = ((System.Guid) data["UID"]).ToString();
            }
            _data = data;
            InitializeComponent();
            MyInit();
            Getmaxstt();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataDictionary(this, _data);
                txtMaCt.Text = "S0K";
                if (_mode == V6Mode.Add)
                {
                    txtma_tt.Value = 2;
                }


                var length = V6BusinessHelper.VFV_iFsize("APDMO", "SO_CT");
                if (length == 0) length = 12;
                txtso_ct.MaxLength = length;
                length = V6BusinessHelper.VFV_iFsize("APDMO", "dien_giai");
                if (length == 0) length = 128;
                txtdien_giai.MaxLength = length;

                txttk.SetInitFilter("loai_tk=1");
                txttk_i.SetInitFilter("loai_tk=1");

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public  void Getmaxstt()
        {
            if (_mode == V6Mode.Add)
            {
               
            }
        }

        private void UpdateData()
        {
            try
            {
                var data = GetData();

                data["STT_REC"] = _stt_rec;
                data["STATUS"] = "2";
                data["KIEU_POST"] = "2";
                
                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _uid },
                    { "STT_REC", _stt_rec}
                };

                var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
                if (result == 1)
                {
                    Dispose();
                    ShowMainMessage(V6Text.UpdateSuccess);
                    OnUpdateSuccessEvent(data);
                }
                else
                {
                    this.ShowWarningMessage("Update: " + result);
                }


            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Update error:\n" + ex.Message);
            }
        }

        private void InsertNew()
        {
            try
            {

                var data = GetData();

                data["STT_REC"] = _stt_rec;
                data["STATUS"] = "2";
                data["KIEU_POST"] = "2";

                var result = V6BusinessHelper.Insert(_tableName, data);

                if (result)
                {
                    Dispose();
                    ShowMainMessage(V6Text.AddSuccess);
                    OnInsertSuccessEvent(data);
                }
                else
                {
                    this.ShowWarningMessage("Insert Error!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Insert error:\n" + ex.Message);
            }
        }
       
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {

            if (_mode == V6Mode.Edit)
            {
                UpdateData();
            }
            else if(_mode == V6Mode.Add)
            {
                InsertNew();
            }
        }


        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;

        private void txtma_nt_TextChanged(object sender, EventArgs e)
        {
            if (txtma_nt.Text == V6Options.M_MA_NT0)
            {
                txtt_tien_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                txtt_tien_nt.Value = V6BusinessHelper.Vround(txtt_tien_nt.Value, txtt_tien_nt.DecimalPlaces);
            }
            else if(txtma_nt.Text == "USD")
            {
                txtt_tien_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                txtt_tien_nt.Value = V6BusinessHelper.Vround(txtt_tien_nt.Value, txtt_tien_nt.DecimalPlaces);
            }
        }


        private void txtt_tien_nt_TextChanged(object sender, EventArgs e)
        {
            Txtt_tien.Value = txtt_tien_nt.Value * txtty_gia.Value;
        }

        private void txtty_gia_TextChanged(object sender, EventArgs e)
        {
            Txtt_tien.Value = txtt_tien_nt.Value * txtty_gia.Value;
        }

        private void btnChonPX_Click(object sender, EventArgs e)
        {
            XuLyChonPhieuXuat();
        }

        
        private void XuLyChonPhieuXuat()
        {
            try
            {
                var ma_kh = txtma_kh.Text.Trim();
                var ma_dvcs = txtma_dvcs.Text.Trim();
                var message = "";
                if (ma_kh != "" && ma_dvcs != "")
                {
                    // CPXHangTraLaiForm chonpx = new CPXHangTraLaiForm(this, txtma_dvcs.Text, txtma_kh.Text);
                    //chonpx.AcceptSelectEvent += chonpx_AcceptSelectEvent;
                    //chonpx.ShowDialog(this);
                    var initFilter = "MA_KH='"+ma_kh+"'";
                    var Invoice = new V6Invoice41();
                    var _soCt0 = new V6VvarTextBox();
                    _soCt0.AccessibleName = "SO_CT";

                    var f = new FilterView_ARS20(Invoice, _soCt0, _stt_rec, ma_dvcs, initFilter);
                    f.MultiSeletion = false;
                    
                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        var data = (DataGridViewRow) _soCt0.Tag;
                        txtso_ct_tt.Text = data.Cells["SO_CT"].Value.ToString().Trim();
                        txtSttRecGt.Text = data.Cells["STT_REC"].Value.ToString();
                        int isAuto = 0;
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", _stt_rec),
                            new SqlParameter("@Stt_rec_gt", txtSttRecGt.Text),
                            new SqlParameter("@Tk", txttk_i.Text),
                            new SqlParameter("@Dien_giai", txtdien_giai.Text),
                            new SqlParameter("@Ma_ct", txtMaCt.Text),
                            new SqlParameter("@Ma_nt", txtma_nt.Text),
                            new SqlParameter("@Tt_qd", Txtt_tien.Value),
                            new SqlParameter("@Tt_dn", Txtt_tien.Value),
                            new SqlParameter("@Tt_dn_nt", txtt_tien_nt.Value),
                            new SqlParameter("@IsAuto", isAuto),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        var a = V6BusinessHelper.ExecuteProcedureNoneQuery("AArttpb_GT", plist);
                        ShowMainMessage(V6Text.Finish);
                    }
                }
                else
                {
                    if (ma_kh == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã khách hàng!\n" : "Customers ID needs to enter!\n";
                    if (ma_dvcs == "") message += V6Setting.IsVietnamese ? "Chưa chọn mã đơn vị." : "Agent ID needs to enter!";
                    this.ShowWarningMessage(message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void txtma_tt_TextChanged(object sender, EventArgs e)
        {
            if (_mode == V6Mode.Edit && txtma_tt.Text == "2")
            {
                btnChonPX.Enabled = true;
                btnXoaChon.Enabled = true;
            }
            else
            {
                btnChonPX.Enabled = false;
                btnXoaChon.Enabled = false;
            }
        }

        private void btnXoaChon_Click(object sender, EventArgs e)
        {
            XyLyXoaChonHoaDon();
        }

        private void XyLyXoaChonHoaDon()
        {
            try
            {
                if (this.ShowConfirmMessage("Có chắc chắn xóa chọn hay không?") == DialogResult.Yes)
                {
                    int isAuto = 0;
                    SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", _stt_rec),
                            new SqlParameter("@Stt_rec_gt", txtSttRecGt.Text),
                            new SqlParameter("@Tk", txttk_i.Text),
                            new SqlParameter("@Dien_giai", txtdien_giai.Text),
                            new SqlParameter("@Ma_ct", txtMaCt.Text),
                            new SqlParameter("@Ma_nt", txtma_nt.Text),
                            new SqlParameter("@Tt_qd", Txtt_tien.Value),
                            new SqlParameter("@Tt_dn", Txtt_tien.Value),
                            new SqlParameter("@Tt_dn_nt", txtt_tien_nt.Value),
                            new SqlParameter("@IsAuto", isAuto),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                    var a = V6BusinessHelper.ExecuteProcedureNoneQuery("AArttpb_GT_DEL", plist);
                    txtso_ct_tt.Clear();
                    ShowMainMessage(V6Text.Finish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }
    }
}
