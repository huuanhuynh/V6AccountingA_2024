﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AFADCCTBP : XuLyBase
    {
        public AFADCCTBP(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }
        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F3: {0}, F4: {1}, F8: {2}Xóa tỷ lệ TSCĐ", V6Text.Text("SUA"), V6Text.Text("TTLKHTSCD"), V6Text.Text("XTLKHTSCD"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        public override void FormatGridViewExtern()
        {
            try
            {
                V6ControlFormHelper.FormatGridView(dataGridView1, "TS0", "=", 1, true, false, Color.MediumAquamarine);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FormatGridviewExtern ovr: " + ex.Message);
            }
        }

        protected override void XuLyF7()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
        }

        #region ==== Xử lý F8 ====

        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!V6Login.UserRight.AllowDelete("", "S02")){
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }
                    
                    var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                    int TS0 = currentRowData.ContainsKey("TS0")
                        ? ObjectAndString.ObjectToInt(currentRowData["TS0"])
                        : 1;

                    if (TS0 == 1)
                    {
                        this.ShowWarningMessage(V6Text.DeleteDenied);

                    }
                    else
                    {
                        if (this.ShowConfirmMessage("Có chắc chắn xóa điều chuyển bộ phận ?") == DialogResult.Yes)
                        {
                            int nam = currentRowData.ContainsKey("RNAM")
                                ? ObjectAndString.ObjectToInt(currentRowData["RNAM"])
                                : 1900;
                            int ky1 = currentRowData.ContainsKey("RKY1")
                                ? ObjectAndString.ObjectToInt(currentRowData["RKY1"])
                                : 0;
                            int ky2 = currentRowData.ContainsKey("RKY2")
                                ? ObjectAndString.ObjectToInt(currentRowData["RKY2"])
                                : 0;
                            string Diengiai = currentRowData.ContainsKey("RDIEN_GIAI")
                                ? ObjectAndString.ObjectToString(currentRowData["RDIEN_GIAI"])
                                : "";
                            string Madvcs = currentRowData.ContainsKey("MA_DVCS")
                                ? ObjectAndString.ObjectToString(currentRowData["MA_DVCS"])
                                : "";
                            string Madvcs0 = currentRowData.ContainsKey("MA_DVCS0")
                                ? ObjectAndString.ObjectToString(currentRowData["MA_DVCS0"])
                                : "";
                            string Sothets = currentRowData.ContainsKey("SO_THE_TS")
                                ? ObjectAndString.ObjectToString(currentRowData["SO_THE_TS"])
                                : "";

                            SqlParameter[] plist =
                            {
                                new SqlParameter("@nam", nam),
                                new SqlParameter("@ky1", ky1),
                                new SqlParameter("@ky2", ky2),
                                new SqlParameter("@User_id", V6Login.UserId),
                                new SqlParameter("@So_the_ts", Sothets),
                                new SqlParameter("@Ma_dvcs", Madvcs),
                                new SqlParameter("@Ma_dvcs0", Madvcs0),
                                new SqlParameter("@uid", currentRowData["UID"])

                            };
                            var result = V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "_F8",
                                plist);
                            if (result > 0)
                            {
                                V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                                btnNhan.PerformClick();
                            }
                            else
                            {
                                V6ControlFormHelper.ShowMainMessage(V6Text.DeleteFail);
                            }
                        }
                    }                      

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF8: " + ex.Message);
            }
        }

        #endregion xử lý F8

        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }
        
        #endregion xulyF9

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!V6Login.UserRight.AllowEdit("", "S02"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                    int TS0 = currentRowData.ContainsKey("TS0")
                        ? ObjectAndString.ObjectToInt(currentRowData["TS0"])
                        : 1;
                    
                    if (TS0 == 1)
                    {
                        this.ShowWarningMessage(V6Text.EditDenied);
                    }
                    else
                    {
                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct")
                            && dataGridView1.Columns.Contains("UID"))
                        {
                            var selectedMaCt = currentRow.Cells
                                ["Ma_ct"].Value.ToString().Trim();
                            var selectedSttRec = currentRow.Cells
                                ["Stt_rec"].Value.ToString().Trim();

                            var sothets = currentRow.Cells
                                ["SO_THE_TS"].Value.ToString().Trim();
                            
                            var nam = currentRow.Cells
                                ["NAM"].Value;
                            
                            var ky = currentRow.Cells
                              ["KY"].Value;

                            var uid = currentRow.Cells
                              ["UID"].Value;


                            if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                            {
                                var plist = new List<SqlParameter>
                                {
                                    new SqlParameter("@uid", uid)
                                };

                                var am = V6BusinessHelper.Select("ADCTTSBP", "*",
                                    "UID=@uid",
                                    "", "", plist.ToArray()).Data;

                                var fText = V6Text.Text("SUATYLEKHAUHAO") + ": " + sothets;
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var hoaDonForm = new AFADCCTBP_F3(selectedSttRec, am.Rows[0]);

                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                                btnNhan.PerformClick();
                            }
                        }
                        else
                        {
                            this.ShowWarningMessage(V6Text.EditDenied);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF3", ex);
            }
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (!V6Login.UserRight.AllowAdd("", "S02"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct"))
                    {
                        var selectedMaCt = currentRow.Cells
                            ["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells
                            ["Stt_rec"].Value.ToString().Trim();

                        var sothets = currentRow.Cells
                          ["SO_THE_TS"].Value.ToString().Trim();

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

                                var fText = V6Text.Text("TTLKH") + ": " + sothets;
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var hoaDonForm = new AFADCCTBP_F4(selectedSttRec, am.Rows[0]);

                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                                btnNhan.PerformClick();

                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.AddDenied);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

      
    }
}
