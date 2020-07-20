using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace V6ThuePost.MONET_Objects.Response
{
    public class MONET_ADD_Response
    {
        public string action { get; set; }
        public string returnDate { get; set; }
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        //public Dictionary<string, object> data { get; set; }
        public object data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> dataObject
        {
            get
            {
                if (data == null) return null;
                string dataString = data.ToString();
                if (dataString == "[]") return null;
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataString);
                return result;
            }
        }

        public string invoiceNo
        {
            get
            {
                if (dataObject != null && dataObject.ContainsKey("invCode"))
                {
                    return dataObject["invCode"].ToString();
                }
                return null;
            }
        }
        public string oid
        {
            get
            {
                if (dataObject != null && dataObject.ContainsKey("oid"))
                {
                    return dataObject["oid"].ToString();
                }

                return null;
            }
        }

        public string invRef { get; set; }
        public string[] tokenInfo { get; set; }
        
    }
}
