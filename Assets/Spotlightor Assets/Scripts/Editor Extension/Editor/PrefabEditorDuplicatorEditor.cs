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

[CustomEditor (typeof(PrefabEditorDuplicator), true)]
public class PrefabEditorDuplicatorEditor : Editor
{
	private PrefabEditorDuplicator Duplicator{ get { return target as PrefabEditorDuplicator; } }

	void OnSceneGUI ()
	{
		if (Duplicator == null || !Duplicator.enabled)
			return;

		GameObject targetPrefab = PrefabUtility.GetPrefabParent (Duplicator.gameObject) as GameObject;
		if (targetPrefab != null) {
			if (Duplicator.duplicateWaypoints != null) {
				foreach (PrefabEditorDuplicator.DuplicateWaypoint waypoint in Duplicator.duplicateWaypoints)
					DrawInsertNewPartButton (targetPrefab, waypoint);
			}
		}
	}

	private bool DrawInsertNewPartButton (GameObject prefab, PrefabEditorDuplicator.DuplicateWaypoint waypoint)
	{
		if (prefab != null)
			Handles.color = new Color (0, 1, 0, 0.5f);

		Vector3 buttonPos = Duplicator.transform.TransformPoint (waypoint.position);
		Quaternion buttonRot = Duplicator.transform.rotation * Quaternion.Euler (waypoint.rotation);
		buttonPos += buttonRot * Duplicator.buttonOffset;

		if (Handles.Button (buttonPos, buttonRot, Duplicator.buttonSize, Duplicator.buttonSize, Handles.CubeHandleCap)) {
			InstantiatePrefabAt (prefab, waypoint);
			return true;
		} else
			return false;
	}

	private GameObject InstantiatePrefabAt (GameObject prefab, PrefabEditorDuplicator.DuplicateWaypoint waypoint)
	{
		GameObject instance = PrefabUtility.InstantiatePrefab (prefab) as GameObject;

		instance.transform.SetParent (Duplicator.transform.parent, false);
		instance.transform.SetSiblingIndex (Duplicator.transform.GetSiblingIndex () + 1);
		instance.transform.rotation = Duplicator.transform.rotation * Quaternion.Euler (waypoint.rotation);
		instance.transform.position = Duplicator.transform.TransformPoint (waypoint.position);

		Selection.activeGameObject = instance;

		#if UNITY_5
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
		#else
		EditorApplication.MarkSceneDirty ();
		#endif

		return instance;
	}
}
