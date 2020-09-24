using System.Collections.Generic;

namespace V6ThuePostViettelApi
{
    public class WebApiResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
