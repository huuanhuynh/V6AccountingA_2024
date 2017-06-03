using System.Web;
using System.Web.Mvc;

namespace V6Web_Mod_Controls
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
