using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class GradientExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Gradient_Dimensions()
        {
            builder.Gradient(img => img.Dimensions(200, 100)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=200&height=100");
        }

        [Test]
        public void Gradient_Colors()
        {
            builder.Gradient(img => img.Colors("red", "black")).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?color1=red&color2=black");
        }

        [Test]
        public void Gradient_Dimensions_Colors_Angle()
        {
            builder.Gradient(img => img.Dimensions(200, 100).Colors("red", "black").Angle(90)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=200&height=100&color1=red&color2=black&angle=90");
        }
    }
}
