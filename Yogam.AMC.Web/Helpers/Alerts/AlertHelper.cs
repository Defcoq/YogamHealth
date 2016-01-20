using System.Web.Mvc;

namespace Yogam.AMC.Web.Helpers.Alerts
{
public static class AlertHelper
{
    public static Alert Alert(this HtmlHelper html,string message)
    {
        return new Alert(message);
    }
}
}