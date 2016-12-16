using UnityEngine;
using System.Collections.Generic;
using Binocle.Importers;

namespace Binocle.Atlases
{
    public class TexturePackerAtlas
    {
        public Texture2D texture;
        public readonly List<Subtexture> subtextures;

        /// <summary>
        /// maps actual image names to the index in the subtextures list
        /// </summary>
        readonly Dictionary<string, int> _subtextureMap;


        public TexturePackerAtlas(Texture2D texture)
        {
            this.texture = texture;
            subtextures = new List<Subtexture>();
            _subtextureMap = new Dictionary<string, int>();
        }


        public static TexturePackerAtlas create(Texture2D texture, int regionWidth, int regionHeight, int maxRegionCount = int.MaxValue, int margin = 0, int spacing = 0)
        {
            var textureAtlas = new TexturePackerAtlas(texture);
            var count = 0;
            var width = texture.width - margin;
            var height = texture.height - margin;
            var xIncrement = regionWidth + spacing;
            var yIncrement = regionHeight + spacing;

            for (var y = margin; y < height; y += yIncrement)
            {
                for (var x = margin; x < width; x += xIncrement)
                {
                    var regionName = string.Format("{0}{1}", texture.name ?? "region", count);
                    textureAtlas.createRegion(regionName, x, y, regionWidth, regionHeight);
                    count++;

                    if (count >= maxRegionCount)
                        return textureAtlas;
                }
            }

            return textureAtlas;
        }

        public static TexturePackerAtlas create(TexturePackerFile f, string folder)
        {
            Texture2D texture = Resources.Load(folder + "/" + Utils.RemoveFileExtension(f.meta.image), typeof(Texture2D)) as Texture2D;
            TexturePackerAtlas atlas = new TexturePackerAtlas(texture);
            for (var i = 0; i < f.frames.Count; i++)
            {
                atlas.createRegion
                (
                    name: f.frames[i].filename,
                    x: f.frames[i].frame.x,
                    y: f.frames[i].frame.y,
                    width: f.frames[i].frame.w,
                    height: f.frames[i].frame.h
                );
            }
            return atlas;
        }


        public Subtexture createRegion(string name, int x, int y, int width, int height)
        {
            if (_subtextureMap.ContainsKey(name))
                throw new UnityException("Region {0} already exists in the texture atlas");

            var region = new Subtexture(texture, x, y, width, height);
            var index = subtextures.Count;
            subtextures.Add(region);
            _subtextureMap.Add(name, index);

            return region;
        }


        public void removeSubtexture(int index)
        {
            subtextures.RemoveAt(index);
        }


        public void removeSubtexture(string name)
        {
            int index;

            if (_subtextureMap.TryGetValue(name, out index))
            {
                removeSubtexture(index);
                _subtextureMap.Remove(name);
            }
        }


        public Subtexture getSubtexture(int index)
        {
            if (index < 0 || index >= subtextures.Count)
                throw new UnityException("Index out of range");

            return subtextures[index];
        }


        public Subtexture getSubtexture(string name)
        {
            int index;

            if (_subtextureMap.TryGetValue(name, out index))
                return getSubtexture(index);

            throw new KeyNotFoundException(name);
        }


        public Subtexture this[string name]
        {
            get { return getSubtexture(name); }
        }


        public Subtexture this[int index]
        {
            get { return getSubtexture(index); }
        }

    }
}