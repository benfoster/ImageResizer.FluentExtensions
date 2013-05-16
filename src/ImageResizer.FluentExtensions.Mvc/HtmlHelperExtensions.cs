using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageResizer.FluentExtensions.Mvc
{
    public static class HtmlHelperExtensions
    {        
        public static MvcHtmlString Image(this HtmlHelper html, string src, string alternateText = "", object htmlAttributes = null)
        {
            if (string.IsNullOrEmpty(src))
                throw new ArgumentException("src");
            
            var img = new TagBuilder("img");

            if (src.StartsWith("~/"))
                src = VirtualPathUtility.ToAbsolute(src);

            img.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", alternateText);

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString BuildImage(this HtmlHelper html, string src, Action<ImageUrlBuilder> configure, string alternateText = "", object htmlAttributes = null)
        {
            var imageUrl = html.CreateUrlHelper().ImageUrl(src, configure);
            return html.Image(imageUrl, alternateText, htmlAttributes);
        }

        public static MvcHtmlString BuildImage(this HtmlHelper html, string src, ImageUrlBuilder builder, string alternateText = "", object htmlAttributes = null)
        {
            var imageUrl = html.CreateUrlHelper().ImageUrl(src, builder);
            return html.Image(imageUrl, alternateText, htmlAttributes);
        }

        private static UrlHelper CreateUrlHelper(this HtmlHelper html)
        {
            return new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);
        }
    }
}
