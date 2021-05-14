using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter;
//using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls.LichView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class HPRCONGCT_XL1_F3 : V6Form
    {
        #region Biến toàn cục

        protected SortedDictionary<int, LichViewCellData> _data;
        private IDictionary<string, object> _rowData;
        protected V6Mode _mode;
        protected int _year, _month;
        protected string _ten_nv;
        private FilterBase FilterControl;
        //protected string _text;
        //protected string _uid;
        protected string _tableName = "PRCONG2";

        public event HandleResultData InsertSuccessEvent;
        protected virtual void OnInsertSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = InsertSuccessEvent;
            if (handler != null) handler(datadic);
        }
        public event HandleResultData UpdateSuccessEvent;
        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
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
        public HPRCONGCT_XL1_F3()
        {
            InitializeComponent();
        }

        public HPRCONGCT_XL1_F3(V6Mode mode, int year, int month, string ten_nv, FilterBase filterControl,
            SortedDictionary<int, LichViewCellData> data, IDictionary<string, object> rowData)
        {
            _mode = mode;
            _year = year;
            _month = month;
            _ten_nv = ten_nv;
            FilterControl = filterControl;
            _data = data;
            _rowData = rowData;
            InitializeComponent();
            MyInit();
            //Getmaxstt();
        }

        private void MyInit()
        {
            try
            {
                var currentDate = V6BusinessHelper.GetServerDateTime();
                lichView1.SetData(_year, _month, currentDate, _data, _rowData, _ten_nv);
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

        //public  void Getmaxstt()
        //{
        //    if (_mode == V6Mode.Add)
        //    {
        //        decimal maxvalue = V6BusinessHelper.GetMaxValueTable("V6HELP_QA", "STT", "1=1");
        //        txtSoLuong.Value = maxvalue + 1;
        //    }
        //}

        private void UpdateData()
        {
            //try
            //{
            //    var data = GetData();

            //    //data["KHOA_HELP"] = _stt_rec;
                
            //    var keys = new SortedDictionary<string, object>
            //    {
            //        { "UID", _data["UID"]}
            //    };

            //    var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
            //    if (result == 1)
            //    {
            //        Dispose();
            //        ShowTopMessage(V6Text.UpdateSuccess);
            //        OnUpdateSuccessEvent(data);
            //    }
            //    else
            //    {
            //        this.ShowWarningMessage("Update: " + result);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorMessage(GetType() + ".Update error:\n" + ex.Message);
            //}
        }

        private void InsertNew()
        {
            //try
            //{

            //    var data = GetData();

            //    //data["KHOA_HELP"] = _stt_rec;

            //    var result = V6BusinessHelper.Insert(_tableName, data);

            //    if (result)
            //    {
            //        Dispose();
            //        ShowTopMessage(V6Text.AddSuccess);
            //        OnInsertSuccessEvent(data);
            //    }
            //    else
            //    {
            //        this.ShowWarningMessage("Insert Error!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.ShowErrorMessage(GetType() + ".Insert error:\n" + ex.Message);
            //}
        }
       
        
        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            //if (ValidateData())
            //{
            //    if (_mode == V6Mode.Edit)
            //    {
            //        UpdateData();
            //    }
            //    else if (_mode == V6Mode.Add)
            //    {
            //        InsertNew();
            //    }
            //}
            //else
            //{
            //    ShowMainMessage(V6Text.ExistData);
            //}
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
        

        private void lichView1_ClickNextEvent(LichViewEventArgs obj)
        {
            ShowMainMessage("Next.");
        }

        private void lichView1_ClickPreviousEvent(LichViewEventArgs obj)
        {
            ShowMainMessage("Previous.");
        }

        void lichView1_ClickCellEvent(LichViewControl sender, LichViewEventArgs obj)
        {
            string message = string.Format("Click {0}.", obj.CellData.Key);
            if (obj.IsClickDetail1)
            {
                message += " Detail1";
            }

            if (obj.IsClickDetail2) // Giả F5
            {
                message += " Detail2";
                var ma_ns = sender.RowData["MA_NS"].ToString().Trim();
                string value1 = FilterControl.Check1 ? obj.CellData.Detail1 : "";
                decimal value2 = FilterControl.Check1 ? 0 : ObjectAndString.ObjectToDecimal(obj.CellData.Detail1);

                SqlParameter[] plist =
                    {
                        new SqlParameter("@dWork", obj.CellData.Date),
                        new SqlParameter("@nUserID", V6Login.UserId),
                        new SqlParameter("@cType", FilterControl.Check1 ? "0" : "1"),
                        new SqlParameter("@cMa_ns", ma_ns),
                        new SqlParameter("@cField", string.Format("CONG_{0:00}", obj.CellData.Day)),
                        new SqlParameter("@cValue1", value1),
                        new SqlParameter("@nValue2", value2),
                    };


                DateTime date_ngay = new DateTime(FilterControl.Date1.Year, FilterControl.Date1.Month, obj.CellData.Day);

                new HPRCONGCT_XL1_F5(plist)
                {
                    Ma_ns = ma_ns,
                    Ngay = date_ngay,
                }
                .ShowDialog(this);
            }

            if (obj.IsClickDetail3)
            {
                message += " Detail3";
            }

            ShowMainMessage(message);
        }

    }
}
