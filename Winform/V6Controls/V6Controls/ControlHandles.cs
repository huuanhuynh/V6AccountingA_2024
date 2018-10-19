using System;
using System.Collections.Generic;
using System.Data;

namespace V6Controls
{
    /// <summary>
    /// Xử lý dữ liệu, dataDic là dữ liệu đưa vào để xử lý
    /// Nếu hàm có kiểm tra không thành công thì quăng lỗi.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public delegate void HandleData(SortedDictionary<string, object> data);
    public delegate void DataTableHandler(DataTable table);
    public delegate void DataSelectHandler(string idList, List<IDictionary<string, object>> dataList);
    /// <summary>
    /// Xử lý dữ liệu kết quả (nếu muốn)
    /// </summary>
    /// <param name="dataDic"></param>
    public delegate void HandleResultData(SortedDictionary<string, object> dataDic);
    
    public delegate void ControlEventHandle(object sender);//EventHandler(s,e);
    public delegate void SimpleHandle();

    public delegate void StringValueChanged(string oldvalue, string newvalue);
    public delegate void CheckValueChanged(bool oldvalue, bool newvalue);

    public delegate void LookupButtonEventHandler(Object sender, LookupEventArgs e);
    public class LookupEventArgs : EventArgs
    {
        public string MaCt { get; set; }
        public string Stt_rec { get; set; }
    }

}
