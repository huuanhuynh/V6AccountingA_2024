﻿using System;
using System.Collections.Generic;
using System.Threading;
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
    public class AGLTH1T : XuLyBase
    {
        public AGLTH1T(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F9: {0}, F10: {1}", V6Text.Text("INTUNGTRANG"), V6Text.Text("INLIENTUC")));
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        #region ==== Xử lý F9 ====


        private bool InLienTuc;
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
                //t.SetApartmentState(ApartmentState.STA);
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

                        var tk = (row.Cells["tk"].Value ?? "").ToString().Trim();

                        var oldKeys = FilterControl.GetFilterParameters();
                        var _reportFileF5 = "AGLTH1TF5";
                        var _reportTitleF5 = "SỔ TỔNG HỢP CHỮ T CỦA MỘT TÀI KHOẢN";
                        var _reportTitle2F5 = "General ledger";

                        var view = new ReportRViewBase(m_itemId, _program + "F5", _program + "F5",_reportFileF5,
                            _reportTitleF5, _reportTitle2F5, "", "", "");
                        
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
                            f.Dispose();
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
                var _reportFileF5 = "AGLTH1TF10";
                var _reportTitleF5 = "SỔ TỔNG HỢP CHỮ T CỦA MỘT TÀI KHOẢN";
                var _reportTitle2F5 = "General ledger";

              
                var view = new ReportRViewBase(m_itemId, _program + "F10", _program + "F10", _reportFileF5,
                    _reportTitleF5, _reportTitle2F5, "", "", "");

               

                view.CodeForm = CodeForm;
                view.Advance = FilterControl.Advance;
                view.FilterControl.String1 = FilterControl.String1;
                view.FilterControl.String2 = FilterControl.String2;

                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = oldKeys;

                view.FilterControl.SetParentAllRow(dataGridView1);
               
                //view.AutoPrint = FilterControl.Check1;
                view.AutoPrint = InLienTuc;

                view.PrinterName = _PrinterName;
                view.PrintCopies = _PrintCopies;

                var f = new V6Form();
                f.WindowState = FormWindowState.Maximized;
                f.Controls.Add(view);
                view.Disposed += delegate
                {
                    f.Dispose();
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
    }
}