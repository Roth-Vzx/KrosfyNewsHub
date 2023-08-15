using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrosfyNewsHub.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (!HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Login");
            return View();
        }
    }
}