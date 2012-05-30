using System;
using Machine.Specifications;

namespace ImageResizer.FluentExtensions.Tests
{
    public class ImageUrlSpecs
    {
        [Subject("Creating an image url")]
        public class Creating_an_image_url
        {
            static Exception ex;
            static ImageUrl imageUrl;
            
            public class When_the_image_path_is_null
            {
                Because of = ()
                    => ex = Catch.Exception(() => new ImageUrl(null));

                It Should_throw = ()
                    => ex.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_image_path_is_empty
            {
                Because of = ()
                    => ex = Catch.Exception(() => new ImageUrl(""));

                It Should_throw = ()
                    => ex.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_image_path_is_a_valid_string
            {
                Because of = ()
                    => imageUrl = new ImageUrl("testimage.jpg");
                
                It Should_set_the_image_path = ()
                    => imageUrl.OriginalPath.ShouldEqual("testimage.jpg");
            }
        }

        [Subject("Adding modifiers")]
        public class Adding_a_modifier
        {
            static Exception ex;
            static ImageUrl imageUrl;
            
            public class When_the_modifier_is_null
            {
                Because of = ()
                    => ex = Catch.Exception(() => new ImageUrl("testimage.jpg").AddModifier(null));

                It Should_throw = ()
                    => ex.ShouldBeOfType<ArgumentNullException>();
            }

            public class When_the_modifier_is_a_valid_delegate
            {
                Establish ctx = ()
                    => imageUrl = new ImageUrl("testimage.jpg");

                Because of = ()
                    => imageUrl.AddModifier(s => "modified-" + s);

                It Should_add_the_modifier = ()
                    => imageUrl.ToString().ShouldEqual("modified-testimage.jpg");
                    
            }
        }

        [Subject("Processing the image url")]
        public class Processing_the_image_url
        {
            static ImageUrl imageUrl;
            
            public class When_no_modifiers_exist
            {
                Because of = ()
                    => imageUrl = new ImageUrl("testimage.jpg");

                It Should_return_the_original_image_path = ()
                    => imageUrl.ToString().ShouldEqual("testimage.jpg");
            }

            public class When_modifiers_exist
            {
                Establish ctx = ()
                    => imageUrl = new ImageUrl("testimage.jpg");

                Because of = () =>
                {
                    imageUrl.AddModifier(s => "3-" + s);
                    imageUrl.AddModifier(s => "2-" + s);
                    imageUrl.AddModifier(s => "1-" + s);
                };

                It Should_process_each_modifier_in_order = ()
                    => imageUrl.ToString().ShouldEqual("1-2-3-testimage.jpg");
            }
        }
    }
}
