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

[RequireComponent (typeof(Rigidbody))]
public class RigidbodyMover : MonoBehaviour
{
	private Vector3 lastMoveDeltaPosition = Vector3.zero;
	private Quaternion lastMoveDeltaRotation = Quaternion.identity;
	private bool movedLastFrame = false;

	public Vector3 LastMoveVelocity{ get { return LastMoveDeltaPosition / Time.fixedDeltaTime; } }

	public Vector3 LastMoveAngularVelocity { get { return MathAddons.GetAngularVelocity (LastMoveDeltaRotation, Time.fixedDeltaTime); } }

	public Vector3 LastMoveDeltaPosition { 
		get { return lastMoveDeltaPosition; } 
		private set { lastMoveDeltaPosition = value; }
	}

	public Quaternion LastMoveDeltaRotation { 
		get { return lastMoveDeltaRotation; } 
		private set { lastMoveDeltaRotation = value; }
	}

	public Vector3 Position {
		get { return MyRigidbody.position; }
		set { MyRigidbody.position = value; }
	}

	public Quaternion Rotation {
		get { return MyRigidbody.rotation; }
		set { MyRigidbody.rotation = value; }
	}

	public Rigidbody MyRigidbody{ get { return GetComponent<Rigidbody> (); } }

	void OnEnable ()
	{
		if (MyRigidbody != null && !MyRigidbody.isKinematic)
			StartCoroutine ("LateFixedUpdates");
	}

	void OnDisable ()
	{
		if (MyRigidbody != null && !MyRigidbody.isKinematic)
			StopCoroutine ("LateFixedUpdates");
	}

	private IEnumerator LateFixedUpdates ()
	{
		while (true) {
			yield return new WaitForFixedUpdate ();

			if (MyRigidbody != null) {
				MyRigidbody.velocity = Vector3.zero;
				MyRigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

	private void LateUpdate ()
	{
		if (!movedLastFrame) {
			lastMoveDeltaPosition = Vector3.zero;
			lastMoveDeltaRotation = Quaternion.identity;
		}
		movedLastFrame = false;
	}

	public void MovePosition (Vector3 position)
	{
		LastMoveDeltaPosition = position - this.Position;

		if (MyRigidbody.isKinematic)
			MyRigidbody.MovePosition (position);
		else
			MyRigidbody.velocity = LastMoveDeltaPosition / Time.fixedDeltaTime;

		movedLastFrame = true;
	}

	public void MoveRotation (Quaternion rotation)
	{
		LastMoveDeltaRotation = rotation * Quaternion.Inverse (this.Rotation);

		if (MyRigidbody.isKinematic)
			MyRigidbody.MoveRotation (rotation);
		else
			MyRigidbody.angularVelocity = MathAddons.GetAngularVelocity (LastMoveDeltaRotation, Time.fixedDeltaTime);

		movedLastFrame = true;
	}

	public Vector3 GetLastMovePointVelocity (Vector3 worldPoint)
	{
		return LastMoveVelocity + Vector3.Cross (Position - worldPoint, LastMoveAngularVelocity);
	}
}
