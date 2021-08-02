
using V6ControlManager.FormManager.ReportManager.Filter;

namespace V6ControlManager.FormManager.NhanSu.Filter
{
    public static class NhanSuFilterManager
    {
        public static FilterBase GetFilterControl(string program)
        {
            program = program.Trim().ToUpper();
            //switch (program)
            //{
            //    #region ==== Nhân sự ====
            //    case "HHRAPPFAMILY":
            //        return new HRAPPFAMILY_filter();
            //    case "HHRIMAGES":
            //        return new HRIMAGES_filter();
            //    case "HHRINFOR1":
            //        return new HHRINFOR1_filter();
            //    case "HHRINFOR2":
            //        return new HHRINFOR2_filter();
            //    case "HHRHEALTHPROFILE":
            //        return new HHRHEALTHPROFILE_filter();
            //    case "HHRJOBEXPERIENCE":
            //        return new HHRJOBEXPERIENCE_filter();
            //    case "HHRJOBEXPERIENCE2":
            //        return new HHRJOBEXPERIENCE2_filter();

            //        #endregion a
            //}
            if (program.StartsWith("H")) return new MA_NS_filter();

            return new FilterBase();
        }
    }
}
