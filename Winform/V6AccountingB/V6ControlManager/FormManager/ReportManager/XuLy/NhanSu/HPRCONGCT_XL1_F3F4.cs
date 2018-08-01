using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class HPRCONGCT_XL1_F3F4 : V6Form
    {
        #region Biến toàn cục

        protected SortedDictionary<string, object> _data;
        protected V6Mode _mode;
        //protected string _text;
        //protected string _uid;
        protected string _tableName = "PRCONG2";

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
        public HPRCONGCT_XL1_F3F4()
        {
            InitializeComponent();
        }

        public HPRCONGCT_XL1_F3F4(V6Mode mode, SortedDictionary<string, object> data)
        {
            _mode = mode;
            
            _data = data;
            InitializeComponent();
            MyInit();
            //Getmaxstt();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataDictionary(this, _data);
                dateNgay.DisableTag();
                txtMaNhanSu.ExistRowInTable();

                if (_mode == V6Mode.Add)
                {
                    Text = "Thêm";
                }
                else if(_mode == V6Mode.Edit)
                {
                    Text = "Sửa";
                }
                else if (_mode == V6Mode.View)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(this, true);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Tinh_gio(object sender, EventArgs e)
        {
            if (_mode == V6Mode.Add || _mode == V6Mode.Edit)
            {
                decimal gio_vao = 0;
                decimal gio_ra = 0;
                decimal gio = 0;
                decimal sl_td3 = 0;
                gio_vao = Txtgio_vao.Value;
                gio_ra = Txtgio_ra.Value;

                if (gio_vao != 0 && gio_ra != 0)
                {
                    if ((gio_ra - gio_vao) - 0.5m < 0)
                    {
                        sl_td3 = 0;
                        gio = 0;
                    }
                    else if ((gio_ra - gio_vao) < 5)
                    {
                        sl_td3 = gio_ra - gio_vao;
                        gio = sl_td3;
                    }
                    else
                    {
                        sl_td3 = gio_ra - gio_vao - 1;
                    }
                    if (sl_td3 >= ObjectAndString.ObjectToDecimal(V6Options.V6OptionValues["M_PR_SO_GIO"]))
                    {
                        gio = 8;
                    }
                    Txtsl_td3.Value = sl_td3;
                    txtGio.Value = gio;

                }
            }
        }

        private void UpdateData()
        {
            try
            {
                var data = GetData();

                //data["KHOA_HELP"] = _stt_rec;
                
                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _data["UID"]}
                };

                var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
                if (result == 1)
                {
                    Dispose();
                    ShowTopLeftMessage(V6Text.UpdateSuccess);
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

                //data["KHOA_HELP"] = _stt_rec;

                var result = V6BusinessHelper.Insert(_tableName, data);

                if (result)
                {
                    Dispose();
                    ShowTopLeftMessage(V6Text.AddSuccess);
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
       
        
        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (_mode == V6Mode.Edit)
                {
                    UpdateData();
                }
                else if (_mode == V6Mode.Add)
                {
                    InsertNew();
                }
            }
            else
            {
                ShowMainMessage(V6Text.ExistData);
            }
        }

        private bool ValidateData()
        {
            try
            {
                byte status = 0;
                var dataOld = new SortedDictionary<string, object>();
                if (_mode == V6Mode.Edit)
                {
                    status = 0;
                    dataOld.AddRange(_data);
                }
                else
                {
                    status = 1;
                    dataOld = GetData();
                }
                
                bool valid = V6BusinessHelper.IsValidThreeCode_OneDate(_tableName, status,
                    "MA_NS", txtMaNhanSu.Text, (dataOld["MA_NS"]??"").ToString().Trim(),
                    "MA_CONG", txtMaCong.Text, (dataOld["MA_CONG"]??"").ToString().Trim(),
                    "MA_BP", (txtMaBp.Value + "").Trim(), (dataOld["MA_BP"]??"").ToString().Trim(),
                    "NGAY", dateNgay.YYYYMMDD, ObjectAndString.ObjectToFullDateTime(dataOld["NGAY"]).ToString("yyyyMMdd")
                    );
                return valid;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("ValidateData", ex);
            }
            return false;
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

    }
}
