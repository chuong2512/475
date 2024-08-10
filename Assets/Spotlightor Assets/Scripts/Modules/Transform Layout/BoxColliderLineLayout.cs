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

public class BoxColliderLineLayout : TransformLayout
{
	public LayoutAxis axis = LayoutAxis.Y;
	public bool alignByColliderCenter = true;
	public bool inverse = false;

	protected override void UpdateLayoutForChildTransforms (List<Transform> childTransforms)
	{
		Vector3 localPos = Vector3.zero;
		foreach (Transform child in childTransforms) {
			BoxCollider boxCollider = child.GetComponent<BoxCollider> ();
			if (boxCollider != null) {
				Vector3 axisOffset = Vector3.zero;
				if (axis == LayoutAxis.X)
					axisOffset.x = boxCollider.size.x;
				else if (axis == LayoutAxis.Y)
					axisOffset.y = boxCollider.size.y;
				else if (axis == LayoutAxis.Z)
					axisOffset.z = boxCollider.size.z;

				if (inverse)
					axisOffset = -axisOffset;

				if (alignByColliderCenter)
					child.localPosition = localPos - boxCollider.center + 0.5f * axisOffset;
				else
					child.localPosition = localPos;

				localPos += axisOffset;
			}
		}
	}
}
