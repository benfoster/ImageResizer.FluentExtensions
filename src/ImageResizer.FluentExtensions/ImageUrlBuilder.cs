using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ImageResizer.FluentExtensions
{
    /// <summary>
    /// A class used to build URLs for the ImageResizer URL API
    /// </summary>
    public class ImageUrlBuilder
    {
        private readonly NameValueCollection configuration;
        private readonly List<Func<string, string>> modifiers;

        public ImageUrlBuilder()
        {
            configuration = new NameValueCollection();
            modifiers = new List<Func<string, string>>();
        }

        /// <summary>
        /// Adds a URL modifier that will be applied when the image URL is generated
        /// </summary>
        /// <exception cref="System.ArgumentNullException">If the modifier is null</exception>
        public ImageUrlBuilder AddModifier(Func<string, string> modifier)
        {
            if (modifier == null)
                throw new ArgumentNullException("modifier");

            modifiers.Add(modifier);
            return this;
        }

        /// <summary>
        /// Builds an ImageResizer image URL using the current configuration and assigned modifiers
        /// </summary>
        /// <param name="imagePath">The source path of the image</param>
        /// <returns>An image URL modified for use with the ImageResizer URL api</returns>
        public string BuildUrl(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            return ApplyConfiguration(imagePath);
        }

        /// <summary>
        /// A quick way of adding parameters to the ImageUrlBuilder configuration
        /// </summary>
        /// <param name="parameterName">The parameter name</param>
        /// <param name="parameterValue">The parameter value</param>
        /// <exception cref="System.ArgumentNullException">If the <paramref name="parameterName"/> or <paramref name="parameterValue"/> are null or empty</exception>
        /// <example><code>builder.SetParameter("width", "100");</code></example>
        public ImageUrlBuilder SetParameter(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName");

            if (string.IsNullOrEmpty(parameterValue))
                throw new ArgumentNullException("parameterValue");

            configuration.Set(parameterName, parameterValue);
            return this;
        }


        /// <summary>
        /// Applies the current configuration to <paramref name="imagePath"/>
        /// </summary>
        private string ApplyConfiguration(string imagePath)
        {
            if (configuration.Count > 0)
            {
                var parameters = configuration.AllKeys
                    .Select(param => string.Concat(param, "=", HttpUtility.UrlEncode(configuration[param])));

                imagePath = string.Format("{0}?{1}", imagePath, string.Join("&", parameters));
            }

            return GetModifiedPath(imagePath);
        }

        /// <summary>
        /// Applies the current modifiers to the generated <paramref name="path"/>
        /// </summary>
        private string GetModifiedPath(string path)
        {
            var modifiedPath = modifiers.Aggregate(path, (src, modifier) => modifier(src));
            return modifiedPath;
        }
    }
}
