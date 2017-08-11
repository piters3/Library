using System.Web.Mvc;
using System.Web.Routing;

namespace Library {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Catalog",
            //    url: "{controller}/{action}/{category}",
            //    defaults: new { controller = "Catalog", action = "Index", category = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}