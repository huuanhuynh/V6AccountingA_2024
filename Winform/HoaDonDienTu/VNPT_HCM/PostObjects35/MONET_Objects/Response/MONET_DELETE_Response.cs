using System;
using System.Collections.Generic;

namespace V6ThuePost.MONET_Objects.Response
{
    public class MONET_DELETE_Response : MONET_API_Response
    {
        public Dictionary<string, object> data { get; set; }
        public string invRef { get; set; }
        public string[] tokenInfo { get; set; }
        
    }
}
