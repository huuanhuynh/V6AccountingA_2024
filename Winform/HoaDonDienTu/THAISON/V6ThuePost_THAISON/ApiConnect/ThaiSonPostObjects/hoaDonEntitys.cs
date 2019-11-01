using System.Collections.Generic;

namespace V6ThuePostThaiSonApi.ThaiSonPostObjects
{
    public class hoaDonEntitys// : JsonObject
    {
        public hoaDonEntitys()
        {
            _hoaDonEntities = new List<hoaDonEntity>();
        }

        /// <summary>
        /// &lt;Inv>...&lt;/Inv>[...]
        /// </summary>
        public List<hoaDonEntity> _hoaDonEntities;
    }
}
