using Newtonsoft.Json;

namespace ApiConnect
{
    public class OData<T>
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        public T Value { get; set; }
    }
}
