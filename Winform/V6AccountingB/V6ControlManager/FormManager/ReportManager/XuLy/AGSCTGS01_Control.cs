using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGSCTGS01_Control : XuLyBase
    {
        public AGSCTGS01_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            _Ctrl_A = false;
            _Ctrl_U = false;
            _SpaceBar = false;
        }

        protected string _tableName = "ARctgs01";
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3: Sửa, F4:Thêm, F8: Xóa.");
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
                //V6ControlFormHelper.FormatGridView(dataGridView1, "TS", "=", 1, true, false, Color.MediumAquamarine);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FormatGridviewExtern ovr: " + ex.Message);
            }
        }

        #region ==== Xử lý F8 ====

        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {

                    if (V6Login.UserRight.AllowDelete("", "S07"))

                    {
                        var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();


                        if (this.ShowConfirmMessage("Có chắc chắn xóa?") ==
                            DialogResult.Yes)
                        {
                            var uid = currentRowData.ContainsKey("UID")
                                ? ObjectAndString.ObjectToString(currentRowData["UID"])
                                : "";
                            var khoa = currentRowData.ContainsKey("KHOA_CTGS")
                                ? ObjectAndString.ObjectToString(currentRowData["KHOA_CTGS"])
                                : "";

                            var keys = new SortedDictionary<string, object>
                            {
                                {"UID", uid},
                                {"KHOA_CTGS", khoa}
                            };
                            var result = V6BusinessHelper.Delete(_tableName, keys);
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
                if (dataGridView1.CurrentRow == null) return;
                if (!V6Login.UserRight.AllowEdit("", "S07"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                var currentRow = dataGridView1.CurrentRow;
                var currentRowData = currentRow.ToDataDictionary();


                var selected_khoa = currentRow.Cells
                    ["Khoa_ctgs"].Value.ToString().Trim();

                if (!string.IsNullOrEmpty(selected_khoa))
                {

                    var fText = "Sửa: ";
                    var f = new V6Form
                    {
                        Text = fText,
                        AutoSize = true,
                        FormBorderStyle = FormBorderStyle.FixedSingle
                    };

                    var hoaDonForm = new AGSCTGS01_F3F4(V6Mode.Edit, selected_khoa, currentRowData);
                    hoaDonForm.UpdateSuccessEvent += data =>
                    {

                    };

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
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF3", ex);
            }
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (!V6Login.UserRight.AllowAdd("", "S07"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                IDictionary<string, object> currentRowData = new SortedDictionary<string, object>();


                var new_like_stt_rec = V6BusinessHelper.GetNewLikeSttRec("S07", "Khoa_ctgs", "M");
                currentRowData["KHOA_CTGS"] = new_like_stt_rec;
                currentRowData["THANG"] = FilterControl.Number1;
                currentRowData["NAM"] = FilterControl.Number2;
                SqlParameter[] plist2 =
                    {
                        new SqlParameter("@Year", (int)FilterControl.Number2), 
                        new SqlParameter("@Period", (int)FilterControl.Number1), 
                    };
                var ngayCuoiKy = ObjectAndString.ObjectToFullDateTime(V6BusinessHelper.ExecuteFunctionScalar("vfa_GetEndDateOfPeriod", plist2));
                currentRowData["NGAY_LO"] = ngayCuoiKy;

                var fText = "Thêm: ";
                var f = new V6Form
                {
                    Text = fText,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.FixedSingle
                };

                var hoaDonForm = new AGSCTGS01_F3F4(V6Mode.Add, new_like_stt_rec, currentRowData);
                hoaDonForm.InsertSuccessEvent += data =>
                {

                };

                f.Controls.Add(hoaDonForm);
                hoaDonForm.Disposed += delegate
                {
                    f.Dispose();
                };

                f.ShowDialog(this);
                SetStatus2Text();
                btnNhan.PerformClick();
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }


    }
}
