using UnityEngine;
using System.Collections.Generic;

namespace Binocle
{
    /// <summary>
    /// represents a single element in a texture atlas consisting of a texture and the source rectangle for the frame
    /// </summary>
    public class Subtexture
    {
        /// <summary>
        /// the actual Texture2D
        /// </summary>
        public Texture2D texture2D;

        /// <summary>
        /// rectangle in the Texture2D for this element
        /// </summary>
        public Rect sourceRect;

        /// <summary>
        /// center of the sourceRect if it had a 0,0 origin. This is basically the center in sourceRect-space.
        /// </summary>
        /// <value>The center.</value>
        public Vector2 center;


        public Subtexture(Texture2D texture, Rect sourceRect)
        {
            this.texture2D = texture;
            this.sourceRect = sourceRect;
            center = sourceRect.size * 0.5f;
        }


        public Subtexture(Texture2D texture) : this(texture, new Rect(0, 0, texture.width, texture.height))
        {
        }


        public Subtexture(Texture2D texture, int x, int y, int width, int height) : this(texture, new Rect(x, y, width, height))
        {
        }


        /// <summary>
        /// convenience constructor that casts floats to ints for the sourceRect
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Subtexture(Texture2D texture, float x, float y, float width, float height) : this(texture, (int)x, (int)y, (int)width, (int)height)
        {
        }


        public static List<Subtexture> subtexturesFromAtlas(Texture2D texture, int cellWidth, int cellHeight)
        {
            var subtextures = new List<Subtexture>();

            var cols = texture.width / cellWidth;
            var rows = texture.height / cellHeight;

            for (var y = 0; y < rows; y++)
            {
                for (var x = 0; x < cols; x++)
                {
                    subtextures.Add(new Subtexture(texture, new Rect(x * cellWidth, y * cellHeight, cellWidth, cellHeight)));
                }
            }

            return subtextures;
        }


        public virtual void unload()
        {
            texture2D = null;
        }


        public static implicit operator Texture2D(Subtexture tex)
        {
            return tex.texture2D;
        }


        public override string ToString()
        {
            return string.Format("{0}", sourceRect);
        }

    }
}