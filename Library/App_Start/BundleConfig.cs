using System.Web.Optimization;

namespace IdentitySample {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                      "~/Content/Admin/css/templatemo_main.css",
                      "~/Content/Admin/css/font-awesome.css"
                      //"~/Content/bootstrap.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/admin/jquery").Include(
                        "~/Scripts/Admin/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/bootstrap").Include(
                        "~/Scripts/Admin/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/templatemo_script").Include(
                        "~/Scripts/Admin/templatemo_script.js"));
        }
    }
}
