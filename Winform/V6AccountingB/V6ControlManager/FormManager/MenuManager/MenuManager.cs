using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.HeThong;
using V6ControlManager.FormManager.HeThong.QuanLyHeThong;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.NhanSu;
using V6ControlManager.FormManager.NhanSu.View;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6ControlManager.FormManager.ReportManager.XuLy;
using V6ControlManager.FormManager.SoDuManager;
using V6ControlManager.FormManager.SoDuManager.FirstFilter;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.MenuManager
{
    public static class MenuManager
    {
        /// <summary>
        /// formname = codeform.Substring(1).ToUpper();
        /// </summary>
        private static string FORM_NAME;
        /// <summary>
        /// TABLE_NAME = codeform.Substring(1).ToUpper();
        /// </summary>
        private static string TABLE_NAME;
        public static readonly SortedDictionary<string, string> CheckV6Tables = new SortedDictionary<string, string>()
        {
            {"ALDM", "ALDM"},
            {"V6LOOKUP", "V6LOOKUP"},
            {"ALREPORT", "ALREPORT"},
            {"ALREPORT1", "ALREPORT1"},
            {"", ""},
        };
        public static readonly SortedDictionary<string, string> CheckAdminTables = new SortedDictionary<string, string>()
        {
            {"V6USER", "V6USER"},
            {"V6CLIENTS", "V6CLIENTS"},
            {"ALSTT", "ALSTT"},
            {"V6OPTION", "V6OPTION"},
        };

        public static V6Control GenControl(Control owner, MenuButton mButton, MouseEventArgs e)
        {
            try
            {
                string item_id = mButton.ItemID;
                string codeform = mButton.CodeForm;
                var repFile = mButton.ReportFile;
                var repTitle = mButton.ReportTitle;
                var repTitle2 = mButton.ReportTitle2;

                var repFileF5 = mButton.ReportFileF5;
                var repTitleF5 = mButton.ReportTitleF5;
                var repTitle2F5 = mButton.ReportTitle2F5;

                V6Control c = null;
                if (!string.IsNullOrEmpty(codeform))
                {
                    var code = codeform.Substring(0, 1);
                    FORM_NAME = codeform.Substring(1).ToUpper();
                    TABLE_NAME = codeform.Substring(1).ToUpper();
                    bool check = true, mouse_left = false, ctrl_is_down = false;
                    if (e == null || e.Button == MouseButtons.Left)
                    {
                        mouse_left = true;
                    }
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        ctrl_is_down = true;
                    }

                    switch (code)
                    {
                        case "1"://Chung tu

                            code = codeform.Substring(1);
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                                c = ChungTu.GetChungTuContainer(code, item_id);
                            else
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);

                            break;
                        case "2"://DanhMucView
                            #region ==== DanhMucView ====
                            if (CheckV6Tables.ContainsKey(TABLE_NAME))
                            {
                                check = CheckPasswordV6(owner);
                            }
                            else if (CheckAdminTables.ContainsKey(TABLE_NAME) || codeform.StartsWith("8"))
                            {
                                if (mouse_left && ctrl_is_down)
                                {
                                    check = CheckPasswordV6(owner);
                                }
                                else
                                {
                                    check = CheckPassword(owner);
                                }
                            }

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (check)
                                {
                                    bool is_aldm = false, check_admin = false, check_v6 = false;
                                    IDictionary<string, object> keys = new Dictionary<string, object>();
                                    keys.Add("MA_DM", TABLE_NAME);
                                    var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                                    if (aldm.Rows.Count == 1)
                                    {
                                        is_aldm = aldm.Rows[0]["IS_ALDM"].ToString() == "1";
                                        check_admin = aldm.Rows[0]["CHECK_ADMIN"].ToString() == "1";
                                        check_v6 = aldm.Rows[0]["CHECK_V6"].ToString() == "1";
                                    }

                                    var where = "";
                                    if (TABLE_NAME == "ALKC")
                                    {
                                        var filterForm = new YearFilterForm(TABLE_NAME);
                                        if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                        {

                                            where = filterForm.QueryString;
                                            c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                                V6Login.GetInitFilter(TABLE_NAME, V6ControlFormHelper.FindFilterType(owner)), null, is_aldm)
                                            {
                                                Name = item_id,
                                                ReportFile = repFile,
                                                ReportTitle = repTitle,
                                                ReportTitle2 = repTitle2
                                            };
                                            ((DanhMucView) c).AddInitFilter(where);
                                        }
                                    }
                                    else
                                    {
                                        if (is_aldm)
                                        {
                                            if (check_admin && V6Login.IsAdmin)
                                            {
                                                check = CheckPassword(owner);
                                            }
                                            else if (check_v6)
                                            {
                                                check = CheckPasswordV6(owner);
                                            }
                                        }

                                        if (!check) return null;

                                        c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                            V6Login.GetInitFilter(TABLE_NAME, V6ControlFormHelper.FindFilterType(owner)), null, is_aldm)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        if (TABLE_NAME == "V6LOOKUP")
                                        {
                                            var dv = (DanhMucView) c;
                                            //dv.EnableAdd = false;
                                            dv.EnableDelete = false;
                                            dv.EnableChangeCode = false;
                                            dv.EnableChangeGroup = false;
                                        }
                                    }

                                }
                                else
                                {
                                    c = null;
                                }
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }


                            break;
                            #endregion danhmucview
                        case "3":
                            #region ==== //cap nhap so du //SoDuView ====
                            var TABLE_NAME_SD = codeform.Substring(1).ToUpper();

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (TABLE_NAME_SD == "ABSPYTCP" || TABLE_NAME_SD == "ABSPDD" ||
                                    TABLE_NAME_SD == "ACOSXLT_ABSPDD" || TABLE_NAME_SD == "ACOSXLT_ABSPYTCP" ||
                                    TABLE_NAME_SD == "ACOSXLSX_ABSPDD" || TABLE_NAME_SD == "ACOSXLSX_ABSPYTCP" ||
                                    TABLE_NAME_SD == "ABHHVT" || TABLE_NAME_SD == "ABNGHI")
                                {
                                    var filterForm = new YearMonthFilterForm(TABLE_NAME_SD);
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        var where = filterForm.QueryString;
                                        c = new SoDuView(item_id, mButton.Text, TABLE_NAME_SD)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView) c).AddInitFilter(where);
                                    }
                                }
                                else
                                {

                                    c = new SoDuView(item_id, mButton.Text, TABLE_NAME_SD,
                                        V6Login.GetInitFilter(TABLE_NAME_SD, V6ControlFormHelper.FindFilterType(owner)))
                                    {
                                        Name = item_id,
                                        ReportFile = repFile,
                                        ReportTitle = repTitle,
                                        ReportTitle2 = repTitle2
                                    };
                                }
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;
                            #endregion soduview

                        case "4": //bao cao
                            if (!V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                                break;
                            }

                            var program = codeform.Substring(1);
                            if (codeform.StartsWith("44"))
                            {
                                program = codeform.Substring(2);
                                c = new ReportR44ViewBase(item_id, program, program, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            else if (codeform.StartsWith("45")) // Không hiển thị report.
                            {
                                program = codeform.Substring(2);
                                c = new ReportR45ViewBase(item_id, program, program, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            else if (codeform.StartsWith("47")) // Không hiển thị report.
                            {
                                // Hai bộ report trên dưới
                                // 1 form cha chứa 2 form report viewbase 47 bên trong.
                                program = codeform.Substring(2);
                                c = new ReportR47Container(item_id, program, program, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            else
                            {
                                c = new ReportRViewBase(item_id, program, program, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }

                            End_case_4:
                            break;

                        case "9":
                            #region ==== //bao cao dong ReportDViewBase ====
                            var program1 = codeform.Substring(1);
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (codeform.StartsWith("99"))
                                {
                                    program = codeform.Substring(2);
                                    c = new ReportD99ViewBase(item_id, program, program, repFile, repTitle, repTitle2,
                                        repFileF5, repTitleF5, repTitle2F5);
                                }
                                else
                                {
                                    c = new ReportDViewBase(item_id, program1, program1, repFile, repTitle, repTitle2,
                                        repFileF5, repTitleF5, repTitle2F5);
                                }
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;
                            #endregion ReportDViewBase

                        case "5": //nhat ky popup
                            
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                c = new V6UserControlEmpty("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;

                        case "6":
                            #region ==== //DanhMucView Edit only//====

                            var tableNameEdit = codeform.Substring(1);
                            string getInitFilter = null;
                            bool check1 = true;

                            if (tableNameEdit.ToUpper() == "ALSTT" || tableNameEdit.ToUpper() == "V6OPTION")
                            {
                                if (mouse_left && ctrl_is_down)
                                {
                                    check1 = CheckPasswordV6(owner);
                                    getInitFilter = "";
                                }
                                else
                                {
                                    check1 = CheckPassword(owner);
                                    getInitFilter = V6Login.GetInitFilter(tableNameEdit, V6ControlFormHelper.FindFilterType(owner));
                                }
                            }

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (check1)
                                {
                                    bool is_aldm = false, check_admin = false, check_v6 = false;
                                    IDictionary<string, object> keys = new Dictionary<string, object>();
                                    keys.Add("MA_DM", TABLE_NAME);
                                    var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                                    if (aldm.Rows.Count == 1)
                                    {
                                        is_aldm = aldm.Rows[0]["IS_ALDM"].ToString() == "1";
                                        check_admin = aldm.Rows[0]["CHECK_ADMIN"].ToString() == "1";
                                        check_v6 = aldm.Rows[0]["CHECK_V6"].ToString() == "1";
                                    }

                                    if (is_aldm)
                                    {
                                        if (check_admin && V6Login.IsAdmin)
                                        {
                                            check = CheckPassword(owner);
                                        }
                                        else if (check_v6)
                                        {
                                            check = CheckPasswordV6(owner);
                                        }
                                    }

                                    if (!check) return null;

                                    c = new DanhMucView(item_id, mButton.Text, tableNameEdit, getInitFilter, "", false)
                                    {
                                        Name = item_id,
                                        ReportFile = repFile,
                                        ReportTitle = repTitle,
                                        ReportTitle2 = repTitle2,

                                        EnableAdd = false,
                                        EnableEdit = true,
                                        EnableDelete = false,
                                        EnableCopy = false,
                                    };

                                }
                                else
                                {
                                    c = null;
                                }
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;
                            #endregion DanhMucView edit only

                        case "7":
                            #region ==== //DanhMucView View only//====
                            var tableNameView = codeform.Substring(1);
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                c = new DanhMucView(item_id, mButton.Text, tableNameView,
                                    V6Login.GetInitFilter(tableNameView, V6ControlFormHelper.FindFilterType(owner)), null, false)
                                {
                                    Name = item_id,
                                    ReportFile = repFile,
                                    ReportTitle = repTitle,
                                    ReportTitle2 = repTitle2,

                                    EnableAdd = false,
                                    EnableEdit = false,
                                    EnableDelete = false,
                                    EnableCopy = false,
                                };
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;
                            #endregion view only

                        case "8":
                            #region ==== V6CLIENTS V6ONLINES ====
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                check = CheckPassword(owner);

                                if (check)
                                {
                                    if (FORM_NAME == "V6CLIENTS") c = new ClientManager(item_id);
                                    if (FORM_NAME == "V6ONLINES")
                                    {
                                        if(V6Login.IsLocal) c = new OnlineManager(item_id);
                                        else c = new V6UserControlEmpty(V6Text.NotAllowed);
                                    }
                                }
                            }
                            break;
                            #endregion V6CLIENTS V6ONLINES
                        case "G":
                            c = FormManagerHelper.GetGeneralControl(codeform.Substring(1), item_id);
                            break;
                        case "W":
                            #region ==== //bao cao link 2 tables ====

                            if (!V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                                break;
                            }

                            var programW = codeform.Substring(1);
                            if (codeform.StartsWith("WW"))
                            {
                                programW = codeform.Substring(2);
                                c = new ReportRWWView2Base(item_id, programW, programW, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            else
                            {
                                c = new ReportRView2Base(item_id, programW, programW, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }

                            End_case_W:
                            break;
                            #endregion bao cao link 2 table

                        case "X":
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                AldmConfig config = V6ControlsHelper.GetAldmConfig(programX);

                                if (config.HaveInfo && config.CHECK_ADMIN && V6Login.IsAdmin)
                                {
                                    check = CheckPassword(owner);
                                }
                                else if (config.HaveInfo && config.CHECK_V6)
                                {
                                    check = CheckPasswordV6(owner);
                                }
                                if (!check) return null;
                                c = XuLy.GetXuLyControlX(item_id, programX, programX, repFile, mButton.Text);
                            }
                            break;
                        case "Z":
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (codeform.StartsWith("Z44")) // có hiện chi tiết
                                {
                                    var programZ = codeform.Substring(3);
                                    c = new XuLy44Base(item_id, programZ, programZ, repFile, mButton.ReportTitle, mButton.ReportTitle2, true, repFileF5, repTitleF5, repTitle2F5);
                                }
                                else if (codeform.StartsWith("Z45")) // Không hiện chi tiết
                                {
                                    var programZ = codeform.Substring(3);
                                    c = new XuLy44Base(item_id, programZ, programZ, repFile, mButton.ReportTitle, mButton.ReportTitle2, false, repFileF5, repTitleF5, repTitle2F5);
                                }
                                else
                                {
                                    var programZ = codeform.Substring(1);
                                    c = XuLy.GetXuLyControl(item_id, programZ, programZ, repFile, mButton.ReportTitle,
                                        mButton.ReportTitle2,
                                        repFileF5, repTitleF5, repTitle2F5);
                                }
                            }
                            break;
                        case "B":
                            #region ==== //So du 2 //SoDuView2 ====
                            var maCt = codeform.Substring(1);

                            //Check Admin,V6
                            if (maCt == "S06")
                            {
                                if (mouse_left && ctrl_is_down)
                                {
                                    check = CheckPasswordV6(owner);
                                }
                                else
                                {
                                    if (V6Login.IsAdmin)
                                    {
                                        check = CheckPassword(owner);
                                    }
                                }
                                if (!check) return null;
                            }


                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                string where;
                                if (maCt == "S05") // Aldmvt
                                {
                                    var filterForm = new AldmvtFilterForm();
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                if (maCt == "S10" + "") // acosxlt_aldmvt
                                {
                                    var filterForm = new AldmvtSXLTFilterForm();
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                    if (maCt == "S11" + "") // acosxlt_aldmvt
                                {
                                    var filterForm = new AldmvtSXDHFilterForm();
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                else if (maCt == "S0M") // Alkmb
                                {
                                    var filterForm = new AlkmbFilterForm();
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                else if (maCt == "S0N") // Akhungck
                                {
                                    var filterForm = new AkhungckFilterForm();
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                else if (maCt == "S04")
                                {
                                    var filterForm = new YearFilterForm(maCt);
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {
                                        where = filterForm.QueryString;
                                        c = new SoDuView2(mButton.Text, maCt, item_id)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        ((SoDuView2)c).AddInitFilter(where);
                                    }
                                }
                                else
                                {
                                    c = new SoDuView2(mButton.Text, maCt, item_id)
                                    {
                                        Name = item_id,
                                        ReportFile = repFile,
                                        ReportTitle = repTitle,
                                        ReportTitle2 = repTitle2
                                    };

                                }

                            }
                            else
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            break;
                            #endregion soduview2
                        case "T": //bao cao TreeView
                            var programT = codeform.Substring(1);
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                c = new ReportTreeViewBase(item_id, programT, programT, repFile, repTitle, repTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            else
                            {
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                            }
                            break;

                        case "C": // NhanSu NoGridControl
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                //var programX = codeform.Substring(1);
                                c = new NoGridControl(item_id, FORM_NAME);
                            }
                            break;

                        case "D": // NhanSu OneGridControl
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                //var programX = codeform.Substring(1);
                                c = new OneGridControl(item_id, FORM_NAME);
                            }
                            break;

                        case "E": // NhanSu TwoGridControl
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                //var programX = codeform.Substring(1);
                                c = new TwoGridControl(item_id, FORM_NAME);
                            }
                            break;

                        case "F": // Nhân sự view HLNS
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                c = NhanSuManager.GetControl(item_id, FORM_NAME);
                            }
                            break;
                        case "H":
                            #region ==== // Danhmucview format Aldm ====
                            TABLE_NAME = codeform.Substring(1).ToUpper();
                            if (CheckAdminTables.ContainsKey(TABLE_NAME) || codeform.StartsWith("8"))
                            {
                                if (mouse_left && ctrl_is_down)
                                {
                                    check = CheckPasswordV6(owner);
                                }
                                else
                                {
                                    check = CheckPassword(owner);
                                }
                            }
                            else if (CheckV6Tables.ContainsKey(TABLE_NAME))
                            {
                                check = CheckPasswordV6(owner);
                            }

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (check)
                                {
                                    //var where = "";
                                    bool is_aldm = false, check_admin = false, check_v6 = false;
                                    IDictionary<string, object> keys = new Dictionary<string, object>();
                                    keys.Add("MA_DM", TABLE_NAME);
                                    var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                                    if (aldm.Rows.Count == 1)
                                    {
                                        is_aldm = aldm.Rows[0]["IS_ALDM"].ToString() == "1";
                                        check_admin = aldm.Rows[0]["CHECK_ADMIN"].ToString() == "1";
                                        check_v6 = aldm.Rows[0]["CHECK_V6"].ToString() == "1";
                                    }

                                    if (is_aldm)
                                    {
                                        if (check_admin && V6Login.IsAdmin)
                                        {
                                            check = CheckPassword(owner);
                                        }
                                        else if (check_v6)
                                        {
                                            check = CheckPasswordV6(owner);
                                        }
                                    }

                                    if (!check) return null;

                                    c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                        V6Login.GetInitFilter(TABLE_NAME, V6ControlFormHelper.FindFilterType(owner)), null, is_aldm)
                                    {
                                        Name = item_id,
                                        ReportFile = repFile,
                                        ReportTitle = repTitle,
                                        ReportTitle2 = repTitle2
                                    };
                                }
                                else
                                {
                                    c = null;
                                }
                            }
                            else
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);

                            break;
                            #endregion
                        default:
                            c = new V6UserControlEmpty("Key:" + item_id + "; Codeform:" + codeform);
                            break;
                    }
                }
                else // Dùng trực tiếp Item_ID để gọi 1 control riêng nào đó.
                {
                    //Key: C0108028; 
                    if (item_id == "C0108028")
                    {
                        string path = Path.Combine(Application.StartupPath, "V6HELP\\V6HELP.chm");
                        Help.ShowHelp(owner, "File://" + path);
                    }
                    else if (item_id == "DPASS386")
                    {
                        //Tuanmh 23/09/2017 Change pass
                        new ChangePassword().ShowDialog();
                        

                    }
                    else if (item_id == "C0108028")
                    {
                        c = new KhoHangControl();
                    }
                    else if (item_id == "C0108027")
                    {
                        c = new ThongTinChuongTrinh();
                    }
                    else if (item_id == "D0000019")
                    {
                        c = new HuuanTetris.HuuanTetris1();
                    }
                    else
                    {
                        c = new V6UserControlEmpty("Key:" + item_id + "; Codeform:" + codeform);
                    }
                }

                if (c != null)
                {
                    c.Dock = DockStyle.Fill;
                    c.Name = item_id;
                    c.CodeForm = codeform;
                }
                return c;
            }
            catch (Exception ex)
            {
                owner.ShowErrorMessage(MethodBase.GetCurrentMethod().DeclaringType + " GenControl: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra password của user đang đăng nhập.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static bool CheckPassword(IWin32Window owner)
        {
            return new ConfirmPassword().ShowDialog(owner) == DialogResult.OK;
        }
        
        /// <summary>
        /// Kiểm tra admin 1 lần (chưa làm)
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        private static bool CheckPasswordAdmin(IWin32Window owner)
        {
            return new ConfirmPassword().ShowDialog(owner) == DialogResult.OK;
        }

        /// <summary>
        /// Kiểm tra password V6
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static bool CheckPasswordV6(IWin32Window owner)
        {
            return new ConfirmPasswordV6().ShowDialog(owner) == DialogResult.OK;
        }
    }
}
