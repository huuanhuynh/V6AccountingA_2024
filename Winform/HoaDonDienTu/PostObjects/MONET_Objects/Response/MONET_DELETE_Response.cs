using System;
using System.Collections.Generic;

namespace V6ThuePost.MONET_Objects.Response
{
    public class MONET_DELETE_Response
    {
        public string action { get; set; }
        public string returnDate { get; set; }
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public Dictionary<string, object> data { get; set; }
        public string invRef { get; set; }
        public string[] tokenInfo { get; set; }
        
    }
}
