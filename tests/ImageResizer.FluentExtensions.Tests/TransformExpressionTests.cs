using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class TransformExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Transform_AutoRotate()
        {
            builder.Transform(img => img.AutoRotate()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?autorotate=true");
        }

        [Test]
        public void Transform_Rotate_Interval()
        {
            builder.Transform(img => img.Rotate(RotateType.Rotate180)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?srotate=180");
        }

        [Test]
        public void Transform_Rotate_Degrees()
        {
            builder.Transform(img => img.Rotate(270)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?rotate=270");
        }

        [Test]
        public void Transform_Flip_Before()
        {
            builder.Transform(img => img.FlipBefore(FlipType.X)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?sflip=x");
        }

        [Test]
        public void Transform_Flip_After()
        {
            builder.Transform(img => img.FlipAfter(FlipType.XY)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?flip=xy");
        }
    }
}
