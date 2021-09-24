using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
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
    public class AAPPR_XL_LIST_ALL : XuLyBase
    {
        public AAPPR_XL_LIST_ALL(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            dataGridView1.Control_S = true;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AAPPR_XL_LIST_ALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AAPPR_XL_LIST_ALL";
            this.Load += new System.EventHandler(this.AAPPR_XL_LIST_ALL_Load);
            this.ResumeLayout(false);

        }

        private void AAPPR_XL_LIST_ALL_Load(object sender, EventArgs e)
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
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Setting.IsVietnamese ? "F3: Sửa, F5: Xem, F9: Xử lý chứng từ, F8: Hủy xử lý." : "F3: Edit, F5: View, F9: Processing, F8: Cancel processing.";
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
            _MA_DM = FilterControl.String1;
        }

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            //_MA_DM = FilterControl.String1;
            if (V6Login.UserRight.AllowEdit("", _MA_DM.ToUpper() + "6"))
            {
                DoEdit();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
            SetStatus2Text();
        }

        private string _MA_DM = null;
        private void DoEdit() // Copy từ danhmucview
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);
                    if (_MA_DM.ToUpper().StartsWith("CORPLAN"))
                    {
                        if (dataGridView1.Columns.Contains("ID"))
                            keys.Add("ID", row.Cells["ID"].Value);
                    }

                    //if (KeyFields != null)
                    //    foreach (var keyField in KeyFields)
                    //    {
                    //        if (dataGridView1.Columns.Contains(keyField))
                    //        {
                    //            keys[keyField] = row.Cells[keyField].Value;
                    //        }
                    //    }

                    //_data = row.ToDataDictionary();
                    var f = new FormAddEdit(_MA_DM, V6Mode.Edit, keys, null);
                    f.AfterInitControl += f_AfterInitControl;
                    //f.UpdateSuccessEvent += f_UpdateSuccess;
                    //f.CallReloadEvent +=delegate(object sender, EventArgs args) { btnNhan.PerformClick(); };
                    f.InitFormControl(this);
                    f.ShowDialog(this);

                    if (f.UpdateSuccess)
                    {
                        f_UpdateSuccess(f.Data);
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                //if (!string.IsNullOrEmpty(_aldmConfig.TABLE_VIEW)
                //    && V6BusinessHelper.IsExistDatabaseTable(_aldmConfig.TABLE_VIEW))
                //{
                //    ReLoad();
                //}
                //else
                {
                    if (data == null) return;
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                    V6ControlFormHelper.UpdateGridViewRow(row, data);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
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
            SetStatus2Text();
        }


        protected override void XuLyXemChiTietF5()
        {
            if (V6Login.UserRight.AllowView("", _MA_DM.ToUpper() + "6"))
            {
                DoView();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
            SetStatus2Text();
        }

        private void DoView()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);

                    //if (KeyFields != null)
                    //    foreach (var keyField in KeyFields)
                    //    {
                    //        if (dataGridView1.Columns.Contains(keyField))
                    //        {
                    //            keys[keyField] = row.Cells[keyField].Value;
                    //        }
                    //    }

                    //var _data = row.ToDataDictionary();
                    var f = new FormAddEdit(_MA_DM, V6Mode.View, keys, null);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _MA_DM, ex.Message));
            }
        }

        protected override void XuLyF7()
        {
            if (V6Login.UserRight.AllowPrint("", _MA_DM.ToUpper() + "6"))
            {
                DoPrint();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
            SetStatus2Text();
        }

        public void DoPrint()
        {
            try
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                var rowData = row.ToDataDictionary();
                if (row == null) return;
                AldmConfig config = ConfigManager.GetAldmConfig(_MA_DM);

                var keys = ObjectAndString.SplitString(config.KEY);
                string values = "";
                foreach (string field in keys)
                {
                    string FIELD = field.ToUpper();
                    if (rowData.ContainsKey(FIELD))
                    {
                        values += "," + rowData[field.ToUpper()];
                    }
                    else
                    {
                        this.ShowErrorMessage(V6Text.NoData + ": " + FIELD);
                        return;
                    }
                }
                values = values.Substring(1);
                string uid = "";
                if (rowData.ContainsKey("UID")) uid = rowData["UID"] + "";

                string program = _program + "_IN";
                string proc = _program + "_IN"; // AAPPR_XL_LIST_ALL_IN
                string repFile = _program + _MA_DM;
                var repTitle = "LIST";
                var repTitle2 = "LIST";

                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (MenuButton.UseXtraReport != shift_is_down)
                {
                    var c = new ReportR_DX(_MA_DM, program, proc, repFile, repTitle, repTitle2, "", "", "");
                    LockSomeControls(c);
                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@MA_DM", _MA_DM));
                    plist.Add(new SqlParameter("@Fields", config.KEY));
                    plist.Add(new SqlParameter("@Values", values));
                    plist.Add(new SqlParameter("@uid", uid));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;
                    c.AutoClickNhan = true;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                else
                {
                    var c = new ReportRViewBase(_MA_DM, program, proc, repFile, repTitle, repTitle2, "", "", "");
                    LockSomeControls(c);
                    List<SqlParameter> plist = new List<SqlParameter>();
                    plist.Add(new SqlParameter("@MA_DM", _MA_DM));
                    plist.Add(new SqlParameter("@Fields", config.KEY));
                    plist.Add(new SqlParameter("@Values", values));
                    plist.Add(new SqlParameter("@uid", uid));
                    plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                    c.FilterControl.InitFilters = plist;
                    c.AutoClickNhan = true;
                    c.ShowToForm(this, V6Setting.IsVietnamese ? repTitle : repTitle2, true);
                }
                

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _MA_DM, ex.Message));
            }
        }

        private void LockSomeControls(V6Control c)
        {
            try
            {
                var lc = c.GetControlByName("cboMauIn");
                if (lc != null) lc.DisableTag();
                lc = c.GetControlByName("chkHienTatCa");
                if (lc != null) lc.DisableTag();
                lc = c.GetControlByName("btnSuaTTMauBC");
                if (lc != null) lc.DisableTag();
                lc = c.GetControlByName("btnThemMauBC");
                if (lc != null) lc.DisableTag();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, _MA_DM, ex.Message));
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

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "F9", plist);
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

                SetStatusText("F9 " + V6Text.Finish);

            }
        }
        #endregion xulyF9
        
        public override void FormatGridViewExtern()
        {
            try
            {
                //VPA_GetFormatGridView]@Codeform VARCHAR(50),@Type VARCHAR(20)
                string FIELDV, OPERV, BOLD_YN, COLOR_YN, COLORV;
                object VALUEV;
                V6BusinessHelper.GetFormatGridView(FilterControl.String1, "LIST", out FIELDV, out OPERV, out VALUEV, out BOLD_YN,
                    out COLOR_YN, out COLORV);
                //Color.MediumAquamarine
                V6ControlFormHelper.FormatGridView(dataGridView1, FIELDV, OPERV, VALUEV, BOLD_YN == "1", COLOR_YN == "1",
                    ObjectAndString.StringToColor(COLORV));

                AldmConfig aldmConfig = ConfigManager.GetAldmConfig(FilterControl.String1);
                if (aldmConfig.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, aldmConfig.GRDS_V1, aldmConfig.GRDF_V1, aldmConfig.GRDH_LANG_V1);
                }

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

        private string mact_format = null;
        protected override void FormatGridView2()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                string mact = dataGridView1.CurrentRow.Cells["Ma_ct"].Value.ToString().Trim();
                if (mact != mact_format)
                {
                    mact_format = mact;
                    //var alctconfig = ConfigManager.GetAlctConfig(mact);
                    var aldmConfig = ConfigManager.GetAldmConfig("AAPPR_AD_" + mact);
                    if (!aldmConfig.HaveInfo) return;

                    var headerString = V6Setting.IsVietnamese ? aldmConfig.GRDHV_V1 : aldmConfig.GRDHE_V1;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, aldmConfig.GRDS_V1, aldmConfig.GRDF_V1, headerString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView2", ex);
            }
        }
    }
}
