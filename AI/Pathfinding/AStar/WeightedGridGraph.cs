using System;
using System.Collections.Generic;
using UnityEngine;


namespace Binocle.AI.Pathfinding
{
	/// <summary>
	/// basic grid graph with support for one type of weighted node
	/// </summary>
	public class WeightedGridGraph : IWeightedGraph<Vector2>
	{
		public static readonly Vector2[] DIRS = new []
		{
			new Vector2( 1, 0 ),
			new Vector2( 0, -1 ),
			new Vector2( -1, 0 ),
			new Vector2( 0, 1 )
		};

		public HashSet<Vector2> walls = new HashSet<Vector2>();
		public HashSet<Vector2> weightedNodes = new HashSet<Vector2>();
		public int defaultWeight = 1;
		public int weightedNodeWeight = 5;

		int _width, _height;
		List<Vector2> _neighbors = new List<Vector2>( 4 );


		public WeightedGridGraph( int width, int height )
		{
			this._width = width;
			this._height = height;
		}


		/// <summary>
		/// creates a WeightedGridGraph from a TiledTileLayer. Present tile are walls and empty tiles are passable.
		/// </summary>
		/// <param name="tiledLayer">Tiled layer.</param>
        /*
		public WeightedGridGraph( TiledTileLayer tiledLayer )
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

		/// <summary>
		/// ensures the node is in the bounds of the grid graph
		/// </summary>
		/// <returns><c>true</c>, if node in bounds was ised, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		bool isNodeInBounds( Vector2 node )
		{
			return 0 <= node.x && node.x < _width && 0 <= node.y && node.y < _height;
		}


		/// <summary>
		/// checks if the node is passable. Walls are impassable.
		/// </summary>
		/// <returns><c>true</c>, if node passable was ised, <c>false</c> otherwise.</returns>
		/// <param name="node">Node.</param>
		bool isNodePassable( Vector2 node )
		{
			return !walls.Contains( node );
		}


		/// <summary>
		/// convenience shortcut for calling AStarPathfinder.search
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="goal">Goal.</param>
		public List<Vector2> search( Vector2 start, Vector2 goal )
		{
			return AStarPathfinder.search( this, start, goal );
		}


		#region IWeightedGraph implementation

		IEnumerable<Vector2> IWeightedGraph<Vector2>.getNeighbors( Vector2 node )
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


		int IWeightedGraph<Vector2>.cost( Vector2 from, Vector2 to )
		{
			return weightedNodes.Contains( to ) ? weightedNodeWeight : defaultWeight;
		}


		int IWeightedGraph<Vector2>.heuristic( Vector2 node, Vector2 goal )
		{
			return (int)(Mathf.Abs( node.x - goal.x ) + Mathf.Abs( node.y - goal.y ));
		}

		#endregion

	}
}

