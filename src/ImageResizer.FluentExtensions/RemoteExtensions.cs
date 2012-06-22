using System;
using System.IO;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// <see cref="ImageUrlBuilder"/> extensions for the RemoteReader plugin.
    /// For more information see http://imageresizing.net/plugins/remotereader.
    /// </summary>
    public static class RemoteExtensions
    {
        /// <summary>
        /// Applies a url modifier for use with the remote reader plugin.
        /// This modifier requires that the urls you pass to the builder are absolute e.g. http://somedomain.com/image.jpg
        /// For more information see http://imageresizing.net/plugins/remotereader.
        /// </summary>
        /// <param name="urlBuilder">The <see cref="ImageUrlBuilder"/> instance to apply the modifier to.</param>
        /// <returns></returns>
        public static ImageUrlBuilder FromRemote(this ImageUrlBuilder builder)
        {             
            builder.AddModifier(s => CreateRemoteUrl(s));
            return builder;
        }

        private static string CreateRemoteUrl(string sourceImageUrl)
        {
            Uri uri;
            if (!Uri.TryCreate(sourceImageUrl, UriKind.Absolute, out uri))
                return sourceImageUrl;

            return Path.Combine(
                PathUtils.GetAppVirtualPath(), 
                "remote", 
                uri.Host, 
                uri.PathAndQuery.TrimStart('/')).Replace("\\", "/");
        }
    }
}
