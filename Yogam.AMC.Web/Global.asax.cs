using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Infrastructure.Conventions;
using Yogam.AMC.Infrastructure.Registries;
using Yogam.AMC.Infrastructure.Tasks;
using StructureMap;
using StructureMap.TypeRules;
using MvcGlobalisationSupport;

namespace Yogam.AMC.Web
{
    public class MvcApplication : HttpApplication
    {
        public IContainer Container
        {
            get
            {
                return (IContainer)HttpContext.Current.Items["_Container"];
            }
            set
            {
                HttpContext.Current.Items["_Container"] = value;
            }
        }
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
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Insert(0, new LocalizedViewEngine());
            RegisterRoutes(RouteTable.Routes);
            // RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));
            ObjectFactory.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new ActionFilterRegistry(() => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new MvcRegistry());
                cfg.AddRegistry(new TaskRegistry());
            });

            using (var container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }

        public void Application_BeginRequest()
        {
            Container = ObjectFactory.Container.GetNestedContainer();
            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in
                    Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }
    }
}
