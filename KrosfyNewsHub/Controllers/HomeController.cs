using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrosfyNewsHub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResult()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SinglePost()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category()
        {
            ViewBag.Message = "Category View";

            return View();
        }

    }
}