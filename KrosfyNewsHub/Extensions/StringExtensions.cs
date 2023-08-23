using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace KrosfyNewsHub.Extensions
{
    static class StringExtensions
    {
        public static string q(this string aString)
        {
            return aString.Replace("'", "''");
        }

        public static string qq(this string aString)
        {
            return "'" + aString.Replace("'", "''") + "' ";
        }

        public static string qv(this string aString)
        {
            return aString.Replace("'", "''");
        }

        public static string vp(this string aString)
        {
            return aString.Replace(",", ".");
        }
    }
}