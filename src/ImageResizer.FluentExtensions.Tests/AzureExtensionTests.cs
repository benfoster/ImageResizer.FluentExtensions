using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class AzureExtensionTests
    {
        private ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }
        
        [Test]
        public void Azure_with_defaults()
        {
            builder.FromAzure().BuildUrl("testimage.jpg").ShouldEqual("/azure/testimage.jpg");
        }

        [Test]
        public void Azure_with_prefix()
        {
            builder.FromAzure(prefix: "blobs").BuildUrl("testimage.jpg").ShouldEqual("/blobs/testimage.jpg");
        }

        [Test]
        public void Azure_with_container()
        {
            builder.FromAzure(container: "images").BuildUrl("testimage.jpg").ShouldEqual("/azure/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_prefix_and_container()
        {
            builder.FromAzure("blobs", "images").BuildUrl("testimage.jpg").ShouldEqual("/blobs/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_subfolders()
        {
            builder.FromAzure().BuildUrl("images/archive/2011/testimage.jpg")
                .ShouldEqual("/azure/images/archive/2011/testimage.jpg");
        }

        [Test]
        public void Azure_with_container_and_subfolders()
        {
            builder.FromAzure(container: "blobs").BuildUrl("images/archive/2011/testimage.jpg")
                .ShouldEqual("/azure/blobs/images/archive/2011/testimage.jpg");
        }

        [Test]
        public void Azure_with_absolute_path()
        {
            builder.FromAzure().BuildUrl("/images/testimage.jpg")
                .ShouldEqual("/azure/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_virtual_path()
        {
            builder.FromAzure().BuildUrl("~/images/testimage.jpg")
                .ShouldEqual("/azure/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_prefix_container_and_commands()
        {
            builder.Resize(img => img.MaxWidth(100)).FromAzure("cloud", "images")
                .BuildUrl("/archive/testimage.jpg")
                .ShouldEqual("/cloud/images/archive/testimage.jpg?maxwidth=100");
        }
    }
}
