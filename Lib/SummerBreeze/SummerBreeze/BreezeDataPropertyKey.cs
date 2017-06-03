using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerBreeze
{
    public class BreezeDataPropertyKey
    {
        public IList<object> propertyRef { get; set; }

        public BreezeDataPropertyKey()
        {
            propertyRef = new List<object>();
        }
    }
}
