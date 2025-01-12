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

public class ReplaceSelectionWithPrefab : ScriptableWizard
{
	private static GameObject lastSelectedPrefab;
	public GameObject prefab;
	public bool includeChildren = false;
	public bool excludePrefabs = false;
	public bool deleteSelectedObjects = true;
	public bool copyPosition = true;
	public bool copyRotation = true;
	public bool copyScale = true;
	private SelectionMode modePrefs;

	[MenuItem ("Custom/Replace Selection with Prefab %#r")]
	static void DoSet ()
	{
		ScriptableWizard.DisplayWizard ("Replace Selection with Prefab", typeof(ReplaceSelectionWithPrefab), "Replace");
	}

	void OnWizardUpdate ()
	{
		if (prefab == null && lastSelectedPrefab != null) {
			prefab = lastSelectedPrefab;
			lastSelectedPrefab = null;
		}
		
		helpString = "Duplicate the selected prefab and place it around the scene to the position of the selected objects. Tick Include Children to also place the Prefab at the position of the children of selected objects or tick Exclude Prefabs if you don't want instanced objects (from prefabs) to be changed. Tick DeleteSelectedObjects to delete the original selection after placing the prefab. The new objects are nicely placed in the same hierarchy as the old ones.";
	}

	void OnWizardCreate ()
	{
		if (includeChildren || excludePrefabs)
			modePrefs = (SelectionMode.ExcludePrefab | SelectionMode.Editable | SelectionMode.Deep);
		else
			modePrefs = (SelectionMode.Editable);

		Object[] objs = Selection.GetFiltered (typeof(GameObject), modePrefs);

		foreach (GameObject go in objs) {
			GameObject clone = PrefabUtility.InstantiatePrefab (prefab) as GameObject;

			clone.name = prefab.name;
			clone.transform.SetParent (go.transform.parent);
			if (copyPosition)
				clone.transform.localPosition = go.transform.localPosition;
            
			if (copyRotation)
				clone.transform.localRotation = go.transform.localRotation;
            
			if (copyScale)
				clone.transform.localScale = go.transform.localScale;

			Undo.RegisterCreatedObjectUndo (clone, "Replace " + go.name);
		}
		if (deleteSelectedObjects) {
			foreach (GameObject go in objs)
				Undo.DestroyObjectImmediate (go.gameObject);
		}

		lastSelectedPrefab = prefab;
	}
}
