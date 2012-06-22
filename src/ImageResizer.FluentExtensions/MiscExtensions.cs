using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// Various plugin extensions that don't really belong in their own expressions
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// The name of one or more watermark layers (or layer groups) to render.
        /// For more information see http://imageresizing.net/plugins/watermark
        /// </summary>
        public static ImageUrlBuilder Watermark(this ImageUrlBuilder builder, string watermarkNames)
        {
            if (string.IsNullOrEmpty(watermarkNames))
                throw new ArgumentNullException("watermarkNames");
            
            return builder.SetParameter(MiscCommands.Watermark, watermarkNames);
        }

        /// <summary>
        /// The path to the fallback image, or a named preset if the image is not found.
        /// For more information see http://imageresizing.net/docs/reference
        /// </summary>
        public static ImageUrlBuilder Image404(this ImageUrlBuilder builder, string fallbackImagePathOrPresetName)
        {
            if (string.IsNullOrEmpty(fallbackImagePathOrPresetName))
                throw new ArgumentNullException("fallbackImagePathOrPresetName");

            return builder.SetParameter(MiscCommands.Image404, fallbackImagePathOrPresetName);
        }

        /// <summary>
        /// Allows you to define sets of settings in Web.config and reference them by name.
        /// </summary>
        /// <param name="presets">A list of preset settings groups to apply. <example>preset1,preset2,preset3</example></param>
        public static ImageUrlBuilder ApplyPresets(this ImageUrlBuilder builder, string presets)
        {
            if (string.IsNullOrEmpty(presets))
                throw new ArgumentNullException("presets");

            return builder.SetParameter(MiscCommands.Preset, presets);
        }

        /// <summary>
        /// Ignores the ICC profile embedded in the source image
        /// </summary>
        public static ImageUrlBuilder IgnoreICC(this ImageUrlBuilder builder)
        {
            return builder.SetParameter(MiscCommands.IgnoreICC, true.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Always forces the image to be cached even if it wasn't modified by the resizing module. 
        /// No disables caching even if it was.
        /// </summary>
        public static ImageUrlBuilder Cache(this ImageUrlBuilder builder, CacheOptions cacheOption = CacheOptions.Default)
        {
            return builder.SetParameter(MiscCommands.Cache, cacheOption.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Always forces the image to be re-encoded even if it wasn't modified. 
        /// No prevents the image from being modified.
        /// </summary>
        public static ImageUrlBuilder Process(this ImageUrlBuilder builder, ProcessOptions processOption = ProcessOptions.Default)
        {
            return builder.SetParameter(MiscCommands.Process, processOption.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// The DPI at which the image should be printed. Ignored by all browsers, most operating systems, and most printers.
        /// </summary>
        public static ImageUrlBuilder DPI(this ImageUrlBuilder builder, DPIOptions dpiOption)
        {
            return builder.SetParameter(MiscCommands.DPI, ((int)dpiOption).ToString());
        }

        /// <summary>
        /// Miscellaneous commands - see http://imageresizing.net/docs/reference
        /// </summary>
        private static class MiscCommands
        {
            internal const string Watermark = "watermark";
            internal const string Image404 = "404";
            internal const string Preset = "preset";
            internal const string IgnoreICC = "ignoreicc";
            internal const string Cache = "cache";
            internal const string Process = "process";
            internal const string DPI = "dpi";
        }
    }

    public enum CacheOptions 
    {
        Default,
        Always,
        No
    }

    public enum ProcessOptions
    {
        Default,
        Always,
        No
    }

    public enum DPIOptions
    {
        DPI300 = 300,
        DPI600 = 600,
        DPI90 = 90,
    }
}
