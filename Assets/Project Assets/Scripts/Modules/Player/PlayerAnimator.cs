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
using UnityEngine.Events;

public class PlayerAnimator : MonoBehaviour
{
	public UnityEvent onWalkStart;
	public UnityEvent onWalkStop;
	private Vector3 lastPosition;
	[SerializeField]
	private SmoothFloat smoothVelocity = new SmoothFloat (0.2f, 0, 0, 100);

	private bool isWalking = false;

	public bool IsWalking {
		get {
			return isWalking;
		}
		set {
			if (isWalking != value) {
				isWalking = value;
				if (value)
					onWalkStart.Invoke ();
				else
					onWalkStop.Invoke ();
			}
		}
	}

	void Start ()
	{
		lastPosition = transform.position;
	}

	void Update ()
	{
		Vector3 deltaPos = transform.position - lastPosition;
		if (Time.deltaTime > 0) {
			float velocity = deltaPos.magnitude / Time.deltaTime;
			smoothVelocity.FollowToTargetValue (velocity);
			GetComponent<Animator> ().SetFloat ("Velocity", smoothVelocity);
		} else
			smoothVelocity.FollowToTargetValue (0);

		lastPosition = transform.position;

		IsWalking = smoothVelocity.Value > 1;
	}
}
