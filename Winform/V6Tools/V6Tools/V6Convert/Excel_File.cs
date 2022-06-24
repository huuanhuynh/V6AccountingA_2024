using System;
using System.Data;
using System.IO;

namespace V6Tools.V6Convert
{
    public class Excel_File
    {
                /// <summary>
        /// Đọc sheet1 của file excel vào bảng dữ liệu.
        /// </summary>
        /// <param name="fileName">Đường dẫn file excel xls xlsx.</param>
        /// <param name="beginRow">Dòng bắt đầu, bắt đầu là 0.</param>
        /// <param name="maxRowCountLimit">Số dòng dữ liệu giới hạn.</param>
        /// <returns></returns>
        public static DataTable Sheet1ToDataTable(string fileName, int beginRow, int maxRowCountLimit)
        {
            if (beginRow < 0) beginRow = 0;
            DataTable sheetTable = new DataTable();
            try
            {
                string ext = Path.GetExtension(fileName).ToLower();
                SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();

                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {
                            if (ext == ".xls")
                                workbook.read(fileName);
                            else if (ext == ".xlsx")
                                workbook.readXLSX(fileName);
                            else if (ext == ".xml")
                                workbook.readXML(fileName);
                        }
                        catch
                        {
                            workbook.readXLSX(fileName);
                        }
                    }
                    catch
                    {
                        workbook.readXML(fileName);
                    }
                }
                catch
                {
                    workbook.read(fileName);
                }

                #endregion

                //int sheetCount = workbook.NumSheets;
                //string text;

                int sheetIndex = 0;

                //select sheet
                workbook.Sheet = sheetIndex;

                string sheetName = workbook.getSheetName(sheetIndex);
                sheetTable = new DataTable(sheetName);

                int lastRow = workbook.LastRow;
                if (lastRow < 1)
                    return sheetTable;
                int getRowCount = lastRow - beginRow + 1;
                if (maxRowCountLimit > 0 && getRowCount > maxRowCountLimit)
                {
                    getRowCount = maxRowCountLimit;
                }
                int lastCol = workbook.LastCol;
                if (lastCol < 1)
                    return sheetTable;

                sheetTable = workbook.ExportDataTable(beginRow, 0, getRowCount, lastCol + 1, true, true);

                return sheetTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Sheet1ToDataTable " + ex.Message);
            }
            return sheetTable;
        }

        /// <summary>
        /// Đọc dữ liệu trong Sheet1
        /// </summary>
        /// <param name="fileName">Tên file Excel.</param>
        /// <param name="beginRow">Dòng bắt đầu, 0 base.</param>
        /// <returns></returns>
        public static DataTable Sheet1ToDataTable(string fileName, int beginRow = 0)
        {
            if (beginRow < 0) beginRow = 0;
            DataTable sheetTable = new DataTable();
            try
            {
                string ext = Path.GetExtension(fileName).ToLower();
                SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();
                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {

                            if (ext == ".xls")
                                workbook.read(fileName);
                            else if (ext == ".xlsx")
                                workbook.readXLSX(fileName);
                            else if (ext == ".xml")
                                workbook.readXML(fileName);
                        }
                        catch
                        {
                            workbook.readXLSX(fileName);
                        }
                    }
                    catch
                    {
                        workbook.readXML(fileName);
                    }
                }
                catch
                {
                    workbook.read(fileName);
                }
                #endregion

                //int sheetCount = workbook.NumSheets;
                //string text;

                int sheetIndex = 0;
                {

                    //select sheet
                    workbook.Sheet = sheetIndex;

                    string sheetName = workbook.getSheetName(sheetIndex);
                    sheetTable = new DataTable(sheetName);

                    int lastRow = workbook.LastRow; if (lastRow < 1)
                        return sheetTable;
                    int getRowCount = lastRow - beginRow + 1;
                    int lastCol = workbook.LastCol; if (lastCol < 1)
                        return sheetTable;

                    sheetTable = workbook.ExportDataTable(beginRow, 0, getRowCount, lastCol + 1, true, true);

                    return sheetTable;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Sheet1ToDataTable " + ex.Message);
            }
            return sheetTable;
        }

        public static DataTable AllSheetToDataTable(string fileName)
        {
            DataTable result = new DataTable("AllSheetInOne");
            DataSet ds = ToDataSet(fileName);
            foreach (DataTable table in ds.Tables)
            {
                result.AddRowByTable(table, true);
            }
            return result;
        }

        public static DataTable ToDataTable(string fileName)
        {
            return ToDataSet(fileName).Tables[0];
        }
        public static DataSet ToDataSet(string fileName)
        {
            DataSet result = new DataSet();
            try
            {
                string ext = Path.GetExtension(fileName).ToLower();
                SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();
                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {

                            if (ext == ".xls")
                                workbook.read(fileName);
                            else if (ext == ".xlsx")
                                workbook.readXLSX(fileName);
                            else if (ext == ".xml")
                                workbook.readXML(fileName);
                        }
                        catch
                        {
                            workbook.readXLSX(fileName);
                        }
                    }
                    catch
                    {
                        workbook.readXML(fileName);
                    }
                }
                catch
                {
                    workbook.read(fileName);
                }
                #endregion

                int sheetCount = workbook.NumSheets;

                string t;

                for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                {
                    //select sheet
                    workbook.Sheet = sheetIndex;
                    string sheetName = workbook.getSheetName(sheetIndex);
                    DataTable sheetTable = new DataTable(sheetName);

                    //0base
                    int lastRow = workbook.LastRow; if (lastRow < 1) continue;
                    int lastCol = workbook.LastCol; if (lastCol < 1) continue;

                    sheetTable = workbook.ExportDataTable(0, 0, lastRow + 1, lastCol + 1, true, true);
                    result.Tables.Add(sheetTable);
                    continue;

                    //Tao tieu de table tu row 0
                    for (int col = 0; col <= lastCol; col++)
                    {
                        t = workbook.getText(0, col);
                        if (sheetTable.Columns.Contains(t))
                        {
                            sheetTable.Columns.Add(t + col);
                        }
                        else
                        {
                            sheetTable.Columns.Add(t);
                        }
                    }

                    //Lay du lieu tu row 1
                    for (int rowIndex = 1; rowIndex <= lastRow; rowIndex++)
                    {
                        //get the last column of this row.
                        int lastColForRow = workbook.getLastColForRow(rowIndex);
                        DataRow row = sheetTable.NewRow();
                        for (int colIndex = 0; colIndex <= lastColForRow; colIndex++)
                        {
                            t = workbook.getText(rowIndex, colIndex);
                            row[colIndex] = t;
                        }
                        sheetTable.Rows.Add(row);
                    }
                    result.Tables.Add(sheetTable);

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("ExcelToDataSet " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Thực hiện đổi mã, lưu file (ghi đè) và trả về DataSet
        /// </summary>
        /// <param name="fileName">file nguồn.</param>
        /// <param name="from">mã nguồn</param>
        /// <param name="to">mã đích</param>
        /// <param name="saveFile">file đích.</param>
        /// <returns>DataSet</returns>
        public static DataSet ChangeCode(string fileName, string from, string to, string saveFile)
        {
            DataSet result = new DataSet();
            try
            {
                string ext = Path.GetExtension(fileName).ToLower();
                string saveext = Path.GetExtension(saveFile).ToLower();
                SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();
                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {

                            if (ext == ".xls")
                                workbook.read(fileName);
                            else if (ext == ".xlsx")
                                workbook.readXLSX(fileName);
                            else if (ext == ".xml")
                                workbook.readXML(fileName);
                        }
                        catch
                        {
                            workbook.readXLSX(fileName);
                        }
                    }
                    catch
                    {
                        workbook.readXML(fileName);
                    }
                }
                catch
                {
                    workbook.read(fileName);
                }
                #endregion

                int sheetCount = workbook.NumSheets;

                string cellText;

                for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                {
                    //select sheet
                    workbook.Sheet = sheetIndex;
                    string sheetName = workbook.getSheetName(sheetIndex);
                    DataTable sheetTable = new DataTable(sheetName);

                    //0base
                    int lastRow = workbook.LastRow; if (lastRow < 1) continue;
                    int lastCol = workbook.LastCol; if (lastCol < 1) continue;

                    //Lay du lieu tu row 1
                    for (int rowIndex = 1; rowIndex <= lastRow; rowIndex++)
                    {
                        //get the last column of this row.
                        int lastColForRow = workbook.getLastColForRow(rowIndex);
                        for (int colIndex = 0; colIndex <= lastColForRow; colIndex++)
                        {
                            cellText = workbook.getText(rowIndex, colIndex);
                            if (!string.IsNullOrEmpty(cellText) && NaN(cellText))
                            {
                                var cellText2 = ChuyenMaTiengViet.VIETNAM_CONVERT(cellText, from, to);
                                if (cellText2 != cellText) workbook.setText(rowIndex, colIndex, cellText2);
                            }
                        }
                    } // finish sheet

                    sheetTable = workbook.ExportDataTable(0, 0, lastRow + 1, lastCol + 1, true, true);
                    result.Tables.Add(sheetTable);

                }// finish all sheets

                if (saveext == ".xls")
                    workbook.write(saveFile);
                else if (saveext == ".xlsx")
                    workbook.writeXLSX(saveFile);
                else workbook.writeXLSX(saveFile + ".xlsx");
            }
            catch (Exception ex)
            {
                throw new Exception("ExcelToDataSet " + ex.Message);
            }
            return result;
        }

        private static bool NaN(string s)
        {
            if (s == null) return false;
            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c)) continue;
                if (!char.IsNumber(c)) return true;
            }

            return false;
        }

        /// <summary>
        /// Lấy index dòng trong địa chỉ ô excel. vd A1 là dòng 0
        /// </summary>
        /// <param name="A1"></param>
        /// <returns></returns>
        public static int GetExcelRow(string A1)
        {
            int startIndex = A1.IndexOfAny("0123456789".ToCharArray());
            //string column = A1.Substring(0, startIndex);
            string rowID = A1.Substring(startIndex);
            return int.Parse(rowID) - 1;
        }
        /// <summary>
        /// Lấy index cột trong địa chỉ ô excel. vd: A1 là cột 0
        /// </summary>
        /// <param name="A1"></param>
        /// <returns></returns>
        public static int GetExcelColumn(string A1)
        {
            int startIndex = A1.IndexOfAny("0123456789".ToCharArray());
            string str = A1.Substring(0, startIndex).ToUpper();

            if (string.IsNullOrEmpty(str)) throw new Exception("Không có giá trị!");
            if (str.Length > 2) throw new Exception("Quá dài!");

            str = str.ToUpper();
            if (str.Length == 1)
                return str[0] - 'A';

            const int max = ('Z' - 'A' + 1) * ('I' - 'A' + 1) + ('V' - 'A');
            var result = ('Z' - 'A' + 1) * (str[0] - 'A' + 1) + (str[1] - 'A');
            if (result > max) throw new Exception("Giá trị lớn nhất là IV = " + max + ". (A=0)");
            return result;
        }

        public static int ToExcelColumnIndex(string str)
        {
            //max = IV
            if (string.IsNullOrEmpty(str)) throw new Exception("Không có giá trị!");
            if (str.Length > 2) throw new Exception("Quá dài!");

            str = str.ToUpper();
            if (str.Length == 1)
                return str[0] - 'A';

            const int max = ('Z' - 'A' + 1) * ('I' - 'A' + 1) + ('V' - 'A');
            var result = ('Z' - 'A' + 1) * (str[0] - 'A' + 1) + (str[1] - 'A');
            if (result > max) throw new Exception("Giá trị lớn nhất là IV = " + max + ". (A=0)");
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socot">socot = columnIndex + 1</param>
        /// <returns></returns>
        public static string ToExcelColumnString(int socot)
        {
            int a = 'Z' - 'A' + 1;// = 26// là bảng chữ cái có 26 chữ cái đó mà :D
            string cell2range = "";
            var last = false;
            if (socot%26 == 0) last = true;
            if (socot > 26)//Nếu nhiều hơn bảng chữ cái (ASCII 'A' 65)
            {
                cell2range = Convert.ToChar((socot / 26) - (last ? 1 : 0) + 64).ToString();
            }
            if (last)
            {
                cell2range += "Z";
            }
            else
            {
                cell2range += Convert.ToChar((socot % 26) + 64);
            }
            
            return cell2range;
        }
    }
}
