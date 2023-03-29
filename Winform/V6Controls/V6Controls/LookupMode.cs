using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6Controls
{
    public enum LookupMode 
    {
        /// <summary>
        /// Chọn 1 dòng khi lookup, nhận về 1 mã trong senderTextBox.
        /// </summary>
        Single = 0,
        /// <summary>
        /// Chọn nhiều dòng khi lookup, nhận về danh sách mã trong senderTextBox.
        /// </summary>
        Multi = 1,
        /// <summary>
        /// Chọn nhiều dòng dữ liệu khi lookup, nhận về dữ liệu trong sự kiện.
        /// </summary>
        Data = 2
    }
}
