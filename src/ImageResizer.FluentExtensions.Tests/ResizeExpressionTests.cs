using NUnit.Framework;

namespace ImageResizer.FluentExtensions.Tests
{
    [TestFixture]
    public class ResizeExpressionTests
    {
        ImageUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new ImageUrlBuilder();
        }

        [Test]
        public void Resize_dimensions()
        {
            builder.Resize(img => img.Dimensions(100, 150)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=150");
        }

        [Test]
        public void Resize_height()
        {
            builder.Resize(img => img.Height(100)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?height=100");
        }

        [Test]
        public void Resize_width()
        {
            builder.Resize(img => img.Width(100)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100");
        }

        [Test]
        public void Resize_maxheight()
        {
            builder.Resize(img => img.MaxHeight(100)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?maxheight=100");
        }

        [Test]
        public void Resize_maxwidth()
        {
            builder.Resize(img => img.MaxWidth(100)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?maxwidth=100");
        }

        [Test]
        public void Resize_Crop()
        {
            builder.Resize(img => img.Crop(1,2,3,4)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?crop=(1,2,3,4)");
        }

        [Test]
        public void Resize_Crop_with_Rectange()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(1, 1, 10, 10);
            builder.Resize(img => img.Crop(rect)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?crop=(1,1,11,11)");
        }

        [Test]
        public void Resize_Crop_with_alignment()
        {
            builder.Resize(img => img.Crop(1, 2, 3, 4).Anchor(AnchorPoint.TopCenter)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?crop=(1,2,3,4)&anchor=topcenter");
        }

        [Test]
        public void Resize_mode_Max()
        {
            builder.Resize(img => img.Dimensions(100, 100).Max()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=max");
        }

        [Test]
        public void Resize_mode_Pad()
        {
            builder.Resize(img => img.Dimensions(100, 100).Pad()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=pad");
        }

        [Test]
        public void Resize_mode_Pad_with_alignment()
        {
            builder.Resize(img => img.Dimensions(100, 100).Pad().Anchor(AnchorPoint.TopCenter)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=pad&anchor=topcenter");
        }

        [Test]
        public void Resize_mode_Crop()
        {
            builder.Resize(img => img.Dimensions(100, 100).Crop()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=crop");
        }

        [Test]
        public void Resize_mode_Crop_with_alignment()
        {
            builder.Resize(img => img.Dimensions(100, 100).Crop().Anchor(AnchorPoint.TopCenter)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=crop&anchor=topcenter");
        }

        [Test]
        public void Resize_mode_Stretch()
        {
            builder.Resize(img => img.Dimensions(100, 100).Stretch()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&mode=stretch");
        }

        [Test]
        public void Scale_type_Both()
        {
            builder.Resize(img => img.Dimensions(100, 100).ScaleBoth()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&scale=both");
        }

        [Test]
        public void Scale_type_Up()
        {
            builder.Resize(img => img.Dimensions(100, 100).ScaleUp()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&scale=upscaleonly");
        }

        [Test]
        public void Scale_type_Down()
        {
            builder.Resize(img => img.Dimensions(100, 100).ScaleDown()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&scale=downscaleonly");
        }

        [Test]
        public void Scale_type_Canvas()
        {
            builder.Resize(img => img.Dimensions(100, 100).ScaleCanvas()).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&scale=upscalecanvas");
        }

        [Test]
        public void Scale_type_Canvas_with_anchor()
        {
            builder.Resize(img => img.Dimensions(100, 100).ScaleCanvas().Anchor(AnchorPoint.TopLeft)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&scale=upscalecanvas&anchor=topleft");
        }

        [Test]
        public void Resize_Zoom()
        {
            builder.Resize(img => img.Dimensions(100, 100).Zoom(2)).BuildUrl("image.jpg")
                .ShouldEqual("image.jpg?width=100&height=100&zoom=2");
        }
    }
}
