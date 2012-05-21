using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class ImageBuilderTests
    {
        ImageBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageBuilder();
        }
        
        [Test]
        public void Empty_configuration_returns_original_image_name()
        {
            builder.Build("image.jpg")
                .ShouldEqual("image.jpg");
        }

        [Test]
        public void Can_chain_expressions()
        {
            builder
                .Resize(img => img.Dimensions(200, 100).Crop().Anchor(AnchorPoint.TopLeft))
                .Transform(img => img.FlipAfter(FlipType.X).Rotate(RotateType.Rotate180))
                .Style(img => img.PaddingWidth(10).PaddingColor("000000"))
                .Output(img => img.Format(OutputFormat.Png).Quality(90))
                .Build("image.jpg")
                .ShouldEqual("image.jpg?width=200&height=100&mode=crop&anchor=topleft&flip=x&srotate=180&paddingWidth=10&paddingColor=000000&format=png&quality=90");
        }
    }
}
