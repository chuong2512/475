/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileMapData : ISerializationCallbackReceiver
{
	[System.Serializable]
	public class Tile
	{
		public int x = 0;
		public int z = 0;
		
		public Tile (int x, int z)
		{
			this.x = x;
			this.z = z;
		}
	}
	public int height = 0;
	public List<Tile> tiles = new List<Tile> (){new Tile(0,0), new Tile(0,1), new Tile(1,0), new Tile(1,1)};

	public List<List<int>> TileMap{ get; private set; }

	public int ColumnsCount { get { return MaxX - MinX + 1; } }

	public int RowsCount { get { return MaxZ - MinZ + 1; } }

	public int MinX { get; private set; }

	public int MaxX { get; private set; }

	public int MinZ { get; private set; }

	public int MaxZ { get; private set; }

	public void OnBeforeSerialize ()
	{
		height = Mathf.Max (0, height);

		if (tiles == null || tiles.Count < 4) 
			ResetTiles ();
	}

	public void ResetTiles ()
	{
		tiles = new List<Tile> (){
			new Tile(0,0), new Tile(0,1), new Tile(0,-1),
			new Tile(1,0), new Tile(1,1), new Tile(1,-1),
			new Tile(-1,0), new Tile(-1,1), new Tile(-1,-1)
		};
		UpdateTileMapFromTiles ();
	}

	public void OnAfterDeserialize ()
	{
		UpdateTileMapFromTiles ();
	}

	public void SaveTileMapToTiles ()
	{
		tiles = new List<Tile> ();
		for (int z = 0; z < RowsCount; z++) {
			for (int x = 0; x < ColumnsCount; x++) {
				int tileValue = TileMap [z] [x];
				if (tileValue > 0)
					tiles.Add (new Tile (x + MinX, z + MinZ));
			}
		}

		UpdateTileMapFromTiles ();
	}

	private void UpdateTileMapFromTiles ()
	{
		MaxX = int.MinValue;
		MinX = int.MaxValue;
		MaxZ = int.MinValue;
		MinZ = int.MaxValue;
		
		foreach (Tile wp in tiles) {
			MaxX = Mathf.Max (wp.x, MaxX);
			MinX = Mathf.Min (wp.x, MinX);
			MaxZ = Mathf.Max (wp.z, MaxZ);
			MinZ = Mathf.Min (wp.z, MinZ);
		}
		
		MaxX++;
		MinX--;
		MaxZ++;
		MinZ--;
		
		TileMap = new List<List<int>> ();
		int numRow = MaxZ - MinZ + 1;
		int numColumn = MaxX - MinX + 1;
		for (int z = 0; z < numRow; z++) {
			List<int> row = new List<int> ();
			for (int x = 0; x < numColumn; x++)
				row.Add (0);
			TileMap.Add (row);
		}
		
		foreach (Tile tile in tiles) 
			TileMap [tile.z - MinZ] [tile.x - MinX] = 1;
	}
}
