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

public class TransformMatrixLayout : TransformLayout
{
	public float offsetX = 1;
	public bool centeredX = false;
	public int xElementsCount = 3;
	public float offsetY = 1;
	public bool centeredY = false;
	public int yElementsCount = 3;
	public float offsetZ = 1;
	public bool centeredZ = false;

	protected override void UpdateLayoutForChildTransforms (List<Transform> childTransforms)
	{
		Vector3 localPosition = Vector3.zero;
		
		if (centeredX && childTransforms.Count > 1)
			localPosition.x = (float)(xElementsCount - 1) * -0.5f * offsetX;
		
		if (centeredY && childTransforms.Count > 1)
			localPosition.y = (float)((childTransforms.Count - 1) / xElementsCount) * -0.5f * offsetY;

		if (centeredZ && childTransforms.Count > 1)
			localPosition.z = (float)((childTransforms.Count - 1) / (xElementsCount * yElementsCount)) * -0.5f * offsetZ;
		
		int xRowIndex = 0;
		int yRowIndex = 0;
		float startX = localPosition.x;
		float startY = localPosition.y;
		foreach (Transform childTransform in childTransforms) {
			if (childTransform.localPosition != localPosition)
				childTransform.localPosition = localPosition;
			
			localPosition.x += offsetX;
			
			xRowIndex++;
			if (xRowIndex >= xElementsCount) {
				localPosition.y += offsetY;
				localPosition.x = startX;
				xRowIndex = 0;

				yRowIndex++;
				if (yRowIndex >= yElementsCount) {
					localPosition.z += offsetZ;
					localPosition.y = startY;
					yRowIndex = 0;
				}
			}
		}
	}
}
