using System.Web;
using System.Web.Mvc;
using Authentication;

namespace TestAngular
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           filters.Add(new WebAuthorizeAttribute());
        }
    }
}
