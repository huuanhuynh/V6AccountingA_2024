using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ATOSUAPB : XuLyBase
    {
        public ATOSUAPB(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3: Sửa phân bổ, F8: Xóa phân bổ");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        #region ==== Xử lý F8 ====

        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

                    var ma_ct = currentRowData.ContainsKey("MA_CT")
                        ? currentRowData["MA_CT"].ToString().Trim()
                        : "";
                    int nam = currentRowData.ContainsKey("NAM")
                        ? ObjectAndString.ObjectToInt(currentRowData["NAM"])
                        : 1900;
                    int ky = currentRowData.ContainsKey("KY")
                        ? ObjectAndString.ObjectToInt(currentRowData["KY"])
                        : 0;
                    string so_the_cc = currentRowData.ContainsKey("SO_THE_CC")
                        ? currentRowData["SO_THE_CC"].ToString()
                        : "";
                    string ma_nv = currentRowData.ContainsKey("MA_NV") ? currentRowData["MA_NV"].ToString() : "";

                    int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, ky, nam);
                    if (check == 1)
                    {
                        this.ShowWarningMessage(V6Text.CheckLock);
                        return;
                    }

                    if (V6Login.UserRight.AllowDelete("", ma_ct))
                    {
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + so_the_cc) != DialogResult.Yes) return;

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@nam", nam),
                            new SqlParameter("@ky", ky),
                            new SqlParameter("@so_the_cc", so_the_cc),
                            new SqlParameter("@ma_nv", ma_nv)
                        };
                        var result = V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "_F8", plist);
                        if (result > 0)
                        {
                            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage(V6Text.DeleteFail);
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
                    var currentRow = dataGridView1.CurrentRow;
                    var selectedMaCt = currentRow.Cells
                            ["Ma_ct"].Value.ToString().Trim();
                    var selectedSttRec = currentRow.Cells
                        ["Stt_rec"].Value.ToString().Trim();

                    var formView = new V6Form
                    {
                        Text = "Sửa phân bổ",
                        AutoSize = true,
                        FormBorderStyle = FormBorderStyle.FixedSingle
                    };

                    var formF3 = new ATOSUAPB_F3(selectedSttRec, currentRow.ToDataDictionary());
                    

                    
                    formF3.UpdateSuccessEvent += data =>
                    {
                        currentRow.Cells["GT_PB_KY"].Value = data["GT_PB_KY"];
                        currentRow.Cells["SUA_PB"].Value = 1;
                    };
                    formView.Controls.Add(formF3);
                    formF3.Disposed += delegate
                    {
                        formView.Dispose();
                    };

                    formView.ShowDialog(this);
                    SetStatus2Text();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ATOSUAPB F3: " + ex.Message);
            }
        }

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
