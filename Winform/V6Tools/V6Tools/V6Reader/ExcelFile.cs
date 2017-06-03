using System;
using System.Data;
using System.IO;

namespace V6Tools.V6Reader
{
    public class ExcelFile
    {
        public static DataTable Sheet1ToDataTable(string fileName)
        {
            return V6Convert.Excel_File.Sheet1ToDataTable(fileName);
            //DataTable sheetTable = new DataTable();
            //try
            //{
            //    string ext = Path.GetExtension(fileName).ToLower();
            //    SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();
            //    #region ==== workbook try to read file ====

            //    try
            //    {
            //        try
            //        {
            //            try
            //            {

            //                if (ext == ".xls")
            //                    workbook.read(fileName);
            //                else if (ext == ".xlsx")
            //                    workbook.readXLSX(fileName);
            //                else if (ext == ".xml")
            //                    workbook.readXML(fileName);
            //            }
            //            catch
            //            {
            //                workbook.readXLSX(fileName);
            //            }
            //        }
            //        catch
            //        {
            //            workbook.readXML(fileName);
            //        }
            //    }
            //    catch
            //    {
            //        workbook.read(fileName);
            //    }
            //    #endregion

            //    int sheetCount = workbook.NumSheets;
            //    string t;

            //    int sheetIndex = 0;
            //    {
            //        //select sheet
            //        workbook.Sheet = sheetIndex;

            //        string sheetName = workbook.getSheetName(sheetIndex);
            //        sheetTable = new DataTable(sheetName);

            //        int lastRow = workbook.LastRow; if (lastRow < 1)
            //            return sheetTable;
            //        int lastCol = workbook.LastCol; if (lastCol < 1)
            //            return sheetTable;

            //        //Tao tieu de table tu row 0
            //        for (int col = 0; col <= lastCol; col++)
            //        {
            //            t = workbook.getText(0, col);
            //            if (sheetTable.Columns.Contains(t))
            //            {
            //                sheetTable.Columns.Add(t + col);
            //            }
            //            else
            //            {
            //                sheetTable.Columns.Add(t);
            //            }
            //        }
            //        //Lay du lieu tu row 1
            //        for (int rowIndex = 1; rowIndex <= lastRow; rowIndex++)
            //        {
            //            //get the last column of this row.
            //            int lastColForRow = workbook.getLastColForRow(rowIndex);
            //            DataRow row = sheetTable.NewRow();
            //            for (int colIndex = 0; colIndex <= lastColForRow; colIndex++)
            //            {
            //                t = workbook.getText(rowIndex, colIndex);
            //                row[colIndex] = t;
            //            }
            //            sheetTable.Rows.Add(row);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Sheet1ToDataTable " + ex.Message);
            //}
            //return sheetTable;
        }

        public static DataTable ToDataTable(string fileName)
        {
            return Sheet1ToDataTable(fileName);
        }
        public static DataSet ToDataSet(string fileName)
        {
            return V6Convert.Excel_File.ToDataSet(fileName);
        }
    }
}
