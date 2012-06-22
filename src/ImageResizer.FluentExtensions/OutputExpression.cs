using System;
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring image putput options
    /// </summary>
    public class OutputExpression : ImageUrlBuilderExpression
    {
        internal OutputExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Sets the output format to use. By default, the original format (or the closest match) is used.
        /// </summary>
        /// <param name="format">The desired output format</param>
        public OutputExpression Format(OutputFormat format)
        {
            builder.SetParameter(OutputCommands.Format, format.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Sets the Jpeg compression for the image
        /// </summary>
        /// <param name="compression">Jpeg compression: 0-100 100=best, 90=very good balance, 0=ugly</param>
        public OutputExpression Quality(int compression)
        {
            if (!compression.IsBetween(0, 100))
                throw new ArgumentOutOfRangeException("The Jpeg compression must be between 0 and a 100");
            
            builder.SetParameter(OutputCommands.Quality, compression.ToString());
            return this;
        }

        /// <summary>
        /// Control the palette size of PNG and GIF images. If omitted, PNGs will be 24-bit. (PrettyGifs plugin required)
        /// </summary>
        /// <param name="numberOfColors">A number between 2 and 255</param>
        public OutputExpression Colors(int numberOfColors)
        {
            if (!numberOfColors.IsBetween(2, 255))
                throw new ArgumentOutOfRangeException("The number of colors must be between 2 and 255");

            builder.SetParameter(OutputCommands.Colors, numberOfColors.ToString());
            return this;
        }

        /// <summary>
        /// Gain a 15-30% speed boost by sacrificing rendering quality.
        /// For more information see http://imageresizing.net/plugins/speedorquality.
        /// </summary>
        /// <param name="quality">A value between 0 (highest quality) and 3 (lowest quality, highest speed)</param>
        /// <returns></returns>
        public OutputExpression SpeedOrQuality(int quality)
        {
            if (quality.IsNotBetween(0, 3))
                throw new ArgumentOutOfRangeException("Quality value must be between 0 and 3");

            builder.SetParameter(OutputCommands.SpeedOrQuality, quality.ToString());
            return this;
        }

        /// <summary>
        /// Output commands see http://imageresizing.net/docs/reference
        /// </summary>
        private static class OutputCommands
        {
            internal const string Format = "format";
            internal const string Quality = "quality";
            internal const string Colors = "colors";
            internal const string SpeedOrQuality = "speed";
        }
    }

    /// <summary>
    /// Output format options
    /// </summary>
    public enum OutputFormat
    {
        Jpg,
        Png,
        Gif
    }

    public static class OutputExtensions
    {
        /// <summary>
        /// Adds Output options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example sets the output quality to 80
        /// <code>
        /// builder.Output(img => img.Quality(90))
        /// </code>
        /// </example>
        public static ImageUrlBuilder Output(this ImageUrlBuilder builder, Action<OutputExpression> configure)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");
            
            if (configure == null)
                throw new ArgumentNullException("configure");
            
            configure(new OutputExpression(builder));
            return builder;
        }
    }
}
