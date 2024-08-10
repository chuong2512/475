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

public static class MeshUtility {

	public static void Tint(Mesh mesh, Color color)
	{
		Color32 color32 = color;
		Color32[] colors32 = mesh.colors32;
		if (colors32.Length != mesh.vertexCount)
			colors32 = new Color32[mesh.vertexCount];
		for (int i = 0; i  < colors32.Length; i++)
			colors32 [i] = color32;
		
		mesh.colors32 = colors32;
	}
}
