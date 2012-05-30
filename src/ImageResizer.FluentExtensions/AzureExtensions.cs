using System;
using System.IO;
using System.Web.Hosting;

namespace ImageResizer.FluentExtensions
{
    public static class AzureExtensions
    {
        public static ImageUrl FromAzure(this ImageUrl url, string prefix = "azure", string container = null)
        {
            url.AddModifier(s => FixPath(s, prefix, container));
            return url;
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
