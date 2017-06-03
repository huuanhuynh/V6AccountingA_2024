﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using SmartXLS;
using V6Tools.V6Convert;

namespace V6Tools.V6Export
{
    public static class Data_Table
    {
        /// <summary>
        /// Ghi file DBF hàm tự viết
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public static void ToDbfFile(DataTable data, string fileName)
        {
            ParseDBF.WriteDBF(data, fileName);
        }

        /// <summary>
        /// Chứa lỗi nếu có.
        /// </summary>
        public static string Message { get; set; }

        /// <summary>
        /// Lấy index dòng trong địa chỉ ô excel. vd A1 là dòng 0
        /// </summary>
        /// <param name="A1"></param>
        /// <returns></returns>
        private static int GetExcelRow(string A1)
        {
            int startIndex = A1.IndexOfAny("0123456789".ToCharArray());
            //string column = A1.Substring(0, startIndex);
            string rowID = A1.Substring(startIndex);
            return int.Parse(rowID)-1;
        }
        /// <summary>
        /// Lấy index cột trong địa chỉ ô excel. vd: A1 là cột 0
        /// </summary>
        /// <param name="A1"></param>
        /// <returns></returns>
        private static int GetExcelColumn(string A1)
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

        

        #region ==== SmartXLS ====

        /// <summary>
        /// Gán data với định dạng tùy biến theo kiểu dữ liệu.
        /// workBook.ImportDataTable không đảm bảo được UID.
        /// </summary>
        /// <param name="workBook">Excel workBook object.</param>
        /// <param name="data">Bảng dữ liệu sẽ xuất, không được null.</param>
        /// <param name="columns">Danh sách các cột sẽ xuất. Nếu xuất hết để null.</param>
        /// <param name="isShiftRows">Chèn vùng trước khi chèn dữ liệu.</param>
        /// <param name="isFieldNameShown">Hiển thị tên cột.</param>
        /// <param name="drawLine">Tô đen đường kẻ</param>
        /// <param name="firstRow">Dòng bắt đầu, 0 là đầu tiên, phải nhỏ hơn maxRows.</param>
        /// <param name="firstColumn">Cột bắt đầu, 0 là đầu tiên, phải nhỏ hơn maxColumns.</param>
        /// <param name="maxRows">Xuất hết thì để 0 hoặc -1.</param>
        /// <param name="maxColumns">Xuất hết thì để 0 hoặc -1.</param>
        /// <param name="autoColWidth">Chỉnh lại độ rộng mỗi cột cho phù hợp với dữ liệu.</param>
        /// <param name="bPreserveTypes">Chưa đụng tới - bảo toàn kiểu dữ liệu.</param>
        private static void ImportDataTable(
            WorkBook workBook, DataTable data, string[] columns,
            bool isShiftRows, bool isFieldNameShown, bool drawLine,
            int firstRow, int firstColumn,
            int maxRows, int maxColumns,
            //IList<DataColumn> arrColumns = null,
            bool autoColWidth = false, bool bPreserveTypes = true)
        {
            if(data == null)
                throw new ArgumentNullException("data");

            var use_arr_cols = columns != null && columns.Length > 0;
            var arrColumns = new List<DataColumn>();
            if (use_arr_cols)
            {
                foreach (string column in columns)
                {
                    if (data.Columns.Contains(column))
                        arrColumns.Add(data.Columns[column]);
                }
                //var arrayCols = Cols.ToArray();
            }

            var numOfRows = data.Rows.Count;
            
            var numOfColumns = use_arr_cols ? arrColumns.Count : data.Columns.Count;
            if (numOfColumns < 1)
            {
                throw new Exception("ExportExcel Column error.");
            }

            if (maxRows > 0 && numOfRows > maxRows)
                numOfRows = maxRows;
            
            if (maxColumns > 0 && numOfColumns > maxColumns)
                numOfColumns = maxColumns;
            //if (firstColumn > 0 && firstColumn < numOfColumns)
            //    numOfColumns -= firstColumn;

            #region ==== Chèn vùng dữ liệu =====
            if (isShiftRows && numOfRows>0)
            {
                var startRow = firstRow;
                var startCol = firstColumn;
                var endRow = startRow + numOfRows - 1;
                var endCol = startCol + numOfColumns -1;
                workBook.insertRange(firstRow, firstColumn,
                    firstRow + numOfRows -1, firstColumn + numOfColumns -1,
                    WorkBook.ShiftRows);
                RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
                ResetRangeStyleFormat(rangeStyle);
                workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
            }
            #endregion chèn vùng

            #region === Điền tên cột ===
            if (isFieldNameShown)
            {
                //Nếu hiện tên cột thì chèn thêm một dòng
                workBook.insertRange(firstRow, firstColumn, firstRow, firstColumn + numOfColumns - 1, WorkBook.ShiftRows);
                if (use_arr_cols)
                {
                    for (int i = 0; i < numOfColumns; i++)
                    {
                        workBook.setText(firstRow, i + firstColumn, arrColumns[i].ColumnName);
                    }
                }
                else
                {
                    for (int i = 0; i < numOfColumns; i++)
                    {
                        workBook.setText(firstRow, i + firstColumn, data.Columns[i].ColumnName);
                    }
                }
                firstRow++;
            }
            #endregion điền tên cột

            #region ==== Tô đen đường kẻ ====
            if (drawLine && numOfRows>0)
            {
                var startRow = firstRow - (isFieldNameShown ? 1 : 0);
                var startCol = firstColumn;
                var endRow = startRow + numOfRows - (isFieldNameShown ? 0 : 1);
                var endCol = startCol + numOfColumns - 1;

                RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
                rangeStyle.LeftBorder = RangeStyle.BorderThin;
                rangeStyle.RightBorder = RangeStyle.BorderThin;
                rangeStyle.TopBorder = RangeStyle.BorderThin;
                rangeStyle.BottomBorder = RangeStyle.BorderThin;
                rangeStyle.HorizontalInsideBorder = RangeStyle.BorderThin;
                rangeStyle.VerticalInsideBorder = RangeStyle.BorderThin;

                workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
            }
            #endregion tô đường kẻ
            
            // ImportData by DataType
            // Column by column
            // Duyệt qua từng cột một
            // Chèn dữ liệu theo kiểu dữ liệu
            for (int i = 0; i < numOfColumns; i++)
            {
                #region === Chèn dữ liệu cho cột i ===
                var column = use_arr_cols ? arrColumns[i] : data.Columns[i];
                var type = column.DataType;
                var field = column.ColumnName;

                if (type == typeof(DateTime))
                {
                    RangeStyle rs = workBook.getRangeStyle(firstRow, firstColumn + i, firstRow + numOfRows, firstColumn + i);
                    
                    var systemFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    rs.CustomFormat = systemFormat;// "dd/mm/yyyy"; // excel format
                    // Áp định dạng cho cả cột
                    workBook.setRangeStyle(rs, firstRow, firstColumn + i, firstRow + numOfRows, firstColumn + i);

                    for (int j = 0; j < numOfRows; j++)
                    {
                        var date_string = ObjectAndString.ObjectToString(data.Rows[j][field], systemFormat);
                        if (date_string != null)
                        {
                            workBook.setEntry(firstRow + j, firstColumn + i, date_string);
                        }
                        else
                        {
                            workBook.setEntry(firstRow + j, firstColumn + i, "");
                        }
                    }
                    //Ap lai format dd/MM/yyyy
                    rs.CustomFormat = "dd/MM/yyyy";
                    workBook.setRangeStyle(rs, firstRow, firstColumn + i, firstRow + numOfRows, firstColumn + i);
                }
                else if (type == typeof(decimal) || type == typeof(float) || type == typeof(double)
                    || type == typeof(int) || type == typeof(long) || type == typeof(short)
                    || type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort)
                    || type == typeof(byte)
                    )
                {
                    // Áp định dạng canh phải cho kiểu số
                    RangeStyle rs = workBook.getRangeStyle(firstRow, firstColumn + i, firstRow + numOfRows, firstColumn + i);
                    rs.HorizontalAlignment = RangeStyle.HorizontalAlignmentRight;
                    workBook.setRangeStyle(rs, firstRow, firstColumn + i, firstRow + numOfRows, firstColumn + i);

                    for (int j = 0; j < numOfRows; j++)
                    {
                        workBook.setNumber(firstRow + j, firstColumn + i, ObjectAndString.ToObject<double>(data.Rows[j][field]));
                    }
                }
                else
                {
                    for (int j = 0; j < numOfRows; j++)
                    {
                        workBook.setText(firstRow + j, firstColumn + i, ObjectAndString.ObjectToString(data.Rows[j][field]));
                    }
                }
                #endregion chèn dữ liệu cột i

                if(autoColWidth) workBook.setColWidthAutoSize(i, true);
            }

            
        }
        private static void ResetRangeStyleFormat(RangeStyle range)
        {
            if (range != null)
            {
                range.FontBold = false;
                range.FontItalic = false;
                range.FontUnderline = RangeStyle.UnderlineNone;
                range.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
                range.Pattern = RangeStyle.PatternNull;
                range.PatternBG = Color.White.ToArgb();
            }
        }

        private static void SetEntry(WorkBook workbook, object value)
        {
            
        }

        /// <summary>
        /// Gán một giá trị vào địa chỉ cho trước với format
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="value">Giá trị kiểu chuổi</param>
        /// <param name="address">vd: A1</param>
        /// <param name="format">vd: N2 hoặc 0.00 hoặc #,##0.00</param>
        private static void SetEntryFormat(WorkBook workbook, string value, string address, string format)
        {
            var row = GetExcelRow(address);
            var col = GetExcelColumn(address);

            if (string.IsNullOrEmpty(format))   // Nếu không có format thì set mặc định như gõ vào
            {
                workbook.setEntry(row, col, value);
            }
            else
            {
                //Kieu so với định dạng N?, ? là số chữ số phần thập phân (sau dấu.)
                if (format.StartsWith("N"))
                {
                    var decimalPlace = Convert.ToInt32(format.Substring(1));
                    RangeStyle rs = workbook.getRangeStyle(row, col, row, col);
                    //rs.CustomFormat = "$#,##0.00;[Red]$#,##0.00";

                    string fstring = "#,##0.";
                    for (int i = 0; i < decimalPlace; i++)
                    {
                        fstring += "0";
                    }
                    rs.CustomFormat = fstring;
                    //rs.CustomFormat = "dd-mmm-yyyy";
                    workbook.setRangeStyle(rs, row, col, row, col);
                    //rs.CustomFormat = "dd/mm/yyyy";//excel format
                    //workbook.setRangeStyle(rs, row, col, row, col);

                    workbook.setNumber(row, col, Convert.ToDouble(value));
                }
                else // Kiểu định dạng mặc định theo smart_xls
                {
                    RangeStyle rs = workbook.getRangeStyle(row, col, row, col);
                    rs.CustomFormat = format;
                    //workbook.setRangeStyle(rs, row, col, row, col);
                    workbook.setEntry(row, col, value);
                    workbook.setRangeStyle(rs, row, col, row, col);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="rowIndex">Dòng bắt đầu. 0 base</param>
        /// <param name="startColumnIndex">Cột bắt đầu. 0 base</param>
        /// <param name="headers"></param>
        /// <param name="drawLine">Vẽ đường kẻ</param>
        private static void SetHeaders(WorkBook workBook, IList<string> headers,
            int rowIndex, int startColumnIndex, bool drawLine)
        {
            var numOfColumns = headers.Count;
            for (int i = 0; i < numOfColumns; i++)
            {
                workBook.setText(rowIndex, i + startColumnIndex, headers[i]);
            }
            if (drawLine)
            {
                var startRow = rowIndex;
                var startCol = startColumnIndex;
                var endRow = startRow;
                var endCol = startCol + numOfColumns - 1;

                RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
                rangeStyle.LeftBorder = RangeStyle.BorderThin;
                rangeStyle.RightBorder = RangeStyle.BorderThin;
                rangeStyle.TopBorder = RangeStyle.BorderThin;
                rangeStyle.BottomBorder = RangeStyle.BorderThin;
                rangeStyle.HorizontalInsideBorder = RangeStyle.BorderThin;
                rangeStyle.VerticalInsideBorder = RangeStyle.BorderThin;

                workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
            }
        }

        private static void SetParameters(WorkBook workbook, SortedDictionary<string, object> parameters)
        {
            #region ===== Parameter {A1:dd/mm/yyyy,29-01-1987} =====

            var system_date_format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            //Write parameter with key is address[:format]
            foreach (KeyValuePair<string, object> item in parameters)
            {
                var KEY = item.Key;
                string A1 = KEY, format = null;
                if (KEY.Contains(":"))
                {
                    var ss = KEY.Split(':');
                    A1 = ss[0];
                    format = ss[1];
                }
                var c_column_index = GetExcelColumn(A1);
                var c_row_index = GetExcelRow(A1);
                var rs = workbook.getRangeStyle(c_row_index, c_column_index, c_row_index, c_column_index);

                if (ObjectAndString.IsNumberType(item.Value.GetType()))
                {
                    workbook.setNumber(A1, ObjectAndString.ToObject<Double>(item.Value));
                    if (!string.IsNullOrEmpty(format))
                    {
                        rs.CustomFormat = format;
                        workbook.setRangeStyle(rs, c_row_index, c_column_index, c_row_index, c_column_index);
                    }
                }
                else if (item.Value is DateTime)
                {
                    rs.CustomFormat = system_date_format;
                    workbook.setRangeStyle(rs, c_row_index, c_column_index, c_row_index, c_column_index);
                    var date_string = ObjectAndString.ObjectToString(item.Value, system_date_format);
                    workbook.setEntry(c_row_index, c_column_index, date_string);
                    rs.CustomFormat = "DD/mm/yyyy";
                    workbook.setRangeStyle(rs, c_row_index, c_column_index, c_row_index, c_column_index);
                }
                else if (string.IsNullOrEmpty(format))
                {
                    workbook.setEntry(c_row_index, c_column_index, ObjectAndString.ObjectToString(item.Value, system_date_format));
                    workbook.setRangeStyle(rs, c_row_index, c_column_index, c_row_index, c_column_index);
                }
                else
                {
                    workbook.setEntry(c_row_index, c_column_index, ObjectAndString.ObjectToString(item.Value));
                }
            }
            #endregion parameters
        }

        static WorkBook ReadWorkBookCopy(string xlsTemplateFile, string saveFile)
        {
            if(!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);

            WorkBook workbook = new WorkBook();
            workbook.setDefaultFont("Arial", 10*20, 1);

            xlsTemplateFile = Path.GetFullPath(xlsTemplateFile);
            saveFile = Path.GetFullPath(saveFile);
            if (String.Equals(saveFile, xlsTemplateFile, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ExportException("File lưu trùng file mẫu!");
            }
            string ext = Path.GetExtension(xlsTemplateFile).ToLower();
            if (File.Exists(xlsTemplateFile))
            {
                #region ==== Copy file ====
                File.Copy(xlsTemplateFile, saveFile, true);
                #endregion

                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {

                            if (ext == ".xls")
                                workbook.read(saveFile);
                            else if (ext == ".xlsx")
                                workbook.readXLSX(saveFile);
                            else if (ext == ".xml")
                                workbook.readXML(saveFile);
                            else
                                workbook.read(saveFile);
                        }
                        catch
                        {
                            workbook.readXLSX(saveFile);
                        }
                    }
                    catch
                    {
                        workbook.readXML(saveFile);
                    }
                }
                catch
                {
                    workbook.read(saveFile);
                }

                #endregion
            }
            else
            {
                Message = "Không có file mẫu.";
            }

            return workbook;
        }

        /// <summary>
        /// Xuất một bảng dữ liệu ra file excel với mẫu có sẵn
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu</param>
        /// <param name="data">Dữ liệu vào</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết.</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, không được trùng với file mẫu</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplate(string xlsTemplateFile, DataTable data, string saveFile,
            string firstCell, string[] columns, SortedDictionary<string, object> parameters, 
            NumberFormatInfo nfi,bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.setDefaultFont("Arial", 10*20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                    workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);
                
                //select sheet
                int sheetIndex = 0;
                workbook.Sheet = sheetIndex;
                int sheetCount = workbook.NumSheets;
                string sheetName = workbook.getSheetName(sheetIndex);
                string t;

                int startRow = 1, startCol = 0;
                int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                int lastCol = workbook.LastCol;
                if (string.IsNullOrEmpty(firstCell))
                {
                    startRow = lastRow +1;
                    startCol = 0;
                }
                else
                {
                    startRow = GetExcelRow(firstCell);
                    startCol = GetExcelColumn(firstCell);
                }

                SetParameters(workbook, parameters);

                //var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
                //int endCol;// startCol + data.Columns.Count - 1;

                ImportDataTable(workbook, data, columns, rowInsert, false, drawLine, startRow, startCol, -1, -1);
                
                var a = workbook.write(saveFile);
                workbook.Dispose();
                return true;//a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplate " + ex.Message;
                workbook.Dispose();
                return false;
            }
        }


        /// <summary>
        /// Xuất một bảng dữ liệu ra file excel với mẫu có sẵn, thêm tiêu đề gửi vào
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu</param>
        /// <param name="data">Dữ liệu vào</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, không được trùng với file mẫu</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết.</param>
        /// <param name="headers">Tiêu đề cột</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplate(string xlsTemplateFile, DataTable data, string saveFile,
            string firstCell, string[] columns, string[] headers, SortedDictionary<string, object> parameters,
            NumberFormatInfo nfi, bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                    workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);

                //select sheet
                int sheetIndex = 0;
                workbook.Sheet = sheetIndex;
                int sheetCount = workbook.NumSheets;
                string sheetName = workbook.getSheetName(sheetIndex);
                string t;

                int startRow = 1, startCol = 0;
                int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                int lastCol = workbook.LastCol;
                if (string.IsNullOrEmpty(firstCell))
                {
                    startRow = lastRow + 1;
                    startCol = 0;
                }
                else
                {
                    startRow = GetExcelRow(firstCell);
                    startCol = GetExcelColumn(firstCell);
                }

                SetParameters(workbook, parameters);

                //var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
                //int endCol;// startCol + data.Columns.Count - 1;

                ImportDataTable(workbook, data, columns, rowInsert, false, drawLine, startRow, startCol, -1, -1);

                //Nếu rowIndex = 0 thì chèn thêm một dòng
                if (startRow == 0)
                {
                    workbook.insertRange(startRow, startCol, startRow, startCol + headers.Length - 1, WorkBook.ShiftRows);
                }
                SetHeaders(workbook, headers, startRow, startCol, drawLine);
                

                var a = workbook.write(saveFile);
                workbook.Dispose();
                return true;//a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplate " + ex.Message;
                workbook.Dispose();
                return false;
            }
        }

        
        /// <summary>
        /// Xuất excel báo cáo thuế HTKK
        /// </summary>
        public static bool ToExcelTemplateHTKK(string xlsTemplateFile, SortedDictionary<string, object> parameters,
            SortedDictionary<string, DataTable> datas, string[] columns, string saveFile, NumberFormatInfo nfi,
            bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if(!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workBook = new WorkBook();
            try
            {
                if(File.Exists(xlsTemplateFile))
                    workBook = ReadWorkBookCopy(xlsTemplateFile, saveFile);

                SetParameters(workBook, parameters);

                //select sheet
                int sheetIndex = 0;
                workBook.Sheet = sheetIndex;

                int startRow = 1, startCol = 0;

                //Đảo data
                KeyValuePair<string, DataTable>[] data1 = new KeyValuePair<string, DataTable>[datas.Count];
                var index = 0;
                foreach (KeyValuePair<string, DataTable> item in datas)
                {
                    data1[index] = item;
                    index++;
                }
                for (int i = data1.Length - 1; i >= 0; i--)
                {
                    KeyValuePair<string, DataTable> item = data1[i];
                    var firstCell = item.Key;
                    var data = item.Value;

                    startRow = GetExcelRow(firstCell);
                    startCol = GetExcelColumn(firstCell);

                    //Sua lap
                    ImportDataTable(workBook, data, columns, rowInsert, false, drawLine, startRow, startCol, -1, -1);
                }

                var a = workBook.write(saveFile);
                workBook.Dispose();

                return true; //a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplate " + ex.Message;
                workBook.Dispose();
                return false;
            }
        }

        /// <summary>
        /// sx
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveAs">Tên file lưu</param>
        /// <param name="title"></param>
        /// <param name="fontName"></param>
        public static void ToExcel(DataTable data, string saveAs, string title, string fontName = "")
        {
            ToExcel(data, saveAs, title, false, fontName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveAs"></param>
        /// <param name="title"></param>
        /// <param name="line">Kẻ ô</param>
        /// <param name="fontName"></param>
        public static void ToExcel(DataTable data, string saveAs, string title, bool line, string fontName = "")
        {
            SmartXLS.WorkBook workBook = new SmartXLS.WorkBook();
            if (fontName == "" || !IsFontInstalled(fontName)) fontName = "Times New Roman";
            workBook.setDefaultFont(fontName, 12*20, 1);
            int startRow = 0, startCol = 0, endRow = 0, endCol = data.Columns.Count-1;

            //merge/unmerge range of cells

            //range contain merged area can not be merged
            //SmartXLS.RangeStyle rangeStyle = workBook.getRangeStyle(1, 1, 2, 2);//get format from range B2:C3
            SmartXLS.RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
            rangeStyle.MergeCells = true;//merge range
            //rangeStyle.MergeCells = false;//unmerge range
            rangeStyle.LeftBorder = SmartXLS.RangeStyle.BorderThin;
            rangeStyle.RightBorder = SmartXLS.RangeStyle.BorderThin;
            rangeStyle.TopBorder = SmartXLS.RangeStyle.BorderThin;
            rangeStyle.BottomBorder = SmartXLS.RangeStyle.BorderThin;
            
            rangeStyle.HorizontalAlignment = SmartXLS.RangeStyle.HorizontalAlignmentCenter;
            rangeStyle.VerticalAlignment = SmartXLS.RangeStyle.VerticalAlignmentCenter;
            rangeStyle.FontBold = true;
            
            rangeStyle.FontColor = 0x0000FF;
            rangeStyle.FontSize = 12*20;
            
            // Set title
            workBook.Sheet = 0;
            if (!string.IsNullOrEmpty(title))
            {
                workBook.setText(startRow, startCol, title);
                if (line)
                    workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                startRow++; 
            }

            endRow = startRow + data.Rows.Count;

            //if (line)
            //{
            //    rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
            //    rangeStyle.LeftBorder = SmartXLS.RangeStyle.BorderThin;
            //    rangeStyle.RightBorder = SmartXLS.RangeStyle.BorderThin;
            //    rangeStyle.TopBorder = SmartXLS.RangeStyle.BorderThin;
            //    rangeStyle.BottomBorder = SmartXLS.RangeStyle.BorderThin;
            //    rangeStyle.HorizontalInsideBorder = SmartXLS.RangeStyle.BorderThin;
            //    rangeStyle.VerticalInsideBorder = SmartXLS.RangeStyle.BorderThin;

            //    workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
            //}

            ImportDataTable(workBook, data, null, false, true, line, startRow, startCol, -1, -1);
            
            var ext = Path.GetExtension(saveAs);
            if (ext != null)
            {
                ext = ext.ToLower();
                switch (ext)
                {
                    case ".xlsx":
                        workBook.writeXLSX(saveAs);
                        break;
                    case ".csv":
                        workBook.writeCSV(saveAs);
                        break;
                    default:
                        workBook.write(saveAs);
                        break;
                }
            }
            workBook.Dispose();
        }

        /// <summary>
        /// Bỏ qua không dùng nữa. luôn return true!
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        static bool IsFontInstalled(string fontName)
        {
            return true;

            //using (var testFont = new System.Drawing.Font(fontName, 8))
            //{
            //    return 0 == string.Compare(
            //      fontName,
            //      testFont.Name,
            //      StringComparison.InvariantCultureIgnoreCase);
            //}
        }
        #endregion smartxls

        public static bool ToTextFile(DataTable data, string fileName)
        {
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
                    //exported++;
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                sw.Close();
                fs.Close();
                throw new ExportException("DataTableToTextFile " + ex.Message);
            }
        }

        public static bool ToXmlFile(DataTable data, string fileName)
        {
            
            FileStream fs = new FileStream(fileName, FileMode.Create);
            try
            {
                data.WriteXml(fs);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                fs.Close();
                throw new ExportException("DataTableToXmlFile " + ex.Message);
            }
        }

        #region ==== NPOI ====
        /// <summary>
        /// Chưa xài được
        /// Xuất một bảng dữ liệu ra file excel với mẫu có sẵn
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu</param>
        /// <param name="data">Dữ liệu vào</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết.</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, không được trùng với file mẫu</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplate_NPOI(string xlsTemplateFile, DataTable data, string saveFile,
            string firstCell, string[] columns, SortedDictionary<string, string> parameters,
            bool drawLine = false, bool rowInsert = false)
        {
            Message = "";

            POIFSFileSystem fs;
            HSSFWorkbook workbook = new HSSFWorkbook();

            try
            {




                xlsTemplateFile = Path.GetFullPath(xlsTemplateFile);
                saveFile = Path.GetFullPath(saveFile);
                if (String.Equals(saveFile, xlsTemplateFile, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new ExportException("File lưu trùng file mẫu!");
                }
                string ext = (Path.GetExtension(xlsTemplateFile) ?? "").ToLower();
                if (File.Exists(xlsTemplateFile))
                {
                    #region ==== Copy file ====
                    File.Copy(xlsTemplateFile, saveFile, true);
                    #endregion

                    fs = new POIFSFileSystem(new FileStream(xlsTemplateFile, FileMode.Open));
                    workbook = new HSSFWorkbook(fs, false);

                    //Create a entry of DocumentSummaryInformation
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "V6Corp";
                    workbook.DocumentSummaryInformation = dsi;

                    //Create a entry of SummaryInformation
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Subject = "ExportExcel";
                    workbook.SummaryInformation = si;

                }
                else
                {
                    Message = "Không có file mẫu.";
                }

                //select sheet
                int sheetIndex = 0;
                workbook.SetActiveSheet(sheetIndex);//.Sheet = sheetIndex;
                int sheetCount = workbook.NumberOfSheets;//.NumSheets;
                string sheetName = workbook.GetSheetName(sheetIndex);
                ISheet sheet = workbook.GetSheet(sheetName);
                string t;

                int startRow = 1, startCol = 0;
                int lastRow = sheet.LastRowNum;//.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                //int lastCol = sheet.colum;
                if (string.IsNullOrEmpty(firstCell))
                {
                    startRow = lastRow + 1;
                    startCol = 0;
                }
                else
                {
                    startRow = GetExcelRow(firstCell);
                    startCol = GetExcelColumn(firstCell);
                }


                //Write parameter with key is address
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    var A1 = item.Key;
                    var row = GetExcelRow(A1);
                    var col = GetExcelColumn(A1);

                    //workbook.setText(row, col, item.Value);
                    sheet.GetRow(row).GetCell(col).SetCellValue(item.Value);
                }

                var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
                int endCol;// startCol + data.Columns.Count - 1;

                List<DataColumn> Cols = new List<DataColumn>();
                if (columns == null)
                {
                    endCol = startCol + data.Columns.Count - 1;
                    //if (rowInsert)
                    //{
                    //    //Insert row (vùng insert thêm bằng với vùng chọn)
                    //    workbook.insertRange(startRow, startCol, endRow, endCol,
                    //        WorkBook.ShiftRows);

                    //    RangeStyle rangeStyle = workbook.getRangeStyle(startRow, startCol, endRow, endCol);
                    //    ResetRangeStyleFormat(rangeStyle);
                    //    workbook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                    //}
                    
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = startCol; j < endCol; j++)
                        {
                            sheet.GetRow(i).GetCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                    }
                }
                //else
                //{
                //    foreach (string column in columns)
                //    {
                //        if (data.Columns.Contains(column))
                //            Cols.Add(data.Columns[column]);
                //    }
                //    var arrayCols = Cols.ToArray();
                //    if (Cols.Count > 0)
                //    {
                //        endCol = startCol + arrayCols.Length - 1;
                //        if (rowInsert)
                //        {
                //            //Insert row (vùng insert thêm bằng với vùng chọn)
                //            workbook.insertRange(startRow, startCol, endRow, endCol,
                //                WorkBook.ShiftRows);

                //            RangeStyle rangeStyle = workbook.getRangeStyle(startRow, startCol, endRow, endCol);
                //            ResetRangeStyleFormat(rangeStyle);
                //            workbook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                //        }
                //      ImportDataTable(workbook, data, false, startRow, startCol, -1, -1);

                //    }
                //    else
                //    {
                //        endCol = data.Columns.Count - 1;
                //        if (rowInsert)
                //        {
                //            //Insert row (vùng insert thêm bằng với vùng chọn)
                //            workbook.insertRange(startRow, startCol, endRow, endCol,
                //                WorkBook.ShiftRows);

                //            RangeStyle rangeStyle = workbook.getRangeStyle(startRow, startCol, endRow, endCol);
                //            ResetRangeStyleFormat(rangeStyle);
                //            workbook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                //        }
                //      ImportDataTable(workbook, data, false, startRow, startCol, -1, -1);
                //    }
                //}



                //if (drawLine)
                //{
                //    RangeStyle rangeStyle = workbook.getRangeStyle(startRow, startCol, endRow, endCol);
                //    rangeStyle.LeftBorder = RangeStyle.BorderThin;
                //    rangeStyle.RightBorder = RangeStyle.BorderThin;
                //    rangeStyle.TopBorder = RangeStyle.BorderThin;
                //    rangeStyle.BottomBorder = RangeStyle.BorderThin;
                //    rangeStyle.HorizontalInsideBorder = RangeStyle.BorderThin;
                //    rangeStyle.VerticalInsideBorder = RangeStyle.BorderThin;

                //    workbook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                //}

                //var a = workbook.w rite(saveFile);
                //workbook.Dispose();

                FileStream file = new FileStream(saveFile, FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook.Close();
                return true;//a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplate " + ex.Message;
                workbook.Close();
                return false;
            }
        }
        #endregion npoi

        
    }


}
