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
            V6ControlFormHelper.SetStatusText2(string.Format("F4: {0}, F9: {1}", V6Text.Text("BOSUNGTHONGTIN"), V6Text.Text("BOSUNGTTNCT")));
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
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        // E_G1: gạch nợ    E_H1: hủy hóa đơn   E_S1: sửa hd    E_T1: thay thế hd
                        string mode = form.SelectedMode;
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
                        string result = "";//, error = "", sohoadon = "", id = "";
                        var paras = new PostManagerParams
                        {
                            DataSet = ds,
                            Mode = mode,
                            Branch = FilterControl.String1,
                            Dir = dir,
                            FileName = file,
                            Fkey_hd = fkey_hd,
                        };
                        result = PostManager.PowerPost(paras);

                        if (paras.Result.IsSuccess(mode))
                        {
                            f9MessageAll += string.Format("\n{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}", soct, paras.Result.InvoiceNo, paras.Result.Id, paras.Result.ResultString, V6Text.Text("ThanhCong"));
                            
                            SqlParameter[] plist2 =
                            {
                                new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                                new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                                new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                                new SqlParameter("@Set_fkey_hd", paras.Result.Id),
                                new SqlParameter("@MA_TD1", FilterControl.String1),
                                new SqlParameter("@Partner_infors", paras.Result.PartnerInfors),
                                new SqlParameter("@User_ID", V6Login.UserId)
                            };
                            V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "_UPDATE", plist2);
                        }
                        else
                        {
                            f9MessageAll += string.Format("\n{3} Soct:{0}, error:{1}\nResult:{2}", soct, paras.Result.ResultError, paras.Result.ResultString, V6Text.Text("COLOI"));
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

                this.ShowMessage(V6Text.Finish+f9ErrorAll);
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                //V6ControlFormHelper.ShowMainMessage("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                this.ShowInfoMessage(V6Text.NotSupported);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
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
