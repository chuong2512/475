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
using UnityEditor;

[CustomEditor (typeof(ChildrenTransformer))]
public class ChildrenTransformerEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		ChildrenTransformer transformer = target as ChildrenTransformer;

		List<float> angles = new List<float> (){ 180, 90, -90, 45, -45 };

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("旋转子物体");
		foreach (float angle in angles) {
			if (GUILayout.Button (angle.ToString ()))
				RotateChildren (Vector3.up, angle);
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("旋转中心点");
		foreach (float angle in angles) {
			if (GUILayout.Button (angle.ToString ())) {
				RotateChildren (Vector3.up, -angle);

				Undo.RecordObject (transformer.transform, "Rotate Pivot");
				transformer.transform.Rotate (Vector3.up, angle);
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.Space (10);
		List<Vector3> movements = new List<Vector3> () {
			transformer.transform.TransformVector (Vector3.left),
			transformer.transform.TransformVector (Vector3.forward),
			transformer.transform.TransformVector (Vector3.back),
			transformer.transform.TransformVector (Vector3.right),
			transformer.transform.TransformVector (Vector3.up),
			transformer.transform.TransformVector (Vector3.down)
		};

		DrawDpadMovementGui ("平移子物体", movements, false);
		GUILayout.Space (10);
		DrawDpadMovementGui ("平移中心点", movements, true);
	}

	private void RotateChildren (Vector3 axis, float angle)
	{
		Transform targetTransform = (target as ChildrenTransformer).transform;
		List<Transform> children = GetChildrenToTransform ();
		Undo.RecordObjects (children.ToArray (), "Rotate Children");
		children.ForEach (c => c.RotateAround (targetTransform.position, axis, angle));
	}

	private void DrawDpadMovementGui (string label, List<Vector3> movements, bool movePivotOnly)
	{
		ChildrenTransformer transformer = target as ChildrenTransformer;
		if (movePivotOnly) {
			for (int i = 0; i < movements.Count; i++)
				movements [i] *= -1;
		}

		int buttonWidth = 30;
		GUILayout.Label (label);
		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.BeginHorizontal (GUILayout.Width (buttonWidth * 3));

		EditorGUILayout.BeginVertical ();
		GUILayout.Label ("");
		if (GUILayout.Button ("←")) {
			MoveChildren (movements [0]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [0];
			}
		}
		GUILayout.Label ("");
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ();
		if (GUILayout.Button ("↑")) {
			MoveChildren (movements [1]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [1];
			}
		}
		GUILayout.Label ("水平");
		if (GUILayout.Button ("↓")) {
			MoveChildren (movements [2]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [2];
			}
		}
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ();
		GUILayout.Label ("");
		if (GUILayout.Button ("→")) {
			MoveChildren (movements [3]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [3];
			}
		}
		GUILayout.Label ("");
		EditorGUILayout.EndVertical ();

		EditorGUILayout.EndHorizontal ();

		GUILayout.Space (20);

		EditorGUILayout.BeginHorizontal (GUILayout.Width (buttonWidth));
		EditorGUILayout.BeginVertical ();
		if (GUILayout.Button ("↑")) {
			MoveChildren (movements [4]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [4];
			}
		}
		GUILayout.Label ("垂直");
		if (GUILayout.Button ("↓")) {
			MoveChildren (movements [5]);
			if (movePivotOnly) {
				Undo.RecordObject (transformer.transform, "Move Pivot");
				transformer.transform.position -= movements [5];
			}
		}
		EditorGUILayout.EndVertical ();
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.EndHorizontal ();
	}

	private void MoveChildren (Vector3 movement)
	{
		List<Transform> children = GetChildrenToTransform ();
		Undo.RecordObjects (children.ToArray (), "Move Children");
		children.ForEach (c => c.position += movement);
	}

	private List<Transform> GetChildrenToTransform ()
	{
		Transform targetTransform = (target as ChildrenTransformer).transform;
		int childrenDepth = (target as ChildrenTransformer).childrenDepth;

		int currentChildrenDepth = 0;
		List<Transform> children = new List<Transform> (){ targetTransform };
		do {
			List<Transform> newChildren = new List<Transform> ();
			foreach (Transform parent in children) {
				for (int i = 0; i < parent.childCount; i++)
					newChildren.Add (parent.GetChild (i));
			}
			children = newChildren;
			currentChildrenDepth++;
		} while(currentChildrenDepth <= childrenDepth);
		return children;
	}
}
