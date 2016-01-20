using System.Web;

namespace Yogam.AMC.Web.Helpers.Alerts
{
    public interface IAlertFluent : IHtmlString
    {
        IAlertFluent Dismissible(bool canDismiss = true);
    }
}