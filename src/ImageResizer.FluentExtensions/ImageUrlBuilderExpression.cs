using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// A base class for image builder expressions
    /// </summary>
    public abstract class ImageUrlBuilderExpression
    {
        protected ImageUrlBuilder builder;

        public ImageUrlBuilderExpression(ImageUrlBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
        }

        public virtual ImageUrlBuilderExpression Initialize(ImageUrlBuilder builder)
        {
            this.builder = builder;
            return this;
        }
    }
}
