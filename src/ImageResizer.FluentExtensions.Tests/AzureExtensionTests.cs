using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class AzureExtensionTests
    {       
        [Test]
        public void Azure_with_defaults()
        {
            var url = new ImageUrl("testimage.jpg");
            url.FromAzure().ToString().ShouldEqual("/azure/testimage.jpg");
        }

        [Test]
        public void Azure_with_prefix()
        {
            var url = new ImageUrl("testimage.jpg");
            url.FromAzure(prefix: "blobs").ToString().ShouldEqual("/blobs/testimage.jpg");
        }

        [Test]
        public void Azure_with_container()
        {
            var url = new ImageUrl("testimage.jpg");
            url.FromAzure(container: "images").ToString().ShouldEqual("/azure/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_prefix_and_container()
        {
            var url = new ImageUrl("testimage.jpg");
            url.FromAzure("blobs", "images").ToString().ShouldEqual("/blobs/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_subfolders()
        {
            var url = new ImageUrl("images/archive/2011/testimage.jpg");
            url.FromAzure().ToString().ShouldEqual("/azure/images/archive/2011/testimage.jpg");
        }

        [Test]
        public void Azure_with_container_and_subfolders()
        {
            var url = new ImageUrl("images/archive/2011/testimage.jpg");
            url.FromAzure(container: "blobs").ToString().ShouldEqual("/azure/blobs/images/archive/2011/testimage.jpg");
        }

        [Test]
        public void Azure_with_absolute_path()
        {
            var url = new ImageUrl("/images/testimage.jpg");
            url.FromAzure().ToString().ShouldEqual("/azure/images/testimage.jpg");
        }

        [Test]
        public void Azure_with_virtual_path()
        {
            var url = new ImageUrl("~/images/testimage.jpg");
            url.FromAzure().ToString().ShouldEqual("/azure/images/testimage.jpg");
        }
    }
}
