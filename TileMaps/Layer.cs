using System;
using System.Collections.Generic;
using UnityEngine;
using Binocle;

namespace Binocle.TileMaps
{
	public class Layer
	{
		public Tile[] tiles;
		public TileMap mapLevel;
		public string name;
		public bool visible = true;

		public Layer (TileMap mapLevel, string name)
		{
			this.mapLevel = mapLevel;
			this.name = name;
			tiles = new Tile[mapLevel.width * mapLevel.height];
		}

		public Tile addTile (int x, int y, int tileId)
		{
			Tile tile = new Tile (this);
			tile.TileId = tileId;
			tile.x = x;
			tile.y = y;
			tiles [getIndex (x, y)] = tile;
			return tile;
		}

		public int getIndex (int x, int y)
		{
			return mapLevel.width * y + x;
		}

		public Tile getTile (int x, int y)
		{
			return tiles [x + y * mapLevel.width];
		}

		public static Rect getBoundsForTile (Tile tile, TileMap tilemap)
		{
			return new Rect (tile.x * tilemap.tileWidth, tile.y * tilemap.tileHeight, tilemap.tileWidth, tilemap.tileHeight);
		}


		public Tile getTileAtWorldPosition (Vector2 pos)
		{
			return getTile (mapLevel.worldPositionToTilePositionX (pos.x), mapLevel.worldPositionToTilePositionY (pos.y));
		}

		public List<Rect> getCollisionRectangles ()
		{
			var checkedIndexes = new bool?[tiles.Length];
			var rectangles = new List<Rect> ();
			var startCol = -1;
			var index = -1;

			for (var y = 0; y < mapLevel.height; y++) {
				for (var x = 0; x < mapLevel.width; x++) {
					index = y * mapLevel.width + x;
					var tile = getTile (x, y);

					if (tile != null && (checkedIndexes [index] == false || checkedIndexes [index] == null)) {
						if (startCol < 0)
							startCol = x;

						checkedIndexes [index] = true;
					} else if (tile == null || checkedIndexes [index] == true) {
						if (startCol >= 0) {
							rectangles.Add (findBoundsRect (startCol, x, y, checkedIndexes));
							startCol = -1;
						}
					}
				} // end for x

				if (startCol >= 0) {
					rectangles.Add (findBoundsRect (startCol, mapLevel.width, y, checkedIndexes));
					startCol = -1;
				}
			}

			return rectangles;
		}


		public Rect findBoundsRect (int startX, int endX, int startY, bool?[] checkedIndexes)
		{
			var index = -1;

			for (var y = startY + 1; y < mapLevel.height; y++) {
				for (var x = startX; x < endX; x++) {
					index = y * mapLevel.width + x;
					var tile = getTile (x, y);

					if (tile == null || checkedIndexes [index] == true) {
						// Set everything we've visited so far in this row to false again because it won't be included in the rectangle and should be checked again
						for (var _x = startX; _x < x; _x++) {
							index = y * mapLevel.width + _x;
							checkedIndexes [index] = false;
						}

						return new Rect (startX * mapLevel.tileWidth, startY * mapLevel.tileHeight, (endX - startX) * mapLevel.tileWidth, (y - startY) * mapLevel.tileHeight);
					}

					checkedIndexes [index] = true;
				}
			}

			return new Rect (startX * mapLevel.tileWidth, startY * mapLevel.tileHeight, (endX - startX) * mapLevel.tileWidth, (mapLevel.height - startY) * mapLevel.tileHeight);
		}


		public List<Tile> getTilesIntersectingBounds (Rect bounds)
		{
			var minX = mapLevel.worldPositionToTilePositionX (bounds.xMin);
			var minY = mapLevel.worldPositionToTilePositionY (bounds.yMin);
			var maxX = mapLevel.worldPositionToTilePositionX (bounds.xMax);
			var maxY = mapLevel.worldPositionToTilePositionY (bounds.yMax);

			var tilelist = new List<Tile> ();

			for (var x = minX; x <= maxX; x++) {
				for (var y = minY; y <= maxY; y++) {
					var tile = getTile (x, y);
					if (tile != null)
						tilelist.Add (tile);
				}
			}

			return tilelist;
		}

		public void CreateTiles (Entity parent, float zIndex, string sortingLayer, string collisionLayer = null)
		{
			var l = mapLevel.Scene.CreateEntity(name);
			l.SetParent (parent);
			l.GameObject.transform.localPosition = new Vector3 (0, 0, zIndex);
			for (int y = 0; y < mapLevel.height; y++) {
				for (int x = 0; x < mapLevel.width; x++) {
					var t = getTile (x, y);
					if (t == null)
						continue;
					Entity tile = mapLevel.Scene.CreateEntity ("tile-" + x + "-" + y);
					tile.SetParent (l);
					tile.GameObject.transform.localPosition = new Vector2 (x * 16, ((mapLevel.height - 1) - y) * 16);
					if (collisionLayer != null) {
						tile.GameObject.layer = LayerMask.NameToLayer (collisionLayer);
						var coll = tile.AddComponent<BoxCollider2D> ();
						coll.size = new Vector2 (16, 16);
						coll.offset = new Vector2(8, 8);
					}
					var sr = tile.AddComponent<SpriteRenderer> ();
					sr.sortingLayerName = sortingLayer;
					sr.sprite = mapLevel.tileSet.GetSprite (t.TileId);
				}
			}
		}

	}
}

