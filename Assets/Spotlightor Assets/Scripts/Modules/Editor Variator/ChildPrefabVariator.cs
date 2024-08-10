/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

public class ChildPrefabVariator : Variator
{
	[System.Serializable]
	public class ChildPrefabVariation : Variation
	{
		public GameObject childPrefab;
		public bool exclusive = true;
		public bool autoSelect = false;

		public override void Apply (GameObject target)
		{
			#if UNITY_EDITOR
			if (exclusive) {
				int i = 0;
				ChildPrefabVariator variatorInstance = target.GetComponent<ChildPrefabVariator> ();
				while (i < target.transform.childCount) {
					GameObject childGo = target.transform.GetChild (i).gameObject;
					if (variatorInstance.IsInstanceFromVariations (childGo) && PrefabUtility.GetPrefabParent (childGo) != childPrefab) {
						Undo.DestroyObjectImmediate (childGo);
					} else
						i++;
				}
			}
			for (int j = 0; j < target.transform.childCount; j++) {
				GameObject childGo = target.transform.GetChild (j).gameObject;
				if (PrefabUtility.GetPrefabParent (childGo) == childPrefab) {
					Undo.DestroyObjectImmediate (childGo);
					return;
				}
			}

			if (childPrefab != null) {
				GameObject childInstance = PrefabUtility.InstantiatePrefab (childPrefab) as GameObject;
				
				childInstance.transform.SetParent (target.transform, false);
				childInstance.transform.localPosition = childPrefab.transform.localPosition;
				childInstance.transform.localRotation = childPrefab.transform.localRotation;
				childInstance.transform.localScale = childPrefab.transform.localScale;

				Undo.RegisterCreatedObjectUndo (childInstance, "Instantiate " + childInstance.name);

				if (autoSelect)
					Selection.activeGameObject = childInstance;
			}
			#endif
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			return null;
		}
	}

	public ChildPrefabVariation[] variations;

	public override Variation[] Variations { get { return variations; } }

	private bool IsInstanceFromVariations (GameObject go)
	{
		bool result = false;
#if UNITY_EDITOR
		foreach (ChildPrefabVariation variation in variations) {
			if (variation.childPrefab != null && PrefabUtility.GetPrefabParent (go) == variation.childPrefab) {
				result = true;
				break;
			}
		}
#endif
		return result;
	}
}
