using System.Web.Mvc;

namespace Yogam.AMC.Infrastructure.Alerts
{
    // As implemented by Matt Honeycutt's FailTracker project: https://github.com/MattHoneycutt/Fail-Tracker
    public class AlertDecoratorResult : ActionResult
    {
        public ActionResult InnerResult { get; set; }
        public string AlertClass { get; set; }
        public string Message { get; set; }

        public AlertDecoratorResult(ActionResult innerResult, string alertClass, string message)
        {
            InnerResult = innerResult;
            AlertClass = alertClass;
            Message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetAlerts();
            alerts.Add(new Alert(AlertClass,Message));
            InnerResult.ExecuteResult(context);
        }
    }
}
