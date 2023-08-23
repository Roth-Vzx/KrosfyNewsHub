using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace KrosfyNewsHub.Controllers
{
    public class LanguageController : Controller
    {
        private string lastUserLanguage = "";

        public ActionResult ChangeLanguage(string lang)
        {
            string referrerUrl = Request.UrlReferrer?.ToString();
            if (Session["lang"]?.ToString() != lang)
            {
                Session["lang"] = lang;
            }

            if (Session["lang"] == null)
            {
                Session["lang"] = "es";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["lang"].ToString());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["lang"].ToString());
            if (lastUserLanguage != lang)
            {
                lastUserLanguage = lang;
            }
            return Redirect(referrerUrl);
        }
    }
}