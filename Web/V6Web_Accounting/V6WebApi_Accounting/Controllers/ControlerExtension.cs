using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using V6SqlConnect;
using V6Tools.V6Convert;

namespace V6Soft.WebApi.Accounting.Controllers
{
    public static class ControlerExtension
    {
        public static string GetUserName(this ApiController api)
        {
            return System.Security.Claims.ClaimsPrincipal.Current.Claims.First().Value;
        }

        public static DataRow GetUserInfo(this ApiController api)
        {
            return SqlConnect.SelectV6User(api.GetUserName());
        }

        public static int GetUserId(this ApiController api)
        {
            return ObjectAndString.ObjectToInt(api.GetUserInfo()["user_id"]);
        }
    }
}