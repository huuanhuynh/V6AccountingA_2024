using System.Data;
using V6SqlConnect;

namespace V6Init
{
    public static class CorpLang
    {
        private static DataTable lTable = null;
        public static DataTable LanguageTable
        {
            get
            {
                if (lTable == null)
                {
                    DataTable langTable = SqlConnect.Select("CorpLang", "*", "[status]='1'", "", "STT").Data;
                    lTable = langTable;
                }
                return lTable;
            }
        }

    }

}
