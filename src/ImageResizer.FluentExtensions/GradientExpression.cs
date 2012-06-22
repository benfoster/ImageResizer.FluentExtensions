using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring gradient plugin options. 
    /// For more information see http://imageresizing.net/plugins/gradient.
    /// </summary>   
    public class GradientExpression : ImageUrlBuilderExpression
    {
        public GradientExpression(ImageUrlBuilder builder) : base(builder) { }
        
        /// <summary>
        /// Sets the dimensions of the gradient PNG.
        /// </summary>
        /// <param name="width">The desired width in pixels.</param>
        /// <param name="height">The desired height in pixels.</param>
        public GradientExpression Dimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentException("Width must be greater than 0.");
            if (height <= 0)
                throw new ArgumentException("Height must be greater than 0.");

            builder.SetParameter(GradientCommands.Width, width.ToString());
            builder.SetParameter(GradientCommands.Height, height.ToString());
            return this;
        }

        /// <summary>
        /// Sets the colors of the gradient. can be a named color, or a hex color. 
        /// Accepts 6 and 8-digit hex values (last two digits of 8-digit hex values are for transparency).
        /// </summary>
        /// <param name="color1">The first color in the gradient.</param>
        /// <param name="color2">The second color in the gradient.</param>
        public GradientExpression Colors(string color1, string color2)
        {
            if (string.IsNullOrEmpty(color1))
                throw new ArgumentNullException("color1");

            if (string.IsNullOrEmpty(color2))
                throw new ArgumentNullException("color2");

            builder.SetParameter(GradientCommands.Color1, color1);
            builder.SetParameter(GradientCommands.Color2, color2);
            return this;
        }

        /// <summary>
        /// Sets the angle of the gradient.
        /// </summary>
        /// <param name="gradientAngle">The gradient angle in degrees.</param>
        public GradientExpression Angle(int gradientAngle)
        {
            if (!gradientAngle.IsBetween(0, 360))
                throw new ArgumentOutOfRangeException("The gradient angle must be between 0 and 360 degrees.");

            builder.SetParameter(GradientCommands.Angle, gradientAngle.ToString());
            return this;
        }

        /// <summary>
        /// Gradient plugin commands see http://imageresizing.net/plugins/gradient
        /// </summary>
        private static class GradientCommands
        {
            internal const string Width = "width";
            internal const string Height = "height";
            internal const string Color1 = "color1";
            internal const string Color2 = "color2";
            internal const string Angle = "angle";
        }
    }

    public static class GradientExtensions
    {
        /// <summary>
        /// Adds gradient plugin options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example creates a 200px x 10px gradient from red to black
        /// <code>
        /// builder.Gradient(img => img.Width(200).Height(10).Colors("red", "black"));
        /// </code>
        /// </example>
        public static ImageUrlBuilder Gradient(this ImageUrlBuilder builder, Action<GradientExpression> configure)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (configure == null)
                throw new ArgumentNullException("configure");

            configure(new GradientExpression(builder));
            return builder;
        }
    }
}
