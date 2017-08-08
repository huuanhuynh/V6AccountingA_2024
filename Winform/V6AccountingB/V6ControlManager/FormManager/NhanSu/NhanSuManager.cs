using System.Data;
using System.Windows.Forms;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.NhanSu.ThongTin;
using V6ControlManager.FormManager.SoDuManager.FirstFilter;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.NhanSu;
using V6Init;

namespace V6ControlManager.FormManager.NhanSu
{
    public static class NhanSuManager
    {
        public static V6Control GetControl(string itemID, string code)
        {
            switch (code)
            {
                case "HLNS":
                    return new NhanSuView(itemID, "Nhân sự", "HRPERSONAL");
                case "HLNSCT":
                    return new NhanSuView2(itemID, "Nhân sự 2", "HRPERSONAL");

                case "KTH":
                    return new CacKhoanThuongControl();
                case "KGT":
                    return new CacKhoanGiamTruControl();
                case "KLP":
                    return new KhoanLuongPhu();
                case "HHRAPPFAMILY":
                    return new ThongTinLyLichForm();
                case "HINFOR_NS":
                    return new HINFOR_NS();
                case "HHRIMAGES":
                    return new ThongTinHinhAnhChuKy();

                    
                default:
                    
                    break;
            }
            return null;
        }

        public static V6Control GenControl(Control owner, DataRowView mButton, string maNhanSu, MouseEventArgs e = null)
        {
            
            string item_id = mButton["ItemID"].ToString().Trim();
            var codeform = ("" + mButton["Codeform"]).Trim();
            string bar_text = mButton[V6Setting.IsVietnamese ? "vbar" : "vbar2"].ToString().Trim();

            var repFile = mButton["Rep_File"].ToString().Trim();
            var repTitle = mButton["Title"].ToString().Trim();
            var repTitle2 = mButton["Title2"].ToString().Trim();

            var repFileF5 = mButton["Rep_FileF5"].ToString().Trim();
            var repTitleF5 = mButton["TitleF5"].ToString().Trim();
            var repTitle2F5 = mButton["Title2F5"].ToString().Trim();

            V6Control c = null;

            if (!string.IsNullOrEmpty(codeform))
            {
                var code = codeform.Substring(0, 1);
                switch (code)
                {
                    case "1":

                        code = codeform.Substring(1);
                        if (code == "KTH")
                        {
                            c = new CacKhoanThuongControl(maNhanSu);
                        }
                        else if (code == "KGT")
                        {
                            c = new CacKhoanGiamTruControl(maNhanSu);
                        }
                        else if (code == "KLP")
                        {
                            c = new KhoanLuongPhu(maNhanSu);
                        }
                        else
                        {
                            if (V6Login.UserRight.AllowRun(item_id, codeform))
                                c = ChungTu.GetChungTuContainer(code, item_id);
                            else
                                c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);
                        }
                        break;
                    case "2": //DanhMucView

                        var TABLE_NAME = codeform.Substring(1).ToUpper();
                        var check = true;
                        if (TABLE_NAME == "V6USER")
                        {
                            check = new ConfirmPassword().ShowDialog(owner) == DialogResult.OK;
                        }
                        else if (TABLE_NAME == "ALDM" || TABLE_NAME == "V6LOOKUP")
                        {
                            check = new ConfirmPasswordV6().ShowDialog(owner) == DialogResult.OK;
                        }

                        if (V6Login.UserRight.AllowRun(item_id, codeform))
                        {
                            if (check)
                            {

                                var where = "";
                                if (TABLE_NAME == "ALKC")
                                {
                                    var filterForm = new YearFilterForm(TABLE_NAME);
                                    if (filterForm.ShowDialog(owner) == DialogResult.OK)
                                    {

                                        where = filterForm.QueryString;
                                        c = new DanhMucView(item_id, bar_text, TABLE_NAME,
                                            V6Login.GetInitFilter(TABLE_NAME), null, false)
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
                                    c = new DanhMucView(item_id, bar_text, TABLE_NAME,
                                        V6Login.GetInitFilter(TABLE_NAME), null, false)
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
                            c = new V6UserControlNoRight("Key:" + item_id + "; Codeform:" + codeform);


                        break;
                    default:
                        c = new V6UserControlEmpty("Key:" + item_id + "; Codeform:" + codeform);
                        break;
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
    }
}
