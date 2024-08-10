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

public class RotateTransitionManager : ValueTransitionManager
{
	public Vector3 inEulerAngles = Vector3.zero;
	public Vector3 outEulerAngles = new Vector3 (0, 0, 360);

	private RigidbodyMover mover;

	private RigidbodyMover Mover {
		get {
			if (mover == null) {
				mover = GetComponent<RigidbodyMover> ();
				if (mover == null)
					mover = gameObject.AddComponent<RigidbodyMover> ();
			}
			return mover;
		}
	}

	protected override void OnProgressValueUpdated (float progress)
	{
		Vector3 localEulerAngles = outEulerAngles + (inEulerAngles - outEulerAngles) * progress;
		if (GetComponent<Rigidbody> () == null)
			transform.localEulerAngles = localEulerAngles;
		else {
			Quaternion worldRotation = Quaternion.Euler (localEulerAngles);
			if (transform.parent != null)
				worldRotation *= transform.parent.rotation;
			Mover.MoveRotation (worldRotation);
		}
	}

}
