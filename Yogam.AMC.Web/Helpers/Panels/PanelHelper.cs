using System.Web.Mvc;
namespace Yogam.AMC.Web.Helpers.Panels
{
    public static class PanelHelper
    {
        public static Panel Panel(this HtmlHelper html, string title, Enums.PanelStyle style=Enums.PanelStyle.Default)
        {
            return new Panel(html, title, style);
        }
    }
}