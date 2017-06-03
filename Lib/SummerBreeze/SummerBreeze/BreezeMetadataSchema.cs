using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerBreeze
{
    public class BreezeMetadataSchema
    {
        public string @namespace { get; set; }

        public string xmlns { get; set; }
        
        public IList<BreezeEntityTypeMetadata> entityType { get; set; }
    }
}
