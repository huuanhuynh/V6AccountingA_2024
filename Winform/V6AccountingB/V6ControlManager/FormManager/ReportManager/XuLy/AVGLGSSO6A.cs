using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AVGLGSSO6A : XuLyBase
    {
        public AVGLGSSO6A(string itemId, string program, string reportProcedure, string reportFile, string reportCaption,
            string reportCaption2, string repFileF5, string repTitleF5, string repTitle2F5)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false,
                repFileF5, repTitleF5, repTitle2F5)
        {

        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F5: {0}, F9: {1}", V6Text.Text("CHITIET"), V6Text.Text("INTUNGTRANG")));
            
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        #region ==== Xử lý F9 ====


        private bool InLienTuc = false;
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string _oldDefaultPrinter, _PrinterName;
        private int _PrintCopies;
        private bool printting;
        protected override void XuLyF9()
        {
            try
            {
                 if (this.ShowConfirmMessage(V6Text.Text("ASKINTUNGTRANG1")) != DialogResult.Yes)
                   {
                       return;
                   }
                InLienTuc = true;


                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                if (InLienTuc) // thì chọn máy in trước
                {
                    var printerst = V6ControlFormHelper.ChoosePrinter(this, _oldDefaultPrinter);
                    if (printerst != null)
                    {
                        _PrinterName = printerst.PrinterName;
                        _PrintCopies = printerst.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                    }
                    else
                    {
                        printting = false;
                    }
                }

                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                F9Thread();
                //Thread t = new Thread(F9Thread);
                //t.IsBackground = true;
                //t.Start();
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
                        //var so_ctgs = row.Cells["SO_LO"].Value.ToString();
                        var khoa_ctgs = (row.Cells["KHOA_CTGS"].Value ?? "").ToString().Trim();

                        var oldKeys = FilterControl.GetFilterParameters();
                        var reportFileF5 = "AVGLGSSO6AF5";
                        var reportTitleF5 = "CHỨNG TỪ GHI SỔ SỐ: ";
                        var reportTitle2F5 = "JOURNAL VOUCHER: ";

                        var view = new ReportRViewBase(m_itemId, _program + "F5", _program + "F5",reportFileF5,
                            reportTitleF5, reportTitle2F5, "", "", "");
                        
                        view.CodeForm = CodeForm;
                        //view.FilterControl.Call1(ma_kh);
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("KHOA_CTGS0", khoa_ctgs);
                        V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                        view.CodeForm = CodeForm;
                        view.Advance = FilterControl.Advance;
                        view.FilterControl.String1 = FilterControl.String1;
                        view.FilterControl.String2 = FilterControl.String2;

                        view.Dock = DockStyle.Fill;
                        view.FilterControl.InitFilters = oldKeys;

                        view.FilterControl.SetParentRow(row.ToDataDictionary());

                        //view.AutoPrint = FilterControl.Check1;
                        view.AutoPrint = InLienTuc;
                        
                        view.PrinterName = _PrinterName;
                        view.PrintCopies = _PrintCopies;

                        view.AutoClickNhan = true;
                        view.ShowToForm(this, "AVGLGSSO6A", true);
                        
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
                SetStatus2Text();
                //  btnNhan.PerformClick();
                
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        
        private bool f10Running;
        private string f10Message = "";
        private string f10ErrorAll = "";
        private string f10Error = "";

        protected override void XuLyF10()
        {
            try
            {

                if (this.ShowConfirmMessage(V6Text.Text("ASKINLIENTUC")) != DialogResult.Yes)
                {
                    return;
                }


                InLienTuc = false;

                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                
                if (InLienTuc) // thì chọn máy in trước
                {
                    var printerst = V6ControlFormHelper.ChoosePrinter(this, _oldDefaultPrinter);
                    if (printerst != null)
                    {
                        _PrinterName = printerst.PrinterName;
                        _PrintCopies = printerst.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                    }
                    else
                    {
                        printting = false;
                    }
                }
                remove_list_g = new List<DataGridViewRow>();
                Timer tF10 = new Timer();
                tF10.Interval = 500;
                tF10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                F10Thread();
                //Thread t = new Thread(F10Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                tF10.Start();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF10: " + ex.Message);
            }
        }

        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";
          
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsSelect())
                    {
                        remove_list_g.Add(row);
                    }
                }
                var oldKeys = FilterControl.GetFilterParameters();
                var _reportFileF5 = "AVGLGSSO6AF10";
                var _reportTitleF5 = "CHỨNG TỪ GHI SỔ";
                var _reportTitle2F5 = "JOURNAL VOUCHER ";

              
                //var view = new ReportRViewBase(m_itemId, _program + "F10", _program + "F10", _reportFileF5,
                //    _reportTitleF5, _reportTitle2F5, "", "", "");
                var view = new ReportRView2Base(m_itemId, _program + "F10", _program + "F10", _reportFileF5,
                    _reportTitleF5, _reportTitle2F5, "", "", "");

               

                view.CodeForm = CodeForm;
                view.Advance = FilterControl.Advance;
                view.FilterControl.String1 = FilterControl.String1;
                view.FilterControl.String2 = FilterControl.String2;

                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = oldKeys;

                view.FilterControl.SetParentAllRow(dataGridView1);
               
                //view.AutoPrint = InLienTuc;
                //view.PrinterName = _PrinterName;
                //view.PrintCopies = _PrintCopies;
                view.btnNhan_Click(null, null);
                view.ShowToForm(this, _reportTitleF5, true);

                //var f  = new V6Form();
                //f.WindowState = FormWindowState.Maximized;
                //f.Controls.Add(view);
                //view.Disposed += delegate
                //{
                //    f.Close();
                //};
                //view.btnNhan_Click(null, null);
                //f.ShowDialog(this);
                SetStatus2Text();
            }
            catch (Exception ex)
            {

                f10Error += ex.Message;
                f10ErrorAll += ex.Message;
            }


            f10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (f10Running)
            {
                var cError = f10Error;
                f10Error = f10Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F10 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                SetStatusText("F10 " + V6Text.Finish);
            }
        }
        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                //var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                //var data = invoice.LoadAd81(sttRec);
                //dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        private string so_ctx_temp;
        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        protected override void XuLyXemChiTietF5()
        {
            //base.XuLyXemChiTietF5();
            var oldKeys = FilterControl.GetFilterParameters();
            var view = new ReportRViewBase(m_itemId, _program + "F5", _program + "F5", _reportFile + "F5", _reportCaption, _reportCaption2, "", "", "")
            {
                CodeForm = CodeForm
            };
            view.CodeForm = CodeForm;
            view.Dock = DockStyle.Fill;
            view.FilterControl.InitFilters = oldKeys;
            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
            view.btnNhan_Click(null, null);
            view.ShowToForm(this, _reportCaption, true);
            //var f = new V6Form();
            //f.WindowState = FormWindowState.Maximized;
            //f.Controls.Add(view);
            //f.ShowDialog(this);
            SetStatus2Text();
        }
    }
}
