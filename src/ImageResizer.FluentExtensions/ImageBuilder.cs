using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace ImageResizer.FluentExtensions
{
    public class ImageBuilder
    {
        private readonly NameValueCollection configuration = new NameValueCollection();
        
        public ImageBuilder Resize(Action<ResizeExpression> configure)
        {
            var resize = new ResizeExpression(this);
            configure(resize);
            return this;
        }

        public ImageBuilder Transform(Action<TransformExpression> configure)
        {
            var transformation = new TransformExpression(this);
            configure(transformation);
            return this;
        }

        public ImageBuilder Style(Action<StyleExpression> configure)
        {
            var style = new StyleExpression(this);
            configure(style);
            return this;
        }

        public ImageBuilder Output(Action<OutputExpression> configure)
        {
            var expression = new OutputExpression(this);
            configure(expression);
            return this;
        }

        public string Build(string srcImageName)
        {
            if (string.IsNullOrEmpty(srcImageName))
                throw new ArgumentNullException("srcImageName");

            if (configuration.Count == 0)
                return srcImageName;

            var parameters = configuration.AllKeys
                .Select(param => string.Concat(param, "=", HttpUtility.UrlEncode(configuration[param])));

            return string.Format("{0}?{1}", srcImageName, string.Join("&", parameters));
        }

        public void SetParameter(string parameterName, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName");

            if (string.IsNullOrEmpty(parameterValue))
                throw new ArgumentNullException("parameterValue");

            configuration.Set(parameterName, parameterValue);
        }
    }
}
