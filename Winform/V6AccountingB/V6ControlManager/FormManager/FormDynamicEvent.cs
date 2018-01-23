namespace V6ControlManager.FormManager
{
    /// <summary>
    /// Tập hợp các event động trên form.
    /// </summary>
    public static class FormDynamicEvent
    {
        /// <summary>
        /// Sau khi init control trên form.
        /// </summary>
        public static string INIT = "INIT";

        /// <summary>
        /// Form_Load
        /// </summary>
        public static string INIT2 = "INIT2";
        /// <summary>
        /// Xử lý khác trong nút chức năng menu.
        /// </summary>
        public static string INKHAC = "INKHAC";


        
        public static string AFTERADDSUCCESS = "AFTERADDSUCCESS";
        public static string AFTEREDITSUCCESS = "AFTEREDITSUCCESS";
        public static string AFTERSAVESUCCESS = "AFTERSAVESUCCESS";
        public static string AFTERDELETESUCCESS = "AFTERDELETESUCCESS";
        public static string AFTERLOADDATA = "AFTERLOADDATA";
        public static string AFTERADDFILTERCONTROL = "AFTERADDFILTERCONTROL";

        public static string BEFOREADD = "BEFOREADD";
        public static string BEFOREEDIT = "BEFOREEDIT";
        public static string BEFORESAVE = "BEFORESAVE";
        public static string BEFOREDELETE = "BEFOREDELETE";
        public static string BEFORELOADDATA = "BEFORELOADDATA";
        

        /// <summary>
        /// AAPPR_IND
        /// </summary>
        public static string F9EVENT = "F9EVENT";

        
    }
}
