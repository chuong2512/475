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

public class StickyWalker : MonoBehaviour
{
	public delegate void EdgeHitEventHandler (StickyWalker walker, Vector3 edgeGroundNormal);

	public event EdgeHitEventHandler EdgeHit;

	public int fixedDirections = 0;
	public float speedScale = 1;
	public float forwardSpeed = 2;
	public bool useViewportAxis = true;

	[Space ()]
	public bool canClimbWall = true;
	public float groundHitLength = 0.1f;
	public LayerMask groundHitMask = -1;
	public float pitchStart = -90;
	public float pitchEnd = 90;
	public float pitchStep = 5;
	public List<string> unclimbableTags = new List<string> (){ "Unclimbable" };

	private RigidbodyMover lastHitRigidMover;
	private Vector3 lastHitPoint;
	private Vector3 lastHitNormal = Vector3.up;

	public float ForwardStrength { get; set; }

	public Vector2 Facing{ get; set; }

	void OnDisable ()
	{
		ForwardStrength = 0;
	}

	void FixedUpdate ()
	{
		float speed = ForwardStrength * forwardSpeed * speedScale;

		lastHitPoint = transform.position;
		if (speed > 0) {
			for (float pitch = pitchStart; pitch <= pitchEnd; pitch += pitchStep) {
				Vector3 forward = transform.TransformDirection (Quaternion.Euler (pitch, 0, 0) * Vector3.forward);
				Vector3 up = transform.TransformDirection (Quaternion.Euler (pitch, 0, 0) * Vector3.up);

				Vector3 newPos = transform.position + forward * speed * Time.fixedDeltaTime;
				Vector3 groundHitStart = newPos + up * (groundHitLength - 0.01f);
				Ray groundHitRay = new Ray (groundHitStart, -up);
				RaycastHit hit;
				if (Physics.Raycast (groundHitRay, out hit, groundHitLength, groundHitMask)) {
					if (CanClimbOn (hit.collider)) {
						bool isWall = hit.normal.y < 0.9f;
						if (canClimbWall || !isWall) {
							Vector3 groundNormalInCameraView = Camera.main.transform.InverseTransformDirection (hit.normal);
							groundNormalInCameraView.y = 0;
							if (groundNormalInCameraView.z < 0) {
								if (hit.collider.attachedRigidbody != null) {
									lastHitRigidMover = hit.collider.attachedRigidbody.GetComponent<RigidbodyMover> ();
									if (lastHitRigidMover != null)
										lastHitPoint = hit.point;
								} else
									lastHitRigidMover = null;

								newPos = hit.point;
								transform.rotation = Quaternion.LookRotation (newPos - transform.position, hit.normal);
								transform.position = newPos;
								lastHitNormal = hit.normal;
							} else {
								if (EdgeHit != null)
									EdgeHit (this, hit.normal);
							}
						}
					}
					break;
				}
			}
		} else {
			lastHitPoint = transform.position;
		}
		if (lastHitRigidMover != null) {
			Vector3 velocity = lastHitRigidMover.GetLastMovePointVelocity (lastHitPoint);
			transform.position += velocity * Time.fixedDeltaTime;
		}

		if (Facing != Vector2.zero) {
			if (useViewportAxis) {
				Vector3 forward = new Vector3 (Facing.x, Facing.y, 0).normalized;
				forward = Camera.main.transform.TransformDirection (forward);
				Vector3 fwdPoint = transform.position + forward * 0.1f;

				Vector3 fwdPointOnGround = fwdPoint;
				Ray ray = new Ray (Camera.main.transform.position, fwdPoint - Camera.main.transform.position);
				float enter = 1;

				Plane groundPlane = new Plane (lastHitNormal, transform.position);
				groundPlane.Raycast (ray, out enter);

				fwdPointOnGround = ray.origin + ray.direction * enter;
				transform.rotation = Quaternion.LookRotation ((fwdPointOnGround - transform.position).normalized, lastHitNormal);
			} else {
				Vector3 forward = new Vector3 (Facing.x, Facing.y, 0).normalized;
				Quaternion backToHit = Quaternion.FromToRotation (Vector3.back, lastHitNormal);
				forward = backToHit * forward;
				transform.rotation = Quaternion.LookRotation (forward, lastHitNormal);
			}
		}
	}

	private bool CanClimbOn (Collider targetCollider)
	{
		bool canClimb = true;
		if (targetCollider.isTrigger)
			canClimb = false;
		else {
			foreach (string unclimableTag in unclimbableTags) {
				if (targetCollider.CompareTag (unclimableTag)) {
					canClimb = false;
					break;
				}
			}
		}
		return canClimb;
	}

	void OnDrawGizmosSelected ()
	{
		for (float pitch = pitchStart; pitch <= pitchEnd; pitch += pitchStep) {
			Vector3 forward = transform.TransformDirection (Quaternion.Euler (pitch, 0, 0) * Vector3.forward);
			Vector3 up = transform.TransformDirection (Quaternion.Euler (pitch, 0, 0) * Vector3.up);

			Vector3 newPos = transform.position + forward * forwardSpeed * 0.1f;
			Vector3 groundHitStart = newPos + up * (groundHitLength - 0.01f);
			Ray groundHitRay = new Ray (groundHitStart, -up);
			Gizmos.DrawLine (groundHitRay.origin, groundHitRay.origin + groundHitRay.direction * groundHitLength);
		}
	}
}
