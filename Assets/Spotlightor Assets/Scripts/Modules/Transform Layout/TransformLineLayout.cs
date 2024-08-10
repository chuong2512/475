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

public class TransformLineLayout : TransformLayout
{
	public LayoutAxis axis = LayoutAxis.X;
	public float offset = 1;
	public bool centered = false;

	protected override void UpdateLayoutForChildTransforms (List<Transform> childTransforms)
	{
		Vector3 localPosition = Vector3.zero;

		if (centered && childTransforms.Count > 1)
			localPosition.x = (float)(childTransforms.Count - 1) * -0.5f * offset;

		foreach (Transform childTransform in childTransforms) {
			Vector3 mappedPosition = MapLinePosVectorToAxis (localPosition, axis);
			if (childTransform.localPosition != mappedPosition)
				childTransform.localPosition = mappedPosition;

			localPosition.x += offset;
		}
	}

	private static Vector3 MapLinePosVectorToAxis (Vector3 linePosVector, LayoutAxis axis)
	{
		Vector3 result = linePosVector;
		if (axis == LayoutAxis.Y) {
			float temp = result.y;
			result.y = result.x;
			result.x = temp;
		} else if (axis == LayoutAxis.Z) {
			float temp = result.z;
			result.z = result.x;
			result.x = temp;
		}
		return result;
	}
}
