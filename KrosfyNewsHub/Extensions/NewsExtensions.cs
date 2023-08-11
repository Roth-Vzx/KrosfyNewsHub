using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrosfyNewsHub.Extensions
{
    public static class NewsExtensions
    {
        public static bool isRequestFromMobile(string userAgent)
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

        public static string GetSubCategoryName(int subcategoryID)
        {
            string SubCategoryName = "";

            switch (subcategoryID)
            {
                case 1: SubCategoryName = "Bitcoin"; break;
                case 2: SubCategoryName = "Ethereum"; break;
                case 3: SubCategoryName = "Tether"; break;
                case 4: SubCategoryName = "Cardano"; break;
                case 5: SubCategoryName = "Inteligencia Artificial"; break;
                case 6: SubCategoryName = "Gaming"; break;
                default:
                    break;
            }
            return SubCategoryName;

        }

        public static string GetCategoryName(int categoryID)
        {
            string CategoryName = "";

            switch (categoryID)
            {
                case 1: CategoryName = Resources.Resources.Menu_Crypto; break;
                case 2: CategoryName = Resources.Resources.Menu_Technology; break;

                default:
                    break;
            }
            return CategoryName;

        }
    }
}