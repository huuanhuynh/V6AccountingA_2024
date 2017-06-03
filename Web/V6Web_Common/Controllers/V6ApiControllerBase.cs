using System;
using System.Web.Http;
using Newtonsoft.Json;
using V6Soft.Common.Utils;


namespace V6Soft.Web.Common.Controllers
{
    public abstract class V6ApiControllerBase : ApiController
    {
        protected static SequentialGuid s_UID = new SequentialGuid();

        protected Guid NextUID
        {
            get
            {
                s_UID++;
                return s_UID.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string ToSafeJson(object target)
        {
            return JsonConvert.SerializeObject(new { content = target });
        }     
    }
}
