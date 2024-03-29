﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.SoDuManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGLTHUE20 : XuLyBase
    {
        public AGLTHUE20(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {

        }
        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "F3: Sửa,F4:Thêm hóa đơn bán ra, F8: Xóa";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
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

                    if (V6Login.UserRight.AllowDelete("", "ARV"))

                    {
                       
                     var currentRow = dataGridView1.CurrentRow;

                     var selectedMaCt = currentRow.Cells
                                ["Ma_ct"].Value.ToString().Trim();
                     var selectedSttRec = currentRow.Cells
                                ["Stt_rec"].Value.ToString().Trim();                                                     
                           

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt)
                            && selectedMaCt == "ARV")
                        {

                            if (dataGridView1.Columns.Contains("UID"))
                            {
                                var keys = new SortedDictionary<string, object> { { "UID", currentRow.Cells["UID"].Value } };

                                if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " ", V6Text.DeleteConfirm)
                                    == DialogResult.Yes)
                                {
                                    var t =V6BusinessHelper.Delete("ARV20", keys);

                                    if (t > 0)
                                    {
                                        V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                                    }
                                    else
                                    {
                                        V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                                    }
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
                    if (!V6Login.UserRight.AllowEdit("", "ARV"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                   
                    {
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

                            if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt)
                               && selectedMaCt == "ARV")
                            {

                                var plist = new List<SqlParameter>
                                    {
                                        new SqlParameter("@uid", uid)
                                    };

                                var am = V6BusinessHelper.Select("ARV20", "*",
                                    "UID=@uid",
                                    "", "", plist.ToArray()).Data;



                                var fText = "Sửa hóa đơn: " + selectedSoct;
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                _sttRec = selectedSttRec;
                                var hoaDonForm = new AGLTHUE20_F4(selectedSttRec, am.Rows[0]);
                                
                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                                btnNhan.PerformClick();


                            }
                            else
                            {
                                this.ShowWarningMessage(V6Text.EditDenied);
                            }
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
            //var currentRow = dataGridView1.CurrentRow;

            var f = new V6Form
            {
                AutoSize = true,
                FormBorderStyle = FormBorderStyle.FixedSingle
            };
          
            var hoaDonForm = new AGLTHUE20_F4();

            f.Controls.Add(hoaDonForm);
            hoaDonForm.Disposed += delegate
            {
                f.Dispose();
            };

            f.ShowDialog(this);
            SetStatus2Text();
            btnNhan.PerformClick();

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // btnNhan
            // 
            this.btnNhan.Click += new System.EventHandler(this.btnNhan_Click_1);
            // 
            // AGLTHUE20
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AGLTHUE20";
            this.ResumeLayout(false);

        }

        private void btnNhan_Click_1(object sender, EventArgs e)
        {

        }
    }
}

