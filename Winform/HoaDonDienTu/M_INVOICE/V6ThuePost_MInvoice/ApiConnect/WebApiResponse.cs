using System.Collections.Generic;

namespace V6ThuePostMInvoiceApi
{
    public class WebApiResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
