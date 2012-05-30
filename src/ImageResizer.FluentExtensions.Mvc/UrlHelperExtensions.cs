using System;
using System.Web;
using System.Web.Mvc;

namespace ImageResizer.FluentExtensions.Mvc
{
    public static class UrlHelperExtensions
    {
        public static ImageUrl ImageUrl(this UrlHelper url, string imagePath, Action<ImageUrlBuilder> configure)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            if (configure == null)
                throw new ArgumentNullException("configure");

            var builder = new ImageUrlBuilder();
            configure(builder);

            return url.ImageUrl(imagePath, builder);
        }

        public static ImageUrl ImageUrl(this UrlHelper url, string imagePath, ImageUrlBuilder builder)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            if (builder == null)
                throw new ArgumentNullException("builder");

            if (VirtualPathUtility.IsAppRelative(imagePath))
                imagePath = VirtualPathUtility.ToAbsolute(imagePath);

            return builder.BuildUrl(imagePath);
        }
    }
}
