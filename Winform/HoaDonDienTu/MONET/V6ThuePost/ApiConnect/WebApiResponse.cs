using System.Collections.Generic;

namespace V6ThuePostMonetApi
{
    public class WebApiResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
