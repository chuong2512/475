/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

public class TransformFormatter
{
	[MenuItem ("GameObject/Snap Position &p", false, 210)]
	static void SnapPosition ()
	{
		SnapPositionByUnit (1f);
	}

	[MenuItem ("GameObject/Snap Position 0.5 &#p", false, 210)]
	static void SnapPositionPoint5 ()
	{
		SnapPositionByUnit (0.5f);
	}

	static void SnapPositionByUnit (float unit)
	{
		Transform[] selectedTransforms = Selection.transforms;
		Undo.RecordObjects (selectedTransforms, "Snap local position");
		foreach (Transform transform in selectedTransforms) {
			Vector3 snappedPos = transform.localPosition;
			snappedPos.x = Mathf.RoundToInt (snappedPos.x / unit) * unit;
			snappedPos.y = Mathf.RoundToInt (snappedPos.y / unit) * unit;
			snappedPos.z = Mathf.RoundToInt (snappedPos.z / unit) * unit;
			transform.localPosition = snappedPos;
		}
	}

	[MenuItem ("GameObject/Snap Rotation &r", false, 211)]
	static void SnapRotation ()
	{
		Transform[] selectedTransforms = Selection.transforms;
		Undo.RecordObjects (selectedTransforms, "Snap local rotation");
		float snapStep = 15f;
		foreach (Transform transform in selectedTransforms) {
			Vector3 snappedRot = transform.localEulerAngles;
			snappedRot.x = Mathf.RoundToInt (snappedRot.x / snapStep) * snapStep;
			snappedRot.y = Mathf.RoundToInt (snappedRot.y / snapStep) * snapStep;
			snappedRot.z = Mathf.RoundToInt (snappedRot.z / snapStep) * snapStep;
			transform.localEulerAngles = snappedRot;
		}
	}

	[MenuItem ("GameObject/Snap Scale &s", false, 212)]
	static void SnapScale ()
	{
		Transform[] selectedTransforms = Selection.transforms;
		
		Undo.RecordObjects (selectedTransforms, "Snap local scale");
		foreach (Transform transform in selectedTransforms) {
			Vector3 snappedScale = transform.localScale;
			snappedScale.x = Mathf.RoundToInt (snappedScale.x);
			snappedScale.y = Mathf.RoundToInt (snappedScale.y);
			snappedScale.z = Mathf.RoundToInt (snappedScale.z);
			transform.localScale = snappedScale;
		}
	}

	[MenuItem ("GameObject/Clear Pos+Rot+Scale &0", false, 213)]
	static void ClearTransform ()
	{
		Transform[] selectedTransforms = Selection.transforms;
		
		Undo.RecordObjects (selectedTransforms, "Clear Local Transform");
		foreach (Transform transform in selectedTransforms) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}

	[MenuItem ("GameObject/Clear Pos+rot &#0", false, 214)]
	static void Clear ()
	{
		Transform[] selectedTransforms = Selection.transforms;

		Undo.RecordObjects (selectedTransforms, "Clear Local Transform");
		foreach (Transform transform in selectedTransforms) {
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}
	}
}
