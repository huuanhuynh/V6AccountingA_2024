using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6Tools.V6Export
{
    public class ExportExcelSetting
    {
        public bool BOLD_YN = false;
        public Condition BOLD_CONDITION = null;
        //public string IS_BOLD_FIELD = null;
        //public string IS_BOLD_OPER = "=";
        //public string IS_BOLD_VALUE = "1";
        
        public string xlsTemplateFile = null;
        public string saveFile;
        public string title;
        public string fontName;
        public IList<string> Columns = null;
        public bool isShiftRows;
        public bool isShowFieldName;
        public bool isDrawLine;

        public int firstRow;
        public int firstColumn;

        public void SetFirstCell(string firstCell)
        {
            firstRow = GetExcelRow(firstCell);
            firstColumn = GetExcelColumn(firstCell);
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
