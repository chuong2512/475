/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;

public class TransformGridLayout : TransformLayout
{
	public TransformLayout.LayoutPlane plane = TransformLayout.LayoutPlane.XY;
	public float offsetX = 1;
	public bool centeredX = false;
	[StepperInt (1, 1, int.MaxValue)]
	public int xElementsCount = 3;
	public float offsetY = 1;
	public bool centeredY = false;

	protected override void UpdateLayoutForChildTransforms (List<Transform> childTransforms)
	{
		Vector3 localPosition = Vector3.zero;

		if (centeredX && childTransforms.Count > 1)
			localPosition.x = (float)(xElementsCount - 1) * -0.5f * offsetX;

		if (centeredY && childTransforms.Count > 1)
			localPosition.y = (float)((childTransforms.Count - 1) / xElementsCount) * -0.5f * offsetY;

		int xRowIndex = 0;
		float rowStartX = localPosition.x;
		foreach (Transform childTransform in childTransforms) {
			Vector3 mappedLocalPosition = MapXyVectorToPlane (localPosition, plane);
			if (childTransform.localPosition != mappedLocalPosition)
				childTransform.localPosition = mappedLocalPosition;

			localPosition.x += offsetX;

			xRowIndex++;
			if (xRowIndex >= xElementsCount) {
				localPosition.y += offsetY;
				localPosition.x = rowStartX;
				xRowIndex = 0;
			}
		}
	}

	private static Vector3 MapXyVectorToPlane (Vector3 xyPlaneVector, TransformLayout.LayoutPlane targetPlane)
	{
		Vector3 result = xyPlaneVector;
		if (targetPlane == LayoutPlane.XZ) {
			float temp = result.z;
			result.z = result.y;
			result.y = temp;
		} else if (targetPlane == LayoutPlane.YZ) {
			float temp = result.z;
			result.z = result.x;
			result.x = temp;
		}
		return result;
	}
}
