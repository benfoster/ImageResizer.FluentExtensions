using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class OutputExpressionTests
    {
        ImageBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageBuilder();
        }

        [Test]
        public void Output_Format()
        {
            builder.Output(img => img.Format(OutputFormat.Png)).Build("image.jpg")
                .ShouldEqual("image.jpg?format=png");
        }

        [Test]
        public void Output_Quality()
        {
            builder.Output(img => img.Quality(90)).Build("image.jpg")
                .ShouldEqual("image.jpg?quality=90");
        }
    }
}
