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
public class TubePlacer : MonoBehaviour
{
	[System.Serializable]
	public class PartSetting
	{
		public List<TubePart> prefabs;

		public TubePart DefaultPrefab{ get { return prefabs.Count > 0 ? prefabs [0] : null; } }
	}

	public PartSetting head;
	public PartSetting tail;
	public PartSetting straight;
	public List<PartSetting> bendRight;
	public List<PartSetting> bendLeft;
	public List<PartSetting> bendUp;
	public List<PartSetting> bendDown;
	public float buttonSize = 2;
	public float bendArrowOffset = 2;

	void Update ()
	{
		if (Application.isEditor && !Application.isPlaying)
			UpdatePartsPlacement ();
	}

	public virtual void UpdatePartsPlacement ()
	{
		List<TubePart> childParts = new List<TubePart> ();
		for (int i = 0; i < transform.childCount; i++) {
			TubePart part = transform.GetChild (i).GetComponent<TubePart> ();
			if (part != null && part.gameObject.activeSelf)
				childParts.Add (part);
		}
		for (int i = 0; i < childParts.Count; i++) {
			TubePart part = childParts [i];
			if (i == 0) {
				part.transform.localPosition = Vector3.zero;
				part.transform.localRotation = Quaternion.identity;
			} else {
				TubePart prevPart = childParts [i - 1];
				part.transform.position = prevPart.EndWorldPosition;
				part.transform.rotation = prevPart.EndWorldRotation;
			}
		}
	}
}
