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
	public class TileSet : ScriptableObject
	{
		[System.Serializable]
		public class Element
		{
			public TileMesh top;
			public Vector3 topRotation = Vector3.zero;
			public TileMesh wall;
			public Vector3 wallRotation = Vector3.zero;
			public TileMesh bottom;
			public Vector3 bottomRotation = Vector3.zero;
		}

		public List<Element> centers;
		[Header ("Edge")]
		public List<Element> edgesLeft;
		public List<Element> edgesRight;
		public List<Element> edgesFront;
		public List<Element> edgesBack;
		[Header ("Corner")]
		public List<Element> cornersBL;
		public List<Element> cornersBR;
		public List<Element> cornersFL;
		public List<Element> cornersFR;
		[Header ("Negative Corner")]
		public List<Element> negativeCornersBL;
		public List<Element> negativeCornersBR;
		public List<Element> negativeCornersFL;
		public List<Element> negativeCornersFR;

		public List<Element> GetElementsByTileType (TileTypes tileType)
		{
			List<Element> result = centers;
			if (tileType == TileTypes.EdgeLeft)
				result = edgesLeft;
			else if (tileType == TileTypes.EdgeRight)
				result = edgesRight;
			else if (tileType == TileTypes.EdgeFront)
				result = edgesFront;
			else if (tileType == TileTypes.EdgeBack)
				result = edgesBack;
			else if (tileType == TileTypes.CornerBL)
				result = cornersBL;
			else if (tileType == TileTypes.CornerBR)
				result = cornersBR;
			else if (tileType == TileTypes.CornerFL)
				result = cornersFL;
			else if (tileType == TileTypes.CornerFR)
				result = cornersFR;
			else if (tileType == TileTypes.NegativeCornerBL)
				result = negativeCornersBL;
			else if (tileType == TileTypes.NegativeCornerBR)
				result = negativeCornersBR;
			else if (tileType == TileTypes.NegativeCornerFL)
				result = negativeCornersFL;
			else if (tileType == TileTypes.NegativeCornerFR)
				result = negativeCornersFR;

			return result;
		}
	}
}
