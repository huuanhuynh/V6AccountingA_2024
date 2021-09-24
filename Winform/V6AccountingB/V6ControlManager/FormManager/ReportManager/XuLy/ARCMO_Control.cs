using System;
using System.Collections.Generic;
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
    public class ARCMO_Control : XuLyBase
    {
        public ARCMO_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            _Ctrl_A = false;
            _Ctrl_U = false;
            _SpaceBar = false;
        }

        protected string _tableName = "ARCMO";
        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "F3: Sửa, F4:Thêm, F8: Xóa.";
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
                    if (V6Login.UserRight.AllowDelete("", "S0J"))
                    {
                        var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm) ==
                            DialogResult.Yes)
                        {
                            var keys = new SortedDictionary<string, object>
                            {
                                {"UID", currentRowData["UID"]},
                                {"STT_REC", currentRowData["STT_REC"]}
                            };
                            var result = V6BusinessHelper.Delete(_tableName, keys);
                            result += V6BusinessHelper.Delete(_tableName + "CT", keys);
                            if (result > 0)
                            {
                                V6ControlFormHelper.ShowMainMessage(V6Text.DeleteSuccess);
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
                if (dataGridView1.CurrentRow == null) return;


                // Ma_tt=2 : Not run Tuanmh 26/10/2016
                var selectedData = dataGridView1.CurrentRow.ToDataDictionary();
                var selected_matt = selectedData.ContainsKey("MA_TT")
                              ? ObjectAndString.ObjectToInt(selectedData["MA_TT"])
                              :0 ;
               
                if (selected_matt == 2)
                {
                    this.ShowErrorMessage(V6Text.NotRunHere);
                    return;
                }


                XuLyBase view = new ARCMO_ARF9Control(ItemID, _program + "F9", _reportProcedure + "F9", _reportFile + "F9", _reportCaption, _reportCaption2);
                //Set filter data.
               
                view.FilterControl.SetData(selectedData);
                view.btnNhan_Click(null, null);
                view.ShowToForm(this, V6Text.PhanBo);
                
                SetStatus2Text();
                //btnNhan.PerformClick();
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
                if (!V6Login.UserRight.AllowEdit("", "S0J"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                var currentRow = dataGridView1.CurrentRow;
                var currentRowData = currentRow.ToDataDictionary();


                var selected_khoa = currentRow.Cells
                    ["STT_REC"].Value.ToString().Trim();

                if (!string.IsNullOrEmpty(selected_khoa))
                {

                    var fText = "Sửa: ";
                    var f = new V6Form
                    {
                        Text = fText,
                        AutoSize = true,
                        FormBorderStyle = FormBorderStyle.FixedSingle
                    };

                    var hoaDonForm = new ARCMO_F3F4(V6Mode.Edit, selected_khoa, currentRowData);
                    hoaDonForm.UpdateSuccessEvent += data =>
                    {
                        SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                        keys.Add("STT_REC", selected_khoa);

                        data["DIEN_GIAII"] = data["DIEN_GIAI"];
                        data["TIEN_NT"] = data["T_TIEN_NT"];
                        data["TIEN"] = data["T_TIEN"];
                        V6BusinessHelper.Update(_tableName + "CT", data, keys);
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
                if (!V6Login.UserRight.AllowAdd("", "S0J"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                IDictionary<string, object> currentRowData = new SortedDictionary<string, object>();


                var new_like_stt_rec = V6BusinessHelper.GetNewLikeSttRec("S0J", "STT_REC", "M");
                currentRowData["STT_REC"] = new_like_stt_rec;
                

                var fText = "Thêm: ";
                var f = new V6Form
                {
                    Text = fText,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.FixedSingle
                };

                var hoaDonForm = new ARCMO_F3F4(V6Mode.Add, new_like_stt_rec, currentRowData);
                hoaDonForm.InsertSuccessEvent += data =>
                {
                    data["STT_REC0"] = "00001";
                    data["DIEN_GIAII"] = data["DIEN_GIAI"];
                    data["TIEN_NT"] = data["T_TIEN_NT"];
                    data["TIEN"] = data["T_TIEN"];
                    V6BusinessHelper.Insert(_tableName + "CT", data);
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
