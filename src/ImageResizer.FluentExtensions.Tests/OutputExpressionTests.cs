using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class OutputExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Output_Format()
        {
            builder.Output(img => img.Format(OutputFormat.Png)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?format=png");
        }

        [Test]
        public void Output_Quality()
        {
            builder.Output(img => img.Quality(90)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?quality=90");
        }

        [Test]
        public void Output_Colors()
        {
            builder.Output(img => img.Colors(64)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?colors=64");
        }
    }
}
