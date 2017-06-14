using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.DanhMucManager.ChangeCode;
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
                    var formname = codeform.Substring(1).ToUpper();
                    var TABLE_NAME = codeform.Substring(1).ToUpper();
                    var check = true;

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
                            //var TABLE_NAME = codeform.Substring(1).ToUpper();
                            //var check = true;
                            if (TABLE_NAME == "V6USER")
                            {
                                check = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                            }
                            else if (TABLE_NAME == "ALDM" || TABLE_NAME == "V6LOOKUP" || TABLE_NAME == "ALREPORT1" || TABLE_NAME == "ALREPORT")
                            {
                                check = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
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
                                        if (filterForm.ShowDialog() == DialogResult.OK)
                                        {

                                            where = filterForm.QueryString;
                                            c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                                V6Login.GetInitFilter(TABLE_NAME), null, is_aldm)
                                            {
                                                Name = item_id,
                                                ReportFile = repFile,
                                                ReportTitle = repTitle,
                                                ReportTitle2 = repTitle2
                                            };
                                            ((DanhMucView)c).AddInitFilter(where);
                                        }
                                    }
                                    else
                                    {
                                        if (is_aldm)
                                        {
                                            if (check_admin)
                                            {
                                                check = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                                            }
                                            else if (check_v6)
                                            {
                                                check = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
                                            }
                                        }

                                        if (!check) return null;

                                        c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                            V6Login.GetInitFilter(TABLE_NAME), null, is_aldm)
                                        {
                                            Name = item_id,
                                            ReportFile = repFile,
                                            ReportTitle = repTitle,
                                            ReportTitle2 = repTitle2
                                        };
                                        if (TABLE_NAME == "V6LOOKUP")
                                        {
                                            var dv = (DanhMucView)c;
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
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);


                            break;
                            #endregion danhmucview
                        case "3":
                            #region ==== //cap nhap so du //SoDuView ====
                            var tableNamesd = codeform.Substring(1);

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                if (tableNamesd == "ABSPYTCP" || tableNamesd == "ABSPDD" ||
                                    tableNamesd == "ACOSXLT_ABSPDD" || tableNamesd == "ACOSXLT_ABSPYTCP" ||
                                    tableNamesd == "ACOSXLSX_ABSPDD" || tableNamesd == "ACOSXLSX_ABSPYTCP")
                                {
                                    var filterForm = new YearMonthFilterForm(tableNamesd);
                                    if (filterForm.ShowDialog() == DialogResult.OK)
                                    {
                                        var where = filterForm.QueryString;
                                        c = new SoDuView(item_id, mButton.Text, tableNamesd)
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

                                    c = new SoDuView(item_id, mButton.Text, tableNamesd,
                                        V6Login.GetInitFilter(tableNamesd))
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
                            bool check1 = true, control_press = false;

                            if (e.Button == MouseButtons.Left && (Control.ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                control_press = true;
                            }

                            if (tableNameEdit.ToUpper() == "ALSTT" || tableNameEdit.ToUpper() == "V6OPTION")
                            {
                                if (control_press)
                                {
                                    check1 = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
                                    getInitFilter = "";
                                }
                                else
                                {
                                    check1 = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                                    getInitFilter = V6Login.GetInitFilter(tableNameEdit);
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
                                        if (check_admin)
                                        {
                                            check = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                                        }
                                        else if (check_v6)
                                        {
                                            check = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
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
                                    V6Login.GetInitFilter(tableNameView), null, false)
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
                                check = new ConfirmPassword().ShowDialog() == DialogResult.OK;

                                if (check)
                                {
                                    if (formname == "V6CLIENTS") c = new ClientManager(item_id);
                                    if (formname == "V6ONLINES")
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
                                c = XuLy.GetXuLyControlX(item_id, programX, programX, repFile, mButton.Text);
                            }
                            break;
                        case "Z":
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programZ = codeform.Substring(1);
                                c = XuLy.GetXuLyControl(item_id, programZ, programZ, repFile, mButton.ReportTitle, mButton.ReportTitle2,
                                    repFileF5, repTitleF5, repTitle2F5);
                            }
                            break;
                        case "B":
                            #region ==== //So du 2 //SoDuView2 ====
                            var maCt = codeform.Substring(1);

                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                string where;
                                if (maCt == "S05") // Aldmvt
                                {
                                    var filterForm = new AldmvtFilterForm();
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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
                                    if (filterForm.ShowDialog() == DialogResult.OK)
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

                        case "C": // View readonly
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                //c = XuLy.GetXuLyControlX(item_id, programX, programX, repFile, mButton.Text);
                                c = new NoGridControl(item_id, formname);
                            }
                            break;

                        case "D": // GridView/form readonly
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                //c = XuLy.GetXuLyControlX(item_id, programX, programX, repFile, mButton.Text);
                                c = new OneGridControl(item_id, formname);
                            }
                            break;

                        case "E": // 2 GridView / form readonly
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                c = new TwoGridControl(item_id, formname);
                            }
                            break;

                        case "F": // Nhân sự view
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                            {
                                var programX = codeform.Substring(1);
                                c = NhanSuManager.GetControl(item_id, formname);
                            }
                            break;
                        case "H":
                            #region ==== // Danhmucview format Aldm ====
                            TABLE_NAME = codeform.Substring(1).ToUpper();
                            if (TABLE_NAME == "V6USER")
                            {
                                check = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                            }
                            else if (TABLE_NAME == "ALDM" || TABLE_NAME == "V6LOOKUP" )
                            {
                                check = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
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
                                        if (check_admin)
                                        {
                                            check = new ConfirmPassword().ShowDialog() == DialogResult.OK;
                                        }
                                        else if (check_v6)
                                        {
                                            check = new ConfirmPasswordV6().ShowDialog() == DialogResult.OK;
                                        }
                                    }

                                    if (!check) return null;

                                    c = new DanhMucView(item_id, mButton.Text, TABLE_NAME,
                                        V6Login.GetInitFilter(TABLE_NAME), null, is_aldm)
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
                    else if (item_id == "D0000019" || item_id == "HM000026")
                    {
                        // Thư giãn || Nhân sự
                        c = new NhanSuView(item_id, "Nhân sự", "HLNS");
                    }
                    else if (item_id == "C0108028")
                    {
                        c = new KhoHangControl();
                    }
                    else if (item_id == "C0108027")
                    {
                        c = new ThongTinChuongTrinh();
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
    }
}
