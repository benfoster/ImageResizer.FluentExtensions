using System;
using System.IO;
using System.Web.Hosting;

namespace ImageResizer.FluentExtensions
{
    public static class PathUtils
    {
        /// <summary>
        /// Modifies the <paramref name="imagePath"/> with the specified <paramref name="prefix"/> and <paramref name="container"/> name.
        /// </summary>
        /// <param name="imagePath">The image path to modify</param>
        /// <param name="prefix">The prefix you would like to append to the path</param>
        /// <param name="container">The (optional) container (root directory) name</param>
        /// <exception cref="System.ArgumentNullException">If the <paramref name="imagePath"/> or <paramref name="prefix"/> are null or empty.</exception>
        public static string ModifyPath(string imagePath, string prefix, string container = null)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException("imagePath");

            prefix = Path.Combine(prefix, container ?? string.Empty);
            var fixedPath = Path.Combine(GetAppVirtualPath(), prefix, imagePath.TrimStart('/', '\\', '~'));

            return fixedPath.Replace('\\', '/');
        }

        public static string GetAppVirtualPath()
        {
            return HostingEnvironment.ApplicationVirtualPath ?? "/";
        }
    }
}
