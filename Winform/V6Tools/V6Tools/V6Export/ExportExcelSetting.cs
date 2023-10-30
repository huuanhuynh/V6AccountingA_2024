using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace V6Tools.V6Export
{
    public class ExportExcelSetting
    {
        public bool BOLD_YN = false;
        public Condition BOLD_CONDITION { get; set; }
        /// <summary>
        /// Lấy từ xml setting
        /// </summary>
        public string COLOR_FIELD { get; set; }
        /// <summary>
        /// Màu nền dòng dữ liệu xuất Excel. Lấy từ V6Options.
        /// </summary>
        public string M_COLOR_SUM { get; set; }
        //public string IS_BOLD_FIELD = null;
        //public string IS_BOLD_OPER = "=";
        //public string IS_BOLD_VALUE = "1";
        public IDictionary<string, object> albcConfigData = null;
        /// <summary>
        /// data dạng key là địa chỉ Excel:ExcelFormat và value {A1:format, value}
        /// </summary>
        public IDictionary<string, object> parameters = null;
        /// <summary>
        /// Chứa tạm để tạo parameters.
        /// </summary>
        public IDictionary<string, object> reportParameters = null;
        /// <summary>
        /// File Excel mẫu
        /// </summary>
        public string xlsTemplateFile = null;
        /// <summary>
        /// Tên tập tin sẽ lưu, không được trùng với file mẫu
        /// </summary>
        public string saveFile;
        //public int sheetIndex = 0;
        public string sheet_name = null;
        public string title = "";
        public string fontName;
        /// <summary>
        /// data chi tiết, trường hợp group là dữ liệu nhóm.
        /// </summary>
        public DataTable data;
        /// <summary>
        /// data thông tin chính, trường hợp group là data chi tiết.
        /// </summary>
        public DataTable data2 = null;
        /// <summary>
        /// Trường hợp Group. Dữ liệu phụ (thông tin) khi type = 2 (như data2 trường hợp thường).
        /// </summary>
        public DataTable data3 = null;
        public string[] columns = null;
        
        public bool isShowFieldName;
        public bool isDrawLine, isInsertRow, isFieldNameShow;
        public string firstCell = "A4";
        /// <summary>
        /// Dòng bắt đầu chèn dữ liệu (0_base).
        /// </summary>
        public int startRow = 3;
        /// <summary>
        /// Cột bắt đầu chèn dữ liệu (0_base).
        /// </summary>
        public int startColumn = 1;
        /// <summary>
        /// Gán ô bắt đầu chèn dữ liệu theo Excel (vd:A1).
        /// </summary>
        /// <param name="firstCell"></param>
        public void SetFirstCell(string firstCell)
        {
            this.firstCell = firstCell;
            startRow = GetExcelRow(firstCell);
            startColumn = GetExcelColumn(firstCell);
        }

        private static int GetExcelRow(string A1)
        {
            int startIndex = A1.IndexOfAny("0123456789".ToCharArray());
            //string column = A1.Substring(0, startIndex);
            string rowID = A1.Substring(startIndex);
            return int.Parse(rowID) - 1;
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
    }
}
