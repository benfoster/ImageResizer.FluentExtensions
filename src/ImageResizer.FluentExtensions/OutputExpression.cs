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
            builder.SetParameter(OutputParameters.Format, format.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Sets the Jpeg compression for the image
        /// </summary>
        /// <param name="compression">Jpeg compression: 0-100 100=best, 90=very good balance, 0=ugly</param>
        public OutputExpression Quality(int compression)
        {
            if (compression < 0 || compression > 100)
                throw new ArgumentException("The Jpeg compression must be between 0 and a 100");
            
            builder.SetParameter(OutputParameters.Quality, compression.ToString());
            return this;
        }

        private static class OutputParameters
        {
            internal const string Format = "format";
            internal const string Quality = "quality";
        }
    }

    public enum OutputFormat
    {
        Jpg,
        Png,
        Gif
    }
}
