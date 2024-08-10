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

public static class GizmosExtended
{
	public static void DrawWireRing (Vector3 center, float radius)
	{
		DrawWireRing (center, radius, Vector3.up);
	}

	public static void DrawWireRing (Vector3 center, float radius, Vector3 up)
	{
		DrawWireRing (center, radius, up, 32);
	}

	public static void DrawWireRing (Vector3 center, float radius, Vector3 up, int segments)
	{
		Matrix4x4 preMatrix = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.FromToRotation (Vector3.up, up), Vector3.one);
		segments = Mathf.Max (segments, 4);
		float centralAngleStep = 360f / (float)segments;
		for (int i = 0; i < segments; i++) {
			float startCentralAngle = (float)i * centralAngleStep;
			float endCentralAngle = startCentralAngle + centralAngleStep;
			Vector3 startOffset = Quaternion.Euler (0, startCentralAngle, 0) * Vector3.forward * radius;
			Vector3 endOffset = Quaternion.Euler (0, endCentralAngle, 0) * Vector3.forward * radius;
			Gizmos.DrawLine (center + startOffset, center + endOffset);
		}
		Gizmos.matrix = preMatrix;
	}
}
