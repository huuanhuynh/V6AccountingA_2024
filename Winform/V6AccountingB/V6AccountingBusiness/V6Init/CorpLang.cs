using System;
using System.Data;
using System.Reflection;
using V6SqlConnect;

namespace V6Init
{
    /// <summary>
    /// Danh sách ngôn ngữ.
    /// </summary>
    public static class CorpLang
    {
        private static DataTable lTable = null;
        public static DataTable LanguageTable
        {
            get
            {
                try
                {
                    if (lTable == null)
                    {
                        DataTable langTable = SqlConnect.Select("CorpLang", "*", "[status]='1'", "", "STT").Data;
                        lTable = langTable;
                    }
                    return lTable;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodBase.GetCurrentMethod().Name + ":" + ex.Message, ex);
                }
            }
        }

    }

}
