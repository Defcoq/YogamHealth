using System.Web.Mvc;

namespace MultilanguageMVC4.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "MultilanguageMVC4.Areas.Admin.Controllers" }
            );

            //LocalizedViewEngine: this is to support views also when named e.g. Index.de.cshtml
            context.MapRoute(
                "Admin_Default2",
                "{culture}/Admin/{controller}/{action}/{id}",
                new
                {
                    culture = "en",
                    controller = "Account",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            ).RouteHandler = new LocalizedMvcRouteHandler();
        }
    }
}
