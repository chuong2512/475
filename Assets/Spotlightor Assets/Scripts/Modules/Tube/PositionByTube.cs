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

[ExecuteInEditMode ()]
public class PositionByTube : MonoBehaviour
{
	public Transform target;
	public TubePlacer tube;
	public bool rotateByTube = true;

	void OnEnable ()
	{
		if (Application.isEditor && !Application.isPlaying)
			UpdatePosition ();
		else
			Destroy (this);
	}

	void Update ()
	{
		if (Application.isEditor && !Application.isPlaying)
			UpdatePosition ();
	}

	public void UpdatePosition ()
	{
		if (target != null && tube != null && tube.gameObject.activeSelf) {
			TubePart lastPart = null;
			int childIndex = tube.transform.childCount - 1;
			while (lastPart == null && childIndex >= 0) {
				if (tube.transform.GetChild (childIndex).gameObject.activeSelf)
					lastPart = tube.transform.GetChild (childIndex).GetComponent<TubePart> ();

				childIndex--;
			}
			if (lastPart != null) {
				#if UNITY_EDITOR
				UnityEditor.Undo.RecordObject (target, "Place by Tube");
				target.position = lastPart.EndWorldPosition;
				if (rotateByTube)
					target.rotation = lastPart.EndWorldRotation;
				#endif
			}
		}
	}

	void Reset ()
	{
		if (target == null)
			target = this.transform;
		if (tube == null)
			tube = GetComponentInChildren<TubePlacer> (true);
	}
}
