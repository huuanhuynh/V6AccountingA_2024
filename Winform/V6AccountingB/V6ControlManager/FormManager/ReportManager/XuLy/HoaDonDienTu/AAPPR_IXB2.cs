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
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    /// <summary>
    /// Chuyển sang hóa đơn điện tử.
    /// </summary>
    public class AAPPR_IXB2 : XuLyBase
    {
		V6Invoice85 invoice = new V6Invoice85();
		
        public AAPPR_IXB2(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit2();
            PostManager.ResetWS();
        }

        private void AAPPR_IXB2_Load(object sender, EventArgs e)
        {
            
        }

        private void MyInit2()
        {
            try
            {
                dataGridView1.Control_S = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit2", ex);
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "F4: Tạo HĐĐT trắng, F6: Sửa HĐĐT, F9: Chuyển.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        private Button btnPrintF7;

        
        
        #region ==== Xử lý F9 ====

        private string Key_Down = "";
        protected override void XuLyBoSungThongTinChungTuF4()
        {
            if (FilterControl.String1 == "1" || FilterControl.String1 == "3" || FilterControl.String1 == "5")
            {
                Key_Down = "F4";
                XuLyF9();
            }
        }

        protected override void XuLyF6()
        {
            if (FilterControl.String1 == "1" || FilterControl.String1 == "3" || FilterControl.String1 == "5")
            {
                Key_Down = "F6";
                XuLyF9();
            }
        }

        private bool f9Running;
        private string f9Error = "";
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
                        string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                        string serial = row.Cells["SO_SERI"].Value.ToString().Trim();

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
                            //Mode = "M", // tạo mới test luôn M
                            Branch = FilterControl.String1,
                            Dir = dir,
                            FileName = file,
                            Key_Down = Key_Down,
                            RptFileFull = ReportFileFull,
                            Fkey_hd = fkey_hd,
                            Pattern = pattern,
                            Serial = serial,
                            Form = this,
                        };
                        result = PostManager.PowerPost(paras);

                        if (paras.Result.IsSuccess(paras.Mode))
                        {
                            f9MessageAll += string.Format("\n{4} Soct:{0}, sohd:{1}, id:{2}\nResult:{3}",
                                soct, paras.Result.InvoiceNo, paras.Result.Id, result.Left(100), V6Text.Text("ThanhCong"));
                            //[AAPPR_IXB2_UPDATE]
                            SqlParameter[] plist2 =
                            {
                                new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                                new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                                new SqlParameter("@Set_so_ct", paras.Result.InvoiceNo),
                                new SqlParameter("@Set_fkey_hd", paras.Result.Id),
                                new SqlParameter("@MA_TD1", FilterControl.String1),
                                new SqlParameter("@Partner_infors", paras.Result.PartnerInfors),
                                new SqlParameter("@Key_down", paras.Key_Down),
                                new SqlParameter("@User_ID", V6Login.UserId)
                            };
                            V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "_UPDATE", plist2);
                            //row[]
                        }
                        else
                        {
                            f9MessageAll += string.Format("\n{3} Soct:{0}, error:{1}\nResult:{2}", soct, paras.Result.ResultErrorMessage, result, V6Text.Text("COLOI"));
                        }
                        
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
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
                Key_Down = "";
                RemoveGridViewRow();
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText(f9MessageAll);
                V6ControlFormHelper.ShowMainMessage( f9MessageAll);
                this.ShowMessage(V6Text.End + f9MessageAll, 300);
            }
        }
        #endregion xulyF9

        protected override void XuLyF10()
        {
            try
            {
                // Test search thong tin.
                var row = dataGridView1.CurrentRow;
                if (row == null) return;

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

                DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F10", plist);

                string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                string serial = row.Cells["SO_SERI"].Value.ToString().Trim();
                string invoiceNo = serial + row.Cells["SO_CT"].Value.ToString().Trim();
                DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["NGAY_CT"].Value);

                var paras = new PostManagerParams
                {
                    DataSet = ds,
                    Mode = "TestView",
                    Branch = FilterControl.String1,
                    //Dir = dir,
                    //FileName = file,
                    //Key_Down = "TestView",
                    //RptFileFull = ReportFileFull,
                    //Fkey_hd = fkey_hd,
                    Pattern = pattern,
                    InvoiceNo = invoiceNo,
                    InvoiceDate = ngay_ct,
                };
                var result = PostManager.SearchInvoice(paras);
                this.ShowInfoMessage(result.Left(200));
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF10", ex);
            }
        }

        private void btnTestViewXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.CurrentRow == null)
                {
                    return;
                }

                var row = dataGridView1.CurrentRow;

                //string mode = row.Cells["Kieu_in"].Value.ToString();
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

                DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9", plist);
                //DataTable data0 = ds.Tables[0];
                string result = "";//, error = "", sohoadon = "", id = "";
                var paras = new PostManagerParams
                {
                    DataSet = ds,
                    Mode = "TestView",
                    Branch = FilterControl.String1,
                    Dir = dir,
                    FileName = file,
                    Key_Down = "TestView",
                    RptFileFull = ReportFileFull,
                    Fkey_hd = fkey_hd,
                    Form = this,
                };
                result = PostManager.PowerPost(paras);
                Clipboard.SetText(result);
                //this.ShowMessage(result);
                AAPPR_SOA2_ViewXml viewer = new AAPPR_SOA2_ViewXml(result); // Xài chung với SOA
                viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestViewXml_Click", ex);
            }
        }

        private void btnPrintF7_Click(object sender, EventArgs e)
        {

        }

        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
                dataGridView2.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_IXB2 ViewDetails: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.btnTestViewXml = new System.Windows.Forms.Button();
            this.btnPrintF7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestViewXml
            // 
            this.btnTestViewXml.Location = new System.Drawing.Point(190, 30);
            this.btnTestViewXml.Name = "btnTestViewXml";
            this.btnTestViewXml.Size = new System.Drawing.Size(55, 23);
            this.btnTestViewXml.TabIndex = 23;
            this.btnTestViewXml.Text = "XML";
            this.btnTestViewXml.UseVisualStyleBackColor = true;
            this.btnTestViewXml.Click += new System.EventHandler(this.btnTestViewXml_Click);
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
            // AAPPR_IXB2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnPrintF7);
            this.Controls.Add(this.btnTestViewXml);
            this.Name = "AAPPR_IXB2";
            this.Load += new System.EventHandler(this.AAPPR_IXB2_Load);
            this.Controls.SetChildIndex(this.btnSuaTTMauBC, 0);
            this.Controls.SetChildIndex(this.btnThemMauBC, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnTestViewXml, 0);
            this.Controls.SetChildIndex(this.btnPrintF7, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button btnTestViewXml;

    }

    //internal class ConfigLine
    //{
    //    public string Field { get; set; }
    //    public string Value { get; set; }
    //    public string FieldV6 { get; set; }
    //    /// <summary>
    //    /// <para>Field -> lấy từ dữ liệu theo field.</para>
    //    /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
    //    /// </summary>
    //    public string Type { get; set; }
    //    public string DataType { get; set; }
    //}
}
