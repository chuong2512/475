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

public class MoveTransitionWidget : TransitionWidget
{
	public Vector3 posIn = Vector3.one;
	public Vector3 posOut = Vector3.zero;
	public bool isLocal = true;

	public override void UpdateTransitionProgress (float progress)
	{
		Vector3 pos = MathAddons.LerpUncapped (posOut, posIn, progress);
		if (isLocal && transform.parent != null)
			pos = transform.parent.TransformPoint (pos);

		Rigidbody myRigidbody = GetComponent<Rigidbody> ();
		if (myRigidbody != null)
			myRigidbody.MovePosition (pos);
		else
			transform.position = pos;
	}

	void Reset ()
	{
		posIn = posOut = transform.localPosition;
	}
}
