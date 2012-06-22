
using System;
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring image style options
    /// </summary>
    public class StyleExpression : ImageUrlBuilderExpression
    {
        internal StyleExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Sets the background/whitespace color.
        /// </summary>
        /// <param name="backgroundColor">Either a color name or 6 character hex code e.g. FFFFFF</param>
        public StyleExpression BackgroundColor(string backgroundColor)
        {
            if (string.IsNullOrEmpty(backgroundColor))
                throw new ArgumentNullException("backgroundColor");
            
            builder.SetParameter(StyleCommands.BackgroundColor, backgroundColor);
            return this;
        }

        /// <summary>
        /// Sets the padding width
        /// </summary>
        /// <param name="paddingWidth">The desired width in pixels</param>
        public StyleExpression PaddingWidth(int paddingWidth)
        {
            if (paddingWidth < 0)
                throw new ArgumentException("Padding width cannot be negative.");

            builder.SetParameter(StyleCommands.PaddingWidth, paddingWidth.ToString());
            return this;
        }

        /// <summary>
        /// Sets the padding color (defaults to background color)
        /// </summary>
        /// <param name="paddingColor">Either a color name or 6 character hex code e.g. FFFFFF</param>
        public StyleExpression PaddingColor(string paddingColor)
        {
            if (string.IsNullOrEmpty(paddingColor))
                throw new ArgumentNullException("paddingColor");

            builder.SetParameter(StyleCommands.PaddingColor, paddingColor);
            return this;
        }

        /// <summary>
        /// Sets the width of the image border
        /// </summary>
        /// <param name="borderWidth">The desired width in pixels</param>
        public StyleExpression BorderWidth(int borderWidth)
        {
            if (borderWidth < 0)
                throw new ArgumentException("Border width cannot be negative.");

            builder.SetParameter(StyleCommands.BorderWidth, borderWidth.ToString());
            return this;
        }

        /// <summary>
        /// Sets the border color
        /// </summary>
        /// <param name="borderColor">Either a color name or 6 character hex code e.g. FFFFFF</param>
        public StyleExpression BorderColor(string borderColor) 
        {
            if (string.IsNullOrEmpty(borderColor))
                throw new ArgumentNullException("borderColor");

            builder.SetParameter(StyleCommands.BorderColor, borderColor);
            return this;
        }

        /// <summary>
        /// Sets a universal margin for the image
        /// </summary>
        /// <param name="width">The desired margin size in pixels</param>
        public StyleExpression Margin(int width)
        {
            if (width < 0)
                throw new ArgumentException("Margins cannot be negative.");

            builder.SetParameter(StyleCommands.Margin, width.ToString());
            return this;
        }

        /// <summary>
        /// Sets the left, top, right and bottom margin widths
        /// </summary>
        public StyleExpression Margin(int left, int top, int right, int bottom)
        {
            int[] margins = new[] { left, top, right, bottom };

            foreach (var margin in margins)
                if (margin < 0) { throw new ArgumentException("Margins cannot be negative"); }

            builder.SetParameter(StyleCommands.Margin, string.Join(",", margins));
            return this;
        }

        /// <summary>
        /// Adds a drop shadow to the image. Requires the drop shadow plugin.
        /// For more information see http://imageresizing.net/plugins/dropshadow.
        /// </summary>
        /// <param name="shadowWidth">The desired shadow width in pixels. <example>1</example></param>
        /// <param name="shadowColor">Configure the shade and opacity of the drop-shadow <example>black|rrggbbaa</example></param>
        public DropShadowExpression DropShadow(int shadowWidth, string shadowColor)
        {
            if (shadowWidth < 1)
                throw new ArgumentException("Shadow Width must be greater than 0.");

            if (string.IsNullOrEmpty(shadowColor))
                throw new ArgumentNullException("shadowColor");

            builder.SetParameter(StyleCommands.ShadowWidth, shadowWidth.ToString());
            builder.SetParameter(StyleCommands.ShadowColor, shadowColor);
            return new DropShadowExpression(builder);
        }

        /// <summary>
        /// Styling commands see http://imageresizing.net/docs/reference
        /// </summary>
        private static class StyleCommands
        {
            internal const string BackgroundColor = "bgcolor";
            internal const string PaddingWidth = "paddingWidth";
            internal const string PaddingColor = "paddingColor";
            internal const string BorderWidth = "borderWidth";
            internal const string BorderColor = "borderColor";
            internal const string Margin = "margin";
            internal const string ShadowWidth = "shadowwidth";
            internal const string ShadowColor = "shadowcolor";
        }
    }

    public static class StyleExtensions
    {
        /// <summary>
        /// Adds Styling options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example adds 5px padding to the image and applies a 2px black border
        /// <code>
        /// builder.Style(img => img.PaddingWidth(5).BorderColor("000000").BorderWidth(2))
        /// </code>
        /// </example>
        public static ImageUrlBuilder Style(this ImageUrlBuilder builder, Action<StyleExpression> configure)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (configure == null)
                throw new ArgumentNullException("configure");
            
            configure(new StyleExpression(builder));
            return builder;
        }
    }
}
