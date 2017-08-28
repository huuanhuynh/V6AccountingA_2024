
using V6ControlManager.FormManager.ReportManager.Filter;

namespace V6ControlManager.FormManager.NhanSu.Filter
{
    public static class NhanSuFilterManager
    {
        public static FilterBase GetFilterControl(string program)
        {
            program = program.Trim().ToUpper();
            switch (program)
            {
                #region ==== Nhân sự ====
                case "HHRAPPFAMILY":
                    return new HRAPPFAMILY_filter();
                case "HHRIMAGES":
                    return new HRIMAGES_filter();
                case "HHRINFOR1":
                    return new HHRINFOR1_filter();
                    

                  #endregion a
            }
            return new FilterBase(){Visible = false};
        }
    }
}
