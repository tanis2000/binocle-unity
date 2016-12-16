using System;
using System.Collections.Generic;
using UnityEngine;


namespace Binocle.AI.Pathfinding
{
	/// <summary>
	/// basic unweighted grid graph for use with the BreadthFirstPathfinder
	/// </summary>
	public class UnweightedGridGraph : IUnweightedGraph<Vector2>
	{
		static readonly Vector2[] DIRS = new []
		{
			new Vector2( 1, 0 ),
			new Vector2( 0, -1 ),
			new Vector2( -1, 0 ),
			new Vector2( 0, 1 )
		};

		public HashSet<Vector2> walls = new HashSet<Vector2>();

		int _width, _height;
		List<Vector2> _neighbors = new List<Vector2>( 4 );


		public UnweightedGridGraph( int width, int height )
		{
			this._width = width;
			this._height = height;
		}


        /*
		public UnweightedGridGraph( TiledTileLayer tiledLayer )
		{
			_width = tiledLayer.width;
			_height = tiledLayer.height;

			for( var y = 0; y < tiledLayer.tiledMap.height; y++ )
			{
				for( var x = 0; x < tiledLayer.tiledMap.width; x++ )
				{
					if( tiledLayer.getTile( x, y ) != null )
						walls.Add( new Point( x, y ) );
				}
			}
		}
        */

		public bool isNodeInBounds( Vector2 node )
		{
			return 0 <= node.x && node.x < _width && 0 <= node.y && node.y < _height;
		}


		public bool isNodePassable( Vector2 node )
		{
			return !walls.Contains( node );
		}


		IEnumerable<Vector2> IUnweightedGraph<Vector2>.getNeighbors( Vector2 node )
		{
			_neighbors.Clear();

			foreach( var dir in DIRS )
			{
				var next = new Vector2( node.x + dir.x, node.y + dir.y );
				if( isNodeInBounds( next ) && isNodePassable( next ) )
					_neighbors.Add( next );
			}

			return _neighbors;
		}
	

		/// <summary>
		/// convenience shortcut for clling BreadthFirstPathfinder.search
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="goal">Goal.</param>
		public List<Vector2> search( Vector2 start, Vector2 goal )
		{
			return BreadthFirstPathfinder.search( this, start, goal );
		}

	}
}