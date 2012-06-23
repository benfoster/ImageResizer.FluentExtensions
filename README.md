# Fluent Extensions for the ImageResizing image processing module

## Overview

This project provides a fluent interface for constructing requests to the [ImageResizer](http://imageresizing.net) URL api.

The fluent API is based on the command structure found on the [ImageResizer Command Reference Page](http://imageresizing.net/docs/reference). Currently we're up to about 80% API coverage.

## Download

Available on NuGet:

- [ImageResizing.FluentExtensions](http://nuget.org/packages/ImageResizer.FluentExtensions) (the fluent api)
- [ImageResizing.FluentExtensions.Mvc](http://nuget.org/packages/ImageResizer.FluentExtensions.Mvc) (ASP.NET MVC Helpers)

## License

Licensed under the MIT License.

## Using the Fluent API

The Fluent API is easily discoverable, well documented and doesn't require knowledge of the Image Resizer URL API.

The core ImageResizer functionality is grouped logically into the following expressions:

- Resize
- Transform
- Style
- Output

Each expression has various configuration methods that set up the commands for the ImageResizer API. 

We've also created extensions for the following ImageResizer plugins:

- Gradients > `builder.Gradient(img => ...)`
- SimpleFilters > `builder.SimpleFilters(img => ...)`
- DropShadow > `buider.Style(img => img.DropShadow(...))`
- Watermark > `builder.Watermark(...)`
- Image404 > `builder.Image404(...)`
- Presets > `builder.ApplyPresets(...)`

The two main components of the API are **Builder Expressions** and **Url Modifiers**. 

#### Builder Expressions

Builder Expressions are used to add command arguments (querystring parameters) to your image url. Effectively these are just extension methods for `ImageUrlBuilder` that provide a nice way of calling `ImageUrlBuilder.SetParameter`. This makes creating up your own fluent extensions fairly trivial.

As an example, below shows how you would construct an image url using the native URL API to perform resizing, transformations, styling and configuring output options. In reality you're usage is likely to be more simple than this.

	/image.jpg?width=400&height=300&mode=crop _
		&flip=x&paddingWidth=10&paddingColor=FF0066&margin=20&bgcolor=000000 _
		&quality=90&format=png

To generate the URL using the Fluent API we would do the following:

    var urlString = new ImageUrlBuilder()
        .Resize(img => img.Dimensions(400, 300).Crop())
        .Transform(img => img.FlipAfter(FlipType.X))
        .Style(img => img.PaddingWidth(10).PaddingColor("FF0066").Margin(20).BackgroundColor("000000"))
        .Output(img => img.Quality(90).Format(OutputFormat.Png))
		.BuildUrl("image.jpg")

The ImageResizer.FluentExtensions.Mvc project also includes both HtmlHelper and UrlHelper extension methods for building URLs.

Using the HtmlHelper:

	@Html.BuildImage("~/images/image.jpg", x => x.Resize(img => img.MaxHeight(200)))

Using the UrlHelper:

	<img src="@Url.ImageUrl("~/images/image.jpg", x => x.Resize(img => img.MaxHeight(200)))" alt=""/>

Notice that both take in a delegate for configuring the ImageUrlBuilder.

**Reuse your builders**

We've designed `ImageUrlBuilder` to be reusable. A common scenario would be to loop through a collection of image paths and display the image. Since you probably wan't all your images to look the same, you should configure your builder and reuse it for every image. Both the UrlHelper and HtmlHelper extension methods have overloads for doing this:

	@{
		var builder = new ImageUrlBuilder().Resize(img => img.Dimensions(200, 300));
	}
	
	<ul>
		@foreach (var image in Model.Images) {
			<li><img src="@Url.ImageUrl(image, builder)" alt=""/></li>
		}
	</ul>

#### Url Modifiers

Url Modifiers are functions that are applied to a an Image URL after it has been generated. Some of the plugins (such as those in the [Cloud Bundle](http://imageresizing.net/plugins/bundles/3)) require that you add a prefix to your ImageURLs. The API currently includes modifiers for Azure, S3 and Remote plugins:

	// using the Azure Reader Plugin
	var builder = new ImageUrlBuilder()
					.Resize(img => img.MaxWidth(200))
					.FromAzure(prefix: "cloud", container: "public")
					.BuildUrl("image.jpg");

	// using the S3 Reader Plugin
	var builder = new ImageUrlBuilder()
					.Resize(img => img.MaxWidth(200))
					.FromS3(bucketName: "public")
					.BuildUrl("image.jpg");
					
	// using the Remote Reader Plugin
	var builder = new ImageUrlBuilder()
					.Resize(img => img.MaxWidth(200))
					.FromRemote()
					.BuildUrl("http://somedomain.com/images/image.jpg");

Both the Azure and S3 extensions work in the same way in that if you haven't specified a container/bucket name, it will be inferred from your image path, so passing the builder a url of "public/images/someimage.jpg" would infer that your container/bucket name is "public". Alternatively specifying a container/bucket name of "myblog" would generate a url of "{prefix}/myblog/public/images/someimage.jpg".

You can also apply your own modifiers by simply passing a string function to the builder. Here's one for lower-casing all URLs:

	var builder = new ImageUrlBuilder()
					.Resize(img => img.MaxWidth(200))
					.AddModifier(url => url.ToLower())
					.BuildUrl("image.jpg");

## Release Notes

### Changes in 1.0.0.4

 - ImageBuilder renamed to ImageUrlBuilder
 - Build() method renamed to BuildUrl e.g.

	 	new ImageUrlBuilder().BuildUrl("image.jpg")

 - New UrlHelper extension methods in ImageResizer.FluentExtensions.Mvc
	
		// passing in a builder expression
		@Url.ImageUrl("~/image.jpg", builder => builder.Resize(img => img.Width(200)))  

		// passing in an existing builder
		@Url.ImageUrl("~/image.jpg", builder) 

