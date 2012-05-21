# Fluent Extensions for the ImageResizing image processing module

## Overview

This project provides a fluent interface for constructing requests to the [ImageResizer HTTP module](http://imageresizing.net).

The fluent API is based on the command structure found on the [ImageResizer Command Reference Page](http://imageresizing.net/docs/reference).

## Example

Using the querystring api:

`/image.jpg?width=400&height=300&mode=crop&flip=x&paddingWidth=10&paddingColor=FF0066&margin=20&bgcolor=000000&quality=90&format=png`

Using the fluent api mvc helper:

    var builder = new ImageBuilder()
        .Resize(img => img.Dimensions(400, 300).Crop())
        .Transform(img => img.FlipAfter(FlipType.X))
        .Style(img => img.PaddingWidth(10).PaddingColor("FF0066").Margin(20).BackgroundColor("000000"))
        .Output(img => img.Quality(90).Format(OutputFormat.Png));


### Download

Available on NuGet:

- [ImageResizing.FluentExtensions](http://nuget.org/packages/ImageResizer.FluentExtensions) (the fluent api)
- [ImageResizing.FluentExtensions.Mvc](http://nuget.org/packages/ImageResizer.FluentExtensions.Mvc) (ASP.NET MVC Helpers)

### License

Licensed under the MIT License.