using System;
using System.IO;
using System.Web.Hosting;

namespace ImageResizer.FluentExtensions
{
    public static class AzureExtensions
    {
        public static ImageUrlBuilder FromAzure(this ImageUrlBuilder urlBuilder, string prefix = "azure", string container = null)
        {
            urlBuilder.AddModifier(s => FixPath(s, prefix, container));
            return urlBuilder;
        }

        private static string FixPath(string imagePath, string prefix, string container)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException("imagePath");
           
            prefix = Path.Combine(prefix, container ?? string.Empty);
            var fixedPath = Path.Combine(GetAppVirtualPath(), prefix, imagePath.TrimStart('/', '\\', '~'));

            return fixedPath.Replace('\\', '/');
        }

        private static string GetAppVirtualPath()
        {
            return HostingEnvironment.ApplicationVirtualPath ?? "/";
        }
    }
}
