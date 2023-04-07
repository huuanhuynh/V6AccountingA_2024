using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6SyncLibrary2021;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6IMDATA2TH1_Control : XuLyBase
    {
        //private readonly V6Categories _categories = new V6Categories();
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check_string = null;
        private Timer timerAutoF9;
        private System.ComponentModel.IContainer components;
        public string M_SOA_MULTI_VAT = "0";
        
        bool AUTOF9
        {
            get
            {
                return FilterControl.ObjectDictionary.ContainsKey("AUTOF9") &&
                       ObjectAndString.ObjectToBool(FilterControl.ObjectDictionary["AUTOF9"]);
            }
        } 

        public V6IMDATA2TH1_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            try
            {
                InitializeComponent();
                M_SOA_MULTI_VAT = V6Options.GetValue("M_SOA_MULTI_VAT");
                SetDefaultTime();
            }
            catch (Exception)
            {

            }
        }

        private void SetDefaultTime()
        {
            try
            {
                var date1 = FilterControl.GetControlByName("dateNgay_ct1") as DateTimePicker;
                var date2 = FilterControl.GetControlByName("dateNgay_ct2") as DateTimePicker;
                if (date1 != null && EXTRA_INFOR.ContainsKey("TIME1"))
                {
                    var HHmm = ObjectAndString.SplitStringBy(EXTRA_INFOR["TIME1"], ':');
                    var date1_value = new DateTime(date1.Value.Year, date1.Value.Month, date1.Value.Day,
                        0, ObjectAndString.ObjectToInt(HHmm[1]), 00);

                    date1.Value = date1_value.AddHours((double)Number.GiaTriBieuThuc(HHmm[0], null));
                }
                if (date2 != null && EXTRA_INFOR.ContainsKey("TIME2"))
                {
                    var HHmm = ObjectAndString.SplitStringBy(EXTRA_INFOR["TIME2"], ':');
                    var date2_value = new DateTime(date2.Value.Year, date2.Value.Month, date2.Value.Day,
                        0, ObjectAndString.ObjectToInt(HHmm[1]), 00);

                    date2.Value = date2_value.AddHours((double) Number.GiaTriBieuThuc(HHmm[0], null));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "SetDefaultTime", ex);
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F9: {0}", V6Text.Text("CHUYEN"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }
        
        protected override void MakeReport2()
        {
            try
            {
                FilterControl.UpdateValues();
                var plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("@ngay_ct1", FilterControl.Date1.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ngay_ct2", FilterControl.Date2.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ngay_ct3", FilterControl.Date3.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ma_dvcs", FilterControl.String1));
                plist.Add(new SqlParameter("@dele_yn", FilterControl.Check1 ? "1" : "0"));
                plist.Add(new SqlParameter("@auto_yn", FilterControl.Check2 ? "1" : "0"));
                plist.Add(new SqlParameter("@auto_f9", FilterControl.Check3 ? "1" : "0"));
                plist.Add(new SqlParameter("@HHFrom", (int)FilterControl.Number1));
                plist.Add(new SqlParameter("@HHTo", (int)FilterControl.Number2));
                plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                var ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, plist.ToArray());
                _tbl = ds.Tables[0];
                
                                
                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);
                InvokeFormEvent("AFTERFIXDATA");
                
                dataGridView1.DataSource = _tbl;
                FormatGridViewBase();
                FormatGridViewExtern();
                
                if (!AUTOF9 && !string.IsNullOrEmpty(check_string))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check_string);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        
        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (!string.IsNullOrEmpty(check_string))
                {
                    if (!AUTOF9) this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check_string);
                    return;
                }

                if (newMyThread != null && newMyThread._Status == Status.Running)
                {
                    this.ShowWarningMessage("Running!");
                    return;
                }

                if (_tbl != null)
                {
                    FilterControl.UpdateValues();
                    LockButtons();
                    
                    chkAutoSoCt_Checked = FilterControl.Check2;

                    Timer timerF9 = new Timer {Interval = 1000};
                    timerF9.Tick += tF9_Tick;
                    remove_list_d = new List<DataRow>();
                    Thread t = new Thread(F9Thread_AMAD);
                    t.SetApartmentState(ApartmentState.STA);
                    CheckForIllegalCrossThreadCalls = false;
                    t.IsBackground = true;
                    t.Start();
                    timerF9.Start();

                    if (Visible) V6ControlFormHelper.SetStatusText("F9 running ");
                }
                else
                {
                    this.ShowInfoMessage(V6Text.Text("NODATA"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        /// <summary>
        /// Thông báo tức thời trong Status1.
        /// </summary>
        private string f9Message = "";
        /// <summary>
        /// Thông báo cuối cùng sau khi chạy xong.
        /// </summary>
        private string f9MessageAll = "";
        private IDictionary<string, object> AM_DATA;
        private bool chkAutoSoCt_Checked = false;
        MyThread newMyThread;
        private void F9Thread_AMAD()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";

                var ALFCOPY2LIST = V6BusinessHelper.Select("ALFCOPY2LIST", "*", "MA_FILE='" + _reportProcedure + "'").Data;
                var ALFCOPY2DATA = V6BusinessHelper.Select("ALFCOPY2DATA", "*", "MA_FILE='" + _reportProcedure + "'").Data;
                newMyThread = new MyThread(DatabaseConfig.ConnectionString, DatabaseConfig.ConnectionString2_TH, DatabaseConfig.ServerName, 0, _tbl.Rows[0]);
                newMyThread.ConnectionTimeOut = DatabaseConfig.TimeOut;
                newMyThread.ALFCOPY2LIST = ALFCOPY2LIST;
                newMyThread.ALFCOPY2DATA = ALFCOPY2DATA;
                newMyThread.ThrowExceptionEvent += newMyThread_ThrowExceptionEvent;
                
                newMyThread.Start();
            }
            catch (Exception ex)
            {
                f9Message = "F9Thread_AMAD: " + ex.Message;
            }

            f9Running = false;
        }

        void newMyThread_ThrowExceptionEvent(Exception ex)
        {
            ShowMainMessage(_program + " " + ex.Message);
        }


        private int total, index;
        private string f9Error = "";
        private string f9ErrorAll = "";
        
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running || newMyThread._Status == Status.Running)
            {
                //Remove
                while (remove_list_d.Count > 0)
                {
                    _tbl.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }

                //var cError = newMyThread._Message;
                //if (cError.Length > 0)
                //{
                //    V6ControlFormHelper.SetStatusText(cError);
                //    foreach (DataGridViewRow row in dataGridView1.Rows)
                //    {
                //        row.DefaultCellStyle.BackColor = Color.Red;
                //    }
                //}
            }
            else
            {
                ((Timer)sender).Stop();
                UnlockButtons();
                InvokeFormEvent(FormDynamicEvent.AFTERF9);
                //Remove
                while (remove_list_d.Count > 0)
                {
                    _tbl.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }

                SetStatusText("V6IMDATA2 F9 finish " + newMyThread._Status + newMyThread._Message);
                
                if (newMyThread._Status == Status.Exception)
                {
                    this.ShowErrorMessage(newMyThread._Message);
                }
                else if (newMyThread._Status == Status.Finish)
                {
                    this.ShowMessage(V6Text.Finish + " " + newMyThread._Message, 500);
                }
                //V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9MessageAll, 500, this);
                if (f9MessageAll.Length > 0)
                {
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "F9 " + f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }
        #endregion xử lý F9

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerAutoF9 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerAutoF9
            // 
            this.timerAutoF9.Enabled = true;
            this.timerAutoF9.Interval = 1000;
            this.timerAutoF9.Tick += new System.EventHandler(this.timerAutoF9_Tick);
            // 
            // V6IMDATA2TH1_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "V6IMDATA2TH1_Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private int _autoF9count = 0;
        private void timerAutoF9_Tick(object sender, EventArgs e)
        {
            try
            {
                _autoF9count++;
                if (InTimeAutoF9())
                {
                    _autoF9count = 0;
                    btnNhan.PerformClick();
                    XuLyF9();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GET_AM_Data", ex);
            }
        }

        private bool InTimeAutoF9()
        {
            if (!AUTOF9) return false;
            if (!FilterControl.ObjectDictionary.ContainsKey("AUTOF9TIME")) return false;
            int minute = ObjectAndString.ObjectToInt(FilterControl.ObjectDictionary["AUTOF9TIME"]);
            if (_autoF9count < minute * 60) return false;
            return true;
        }
    }
}
