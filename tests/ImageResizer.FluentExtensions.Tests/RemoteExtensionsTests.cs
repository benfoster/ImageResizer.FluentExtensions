using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class RemoteExtensionsTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Can_generate_url()
        {
            builder.Resize(img => img.Height(200)).FromRemote()
                .BuildUrl("http://img.imageresizing.net/utah2.jpg")
                .ShouldEqual("/remote/img.imageresizing.net/utah2.jpg?height=200");
        }
    }
}
