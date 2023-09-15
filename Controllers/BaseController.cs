using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.Security.Policy;

namespace ETicaret.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Session.SetString("lang",HttpContext.Session.GetString("lang") ?? "tr-TR");
            HttpContext.Session.SetString("symbol", HttpContext.Session.GetString("symbol") ?? "$");
            HttpContext.Session.SetString("multiplier", HttpContext.Session.GetString("multiplier") ?? "1");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(HttpContext.Session.GetString("lang"));
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(HttpContext.Session.GetString("lang"));
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(HttpContext.Session.GetString("lang"));
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(HttpContext.Session.GetString("lang"));
        }
    }
}
