using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KopLibrary.TagHelpers
{
    [HtmlTargetElement("language")]
    public class LanguageChooserTagHelper : TagHelper
    {
        [HtmlAttributeName("code")]
        public string Code { get; set; }

        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pageController = ViewContext.RouteData.Values["controller"];
            var pageAction = ViewContext.RouteData.Values["action"];
            var language = ViewContext.RouteData.Values["lang"];
            var id = ViewContext.RouteData.Values["id"];

            output.SuppressOutput();
            output.Content.Clear();

            var content = new StringBuilder();

            content.AppendLine($"<li class=\"{(Code == language.ToString() ? "active" : "")}\"><a href=\"/{Code}/{pageController}/{pageAction}/{id}\" title=\"\">{Text}</a></li>");

            output.Content.AppendHtml(content.ToString());
        }

    }
}
