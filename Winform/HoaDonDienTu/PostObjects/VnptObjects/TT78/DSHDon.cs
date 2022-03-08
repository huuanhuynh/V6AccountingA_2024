using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.VnptObjects
{
    public class DSHDon : V6JsonObject
    {
        public DSHDon()
        {
            HDon = new List<HDon_Object>();
        }

        /// <summary>
        /// &lt;Inv>...&lt;/Inv>[...]
        /// </summary>
        public List<HDon_Object> HDon;
    }
}
