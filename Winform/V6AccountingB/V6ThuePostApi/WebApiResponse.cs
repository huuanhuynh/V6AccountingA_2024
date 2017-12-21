using System.Collections.Generic;

namespace V6ThuePostApi
{
    public class WebApiResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
