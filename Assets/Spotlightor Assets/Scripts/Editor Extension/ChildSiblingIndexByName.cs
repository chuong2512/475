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

[ExecuteInEditMode ()]
public class ChildSiblingIndexByName : MonoBehaviour
{

	#if !UNITY_EDITOR
	void Awake ()
	{
		Destroy (this);
	}
	#endif

	#if UNITY_EDITOR
	void Update ()
	{
		if (Application.isEditor && !Application.isPlaying)
			UpdateChildrenSiblingIndexes ();
	}

	private void UpdateChildrenSiblingIndexes ()
	{
		List<Transform> children = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++)
			children.Add (transform.GetChild (i));

		children.Sort (
			delegate(Transform x, Transform y) {
				int result = x.name.CompareTo (y.name);
				if (result == 0)
					result = x.GetSiblingIndex ().CompareTo (y.GetSiblingIndex ());
				return result;
			}
		);

		for (int i = 0; i < children.Count; i++) {
			Transform child = children [i];
			if (child.GetSiblingIndex () != i) {
				UnityEditor.Undo.RecordObject (child, "Sibling Index by Name");
				child.SetSiblingIndex (i);
				this.Log ("Update {0} siblingIndex => {1}", child.name, i);
			}
		}
	}
	#endif
}
