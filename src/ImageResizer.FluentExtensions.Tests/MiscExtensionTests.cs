using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class MiscExtensionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Watermark()
        {
            builder.Watermark("test1,test2").BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?watermark=test1,test2".Replace(",", "%2c"));
        }

        [Test]
        public void Image404()
        {
            builder.Image404("notfound.jpg").BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?404=notfound.jpg");
        }

        [Test]
        public void Apply_Presets()
        {
            builder.ApplyPresets("preset1").BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?preset=preset1");
        }

        [Test]
        public void IgnoreICC()
        {
            builder.IgnoreICC().BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?ignoreicc=true");
        }

        [Test]
        public void Cache()
        {
            builder.Cache(CacheOptions.Always).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?cache=always");
        }

        [Test]
        public void Process()
        {
            builder.Process(ProcessOptions.No).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?process=no");
        }

        [Test]
        public void DPI()
        {
            builder.DPI(DPIOptions.DPI300).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?dpi=300");
        }
    }
}
