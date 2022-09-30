using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ATOBTPBCCN : XuLyBase
    {
        public ATOBTPBCCN(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "F4: Tạo bút toán phân bổ công cụ, F7: In, F8: Xóa bút phân bổ công cụ.";
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
                if (dataGridView1.CurrentRow != null)
                {

                    if (V6Login.UserRight.AllowView("", "BS03"))
                    {
                        bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "_F7", _program, _reportFile,
                                "BÚT TOÁN PHÂN BỔ CÔNG CỤ", "TOOLS ALLOCATION REPORT", "", "", "");
                            view.Dock = DockStyle.Fill;
                            //view.FilterControl.InitFilters = oldKeys;
                            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.ShowToForm(this, _reportCaption, true);
                        }
                        else
                        {
                            var view = new ReportRViewBase(m_itemId, _program + "_F7", _program, _reportFile,
                                "BÚT TOÁN PHÂN BỔ CÔNG CỤ", "TOOLS ALLOCATION REPORT", "", "", "");
                            view.Dock = DockStyle.Fill;
                            //view.FilterControl.InitFilters = oldKeys;
                            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.ShowToForm(this, _reportCaption, true);
                        }

                        SetStatus2Text();
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
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

                    if (V6Login.UserRight.AllowDelete("", "BS03"))
                    {
                        if (this.ShowConfirmMessage(V6Text.DeleteConfirm) != DialogResult.Yes) return;

                        var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

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

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@nam", nam),
                            new SqlParameter("@ky1", ky1),
                            new SqlParameter("@ky2", ky2),
                            new SqlParameter("@User_id", V6Login.UserId),
                            new SqlParameter("@Dien_giai", Diengiai),
                            new SqlParameter("@Ma_dvcs", Madvcs),
                            new SqlParameter("@Ma_dvcs0", Madvcs0),
                            new SqlParameter("@Action", "F8"),
                        };
                        var result = V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist);
                        if (result > 0)
                        {
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
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            
            try
            {
                if (dataGridView1.CurrentRow != null)
                {

                    if (!V6Login.UserRight.AllowEdit("", "BS03"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    if (this.ShowConfirmMessage("Tạo bút toán phân bổ công cụ") == DialogResult.Yes)
                    {
                        //@Nam INT, 
                        //@Ky1 INT,
                        //@Ky2 INT,
                        //@User_id INT,
                        //@Dien_giai NVARCHAR(MAX),
                        //@Ma_dvcs VARCHAR(50),
                        //@Ma_dvcs0 VARCHAR(50),
                        //@Action VARCHAR(50)

                        var currentRowData = dataGridView1.CurrentRow.ToDataDictionary();

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

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@nam", nam),
                            new SqlParameter("@ky1", ky1),
                            new SqlParameter("@ky2", ky2),
                            new SqlParameter("@User_id", V6Login.UserId),
                            new SqlParameter("@Dien_giai",Diengiai),
                            new SqlParameter("@Ma_dvcs", Madvcs),
                            new SqlParameter("@Ma_dvcs0", Madvcs0),
                            new SqlParameter("@Action", "F4"),
                        };
                        var result = V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist);
                        if (result > 0)
                        {
                            V6ControlFormHelper.ShowMainMessage("Đã tạo!");
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMainMessage("Tạo 0");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF4: " + ex.Message);
            }


        }
    }
}
