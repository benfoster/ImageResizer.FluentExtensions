using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring simple filters options. 
    /// For more information see http://imageresizing.net/plugins/simplefilters
    /// </summary>    
    public class SimpleFiltersExpression : ImageUrlBuilderExpression
    {
        public SimpleFiltersExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Applies a greyscale filter - equivalent to greyscale = true|y|ntsc
        /// </summary>
        public SimpleFiltersExpression Grayscale()
        {
            builder.SetParameter(SimpleFiltersParameters.Grayscale, true.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Applies a greyscale filter. For default/NTSC filter use <see cref="Greyscale()"/>
        /// </summary>
        /// <param name="grayScaleOptions">The type of greyscale to apply</param>
        public SimpleFiltersExpression Grayscale(GrayscaleOptions grayScaleOptions)
        {
            builder.SetParameter(SimpleFiltersParameters.Grayscale, grayScaleOptions.ToString().ToLowerInvariant());
            return this;
        }
               
        /// <summary>
        /// Applies a sepia filter
        /// </summary>
        public SimpleFiltersExpression Sepia()
        {
            builder.SetParameter(SimpleFiltersParameters.Sepia, "true");
            return this;
        }

        /// <summary>
        /// Adjusts the alpha (transparancy) of an image. 
        /// For true transparency compine with <see cref="ImageUrlBuilder.Output(img => img.Format(OutputFormat.Png)"/>
        /// </summary>
        /// <param name="adjustment">A value between 0 (transparent) and 1 (non transparent)</param>
        public SimpleFiltersExpression Alpha(double adjustment)
        {
            if (!adjustment.IsBetweenOrEqual(0, 1))
                throw new ArgumentException("Alpha adjustment must be between 0 and 1");

            builder.SetParameter(SimpleFiltersParameters.Alpha, adjustment.ToString());
            return this;
        }

        /// <summary>
        /// Adjust the brightness of an image
        /// </summary>
        /// <param name="adjustment">A value between -1 (darker) and 1 (lighter)</param>
        /// <example>Set <paramref name="adjustment"/> to 0.5 to increase brightness by 50%</example>
        public SimpleFiltersExpression Brightness(double adjustment)
        {
            if (!adjustment.IsBetweenOrEqual(-1, 1))
                throw new ArgumentException("Brightness must be between -1 and 1");

            builder.SetParameter(SimpleFiltersParameters.Brightness, adjustment.ToString());
            return this;
        }

        /// <summary>
        /// Adjusts the contrast of an image
        /// </summary>
        /// <param name="adjustment">A value between -1 (decrease contrast) and 1 (increase contrast)</param>
        public SimpleFiltersExpression Contrast(double adjustment)
        {
            if (!adjustment.IsBetweenOrEqual(-1, 1))
                throw new ArgumentException("Contrast must be between -1 and 1");

            builder.SetParameter(SimpleFiltersParameters.Contrast, adjustment.ToString());
            return this;
        }

        /// <summary>
        /// Adjusts the saturation of an image
        /// </summary>
        /// <param name="adjustment">A value between -1 (decrease saturation) and 1 (increase saturation)</param>
        public SimpleFiltersExpression Saturate(double adjustment)
        {
            if (!adjustment.IsBetweenOrEqual(-1, 1))
                throw new ArgumentException("Contrast must be between -1 and 1");

            builder.SetParameter(SimpleFiltersParameters.Saturation, adjustment.ToString());
            return this;
        }

        /// <summary>
        /// Inverts an image
        /// </summary>
        public SimpleFiltersExpression Invert()
        {
            builder.SetParameter(SimpleFiltersParameters.Invert, true.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Adds evenly rounded corners to an image
        /// </summary>
        /// <param name="radiusPercentage">A percentage between 0 and 100 of 1/2 the smaller of width and height</param>
        public SimpleFiltersExpression RoundedCorners(int radiusPercentage)
        {
            if (!radiusPercentage.IsValidPercentage())
                throw new ArgumentException("Radius Percentage must be between 0 and 100");

            builder.SetParameter(SimpleFiltersParameters.RoundCorners, radiusPercentage.ToString());
            return this;
        }

        /// <summary>
        /// Adds rounded corners to an image. Radius is a percentage between 0 and 100 of 1/2 the smaller of width and height
        /// </summary>
        /// <param name="topLeft">Top left percentage</param>
        /// <param name="topRight">Top right percentage</param>
        /// <param name="bottomRight">Bottom right percentage</param>
        /// <param name="bottomLeft">Bottom left percentage</param>
        public SimpleFiltersExpression RoundedCorners(int topLeft, int topRight, int bottomRight, int bottomLeft)
        {
            int[] percentages = new[] { topLeft, topRight, bottomRight, bottomLeft };

            foreach (var percentage in percentages)
                if (!percentage.IsValidPercentage()) { throw new ArgumentException("Radius percentages must be between 0 and 100"); }

            builder.SetParameter(SimpleFiltersParameters.RoundCorners, string.Join(",", percentages));
            return this;
        }

        /// <summary>
        /// SimpleFilters commands see http://imageresizing.net/plugins/simplefilters
        /// </summary>
        private static class SimpleFiltersParameters
        {
            internal const string Grayscale = "s.grayscale";
            internal const string Sepia = "s.sepia";
            internal const string Alpha = "s.alpha";
            internal const string Brightness = "s.brightness";
            internal const string Contrast = "s.contrast";
            internal const string Saturation = "s.saturation";
            internal const string Invert = "s.invert";
            internal const string RoundCorners = "s.roundcorners";
        }
    }

    /// <summary>
    /// SimpleFilters plugin Greyscale options
    /// </summary>
    public enum GrayscaleOptions
    {
        RY,
        BT709,
        Flat
    }

    public static class SimpleFiltersExtensions
    {
        /// <summary>
        /// Adds SimpleFilters plugin options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example adds 5px padding to the image and applies a 2px black border
        /// <code>
        /// builder.Style(img => img.PaddingWidth(5).BorderColor("000000").BorderWidth(2))
        /// </code>
        /// </example>
        public static ImageUrlBuilder SimpleFilters(this ImageUrlBuilder builder, Action<SimpleFiltersExpression> configure)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (configure == null)
                throw new ArgumentNullException("configure");

            configure(new SimpleFiltersExpression(builder));
            return builder;
        }
    }
}
