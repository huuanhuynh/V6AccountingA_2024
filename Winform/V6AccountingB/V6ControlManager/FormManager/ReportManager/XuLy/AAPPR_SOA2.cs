using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePostManager;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    /// <summary>
    /// Chuyển sang hóa đơn điện tử.
    /// </summary>
    public class AAPPR_SOA2 : XuLyBase
    {
        public string MAU
        {
            get
            {
                return FilterControl != null ? FilterControl.String2 : "";
            }
        }

        public string LAN
        {
            get { return "V"; }
        }
        private string ReportFileFull
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _reportFile + ".rpt";//_reportFile gốc
                }
                return result;
            }
        }
        

        public AAPPR_SOA2(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9: Duyệt chứng từ, F8: Hủy duyệt.");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        
        
        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string f9MessageAll = "";
        protected override void XuLyF9()
        {
            try
            {
                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }
        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";
            f9MessageAll = "";

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        string mode = row.Cells["Kieu_in"].Value.ToString();
                        string soct = row.Cells["So_ct"].Value.ToString().Trim();
                        string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                        string file = row.Cells["File_in"].Value.ToString().Trim();
                        string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@HoaDonMau","0"),
                            new SqlParameter("@isInvoice","1"),
                            new SqlParameter("@ReportFile",""),
                            new SqlParameter("@MA_TD1", FilterControl.String1),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        DataSet ds = V6BusinessHelper.ExecuteProcedure(_program + "F9", plist);
                        //DataTable data0 = ds.Tables[0];
                        string result = "", error = "", sohoadon = "", id = "";
                        var pmparams = new PostManagerParams
                        {
                            DataSet = ds,
                            Mode = mode,
                            Branch = FilterControl.String1,
                            Dir = dir,
                            FileName = file,
                            RptFileFull = ReportFileFull,
                            Fkey_hd = fkey_hd,
                        };
                        result = PostManager.PowerPost(pmparams, out sohoadon, out id, out error);

                        if (string.IsNullOrEmpty(error))
                        {
                            f9MessageAll += string.Format("\n{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct, sohoadon, id, result, V6Text.Text("ThanhCong"));
                        }
                        else
                        {
                            f9MessageAll += string.Format("\n{3} Soct:{0}, error:{1}\nResult:{2}", soct, error, result, V6Text.Text("COLOI"));
                        }
                        
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                    f9MessageAll += ex.Message;
                }
            }
            f9Running = false;
        }
        
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length>0?"Error: ":"")
                    + cError + _message);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                string message = "F9 " + V6Text.Finish + " " + (f9ErrorAll.Length > 0 ? "Error: " : "") + f9ErrorAll;
                V6ControlFormHelper.SetStatusText(message);
                V6ControlFormHelper.ShowMainMessage(message);
                this.ShowMessage("F9 " + V6Text.Finish + " " + f9MessageAll);
            }
        }
        #endregion xulyF9

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA2 ViewDetails: " + ex.Message);
            }
        }
    }

    internal class ConfigLine
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string FieldV6 { get; set; }
        /// <summary>
        /// <para>Field -> lấy từ dữ liệu theo field.</para>
        /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
        /// </summary>
        public string Type { get; set; }

        public string DataType { get; set; }
    }
}
