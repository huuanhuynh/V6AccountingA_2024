using System.Web.Mvc;

namespace V6Soft.Web.Accounting.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }


    }
}