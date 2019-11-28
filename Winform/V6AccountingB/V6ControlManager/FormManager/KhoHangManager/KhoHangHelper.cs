using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.KhoHangManager
{
    public static class KhoHangHelper
    {
        private static bool _is_M_VITRI_CODEDAY_INDEX_analysis = false;

        public static int MaKho_StartIndex = 0;
        public static int MaKho_Length = 1;

        public static int CodeDay_StartIndex = 1;
        public static int CodeDay_Length = 2;

        public static int CodeKe_StartIndex = 4;
        public static int CodeKe_Length = 2;

        public static int CodeVitri_StartIndex = 7;
        public static int CodeVitri_Length = 2;

        public static void Get_M_VITRI_CODEDAY_INDEX_Config()
        {
            try
            {
                if (_is_M_VITRI_CODEDAY_INDEX_analysis) return;
                var ssss = ObjectAndString.SplitString(V6Options.M_VITRI_CODEDAY_INDEX);
                
                var ss0 = ObjectAndString.SplitString(ssss[0]);
                MaKho_StartIndex = ObjectAndString.ObjectToInt(ss0[0]);
                MaKho_Length = ObjectAndString.ObjectToInt(ss0[1]);
                
                var ss1 = ObjectAndString.SplitString(ssss[1]);
                CodeDay_StartIndex = ObjectAndString.ObjectToInt(ss1[0]);
                CodeDay_Length = ObjectAndString.ObjectToInt(ss1[1]);
                
                var ss2 = ObjectAndString.SplitString(ssss[2]);
                CodeKe_StartIndex = ObjectAndString.ObjectToInt(ss2[0]);
                CodeKe_Length = ObjectAndString.ObjectToInt(ss2[1]);

                var ss3 = ObjectAndString.SplitString(ssss[3]);
                CodeVitri_StartIndex = ObjectAndString.ObjectToInt(ss3[0]);
                CodeVitri_Length = ObjectAndString.ObjectToInt(ss3[1]);

                _is_M_VITRI_CODEDAY_INDEX_analysis = true;
            }
            catch (System.Exception)
            {
                //
            }
        }

        public static string GetMaKHo(string mavitri)
        {
            if (string.IsNullOrEmpty(mavitri)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            if (mavitri.Length < MaKho_StartIndex + MaKho_Length) return "";
            string result = mavitri.Substring(MaKho_StartIndex, MaKho_Length);
            return result.ToUpper();
        }
        
        public static string GetCodeDay(string mavitri)
        {
            if (string.IsNullOrEmpty(mavitri)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            if (mavitri.Length < CodeDay_StartIndex + CodeDay_Length) return "";
            string result = mavitri.Substring(CodeDay_StartIndex, CodeDay_Length);
            return result.ToUpper();
        }
        public static string GetCodeDay_FromCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            string mavitri = "______________________________".Substring(0, MaKho_Length) + code;
            if (mavitri.Length < CodeDay_StartIndex + CodeDay_Length) return "";
            string result = mavitri.Substring(CodeDay_StartIndex, CodeDay_Length);
            return result.ToUpper();
        }

        public static string GetCodeKe(string mavitri)
        {
            if (string.IsNullOrEmpty(mavitri)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            if (mavitri.Length < CodeKe_StartIndex + CodeKe_Length) return "";
            string result = mavitri.Substring(CodeKe_StartIndex, CodeKe_Length);
            return result.ToUpper();
        }
        public static string GetCodeKe_FromCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            string mavitri = "______________________________".Substring(0, MaKho_Length) + code;
            if (mavitri.Length < CodeKe_StartIndex + CodeKe_Length) return "";
            string result = mavitri.Substring(CodeKe_StartIndex, CodeKe_Length);
            return result.ToUpper();
        }


        public static string GetCodeVitri(string mavitri)
        {
            if (string.IsNullOrEmpty(mavitri)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            if (mavitri.Length < CodeVitri_StartIndex + CodeVitri_Length) return "";
            string result = mavitri.Substring(CodeVitri_StartIndex, CodeVitri_Length);
            return result.ToUpper();
        }
        public static string GetCodeVitri_FromCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            string mavitri = "______________________________".Substring(0, MaKho_Length) + code;
            if (mavitri.Length < CodeVitri_StartIndex + CodeVitri_Length) return "";
            string result = mavitri.Substring(CodeVitri_StartIndex, CodeVitri_Length);
            return result.ToUpper();
        }

        public static string GetMaVitriShort(string mavitri)
        {
            if (string.IsNullOrEmpty(mavitri)) return "";
            Get_M_VITRI_CODEDAY_INDEX_Config();
            if (mavitri.Length < CodeVitri_StartIndex + CodeVitri_Length) return "";
            string result = mavitri.Substring(0, CodeVitri_StartIndex + CodeVitri_Length);
            return result.ToUpper();
        }
    }
}
