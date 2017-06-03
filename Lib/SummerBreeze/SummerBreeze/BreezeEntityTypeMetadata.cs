﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerBreeze
{
    public class BreezeEntityTypeMetadata
    {
        public string name { get; set; }
        public string @namespace { get; set; }

        private string _autoGeneratedKeyType = string.Empty;
        public string autoGeneratedKeyType { get { return _autoGeneratedKeyType; } set { _autoGeneratedKeyType = value; } }

        public List<BreezeDataPropertyMetadata> property { get; set; }
        public List<BreezeNavigationPropertyMetadata> navigationProperties { get; set; }

        public BreezeDataPropertyKey key { get; set; }

        public BreezeEntityTypeMetadata()
        {
            key = new BreezeDataPropertyKey();
        }
    
    }

    
}
