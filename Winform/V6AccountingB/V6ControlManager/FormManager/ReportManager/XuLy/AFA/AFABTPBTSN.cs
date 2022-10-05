using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AFABTPBTSN : XuLyBase
    {
        public AFABTPBTSN(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("{0}, F4: {1}, F7: {2}, F8: {3}", V6Text.Text("SBSELECT"), V6Text.Text("TBTKHTSCD"), V6Text.Text("in"), V6Text.Text("XBTKHTSCD"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            try
            {
                var plist = FilterControl.GetFilterParameters();
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)FilterControl.Number2, (int)FilterControl.Number3);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                string paramss = V6ControlFormHelper.PlistToString(plist);
                V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
                    string.Format("reportProcedure:{0} {1}", _reportProcedure, paramss));
                base.MakeReport2();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }

        protected override void XuLyF7()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {

                    if (V6Login.UserRight.AllowView("", "BS02"))
                    {
                        bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "_F7", _program, _reportFile,
                                "BÚT TOÁN KHẤU HAO TSCĐ", "FA DEPRECEATION REPORT", "", "", "");
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.ShowToForm(this, _reportCaption, true);
                        }
                        else
                        {
                            var view = new ReportRViewBase(m_itemId, _program + "_F7", _program, _reportFile,
                                "BÚT TOÁN KHẤU HAO TSCĐ", "FA DEPRECEATION REPORT", "", "", "");
                            view.Dock = DockStyle.Fill;
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

                    if (!V6Login.UserRight.AllowDelete("", "BS02"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    if (this.ShowConfirmMessage(V6Text.Text("XBTPBKH")) == DialogResult.Yes)
                    {
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

                    if (!V6Login.UserRight.AllowEdit("", "BS02"))
                    {
                        V6ControlFormHelper.NoRightWarning();
                        return;
                    }

                    if (this.ShowConfirmMessage(V6Text.Text("TBTPBKH")) == DialogResult.Yes)
                    {
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
