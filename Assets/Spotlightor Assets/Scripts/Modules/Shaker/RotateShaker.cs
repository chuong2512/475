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

public class RotateShaker : Shaker
{
	public float intensityToRotation = 0.1f;
	private Vector3 defaultEulerAngles = Vector3.zero;
	private Rigidbody myRigidbody;

	void Awake ()
	{
		defaultEulerAngles = transform.localEulerAngles;
		myRigidbody = GetComponent<Rigidbody> ();
	}

	public override void Shake (Vector3 intensity)
	{
		if (myRigidbody == null)
			transform.localEulerAngles = defaultEulerAngles + intensity * intensityToRotation;
		else {
			Quaternion rotation = Quaternion.Euler (defaultEulerAngles + intensity * intensityToRotation);
			if (transform.parent != null)
				rotation = transform.parent.rotation * rotation;
			
			myRigidbody.MoveRotation (rotation);
		}
	}
}
