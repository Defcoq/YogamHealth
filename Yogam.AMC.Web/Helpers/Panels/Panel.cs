using System;
using System.IO;
using System.Web.Mvc;


namespace Yogam.AMC.Web.Helpers.Panels
{
    public class Panel : IDisposable
    {
        private readonly TextWriter _writer;

        public Panel(HtmlHelper helper, string title, Enums.PanelStyle style = Enums.PanelStyle.Default)
        {
            _writer = helper.ViewContext.Writer;

            var panelDiv = new TagBuilder("div");
            panelDiv.AddCssClass("panel-" + style.ToString().ToLower());
            panelDiv.AddCssClass("panel");

            var panelHeadingDiv = new TagBuilder("div");
            panelHeadingDiv.AddCssClass("panel-heading");

            var heading3Div = new TagBuilder("h3");
            heading3Div.AddCssClass("panel-title");
            heading3Div.SetInnerText(title);

            var panelBodyDiv = new TagBuilder("div");
            panelBodyDiv.AddCssClass("panel-body");

            panelHeadingDiv.InnerHtml = heading3Div.ToString();

            string html = string.Format("{0}{1}{2}", panelDiv.ToString(TagRenderMode.StartTag), panelHeadingDiv, panelBodyDiv.ToString(TagRenderMode.StartTag));
            _writer.Write(html);
        }

        public void Dispose()
        {
            _writer.Write("</div></div>");
        }
    }
}