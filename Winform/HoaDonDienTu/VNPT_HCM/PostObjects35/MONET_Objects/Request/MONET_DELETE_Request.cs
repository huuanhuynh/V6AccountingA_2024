using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V6Tools.V6Objects;

namespace V6ThuePost.MONET_Objects.Request
{
    public class MONET_DELETE_Request : V6JsonObject
    {
        public string token;
        public string invcCode;
        public string invcSign;
        public string invcSample;
        public string description;
        public string returnDocNo;
    }
}
