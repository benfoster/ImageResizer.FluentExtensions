using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageResizer.FluentExtensions
{
    public class ImageUrl
    {
        private readonly List<Func<string, string>> modifiers;
        private readonly string imagePath;
        private string modifiedPath;

        public string OriginalPath
        {
            get { return imagePath; }
        }
        
        public ImageUrl(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentNullException("imagePath");

            this.imagePath = imagePath;
            this.modifiers = new List<Func<string, string>>();
        }

        public ImageUrl AddModifier(Func<string, string> modifier)
        {
            if (modifier == null)
                throw new ArgumentNullException("modifier");

            modifiers.Add(modifier);
            ResetPath();

            return this;
        }

        public override string ToString()
        {
            return GetModifiedPath();
        }

        private string GetModifiedPath()
        {
            if (string.IsNullOrEmpty(modifiedPath))
            {
                modifiedPath =
                    modifiers.Aggregate(imagePath, (src, modifier) => modifier(src));
            }

            return modifiedPath.ToLowerInvariant();
        }
        
        private void ResetPath()
        {
            modifiedPath = null;
        }
    }
}
