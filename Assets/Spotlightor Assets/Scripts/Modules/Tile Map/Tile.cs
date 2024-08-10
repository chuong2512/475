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

namespace Spotlightor.TileMap
{
	public abstract class Tile : MonoBehaviour
	{
		public List<Vector3> bonusTileMapDatas = new List<Vector3> ();

		public List<Vector3> TileMapDatas {
			get {
				List<Vector3> tileMapDatas = new List<Vector3> (){ new Vector3 (0, 0, 0) };
				foreach (Vector3 bonusTileMapData in bonusTileMapDatas)
					tileMapDatas.Add (transform.localRotation * bonusTileMapData);
				
				return tileMapDatas;
			}
		}

		public abstract TileTypes TileType {
			get;
			set;
		}

		public abstract int WallCount {
			get;
			set;
		}
	}
}
