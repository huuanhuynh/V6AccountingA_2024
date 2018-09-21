﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Controls.LichView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class StickNoteForm : V6Form
    {
        #region Biến toàn cục

        //protected SortedDictionary<int, LichViewCellData> _data;
        //private IDictionary<string, object> _rowData;
        protected V6Mode _mode;
        protected DateTime _currentDate = V6BusinessHelper.GetServerDateTime();
        //protected int _year, _month;
        //protected string _ten_nv;
        
        //protected string _text;
        //protected string _uid;
        protected string _tableName = "V6NOTESCT";

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
        public StickNoteForm()
        {
            InitializeComponent();
        }

        public StickNoteForm(V6Mode mode)
        {
            _mode = mode;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                var loadDate = new DateTime(_currentDate.Year, _currentDate.Month, DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month));
                LoadData(loadDate);

                if (_mode == V6Mode.Add)
                {
                    Text = "Stick note - Add";
                }
                else if(_mode == V6Mode.Edit)
                {
                    Text = "Stick note - Edit";
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

        private void LoadData(DateTime loadDate)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@dWork", loadDate.Date),
                new SqlParameter("@nUserID", V6Login.UserId),
                new SqlParameter("@cType", "00"),
            };
            var ds = V6BusinessHelper.ExecuteProcedure("V6STICK_NOTE", plist);
            var rowData2 = ds.Tables[0].Rows[0].ToDataDictionary();
            SortedDictionary<int, LichViewCellData> lichViewdata = new SortedDictionary<int, LichViewCellData>();

            int day_in_month = DateTime.DaysInMonth(loadDate.Year, loadDate.Month);
            for (int i = 1; i <= day_in_month; i++)
            {
                LichViewCellData cellData = new LichViewCellData(i, new DateTime(loadDate.Year, loadDate.Month, i))
                {
                    Day = i,
                    Detail1 = rowData2[string.Format("TYPE_{0:00}", i)].ToString().Trim(),
                    Detail2 = rowData2[string.Format("NOTE_{0:00}", i)].ToString().Trim(),
                    Detail2Color = ObjectAndString.RGBStringToColor(rowData2[string.Format("MAU_{0:00}", i)].ToString()),
                    Detail3 = rowData2[string.Format("NOTE1_{0:00}", i)].ToString().Trim(),
                    Detail4 = rowData2[string.Format("NOTE2_{0:00}", i)].ToString().Trim(),
                };
                lichViewdata[i] = cellData;
            }
            string ten_ns = string.Format("[{0}] [{1}]", rowData2["MA_NVIEN"], rowData2["TEN_NVIEN"].ToString().Trim());

            lichView1.SetData(loadDate.Year, loadDate.Month, _currentDate, lichViewdata, rowData2, ten_ns);
        }

        private void Reload()
        {
            LoadData(lichView1.ViewDate31);
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
            //ShowMainMessage("Bạn đã bấm nút Next.");
            var nextMonthDate = lichView1.ViewDate1.AddMonths(1);
            var nextMonthDate31 = new DateTime(nextMonthDate.Year, nextMonthDate.Month, DateTime.DaysInMonth(nextMonthDate.Year, nextMonthDate.Month));
            LoadData(nextMonthDate31);
        }

        private void lichView1_ClickPreviousEvent(LichViewEventArgs obj)
        {
            //ShowMainMessage("Bạn đã bấm nút Previous.");
            var nextMonthDate = lichView1.ViewDate1.AddMonths(-1);
            var nextMonthDate31 = new DateTime(nextMonthDate.Year, nextMonthDate.Month, DateTime.DaysInMonth(nextMonthDate.Year, nextMonthDate.Month));
            LoadData(nextMonthDate31);
        }

        void lichView1_ClickCellEvent(LichViewControl sender, LichViewEventArgs obj)
        {
            string message = string.Format("Bạn đã click ô {0}.", obj.CellData.Key);
            if (obj.IsClickDetail1)
            {
                message += " Detail1";
            }

            if (obj.IsClickDetail2) // Giả F5
            {
                message += " Detail2";
                var ma_nvien = sender.RowData["MA_NVIEN"].ToString().Trim();
                string value1 = "00";
                string value2 = obj.CellData.Detail1;

                SqlParameter[] plist =
                    {
                        new SqlParameter("@dWork", obj.CellData.Date),
                        new SqlParameter("@nUserID", V6Login.UserId),
                        new SqlParameter("@cType", "00"),
                        new SqlParameter("@cMa_nvien", ma_nvien),
                        new SqlParameter("@cField", string.Format("NOTE_{0:00}", obj.CellData.Day)),
                        new SqlParameter("@cValue1", value1),
                        new SqlParameter("@cValue2", value2),
                    };


                DateTime date_ngay = obj.CellData.Date; // DateTime date_ngay = new DateTime(currentDate.Year, currentDate.Month, obj.CellData.Day);

                new STICK_NOTE_F5(plist)
                {
                    Ma_nvien = ma_nvien,
                    Ngay = date_ngay,
                }
                .ShowDialog(this);
            }

            if (obj.IsClickDetail3)
            {
                message += " Detail3";
            }

            Reload();
            ShowTopLeftMessage(message);
        }

        private void lichView1_HoverCellChanged(LichViewEventArgs obj)
        {
            try
            {
                if (obj.CellData == null || string.IsNullOrEmpty(obj.CellData.Detail1))
                {
                    stickNoteCellView1.Visible = false;
                }
                else
                {
                    //ShowMainMessage(obj.CellData.Detail1 + "\r\n" + obj.CellData.Detail2 + "\r\n" + obj.CellData.Detail3);
                    ShowCellView(obj.CellData, obj.MouseLocation);
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }

        private void ShowCellView(LichViewCellData cellData, Point mouseLocation)
        {
            try
            {
                stickNoteCellView1.Visible = true;
                // Chỉnh vị trí của view
                if (mouseLocation.X > stickNoteCellView1.Width + 10
                    || mouseLocation.Y > stickNoteCellView1.Bottom + 10)
                {
                    stickNoteCellView1.Left = lichView1.Left;
                }
                else
                {
                    stickNoteCellView1.Left = lichView1.Right - stickNoteCellView1.Width;
                }
                // Đổi dữ liệu view
                stickNoteCellView1.SetCellData(cellData);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowCellView", ex);
            }
        }
    }
}