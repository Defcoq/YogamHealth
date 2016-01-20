using System;
using System.Web.Mvc;

namespace Yogam.AMC.Web.Helpers.Alerts
{
    public class Alert : IAlert
    {
        private Enums.AlertStyle _style;
        private bool _dismissible;
        private string _message;

        public Alert(string message)
        {
            _message = message;
        }

        public IAlertFluent Danger()
        {
            _style = Enums.AlertStyle.Danger;
            return new AlertFluent(this);
        }

        public IAlertFluent Info()
        {
            _style = Enums.AlertStyle.Info;
            return new AlertFluent(this);
        }

        public IAlertFluent Success()
        {
            _style = Enums.AlertStyle.Success;
            return new AlertFluent(this);
        }

        public IAlertFluent Warning()
        {
            _style = Enums.AlertStyle.Warning;
            return new AlertFluent(this);
        }

        public IAlertFluent Dismissible(bool canDismiss = true)
        {
            this._dismissible = canDismiss;
            return new AlertFluent(this);
        }

        public string ToHtmlString()
        {
            var alertDiv = new TagBuilder("div");
            alertDiv.AddCssClass("alert");
            alertDiv.AddCssClass("alert-" + _style.ToString().ToLower());
            alertDiv.InnerHtml = _message;

            if (_dismissible)
            {
                alertDiv.AddCssClass("alert-dismissable");
                alertDiv.InnerHtml += AddCloseButton();
            }

            return alertDiv.ToString();
        }

        private static TagBuilder AddCloseButton()
        {
            var closeButton = new TagBuilder("button");
            closeButton.AddCssClass("close");
            closeButton.Attributes.Add("data-dismiss", "alert");
            closeButton.InnerHtml = "&times;";
            return closeButton;
        }
    }
}