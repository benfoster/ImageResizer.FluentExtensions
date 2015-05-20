using System;
using Machine.Specifications;

namespace ImageResizer.FluentExtensions.Tests
{
    public class ImageUrlBuilderSpecs
    {
        static Exception exception;
        static ImageUrlBuilder builder;
        static string result;
        
        [Subject("Adding modifiers")]
        public class Adding_modifiers
        {           
            public class When_the_modifier_is_null
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.AddModifier(null));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_modifier_is_a_valid_function
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => builder.AddModifier(s => s.ToLower());

                It Should_apply_the_modifier = ()
                    => builder.BuildUrl("TESTIMAGE.JPG").ShouldEqual("testimage.jpg");
            }
        }

        [Subject("Clearing modifiers")]
        public class Clearing_modifiers
        {
            public class When_the_url_builder_has_modifiers
            {
                Establish ctx = () => {
                    builder = new ImageUrlBuilder().AddModifier(s => s.ToUpper());
                };

                Because of = ()
                    => builder.ClearModifiers();

                It Should_remove_all_modifiers = ()
                    => builder.BuildUrl("someimage.jpg").ShouldEqual("someimage.jpg"); // url not modified by modifiers
            }
        }

        [Subject("Setting parameters")]
        public class Setting_parameters
        {
            public class When_the_parameter_name_is_null
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.SetParameter(null, "test"));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_parameter_name_is_empty
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.SetParameter("", "test"));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_parameter_value_is_null
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.SetParameter("test", null));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_parameter_value_is_empty
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.SetParameter("test", ""));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }
        }
        
        [Subject("Building a url")]
        public class Building_a_url
        {
            public class When_the_image_path_is_null
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.BuildUrl(null));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_image_path_is_empty
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => exception = Catch.Exception(() => builder.BuildUrl(""));

                It Should_throw = ()
                    => exception.ShouldBeOfType<ArgumentNullException>();
            }
            
            public class When_the_configuration_is_empty_and_no_modifiers_exist
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => result = builder.BuildUrl("images/testimage.jpg");

                It Should_return_the_original_file_name = ()
                    => result.ShouldEqual("images/testimage.jpg");
            }

            public class When_the_configuration_is_empty_and_modifiers_exist
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();
                
                Because of = ()
                    => result = builder.AddModifier(s => s.ToLower()).BuildUrl("TESTIMAGE.JPG");

                It Should_apply_the_modifier = ()
                    => result.ShouldEqual("testimage.jpg");
            }

            public class When_a_configuration_exists_and_no_modifiers_exist
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => result = builder.SetParameter("width", "100").BuildUrl("testimage.jpg");

                It Should_apply_the_configuration_expressions = ()
                    => result.ShouldEqual("testimage.jpg?width=100");
            }

            public class When_multiple_modifiers_exist
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => result = builder
                        .AddModifier(s => "2-" + s)
                        .AddModifier(s => "1-" + s)
                            .BuildUrl("testimage.jpg");

                It Should_apply_them_in_order = ()
                    => result.ShouldEqual("1-2-testimage.jpg");
            }

            public class When_a_configuration_and_modifiers_exist
            {
                Establish ctx = ()
                    => builder = new ImageUrlBuilder();

                Because of = ()
                    => result = builder
                                .Resize(img => img.Dimensions(200, 100).Crop().Anchor(AnchorPoint.TopLeft))
                                .Transform(img => img.FlipAfter(FlipType.X).Rotate(RotateType.Rotate180))
                                .Style(img => img.PaddingWidth(10).PaddingColor("000000"))
                                .Output(img => img.Format(OutputFormat.Png).Quality(90))
                                .AddModifier(s => "/cloud/" + s)
                                .BuildUrl("image.jpg");

                It Should_build_the_url_and_apply_the_modifiers = ()
                    => result.ShouldEqual("/cloud/image.jpg?width=200&height=100&mode=crop&anchor=topleft&flip=x&srotate=180&paddingWidth=10&paddingColor=000000&format=png&quality=90");
            }

            public class When_a_configuration_exists_and_the_image_path_already_contains_a_query
            {
                Establish ctx = () => builder = new ImageUrlBuilder();

                Because of = () => result = builder.Resize(img => img.Dimensions(100, 100)).BuildUrl("http://example.com/images/image.jpg?x=123&y=456");

                It Should_preserve_the_original_query = () => result.ShouldEqual("http://example.com/images/image.jpg?x=123&y=456&width=100&height=100");
            }
        }
    }
}
