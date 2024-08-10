/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBoxesTubeCollider : TransformLayout
{
	public Vector3 center = Vector3.zero;
	public float radius = 2;
	public float holeRadius = 1;
	public float height = 3;

	protected override void UpdateLayoutForChildTransforms (List<Transform> childTransforms)
	{
		List<BoxCollider> boxColliders = new List<BoxCollider> ();
		foreach (Transform child in childTransforms) {
			BoxCollider boxCollider = child.GetComponent<BoxCollider> ();
			if (boxCollider != null)
				boxColliders.Add (boxCollider);
		}

		float childDeltaAngle = 0;
		int sides = boxColliders.Count * 2;
		if (boxColliders.Count > 1)
			childDeltaAngle = 180 / (float)sides;

		float centreAngle = 0;
		Vector3 sideBoxSize = new Vector3 (radius * Mathf.Sin (childDeltaAngle * 0.5f * Mathf.Deg2Rad) * 2, height, radius * Mathf.Cos (childDeltaAngle * 0.5f * Mathf.Deg2Rad) * 2);
		for (int i = 0; i < boxColliders.Count; i++) {
			BoxCollider box = boxColliders [i];

			box.transform.localPosition = center;
			box.transform.localEulerAngles = new Vector3 (0, centreAngle, 0);

			float pieceExtend = sideBoxSize.z * 0.5f - holeRadius;
			float pieceOffset = pieceExtend * 0.5f + holeRadius;

			BoxCollider[] allBoxes = box.GetComponents<BoxCollider> ();
			for (int j = 0; j < allBoxes.Length; j++) {
				if (j == 0) {
					allBoxes [j].size = new Vector3 (sideBoxSize.x, sideBoxSize.y, pieceExtend);
					allBoxes [j].center = new Vector3 (0, 0, pieceOffset);
				} else if (j == 1) {
					allBoxes [j].size = new Vector3 (sideBoxSize.x, sideBoxSize.y, pieceExtend);
					allBoxes [j].center = new Vector3 (0, 0, -pieceOffset);
				} else if (j == 2) {
					allBoxes [j].size = new Vector3 (pieceExtend, sideBoxSize.y, sideBoxSize.x);
					allBoxes [j].center = new Vector3 (pieceOffset, 0, 0);
				} else if (j == 3) {
					allBoxes [j].size = new Vector3 (pieceExtend, sideBoxSize.y, sideBoxSize.x);
					allBoxes [j].center = new Vector3 (-pieceOffset, 0, 0);
				}
			}
			centreAngle += childDeltaAngle;
		}
	}
}
