
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// <see cref="ImageUrlBuilder"/> extensions for the AzureReader plugin.
    /// For more information see http://imageresizing.net/plugins/azurereader.
    /// </summary>
    public static class AzureExtensions
    {
        /// <summary>
        /// Applies a url modifier for use with the AzureReader plugin.
        /// For more information see http://imageresizing.net/plugins/azurereader.
        /// </summary>
        /// <param name="urlBuilder">The <see cref="ImageUrlBuilder"/> instance to apply the modifier to.</param>
        /// <param name="prefix">
        /// The virtual folder that all blobs can be accessed under. Defaults to "azure".
        /// This should match the prefix setting of the plugin defined in web.config.
        /// </param>
        /// <param name="container">
        /// An optional container name. If no name is specified the container is inferred from the image path's root directory.
        /// </param>
        public static ImageUrlBuilder FromAzure(this ImageUrlBuilder urlBuilder, string prefix = "azure", string container = null)
        {
            urlBuilder.AddModifier(s => PathUtils.ModifyPath(s, prefix, container));
            return urlBuilder;
        }
    }
}
