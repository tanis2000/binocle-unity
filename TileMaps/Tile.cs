using System;

namespace Binocle.TileMaps
{
	public class Tile
	{
		public int TileId;
		public Layer Layer;
		public int x = 0;
		public int y = 0;

		public Tile (Layer layer)
		{
			this.Layer = layer;
		}
	}
}

