using System;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// A base class for image builder expressions
    /// </summary>
    public abstract class ImageBuilderExpression
    {
        protected readonly ImageBuilder builder;

        public ImageBuilderExpression(ImageBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
        }
    }
}
