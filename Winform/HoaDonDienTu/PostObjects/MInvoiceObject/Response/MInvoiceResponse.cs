using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V6ThuePost.MInvoiceObject.Response
{
    public class MInvoiceResponse
    {
        public string windowid;
        public string ok;
        public string Message;
        public string error;

        public Dictionary<string, object> data = new Dictionary<string, object>();
    }
}
