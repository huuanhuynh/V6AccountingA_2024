using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6LEVELDOWN : XuLyBase
    {
        public V6LEVELDOWN(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9: Hạ cấp chứng từ, F8: Trở lại người nhập ban đầu.");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct"))
                    {
                        var selectedMaCt = currentRow.Cells
                            ["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells
                            ["Stt_rec"].Value.ToString().Trim();

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                        {
                            var plist = new List<SqlParameter>
                            {
                                new SqlParameter("@stt_rec", selectedSttRec),
                                new SqlParameter("@maCT", selectedMaCt)
                            };
                            var alctRow = V6BusinessHelper.Select("Alct", "m_phdbf,m_ctdbf",
                                "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                            var amName = alctRow["m_phdbf"].ToString().Trim();

                            if (amName != "")
                            {
                                var am = V6BusinessHelper.Select(amName, "*", "STT_REC=@stt_rec and MA_CT=@maCT",
                                    "", "", plist.ToArray()).Data;

                                var fText = "Ghi chú chứng từ";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var hoaDonForm = new AAPPR_SOA_F4(selectedSttRec, am.Rows[0]);
                                hoaDonForm.txtSoCtXuat.Enabled = false;
                                hoaDonForm.txtGhiChu01.Enabled = false;
                                
                              

                                hoaDonForm.UpdateSuccessEvent += data =>
                                {
                                    currentRow.Cells["GHI_CHU01"].Value = data["GHI_CHU01"];
                                    currentRow.Cells["GHI_CHU02"].Value = data["GHI_CHU02"];

                                    currentRow.Cells["SO_CTX"].Value = data["SO_CTX"];
                                    //so_ctx_temp = data["SO_CTX"].ToString().Trim();
                                    //FilterControl.String1 = so_ctx_temp;
                                };
                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Không được phép sửa chi tiết!");
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        #region ==== Xử lý F8 ====

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        protected override void XuLyF8()
        {
            try
            {
                //V6ControlFormHelper.ShowMessage("Chưa xử lý.");
                Timer tF8 = new Timer();
                tF8.Interval = 500;
                tF8.Tick += tF8_Tick;
                Thread t = new Thread(F8Thread);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF8.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF8: " + ex.Message);
            }
        }
        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //string soct = row.Cells["So_ct"].Value.ToString();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Set_ma_level", FilterControl.Kieu_post),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "F8", plist);
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {

                    F8Error += ex.Message;
                    F8ErrorAll += ex.Message;
                }

            }
            F8Running = false;
        }

        void tF8_Tick(object sender, EventArgs e)
        {
            if (F8Running)
            {
                var cError = F8Error;
                F8Error = F8Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F8 Xử lý xong!");
            }
        }
        #endregion xulyF8


        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
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

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //string soct = row.Cells["So_ct"].Value.ToString();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Set_ma_level", FilterControl.Kieu_post),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "F9", plist);
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
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
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
            }
        }
        #endregion xulyF9

        //V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var maCt = FilterControl.String1;
                DataTable data;
                switch (maCt.ToUpper().Trim())
                {
                    case "SOA":
                        data = new V6Invoice81().LoadAd81(sttRec);
                        break;
                    case "SOC":
                        data = new V6Invoice83().LoadAd(sttRec);
                        break;
                    case "POA":
                        data = new V6Invoice81().LoadAd81(sttRec);
                        break;
                    case "SOH":
                        data = new V6Invoice91().LoadAd(sttRec);
                        break;
                    case "SOF":
                        data = new V6Invoice76().LoadAD(sttRec);
                        break;
                    case "SOR":
                        data = new V6Invoice93().LoadAd(sttRec);
                        break;
                    case "AR1":
                        data = new V6Invoice21().LoadAd(sttRec);
                        break;

                    case "POH":
                        data = new V6Invoice92().LoadAd(sttRec);
                        break;
                    case "AP1":
                        data = new V6Invoice31().LoadAd(sttRec);
                        break;
                    case "POC":
                        data = new V6Invoice73().LoadAd(sttRec);
                        break;
                    case "POB":
                        data = new V6Invoice72().LoadAd(sttRec);
                        break;
                    case "AP2":
                        data = new V6Invoice32().LoadAd(sttRec);
                        break;
                    case "IXC":
                        data = new V6Invoice86().LoadAd(sttRec);
                        break;

                    case "BC1":
                    case "TA1":
                        data = new V6Invoice41(maCt).LoadAd(sttRec);
                        break;
                    case "BN1":
                    case "CA1":
                        data = new V6Invoice51(maCt).LoadAd(sttRec);
                        break;

                    case "GL1":
                    case "AR9":
                    case "AP9":
                        data = new V6Invoice11(maCt).LoadAd(sttRec);
                        break;

                    case "IND":
                        data = new V6Invoice74().LoadAd(sttRec);
                        break;
                    case "IXA":
                        data = new V6Invoice84().LoadAd(sttRec);
                        break;
                    case "IXB":
                        data = new V6Invoice85().LoadAd(sttRec);
                        break;

                    default:
                        data = null;
                        break;
                }

                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }
    }
}
