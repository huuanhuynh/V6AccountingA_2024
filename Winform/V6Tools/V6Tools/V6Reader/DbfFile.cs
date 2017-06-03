using System.Data;

namespace V6Tools.V6Reader
{
    public class DbfFile
    {
        public static DataTable ToDataTable(string fileName)
        {
            return ParseDBF.ReadDBF(fileName);
        }
    }
}
