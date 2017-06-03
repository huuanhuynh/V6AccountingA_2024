using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AFAGIAMNG : XuLyBase
    {
        public AFAGIAMNG(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3: Sửa,F4:Thêm tăng giá trị TSCĐ, F8: Xóa tăng TSCĐ");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        protected override void XuLyF7()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF7: " + ex.Message);
            }
        }

        #region ==== Xử lý F8 ====

        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {

                    if (V6Login.UserRight.AllowDelete("", "S02"))

                    {
                        var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                            int TS0 = currentRowData.ContainsKey("TS0")
                                ? ObjectAndString.ObjectToInt(currentRowData["TS0"])
                                : 1;

                            if (TS0 == 1)
                            {
                                this.ShowWarningMessage("Không được xóa phần này!");

                            }
                            else
                            {
                                if (this.ShowConfirmMessage("Có chắc chắn xóa tăng giá trị ?") == DialogResult.Yes)
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


                                    var uid = currentRowData.ContainsKey("UID")
                                        ? ObjectAndString.ObjectToString(currentRowData["UID"])
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
                                        new SqlParameter("@uid", uid)



                                    };
                                    var result = V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "_F8",
                                        plist);
                                    if (result > 0)
                                    {
                                        V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                                        btnNhan.PerformClick();
                                    }
                                    else
                                    {
                                        V6ControlFormHelper.ShowMainMessage("Xóa 0");
                                    }
                                }
                            }

                        }
                    
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
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
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
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
                        this.ShowWarningMessage("Không được sửa phần này!");

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

                                    var am = V6BusinessHelper.Select("ADALTS", "*",
                                        "UID=@uid",
                                        "", "", plist.ToArray()).Data;

                              

                                    var fText = "Sửa giảm nguyên giá: " + sothets;
                                    var f = new V6Form
                                    {
                                        Text = fText,
                                        AutoSize = true,
                                        FormBorderStyle = FormBorderStyle.FixedSingle
                                    };


                                  
                                    var hoaDonForm = new AFAGIAMNG_F3(selectedSttRec, am.Rows[0]);
                                    

                                    f.Controls.Add(hoaDonForm);
                                    hoaDonForm.Disposed += delegate
                                    {
                                        f.Dispose();
                                    };

                                    f.ShowDialog();
                                    SetStatus2Text();
                                    btnNhan.PerformClick();

                                
                            }
                        }


                        else
                        {
                            this.ShowWarningMessage("Không được phép sửa!");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF3: " + ex.Message);
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

                                var fText = "Thêm giảm nguyên giá: " + sothets;
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var hoaDonForm = new AFAGIAMNG_F4(selectedSttRec, am.Rows[0]);

                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog();
                                SetStatus2Text();
                                btnNhan.PerformClick();

                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage("Không được phép thêm!");
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4:\n" + ex.Message);
            }
        }

      
    }
}
