﻿using System.Collections.Generic;
using V6Tools.V6Objects;

namespace V6ThuePost.VnptObjects
{
    public class Invoices : V6JsonObject
    {
        public Invoices()
        {
            Inv = new List<Inv>();
        }

        /// <summary>
        /// &lt;Inv>...&lt;/Inv>[...]
        /// </summary>
        public List<Inv> Inv;
    }
}