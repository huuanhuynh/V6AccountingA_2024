using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V6Tools.V6Objects;

namespace V6ThuePost.MONET_Objects.Request
{
    public class MONET_EDIT_Request : V6JsonObject
    {
        /// <summary>
        /// 1 Điều chỉnh 2 Điều chỉnh tăng 3 Điều chỉnh giảm 4 Thay thế
        /// </summary>
        public string option;// : <1|2|3|4>, 
        public string invCodeOld;//: <invCodeOld_value>, 
        public string invSignOld;//: <Kí hiệu của hóa đơn BỊ điều chỉnh>, 
        public string invNameOld;//: <Mẫu số của hóa đơn BỊ điều chỉnh>, 
        public string invNumberBB;//: < invNumberBB _value> 

    }
}
