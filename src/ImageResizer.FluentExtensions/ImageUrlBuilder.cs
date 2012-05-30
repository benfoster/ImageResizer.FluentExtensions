using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ImageResizer.FluentExtensions
{
    public class ImageUrlBuilder
    {
        private readonly NameValueCollection configuration = new NameValueCollection();
        
        public ImageUrlBuilder Resize(Action<ResizeExpression> configure)
        {
            var resize = new ResizeExpression(this);
            configure(resize);
            return this;
        }

        public ImageUrlBuilder Transform(Action<TransformExpression> configure)
        {
            var transformation = new TransformExpression(this);
            configure(transformation);
            return this;
        }

        public ImageUrlBuilder Style(Action<StyleExpression> configure)
        {
            var style = new StyleExpression(this);
            configure(style);
            return this;
        }

        public ImageUrlBuilder Output(Action<OutputExpression> configure)
        {
            var expression = new OutputExpression(this);
            configure(expression);
            return this;
        }

        public string Build(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            return BuildConfiguration(imagePath);
        }

        public ImageUrl BuildUrl(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            return new ImageUrl(BuildConfiguration(imagePath));
        }

        public void SetParameter(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName");

            if (string.IsNullOrEmpty(parameterValue))
                throw new ArgumentNullException("parameterValue");

            configuration.Set(parameterName, parameterValue);
        }

        private string BuildConfiguration(string imagePath)
        {
            if (configuration.Count == 0)
                return imagePath;

            var parameters = configuration.AllKeys
                .Select(param => string.Concat(param, "=", HttpUtility.UrlEncode(configuration[param])));

            return string.Format("{0}?{1}", imagePath, string.Join("&", parameters));
        }
    }
}
