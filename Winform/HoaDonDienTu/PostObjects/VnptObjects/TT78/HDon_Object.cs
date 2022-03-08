using System.Collections.Generic;

namespace V6ThuePost.VnptObjects
{
    public class HDon_Object
    {
        /// <summary>
        /// &lt;Inv>.......&lt;/Inv>
        /// </summary>
        public HDon_Object()
        {
            DLHDon = new DLHDon_Object();
            //Invoice["Ikey"] = "";
        }

        public string key { get; set; }

        /// <summary>
        /// &lt;Invoice>  &lt;key>value&lt;/key>[key2...]  &lt;/Invoice>
        /// </summary>
        public DLHDon_Object DLHDon { get; set; }

        //public AdjustInv ToAdjustInv()
        //{
        //    AdjustInv result = new AdjustInv();
        //    result.key = key;
        //    result.Invoice = new Dictionary<string, object>(DLHDon);
        //    return result;
        //}

        //public ReplaceInv ToReplaceInv()
        //{
        //    ReplaceInv result = new ReplaceInv();
        //    //result.key = key;
        //    result.Invoice = new Dictionary<string, object>(DLHDon);
        //    result.Invoice["key"] = key;
        //    return result;
        //}
    }
}
