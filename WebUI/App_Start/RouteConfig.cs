using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ///<summary>
            ///This route is specially located higher then default
            ///</summary>
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Clothes", action = "List"}
                ); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Clothes", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
