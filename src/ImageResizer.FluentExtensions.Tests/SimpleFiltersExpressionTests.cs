using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class SimpleFiltersExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }
        
        [Test]
        public void Filters_greyscale_default()
        {
            builder.SimpleFilters(img => img.Grayscale()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.grayscale=true");
        }

        [Test]
        public void Filters_greyscale_advanced()
        {
            builder.SimpleFilters(img => img.Grayscale(GrayscaleOptions.Flat)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.grayscale=flat");
        }

        [Test]
        public void Filters_sepia()
        {
            builder.SimpleFilters(img => img.Sepia()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.sepia=true");
        }

        [Test]
        public void Filters_alpha()
        {
            builder.SimpleFilters(img => img.Alpha(0.5)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.alpha=0.5");
        }

        [Test]
        public void Filters_brightness()
        {
            builder.SimpleFilters(img => img.Brightness(0.5)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.brightness=0.5");
        }

        [Test]
        public void Filters_contrast()
        {
            builder.SimpleFilters(img => img.Contrast(-0.8)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.contrast=-0.8");
        }

        [Test]
        public void Filters_saturation()
        {
            builder.SimpleFilters(img => img.Saturate(.75)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.saturation=0.75");
        }

        [Test]
        public void Filters_invert()
        {
            builder.SimpleFilters(img => img.Invert()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.invert=true");
        }

        [Test]
        public void Filters_rounded_corners_equal()
        {
            builder.SimpleFilters(img => img.RoundedCorners(30)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.roundcorners=30");
        }

        [Test]
        public void Filters_rounded_corners_individual()
        {
            builder.SimpleFilters(img => img.RoundedCorners(30, 0, 0, 30)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?s.roundcorners=30,0,0,30".Replace(",", "%2c")); // url encoded
        }
    }
}
