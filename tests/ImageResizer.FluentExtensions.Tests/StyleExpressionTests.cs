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
            builder.Style(img => img.BackgroundColor("FFFFFF")).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?bgcolor=FFFFFF");
        }

        [Test]
        public void Style_padding_width()
        {
            builder.Style(img => img.PaddingWidth(20)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?paddingWidth=20");
        }

        [Test]
        public void Style_padding_color()
        {
            builder.Style(img => img.PaddingColor("000000")).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?paddingColor=000000");
        }

        [Test]
        public void Style_border_width()
        {
            builder.Style(img => img.BorderWidth(10)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?borderWidth=10");
        }

        [Test]
        public void Style_border_color()
        {
            builder.Style(img => img.BorderColor("000000")).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?borderColor=000000");
        }

        [Test]
        public void Style_margin()
        {
            builder.Style(img => img.Margin(10)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?margin=10");
        }

        [Test]
        public void Style_margin_specific()
        {
            builder.Style(img => img.Margin(10, 5, 10, 5)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?margin=10,5,10,5".Replace(",", "%2c")); // url encoded
        }

        [Test]
        public void Style_Drop_Shadow()
        {
            builder.Style(img => img.DropShadow(10, "black").Offset(5, 5)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?shadowwidth=10&shadowcolor=black&shadowoffset=5,5".Replace(",", "%2c"));
        }
    }
}
