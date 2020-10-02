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
    public class AAPPR_EINVOICE1 : XuLyBase
    {
        public AAPPR_EINVOICE1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F6: {0}, F9: {1}", V6Text.Text("THAYTHECT"), V6Text.Text("XULYCT")));
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
                //AAPPR_EINVOICE1_F9 form = new AAPPR_EINVOICE1_F9();
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

            var form = new AAPPR_EINVOICE1_F9();
            if (form.ShowDialog(this) != DialogResult.OK || form.SelectedMode == "E_T1") // thay thế dùng F6
            {
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
                            AM_new = row_data,
                        };
                        result = PostManager.PowerPost(paras);

                        if (paras.Result.IsSuccess(mode))
                        {
                            f9MessageAll += string.Format("\n{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct, paras.Result.InvoiceNo, paras.Result.Id, paras.Result.ResultString, V6Text.Text("ThanhCong"));
                            
                            SqlParameter[] plist2 =
                            {
                                new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                                new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                                new SqlParameter("@Action", paras.Mode),
                                new SqlParameter("@fkey_hd", paras.Result.Id),
                                new SqlParameter("@V6PARTNER_ID", paras.Result.Id),
                                new SqlParameter("@FKEY_HD_TT", paras.Fkey_hd_tt),
                                new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                                new SqlParameter("@Partner_infors", paras.Result.PartnerInfors),
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
                string message = "F9 " + V6Text.Finish + " " + (f9ErrorAll.Length > 0 ? "Error: " : "") + f9ErrorAll;
                V6ControlFormHelper.SetStatusText(message);
                V6ControlFormHelper.ShowMainMessage(message);
                this.ShowMessage("F9 " + V6Text.Finish + " " + f9MessageAll, 300);
            }
        }
        #endregion xulyF9

        protected override void XuLyF6()
        {
            try
            {
                f9Error = "";
                f9ErrorAll = "";
                f9MessageAll = "";
                if (dataGridView1.CurrentRow == null) return;

                var form = new AAPPR_EINVOICE1_F6();
                if (form.ShowDialog(this) != DialogResult.OK || form.SelectedGridViewRow == null)
                {
                    return;
                }

                DataGridViewRow row = dataGridView1.CurrentRow;
                var am_new = row.ToDataDictionary();

                try
                {
                    // E_G1: gạch nợ    E_H1: hủy hóa đơn   E_S1: sửa hd    E_T1: thay thế hd
                    //string mode = form.SelectedMode;
                    IDictionary<string, object> am_old = form.SelectedGridViewRow.ToDataDictionary();
                    string soct = row.Cells["So_ct"].Value.ToString().Trim();
                    string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                    string fkey_hd_tt = am_old["FKEY_HD_TT"].ToString().Trim();
                    if (string.IsNullOrEmpty(fkey_hd_tt))
                    {
                        f9ErrorAll = "Không có mã FKEY_HD_TT.";
                        return;
                    }

                    string info = string.Format("Thay thế HDDT [{0}] = {1} bằng hóa đơn mới [{2}] = {3}",
                        am_old["SO_CT"], am_old["T_TIEN2"], am_new["SO_CT"], am_new["T_TIEN2"]);
                    this.ShowMainMessage(info);

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                        new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                        new SqlParameter("@HoaDonMau", "0"),
                        new SqlParameter("@isInvoice", "1"),
                        new SqlParameter("@ReportFile", ""),
                        new SqlParameter("@MA_TD1", FilterControl.String1),
                        new SqlParameter("@UserID", V6Login.UserId)
                    };

                    DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist);
                    //DataTable data0 = ds.Tables[0];
                    string result = ""; //, error = "", sohoadon = "", id = "";
                    var paras = new PostManagerParams
                    {
                        DataSet = ds,
                        AM_old = am_old,
                        AM_new = am_new,
                        Mode = "E_T1",
                        Branch = FilterControl.String1,
                        Fkey_hd = fkey_hd,
                        Fkey_hd_tt = fkey_hd_tt, // "[01GTKT0/003]_[AA/17E]_[0000105]",
                    };

                    result = PostManager.PowerPost(paras);

                    if (paras.Result.IsSuccess(paras.Mode))     // Phải phân biệt 2 loại thành công.
                    {
                        if (paras.Result.ResultMessage != null && paras.Result.ResultMessage.Contains("Đã tồn tại Hóa đơn"))
                        {
                            f9MessageAll += string.Format("{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct,
                             paras.Result.InvoiceNo, paras.Result.Id, paras.Result.ResultString,
                             V6Text.Exist);
                        }
                        else
                        {
                            f9MessageAll += string.Format("{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct,
                                paras.Result.InvoiceNo, paras.Result.Id, paras.Result.ResultString,
                                V6Text.Text("ThanhCong"));
                        }

                        SqlParameter[] plist2 =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Action", paras.Mode),
                            new SqlParameter("@fkey_hd", paras.Result.Id),
                            new SqlParameter("@V6PARTNER_ID", paras.Result.Id),
                            new SqlParameter("@FKEY_HD_TT", paras.Fkey_hd_tt),
                            new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                            new SqlParameter("@Partner_infors", paras.Result.PartnerInfors),
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
                    f9ErrorAll += ex.Message;
                }

                // Thông báo hoàn thành:
                this.ShowMessage(f9MessageAll, 300);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF6", ex);
            }
        }

        protected override void XuLyF10()
        {
            bool ctrl_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
            string result = "";//, error = "", sohoadon = "", id = "";
            try
            {
                DataGridViewRow crow = dataGridView1.CurrentRow;
                if (crow == null) return;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Stt_rec", (crow.Cells["Stt_rec"].Value ?? "").ToString()),
                    new SqlParameter("@Ma_ct", (crow.Cells["Ma_ct"].Value ?? "").ToString()),
                    new SqlParameter("@HoaDonMau","0"),
                    new SqlParameter("@isInvoice","1"),
                    new SqlParameter("@ReportFile",""),
                    new SqlParameter("@MA_TD1", FilterControl.String1),
                    new SqlParameter("@UserID", V6Login.UserId)
                };

                DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist);
                var paras = new PostManagerParams
                {
                    DataSet = ds,
                    Mode = "CheckConnection",
                    Branch = FilterControl.String1,
                };
                PostManager.PowerCheckConnection(paras, out result);
                if (!string.IsNullOrEmpty(result))
                {
                    this.ShowInfoMessage(result);
                    return;
                }

                if (ctrl_is_down)
                {
                    if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                result += "\r\n " + UpdateCusVnpt(row.Cells["MA_KH"].Value.ToString());
                            }
                        }
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.Wrong);
                    }
                }
                else
                {
                    result += "\r\n " + UpdateCusVnpt(crow.Cells["MA_KH"].Value.ToString());
                }

                if (result != null && result.Length > 1) result = result.Substring(2);
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF10", ex);
            }
            this.ShowInfoMessage(result);
        }

        public string UpdateCusVnpt(string makh)
        {
            string result = "";//, error = "", sohoadon = "", id = "";
            try
            {
                if (string.IsNullOrEmpty(makh) || makh.Trim() == string.Empty)
                {
                    return null;
                }
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_KH", makh);
                DataTable data = V6BusinessHelper.Select("ALKH", keys, "*").Data;
                result += PostManager.DoUpdateCus(data);
            }
            catch (Exception ex)
            {
                result += ex.Message;
                this.ShowErrorException(GetType() + ".XuLyF10", ex);
            }
            return result;
        }


        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
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
    }
}
