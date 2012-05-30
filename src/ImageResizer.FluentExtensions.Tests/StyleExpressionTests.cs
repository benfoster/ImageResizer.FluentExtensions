using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class StyleExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }
        
        [Test]
        public void Style_background_color()
        {
            builder.Style(img => img.BackgroundColor("FFFFFF")).Build("image.jpg")
                .ShouldEqual("image.jpg?bgcolor=FFFFFF");
        }

        [Test]
        public void Style_padding_width()
        {
            builder.Style(img => img.PaddingWidth(20)).Build("image.jpg")
                .ShouldEqual("image.jpg?paddingWidth=20");
        }

        [Test]
        public void Style_padding_color()
        {
            builder.Style(img => img.PaddingColor("000000")).Build("image.jpg")
                .ShouldEqual("image.jpg?paddingColor=000000");
        }

        [Test]
        public void Style_border_width()
        {
            builder.Style(img => img.BorderWidth(10)).Build("image.jpg")
                .ShouldEqual("image.jpg?borderWidth=10");
        }

        [Test]
        public void Style_border_color()
        {
            builder.Style(img => img.BorderColor("000000")).Build("image.jpg")
                .ShouldEqual("image.jpg?borderColor=000000");
        }

        [Test]
        public void Style_margin()
        {
            builder.Style(img => img.Margin(10)).Build("image.jpg")
                .ShouldEqual("image.jpg?margin=10");
        }

        [Test]
        public void Style_margin_specific()
        {
            builder.Style(img => img.Margin(10, 5, 10, 5)).Build("image.jpg")
                .ShouldEqual("image.jpg?margin=10%2c5%2c10%2c5"); // url encoded
        }
    }
}
