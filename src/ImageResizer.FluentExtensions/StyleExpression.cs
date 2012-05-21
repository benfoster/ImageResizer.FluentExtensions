
using System;
namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring image style options
    /// </summary>
    public class StyleExpression : ImageBuilderExpression
    {
        internal StyleExpression(ImageBuilder builder) : base(builder) { }

        /// <summary>
        /// Sets the background/whitespace color.
        /// </summary>
        /// <param name="backgroundColor">Either a color name or 6 character hex code e.g. FFFFFF</param>
        public StyleExpression BackgroundColor(string backgroundColor)
        {
            if (string.IsNullOrEmpty(backgroundColor))
                throw new ArgumentNullException("backgroundColor");
            
            builder.SetParameter(StyleParameters.BackgroundColor, backgroundColor);
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

            builder.SetParameter(StyleParameters.PaddingWidth, paddingWidth.ToString());
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

            builder.SetParameter(StyleParameters.PaddingColor, paddingColor);
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

            builder.SetParameter(StyleParameters.BorderWidth, borderWidth.ToString());
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

            builder.SetParameter(StyleParameters.BorderColor, borderColor);
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

            builder.SetParameter(StyleParameters.Margin, width.ToString());
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

            builder.SetParameter(StyleParameters.Margin, string.Join(",", margins));
            return this;
        }

        private static class StyleParameters
        {
            internal const string BackgroundColor = "bgcolor";
            internal const string PaddingWidth = "paddingWidth";
            internal const string PaddingColor = "paddingColor";
            internal const string BorderWidth = "borderWidth";
            internal const string BorderColor = "borderColor";
            internal const string Margin = "margin";
        }
    }
}
