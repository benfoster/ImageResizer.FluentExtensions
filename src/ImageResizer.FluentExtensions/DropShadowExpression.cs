using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// As expression for configuring dropshadow plugin options. 
    /// For more information see http://imageresizing.net/plugins/dropshadow.
    /// </summary> 
    public class DropShadowExpression : StyleExpression
    {
        internal DropShadowExpression(ImageUrlBuilder builder) : base(builder) { }

        /// <summary>
        /// Configure the depth and angle of the shadow by specifying how far offset it is from the image
        /// </summary>
        /// <example>
        /// 10,10
        /// </example>
        public StyleExpression Offset(int offsetDepth, int offsetAngle)
        {
            if (offsetDepth < 0)
                throw new ArgumentException("Offset depth must be greater than or equal to 0.");
            if (offsetAngle < 0)
                throw new ArgumentException("Offset angle must be greater than or equal to 0.");

            builder.SetParameter(DropShadowCommands.ShadowOffset, string.Concat(offsetDepth, ",", offsetAngle));
            return this;
        }

        /// <summary>
        /// Drop Shadow Plugin Commands. See http://imageresizing.net/plugins/dropshadow.
        /// </summary>
        private static class DropShadowCommands
        {
            internal const string ShadowOffset = "shadowoffset";
        }
    }
}
