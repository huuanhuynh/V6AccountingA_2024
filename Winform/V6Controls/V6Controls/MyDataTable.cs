using System.Data;

namespace V6Controls
{
    public class MyDataTable
    {
        string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        DataTable objTable;
        public DataTable ObjTable
        {
            get { return objTable; }
            set { objTable = value; }
        }

        public MyDataTable(string tableName, DataTable table)
        {
            this.tableName = tableName;
            this.objTable = table;
        }

        public MyDataTable(string tableName)
        {
            this.tableName = tableName;
        }
    }
}
