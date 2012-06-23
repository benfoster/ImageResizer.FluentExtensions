
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// Useful <see cref="ImageUrlBuilder"/> Modifiers
    /// </summary>
    public static class ModifierExtensions
    {
        /// <summary>
        /// Converts the generated urls to lower case
        /// </summary>
        public static ImageUrlBuilder LowerCase(this ImageUrlBuilder builder)
        {
            return builder.AddModifier(s => s.ToLowerInvariant());
        }
    }
}
