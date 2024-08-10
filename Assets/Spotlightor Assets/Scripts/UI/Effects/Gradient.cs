/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

/// Credit Breyer
/// Sourced from - http://forum.unity3d.com/threads/scripts-useful-4-6-scripts-collection.264161/#post-1780095

using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu ("UI/Effects/Extensions/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		public GradientMode gradientMode = GradientMode.Global;
		public GradientDir gradientDir = GradientDir.Vertical;
		public bool overwriteAllColor = false;
		public Color vertex1 = Color.white;
		public Color vertex2 = Color.black;

		public override void ModifyMesh (VertexHelper vh)
		{
			int count = vh.currentVertCount;
			if (!IsActive () || count == 0) {
				return;
			}
			var vertexList = new List<UIVertex> ();
			vh.GetUIVertexStream (vertexList);
			UIVertex uiVertex = new UIVertex ();
			if (gradientMode == GradientMode.Global) {
				if (gradientDir == GradientDir.DiagonalLeftToRight || gradientDir == GradientDir.DiagonalRightToLeft) {
					#if UNITY_EDITOR
					Debug.LogWarning ("Diagonal dir is not supported in Global mode");
					#endif
					gradientDir = GradientDir.Vertical;
				}
				float bottomY = gradientDir == GradientDir.Vertical ? vertexList [vertexList.Count - 1].position.y : vertexList [vertexList.Count - 1].position.x;
				float topY = gradientDir == GradientDir.Vertical ? vertexList [0].position.y : vertexList [0].position.x;

				float uiElementHeight = topY - bottomY;

				for (int i = 0; i < count; i++) {
					vh.PopulateUIVertex (ref uiVertex, i);
					if (!overwriteAllColor && uiVertex.color != GetComponent<Graphic> ().color)
						continue;
					uiVertex.color *= Color.Lerp (vertex2, vertex1, ((gradientDir == GradientDir.Vertical ? uiVertex.position.y : uiVertex.position.x) - bottomY) / uiElementHeight);
					vh.SetUIVertex (uiVertex, i);
				}
			} else {
				for (int i = 0; i < count; i++) {
					vh.PopulateUIVertex (ref uiVertex, i);
					if (!overwriteAllColor && !CompareCarefully (uiVertex.color, GetComponent<Graphic> ().color))
						continue;
					switch (gradientDir) {
					case GradientDir.Vertical:
						uiVertex.color *= (i % 4 == 0 || (i - 1) % 4 == 0) ? vertex1 : vertex2;
						break;
					case GradientDir.Horizontal:
						uiVertex.color *= (i % 4 == 0 || (i - 3) % 4 == 0) ? vertex1 : vertex2;
						break;
					case GradientDir.DiagonalLeftToRight:
						uiVertex.color *= (i % 4 == 0) ? vertex1 : ((i - 2) % 4 == 0 ? vertex2 : Color.Lerp (vertex2, vertex1, 0.5f));
						break;
					case GradientDir.DiagonalRightToLeft:
						uiVertex.color *= ((i - 1) % 4 == 0) ? vertex1 : ((i - 3) % 4 == 0 ? vertex2 : Color.Lerp (vertex2, vertex1, 0.5f));
						break;

					}
					vh.SetUIVertex (uiVertex, i);
				}
			}
		}

		private bool CompareCarefully (Color col1, Color col2)
		{
			if (Mathf.Abs (col1.r - col2.r) < 0.003f && Mathf.Abs (col1.g - col2.g) < 0.003f && Mathf.Abs (col1.b - col2.b) < 0.003f && Mathf.Abs (col1.a - col2.a) < 0.003f)
				return true;
			return false;
		}
	}

	public enum GradientMode
	{
		Global,
		Local
	}

	public enum GradientDir
	{
		Vertical,
		Horizontal,
		DiagonalLeftToRight,
		DiagonalRightToLeft
		//Free
	}
	//enum color mode Additive, Multiply, Overwrite
}
