
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// <see cref="ImageUrlBuilder"/> extensions for the AzureReader plugin.
    /// For more information see http://imageresizing.net/plugins/s3reader.
    /// </summary>
    public static class S3Extensions
    {
        /// <summary>
        /// Applies a url modifier for use with the S3Reader plugin.
        /// For more information see http://imageresizing.net/plugins/s3reader.
        /// </summary>
        /// <param name="urlBuilder">The <see cref="ImageUrlBuilder"/> instance to apply the modifier to.</param>
        /// <param name="prefix">
        /// The virtual folder that all buckets can be accessed under. Defaults to "s3"
        /// This should match the prefix setting of the plugin defined in web.config.
        /// </param>
        /// <param name="bucketName">
        /// An optional bucket name. If no name is specified the container is inferred from the image path's root directory.
        /// </param>
        public static ImageUrlBuilder FromS3(this ImageUrlBuilder urlBuilder, string prefix = "s3", string bucketName = null)
        {
            urlBuilder.AddModifier(s => PathUtils.ModifyPath(s, prefix, bucketName));
            return urlBuilder;
        }
    }
}
