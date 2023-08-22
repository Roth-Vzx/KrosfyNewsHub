using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KrosfyNewsHub
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string acceptLanguageHeader = Request.Headers["Accept-Language"];
            string preferredLanguage = acceptLanguageHeader?.Split(',').FirstOrDefault();
            if (!string.IsNullOrEmpty(preferredLanguage) && preferredLanguage.Contains("-"))
            {
                preferredLanguage = preferredLanguage.Split('-')[0].ToLower();
            }

            preferredLanguage =  (preferredLanguage != "it" && preferredLanguage != "es") ? "en" : preferredLanguage;

            HttpContext context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                preferredLanguage = context.Session["lang"] != null ? context.Session["lang"].ToString() : preferredLanguage;
            }
            var cultureInfo = new CultureInfo(preferredLanguage);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
