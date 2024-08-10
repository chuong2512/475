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

[RequireComponent (typeof(CharacterController))]
public class EnhancedCharacterController : MonoBehaviour
{
	private const string MessageGroundEnter = "OnGroundEnter";
	private const string MessageGroundExit = "OnGroundExit";

	public delegate void GenericEventHandler (EnhancedCharacterController enhancedController);

	public delegate void GroundEventHandler (EnhancedCharacterController enhancedController, Collider ground);

	public delegate void MoveEventHandler (EnhancedCharacterController enhancedController, Vector3 totalMotion);

	public event GroundEventHandler Grounded;
	public event GroundEventHandler Floated;
	public event GroundEventHandler GroundEntered;
	public event GroundEventHandler GroundExited;
	public event GenericEventHandler BeforeUpdate;
	public event GenericEventHandler Teleported;
	public event MoveEventHandler BeforeApplyMove;
	public event MoveEventHandler AfterApplyMove;

	// To remove "offset" effect of moving platform (due to platform updated after this.Update)
	[Header ("Script Order: 1 (After DefaultTime)")]
	public float gravity = -20;

	[Header ("NearGrounded Detection")]
	public bool doNearGroundDetection = false;
	public LayerMask nearGroundLayerMask = -1;
	[ClampAttribute (0.001f, float.MaxValue)]
	public float nearGroundDistance = 0.1f;

	[Header ("Slope")]
	public float standMaxSlopeAngle = 76;

	[Header ("Move() Settings")]
	public bool alwaysMoveDown = false;
	#if UNITY_4
	public float skinWidth = 0.1f;
	#endif
	private Collider ground;
	private Vector3 totalMotion = Vector3.zero;
	private Vector3 totalAcceleration = Vector3.zero;
	private Vector3 speed = Vector3.zero;
	private Vector3 forward = Vector3.forward;
	private Vector3 velocity = Vector3.zero;
	private bool hitGroundThisUpdate = false;
	private List<ControllerColliderHit> hitsFromSide = new List<ControllerColliderHit> ();

	public float SkinWidth {
		#if UNITY_4
		get {return skinWidth;}
		#else
		get { return Controller.skinWidth; }
		#endif
	}

	public Vector3 UpCenter { get { return Center + Vector3.up * Height * 0.5f; } }

	public Vector3 BelowCenter { get { return Center + Vector3.down * Height * 0.5f; } }

	public Vector3 UpSphereCenter{ get { return Center + Vector3.up * (Controller.height * 0.5f - Controller.radius); } }

	public Vector3 BelowSphereCenter{ get { return Center + Vector3.down * (Controller.height * 0.5f - Controller.radius); } }

	public Vector3 Center{ get { return transform.position + Controller.center; } }

	public float Radius{ get { return Controller.radius + SkinWidth; } }

	public float Height{ get { return Controller.height + 2 * SkinWidth; } }

	public Vector3 LastGroundedVelocity{ get; private set; }

	public CharacterController Controller { get { return GetComponent<CharacterController> (); } }

	public bool IsGroundedAndCanStand{ get { return IsGrounded && GroundSlopeAngle <= standMaxSlopeAngle; } }

	public bool IsGrounded { get { return Controller.isGrounded || Ground != null; } }

	private bool IsNearGrounded{ get { return !Controller.isGrounded && Ground != null; } }

	public Collider Ground { 
		get{ return ground; }
		private set {
			if (ground != value) {
				if (ground != null) {
					if (GroundExited != null)
						GroundExited (this, ground);
					SendMessageToColliderAndRigidbody (ground, MessageGroundExit, this);
				}

				Collider lastGround = ground;
				ground = value;
				
				if (ground != null) {
					LastGroundedVelocity = Velocity;

					if (GroundEntered != null)
						GroundEntered (this, ground);
					SendMessageToColliderAndRigidbody (ground, MessageGroundEnter, this);

					if (lastGround == null) {
						if (Grounded != null)
							Grounded (this, ground);
					}
				} else {
					LastLeaveGroundTime = Time.time;

					if (Floated != null)
						Floated (this, lastGround);
				}
			}
		} 
	}

	public float LastLeaveGroundTime{ get; private set; }

	public Vector3 GroundHitPoint{ get; private set; }

	public float GroundSlopeAngle {
		get {
			float groundHitNormalXZLength = new Vector3 (GroundHitNormal.x, 0, GroundHitNormal.z).magnitude;
			return 90 - Mathf.Atan2 (GroundHitNormal.y, groundHitNormalXZLength) * Mathf.Rad2Deg;
		}
	}

	public Vector3 GroundHitNormal{ get; private set; }

	public Vector3 GroundHitMovement{ get; private set; }

	public Vector3 Speed { get { return speed; } set { speed = value; } }

	public float SpeedX { get { return speed.x; } set { speed.x = value; } }

	public float SpeedY { get { return speed.y; } set { speed.y = value; } }

	public float SpeedZ { get { return speed.z; } set { speed.z = value; } }

	public Vector3 Forward {
		get { return forward; }
		set {
			value.y = 0;
			if (value != Vector3.zero)
				forward = value.normalized;
		}
	}

	public Vector3 Velocity { get { return velocity; } }

	public CollisionFlags TotalCollisionFlags{ get; private set; }

	public List<ControllerColliderHit> HitsFromSide { get { return hitsFromSide; } }

	private void SendMessageToColliderAndRigidbody (Collider groundCollider, string message, object data)
	{
		groundCollider.SendMessage (message, data, SendMessageOptions.DontRequireReceiver);
		if (groundCollider.attachedRigidbody != null && groundCollider.attachedRigidbody.gameObject != groundCollider.gameObject)
			groundCollider.attachedRigidbody.SendMessage (message, data, SendMessageOptions.DontRequireReceiver);
	}

	public void Move (Vector3 motion)
	{
		totalMotion += motion;
	}

	public void Accelerate (Vector3 acceleration)
	{
		totalAcceleration += acceleration;
	}

	public void TeleportTo (Vector3 position, Vector3 targetForward)
	{
		TeleportTo (position);

		this.Forward = targetForward;
		transform.forward = targetForward;
	}

	public void TeleportTo (Vector3 position)
	{
		transform.position = position;
		this.Speed = Vector3.zero;
		this.Ground = null;

		if (Teleported != null)
			Teleported (this);
	}

	private void Update ()
	{
		Vector3 positionBeforeUpdate = transform.position;

		if (BeforeUpdate != null)
			BeforeUpdate (this);

		totalAcceleration.y += gravity;

		speed += totalAcceleration * Time.deltaTime;
		totalMotion += speed * Time.deltaTime;

		totalMotion += GetHorizontalMotionToAvoidStuckOnUnStandableGround (totalMotion);

		if (BeforeApplyMove != null)
			BeforeApplyMove (this, totalMotion);

		transform.forward = Forward;

		hitsFromSide.Clear ();
		hitGroundThisUpdate = false;
		TotalCollisionFlags = CollisionFlags.None;
		if (Controller.enabled) {
			if (alwaysMoveDown) {
				if (totalMotion.y < 0)
					TotalCollisionFlags |= Controller.Move (totalMotion);
				else {
					float bonusY = SkinWidth + 0.01f;
					TotalCollisionFlags |= Controller.Move (totalMotion + Vector3.up * bonusY);
				
					if ((TotalCollisionFlags & CollisionFlags.Above) != 0)
						SpeedY = Mathf.Min (0, SpeedY);
								
					TotalCollisionFlags |= Controller.Move (Vector3.down * bonusY);
				}
			} else {
				TotalCollisionFlags |= Controller.Move (totalMotion);

				if ((TotalCollisionFlags & CollisionFlags.Above) != 0)
					SpeedY = Mathf.Min (0, SpeedY);
			}
		}

		if (doNearGroundDetection && Controller.isGrounded == false && hitGroundThisUpdate == false)
			DetectIsNearGroundedByRaycast ();

		if (hitGroundThisUpdate == false)
			Ground = null;

		if (IsGroundedAndCanStand && SpeedY < 0)
			SpeedY = 0;

		if (AfterApplyMove != null)
			AfterApplyMove (this, totalMotion);

		totalMotion = Vector3.zero;
		totalAcceleration = Vector3.zero;

		Vector3 movementThisFrame = transform.position - positionBeforeUpdate;
		if (Time.timeScale > 0)
			velocity = movementThisFrame / Time.deltaTime;
	}

	private Vector3 GetHorizontalMotionToAvoidStuckOnUnStandableGround (Vector3 totalMotion)
	{
		Vector3 horizontalMotion = Vector3.zero;

		float slopeAngle = GroundSlopeAngle;
		float noGravityMotionY = totalMotion.y - gravity * Time.deltaTime * Time.deltaTime;
		if (IsGrounded && slopeAngle > standMaxSlopeAngle && slopeAngle < 90 && noGravityMotionY < 0) {
			float xzMotionLengthToAvoidStuckOnSlope = -noGravityMotionY / Mathf.Tan (slopeAngle * Mathf.Deg2Rad);
			horizontalMotion = new Vector3 (GroundHitNormal.x, 0, GroundHitNormal.z).normalized;
			horizontalMotion *= xzMotionLengthToAvoidStuckOnSlope;
		}
		return horizontalMotion;
	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.point.y < BelowSphereCenter.y) {
			GroundHitPoint = hit.point;
			// Sphere collider ground hit.normal is pointing sphere's center!? Flip it. Reproduce this "bug" in Scene Physics(Slope)
			GroundHitNormal = hit.normal.y > 0 ? hit.normal : -hit.normal;
			GroundHitMovement = totalMotion;
			Ground = hit.collider;
			hitGroundThisUpdate = true;
		} else if (hit.point.y <= UpSphereCenter.y) {
			hitsFromSide.Add (hit);
		} else {
		}
	}

	private void DetectIsNearGroundedByRaycast ()
	{
		RaycastHit nearGroundHit;
		if (NearGroundCast (nearGroundDistance, out nearGroundHit)) {
			GroundHitPoint = nearGroundHit.point;

			Vector3 normal = BelowSphereCenter - GroundHitPoint;
			float normalHorizontalLength = new Vector3 (normal.x, 0, normal.z).magnitude;
			if (normalHorizontalLength >= Controller.radius)
				normal.y = 0;
			else
				normal.y = Mathf.Sqrt (Controller.radius * Controller.radius - normalHorizontalLength * normalHorizontalLength);
			GroundHitNormal = normal.normalized;

			GroundHitMovement = totalMotion;
			Ground = nearGroundHit.collider;
			hitGroundThisUpdate = true;
		}
	}

	public bool NearGroundCast (float distanceBelow, out RaycastHit nearGroundHit)
	{
		nearGroundHit = new RaycastHit ();
		bool hitNearGound = false;
		float castRadius = Controller.radius + SkinWidth;
		float castDistance = Controller.radius + distanceBelow;

		RaycastHit[] groundHits = Physics.SphereCastAll (BelowSphereCenter + Vector3.up * castRadius, castRadius, Vector3.down, castDistance, nearGroundLayerMask);
		if (groundHits != null && groundHits.Length > 0) {
			float minDistanceToBelow = float.MaxValue;
			int bestGroundHitIndex = -1;
			for (int i = 0; i < groundHits.Length; i++) {
				RaycastHit hit = groundHits [i];
				Vector3 hitOffsetToCenter = hit.point - Center;
				hitOffsetToCenter.y = 0;
				float distanceToCenter = hitOffsetToCenter.magnitude;
				if (!hit.collider.isTrigger && hit.collider != Controller
				    && hit.point.y < BelowSphereCenter.y
				    && distanceToCenter < castRadius) {
					float distanceToBelow = (hit.point - BelowCenter).magnitude;
					if (distanceToBelow < minDistanceToBelow) {
						minDistanceToBelow = distanceToCenter;
						bestGroundHitIndex = i;
					}
				}
			}
			if (bestGroundHitIndex >= 0) {
				nearGroundHit = groundHits [bestGroundHitIndex];
				hitNearGound = true;
			}
		}
		return hitNearGound;
	}


	void OnDisable ()
	{
		Ground = null;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (BelowCenter, BelowCenter + Vector3.up * SkinWidth);
		Gizmos.DrawWireSphere (BelowSphereCenter, Controller.radius + SkinWidth);

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (BelowCenter, BelowCenter + Vector3.down * nearGroundDistance);
		Gizmos.DrawWireSphere (BelowSphereCenter + Vector3.down * (nearGroundDistance + SkinWidth), Radius);
	}

	void OnDrawGizmos ()
	{
		if (Application.isPlaying) {
			if (IsGrounded) {
				Gizmos.color = Color.green;
				Gizmos.DrawSphere (GroundHitPoint, 0.05f);
				Gizmos.DrawRay (GroundHitPoint, GroundHitNormal);
			}
		}
	}
}
