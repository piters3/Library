﻿using System.Web.Optimization;

namespace Library {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/User/bootstrap.js",
                      "~/Scripts/User/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/User/bootstrap.css",
                      "~/Content/User/site.css",
                      "~/Content/Admin/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                      "~/Content/Admin/main.css",
                      "~/Content/Admin/font-awesome.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/admin/jquery").Include(
                        "~/Scripts/Admin/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/bootstrap").Include(
                        "~/Scripts/Admin/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/main").Include(
                        "~/Scripts/Admin/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/pace").Include(
                        "~/Scripts/Admin/plugins/pace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/dataTables").Include(
                        "~/Scripts/Admin/plugins/jquery.dataTables.min.js",
                        "~/Scripts/Admin/plugins/dataTables.bootstrap.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR-2.2.2.min.js"
                        ));

        }
    }
}
