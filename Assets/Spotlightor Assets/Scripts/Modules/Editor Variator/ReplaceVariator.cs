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
using System;

public class ReplaceVariator : Variator
{
	[System.Serializable]
	public class ReplaceVariation : Variation
	{
		public GameObject prefab;

		public override void Apply (GameObject target)
		{
#if UNITY_EDITOR
			if (target != prefab) {
				GameObject replacer = PrefabUtility.InstantiatePrefab (prefab) as GameObject;

				replacer.transform.SetParent (target.transform.parent);
				replacer.transform.CopyPositionRotation (target.transform);
				replacer.transform.localScale = target.transform.localScale;
				replacer.transform.SetSiblingIndex (target.transform.GetSiblingIndex ());

				int selectionIndex = Array.IndexOf (Selection.gameObjects, target);
				if (selectionIndex >= 0) {
					GameObject[] gos = Selection.gameObjects;
					gos [selectionIndex] = replacer;
					Selection.objects = gos;
				}

				DestroyImmediate (target);
			}
#endif
		}

		public override UnityEngine.Object[] GetModifiedObjects (GameObject target)
		{
			return null;
		}
	}

	public ReplaceVariation[] variations;

	public override Variation[] Variations { get { return variations; } }
}
