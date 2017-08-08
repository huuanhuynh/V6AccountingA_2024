using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using SmartXLS;

namespace V6Tools

{
    public class Export
    {
        public static int totalRows = 0, converted = 0, exported = 0, currentSheet = 0, totalSheet = 0;
        public static string currentSheetName = "";
        static void ResetStaticValues()
        {
            totalRows = 0; converted = 0; exported = 0; currentSheet = 0;
            currentSheetName = "";
        }
        static string FontNameSelect(string fontName, string convertTo)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                if (convertTo.StartsWith("U"))
                {
                    fontName = "Times New Roman";
                }
                else if (convertTo.StartsWith("A") || convertTo.StartsWith("TCVN"))
                {
                    fontName = ".VnArial";
                }
                else if (convertTo.StartsWith("V"))
                {
                    fontName = "VNI-Times";
                }
            }
            //else
            //{
            //    convertTo = convertTo.ToUpper();
            //    if (!IsFontInstalled(fontName))
            //    {
            //        if (convertTo.StartsWith("U"))
            //        {
            //            fontName = "Times new roman";
            //        }
            //        else if (convertTo.StartsWith("A") || convertTo.StartsWith("TCVN"))
            //        {
            //            fontName = ".VnArial";
            //        }
            //        else if (convertTo.StartsWith("V"))
            //        {
            //            fontName = "VNI-Times";
            //        }
            //    }
            //}
            return fontName;
        }

        public static void ExportToSql(DataTable data, string tableName2, string sqlConString, string strFrom, string strTo)
        {
            ////Nếu chưa có thì tạo bảng
            //if (!existTable(tableName2, sqlConString))
            //{
            //    SqlTableCreator stc = new SqlTableCreator(new SqlConnection(sqlConString));
            //    stc.DestinationTableName = tableName;
            //    int primari = 0;
            //    for (int i = 0; i < data.Columns.Count; i++)
            //    {
            //        if (data.Columns[i].ColumnName.ToUpper() == strPrimarykey.ToUpper())
            //        {
            //            primari = i;
            //            data.PrimaryKey = new DataColumn[] { data.Columns[strPrimarykey]};
            //            break;
            //        }
            //    }
            //    stc.Connection.Open();
            //    //data.TableName = tableName2;
            //    stc.DestinationTableName = tableName2;
            //    stc.CreateFromDataTable(data);
            //}
            if (!string.IsNullOrEmpty(strFrom) && !string.IsNullOrEmpty(strTo))
            {
                data = V6Tools.Export.ConvertTable(data, strFrom, strTo);
            }
            using (SqlConnection connection = new SqlConnection(sqlConString))
            {
                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                bulkCopy.DestinationTableName = tableName2;
                connection.Open();

                bulkCopy.WriteToServer(data);
                connection.Close();
            }
            MessageBox. Show("Hoàn tất cập nhập " + tableName2 + ".");
        }


        #region ==== Export to Excel3.0 C#Library ====
        //public static void ExportDataToExcel_C_Lib(DataTable data, string fileName, string convertFrom, string convertTo, string fontName)
        //{
        //    #region ==== Font ====
        //    if (string.IsNullOrEmpty(fontName))
        //    {

        //        switch (convertTo)
        //        {
        //            case "U":
        //            case "UNI":
        //            case "UNICODE":
        //                {
        //                    fontName = "Times New Roman";
        //                    break;
        //                }
        //            case "A":
        //            case "ABC":
        //            case "TCVN":
        //            case "TCVN3":
        //                {
        //                    fontName = ".VnTime";
        //                    break;
        //                }
        //            case "V":
        //            case "VNI":
        //                {
        //                    fontName = "VNI-Times";
        //                    break;
        //                }
        //            default:
        //                fontName = "Times New Roman";
        //                break;
        //        }
        //    }
        //    #endregion
        //    totalRows = data.Rows.Count;
        //    converted = 0;
        //    exported = 0;
        //    DataTable convertTable = ConvertTable(data, convertFrom, convertTo);
        //    ExportDataToExcel_C_Lib(convertTable, fileName, fontName);
        //}
        //public static void ExportDataToExcel_C_Lib(DataTable data, string fileName, string fontName)
        //{


        //    XLS3Export.ExcelDocument eDocument =
        //        new XLS3Export.ExcelDocument(
        //            new System.Drawing.Font(fontName ?? "Times new roman", 12));

        //    eDocument.UserName = "V6Soft-Anhh";
        //    eDocument.CodePage = CultureInfo.CurrentCulture.TextInfo.ANSICodePage;            

        //    //eDocument.ColumnWidth(0, 120);
        //    //eDocument.ColumnWidth(1, 80);
        //    //The first row
        //    for(int i = 0; i< data.Columns.Count; i ++)
        //    {
        //        eDocument.WriteCell(0, i, data.Columns[i].ColumnName);
        //    }

        //    // Next rows
        //    exported = 0;
        //    for (int r = 0; r < data.Rows.Count; r++)
        //    {
        //        for (int c = 0; c < data.Columns.Count; c++)
        //        {
        //            eDocument.WriteCell(r+1, c, data.Rows[r][c]);
        //        }
        //        exported++;
        //    }
        //    //eDocument[0, 0].Value = "ExcelWriter Demo";
        //    //eDocument[0, 0].Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
        //    //eDocument[0, 0].ForeColor = XLSExport.ExcelColor.DarkRed;
        //    //eDocument[0, 0].Alignment = XLSExport.Alignment.Centered;
        //    //eDocument[0, 0].BackColor = XLSExport.ExcelColor.Silver;

        //    //eDocument.WriteCell(1, 0, "int");
        //    //eDocument.WriteCell(1, 1, 10);

        //    //eDocument.Cell(2, 0).Value = "double";
        //    //eDocument.Cell(2, 1).Value = 1.5;

        //    //eDocument.Cell(3, 0).Value = "date";
        //    //eDocument.Cell(3, 1).Value = DateTime.Now;
        //    //eDocument.Cell(3, 1).Format = @"dd/mm/yyyy";

        //    FileStream stream = new FileStream(fileName, FileMode.Create);
        //    try
        //    {
        //        eDocument.Save(stream);
        //        stream.Close();
        //    }
        //    catch { stream.Close(); }
            
        //}
        #endregion ==== Export to Excel C#Library ====

        #region ==== Convert Excel SX and ExDataReader Library ================================================

        public static void ConvertExcelUseSX(string fileName, string saveAs, string from, string to,
            string fontName, string U_L)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            ResetStaticValues();
            var s = Path.GetExtension(fileName);
            if (s != null)
            {
                string ext = s.ToLower();
                try
                {
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

                    fontName = FontNameSelect(fontName, to);


                    //iterator all cell's data in the workbook(sheet by sheet,row by row)

                    int numsheets = workbook.NumSheets;
                    if (string.IsNullOrEmpty(U_L))
                    {
                        for (int sheetIndex = 0; sheetIndex < numsheets; sheetIndex++)
                        {

                            //select sheet
                            workbook.Sheet = sheetIndex;
                            currentSheet++;
                            string sheetName = workbook.getSheetName(sheetIndex);
                            currentSheetName = sheetName;

                            int lastRow = workbook.LastRow; if (lastRow < 1) continue;
                            totalRows = lastRow;
                            int lastCol = workbook.LastCol; if (lastCol < 1) continue;

                            RangeStyle rangeStyle = workbook.getRangeStyle(0, 0, lastRow, lastCol);
                            rangeStyle.FontName = fontName;
                            workbook.setRangeStyle(rangeStyle, 0, 0, lastRow, lastCol);

                            for (int rowIndex = 0; rowIndex <= lastRow; rowIndex++)
                            {
                                //get the last column of this row.
                                int lastColForRow = workbook.getLastColForRow(rowIndex);
                                for (int colIndex = 0; colIndex <= lastColForRow; colIndex++)
                                {
                                    //double n;
                                    string t;//, f;
                                    int type = workbook.getType(rowIndex, colIndex);
                                    if (type == WorkBook.TypeText)
                                    {
                                        t = workbook.getText(rowIndex, colIndex);
                                        t = ChuyenMaTiengViet.VIETNAM_CONVERT(t, @from, to);
                                        workbook.setText(rowIndex, colIndex, t);
                                    }

                                }
                                converted++;
                            }

                        }
                    }
                    else
                    {
                        for (int sheetIndex = 0; sheetIndex < numsheets; sheetIndex++)
                        {
                            //select sheet
                            workbook.Sheet = sheetIndex;
                            string sheetName = workbook.getSheetName(sheetIndex);
                            int lastRow = workbook.LastRow; if (lastRow < 1) continue;
                            int lastCol = workbook.LastCol; if (lastCol < 1) continue;

                            RangeStyle rangeStyle = workbook.getRangeStyle(0, 0, lastRow, lastCol);
                            rangeStyle.FontName = fontName;
                            workbook.setRangeStyle(rangeStyle, 0, 0, lastRow, lastCol);

                            for (int rowIndex = 0; rowIndex <= lastRow; rowIndex++)
                            {
                                //get the last column of this row.
                                int lastColForRow = workbook.getLastColForRow(rowIndex);
                                for (int colIndex = 0; colIndex <= lastColForRow; colIndex++)
                                {
                                    //double n;
                                    string t, f;
                                    int type = workbook.getType(rowIndex, colIndex);
                                    if (type < 0)
                                    {
                                        f = workbook.getFormula(rowIndex, colIndex);
                                        type -= 0;
                                    }

                                    if (type == WorkBook.TypeText)
                                    {
                                        t = workbook.getText(rowIndex, colIndex);
                                        t = ChuyenMaTiengViet.VIETNAM_CONVERT(t, @from, to);

                                        U_L = U_L.ToUpper();
                                        if (U_L == "U")
                                            t = ChuyenMaTiengViet.HoaDauTuThuongCuoiTu(t);
                                        else if (U_L == "UA")
                                            t = ChuyenMaTiengViet.HoaTatCa(t);
                                        else if (U_L == "LA")
                                            t = ChuyenMaTiengViet.ThuongTatCa(t);
                                        else if (U_L == "L")
                                            t = ChuyenMaTiengViet.ThuongCuoiTu(t);

                                        workbook.setText(rowIndex, colIndex, t);
                                    }

                                }
                            }
                        }
                    }
                    workbook.Sheet = 0;

                    var extension = Path.GetExtension(saveAs);
                    if (extension != null) ext = extension.ToLower();

                    if (ext == ".xls")
                        workbook.write(saveAs);
                    else if (ext == ".xlsx")
                        workbook.writeXLSX(saveAs);
                    else if (ext == ".cvs")
                        workbook.writeCSV(saveAs);
                    else
                        workbook.write(saveAs);
                }
                catch (Exception ex)
                {
                    MessageBox. Show("Error ConvertExcelSX: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Copy file để tránh lỗi đọc ghi stream
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="saveAs"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="fontName"></param>
        /// <param name="U_L"></param>
        public static void ConvertExcelUseSXCopy(string fileName, string saveAs, string from, string to,
    string fontName, string U_L)
        {
            try
            {
                string ext = Path.GetExtension(fileName);
                string fileCopy = fileName + ".copy" + ext;
                File.Copy(fileName, fileCopy);
                SmartXLS.WorkBook workbook = new SmartXLS.WorkBook();
                workbook.read(fileCopy);

                ConvertExcelUseSX(fileCopy, saveAs, from, to, fontName, U_L);

            }
            catch (Exception ex)
            {
                MessageBox. Show("Error ConvertExcelSXCopy: " + ex.Message);
            }
        }


        public static void ConvertExcelUseReaderandSX(string fileName, string saveAs, string from, string to, string fontName)
        {
            DataTable tb = new DataTable();

            try
            {
                FileStream fs = new FileStream(fileName,
                    FileMode.Open, FileAccess.Read);
                ExcelDataReader.ExcelDataReader rd = new ExcelDataReader.ExcelDataReader(fs);
                fs.Close();
                // Now we can access data:
                DataSet data = rd.WorkbookData;

                tb = ConvertTable(data.Tables[0], from, to);
            }
            catch
            {
                SmartXLS.WorkBook wb = new SmartXLS.WorkBook();
                string ext = Path.GetExtension(fileName).ToLower();
                if (ext == ".xls")
                    wb.read(fileName);
                else if (ext == ".xlsx")
                    wb.readXLSX(fileName);
                tb = ConvertTable(wb.ExportDataTable(), from, to);
            }

            ExportDataTableToExcelUseSX(tb, saveAs, fontName);

        }

        public static void ExportDataTableToExcelUseSX(DataTable data, string fileName, string convertFrom, string convertTo, string fontName)
        {
            #region ==== Font ====
            if (string.IsNullOrEmpty(fontName))
            {

                switch (convertTo)
                {
                    case "U":
                    case "UNI":
                    case "UNICODE":
                        {
                            fontName = "Times New Roman";
                            break;
                        }
                    case "A":
                    case "ABC":
                    case "TCVN":
                    case "TCVN3":
                        {
                            fontName = ".VnTime";
                            break;
                        }
                    case "V":
                    case "VNI":
                        {
                            fontName = "VNI-Times";
                            break;
                        }
                    default:
                        fontName = "Times New Roman";
                        break;
                }
            }
            #endregion
            totalRows = data.Rows.Count;
            converted = 0;
            exported = 0;
            DataTable convertTable = ConvertTable(data, convertFrom, convertTo);
            ExportDataTableToExcelUseSX(convertTable, fileName, fontName);
        }
        static bool IsFontInstalled(string fontName)
        {
            using (var testFont = new System.Drawing.Font(fontName, 8))
            {
                return 0 == string.Compare(
                  fontName,
                  testFont.Name,
                  StringComparison.InvariantCultureIgnoreCase);
            }
        }
        public static void ExportDataTableToExcelUseSX(DataTable data, string saveAs, string fontName)
        {
            SmartXLS.WorkBook wb = new SmartXLS.WorkBook();
            if (fontName == "" || !IsFontInstalled(fontName)) fontName = "Times new roman";
            wb.setDefaultFont(fontName, 12, 0);

            wb.ImportDataTable(data, false, 0, 0, -1, data.Columns.Count);
            //Saving the workbook to disk.
            wb.write(saveAs);

        }
        #endregion ==== Export to Excel C#Library ====


        //#region ==== Export to Excel use Com object ====
        //public static void ExportDataToExcel_Com(DataTable tbl, string fileName, string title,string convertFrom, string convertTo)
        //{
        //    totalRows = tbl.Rows.Count;
        //    converted = 0;
        //    exported = 0;
        //    DataTable convertTable = ConvertTable(tbl, convertFrom, convertTo);
        //    ExportDataToExcel_Com(convertTable, fileName, title);
        //}
        
        //public static void ExportDataToExcel_Com(DataTable data, string fileName, string title)
        //{
        //    totalRows = data.Rows.Count;
        //    //khoi tao cac doi tuong Com Excel de lam viec
        //    Excel.ApplicationClass xlApp;
        //    Excel.Worksheet xlSheet;
        //    Excel.Workbook xlBook;
        //    //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
        //    object missValue = System.Reflection.Missing.Value;
        //    //khoi tao doi tuong Com Excel moi
        //    xlApp = new Excel.ApplicationClass();
        //    xlBook = xlApp.Workbooks.Add(missValue); xlBook.Author = "Anhh-V6Soft";
        //    xlBook.Title = title;
        //    //su dung Sheet dau tien de thao tac
        //    byte sheetCount = 1;
        //    xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(sheetCount);
        //    //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
        //    xlApp.Visible = false;
        //    int socot=data.Columns.Count;
        //    int sohang=data.Rows.Count;
        //    int i,j;

        //    try
        //    {
        //        ////set thuoc tinh cho tieu de
        //        //xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1").Merge(false);
        //        //xlSheet.get_Range(""
        //        //Excel.Range caption = xlSheet.get_Range("A1", Convert.ToChar(socot + 65) + "1");
        //        //caption.Select();
        //        //caption.FormulaR1C1 = tieude;
        //        ////căn lề cho tiêu đề
        //        //caption.HorizontalAlignment = Excel.Constants.xlCenter;
        //        //caption.Font.Bold = true;
        //        //caption.VerticalAlignment = Excel.Constants.xlCenter;
        //        //caption.Font.Size = 15;
        //        ////màu nền cho tiêu đề
        //        //caption.Interior.ColorIndex = 20;
        //        //caption.RowHeight = 30;
        //        //set thuoc tinh cho cac header
        //        //int a = 'Z' - 'A' + 1;// = 26// là bảng chữ cái có 26 chữ cái đó mà :D
        //        //string cell2range = "";
        //        //if(socot > 26)//Nếu nhiều hơn bảng chữ cái (ASCII 'A' 65)
        //        //{
        //        //    cell2range = Convert.ToChar((socot / 26) + 64).ToString();
        //        //}
        //        //cell2range += Convert.ToChar((socot % 26) + 64);

        //        //Excel.Range header = xlSheet.get_Range("A1", cell2range + "1");
        //        //header.Select();//không biết cái chọn này có ích gì!

        //        //header.HorizontalAlignment = Excel.Constants.xlCenter;
        //        //header.Font.Bold = true;
        //        //header.Font.Size = 10;


        //        //điền tiêu đề cho các cột trong file excel// dòng số 1
        //        for (i = 0; i < socot; i++)
        //            xlSheet.Cells[1, i + 1] = data.Columns[i].ColumnName;
                
        //        //dien cot stt, sửa dòng 2 thành dòng 1
        //        //xlSheet.Cells[1, 1] = "STT";
        //        //for (i = 0; i < sohang; i++)
        //        //    xlSheet.Cells[i + 2, 1] = i + 1;

        //        ////dien du lieu vao sheet
        //        int currentRow = 0;
        //        for (i = 0; i < sohang; i++)
        //        {
        //            if (currentRow >= 65530)//////////65536, sử dụng được số tối đa là 65535, 1 dòng đã làm tiêu đề bảng
        //            {
        //                currentRow = 0;
        //                sheetCount++;
        //                xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(sheetCount);
        //                //điền tiêu đề cho các cột trong file excel// dòng số 1
        //                for (int c = 0; c < socot; c++)
        //                    xlSheet.Cells[1, c + 1] = data.Columns[c].ColumnName;
        //            }
        //            //Điền dữ liệu cho mỗi cột trên dòng hiện tại
        //            for (j = 0; j < socot; j++)
        //            {
        //                xlSheet.Cells[currentRow + 2, j + 1] = data.Rows[i][j];
        //            }
        //            exported++;
        //            currentRow++;
        //        }
        //        //autofit độ rộng cho các cột 
        //        for (i = 0; i < socot; i++)
        //            ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

        //        //save file
        //        if (!Path.IsPathRooted(fileName))
        //            fileName = Application.StartupPath + "\\" + fileName;

        //        xlBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                
        //        xlBook.Close(true, missValue, missValue);
        //        xlApp.Quit();

        //        // release cac doi tuong COM
        //        releaseObject(xlSheet);
        //        releaseObject(xlBook);
        //        releaseObject(xlApp);
        //    }
        //    catch
        //    {
        //        //save file

        //        //xlBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
        //        xlBook.Close(true, missValue, missValue);
        //        xlApp.Quit();

        //        // release cac doi tuong COM
        //        releaseObject(xlSheet);
        //        releaseObject(xlBook);
        //        releaseObject(xlApp);
        //        throw;
        //    }
        //}

        

        //static  public void releaseObject(object obj)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //       throw new  Exception("Exception Occured while releasing object " + ex.ToString());
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
        //#endregion ==== Export to Excel use Com object ====

        #region ==== Export CrystalReport Document ====
        //static public bool exportReport(int type,ReportDocument repd)
        //{
        //     SaveFileDialog f  = new SaveFileDialog();
        //     bool result=false;
        //    switch(type)
        //    {
        //        case 1:
             
        //            f.Filter = "Word file(*.doc)|*.doc";
        //            if (f.ShowDialog(null) == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        case 2:
                    
        //            f.Filter = "Pdf file(*.pdf)|*.pdf";
        //            if (f.ShowDialog(null) == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        case 3:
                   
        //            f.Filter = "Excel file(*.xls)|*.xls";
        //            if (f.ShowDialog(null) == DialogResult.OK)
        //            {
        //                repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, f.FileName);
        //                result = true;
        //            }
        //            break;
        //        default:
        //            MessageBox. Show("Khong chon dung loai ");
        //            break;

                    
        //    }
        //    return result;
        //}

        //========================= XLSX =============================
        #endregion ==== Export CrystalReport Document ====

        #region ==== Export to Excel xls, xlsx format use Data access driver  ====
        public static void ExportToXLSX(string saveFileName, DataTable tbl, string tableName,string convertFrom, string convertTo)
        {
            totalRows = tbl.Rows.Count;
            converted = 0;
            exported = 0;         
            DataTable convertTable = ConvertTable(tbl, convertFrom, convertTo);            
            ExportToXLSX(saveFileName, convertTable, tableName);
        }
                
        public static void ExportToXLSX(string saveFileName, DataTable data, string tableName)
        {
            totalRows = data.Rows.Count;
            exported = 0;
            char Space = ' ';
            //adds the closing parentheses to the query string
            if (data.Rows.Count > 65530 && saveFileName.ToLower().EndsWith(".xls"))
            {
                //use Excel 2007 for large sheets.
                saveFileName = saveFileName.ToLower().Replace(".xls", string.Empty) + ".xlsx";
            }
            string dest = saveFileName;

            if (File.Exists(dest))
            {
                File.Delete(dest);
            }

            saveFileName = dest;

            if (tableName == null)
            {
                tableName = string.Empty;
            }

            tableName = tableName.Trim().Replace(Space, '_');
            if (tableName.Length == 0)
            {
                tableName = data.TableName.Replace(Space, '_');
            }

            if (tableName.Length == 0)
            {
                tableName = "NoTableName";
            }

            if (tableName.Length > 30)
            {
                tableName = tableName.Substring(0, 30);
            }

            //Excel names are less than 31 chars
            string queryCreateExcelTable = "CREATE TABLE [" + tableName + "] (";
            //Dictionary<string, string> colNames = new SortedDictionary<string, string>();

            foreach (DataColumn dc in data.Columns)
            {
                //Cause the query to name each of the columns to be created.
                switch (dc.DataType.ToString())
                {
                    case "System.String":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " text,";
                        break;
                    case "System.DateTime":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " datetime,";
                        break;
                    case "System.Boolean":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " LOGICAL,";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " int,";
                        break;
                    case "System.Decimal":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " decimal,";
                        break;
                    case "System.Double":
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " double,";
                        break;
                    default:
                        queryCreateExcelTable += "[" + dc.ColumnName + "]" + " text,";
                        break;
                }

            }

            queryCreateExcelTable = queryCreateExcelTable.TrimEnd(',') + ")";

            string strCn = string.Empty;
            string ext = System.IO.Path.GetExtension(saveFileName).ToLower();
            if (ext == ".xls") strCn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + saveFileName + "; Extended Properties='Excel 8.0;HDR=YES' ";
            if (ext == ".xlsx") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + saveFileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES' ";//<=
            if (ext == ".xlsb") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + saveFileName + ";Extended Properties='Excel 12.0;HDR=YES' ";
            if (ext == ".xlsm") strCn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + saveFileName + ";Extended Properties='Excel 12.0 Macro;HDR=YES' ";

            System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(strCn);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(queryCreateExcelTable, cn);
            cn.Open();
            
            cmd.ExecuteNonQuery();

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + tableName + "]", cn);
            System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(da);

            //creates the INSERT INTO command
            cb.QuotePrefix = "[";
            cb.QuoteSuffix = "]";
            cmd = cb.GetInsertCommand();

            //gets a hold of the INSERT INTO command.
            foreach (DataRow row in data.Rows)
            {
                foreach (System.Data.OleDb.OleDbParameter param in cmd.Parameters)
                {
                    param.Value = row[param.SourceColumn];
                }

                cmd.ExecuteNonQuery(); //INSERT INTO command.
                exported++;
            }
            cn.Close();
            cn.Dispose();
            da.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            exported = totalRows;
        }
        #endregion ==== Export to Excel xls, xlsx format  ====

        #region ==== Export to Excel Xml format ====
        #region exportToExcel() Overload

        public static void exportToExcelxml(DataTable sourceTable, string fileName)
        {
            exportToExcelxml(sourceTable, fileName, "");
        }
        public static void exportToExcelxml(DataTable sourceTable, string fileName, string strTitle)
        {
            exportToExcelxml(sourceTable, fileName, 0, strTitle);
        }
        public static void exportToExcelxml(DataSet sourceDataset, string fileName)
        {
            exportToExcelxml(sourceDataset, fileName, "");
        }
        public static void exportToExcelxml(DataSet sourceDataset, string fileName, string strTitle)
        {
            exportToExcelxml(sourceDataset.Tables[0], fileName, strTitle);
        }
        public static void exportToExcelxml(DataView sourceDataView, string fileName)
        {
            exportToExcelxml(sourceDataView, fileName, "");
        }
        public static void exportToExcelxml(DataView sourceDataView, string fileName, string strTitle)
        {
            exportToExcelxml(sourceDataView.ToTable(), fileName, strTitle);
        }
        public static void exportToExcelxml(DataSet sourceDataset, string fileName, int Decimals)
        {
            exportToExcelxml(sourceDataset.Tables[0], fileName, Decimals);
        }
        public static void exportToExcelxml(DataSet sourceDataset, string fileName, int Decimals, string strTitle)
        {
            exportToExcelxml(sourceDataset.Tables[0], fileName, Decimals, strTitle);
        }
        public static void exportToExcelxml(DataView sourceDataView, string fileName, int Decimals)
        {
            exportToExcelxml(sourceDataView.ToTable(), fileName, Decimals);
        }
        public static void exportToExcelxml(DataView sourceDataView, string fileName, int Decimals, string strTitle)
        {
            exportToExcelxml(sourceDataView.ToTable(), fileName, Decimals, strTitle);
        }
        #endregion

        private static string decimalFormatPlace(int Decimals)//num of places
        {
            //if (Decimals < 0)
            //{
            //    throw new Exception("Parameter is not valid!");
            //}
            if (Decimals == 0)
                return "General";

            string strReturn = "0.";

            if (Decimals >= 0)
            {
                for (int i = 0; i < Decimals; i++)
                {
                    strReturn += "0";
                }
            }
            return strReturn;
        }
        public static void exportToExcelxml(DataTable sourceTable, string fileName, int Decimals)
        {
            exportToExcelxml(sourceTable, fileName, Decimals, "");
        }

        /// <summary>
        /// Xuất dữ liệu bảng ra Excel.xls
        /// </summary>
        /// <param name="sourceTable">Bảng dữ liệu nguồn</param>
        /// <param name="fileName">Tên file sẽ lưu</param>
        /// <param name="Decimals">Định dạng số chữ số thập phân</param>
        public static void exportToExcelxml(DataTable sourceTable, string fileName, int Decimals, string strTitle)
        {
            exportToExcelxml(sourceTable, fileName, Decimals, strTitle, null, null, true);
        }
        public static void exportToExcelxml(DataTable sourceTable, string fileName, int Decimals, string strTitle, string convertFrom, string convertTo, bool _65536_rows_per_sheet)
        {
            totalRows = sourceTable.Rows.Count;
            converted = 0;
            exported = 0;
            if (sourceTable.Rows.Count <= 0)
                throw new Exception("No data!");
            System.IO.StreamWriter excelDoc;

            excelDoc = new System.IO.StreamWriter(fileName);
            string startExcelXML = "<xml version>\r\n<Workbook " +
                  "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                  "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                  "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">\r\n <Styles>\r\n " +
                //==========Add by huuan============
                  "<Style ss:ID=\"Title\">" +
                    "<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>" +
                    "<Borders>" +
                    "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>" +
                    "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>" +
                    "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>" +
                    "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>" +
                    "</Borders>" +
                    "<Interior ss:Color=\"#FFFF00\" ss:Pattern=\"Solid\"/>" +
                    "<Font x:Family=\"Arial\" ss:Size=\"16\" ss:Bold=\"1\"/>" +
                  "</Style>" +
                //                //-------------------------------------------
                //                "<Style ss:ID=\"cellBorders\">"+
                // "<Borders>"+
                //  "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"+
                //  "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"+
                //  "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"+
                //  "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"+
                // "</Borders>"+
                //"</Style>"+
                //                //==================================
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +

   //               "<Borders>" +
                // "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
                // "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
                // "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
                // "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
                //"</Borders>" +

                  "<Alignment ss:Vertical=\"Bottom\"/>\r\n" +
                  "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                  "\r\n <Protection/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"BoldColumn\">\r\n" +

                  "<Borders>" +
    "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
   "</Borders>" +

                  "<Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                  "<Style     ss:ID=\"StringLiteral\">\r\n" +

                  "<Borders>" +
    "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
   "</Borders>" +

                  "<NumberFormat ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"Decimal\">\r\n" +

                  "<Borders>" +
    "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
   "</Borders>" +

                  "<NumberFormat ss:Format=\"" + decimalFormatPlace(Decimals) +
                  "\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"Integer\">\r\n" +

                  "<Borders>" +
    "<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
    "<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>" +
   "</Borders>" +

                  "<NumberFormat ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                  "ss:Format=\"dd/mm/yyyy;@\"/>\r\n </Style>\r\n " +
                  "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            excelDoc.Write(startExcelXML);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");

            //======================Thêm dòng tiêu đề - HuuAn =================================
            //<Row ss:Height="20.25">
            //    <Cell ss:MergeAcross="3" ss:StyleID="s25"><Data ss:Type="String">Trộn abcd</Data></Cell>
            //</Row>
            if (!string.IsNullOrEmpty(strTitle))
            {
                excelDoc.Write("<Row ss:Height=\"20.25\">");
                excelDoc.Write("<Cell ss:MergeAcross=\"" + ((sourceTable.Columns.Count - 1) > 20 ? 20 : (sourceTable.Columns.Count - 1)) + "\" ss:StyleID=\"Title\"><Data ss:Type=\"String\">" + strTitle + "</Data></Cell>");
                excelDoc.Write("</Row>");
            }
            //==================================================================================

            excelDoc.Write("<Row>");
            for (int x = 0; x < sourceTable.Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                if (!string.IsNullOrEmpty(convertTo))
                {
                    excelDoc.Write(ChuyenMaTiengViet.VIETNAM_CONVERT(sourceTable.Columns[x].ColumnName, convertFrom, convertTo));
                }
                else
                {
                    excelDoc.Write(sourceTable.Columns[x].ColumnName);
                }

                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in sourceTable.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output

                if (_65536_rows_per_sheet && rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "

                for (int y = 0; y < sourceTable.Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&");
                            XMLstring = XMLstring.Replace(">", ">");
                            XMLstring = XMLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            if (!string.IsNullOrEmpty(convertTo))
                            {
                                XMLstring = ChuyenMaTiengViet.VIETNAM_CONVERT(XMLstring, convertFrom, convertTo);
                            }
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  

                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                            //The Following Code puts the date stored in XMLDate 

                            //to the format above

                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date

                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">");
                            excelDoc.Write(XMLDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">");
                            excelDoc.Write(" ");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        default:
                            throw (new Exception(rowType.ToString() + " not handled."));
                    }
                }
                excelDoc.Write("</Row>");
                exported++;
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXML);
            excelDoc.Close();
        }
        #endregion ==== Export to Excel Xml format ====

        #region ==== Export to text file ====
        public static void ExprotDataTableToTextFile(DataTable data, string fileName, string convertFrom, string convertTo)
        {
            totalRows = data.Rows.Count;
            converted = 0;
            exported = 0;
            DataTable convertTable = ConvertTable(data, convertFrom, convertTo);
            ExprotDataTableToTextFile(convertTable, fileName);
        }
        public static void ExprotDataTableToTextFile(DataTable data, string fileName)
        {
            exported = 0;
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                //Tieu de cot
                string titles = string.Empty;
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    titles += data.Columns[i].ColumnName + "\t";
                }
                sw.WriteLine(titles.Trim());
                //Data
                foreach (DataRow row in data.Rows)
                {
                    string line = string.Empty;
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        line += row[i].ToString() + "\t";
                    }
                    sw.WriteLine(line.Trim());
                    exported++;
                }
                sw.Close();
                fs.Close();
                //MessageBox. Show(exported.ToString());
            }
            catch (Exception)
            {
                sw.Close();
                fs.Close();
                throw;
            }
        }

        #endregion ==== Export to text file ====

        #region ==== Convert Table ====
        public static DataTable ConvertTable(DataTable data, string convertFrom, string convertTo)
        {
            DataTable convertTable = data.Copy();
            if (!string.IsNullOrEmpty(convertFrom) || !string.IsNullOrEmpty(convertTo))
            {
                converted = 0;
                //Chuyễn mã các tiêu đề cột
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    switch (data.Columns[i].DataType.ToString())
                    {
                        //case "System.Char":
                        case "System.String":
                            string s = ChuyenMaTiengViet.VIETNAM_CONVERT(data.Columns[i].ColumnName, convertFrom, convertTo);
                            data.Columns[i].ColumnName = s;
                            break;
                    }

                }
                converted++;
                //Chuyễn mã từng dòng dữ liệu
                for (int i = 0; i < convertTable.Rows.Count; i++)
                {
                    for (int j = 0; j < convertTable.Columns.Count; j++)
                    {
                        if (data.Columns[j].DataType.ToString() == "System.String")
                        {
                            convertTable.Rows[i][j] = ChuyenMaTiengViet.VIETNAM_CONVERT(convertTable.Rows[i][j].ToString(), convertFrom, convertTo);
                        }
                    }
                    converted++;
                }
                converted = data.Rows.Count;
            }
            return convertTable;
        }
        #endregion ==== Convert Table ====
    }
}
