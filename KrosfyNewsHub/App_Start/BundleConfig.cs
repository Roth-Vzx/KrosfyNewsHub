using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace KrosfyNewsHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate*"));

            //Template add

            bundles.Add(new Bundle("~/bundles/jsTemplate").Include(
                        "~/Content/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Content/assets/vendor/swiper/swiper-bundle.min.js",
                        "~/Content/assets/vendor/glightbox/js/glightbox.min.js",
                        "~/Content/assets/vendor/aos/aos.js",
                        "~/Content/assets/vendor/php-email-form/validate.js",
                        "~/Content/assets/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/vendor/bootstrap-icons/bootstrap-icons.css",
                      "~/Content/assets/vendor/swiper/swiper-bundle.min.css",
                      "~/Content/assets/vendor/glightbox/css/glightbox.min.css",
                      "~/assets/vendor/aos/aos.css",
                      "~/Content/assets/css/variables.css",
                      "~/Content/assets/css/main.css"));




            //    // Use the development version of Modernizr to develop with and learn from. Then, when you're
            //    // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.add(new scriptbundle("~/bundles/modernizr").include(
            //            "~/scripts/modernizr-*"));

            //bundles.add(new bundle("~/bundles/bootstrap").include(
            //          "~/scripts/bootstrap.js"));

            //bundles.add(new stylebundle("~/content/css").include(
            //          "~/content/bootstrap.css",
            //          "~/content/site.css"));

        }
    }
}
