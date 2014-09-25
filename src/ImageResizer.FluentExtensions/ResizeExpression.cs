using System;
using System.Drawing;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring image resize options
    /// </summary>
    public class ResizeExpression : ImageUrlBuilderExpression
    {
        internal ResizeExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Sets the width and height of the image. Adds whitespace and centers to maintain aspect ratio
        /// </summary>
        /// <param name="width">The desired width in pixels</param>
        /// <param name="height">The desired height in pixels</param>
        public ResizeExpression Dimensions(int width, int height)
        {
            return Width(width).Height(height);
        }

        /// <summary>
        /// Sets the width of the image. Aspect ratio is maintained by default.
        /// </summary>
        /// <param name="width">The desired width in pixels</param>
        public ResizeExpression Width(int width)
        {
            if (width <= 0)
                throw new ArgumentException("Width must be greater than 0.");

            builder.SetParameter(ResizeCommands.Width, width.ToString());
            return this;
        }

        /// <summary>
        /// Sets the height of the image. Aspect ratio is maintained by default.
        /// </summary>
        /// <param name="height">The desired height in pixels</param>
        public ResizeExpression Height(int height)
        {
            if (height <= 0)
                throw new ArgumentException("Height must be greater than 0.");

            builder.SetParameter(ResizeCommands.Height, height.ToString());
            return this;
        }

        /// <summary>
        /// Sets the maxiumum width of the image. Maintains aspect ratio but does not add padding.
        /// </summary>
        /// <param name="maxWidth">The desired maxiumum width in pixels</param>
        public ResizeExpression MaxWidth(int maxWidth)
        {
            if (maxWidth <= 0)
                throw new ArgumentException("Max Width must be greater than 0.");

            builder.SetParameter(ResizeCommands.MaxWidth, maxWidth.ToString());
            return this;
        }

        /// <summary>
        /// Sets the maxiumum height of the image. Maintains aspect ratio but does not add padding.
        /// </summary>
        /// <param name="maxWidth">The desired maxiumum height in pixels</param>
        public ResizeExpression MaxHeight(int maxHeight)
        {
            if (maxHeight <= 0)
                throw new ArgumentException("Max Height must be greater than 0.");

            builder.SetParameter(ResizeCommands.MaxHeight, maxHeight.ToString());
            return this;
        }

        /// <summary>
        /// Sets the fit mode to Max - behaves like maxwidth/maxheight
        /// </summary>
        public ResizeExpression Max()
        {
            builder.SetParameter(ResizeCommands.FitMode, ResizeCommands.FitModeMax);
            return this;
        }

        /// <summary>
        /// Sets the fit mode to Pad - adds whitespace to resolve aspect-ratio conflicts
        /// </summary>
        public AlignmentExpression Pad()
        {
            builder.SetParameter(ResizeCommands.FitMode, ResizeCommands.FitModePad);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Sets the fit mode to Crop
        /// </summary>
        public AlignmentExpression Crop()
        {
            builder.SetParameter(ResizeCommands.FitMode, ResizeCommands.FitModeCrop);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Crop the image to the specified rectangle on the source image.
        /// </summary>
        /// <param name="x1">x coordinate of the top-left corner of the original image.</param>
        /// <param name="y1">y coordinate of the top-left corner of the original image.</param>
        /// <param name="x2">x coordinate of the bottom-right corner of the original image.</param>
        /// <param name="y2">y coordinate of the bottom-right corner of the original image.</param>
        public AlignmentExpression Crop(int x1, int y1, int x2, int y2)
        {
            if (x1 >= x2 && (x2 > 0 || x1 < 0))
                throw new ArgumentException("x1 must be greater than x2.");

            if (y1 >= y2 && (y2 > 0 || y1 < 0))
                throw new ArgumentException("y1 must be greater than y2.");

            builder.SetParameter(ResizeCommands.FitModeCrop, string.Format("({0},{1},{2},{3})", x1, y1, x2, y2));
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Crop the image to the specified rectangle on the source image.
        /// </summary>
        /// <param name="rect">The rectangle that represents the coordinates of the crop relative to the original image.</param>
        public AlignmentExpression Crop(Rectangle rect)
        {
            if (rect.Width <= 0)
                throw new ArgumentException("The crop width must be greater than 0.");
            if (rect.Height <= 0)
                throw new ArgumentException("The crop height must be greater than 0.");

            return Crop(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Sets the fit mode to Stretch. Stretches the image, losing aspect ratio
        /// </summary>
        public ResizeExpression Stretch()
        {
            builder.SetParameter(ResizeCommands.FitMode, ResizeCommands.FitModeStretch);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Allows scaling up of the image
        /// </summary>
        public ResizeExpression ScaleUp()
        {
            builder.SetParameter(ResizeCommands.ScaleType, ResizeCommands.ScaleTypeUp);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Allows scaling down of the image (default)
        /// </summary>
        public ResizeExpression ScaleDown()
        {
            builder.SetParameter(ResizeCommands.ScaleType, ResizeCommands.ScaleTypeDown);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Allows scaling up and down
        /// </summary>
        public ResizeExpression ScaleBoth()
        {
            builder.SetParameter(ResizeCommands.ScaleType, ResizeCommands.ScaleTypeBoth);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// Scales the image down and adds a margin to scale up
        /// </summary>
        public AlignmentExpression ScaleCanvas()
        {
            builder.SetParameter(ResizeCommands.ScaleType, ResizeCommands.ScaleTypeCanvas);
            return new AlignmentExpression(this.builder);
        }

        /// <summary>
        /// cale the image by a multiplier. Defaults to 1. 0.5 produces a half-size image, 2 produces a double-size image.
        /// </summary>
        /// <param name="multiplier"></param>
        public ResizeExpression Zoom(decimal multiplier)
        {
            if (multiplier <= 0)
                throw new ArgumentException("The zoom multiplier must be greater than 0.");

            builder.SetParameter(ResizeCommands.Zoom, multiplier.ToString());
            return this;
        }

        /// <summary>
        /// Resize commands see http://imageresizing.net/docs/reference
        /// </summary>
        private static class ResizeCommands
        {
            internal const string Width = "width";
            internal const string Height = "height";
            internal const string MaxWidth = "maxwidth";
            internal const string MaxHeight = "maxheight";
            internal const string FitMode = "mode";
            internal const string FitModeMax = "max";
            internal const string FitModePad = "pad";
            internal const string FitModeCrop = "crop";
            internal const string FitModeStretch = "stretch";
            internal const string ScaleType = "scale";
            internal const string ScaleTypeUp = "upscaleonly";
            internal const string ScaleTypeDown = "downscaleonly";
            internal const string ScaleTypeCanvas = "upscalecanvas";
            internal const string ScaleTypeBoth = "both";
            internal const string Zoom = "zoom";
        }
    }

    public static class ResizeExtensions
    {
        /// <summary>
        /// Adds Resize options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example crops and image to the specified width and height
        /// <code>
        /// builder.Resize(img => img.Width(200).Height(100).Crop())
        /// </code>
        /// </example>
        public static ImageUrlBuilder Resize(this ImageUrlBuilder builder, Action<ResizeExpression> configure)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (configure == null)
                throw new ArgumentNullException("configure");
            
            configure(new ResizeExpression(builder));
            return builder;
        }
    }
}
