using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
//using NPOI.HPSF;
//using NPOI.HSSF.UserModel;
//using NPOI.POIFS.FileSystem;
//using NPOI.SS.UserModel;
using SmartXLS;
using V6Tools.V6Convert;
using System.Text.RegularExpressions;

namespace V6Tools.V6Export
{
    /// <summary>
    /// Các thao tác ghi ra file.
    /// </summary>
    public static class ExportData
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
        /// <param name="A1">Địa chỉ ô excel, Chỉ cột thôi cũng được. vd A1 hoặc A</param>
        /// <returns></returns>
        private static int GetExcelColumn(string A1)
        {
            int startIndex = A1.IndexOfAny("0123456789".ToCharArray());
            string str = A1.Substring(0, startIndex > 0 ? startIndex : A1.Length).ToUpper();

            if (string.IsNullOrEmpty(str)) throw new Exception("GetExcelColumn: Không có giá trị!");
            if (str.Length > 2) throw new Exception("GetExcelColumn: Quá dài!");

            str = str.ToUpper();
            if (str.Length == 1)
                return str[0] - 'A';

            const int max = ('Z' - 'A' + 1) * ('I' - 'A' + 1) + ('V' - 'A');
            var result = ('Z' - 'A' + 1) * (str[0] - 'A' + 1) + (str[1] - 'A');
            if (result > max) throw new Exception("GetExcelColumn: Giá trị lớn nhất là IV = " + max + ". (A=0)");
            return result;
        }

        

        #region ==== SmartXLS ====

        public class MyColumnInfo
        {
            public DataColumn Column { get; set; }
            /// <summary>
            /// Field dạng công thức
            /// </summary>
            public string Expression { get; set; }

            public string ColumnName { get; set; }
        }

        /// <summary>
        /// Gán data với định dạng tùy biến theo kiểu dữ liệu.
        /// workBook.ImportDataTable không đảm bảo được UID.
        /// </summary>
        /// <param name="workBook">Excel workBook object.</param>
        /// <param name="setting">data columns...</param>
        /// <param name="maxRows">Xuất hết thì để 0 hoặc -1.</param>
        /// <param name="maxColumns">Xuất hết thì để 0 hoặc -1.</param>
        /// <param name="autoColWidth">Chỉnh lại độ rộng mỗi cột cho phù hợp với dữ liệu.</param>
        /// <param name="bPreserveTypes">Chưa đụng tới - bảo toàn kiểu dữ liệu.</param>
        private static void ImportDataTable(WorkBook workBook, ExportExcelSetting setting,
            int maxRows, int maxColumns, bool autoColWidth = false, bool bPreserveTypes = true)
        {
            if(setting.data == null)
                throw new ArgumentNullException("data");

            var data = setting.data.Copy();

            var use_arr_cols = setting.columns != null && setting.columns.Length > 0;
            var arrColumnsInfo = new List<MyColumnInfo>();
            if (use_arr_cols)
            {
                foreach (string column1 in setting.columns)
                {
                    MyColumnInfo myColumnInfo = new MyColumnInfo();
                    arrColumnsInfo.Add(myColumnInfo);

                    string column_param_string = column1.Trim();

                    if (data.Columns.Contains(column_param_string))
                    {
                        myColumnInfo.Column = data.Columns[column_param_string];
                        myColumnInfo.ColumnName = myColumnInfo.Column.ColumnName;
                    }
                    else
                    {
                        string[] ss = column_param_string.Split(':');
                            //"ColumnName:Type:DefaultValue ex GhiChu:C:abc ex2 ExtraDate:D:20/11/2018
                        if (ss.Length > 1)
                        {
                            string columnName = ss[0];
                            myColumnInfo.ColumnName = columnName;
                            object default_value = DBNull.Value;

                            if (ss[1].ToUpper() == "N")
                            {
                                if (!data.Columns.Contains(columnName)) data.Columns.Add(columnName, typeof(decimal));
                                if (ss.Length > 2)
                                {
                                    if (ss[2].Contains("+") || ss[2].Contains("-") || ss[2].Contains("*") || ss[2].Contains("/")
                                         || ss[2].Contains("^"))
                                    {
                                        myColumnInfo.Expression = ss[2];
                                    }
                                    else
                                    {
                                        default_value = ObjectAndString.StringToDecimal(ss[2]);
                                    }
                                }
                            }
                            else if (ss[1].ToUpper() == "D")
                            {
                                if (!data.Columns.Contains(columnName)) data.Columns.Add(columnName, typeof(DateTime));
                                if (ss.Length > 2) default_value = ObjectAndString.StringToDate(ss[2]);
                            }
                            else
                            {
                                if (!data.Columns.Contains(columnName)) data.Columns.Add(columnName, typeof(string));
                                if (ss.Length > 2) default_value = ss[2];
                            }
                            //arrColumns.Add(data.Columns[ss[0]]); // !!!!!!!!!!
                            myColumnInfo.Column = data.Columns[columnName];

                            if (myColumnInfo.Expression != null)
                            {
                                foreach (DataRow row in data.Rows)
                                {
                                    row[columnName] = Number.GiaTriBieuThuc(myColumnInfo.Expression, row.ToDataDictionary());
                                }
                            }
                            else
                            {
                                foreach (DataRow row in data.Rows)
                                {
                                    row[columnName] = default_value;
                                }
                            }
                        }
                        else
                        {
                            // no datacolumn
                            throw new Exception("No data column [" + column_param_string + "]");
                        }
                    }
                }
                //var arrayCols = Cols.ToArray();
            }
            else // Lấy tất cả columns.
            {
                foreach (DataColumn column in data.Columns)
                {
                    MyColumnInfo myColumnInfo = new MyColumnInfo();
                    arrColumnsInfo.Add(myColumnInfo);
                    myColumnInfo.Column = column;
                    myColumnInfo.ColumnName = column.ColumnName;
                }
            }

            var numOfRows = data.Rows.Count;
            var numOfColumns = use_arr_cols ? arrColumnsInfo.Count : data.Columns.Count;

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
            int exportStartRow = setting.startRow;
            if (setting.isInsertRow && numOfRows>0)
            {
                var styleStartRow = exportStartRow;
                var styleStartColumn = setting.startColumn;
                var styleEndRow = styleStartRow + numOfRows - 1;
                var styleEndColumn = styleStartColumn + numOfColumns -1;

                int fixStarRow = 0, fix;
                // Check nếu có dòng trống từ first_row thì giảm vùng chèn 1 dòng.
                {
                    string temp = "";
                    for (int col_i = setting.startColumn; col_i < setting.startColumn + numOfColumns; col_i++)
                    {
                        temp += workBook.getText(exportStartRow, col_i);
                    }
                    if (temp.Trim().Length == 0) fixStarRow = 1;
                }
                workBook.insertRange(exportStartRow + fixStarRow, setting.startColumn,
                    exportStartRow + numOfRows - 1, setting.startColumn + numOfColumns - 1,
                    WorkBook.ShiftRows);
                RangeStyle rangeStyle = workBook.getRangeStyle(styleStartRow, styleStartColumn, styleEndRow, styleEndColumn);
                ResetRangeStyleFormat(rangeStyle);
                workBook.setRangeStyle(rangeStyle, styleStartRow, styleStartColumn, styleEndRow, styleEndColumn);
            }
            #endregion chèn vùng

            #region === Điền tên cột ===
            if (setting.isFieldNameShow)
            {
                //Nếu hiện tên cột thì chèn thêm một dòng
                workBook.insertRange(exportStartRow, setting.startColumn, exportStartRow, setting.startColumn + numOfColumns - 1, WorkBook.ShiftRows);
                if (use_arr_cols)
                {
                    for (int i = 0; i < numOfColumns; i++)
                    {
                        workBook.setText(exportStartRow, i + setting.startColumn, arrColumnsInfo[i].ColumnName);
                    }
                }
                else
                {
                    for (int i = 0; i < numOfColumns; i++)
                    {
                        workBook.setText(exportStartRow, i + setting.startColumn, data.Columns[i].ColumnName);
                    }
                }
                exportStartRow++;
            }
            #endregion điền tên cột

            #region ==== Tô đen đường kẻ ====
            if (setting.isDrawLine && numOfRows>0)
            {
                var startRow = exportStartRow - (setting.isFieldNameShow ? 1 : 0);
                var startCol = setting.startColumn;
                var endRow = startRow + numOfRows - (setting.isFieldNameShow ? 0 : 1);
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

            #region ==== Tô đậm theo điều kiện setting.IS_BOLD_FIELD = setting.IS_BOLD_VALUE ====
            if (setting != null && setting.BOLD_YN && setting.BOLD_CONDITION != null)
            {
                var startRow = exportStartRow;
                var startCol = setting.startColumn;
                var endRow = startRow + numOfRows - (setting.isFieldNameShow ? 0 : 1);
                var endCol = startCol + numOfColumns - 1;
                int index = 0;
                for (int i = startRow; i <= endRow; i++)
                {
                    try
	                {
                        DataRow row = data.Rows[index++];
                        bool is_bold = setting.BOLD_CONDITION.Check(row);
                        if (is_bold)
                        {
                            RangeStyle rangeStyle = workBook.getRangeStyle(i, startCol, i, endCol);
                            rangeStyle.FontBold = true;
                            workBook.setRangeStyle(rangeStyle, i, startCol, i, endCol);
                        }
	                }
	                catch (Exception)
	                {
                        //
	                }
                }
            }
            #endregion tô đậm

            #region Tô màu nền mỗi dòng

            // Nếu có tùy chọn tô màu.
            if (!string.IsNullOrEmpty(setting.COLOR_FIELD) && data.Columns.Contains(setting.COLOR_FIELD))
            {
                var startRow = exportStartRow;
                var startCol = setting.startColumn;
                var endRow = startRow + numOfRows - (setting.isFieldNameShow ? 0 : 1);
                var endCol = startCol + numOfColumns - 1;
                int index = 0;
                
                Color default_line_color = Color.White;
                if (!string.IsNullOrEmpty(setting.M_COLOR_SUM))
                {
                    default_line_color = ObjectAndString.StringToColor(setting.M_COLOR_SUM);
                }

                for (int i = startRow; i <= endRow; i++)
                {
                    try
                    {
                        DataRow row = data.Rows[index++];
                        RangeStyle rs = workBook.getRangeStyle(i, startCol, i, endCol);

                        var color = Color.Transparent;
                        // nếu dữ liệu COLOR rỗng
                        if (("" + row[setting.COLOR_FIELD]).Trim() == "")
                        {
                            color = default_line_color;
                        }
                        else
                        {
                            color = ObjectAndString.StringToColor("" + row[setting.COLOR_FIELD]);
                        }
                        
                        rs.Pattern = RangeStyle.PatternSolid;
                        rs.PatternFG = color.ToArgb();
                        workBook.setRangeStyle(rs, i, startCol, i, endCol);
                    }
                    catch (Exception)
                    {
                        //
                    }
                }
            }
            #endregion tô màu nền mỗi dòng.

            // ImportData by DataType
            // Column by column
            // Duyệt qua từng cột một
            // Chèn dữ liệu theo kiểu dữ liệu
            for (int i = 0; i < numOfColumns; i++)
            {
                #region === Chèn dữ liệu cho cột i ===
                var column_i = arrColumnsInfo[i].Column;
                int excelCurrentColumnIndex = setting.startColumn + i;
                //int excelCurrentColumnWidth = workBook.getColWidth(excelCurrentColumnIndex);
                //y1 = y2;
                //y2 += excelCurrentColumnWidth;
                Type type = column_i.DataType;
                
                if (type == typeof(DateTime))
                {
                    RangeStyle rs = workBook.getRangeStyle(exportStartRow, setting.startColumn + i, exportStartRow + numOfRows, setting.startColumn + i);
                    
                    var systemFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    rs.CustomFormat = systemFormat;// "dd/mm/yyyy"; // excel format
                    // Áp định dạng cho cả cột
                    workBook.setRangeStyle(rs, exportStartRow, setting.startColumn + i, exportStartRow + numOfRows, setting.startColumn + i);

                    for (int j = 0; j < numOfRows; j++)
                    {
                        var date_string = ObjectAndString.ObjectToString(data.Rows[j][column_i.ColumnName], systemFormat);
                        if (date_string != null)
                        {
                            workBook.setEntry(exportStartRow + j, setting.startColumn + i, date_string);
                        }
                        else
                        {
                            workBook.setEntry(exportStartRow + j, setting.startColumn + i, "");
                        }
                    }
                    //Ap lai format dd/MM/yyyy
                    rs.CustomFormat = "dd/MM/yyyy";
                    workBook.setRangeStyle(rs, exportStartRow, setting.startColumn + i, exportStartRow + numOfRows, setting.startColumn + i);
                }
                else if (type == typeof(byte[])) // image type
                {
                    for (int j = 0; j < numOfRows; j++)
                    {
                        int excelCurrentRowIndex = exportStartRow + j;
                        //int excelCurrentRowHeight = workBook.getRowHeight(excelCurrentRowIndex);
                        //x1 = x2;
                        //x2 += excelCurrentRowHeight;

                        if (data.Rows[j][column_i.ColumnName] == DBNull.Value) continue;
                        try
                        {
                            var picture = (byte[])data.Rows[j][column_i.ColumnName];
                            workBook.addPicture(excelCurrentColumnIndex, excelCurrentRowIndex,
                                excelCurrentColumnIndex + 1, excelCurrentRowIndex + 1, picture);
                        }
                        catch// (Exception ex)
                        {
                            //
                        }
                    }
                }
                else if (type == typeof(decimal) || type == typeof(float) || type == typeof(double)
                    || type == typeof(int) || type == typeof(long) || type == typeof(short)
                    || type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort)
                    || type == typeof(byte)
                    )
                {
                    // Áp định dạng canh phải cho kiểu số
                    RangeStyle rs = workBook.getRangeStyle(exportStartRow, setting.startColumn + i, exportStartRow + numOfRows, setting.startColumn + i);
                    rs.HorizontalAlignment = RangeStyle.HorizontalAlignmentRight;
                    workBook.setRangeStyle(rs, exportStartRow, setting.startColumn + i, exportStartRow + numOfRows, setting.startColumn + i);

                    for (int j = 0; j < numOfRows; j++)
                    {
                        workBook.setNumber(exportStartRow + j, setting.startColumn + i, ObjectAndString.ToObject<double>(data.Rows[j][column_i.ColumnName]));
                    }
                }
                else
                {
                    for (int j = 0; j < numOfRows; j++)
                    {
                        workBook.setText(exportStartRow + j, setting.startColumn + i, ObjectAndString.ObjectToString(data.Rows[j][column_i.ColumnName]));
                    }
                }
                #endregion chèn dữ liệu cột i

                

                if (autoColWidth) workBook.setColWidthAutoSize(i, true);
            }

            
        }


        private class ColumnInfo
        {
            public ColumnInfo(DataColumn dataColumn)
            {
                _dataColumn = dataColumn;
            }

            private DataColumn _dataColumn;
            public int MergeFrom { get; set; }
            public int MergeTo { get; set; }
            public bool IsMerge { get { return MergeTo > MergeFrom; } }
            public int ExcelColumnIndex { get { return MergeFrom; } private set { MergeFrom = value; } }
            public string ColumnName { get { return _dataColumn == null ? ColumnNameText : _dataColumn.ColumnName; } }
            public string ColumnNameText { get; set; }

            public bool ColumnNameIsText
            {
                get
                {
                    return !string.IsNullOrEmpty(ColumnNameText) && ColumnNameText.StartsWith("\"") && ColumnNameText.EndsWith("\"");
                }
            }

            public Type DataType { get { return _dataColumn == null ? typeof(string) : _dataColumn.DataType; } }

            /// <summary>
            /// Chỉ định cột Excel.
            /// </summary>
            /// <param name="A">A hoặc A+B hoặc B+D</param>
            public void SetExcelColumn(string A)
            {
                if (string.IsNullOrEmpty(A))
                {
                    MergeFrom = 0;
                    MergeTo = 0;
                }
                else
                {
                    if (A.Contains("+"))
                    {
                        var ss = A.Split("+".ToCharArray(), 2);
                        MergeFrom = GetExcelColumn(ss[0]);
                        MergeTo = GetExcelColumn(ss[1]);
                    }
                    else
                    {
                        MergeFrom = GetExcelColumn(A);
                    }
                }
            }

            public bool IsBold { get; set; }
            public bool IsItalic { get; set; }
            public bool IsUnderline { get; set; }
            public Color Color { get; set; }
        }

        /// <summary>
        /// Xuất hai bảng dữ liệu có liên kết 1 nhiều ra file excel đè lên mẫu có sẵn.
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu</param>
        /// <param name="data1">Dữ liệu bảng 1 (chính/AM)</param>
        /// <param name="data2">Dữ liệu bảng nhiều</param>
        /// <param name="keys">Các cột khóa liên kết giữa 2 bảng.</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, không được trùng với file mẫu</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="column_config">
        /// <para>Thông tin trường (cột) dòng chính.</para>
        /// <para>Thông tin trường (cột) các dòng chi tiết.</para>
        /// <para>Thông tin dòng tổng của mối nhóm.</para>
        /// </param>
        /// <param name="headers">Tiêu đề cột. null hoặc rỗng sẽ bỏ qua.</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplateGroup(string xlsTemplateFile, ExportExcelSetting setting, string[] keys, string saveFile,
            string firstCell, IDictionary<string, string> column_config, string[] headers, SortedDictionary<string, object> parameters,
            NumberFormatInfo nfi, bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                {
                    workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);
                    workbook.PrintGridLines = false;
                }
                
                //select sheet
                int sheetIndex = 0;
                workbook.Sheet = sheetIndex;
                if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(sheetIndex, setting.sheet_name);
                int sheetCount = workbook.NumSheets;
                string sheetName = workbook.getSheetName(sheetIndex);
                //string t;

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

                SetParametersAddressFormat(workbook, parameters);

                ImportDataGroup(workbook, setting, keys, column_config, rowInsert, false, drawLine, startRow, startCol, -1, -1);

                //Nếu rowIndex = 0 thì chèn thêm một dòng
                if (headers != null && headers.Length > 0)
                {
                    if (startRow == 0)
                    {
                        workbook.insertRange(startRow, startCol, startRow, startCol + headers.Length - 1, WorkBook.ShiftRows);
                    }
                    SetHeaders(workbook, headers, startRow, startCol, drawLine);
                }

                string save_ext = Path.GetExtension(saveFile).ToLower();
                if (save_ext == ".xlsx") workbook.writeXLSX(saveFile);
                else workbook.write(saveFile);
                workbook.Dispose();
                return true;//a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplateGroup " + ex.Message;
                workbook.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColumnXX"></param>
        /// <param name="ColumnXXconfig"></param>
        /// <param name="settingData">data hoặc data2 ...</param>
        /// <returns></returns>
        private static Dictionary<string, ColumnInfo> ReadColumnDic(string ColumnXX, string ColumnXXconfig, DataTable settingData)
        {
            var columnDic = new Dictionary<string, ColumnInfo>();
            var columns1 = ObjectAndString.SplitString(ColumnXXconfig);

            foreach (string index_field in columns1)
            {
                string ab_field_biu_color = index_field;

                string ab = null, field = null, biu = null, color = null;
                int index = ab_field_biu_color.IndexOf(':');
                if (index > 0)
                {
                    ab = ab_field_biu_color.Substring(0, index);
                    ab_field_biu_color = ab_field_biu_color.Substring(index + 1);
                    // đặt biệt nếu ab = "COLOR", setting để tô màu cho cả dòng.
                    if (ab == "COLOR")
                    {
                        //...
                        var ci1 = new ColumnInfo(null);
                        //ci1.ColumnNameIsText = true;
                        ci1.ColumnNameText = ab;
                        //ci1.SetExcelColumn("IV");
                        ci1.Color = ObjectAndString.StringToColor(ab_field_biu_color);
                        columnDic.Add(ci1.ColumnName, ci1);
                        continue;
                    }
                }
                else
                {
                    throw new Exception(ColumnXX + " không đủ thông tin. Mẫu: A:MA_VT hoặc A+B:MA_VT:BIU:Blue");
                }

                if (ab_field_biu_color.StartsWith("\"")) // value chuỗi cứng. ví dụ "Tổng cộng:"
                {
                    index = ab_field_biu_color.LastIndexOf("\"");
                    field = ab_field_biu_color.Substring(0, index + 1);
                    ab_field_biu_color = ab_field_biu_color.Substring(index + 1);
                    if (ab_field_biu_color.StartsWith(":")) ab_field_biu_color = ab_field_biu_color.Substring(1);
                }
                else
                {
                    index = ab_field_biu_color.IndexOf(':');
                    if (index > 0)
                    {
                        field = ab_field_biu_color.Substring(0, index);
                        ab_field_biu_color = ab_field_biu_color.Substring(index + 1);
                    }
                    else
                    {
                        field = ab_field_biu_color;
                        ab_field_biu_color = "";
                    }
                }

                index = ab_field_biu_color.IndexOf(':');
                if (index > 0)
                {
                    biu = ab_field_biu_color.Substring(0, index);
                    ab_field_biu_color = ab_field_biu_color.Substring(index + 1);
                    index = ab_field_biu_color.IndexOf(':');
                    if (index > 0)
                    {
                        color = ab_field_biu_color.Substring(0, index);
                        ab_field_biu_color = ab_field_biu_color.Substring(index + 1);
                    }
                    else
                    {
                        color = ab_field_biu_color;
                    }
                }
                else if (!string.IsNullOrEmpty(ab_field_biu_color))
                {
                    biu = ab_field_biu_color;
                }

                ColumnInfo ci = null;
                if (settingData.Columns.Contains(field))
                {
                    ci = new ColumnInfo(settingData.Columns[field]);
                    ci.SetExcelColumn(ab.Trim());
                    //if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(ColumnXX + " trùng cột.");
                    columnDic.Add(ci.ColumnName, ci);
                }
                else if (field.StartsWith("\"") && field.EndsWith("\""))
                {
                    ci = new ColumnInfo(null);
                    ci.ColumnNameText = field;
                    ci.SetExcelColumn(ab.Trim());
                    //if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(ColumnXX + " trùng cột.");
                    columnDic.Add(ci.ColumnName, ci);
                }

                if (biu != null)
                {
                    if (biu.Contains("B")) ci.IsBold = true;
                    if (biu.Contains("I")) ci.IsItalic = true;
                    if (biu.Contains("U")) ci.IsUnderline = true;
                }
                if (color != null)
                {
                    ci.Color = ObjectAndString.StringToColor(color);
                }
            }


            return columnDic;
        }

        /// <summary>
        /// Xuất dữ liệu ra excel có nhóm từ 2 bảng (1 - nhiều).
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="data">Bảng 1</param>
        /// <param name="data2">Bảng nhiều</param>
        /// <param name="keys"></param>
        /// <param name="columns_config">
        /// <para>Thông tin trường (cột) dòng chính.</para>
        /// <para>Thông tin trường (cột) các dòng chi tiết.</para>
        /// <para>Thông tin dòng tổng của mối nhóm.</para>
        /// </param>
        /// <param name="isShiftRows">Chèn thêm dòng cho vùng dữ liệu.</param>
        /// <param name="isFieldNameShown"></param>
        /// <param name="drawLine"></param>
        /// <param name="firstRow"></param>
        /// <param name="firstColumn"></param>
        /// <param name="maxRows"></param>
        /// <param name="maxColumns"></param>
        /// <param name="autoColWidth"></param>
        /// <param name="bPreserveTypes"></param>
        private static void ImportDataGroup(
            WorkBook workBook, ExportExcelSetting setting, string[] keys, IDictionary<string, string> columns_config, 
            bool isShiftRows, bool isFieldNameShown, bool drawLine,
            int firstRow, int firstColumn,
            int maxRows, int maxColumns,
            //IList<DataColumn> arrColumns = null,
            bool autoColWidth = false, bool bPreserveTypes = true)
        {
            if (setting.data == null || setting.data2 == null)
                throw new ArgumentNullException("setting.data and setting.data2 null");

            
            var listColumnDic1 = new SortedDictionary<string, Dictionary<string, ColumnInfo>>();
            var listColumnDic2 = new SortedDictionary<string, Dictionary<string, ColumnInfo>>();
            var listColumnDic3 = new SortedDictionary<string, Dictionary<string, ColumnInfo>>();
            int lastColumnIndex = firstColumn;

            foreach (KeyValuePair<string, string> item in columns_config)
            {
                var columnDic = new Dictionary<string, ColumnInfo>();
                var columns1 = ObjectAndString.SplitString(item.Value);
                
                if (item.Key.ToUpper().StartsWith("COLUMNS1")) // Các dòng chính
                {
                    //foreach (string index_field in columns1)
                    //{
                    //    string[] ss = index_field.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    //    if (ss.Length < 2) throw new Exception(item.Key + " không đủ thông tin. Mẫu: A:MA_VT hoặc A+B:MA_VT");
                    //    ColumnInfo ci = null;
                    //    if (setting.data.Columns.Contains(ss[1]))
                    //    {
                    //        ci = new ColumnInfo(setting.data.Columns[ss[1]]);
                    //        ci.SetExcelColumn(ss[0].Trim());
                    //        if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(item.Key + " trùng cột.");
                    //        columnDic.Add(ci.ExcelColumnIndex, ci);
                    //    }
                    //    else if (ss[1].StartsWith("\"") && ss[1].EndsWith("\""))
                    //    {
                    //        ci = new ColumnInfo(null);
                    //        ci.ColumnNameText = ss[1];
                    //        ci.SetExcelColumn(ss[0].Trim());
                    //        if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(item.Key + " trùng cột.");
                    //        columnDic.Add(ci.ExcelColumnIndex, ci);
                    //    }
                    //}
                    columnDic = ReadColumnDic(item.Key.ToUpper(), item.Value, setting.data);

                    listColumnDic1[item.Key.ToUpper()] = columnDic;
                    if (columnDic.Count > 0)
                    {
                        int temp_last_column = columnDic.Last().Value.ExcelColumnIndex;
                        if (temp_last_column > lastColumnIndex) lastColumnIndex = temp_last_column;
                    }
                }
                else if (item.Key.ToUpper().StartsWith("COLUMNS2")) // Các dòng chi tiết.
                {
                    //foreach (string index_field in columns1)
                    //{
                    //    string[] ss = index_field.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    //    if (ss.Length < 2) throw new Exception(item.Key + " không đủ thông tin. Mẫu: A:MA_VT hoặc A+B:MA_VT");
                    //    ColumnInfo ci = null;
                    //    if (setting.data2.Columns.Contains(ss[1]))
                    //    {
                    //        ci = new ColumnInfo(setting.data2.Columns[ss[1]]);
                    //        ci.SetExcelColumn(ss[0].Trim());
                    //        if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(item.Key + " trùng cột.");
                    //        columnDic.Add(ci.ExcelColumnIndex, ci);
                    //    }
                    //    else if (ss[1].StartsWith("\"") && ss[1].EndsWith("\""))
                    //    {
                    //        ci = new ColumnInfo(null);
                    //        ci.ColumnNameText = ss[1];
                    //        ci.SetExcelColumn(ss[0].Trim());
                    //        if (columnDic.ContainsKey(ci.ExcelColumnIndex)) throw new Exception(item.Key + " trùng cột.");
                    //        columnDic.Add(ci.ExcelColumnIndex, ci);
                    //    }
                    //}
                    columnDic = ReadColumnDic(item.Key.ToUpper(), item.Value, setting.data2);

                    listColumnDic2[item.Key.ToUpper()] = columnDic;
                    if (columnDic.Count > 0)
                    {
                        int temp_last_column = columnDic.Last().Value.ExcelColumnIndex;
                        if (temp_last_column > lastColumnIndex) lastColumnIndex = temp_last_column;
                    }
                }
                else if (item.Key.ToUpper().StartsWith("COLUMNS3")) // Các dòng tổng
                {
                    columnDic = ReadColumnDic(item.Key.ToUpper(), item.Value, setting.data);
                    listColumnDic3[item.Key.ToUpper()] = columnDic;
                    if (columnDic.Count > 0)
                    {
                        int temp_last_column = columnDic.Last().Value.ExcelColumnIndex;
                        if (temp_last_column > lastColumnIndex) lastColumnIndex = temp_last_column;
                    }
                }
                //listColumnDic[item.Key.ToUpper()] = columnDic;
            }
            

            #region === Điền tên cột === // Cần làm lại. COLUMNS0

            int numOfColumns1 = lastColumnIndex - firstColumn + 1;
            goto Cancel_Columns_Text;
            if (isFieldNameShown)
            {
                //Nếu hiện tên cột thì chèn thêm một dòng
                workBook.insertRange(firstRow, firstColumn, firstRow, firstColumn + numOfColumns1 - 1, WorkBook.ShiftRows);
                
                //for (int i = 0; i < numOfColumns1; i++)
                //{
                //    //workBook.setText(firstRow, i + firstColumn, listColumnDic1["COLUMNS11"][i].ColumnName);
                //}
                var listColumnDic11 = listColumnDic1["COLUMNS11"];
                foreach (var item in listColumnDic11)
                {
                    workBook.setText(firstRow, item.Value.ExcelColumnIndex, item.Value.ColumnName);
                }
                
                firstRow++;
            }
            Cancel_Columns_Text:

            #endregion điền tên cột

            // ImportData
            DataView data2view = new DataView(setting.data2);
            // Số lượng dòng đã import.
            int importRowCount = 0;
            // index dòng đang đứng.
            int importRowCurrentIndex = firstRow;
            int row1Index = 0;
            // Duyệt qua từng dòng
            

            foreach (DataRow row1 in setting.data.Rows)
            {
                //importRowCurrentIndex = firstRow + importRowCount;    

                #region ==== Điền dòng chính (nếu có) ====================================================================================================

                foreach (var item in listColumnDic1)
                {
                    var columnDic1 = item.Value;
                    RangeStyle rs = isShiftRows
                        ? InsertRange(workBook, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex)
                        : workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                    //SetBorderRange(workBook, rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                    if (setting.BOLD_YN && setting.BOLD_CONDITION != null)
                    {
                        if (setting.BOLD_CONDITION.Check(row1))
                        {
                            //rs = workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                            rs.FontBold = true;
                            workBook.setRangeStyle(rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                        }
                    }
                    ImportDataRow_Group(workBook, row1, columnDic1, importRowCurrentIndex, firstColumn, lastColumnIndex);
                    
                    importRowCount++;
                    importRowCurrentIndex++;
                }

                #endregion //Điền xong 1 dòng chính

                #region ==== Điền vài dòng chi tiết (của dòng chính) ========================================================================================================

                foreach (var item in listColumnDic2)
                {
                    var columnDic2 = item.Value;
                    string filter = "";
                    foreach (string field in keys)
                    {
                        filter += string.Format("and [{0}] = {1}", field, GenFilterValue(row1[field], false));
                    }
                    filter = filter.Substring(4);
                    data2view.RowFilter = filter;
                    DataTable data2filter = data2view.ToTable();

                    Color default_line_color = Color.Transparent;
                    if (!string.IsNullOrEmpty(setting.M_COLOR_SUM)) default_line_color = ObjectAndString.StringToColor(setting.M_COLOR_SUM);
                    // ==== Duyệt qua từng dòng chi tiết ====
                    foreach (DataRow row2 in data2filter.Rows)
                    {
                        RangeStyle rs = isShiftRows
                            ? InsertRange(workBook, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex)
                            : workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);

                        //SetBorderRange(workBook, rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                        ImportDataRow_Group(workBook, row2, columnDic2, importRowCurrentIndex, firstColumn, lastColumnIndex);
                        if (setting.BOLD_YN && setting.BOLD_CONDITION != null)
                        {
                            if (setting.BOLD_CONDITION.Check(row2))
                            {
                                //rs = workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                                rs.FontBold = true;
                                workBook.setRangeStyle(rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                            }
                        }

                        if (!string.IsNullOrEmpty(setting.COLOR_FIELD))
                        {
                            var color = default_line_color;
                            if (row2.Table.Columns.Contains(setting.COLOR_FIELD) && ("" + row2[setting.COLOR_FIELD]).Trim() != "")
                            {
                                color = ObjectAndString.StringToColor("" + row2[setting.COLOR_FIELD]);
                            }
                            else
                            {
                                color = default_line_color;
                            }
                            
                            rs.Pattern = RangeStyle.PatternSolid;
                            rs.PatternFG = color.ToArgb();
                            workBook.setRangeStyle(rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                        }
                        

                        importRowCount++;
                        importRowCurrentIndex++;
                    }
                }

                #endregion// Điền xong vài dòng chi tiết

                #region ==== Tiếp tục dòng tổng nếu có. ========================================================================================================

                foreach (var item in listColumnDic3)
                {
                    var columnDic3 = item.Value;
                    RangeStyle rs = isShiftRows ?
                        InsertRange(workBook, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex) :
                        workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                    //SetBorderRange(workBook, rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                    
                    if (setting.BOLD_YN && setting.BOLD_CONDITION != null)
                    {
                        if (setting.BOLD_CONDITION.Check(row1))
                        {
                            rs = workBook.getRangeStyle(importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                            rs.FontBold = true;
                            workBook.setRangeStyle(rs, importRowCurrentIndex, firstColumn, importRowCurrentIndex, lastColumnIndex);
                        }
                    }
                    ImportDataRow_Group(workBook, row1, columnDic3, importRowCurrentIndex, firstColumn, lastColumnIndex);
                    
                    importRowCount++;
                    importRowCurrentIndex++;
                }
                
                #endregion// Kết thúc dòng tổng.

                
                row1Index++;
            } //end for row1

            var rs1 = workBook.getRangeStyle(firstRow, firstColumn, importRowCurrentIndex - 1, lastColumnIndex);
            SetBorderRange(workBook, rs1, firstRow, firstColumn, importRowCurrentIndex - 1, lastColumnIndex);
            
            //if (autoColWidth) workBook.setColWidthAutoSize(columnIndex, true);
        }

        private static RangeStyle SetBorderRange(WorkBook workBook, RangeStyle range,
            int startRow, int startCol, int endRow, int endColumn,
            bool top = true, bool bottom = true, bool left = true, bool right = true,
            bool horizontal = true, bool vertical = true)
        {
            //RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endCol);
            range.LeftBorder = left ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            range.RightBorder = right ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            range.TopBorder = top ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            range.BottomBorder = bottom ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            range.HorizontalInsideBorder = horizontal ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            range.VerticalInsideBorder = vertical ? RangeStyle.BorderThin : RangeStyle.BorderNone;
            
            workBook.setRangeStyle(range, startRow, startCol, endRow, endColumn);
            return range;
        }

        /// <summary>
        /// Thêm vùng và reset format vùng đó.
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        private static RangeStyle InsertRange(WorkBook workBook, int startRow, int startCol, int endRow, int endColumn)
        {
            workBook.insertRange(startRow, startCol, endRow, endColumn, WorkBook.ShiftRows);
            RangeStyle rangeStyle = workBook.getRangeStyle(startRow, startCol, endRow, endColumn);
            ResetRangeStyleFormat(rangeStyle);
            workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endColumn);
            return rangeStyle;
        }

        /// <summary>
        /// Chèn 1 dòng dữ liệu vào Excel theo các cột cấu hình sẵn.
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="row1"></param>
        /// <param name="importRowIndex"></param>
        /// <param name="columnDic"></param>
        private static void ImportDataRow_Group(WorkBook workBook, DataRow row1, Dictionary<string, ColumnInfo> columnDic,
            int importRowIndex, int firtColumn, int lastColumn)
        {
            try
            {
                foreach (KeyValuePair<string, ColumnInfo> item in columnDic)
                {
                    var column = item.Value;
                    //var type = column.DataType;
                    //var field = column.ColumnName;
                    //int columnIndex = index_column.Key;

                    RangeStyle rs = workBook.getRangeStyle(importRowIndex, column.ExcelColumnIndex, importRowIndex, column.ExcelColumnIndex);
                    if (item.Value.ColumnNameText == "COLOR")
                    {
                        rs.Pattern = RangeStyle.PatternSolid;
                        rs.PatternFG = item.Value.Color.ToArgb();
                        workBook.setRangeStyle(rs, importRowIndex, firtColumn, importRowIndex, lastColumn);
                        continue;
                    }
                    //Merge
                    if (column.IsMerge)
                    {
                        RangeStyle rsM = workBook.getRangeStyle(importRowIndex, column.ExcelColumnIndex, importRowIndex, column.MergeTo);
                        rsM.MergeCells = true;
                        rsM.LeftBorder = RangeStyle.BorderThin;
                        rsM.RightBorder = RangeStyle.BorderThin;
                        rsM.TopBorder = RangeStyle.BorderThin;
                        rsM.BottomBorder = RangeStyle.BorderThin;
                        workBook.setRangeStyle(rsM, importRowIndex, column.ExcelColumnIndex, importRowIndex, column.MergeTo);
                    }

                    // Font style
                    if (column.IsBold) rs.FontBold = column.IsBold;
                    rs.FontItalic = column.IsItalic;
                    rs.FontUnderline = (short) (column.IsUnderline ? 2 : 0);
                    rs.FontColor = column.Color.ToArgb();
                    workBook.setRangeStyle(rs, importRowIndex, column.ExcelColumnIndex, importRowIndex, column.ExcelColumnIndex);

                    if (column.ColumnNameIsText)
                    {
                        string text = column.ColumnNameText.Trim('"');
                        if (text != "") workBook.setText(importRowIndex, column.ExcelColumnIndex, text);
                    }
                    else if (column.DataType == typeof(DateTime))
                    {
                        
                        var systemFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                        rs.CustomFormat = systemFormat; // "dd/mm/yyyy"; // excel format
                        // Áp định dạng.
                        workBook.setRangeStyle(rs, importRowIndex, column.ExcelColumnIndex, importRowIndex, column.ExcelColumnIndex);

                        var date_string = ObjectAndString.ObjectToString(row1[column.ColumnName], systemFormat);
                        if (date_string != null)
                        {
                            workBook.setEntry(importRowIndex, column.ExcelColumnIndex, date_string);
                        }
                        else
                        {
                            workBook.setEntry(importRowIndex, column.ExcelColumnIndex, "");
                        }
                        //Ap lai format dd/MM/yyyy
                        rs.CustomFormat = "dd/MM/yyyy";
                        workBook.setRangeStyle(rs, importRowIndex, column.ExcelColumnIndex, importRowIndex, column.ExcelColumnIndex);
                        // Sở dĩ thay đổi format là để gán đúng giá trị theo định dạng.
                        // Đầu tiên gán theo format hệ thống cho excel nhận đúng giá trị.
                        // Sau đó đổi lại định dạng hiển thị riêng.
                    }
                    else if (ObjectAndString.IsNumberType(column.DataType))
                    {
                        // Áp định dạng canh phải cho kiểu số
                        rs.HorizontalAlignment = RangeStyle.HorizontalAlignmentRight;
                        workBook.setRangeStyle(rs, importRowIndex, column.ExcelColumnIndex, importRowIndex, column.ExcelColumnIndex);
                        workBook.setNumber(importRowIndex, column.ExcelColumnIndex, ObjectAndString.ToObject<double>(row1[column.ColumnName]));
                    }
                    else
                    {
                        string text = ObjectAndString.ObjectToString(row1[column.ColumnName]);
                        if (text != "") workBook.setText(importRowIndex, column.ExcelColumnIndex, text);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Data_Table.ImportDataRow_Group", ex, "");
            }
        }

        public static string GenFilterValue(object value, bool allowNull, string oper = "=")
        {
            string s;
            bool like = oper == "like";
            bool start = oper == "start";
            if (value != null)
            {
                switch (value.GetType().ToString())
                {
                    case "System.DateTime":
                    case "System.DateTime?":
                    case "System.Nullable`1[System.DateTime]":
                        try
                        {
                            s = "'" + ((DateTime)value)
                                .ToString("yyyyMMdd") + "'";
                        }
                        catch
                        {
                            if (allowNull)
                                s = "null";
                            else
                                s = "'" + DateTime.Now.ToString("yyyyMMdd") + "'";
                        }
                        break;
                    case "System.Boolean":
                        s = (bool)value ? "1" : "0";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        try
                        {
                            s = value.ToString().Replace(ObjectAndString.SystemDecimalSymbol, ".");
                        }
                        catch
                        {
                            s = "0";
                        }
                        break;
                    case "System.DBNull":
                        s = "null";
                        break;
                    default:
                        s = "'" + (like ? "%" : "")
                            + value.ToString().Trim().Replace("'", "''")
                            + (like || start ? "%" : "") + "'";
                        break;
                }
            }
            else if (allowNull)
            {
                s = "null";
            }
            else
            {
                s = "''";
            }

            return s;
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

        /// <summary>
        /// hàm rỗng
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="value"></param>
        private static void SetEntry(WorkBook workbook, object value)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// Gán dữ liệu vào Excel theo địa chỉ dạng Idic( key{A1:format} value{object} )
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="parameters"></param>
        private static void SetParametersAddressFormat(WorkBook workbook, IDictionary<string, object> parameters)
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
        
        private static void SetParametersSheetAddress(WorkBook workbook, IDictionary<string, object> parameters)
        {
            #region ===== Parameter {A1:dd/mm/yyyy,29-01-1987} =====

            var system_date_format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            //Write parameter with key is address[:format]
            foreach (KeyValuePair<string, object> item in parameters)
            {
                workbook.Sheet = 0;
                var KEY = item.Key;
                string A1 = KEY, sheet = null, format = null;
                if (string.IsNullOrEmpty(A1)) continue;

                if (KEY.Contains(":"))
                {
                    var ss = KEY.Split(':');
                    A1 = ss[1];
                    sheet = ss[0];
                    workbook.Sheet = ObjectAndString.ObjectToInt(sheet);
                    if (ss.Length > 2)
                    {
                        format = ss[2];
                    }
                    else
                    {
                        format = null;
                    }
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

        /// <summary>
        /// Gán dữ liệu vào Excel workbook nếu ô có text = KEY
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="parameters"></param>
        private static void SetParametersByName(WorkBook workbook, IDictionary<string, object> parameters)
        {
            int lastRow = workbook.LastRow;
            int lastCol = workbook.LastCol;
            for (int r = 0; r <= lastRow; r++)
            {
                for (int c = 0; c <= lastCol; c++)
                {
                    string tName = workbook.getText(r, c).ToUpper().Trim();
                    if (parameters.ContainsKey(tName))
                    {
                        //SetEntryFormat(workbook, ObjectAndString.ObjectToString(parameters[tName]), exc);
                        workbook.setEntry(r, c, ObjectAndString.ObjectToString(parameters[tName]));
                    }
                }
            }
        }

        static WorkBook ReadWorkBookCopy(string xlsTemplateFile, string saveFile)
        {
            if(!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);

            WorkBook workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10*20, 1);

            xlsTemplateFile = Path.GetFullPath(xlsTemplateFile);
            saveFile = Path.GetFullPath(saveFile);
            if (String.Equals(saveFile, xlsTemplateFile, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ExportException("File lưu trùng file mẫu!");
            }
            string read_ext = Path.GetExtension(xlsTemplateFile).ToLower();
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

                            if (read_ext == ".xls")
                                workbook.read(saveFile);
                            else if (read_ext == ".xlsx")
                                workbook.readXLSX(saveFile);
                            else if (read_ext == ".xml")
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
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết. Có thể là một công thức FieldA+FieldB</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, không được trùng với file mẫu</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplate(string xlsTemplateFile, ExportExcelSetting setting, NumberFormatInfo nfi)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10*20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                {
                    workbook = ReadWorkBookCopy(xlsTemplateFile, setting.saveFile);
                    workbook.PrintGridLines = false;
                }
                
                //select sheet

                workbook.Sheet = 0;// setting.sheetIndex;
                if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(0, setting.sheet_name);
                int sheetCount = workbook.NumSheets;
                //string sheetName = workbook.getSheetName(sheetIndex);
                //string t;

                //int startRow = 1, startCol = 0;
                int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                int lastCol = workbook.LastCol;
                if (string.IsNullOrEmpty(setting.firstCell))
                {
                    setting.startRow = lastRow +1;
                    setting.startColumn = 0;
                }

                SetParametersAddressFormat(workbook, setting.parameters);

                //var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
                //int endCol;// startCol + data.Columns.Count - 1;
                setting.isFieldNameShow = false;
                ImportDataTable(workbook, setting, -1, -1);

                string save_ext = Path.GetExtension(setting.saveFile).ToLower();
                if (save_ext == ".xlsx") workbook.writeXLSX(setting.saveFile);
                else workbook.write(setting.saveFile);
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


        public static bool ToExcelTemplate_ManySheet(string xlsTemplateFile, string saveFile, List<ExportExcelSetting> setting_list, NumberFormatInfo nfi0)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);

            if (File.Exists(xlsTemplateFile))
            {
                workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);
                workbook.PrintGridLines = false;
            }
            
            int sheet_index = 0;
            foreach (ExportExcelSetting setting in setting_list)
            {
                try
                {
                    workbook.Sheet = sheet_index;
                    workbook.copySheet(sheet_index + 1);
                    //select sheet
                    workbook.Sheet = sheet_index;

                    if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(sheet_index, setting.sheet_name);
                    int sheetCount = workbook.NumSheets;
                    //string sheetName = workbook.getSheetName(sheetIndex);
                    //string t;

                    //int startRow = 1, startCol = 0;
                    int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                    int lastCol = workbook.LastCol;
                    if (string.IsNullOrEmpty(setting.firstCell))
                    {
                        setting.startRow = lastRow + 1;
                        setting.startColumn = 0;
                    }

                    SetParametersAddressFormat(workbook, setting.parameters);

                    //var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
                    //int endCol;// startCol + data.Columns.Count - 1;
                    setting.isFieldNameShow = false;
                    ImportDataTable(workbook, setting, -1, -1);

                }
                catch (Exception ex)
                {
                    Message = "Data_Table ToExcelTemplate " + ex.Message;
                    workbook.Dispose();
                    
                }
                sheet_index++;
            }

            workbook.Sheet = 0;
            string save_ext = Path.GetExtension(saveFile).ToLower();
            if (save_ext == ".xlsx") workbook.writeXLSX(saveFile);
            else workbook.write(saveFile);
            workbook.Dispose();
            return true;//a false nhưng vẫn lưu file thành công???

            
        }

        


        /// <summary>
        /// Xuất một bảng dữ liệu ra file excel với mẫu có sẵn
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu</param>
        /// <param name="data">Dữ liệu vào</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết. Có thể là một công thức FieldA+FieldB</param>
        /// <param name="saveFile">Tên tập tin sẽ lưu, chỉ dùng lấy phần mở rộng .xlsx</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static Stream ToExcelTemplateStream(string xlsTemplateFile, ExportExcelSetting setting, string saveFile,
            string[] columns, SortedDictionary<string, object> parameters,
            NumberFormatInfo nfi, bool rowInsert = false, bool drawLine = false)
        {
            Stream stream = new MemoryStream();
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            
            string read_ext = Path.GetExtension(xlsTemplateFile).ToLower();
            if (File.Exists(xlsTemplateFile))
            {
                #region ==== workbook try to read file ====

                try
                {
                    try
                    {
                        try
                        {

                            if (read_ext == ".xls")
                                workbook.read(xlsTemplateFile);
                            else if (read_ext == ".xlsx")
                                workbook.readXLSX(xlsTemplateFile);
                            else if (read_ext == ".xml")
                                workbook.readXML(xlsTemplateFile);
                            else
                                workbook.read(xlsTemplateFile);
                        }
                        catch
                        {
                            workbook.readXLSX(xlsTemplateFile);
                        }
                    }
                    catch
                    {
                        workbook.readXML(xlsTemplateFile);
                    }
                }
                catch
                {
                    workbook.read(xlsTemplateFile);
                }

                #endregion
            }
            else
            {
                throw new Exception("Không có file mẫu.");
            }

            //select sheet
            int sheetIndex = 0;
            workbook.Sheet = sheetIndex;
            if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(sheetIndex, setting.sheet_name);
            int sheetCount = workbook.NumSheets;
            
            
            int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
            int lastCol = workbook.LastCol;
            if (string.IsNullOrEmpty(setting.firstCell))
            {
                setting.startRow = lastRow + 1;
                setting.startColumn = 0;
            }

            SetParametersAddressFormat(workbook, parameters);

            //var endRow = startRow + data.Rows.Count - (data.Rows.Count > 0 ? 1 : 0);
            //int endCol;// startCol + data.Columns.Count - 1;
            setting.isInsertRow = rowInsert;
            setting.isDrawLine = drawLine;
            setting.isFieldNameShow = false;
            ImportDataTable(workbook, setting, -1, -1);
                
            string save_ext = Path.GetExtension(saveFile).ToLower();
            if (save_ext == ".xlsx") workbook.writeXLSX(stream);
            else workbook.write(stream);
            workbook.Dispose();
                

            
            return stream;
        }


        /// <summary>
        /// Xuất một bảng dữ liệu ra file excel với mẫu có sẵn, thêm tiêu đề gửi vào
        /// </summary>
        /// <param name="data">Dữ liệu vào</param>
        /// <param name="firstCell">Vị trí ô bắt đầu điền dữ liệu vd: A2.</param>
        /// <param name="columns">Danh sách cột dữ liệu sẽ lấy, null nếu lấy hết.</param>
        /// <param name="headers">Tiêu đề cột. null hoặc rỗng sẽ bỏ qua.</param>
        /// <param name="parameters">Giá trị theo vị trí trong excel. Với key là vị trí vd: A1</param>
        /// <param name="nfi">Thông tin định dạng kiểu số</param>
        /// <param name="drawLine">Vẽ đường kẻ lên dữ liệu</param>
        /// <param name="rowInsert">Chèn dữ liệu vào vị trí chèn, đẩy dòng xuống.</param>
        /// <returns></returns>
        public static bool ToExcelTemplate(ExportExcelSetting setting,
            string[] columns, string[] headers, SortedDictionary<string, object> parameters,
            NumberFormatInfo nfi, bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if (!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            try
            {
                if (File.Exists(setting.xlsTemplateFile))
                {
                    workbook = ReadWorkBookCopy(setting.xlsTemplateFile, setting.saveFile);
                    workbook.PrintGridLines = false;
                }
                
                //select sheet
                int sheetIndex = 0;
                workbook.Sheet = sheetIndex;
                if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(sheetIndex, setting.sheet_name);
                int sheetCount = workbook.NumSheets;
                

                int lastRow = workbook.LastRow;//Dòng cuối cùng có dữ liệu của sheet
                int lastCol = workbook.LastCol;
                if (string.IsNullOrEmpty(setting.firstCell))
                {
                    setting.startRow = lastRow + 1;
                    setting.startColumn = 0;
                }

                SetParametersAddressFormat(workbook, parameters);
                setting.isInsertRow = rowInsert;
                setting.isDrawLine = drawLine;
                setting.isFieldNameShow = false;
                ImportDataTable(workbook, setting, -1, -1);

                //Nếu rowIndex = 0 thì chèn thêm một dòng
                if (headers != null && headers.Length > 0)
                {
                    if (setting.startRow == 0)
                    {
                        workbook.insertRange(setting.startRow, setting.startColumn, setting.startRow, setting.startColumn + headers.Length - 1, WorkBook.ShiftRows);
                    }
                    SetHeaders(workbook, headers, setting.startRow, setting.startColumn, drawLine);
                }

                string save_ext = Path.GetExtension(setting.saveFile).ToLower();
                if (save_ext == ".xlsx") workbook.writeXLSX(setting.saveFile);
                else workbook.write(setting.saveFile);
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
        public static bool ToExcelTemplateHTKK(string xlsTemplateFile,
            SortedDictionary<string, DataTable> datas, ExportExcelSetting setting, string[] columns, string saveFile, NumberFormatInfo nfi,
            bool rowInsert = false, bool drawLine = false)
        {
            Message = "";
            //if(!File.Exists(xlsTemplateFile)) throw new Exception("Không tồn tại: " + xlsTemplateFile);
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                {
                    workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);
                    workbook.PrintGridLines = false;
                }
                
                SetParametersAddressFormat(workbook, setting.parameters);

                //select sheet
                int sheetIndex = 0;
                workbook.Sheet = sheetIndex;
                if (!string.IsNullOrEmpty(setting.sheet_name)) workbook.setSheetName(sheetIndex, setting.sheet_name);
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

                    setting.startRow = GetExcelRow(firstCell);
                    setting.startColumn = GetExcelColumn(firstCell);
                    setting.data = data;
                    //Sua lap
                    setting.isInsertRow = rowInsert;
                    setting.isDrawLine = drawLine;
                    setting.isFieldNameShow = false;
                    ImportDataTable(workbook, setting, -1, -1);
                }

                string save_ext = Path.GetExtension(xlsTemplateFile).ToLower();
                if (save_ext == ".xlsx") workbook.writeXLSX(saveFile);
                else workbook.write(saveFile);
                workbook.Dispose();

                return true; //a false nhưng vẫn lưu file thành công???

            }
            catch (Exception ex)
            {
                Message = "Data_Table ToExcelTemplate " + ex.Message;
                workbook.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveAs"></param>
        /// <param name="title"></param>
        /// <param name="line">Kẻ ô</param>
        /// <param name="fontName"></param>
        public static bool ToExcel(ExportExcelSetting setting)
        {
            SmartXLS.WorkBook workBook = new SmartXLS.WorkBook();
            workBook.PrintGridLines = false;
            string fontName = setting.fontName;
            if (string.IsNullOrEmpty(fontName) || !IsFontInstalled(fontName)) fontName = "Times New Roman";
            workBook.setDefaultFont(fontName, 12*20, 1);
            int startRow = 0, startCol = 0, endRow = 0, endCol = setting.data.Columns.Count - 1;

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
            if (!string.IsNullOrEmpty(setting.title))
            {
                workBook.setText(startRow, startCol, setting.title);
                if (setting.isDrawLine)
                    workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                startRow++; 
            }

            endRow = startRow + setting.data.Rows.Count;

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

            setting.startRow = startRow;
            setting.startColumn = startCol;
            setting.isInsertRow = false;
            setting.isFieldNameShow = true;
            
            ImportDataTable(workBook, setting, -1, -1);

            var ext = Path.GetExtension(setting.saveFile);
            if (ext != null)
            {
                ext = ext.ToLower();
                switch (ext)
                {
                    case ".xlsx":
                        workBook.writeXLSX(setting.saveFile);
                        break;
                    case ".csv":
                        workBook.writeCSV(setting.saveFile);
                        break;
                    default:
                        workBook.write(setting.saveFile);
                        break;
                }
            }
            workBook.Dispose();
            return true;
        }

        public static Stream ToExcelStream(ExportExcelSetting setting)
        {
            SmartXLS.WorkBook workBook = new SmartXLS.WorkBook();
            workBook.PrintGridLines = false;
            string fontName = setting.fontName;
            if (string.IsNullOrEmpty(fontName) || !IsFontInstalled(fontName)) fontName = "Times New Roman";
            workBook.setDefaultFont(fontName, 12 * 20, 1);
            int startRow = 0, startCol = 0, endRow = 0, endCol = setting.data.Columns.Count - 1;

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
            rangeStyle.FontSize = 12 * 20;

            // Set title
            workBook.Sheet = 0;
            if (!string.IsNullOrEmpty(setting.title))
            {
                workBook.setText(startRow, startCol, setting.title);
                if (setting.isDrawLine)
                    workBook.setRangeStyle(rangeStyle, startRow, startCol, endRow, endCol);
                startRow++;
            }

            endRow = startRow + setting.data.Rows.Count;

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

            setting.startRow = startRow;
            setting.startColumn = startCol;
            setting.isInsertRow = false;
            setting.isFieldNameShow = true;
            ImportDataTable(workBook, setting, -1, -1);

            var ext = Path.GetExtension(setting.saveFile);
            Stream st = new MemoryStream();
            if (ext != null)
            {
                ext = ext.ToLower();
                switch (ext)
                {
                    case ".xlsx":
                        workBook.writeXLSX(st);
                        break;
                    case ".csv":
                        workBook.writeCSV(st);
                        break;
                    default:
                        workBook.write(st);
                        break;
                }
            }
            workBook.Dispose();
            return st;
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

        public static void ToXmlFile(DataTable data, string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            try
            {
                data.WriteXml(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                throw new ExportException("ExportDataToXmlFile " + ex.Message);
            }
        }

        /// <summary>
        /// Ghi dữ liệu vào Excel và lưu thành file mới.
        /// </summary>
        /// <param name="xlsTemplateFile">File Excel mẫu.</param>
        /// <param name="saveFile">File Excel sẽ lưu mới.</param>
        /// <param name="mappingData">Dữ liệu theo tên.</param>
        /// <param name="addressData">Dữ liệu theo địa chỉ. (Mới chỉ áp dụng Sheet 0)</param>
        /// <returns></returns>
        public static bool MappingDataToExcelFile(string xlsTemplateFile, string saveFile,
            IDictionary<string, object> mappingData, IDictionary<string, object> addressData)
        {
            var workbook = new WorkBook();
            workbook.PrintGridLines = false;
            workbook.setDefaultFont("Arial", 10 * 20, 1);
            try
            {
                if (File.Exists(xlsTemplateFile))
                {
                    workbook = ReadWorkBookCopy(xlsTemplateFile, saveFile);
                    workbook.PrintGridLines = false;
                }
                
                if (mappingData != null)
                {
                    for (int i = 0; i < workbook.NumSheets; i++)
                    {
                        workbook.Sheet = i;
                        SetParametersByName(workbook, mappingData);
                    }
                }

                if (addressData != null)
                {
                    workbook.Sheet = 0;
                    SetParametersSheetAddress(workbook, addressData);
                }

                string save_ext = Path.GetExtension(saveFile).ToLower();
                if (save_ext == ".xlsx") workbook.writeXLSX(saveFile);
                else workbook.write(saveFile);
            }
            catch
            {
                return false;
            }
            return true;
        }

        

        
    }


}
