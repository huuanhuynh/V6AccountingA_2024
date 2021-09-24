using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_IND : XuLyBase
    {
        public AAPPR_IND(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F4: {0}, F9: {1}, F10: {2}, F8: {3}, F7: {4}.", V6Text.Text("BoSungThongTin"), V6Text.Text("XULYCT"), V6Text.Text("XULYBOSUNG"), V6Text.Text("HUYXULY"), V6Text.Text("Print"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
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

                                var hoaDonForm = new AAPPR_IND_F4(selectedSttRec, am.Rows[0]);
                                hoaDonForm.txtSoCtXuat.Enabled = false;
                                hoaDonForm.txtGhiChu01.Enabled = false;
                                
                                

                                hoaDonForm.UpdateSuccessEvent += data =>
                                {
                                    //currentRow.Cells["GHI_CHU01"].Value = data["GHI_CHU01"];
                                    //currentRow.Cells["GHI_CHU02"].Value = data["GHI_CHU02"];

                                    //currentRow.Cells["SO_CTX"].Value = data["SO_CTX"];
                                    
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
        
        protected override void XuLyF7()
        {
            try
            {
                if (string.IsNullOrEmpty(_ma_xuly))
                {
                    ShowMainMessage(V6Text.Text("MAXULYNULL"));
                    return;
                }
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                var oldKeys = FilterControl.GetFilterParameters();
                if (MenuButton.UseXtraReport != shift_is_down)
                {
                    var c = new ReportR44_DX(ItemID, _program + "F7", _program + "F7",
                        _reportFile, _reportCaption, _reportCaption2, _reportFileF5, _reportTitleF5, _reportTitle2F5);
                    IDictionary<string, object> filterData = new SortedDictionary<string, object>();
                    filterData["MA_XULY"] = _ma_xuly;
                    c.FilterControl.SetData(filterData);
                    c.AutoClickNhan = true;
                    c.ShowToForm(this, _reportCaption, true, true);
                }
                else
                {
                    var c = new ReportR44ViewBase(ItemID, _program + "F7", _program + "F7",
                        _reportFile, _reportCaption, _reportCaption2, _reportFileF5, _reportTitleF5, _reportTitle2F5);
                    IDictionary<string, object> filterData = new SortedDictionary<string, object>();
                    filterData["MA_XULY"] = _ma_xuly;
                    c.FilterControl.SetData(filterData);
                    c.AutoClickNhan = true;
                    c.ShowToForm(this, _reportCaption, true, true);
                }
                

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".F7", ex);
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
                            new SqlParameter("@Set_ma_xuly", ""),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "F8", plist);
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

                V6ControlFormHelper.ShowMainMessage("F8 " + V6Text.Finish);
            }
        }
        #endregion xulyF8


        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string _ma_xuly = "";
        public Dictionary<string, string> varDic = new Dictionary<string, string>(); 
        protected override void XuLyF9()
        {
            try
            {
                //Duyet qua gridview lay thong tin AM cho vao data
                //Tao form. dynamic??
                //form them danh muc dynamic alxuly
                SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                string var1 = "";
                string stt_rec = "", ma_ct = "";
                DateTime? var2 = null;
                DataGridViewRow lastRow = null;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if(!row.IsSelect()) continue;
                    lastRow = row;
                    var rData = row.ToDataDictionary();
                    var1 += "," + rData["SO_CT"].ToString().Trim();
                    var2 = ObjectAndString.ObjectToDate(rData["NGAY_CT"]);
                }

                if (var1 == "") return;

                if (lastRow != null)
                {
                    var lastRowData = lastRow.ToDataDictionary();
                    stt_rec = lastRowData["STT_REC"].ToString().Trim();
                    ma_ct = lastRowData["MA_CT"].ToString().Trim();
                }

                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("STT_REC", stt_rec);
                var detailData = V6BusinessHelper.Select(ma_ct == "IND" ? invoice74.AD_TableName : invoice85.AD_TableName, key, "top 1 *").Data;
                if (detailData.Rows.Count == 0)
                {
                    return;
                }
                var detailDic = detailData.Rows[0].ToDataDictionary();
                

                if (detailDic.ContainsKey("MA_VT"))
                {
                    data["MA_TD1"] = detailDic["MA_VT"].ToString().Trim();
                    
                }
                if (detailDic.ContainsKey("SO_LUONG"))
                    data["SL_TD1"] = detailDic["SO_LUONG"];
                foreach (KeyValuePair<string, string> item in varDic)
                {
                    if (detailDic.ContainsKey(item.Value))
                    {
                        data[item.Key] = detailDic[item.Value];
                    }
                }

                if (var1.Length > 0) var1 = var1.Substring(1);

                data["S1"] = var1;
                data["S7"] = var2;
                data["MA_CT"] = ma_ct;
                data["MA_CT_ME"] = ma_ct;
                data["STATUS"] = "1";
                
                All_Objects["data"] = data;
                All_Objects["dataGridView1"] = dataGridView1;
                All_Objects["dataGridView2"] = dataGridView2;
                All_Objects["detailData"] = detailData;
                All_Objects["lastRowData"] = lastRow.ToDataDictionary();
                InvokeFormEvent(FormDynamicEvent.F9);
                
                var f = new FormAddEdit("Alxuly", V6Mode.Add, null, data);
                f.AfterInitControl += f_AfterInitControl;
                f.InitFormControl(this);
                f.InsertSuccessEvent += f_InsertSuccessEvent;
                f.ShowDialog(this);

                //Nhan Insert => Alxuly

                //Timer tF9 = new Timer();
                //tF9.Interval = 500;
                //tF9.Tick += tF9_Tick;
                //Thread t = new Thread(F9Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //CheckForIllegalCrossThreadCalls = false;
                //remove_list_g = new List<DataGridViewRow>();
                //t.IsBackground = true;
                //t.Start();
                //tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        void f_InsertSuccessEvent(IDictionary<string, object> dataDic)
        {
            _ma_xuly = dataDic["MA_XULY"].ToString().Trim();
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
            //F9Thread();
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
                            new SqlParameter("@Set_ma_xuly", _ma_xuly),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "F9", plist);
                        //remove_list_g.Add(row);
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

                SetStatusText("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        protected override void XuLyF10()
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    ShowMainMessage(V6Text.NoSelection);
                    return;
                }
                var data = dataGridView1.CurrentRow.ToDataDictionary();
                var stt_rec = data["STT_REC"].ToString().Trim();
                var f10_Form = new AAPPR_IND_F10(stt_rec, data);
                if (f10_Form.ShowToForm(this, "title", false, true, false) == DialogResult.OK)
                {
                    _ma_xuly = f10_Form.MA_XULY;
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".F10", ex);
            }
        }

        V6Invoice74 invoice74 = new V6Invoice74();
        V6Invoice85 invoice85 = new V6Invoice85();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                
                var data = invoice74.LoadAD("");


                if (row.Cells["MA_CT"].Value.ToString() == "IND")
                {
                    data = invoice74.LoadAD(sttRec);
                }
                if (row.Cells["MA_CT"].Value.ToString() == "IXB")
                {
                    data = invoice85.LoadAD(sttRec);
                }
                
                dataGridView2.DataSource = data;
                dataGridView2.HideColumns("STT_REC", "UID", "GIA0","MA_CT","NGAY_CT");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_IND ViewDetails: " + ex.Message);
            }
        }
    }
}
