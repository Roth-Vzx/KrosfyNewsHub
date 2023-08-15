using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(KrosfyNewsHub.App_Start.IdentityConfig))]

namespace KrosfyNewsHub.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("COOKIES")) ? "KrosfyNews" : ConfigurationManager.AppSettings.Get("COOKIES"),
                LoginPath = new PathString("/Login/Logout"),
            });
        }
    }
}