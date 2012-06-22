using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring image resize options
    /// </summary>
    public class TransformExpression : ImageUrlBuilderExpression
    {
        internal TransformExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Automatically rotates the image based on the EXIF info from the camera. (Requires the AutoRotate plugin)
        /// </summary>
        public TransformExpression AutoRotate()
        {
            builder.SetParameter(TransformCommands.AutoRotate, true.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Rotates the source image prior to processing (only 90 degree intervals)
        /// </summary>
        /// <param name="rotateType">The type of rotation to perform</param>
        public TransformExpression Rotate(RotateType rotateType)
        {
            builder.SetParameter(TransformCommands.RotateInterval, ((int)rotateType).ToString());
            return this;
        }
        
        /// <summary>
        /// Rotate the image any arbitrary angle (occurs after cropping)
        /// </summary>
        /// <param name="degrees">The angle of which to rotate the image</param>
        /// <returns></returns>
        public TransformExpression Rotate(int degrees)
        {
            if (degrees < 0)
                throw new ArgumentException("The angle can not be negative");

            builder.SetParameter(TransformCommands.RotateDegrees, degrees.ToString());
            return this;
        }

        /// <summary>
        /// Flips the image prior to processing
        /// </summary>
        public TransformExpression FlipBefore(FlipType flipType)
        {
            builder.SetParameter(TransformCommands.FlipBefore, flipType.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Flips the image after everything is done
        /// </summary>
        public TransformExpression FlipAfter(FlipType flipType)
        {
            builder.SetParameter(TransformCommands.FlipAfter, flipType.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Transform commands see http://imageresizing.net/docs/reference
        /// </summary>
        private static class TransformCommands
        {
            internal const string AutoRotate = "autorotate";
            internal const string RotateInterval = "srotate";
            internal const string RotateDegrees = "rotate";
            internal const string FlipBefore = "sflip";
            internal const string FlipAfter = "flip";
        }
    }

    /// <summary>
    /// Image Rotation options
    /// </summary>
    public enum RotateType
    {
        None = 0,
        Rotate90 = 90,
        Rotate180 = 180,
        Rotate270 = 270
    }

    /// <summary>
    /// Image Flip Options
    /// </summary>
    public enum FlipType
    {
        None,
        X,
        Y,
        XY
    }

    public static class TransformExtensions
    {
        /// <summary>
        /// Adds Transformation options to the <see cref="ImageUrlBuilder"/>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the builder or configure action is null</exception>
        /// <example>
        /// This example rotates the image 180 degrees then flips the image on the X axis
        /// <code>
        /// builder.Transform(img => img.Rotate(RotateType.Rotate180).FlipAfter(FlipType.X))
        /// </code>
        /// </example>
        public static ImageUrlBuilder Transform(this ImageUrlBuilder builder, Action<TransformExpression> configure)
        {           
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (configure == null)
                throw new ArgumentNullException("configure");

            configure(new TransformExpression(builder));
            return builder;
        }
    }
}
