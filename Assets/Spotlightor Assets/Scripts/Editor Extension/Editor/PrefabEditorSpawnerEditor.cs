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

[CustomEditor (typeof(PrefabEditorSpawner))]
[CanEditMultipleObjects ()]
public class PrefabEditorSpawnerEditor : Editor
{
//	private Object Prefab {
//		get { return this.serializedObject.FindProperty ("prefab").objectReferenceValue; }
//		set { this.serializedObject.FindProperty ("prefab").objectReferenceValue = value; }
//	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		foreach (Object currentTarget in targets) {
			PrefabEditorSpawner spawner = currentTarget as PrefabEditorSpawner;

			if (spawner.prefab != null) {
				int instancesCount = 0;
				for (int i = 0; i < spawner.transform.childCount; i++) {
					Transform child = spawner.transform.GetChild (i);
					if (PrefabUtility.GetPrefabParent (child.gameObject) == spawner.prefab)
						instancesCount++;
				}
				EditorGUILayout.LabelField ("Instances Count = " + instancesCount.ToString ());
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("+")) {
					GameObject instance = PrefabUtility.InstantiatePrefab (spawner.prefab) as GameObject;
					instance.transform.SetParent (spawner.transform, false);
				}
				GUILayout.EndHorizontal ();
			}
		}
	}
}
