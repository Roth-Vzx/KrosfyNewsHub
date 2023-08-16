using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KrosfyNewsHub.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            try
            {
                TempData.Clear();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
                {
                    TempData.Add("ERROR", "Nombre de usuario o contrasena vacios");
                    return RedirectToAction("Index", "Login");
                }

                if (username == "robi" && password == "1234567890")//consulta a la DB
                {
                    var identity = new ClaimsIdentity(new[] { 
                        new Claim(ClaimTypes.Role, "ADMIN"),
                        new Claim("CODE", "A0001"),
                        new Claim("NAME", "Robi Rondon")
                    }, DefaultAuthenticationTypes.ApplicationCookie);
                    string strsessioncookie = ConfigurationManager.AppSettings.Get("SESSION_COOKIES");
                    int session_cookie = int.Parse(!string.IsNullOrEmpty(strsessioncookie) ? strsessioncookie : "60" );
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new Microsoft.Owin.Security.AuthenticationProperties 
                        { 
                            AllowRefresh = true,
                            IsPersistent = false,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(session_cookie),
                        }, identity);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData.Add("ERROR", "Nombre de usuario o contrasena invalidos");
                    return RedirectToAction("Index", "Login");
                }
            }
            catch(Exception ex) 
            {
                TempData.Clear();
                TempData.Add("ERROR", ex.Message);
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Logout()
        {
            if(HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index","Login");
        }
    }
}