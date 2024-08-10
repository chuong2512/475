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

public class CopyTransformValues : MonoBehaviour
{
	public Transform target;

	public bool copyPosition = true;
	public SmoothVector3 smoothPosition = new SmoothVector3 (0, 0, Vector3.zero, 99999);

	[Space ()]
	public bool copyRotation = true;
	public SmoothRotation smoothRotation = new SmoothRotation (0, 0, Vector3.zero, 99999);

	[Space ()]
	public bool copyLocalValues = false;

	private Rigidbody myRigidbody;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody> ();
		smoothPosition.Value = copyLocalValues ? transform.localPosition : transform.position;
		smoothRotation.Value = copyLocalValues ? transform.localEulerAngles : transform.eulerAngles;
	}

	void LateUpdate ()
	{
		if (myRigidbody == null) {
			if (copyPosition) {
				if (copyLocalValues) {
					smoothPosition.FollowToTargetValue (target.localPosition);
					transform.localPosition = smoothPosition;
				} else {
					smoothPosition.FollowToTargetValue (target.position);
					transform.position = smoothPosition;
				}
			}
			if (copyRotation) {
				if (copyLocalValues) {
					smoothRotation.FollowToTargetValue (target.localRotation);
					transform.localRotation = smoothRotation;
				} else {
					smoothRotation.FollowToTargetValue (target.rotation);
					transform.rotation = smoothRotation;
				}
			}
		}
	}

	void FixedUpdate ()
	{
		if (myRigidbody != null) {
			if (copyPosition) {
				if (copyLocalValues && transform.parent != null) {
					smoothPosition.FollowToTargetValue (transform.TransformPoint (target.localPosition));
					myRigidbody.MovePosition (smoothPosition);
				} else {
					smoothPosition.FollowToTargetValue (target.position);
					myRigidbody.MovePosition (smoothPosition);
				}
			}
			if (copyRotation) {
				if (copyLocalValues && transform.parent != null) {
					smoothRotation.FollowToTargetValue (transform.parent.rotation * target.localRotation);
					myRigidbody.MoveRotation (smoothRotation);
				} else {
					smoothRotation.FollowToTargetValue (target.rotation);
					myRigidbody.MoveRotation (smoothRotation);
				}
			}
		}
	}
}
