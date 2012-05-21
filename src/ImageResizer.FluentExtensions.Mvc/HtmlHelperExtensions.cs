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

        public static MvcHtmlString BuildImage(this HtmlHelper html, string src, Action<ImageBuilder> configure, string alternateText = "", object htmlAttributes = null)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var builder = new ImageBuilder();
            configure(builder);

            return html.BuildImage(src, builder, alternateText, htmlAttributes);
        }


        public static MvcHtmlString BuildImage(this HtmlHelper html, string src, ImageBuilder builder, string alternateText = "", object htmlAttributes = null)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (string.IsNullOrEmpty(src))
                throw new ArgumentNullException("src");

            return html.Image(builder.Build(src), alternateText, htmlAttributes);
        }
    }
}
