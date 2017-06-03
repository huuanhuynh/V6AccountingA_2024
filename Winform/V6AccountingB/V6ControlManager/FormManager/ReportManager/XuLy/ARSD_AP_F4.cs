﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class ARSD_AP_F4 : SoDuAddEditControlVirtual
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
            get { return txtt_tt_nt.Text; }
            set { txtt_tt_nt.Text = value; }
        }
        #endregion properties
        public ARSD_AP_F4()
        {
            InitializeComponent();
        }

        public ARSD_AP_F4(string stt_rec, DataRow am)
        {
            _stt_rec = stt_rec;
            _am = am;
            _uid = ((System.Guid)am["UID"]).ToString();
            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataRow(this, _am);
            
                txttk.SetInitFilter("loai_tk =1 and tk_cn=1");

                var length = V6BusinessHelper.VFV_iFsize("ARS30", "So_ct");
                if (length == 0) length = 12;
                txtso_ct.MaxLength = length;

                length = V6BusinessHelper.VFV_iFsize("ARS30", "dien_giai");
                if (length == 0) length = 128;
                txtdien_giai.MaxLength = length;
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(ex.Message);
            }
        }
        public  void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateData();
                var am = GetData();
                var _ma_ct = "";
                if (_stt_rec != "" && _stt_rec != null) // EDIT
                {
                    am["STT_REC"] = _am["STT_REC"];
                    am["DIEN_GIAI"] = txtdien_giai.Text;

                    var keys = new SortedDictionary<string, object>
                        {
                            { "UID", _uid }
                        };
                    var result = V6BusinessHelper.UpdateSimple("ARS30", am, keys);
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
                    var result = V6BusinessHelper.Insert("ARS30", am);

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
                V6ControlFormHelper.ShowErrorMessage("Insert error:\n" + ex.Message);
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

        protected virtual void OnUpdateSuccessEvent(SortedDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtso_ct.Text.Trim() == "") errors += "Chưa nhập số chứng từ!\r\n";
            if (txtma_kh.Text.Trim() == "") errors += "Chưa nhập mã khách hàng!\r\n";
            if (txttk.Text.Trim() == "") errors += "Chưa nhập tài khoản công nợ!\r\n";
            if (errors.Length > 0) throw new Exception(errors);
        }
      
        private void txtma_nt_TextChanged(object sender, EventArgs e)
        {
            var oldValue = value;
            value = ((TextBox)sender).Text;
            lbT_Tt_NT0.Text = string.Format("Tổng tiền phải thu {0}", value);
            lbTxtT_tt_qd.Text = string.Format("Số tiền đã thu {0}", value);
            lbTxtT_CL_NT.Text = string.Format("Số tiền còn phải thu {0}", value);
        }

        private void txtT_Tt_NT0_TextChanged(object sender, EventArgs e)
        {
            if (txtty_gia.Value == 0)
            {
                txtty_gia.Value = 1;
            }
            txtTongTT0.Value = txtT_Tt_NT0.Value * txtty_gia.Value;

            TxtT_CL_NT.Value = txtT_Tt_NT0.Value - TxtT_tt_qd.Value;
            if (TxtT_CL_NT.Value > 0)
            {
                txttat_toan.Value = 0;
            }
            else
            {
                txttat_toan.Value = 1;
            }
        }

    }
}
