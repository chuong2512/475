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

[RequireComponent (typeof(EnhancedCharacterController))]
public class CharacterMotorMovingPlatformer : FunctionalMonoBehaviour
{
	private bool inheritPlatformVelocity = false;
	private EnhancedCharacterController enhancedController;
	private Vector3 groundHitLocalPositionOnPlatform = Vector3.zero;
	private Vector3 groundHitPositionLastFrame = Vector3.zero;
	private Vector3 controllerLocalForwardOnPlatform = Vector3.forward;
	private Rigidbody movingPlatformRigidbody;

	public Vector3 PlatformVelocityAppliedToCharacter {
		get {
			Vector3 platformVelocityApplied = Vector3.zero;
			if (IsOnMovingPlatform)
				platformVelocityApplied = PlatformVelocity;
			else {
				if (inheritPlatformVelocity && PlatformVelocity != Vector3.zero && !enhancedController.IsGrounded)
					platformVelocityApplied = PlatformVelocity;
			}
			return platformVelocityApplied;
		}
	}

	public Vector3 PlatformVelocity { get; private set; }

	public bool IsOnMovingPlatform{ get; private set; }

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime)
			enhancedController = GetComponent<EnhancedCharacterController> ();
		
		enhancedController.BeforeApplyMove += HandleBeforeApplyMove;
		enhancedController.AfterApplyMove += HandleAfterApplyMove;
		enhancedController.GroundEntered += HandleGroundEntered;
		enhancedController.GroundExited += HandleGroundExited;
	}

	protected override void OnBecameUnFunctional ()
	{
		enhancedController.BeforeApplyMove -= HandleBeforeApplyMove;
		enhancedController.AfterApplyMove -= HandleAfterApplyMove;
		enhancedController.GroundEntered -= HandleGroundEntered;
		enhancedController.GroundExited -= HandleGroundExited;
	}

	void HandleGroundEntered (EnhancedCharacterController enhancedController, Collider ground)
	{
		bool groundIsMovingPlatform = false;
		//TODO: should all use ground.attachedRigidbody.tag
		CharMotorRigidInfo rigidInfo = CharMotorRigidInfoStorage.FindRigidInfoByTag (ground.tag);
		if (rigidInfo == null && ground.attachedRigidbody != null)
			rigidInfo = CharMotorRigidInfoStorage.FindRigidInfoByTag (ground.attachedRigidbody.tag);

		if (rigidInfo != null) {
			groundIsMovingPlatform = rigidInfo.isMovingPlatform;
			inheritPlatformVelocity = rigidInfo.inheritMovingPlatformVelocity;
		}
		if (groundIsMovingPlatform) {
			groundHitLocalPositionOnPlatform = ground.transform.InverseTransformPoint (enhancedController.GroundHitPoint);
			groundHitPositionLastFrame = enhancedController.GroundHitPoint;
			controllerLocalForwardOnPlatform = ground.transform.InverseTransformDirection (transform.forward);

			if (movingPlatformRigidbody != ground.attachedRigidbody)
				PlatformVelocity = Vector3.zero;

			movingPlatformRigidbody = ground.attachedRigidbody;
			IsOnMovingPlatform = true;
		} else {
			PlatformVelocity = Vector3.zero;
			movingPlatformRigidbody = null;
			IsOnMovingPlatform = false;
		}
	}

	void HandleGroundExited (EnhancedCharacterController source, Collider ground)
	{
		if (IsOnMovingPlatform) {
			IsOnMovingPlatform = false;
		}
	}

	void HandleBeforeApplyMove (EnhancedCharacterController source, Vector3 totalMotion)
	{
		if (IsOnMovingPlatform && enhancedController.Ground == null)
			IsOnMovingPlatform = false;

		if (IsOnMovingPlatform) {
			Collider ground = enhancedController.Ground;

			Vector3 inheritForward = ground.transform.TransformDirection (controllerLocalForwardOnPlatform);
			inheritForward.y = 0;
			inheritForward.Normalize ();
			if (inheritForward != Vector3.zero && inheritForward != transform.forward) {
				float currentForwardAngle = Mathf.Atan2 (transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
				float inheritForwardAngle = Mathf.Atan2 (inheritForward.x, inheritForward.z) * Mathf.Rad2Deg;
				float deltaAngle = Mathf.DeltaAngle (currentForwardAngle, inheritForwardAngle);
				enhancedController.Forward = Quaternion.Euler (0, deltaAngle, 0) * enhancedController.Forward;
			}
			
			Vector3 platformDeltaMovement = ground.transform.TransformPoint (groundHitLocalPositionOnPlatform) - groundHitPositionLastFrame;
			if (platformDeltaMovement != Vector3.zero) {
				// If apply platform movement as Move.motion, fast moving up platform will make sinked controller "jump up".
				// And then, "jump up" + movingPlatform.movement => floating on platform
				// So up movement can only be applied as transform offset
				if (platformDeltaMovement.y > 0) {
					enhancedController.transform.position += Vector3.up * platformDeltaMovement.y;
					platformDeltaMovement.y = 0;
				}
				enhancedController.Move (platformDeltaMovement);
			}

			if (Time.deltaTime > 0)
				PlatformVelocity = platformDeltaMovement / Time.deltaTime;
		} else {
			if (inheritPlatformVelocity && PlatformVelocity != Vector3.zero && !enhancedController.IsGrounded) {
				enhancedController.Move (PlatformVelocity * Time.deltaTime);
			}
		}

	}

	void HandleAfterApplyMove (EnhancedCharacterController enhancedController, Vector3 totalMotion)
	{
		if (IsOnMovingPlatform) {
			Collider ground = enhancedController.Ground;

			groundHitLocalPositionOnPlatform = ground.transform.InverseTransformPoint (enhancedController.GroundHitPoint);
			groundHitPositionLastFrame = enhancedController.GroundHitPoint;
			controllerLocalForwardOnPlatform = ground.transform.InverseTransformDirection (transform.forward);
		} else
			movingPlatformRigidbody = null;
	}
}
