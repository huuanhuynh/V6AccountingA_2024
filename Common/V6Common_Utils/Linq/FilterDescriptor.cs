using System.Collections.Generic;

namespace V6Soft.Common.Utils.Linq
{
    public class FilterDescriptor
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public string Logic { get; set; }
        public IList<FilterDescriptor> Filters { get; set; }
    }
}
