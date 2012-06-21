using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    public class SimpleFiltersExpression : ImageUrlBuilderExpression
    {
        public SimpleFiltersExpression(ImageUrlBuilder builder) : base(builder) { }

        public SimpleFiltersExpression Sepia()
        {
            builder.SetParameter(SimpleFiltersParameters.Sepia, "true");
            return this;
        }

        public SimpleFiltersExpression Brightness(decimal value)
        {
            if (value < -1 || value > 1)
                throw new ArgumentException("Brightness must be between -1 and 1");

            builder.SetParameter(SimpleFiltersParameters.Brightness, value.ToString());
            return this;
        }

        private static class SimpleFiltersParameters
        {
            internal const string Sepia = "sepia";
            internal const string Brightness = "brightness";
        }
    }

    public static class ImageBuilderExtensions
    {
        public static ImageUrlBuilder ApplyFilters(this ImageUrlBuilder builder, Action<SimpleFiltersExpression> configure)
        {
            var expression = new SimpleFiltersExpression(builder);
            configure(expression);
            return builder;
        }
    }

    [TestFixture]
    public class ImageUrlBuilderExtensionTests
    {
        [Test]
        public void Can_use_extension()
        {
            new ImageUrlBuilder()
                .Resize(img => img.MaxWidth(200))
                .ApplyFilters(filters => filters.Sepia().Brightness(.75M))
                .BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?maxwidth=200&sepia=true&brightness=0.75");
        }
    }
}
