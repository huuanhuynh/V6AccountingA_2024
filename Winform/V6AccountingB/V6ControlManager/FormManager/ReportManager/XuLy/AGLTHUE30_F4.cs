using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGLTHUE30_F4 : SoDuAddEditControlVirtual
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _stt_rec, _text, _uid;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public event HandleResultData UpdateSuccessEvent;
        private string value;
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

        public string So_ctx
        {
            get { return txtT_tien_nt.Text; }
            set { txtT_tien_nt.Text = value; }
        }
        #endregion properties
        public AGLTHUE30_F4()
        {
            InitializeComponent();
            MyInit();
            Ready();
        }

        public AGLTHUE30_F4(string stt_rec, DataRow am)
        {
            _stt_rec = stt_rec;
            _am = am;
            _uid = ((System.Guid)am["UID"]).ToString();
            InitializeComponent();
            MyInit();
            Ready();
        }
        
        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataRow(this, _am);
            
                txttk_thue_no.SetInitFilter("loai_tk =1");
                txttk_du.SetInitFilter("loai_tk =1");

                var length = V6BusinessHelper.VFV_iFsize("ARV30", "So_ct");
                if (length == 0) length = 12;
                txtso_ct.MaxLength = length;

                length = V6BusinessHelper.VFV_iFsize("ARV30", "dien_giai");
                if (length == 0) length = 128;
                txtdia_chi.MaxLength = length;

                LoadAlnt();
                cboMaNt.SelectedValue = V6Options.M_MA_NT0;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void LoadAlnt()
        {
            try
            {
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
                cboMaNt.DataSource = SqlConnect.Select("Alnt", null, "", "", "ma_nt").Data;
                cboMaNt.ValueMember = "ma_nt";
                cboMaNt.DisplayMember = V6Setting.IsVietnamese ? "Ten_nt" : "Ten_nt2";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public  void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateData();
                var am = GetData();
                var _ma_ct = "APV";
                if (_stt_rec != "" && _stt_rec != null) // EDIT
                {
                    am["STT_REC"] = _am["STT_REC"];
                    am["DIEN_GIAI"] = txtdia_chi.Text;

                    var keys = new SortedDictionary<string, object>
                        {
                            { "UID", _uid }
                        };
                    var result = V6BusinessHelper.UpdateSimple("ARV30", am, keys);
                    if (result >= 1)
                    {

                        Dispose();
                        ShowMainMessage(V6Text.UpdateSuccess);
                    }
                    else
                    {
                        this.ShowWarningMessage("Update: " + result);
                    }
                }
                else // ADD
                {
                    ValidateData();
                    am["MA_CT"] = _ma_ct;
                    am["STT_REC"] = V6BusinessHelper.GetNewSttRec(_ma_ct);
                    var result = V6BusinessHelper.Insert("ARV30", am);

                    if (result)
                    {
                        Dispose();
                        ShowMainMessage(V6Text.AddSuccess);
                        OnUpdateSuccessEvent(am);
                    }
                    else
                    {
                        this.ShowWarningMessage("Insert Error!");
                    }
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Insert error:\n" + ex.Message);
            }
        }
        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnThoat.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;
        

        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }
        public void TinhToan()
        {
            if (!IsReady) return;
            if (txtso_luong.Value * txtGia_NT.Value != 0)
            {
                txtT_tien_nt.Value = V6BusinessHelper.Vround(txtso_luong.Value * txtGia_NT.Value,V6Setting.RoundTien);
            }
            txtT_tien.Value = V6BusinessHelper.Vround(txtT_tien_nt.Value * txtTyGia.Value, V6Setting.RoundTien);
            txtGia.Value = V6BusinessHelper.Vround(txtGia_NT.Value * txtTyGia.Value, V6Setting.RoundGia);
            txtT_thue_nt.Value = V6BusinessHelper.Vround(txtT_tien_nt.Value * txtthue_suat.Value, V6Setting.RoundTienNt) / 100;
            txtt_thue.Value = V6BusinessHelper.Vround(txtT_tien.Value * txtthue_suat.Value, V6Setting.RoundTien) / 100;
            
        }
        
        public override void ValidateData()
        {
            var errors = "";
            if (txtso_ct.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblSoCT.Text + "!\r\n";
            if (txtma_kh.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblMaKH.Text + "!\r\n";
            if (errors.Length > 0) throw new Exception(errors);
        }
      
        private void txtso_luongV6LostFocus(object sender)
        {
            TinhToan();
        }

        private string _maNt;
        private void cboMaNt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMaNt.SelectedValue != null)
                {
                    _maNt = cboMaNt.SelectedValue.ToString().Trim();
                    if (Mode == V6Mode.Add || Mode == V6Mode.Edit)
                    {
                        txtTyGia.Value = V6BusinessHelper.GetTyGia(_maNt, V6Setting.M_SV_DATE.Date);
                    }

                    XuLyThayDoiMaNt();
                }

                //txtTyGia_V6LostFocus(sender);

                TinhToan();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".cboMaNt_SelectedIndexChanged", ex);
            }
        }

        int M_ROUND;
        int M_ROUND_GIA;
        int M_ROUND_NT;
        int M_ROUND_GIA_NT;
        private void XuLyThayDoiMaNt()
        {
            if (_maNt.ToUpper() == V6Options.M_MA_NT0.ToUpper())
            {
                M_ROUND = V6Setting.RoundTien;
                M_ROUND_GIA = V6Setting.RoundGia;
                M_ROUND_NT = M_ROUND;
                M_ROUND_GIA_NT = M_ROUND_GIA;

                txtTyGia.Enabled = false;
                txtTyGia.Value = 1;

                txtGia_NT.DecimalPlaces = V6Options.M_IP_GIA;
                txtT_tien_nt.DecimalPlaces = V6Options.M_IP_TIEN;
                txtT_thue_nt.DecimalPlaces = V6Options.M_IP_TIEN;
            }
            else
            {
                M_ROUND_NT = V6Setting.RoundTienNt;
                M_ROUND = V6Setting.RoundTien;
                M_ROUND_GIA_NT = V6Setting.RoundGiaNt;
                M_ROUND_GIA = V6Setting.RoundGia;
                
                txtTyGia.Enabled = true;

                txtGia_NT.DecimalPlaces = V6Options.M_IP_GIA_NT;
                txtT_tien_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                txtT_thue_nt.DecimalPlaces = V6Options.M_IP_TIEN_NT;
            }
        }
    }
}
