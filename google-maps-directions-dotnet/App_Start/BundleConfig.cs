using System.Web;
using System.Web.Optimization;

namespace google_maps_directions_dotnet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-resource.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/gdir").Include(
                      "~/Scripts/mainController.js",
                      "~/Scripts/customFilters.js"));
 



        }
    }
}
