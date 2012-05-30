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
            builder.SetParameter(TransformParameters.AutoRotate, true.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Rotates the source image prior to processing (only 90 degree intervals)
        /// </summary>
        /// <param name="rotateType">The type of rotation to perform</param>
        public TransformExpression Rotate(RotateType rotateType)
        {
            builder.SetParameter(TransformParameters.RotateInterval, ((int)rotateType).ToString());
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

            builder.SetParameter(TransformParameters.RotateDegrees, degrees.ToString());
            return this;
        }

        /// <summary>
        /// Flips the image prior to processing
        /// </summary>
        public TransformExpression FlipBefore(FlipType flipType)
        {
            builder.SetParameter(TransformParameters.FlipBefore, flipType.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Flips the image after everything is done
        /// </summary>
        public TransformExpression FlipAfter(FlipType flipType)
        {
            builder.SetParameter(TransformParameters.FlipAfter, flipType.ToString().ToLowerInvariant());
            return this;
        }

        private static class TransformParameters
        {
            internal const string AutoRotate = "autorotate";
            internal const string RotateInterval = "srotate";
            internal const string RotateDegrees = "rotate";
            internal const string FlipBefore = "sflip";
            internal const string FlipAfter = "flip";
        }
    }

    public enum RotateType
    {
        None = 0,
        Rotate90 = 90,
        Rotate180 = 180,
        Rotate270 = 270
    }

    public enum FlipType
    {
        None,
        X,
        Y,
        XY
    }
}
