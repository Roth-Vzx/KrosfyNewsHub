using System;
using System.Web.Mvc;

namespace KrosfyNewsHub
{
    public class ContextExtensions : Controller
    {
        //This function verify is the request is from a mobile device
        public bool isRequestFromMobile(string userAgent)
        {
            string[] mobileKeywords = { "Mobile", "Android", "iPhone", "Windows Phone", "BlackBerry" };

            foreach (string keyword in mobileKeywords)
            {
                if (userAgent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}