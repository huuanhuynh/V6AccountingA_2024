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
using V6Structs;
using V6ThuePostManager;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_EINVOICE2 : XuLyBase
    {
        public AAPPR_EINVOICE2(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            dataGridView1.Control_S = true;
            PostManager.ResetWS();
        }

        private void AAPPR_EINVOICE2_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F4: {0}, F5: {1}, F6: {2}, F9: {3}", V6Text.Text("DIEUCHINHTIEN"), V6Text.Text("DIEUCHINHTT"), V6Text.Text("THAYTHECT"), V6Text.Text("XULYCT"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }


        private void btnPrintF7_Click(object sender, EventArgs e)
        {

        }

        
        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private Button btnPrintF7;
        private string f9MessageAll = "";
        
        protected override void XuLyF9()
        {
            try
            {
                //AAPPR_EINVOICE2_F9 form = new AAPPR_EINVOICE2_F9();
                //if (form.ShowDialog(this) != DialogResult.OK)
                //{
                //    return;
                //}
                //TxtMa_bp_Text = form.TxtMa_bp.Text.Trim();
                //TxtMa_nvien_Text = form.TxtMa_nvien.Text.Trim();
                //if (TxtMa_bp_Text == "" && TxtMa_nvien_Text == "")
                //{
                //    return;
                //}

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

            var form = new AAPPR_EINVOICE1_F9(); // Xài chung.
            if (form.ShowDialog(this) != DialogResult.OK || form.SelectedMode == "E_T1") // thay thế dùng F6
            {
                f9Running = false;
                return;
            }

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                var row_data = row.ToDataDictionary();
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        // E_G1: gạch nợ    E_H1: hủy hóa đơn   E_S1: sửa hd    E_T1: thay thế hd
                        string mode = form.SelectedMode;
                        string soct = row.Cells["So_ct"].Value.ToString().Trim();
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

                        DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist);
                        //DataTable data0 = ds.Tables[0];
                        string result = "";//, error = "", sohoadon = "", id = "";
                        var paras = new PostManagerParams
                        {
                            DataSet = ds,
                            Mode = mode,
                            Branch = FilterControl.String1,
                            Fkey_hd = fkey_hd,// Cần thiết cho Hủy VNPT BKAV
                            V6PartnerID = row.Cells["V6PARTNER_ID"].Value.ToString().Trim(),
                            AM_data = row_data,
                        };
                        result = PostManager.PowerPost(paras);

                        if (paras.Result.IsSuccess(mode))
                        {
                            f9MessageAll += string.Format("\n{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}",
                                soct, paras.Result.InvoiceNo, paras.Result.Id, paras.Result.ResultString.Left(100), V6Text.Text("ThanhCong"));
                            
                            SqlParameter[] plist2 =
                            {
                                new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                                new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                                new SqlParameter("@Action", paras.Mode),
                                new SqlParameter("@fkey_hd", paras.Result.Id),
                                new SqlParameter("@V6PARTNER_ID", paras.Result.Id),
                                new SqlParameter("@FKEY_HD_TT", paras.Fkey_hd_tt),
                                new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                                new SqlParameter("@Partner_infors", paras.Result.Return_PartnerInfors),
                                new SqlParameter("@MA_TD1", FilterControl.String1),
                                new SqlParameter("@User_ID", V6Login.UserId)
                            };
                            V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "_UPDATE", plist2);
                        }
                        else
                        {
                            f9MessageAll += string.Format("\n{3} Soct:{0}, error:{1}\nResult:{2}", soct, paras.Result.ResultErrorMessage, paras.Result.ResultString, V6Text.Text("COLOI"));
                        }
                        
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
                V6ControlFormHelper.SetStatusText("F9 running " + _message
                    + (cError.Length > 0 ? " Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();

                btnNhan.PerformClick();
                string message = (f9ErrorAll.Length > 0 ? "Error: " : "") + f9ErrorAll;
                V6ControlFormHelper.SetStatusText(message);
                V6ControlFormHelper.ShowMainMessage(message);
                this.ShowMessage(V6Text.End + f9MessageAll, 300);
            }
        }
        #endregion xulyF9


        protected override void XuLyBoSungThongTinChungTuF4()
        {
            XuLy_ThayThe_DieuChinh("E_S1"); // Điều chỉnh tiền, BKAV _121_CreateAdjust
        }

        protected override void XuLyXemChiTietF5()
        {
            XuLy_ThayThe_DieuChinh("E_S2"); // Điều chỉnh thông tin
        }

        protected override void XuLyF6()
        {
            XuLy_ThayThe_DieuChinh("E_T1");
        }

        void XuLy_ThayThe_DieuChinh(string mode)
        {
            // Lấy hóa đơn từ form F6 thay cho dòng đang đứng.
            try
            {
                f9Error = "";
                f9MessageAll = "";
                if (dataGridView1.CurrentRow == null)
                {
                    this.ShowWarningMessage(V6Text.NoData);
                    return;
                }


                var form = new AAPPR_EINVOICE1_F6();
                if (mode == "E_T1") form.Text += " (Thay thế)";
                else if (mode == "E_S1") form.Text += " (Điều chỉnh tiền)";
                else if (mode == "E_S2") form.Text += " (Điều chỉnh thông tin)";
                if (form.ShowDialog(this) != DialogResult.OK || form.SelectedGridViewRow == null)
                {
                    if (form.SelectedGridViewRow == null) this.ShowWarningMessage(V6Text.NoData);
                    return;
                }

                DataGridViewRow row = dataGridView1.CurrentRow;
                var am_OLD = row.ToDataDictionary();

                try
                {
                    // E_G1: gạch nợ    E_H1: hủy hóa đơn   E_S1: sửa hd    E_T1: thay thế hd
                    //string mode = form.SelectedMode;
                    IDictionary<string, object> am_F6 = form.SelectedGridViewRow.ToDataDictionary();
                    string soct = am_F6["SO_CT"].ToString().Trim();
                    string fkey_hd = am_F6["FKEY_HD"].ToString().Trim();
                    string fkey_hd_tt = am_OLD["FKEY_HD_TT"].ToString().Trim();
                    string STT_REC_OLD = am_OLD["STT_REC"].ToString().Trim();
                    string part_infos = am_OLD["PART_INFOS"].ToString().Trim();
                    if (string.IsNullOrEmpty(fkey_hd_tt))
                    {
                        f9MessageAll = "Không có mã FKEY_HD_TT.";
                        return;
                    }
                    if (string.IsNullOrEmpty(part_infos))
                    {
                        f9MessageAll = "Không có mã part_infos.";
                        return;
                    }

                    string info = "";
                    if (mode == "E_T1") info = string.Format("Thay thế HDDT [{0}] = {1} bằng hóa đơn mới [{2}] = {3}",
                        am_OLD["SO_CT"], am_OLD["T_TIEN2"], am_F6["SO_CT"], am_F6["T_TIEN2"]);
                    else if (mode == "E_S1") info = string.Format("Điều chỉnh tiền HDDT [{0}] = {1} bằng hóa đơn mới [{2}] = {3}",
                        am_OLD["SO_CT"], am_OLD["T_TIEN2"], am_F6["SO_CT"], am_F6["T_TIEN2"]);
                    else if (mode == "E_S2") info = string.Format("Điều chỉnh thông tin HDDT [{0}] = {1} bằng hóa đơn mới [{2}] = {3}",
                        am_OLD["SO_CT"], am_OLD["T_TIEN2"], am_F6["SO_CT"], am_F6["T_TIEN2"]);
                    this.ShowMainMessage(info);

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Stt_rec", am_F6["STT_REC"] + ""),
                        new SqlParameter("@Ma_ct", am_F6["MA_CT"] + ""),
                        new SqlParameter("@HoaDonMau", "0"),
                        new SqlParameter("@isInvoice", "1"),
                        new SqlParameter("@ReportFile", ""),
                        new SqlParameter("@MA_TD1", FilterControl.String1),
                        new SqlParameter("@UserID", V6Login.UserId)
                    };

                    DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist);
                    //DataTable data0 = ds.Tables[0];
                    var paras = new PostManagerParams
                    {
                        DataSet = ds,
                        AM_old = am_OLD,
                        AM_data = am_F6,
                        Mode = mode,
                        Branch = FilterControl.String1,
                        Fkey_hd = fkey_hd,
                        Fkey_hd_tt = fkey_hd_tt, // "[01GTKT0/003]_[AA/17E]_[0000105]",
                        Saved_Partner_infos = part_infos,
                    };

                    string result = PostManager.PowerPost(paras);

                    if (paras.Result.IsSuccess(paras.Mode))     // Phải phân biệt 2 loại thành công.
                    {
                        if (paras.Result.ResultMessage != null && paras.Result.ResultMessage.Contains("Đã tồn tại Hóa đơn"))
                        {
                            f9MessageAll += string.Format("{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct,
                             paras.Result.InvoiceNo, paras.Result.Id, result.Left(100), V6Text.Exist);
                        }
                        else
                        {
                            f9MessageAll += string.Format("{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct,
                                paras.Result.InvoiceNo, paras.Result.Id, result.Left(100), V6Text.Text("ThanhCong"));
                        }

                        paras.Result.STT_REC_TT = STT_REC_OLD;

                        SqlParameter[] plist2 =
                        {
                            new SqlParameter("@Stt_rec", am_F6["STT_REC"].ToString().Trim()),
                            new SqlParameter("@Ma_ct", am_F6["MA_CT"].ToString().Trim()),
                            new SqlParameter("@Action", paras.Mode),
                            new SqlParameter("@fkey_hd", paras.Result.Id),
                            new SqlParameter("@V6PARTNER_ID", paras.Result.Id),
                            new SqlParameter("@FKEY_HD_TT", paras.Fkey_hd_tt),
                            new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                            new SqlParameter("@Partner_infors", paras.Result.Return_PartnerInfors),
                            new SqlParameter("@MA_TD1", FilterControl.String1),
                            new SqlParameter("@User_ID", V6Login.UserId)
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "_UPDATE", plist2);
                    }
                    else
                    {
                        f9MessageAll += string.Format("\n{3} Soct:{0}, error:{1}\nResult:{2}", soct,
                            paras.Result.ResultErrorMessage, paras.Result.ResultString, V6Text.Text("COLOI"));
                    }

                    remove_list_g.Add(row);
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9MessageAll += ex.Message;
                }

                // Thông báo kết thúc:
                this.ShowMessage(V6Text.End + f9MessageAll, 300);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF6", ex);
            }
        }


        

        V6Invoice81 invoice81 = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice81.LoadAD(sttRec);
                dataGridView2.DataSource = data;
                dataGridView2.AutoGenerateColumns = true;
                _tbl2 = data;
                if (data == null)
                {
                    this.WriteToLog(GetType() + ".ViewDetails", "data is null.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.btnPrintF7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrintF7
            // 
            this.btnPrintF7.Location = new System.Drawing.Point(250, 30);
            this.btnPrintF7.Name = "btnPrintF7";
            this.btnPrintF7.Size = new System.Drawing.Size(55, 23);
            this.btnPrintF7.TabIndex = 26;
            this.btnPrintF7.Text = "Print";
            this.btnPrintF7.UseVisualStyleBackColor = true;
            this.btnPrintF7.Click += new System.EventHandler(this.btnPrintF7_Click);
            // 
            // AAPPR_EINVOICE2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnPrintF7);
            this.Name = "AAPPR_EINVOICE2";
            this.Load += new System.EventHandler(this.AAPPR_EINVOICE2_Load);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnSuaTTMauBC, 0);
            this.Controls.SetChildIndex(this.btnThemMauBC, 0);
            this.Controls.SetChildIndex(this.btnPrintF7, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
