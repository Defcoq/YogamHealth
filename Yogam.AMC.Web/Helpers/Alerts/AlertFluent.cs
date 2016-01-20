namespace Yogam.AMC.Web.Helpers.Alerts
{
    public class AlertFluent : IAlertFluent
    {
        private readonly Alert _parent;

        public AlertFluent(Alert parent)
        {
            _parent = parent;
        }

        public IAlertFluent Dismissible(bool canDismiss = true)
        {
            return _parent.Dismissible(canDismiss);
        }

        public string ToHtmlString()
        {
            return _parent.ToHtmlString();
        }
    }
}