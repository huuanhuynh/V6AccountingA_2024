using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ARSD_AR : XuLyBase
    {
        public ARSD_AR(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {

        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3: Sửa");
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
                    if (!V6Login.UserRight.AllowEdit("", ""))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }
                    
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct")
                        && dataGridView1.Columns.Contains("UID") && dataGridView1.Columns.Contains("SO_CT"))
                    {
                        var selectedMaCt = currentRow.Cells
                            ["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells
                            ["Stt_rec"].Value.ToString().Trim();                                                     
                        var uid = currentRow.Cells
                            ["UID"].Value;
                        var selectedSoct = currentRow.Cells
                            ["SO_CT"].Value.ToString().Trim();

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                        {

                            var plist = new List<SqlParameter>
                                {
                                    new SqlParameter("@uid", uid)
                                };

                            var am = V6BusinessHelper.Select("ARS20", "*", "UID=@uid", "", "", plist.ToArray()).Data;

                            var fText = "Sửa hóa đơn: " + selectedSoct;
                            var f = new V6Form
                            {
                                Text = fText,
                                AutoSize = true,
                                FormBorderStyle = FormBorderStyle.FixedSingle
                            };

                            _sttRec = selectedSttRec;
                            var hoaDonForm = new ARSD_AR_F4(selectedSttRec, am.Rows[0]);
                            hoaDonForm.UpdateSuccessEvent += hoaDonForm_UpdateSuccessEvent;
                            f.Controls.Add(hoaDonForm);
                            hoaDonForm.Disposed += delegate
                            {
                                f.Dispose();
                            };

                            f.ShowDialog();
                            SetStatus2Text();
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

        void hoaDonForm_UpdateSuccessEvent(SortedDictionary<string, object> data)
        {
            try
            {
                V6ControlFormHelper.UpdateGridViewRow(dataGridView1.GetFirstSelectedRow(), data);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".f_UpdateSuccess: " + ex.Message);
            }
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            this.ShowWarningMessage("Không được thêm mới!");
        }
    }
}

