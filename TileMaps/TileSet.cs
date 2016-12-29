using System;
using System.Collections.Generic;
using UnityEngine;
using Binocle;

namespace Binocle.TileMaps
{
	public class TileSet
	{
		private readonly Dictionary<int,Subtexture> _regions;

		public Texture2D Texture { get; private set; }
		public int TileWidth { get; private set; }
		public int TileHeight { get; private set; }

		private readonly Dictionary<int, Sprite> _sprites;


		public TileSet( Texture2D texture, int tileWidth, int tileHeight)
		{
			this.Texture = texture;
			this.TileWidth = tileWidth;
			this.TileHeight = tileHeight;

			_regions = new Dictionary<int,Subtexture>();
			_sprites = new Dictionary<int, Sprite>();

			int id = 0;

			for( var y = texture.height-tileHeight; y >= 0 ; y -= tileHeight )
			{
				for( var x = 0; x < texture.width ; x += tileWidth )
				{
					Subtexture sub = new Subtexture( texture, x, y, tileWidth, tileHeight );
					_regions.Add( id, sub );
                    Sprite sprite = Sprite.Create(texture, sub.sourceRect, new Vector2(0.5f, 0.5f), 1);
					_sprites.Add(id, sprite);
					id++;
				}
			}
		}

		public TileSet(string tileSetName, int tileWidth, int tileHeight) 
		{
			this.TileWidth = tileWidth;
			this.TileHeight = tileHeight;

			_sprites = new Dictionary<int, Sprite>();

			int id = 0;

			System.Object [] sprites;
			sprites = Resources.LoadAll(tileSetName);
			for (int i = 1 ; i < sprites.Length ; i++) {
				_sprites.Add(id, (Sprite)sprites[i]);
				id++;
			}
		}

		public Subtexture getTileRegion(int id)
		{
			return _regions[(int)id];
		}

		public Sprite GetSprite(int id)
		{
			return _sprites[(int)id];
		}

	}
}

