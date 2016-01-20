using MvcGlobalisationSupport;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yogam.AMC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            const string defautlRouteUrl = "{controller}/{action}/{id}";

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteValueDictionary defaultRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.Add("DefaultGlobalised", new GlobalisedRoute(defautlRouteUrl, defaultRouteValueDictionary));
            routes.Add("Default", new Route(defautlRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler()));

            //LocalizedViewEngine: this is to support views also when named e.g. Index.de.cshtml
            routes.MapRoute(
                "Default2",
                "{culture}/{controller}/{action}/{id}",
                new
                {
                    culture = "en",
                    controller = "Home",//ControllerName
                    action = "Index",//ActionName
                    id = UrlParameter.Optional
                }
            ).RouteHandler = new LocalizedMvcRouteHandler();


            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
