using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_XULY_ALL : XuLyBase
    {
        public AAPPR_XULY_ALL(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AAPPR_XULY_ALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AAPPR_XULY_ALL";
            this.Load += new System.EventHandler(this.AAPPR_XULY_ALL_Load);
            this.ResumeLayout(false);

        }

        private void AAPPR_XULY_ALL_Load(object sender, EventArgs e)
        {
            try
            {
                btnNhan.PerformClick();
                dataGridView1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Ghi chú, F9: Xử lý chứng từ, F8: Hủy xử lý.");
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

                                var fText = V6Text.Text("GCCT");
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                string extra_infor = "";
                                if (EXTRA_INFOR.ContainsKey("FIELDS_F4")) extra_infor = EXTRA_INFOR["FIELDS_F4"];
                                var hoaDonForm = new AAPPR_SOA_F4(selectedSttRec, am.Rows[0], amName, extra_infor);
                                hoaDonForm.txtSoCtXuat.Enabled = false;
                                hoaDonForm.txtGhiChu01.Enabled = false;
                                
                                //var so_ctx = hoaDonForm.So_ctx.Trim();
                                //if (so_ctx == "")
                                //{
                                //    if (so_ctx_temp != null)
                                //    {
                                //        try
                                //        {
                                //            var int_so_ctx = int.Parse(so_ctx_temp);
                                //            if (int_so_ctx > 0)
                                //            {
                                //                var s0 = "".PadRight(so_ctx_temp.Length, '0');
                                //                var s = (s0 + (int_so_ctx + 1)).Right(so_ctx_temp.Length);
                                //                hoaDonForm.So_ctx = s;
                                //            }
                                //        }
                                //        catch (Exception)
                                //        {
                                //            // ignored
                                //        }
                                //    }
                                //}

                                hoaDonForm.UpdateSuccessEvent += data =>
                                {
                                    currentRow.Cells["GHI_CHU01"].Value = data["GHI_CHU01"];
                                    currentRow.Cells["GHI_CHU02"].Value = data["GHI_CHU02"];

                                    currentRow.Cells["SO_CTX"].Value = data["SO_CTX"];

                                    foreach (KeyValuePair<string, string> item in hoaDonForm._fieldDic)
                                    {
                                        string FIELD = item.Key.ToUpper();
                                        if (hoaDonForm._allwayUpdate.ContainsKey(FIELD) || (data[FIELD] != null && data[FIELD].ToString().Trim() != ""))
                                        {
                                            currentRow.Cells[FIELD].Value = data[FIELD];
                                        }
                                    }
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
                        this.ShowWarningMessage(V6Text.EditDenied);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        
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
                            new SqlParameter("@Set_ma_xuly", FilterControl.Kieu_post),
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

                V6ControlFormHelper.ShowMainMessage("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                if (row != null)
                {
                    var ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["ngay_ct"].Value);
                    var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                    var ma_ct = row.Cells["Ma_ct"].Value.ToString().Trim();
                    DataTable data = null;

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@ngay_ct", ngay_ct.ToString("yyyyMMdd")),
                        new SqlParameter("@ma_ct", ma_ct),
                        new SqlParameter("@stt_rec", sttRec),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@advance", ""),
                    };
                    data = V6BusinessHelper.ExecuteProcedure("AAPPR_XULY_ALL_AD", plist).Tables[0];

                    //switch (ma_ct)
                    //{
                    //    case "SOA":
                    //        data = invoice.LoadAD(sttRec);
                    //        break;
                    //    case "SOB":
                    //        data = new V6Invoice82().LoadAD(sttRec);
                    //        break;
                    //    case "SOC":
                    //        data = new V6Invoice83().LoadAD(sttRec);
                    //        break;
                    //    case "SOR":
                    //        data = new V6Invoice93().LoadAD(sttRec);
                    //        break;
                    //    case "SOF":
                    //        data = new V6Invoice76().LoadAD(sttRec);
                    //        break;
                    //    case "CA1":
                    //        data = new V6Invoice51("CA1").LoadAD(sttRec);
                    //        break;
                    //    case "POA":
                    //        data = new V6Invoice71().LoadAD(sttRec);
                    //        break;
                    //    case "POB":
                    //        data = new V6Invoice72().LoadAD(sttRec);
                    //        break;
                    //    case "POC":
                    //        data = new V6Invoice73().LoadAD(sttRec);
                    //        break;
                    //    case "IND":
                    //        data = new V6Invoice74().LoadAD(sttRec);
                    //        break;
                    //    case "IXA":
                    //        data = new V6Invoice84().LoadAD(sttRec);
                    //        break;
                    //    case "IXB":
                    //        data = new V6Invoice85().LoadAD(sttRec);
                    //        break;
                    //    case "IXC":
                    //        data = new V6Invoice86().LoadAD(sttRec);
                    //        break;
                    //    case "AR1":
                    //        data = new V6Invoice21().LoadAD(sttRec);
                    //        break;
                    //    case "GL1":
                    //        data = new V6Invoice11("GL1").LoadAD(sttRec);
                    //        break;
                    //    case "AR9":
                    //        data = new V6Invoice11("AR9").LoadAD(sttRec);
                    //        break;
                    //    case "AP9":
                    //        data = new V6Invoice11("AP9").LoadAD(sttRec);
                    //        break;
                    //    case "AP1":
                    //        data = new V6Invoice31().LoadAD(sttRec);
                    //        break;
                    //    case "AP2":
                    //        data = new V6Invoice32().LoadAD(sttRec);
                    //        break;
                    //    case "BN1":
                    //        data = new V6Invoice51("BN1").LoadAD(sttRec);
                    //        break;
                    //    case "TA1":
                    //        data = new V6Invoice41("TA1").LoadAD(sttRec);
                    //        break;
                    //    case "BC1":
                    //        data = new V6Invoice41("BC1").LoadAD(sttRec);
                    //        break;
                    //    case "SOH":
                    //        data = new V6Invoice91().LoadAD(sttRec);
                    //        break;
                    //    case "POH":
                    //        data = new V6Invoice92().LoadAD(sttRec);
                    //        break;
                    //    case "INY":
                    //        data = new V6Invoice94INY().LoadAD(sttRec);
                    //        break;
                    //    case "IXY":
                    //        data = new V6Invoice95IXY().LoadAD(sttRec);
                    //        break;
                    //}
                    dataGridView2.AutoGenerateColumns = true;
                    dataGridView2.DataSource = data;
                }
                else
                {
                    dataGridView2.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        public override void FormatGridViewExtern()
        {
            try
            {
                //VPA_GetFormatGridView]@Codeform VARCHAR(50),@Type VARCHAR(20)
                string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
                object VALUEV;
                V6BusinessHelper.GetFormatGridView(_program, "REPORT", out FIELDV, out OPERV, out VALUEV, out BOLD_YN,
                    out COLOR_YN, out COLORV);
                //Color.MediumAquamarine
                V6ControlFormHelper.FormatGridView(dataGridView1, FIELDV, OPERV, VALUEV, BOLD_YN == "1", COLOR_YN == "1",
                    ObjectAndString.StringToColor(COLORV));

                DataGridViewCell cell = null;
                if (dataGridView1.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        var column = dataGridView1.Columns[i];
                        if (column.Visible)
                        {
                            cell = dataGridView1.Rows[0].Cells[i];
                            dataGridView1.CurrentCell = cell;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridViewExtern", ex);
            }
        }

        protected override void FormatGridView2()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;

                string mact = dataGridView1.CurrentRow.Cells["Ma_ct"].Value.ToString().Trim();
                var alct = V6BusinessHelper.GetAlct(mact);
                if (alct.Rows.Count > 0)
                {
                    var row = alct.Rows[0];
                    var showFields = row["GRDS_AD"].ToString().Trim();
                    var formatStrings = row["GRDF_AD"].ToString().Trim();
                    var headerString = row[V6Setting.IsVietnamese ? "GRDHV_AD" : "GRDHE_AD"].ToString().Trim();
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, showFields, formatStrings, headerString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView2", ex);
            }
        }
    }
}
