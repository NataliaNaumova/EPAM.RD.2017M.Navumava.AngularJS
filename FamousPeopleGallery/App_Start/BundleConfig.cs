using System.Web;
using System.Web.Optimization;

namespace FamousPeopleGallery
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css",
                "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/Scripts/angular").Include("~/Scripts/angular.js",
                "~/Scripts/angular-route.js", 
                "~/Scripts/angular-cookies.js", 
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/module.js"));


        }
    }
}