using System;
using System.Collections.Generic;
using UnityEngine;
using Binocle;

namespace Binocle.TileMaps
{
	public class TileMap
	{
		public List<Layer> layers = new List<Layer>();
		public int width;
		public int height;
		public int entranceX = 0;
		public int entranceY = 0;
		public int exitX = 0;
		public int exitY = 0;
		public int tileWidth = 16;
		public int tileHeight = 16;
		public TileSet tileSet;
		public Scene Scene {
			get { return _scene; }
		}
		private Scene _scene;

		public TileMap (Scene scene, int width, int height)
		{
			this._scene = scene;
			this.width = width;
			this.height = height;
		}

		public Layer addLayer(string name)
		{
			Layer l = new Layer (this, name);
			layers.Add (l);
			return l;
		}

		public Layer getLayer(string name)
		{
			foreach (var layer in layers) {
				if (layer.name == name)
					return layer;
			}
			return null;
		}

		public int worldPositionToTilePositionX( float x )
		{
			var tileX = (int)Math.Floor( x / tileWidth );
			return Mathf.Clamp( tileX, 0, width - 1 );
		}


		public int worldPositionToTilePositionY( float y )
		{
			var tileY = (int)Math.Floor( (height*tileHeight - y) / tileHeight );
			return Mathf.Clamp( tileY, 0, height - 1 );
		}

		public int tilePositionToWorldPositionX (int x) 
		{
			var worldX = x * tileWidth;
			return Mathf.Clamp(worldX, 0, (width - 1) * tileWidth);
		}

		public int tilePositionToWorldPositionY (int y) 
		{
			var worldY =  ((height - 1) - y) * tileHeight;
			return Mathf.Clamp(worldY, 0, (height - 1) * tileHeight);
		}

		public Subtexture getTileRegion(int tileId)
		{
			return tileSet.getTileRegion(tileId);
		}

	}
}

