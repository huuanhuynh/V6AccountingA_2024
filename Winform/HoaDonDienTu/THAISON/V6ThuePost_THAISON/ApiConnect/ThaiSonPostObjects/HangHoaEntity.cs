using System.Collections.Generic;

namespace V6ThuePostThaiSonApi.ThaiSonPostObjects
{
    public class HangHoaEntity
    {
        public HangHoaEntity()
        {
            Details = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Details;
    }
}
