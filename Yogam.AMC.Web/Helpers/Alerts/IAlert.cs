
namespace Yogam.AMC.Web.Helpers.Alerts
{
    public interface IAlert : IAlertFluent
    {
        IAlertFluent Danger();
        IAlertFluent Info();
        IAlertFluent Success();
        IAlertFluent Warning();
    }
}