using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINSO1T : XuLyBase
    {
        public AINSO1T(string itemId, string program, string reportProcedure, string reportFile, string reportCaption,
            string reportCaption2, string repFileF5, string repTitleF5, string repTitle2F5)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false,
                repFileF5, repTitleF5, repTitle2F5)
        {

        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F5: Chi tiết, F9: In từng trang, F10: In liên tục");
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
                
                 if (this.ShowConfirmMessage("Có chắc chắn in từng trang (trực tiếp ra máy in) không?") != DialogResult.Yes)
                   {
                       return;
                   }
                InLienTuc = true;


                _oldDefaultPrinter = V6Tools.PrinterStatus.GetDefaultPrinterName();

                PrintDialog p = new PrintDialog();
                p.AllowCurrentPage = false;
                p.AllowPrintToFile = false;
                p.AllowSelection = false;
                p.AllowSomePages = false;
                p.PrintToFile = false;
                p.UseEXDialog = true; //Fix win7
                
                if (InLienTuc) // thì chọn máy in trước
                {
                    DialogResult dr = p.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        _PrinterName = p.PrinterSettings.PrinterName;
                        _PrintCopies = p.PrinterSettings.Copies;
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
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                tF9.Start();
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
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

                        var ma_vt = (row.Cells["MA_VT"].Value ?? "").ToString().Trim();

                        var oldKeys = FilterControl.GetFilterParameters();
                        var _reportFileF5 = "AINSO1TF5";
                        var _reportTitleF5 = "SỔ CHI TIẾT VẬT TƯ";
                        var _reportTitle2F5 = "Item detail";

                        var view = new ReportRViewBase(m_itemId, _program + "F5", _program + "F5",_reportFileF5,
                            _reportTitleF5, _reportTitle2F5, "", "", "");
                        
                        view.CodeForm = CodeForm;
                        //view.FilterControl.Call1(ma_vt);
                        SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                        data.Add("MA_VT", ma_vt);
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

                        var f = new V6Form();
                        f.WindowState = FormWindowState.Maximized;
                        f.Controls.Add(view);
                        view.Disposed += delegate
                        {
                            f.Close();
                        };
                        view.btnNhan_Click(null, null);
                        f.ShowDialog(this);
                        SetStatus2Text();
                        
                        
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
                //  btnNhan.PerformClick();
                try
                {
                    V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
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

                if (this.ShowConfirmMessage("Có chắc chắn in liên tục không?") != DialogResult.Yes)
                {
                    return;
                }


                InLienTuc = false;

                _oldDefaultPrinter = V6Tools.PrinterStatus.GetDefaultPrinterName();

                PrintDialog p = new PrintDialog();
                p.AllowCurrentPage = false;
                p.AllowPrintToFile = false;
                p.AllowSelection = false;
                p.AllowSomePages = false;
                p.PrintToFile = false;
                p.UseEXDialog = true; //Fix win7

                if (InLienTuc) // thì chọn máy in trước
                {
                    DialogResult dr = p.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        _PrinterName = p.PrinterSettings.PrinterName;
                        _PrintCopies = p.PrinterSettings.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                    }
                    else
                    {
                        printting = false;
                    }
                }

                Timer tF10 = new Timer();
                tF10.Interval = 500;
                tF10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                Thread t = new Thread(F10Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
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
                var oldKeys = FilterControl.GetFilterParameters();
                var _reportFileF5 = "AINSO1F10";
                var _reportTitleF5 = "SỔ CHI TIẾT VẬT TƯ";
                var _reportTitle2F5 = "Item detail";

              
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

                var f = new V6Form();
                f.WindowState = FormWindowState.Maximized;
                f.Controls.Add(view);
                view.Disposed += delegate
                {
                    f.Close();
                };
                view.btnNhan_Click(null, null);
                f.ShowDialog(this);
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
                //  btnNhan.PerformClick();
                try
                {
                    V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F10 Xử lý xong!");
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
                this.ShowErrorMessage(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4:\n" + ex.Message);
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
            view.Advance = FilterControl.Advance;
            view.Dock = DockStyle.Fill;
            view.FilterControl.InitFilters = oldKeys;
            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
            view.FilterControl.String1 = FilterControl.String1;
            var f = new V6Form();
            f.WindowState = FormWindowState.Maximized;
            f.Controls.Add(view);
            view.btnNhan_Click(null, null);
            f.ShowDialog(this);
            SetStatus2Text();
        }
    }
}
