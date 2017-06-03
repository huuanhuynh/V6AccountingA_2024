using System.Collections.Generic;
using System.Data;

namespace V6Structs
{
    public class V6SelectResult
    {
        public V6SelectResult()
        {
            Page = 1;
            PageSize = 20;
            Where = "";
            SortField = "";
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Where { get; set; }
        public string SortField { get; set; }
        public bool IsSortOrderAscending { get; set; }

        /// <summary>
        /// Tự tính ra từ TotalRows và PageSize
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (TotalRows == 0 || PageSize == 0) return 0;
                int pages = TotalRows / PageSize;
                if (TotalRows % PageSize > 0) pages++;
                return pages;
            }
        }

        public int TotalRows { get; set; }
        /// <summary>
        /// Dữ liệu khi dùng Sqlconnect
        /// </summary>
        public DataTable Data { get; set; }

        public string Fields { get; set; }
        public SortedDictionary<string, string> FieldsHeaderDictionary { get; set; }
    }
}
