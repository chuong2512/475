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
using System.Collections.Generic;

[CustomEditor (typeof(Variator), true)]
[CanEditMultipleObjects ()]
public class VariatorEditor : Editor
{
	public Variator TargetVariator{ get { return target as Variator; } }

	private bool isEditingMultipleObjects = false;

	public override void OnInspectorGUI ()
	{
		isEditingMultipleObjects = serializedObject.isEditingMultipleObjects;

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Random")) {
			foreach (Object t in targets)
				(t as Variator).RandomVariation ();
			return;
		}
		if (GUILayout.Button ("Random All")) {
			foreach (Object t in targets) {
				if (t != null)
					(t as Variator).RandomAllVariators ();
			}
			return;
		}
		GUILayout.EndHorizontal ();

		base.OnInspectorGUI ();
	}

	void OnSceneGUI ()
	{
		if (TargetVariator == null || isEditingMultipleObjects)
			return;

		Handles.BeginGUI ();

		int height = 23;
		int width = 600;
		Vector2 point = Vector2.zero;
		List<Variator> allVariators = new List<Variator> (TargetVariator.gameObject.GetComponents<Variator> ());
		int index = allVariators.IndexOf (TargetVariator);
		if (index == 0) {
			GUI.color = Color.green;
			GUILayout.BeginArea (new Rect (point, new Vector2 (width, height)));
			if (GUILayout.Button ("Random All", GUILayout.Width (120))) {
				allVariators.ForEach (v => v.RandomVariation ());
				return;
			}
			GUILayout.EndArea ();
			GUI.color = Color.white;
		}
		point.y += height * (index + 1);

		GUILayout.BeginArea (new Rect (point, new Vector2 (width, height)));
		GUILayout.BeginHorizontal ();

		GUI.color = Color.green;
		Variator.Variation[] variations = TargetVariator.Variations;
		if (variations != null && variations.Length > 1 && GUILayout.Button ("Random", GUILayout.Width (60))) {
			TargetVariator.RandomVariation ();
			return;
		}

		if (variations != null) {
			GUI.color = Color.white;
			foreach (Variator.Variation variation in variations) {
				if (GUILayout.Button (variation.name, GUILayout.ExpandWidth (false))) {
					Object[] modifiedObjects = variation.GetModifiedObjects (TargetVariator.gameObject);
					if (modifiedObjects != null && modifiedObjects.Length > 0)
						Undo.RecordObjects (modifiedObjects, string.Format ("{0}", variation.GetType().Name));
					
					variation.Apply (TargetVariator.gameObject);
				}
			}
		}

		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		Handles.EndGUI ();
	}
}
