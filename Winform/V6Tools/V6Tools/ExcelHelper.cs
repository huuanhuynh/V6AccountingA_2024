using System;
using System.Data.OleDb;
using System.Data;
using System.IO;


namespace ExcelConvert
{
    public static class ExcelHelper
    {

        static public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static object GetSheet1Name(string excelFileName)
        {
            OleDbConnection connection = new OleDbConnection();
            try
            {
                connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + excelFileName);
                connection.Open();
                //DataTable datbl = connection.GetSchema("Tables");
                string myTableName = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();

                //string tblname = connection.GetSchema("Tables").Rows[0][
                //DataTable tbl = connection.GetOleDbSchemaTable(Guid.NewGuid(), null);
                connection.Close();
                return myTableName;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }
        public static DataTable ReadExcelSheet1TableContents(string fileName)
        {
            return ReadExcelSheet1Contents(fileName).Tables[0];
        }
        public static DataSet ReadExcelSheet1Contents(string fileName)
        {
            OleDbConnection connection = new OleDbConnection();
            try
            {
                string ext = Path.GetExtension(fileName);//.ext
                string ConnectString = string.Empty;
                switch (ext.ToLower())
                {
                    case ".xls":
                        ConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + "; Extended Properties='Excel 8.0;HDR=YES'";
                        //;ConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES' ";
                        break;
                    case ".xlsx":
                        ConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES' ";
                        break;
                    case ".xlsb":
                        ConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES' ";
                        break;
                    case ".xlsm":
                        ConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0 Macro;HDR=YES' ";
                        break;
                    default:
                        throw new NotSupportedException("Không hỗ trợ kiểu tập tin [" + ext + "]!");
                }

                connection = new OleDbConnection(ConnectString);
                connection.Open();
                string sheet1Name = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                string excelQuery = @"Select * FROM [" + sheet1Name + "]";
                OleDbCommand cmd = new OleDbCommand(excelQuery, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                foreach (DataColumn cl in dt.Columns)
                {
                    cl.ColumnName = cl.ColumnName.Replace('#', '.');
                }
                connection.Close();

                return ds;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }
    }
}
