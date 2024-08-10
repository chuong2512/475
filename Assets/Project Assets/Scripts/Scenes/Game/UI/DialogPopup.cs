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

public class DialogPopup : MonoBehaviour
{
	private static ObjectInstanceFinder<DialogPopup> instanceFinder = new ObjectInstanceFinder<DialogPopup> ();

	public static DialogPopup Instance{ get { return instanceFinder.Instance; } }

	public TypeWriterTextDisplayer textDisplayer;
	public MapPositionToRectTransform posMapper;
	public float timeByTextLength = 0.2f;
	public Vector3 offset = Vector3.up * 2;
	private Transform target;

	public bool IsVisible{ get { return GetComponent<TransitionManager> ().State != TransitionManager.StateTypes.Out; } }

	public void Display (string text)
	{
		Display (text, Player.Instance.transform);
	}

	public void Display (string text, Transform target)
	{
		this.target = target;
		textDisplayer.TypewriteText (text);
		GetComponent<TransitionManager> ().TransitionIn ();

		CancelInvoke ("Hide");
		Invoke ("Hide", timeByTextLength * (float)text.Length);
	}

	public void Hide ()
	{
		GetComponent<TransitionManager> ().TransitionOut ();
	}

	void LateUpdate ()
	{
		if (target != null) {
			posMapper.waypoint.CopyPositionRotation (target);
			posMapper.waypoint.transform.position += offset;
		}
	}
}
