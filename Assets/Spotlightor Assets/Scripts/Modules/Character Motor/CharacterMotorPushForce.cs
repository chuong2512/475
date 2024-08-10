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
[RequireComponent (typeof(CharacterMotorForwardWalker))]
public class CharacterMotorPushForce : MonoBehaviour
{
	public const string ApplyForceMessageName = "OnCharacerMotorForce";

	public enum ForceTypes
	{
		Weight,
		Push,
	}

	public class ApplyForceData
	{
		public Rigidbody rigidbody;
		public Vector3 force = Vector3.zero;
		public Vector3 point = Vector3.zero;
		public ForceTypes forceType;

		public ApplyForceData (Rigidbody rigidbody, Vector3 force, Vector3 point, ForceTypes forceType)
		{
			this.rigidbody = rigidbody;
			this.force = force;
			this.point = point;
			this.forceType = forceType;
		}

		public void Apply ()
		{
			if (rigidbody != null && force != Vector3.zero) {
				rigidbody.AddForceAtPosition (force, point);
				rigidbody.SendMessage (ApplyForceMessageName, this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public float walkStrengthToForce = 12f;
	public float weightForce = 2f;
	[SingleLineLabel ()]
	public AnimationCurve weightForceScaleBySlopeAngle = new AnimationCurve (new Keyframe (0, 1), new Keyframe (45, 1), new Keyframe (55, 0));
	[SingleLineLabel ()]
	public AnimationCurve pushForceScaleByTopHitProgress = new AnimationCurve (new Keyframe (0.7f, 1), new Keyframe (1, 0));
	[SingleLineLabel ()]
	public AnimationCurve pushForceScaleBySideHitAngle = new AnimationCurve (new Keyframe (0, 1), new Keyframe (90, 0));

	private EnhancedCharacterController enhancedController;
	private CharacterMotorForwardWalker walker;
	private List<ApplyForceData> applyForceDatas = new List<ApplyForceData> ();

	public List<ApplyForceData> ApplyForceDatas { get { return applyForceDatas; } }

	void OnEnable ()
	{
		enhancedController = GetComponent<EnhancedCharacterController> ();
		walker = GetComponent<CharacterMotorForwardWalker> ();
		enhancedController.BeforeUpdate += HandleControllerBeforeUpdate;
	}

	void OnDisable ()
	{
		enhancedController.BeforeUpdate -= HandleControllerBeforeUpdate;
	}

	void HandleControllerBeforeUpdate (EnhancedCharacterController enhancedController)
	{
		applyForceDatas.Clear ();
	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.rigidbody != null && IsPushable (hit.rigidbody) && !HasForceDataForRigidbody (hit.rigidbody)) {
			Vector3 force = Vector3.zero;
			ForceTypes forceType = ForceTypes.Weight;
			if (hit.point.y < enhancedController.BelowSphereCenter.y) {
				forceType = ForceTypes.Weight;

				if (enhancedController.IsGrounded)
					force = CalculateWeightForceFor (hit.rigidbody);
			} else if (hit.point.y <= enhancedController.UpSphereCenter.y) {
				forceType = ForceTypes.Push;

				force = CalculatePushForceFor (hit.rigidbody);
				Vector3 hitForward = hit.point - enhancedController.Center;
				hitForward.y = 0;
				hitForward.Normalize ();
				float angleToWalkForward = Vector3.Angle (walker.TargetForward, hitForward);
				force *= pushForceScaleBySideHitAngle.Evaluate (angleToWalkForward);
			} else {
				forceType = ForceTypes.Push;

				force = CalculatePushForceFor (hit.rigidbody);
				float topHitProgress = Mathf.InverseLerp (enhancedController.UpSphereCenter.y, enhancedController.UpCenter.y, hit.point.y);
				force *= pushForceScaleByTopHitProgress.Evaluate (topHitProgress);
			}
			
			if (force != Vector3.zero)
				applyForceDatas.Add (new ApplyForceData (hit.rigidbody, force, hit.point, forceType));
		}
	}

	void FixedUpdate ()
	{
		if (!enhancedController.enabled)
			applyForceDatas.Clear ();
		
		if (enhancedController.IsGrounded && enhancedController.Ground != null) {
			Rigidbody groundRigidbody = enhancedController.Ground.attachedRigidbody;
			if (groundRigidbody != null && IsPushable (groundRigidbody) && !HasForceDataForRigidbody (groundRigidbody)) {
				Vector3 force = CalculateWeightForceFor (groundRigidbody);
				if (force != Vector3.zero)
					applyForceDatas.Add (new ApplyForceData (groundRigidbody, force, enhancedController.BelowCenter, ForceTypes.Weight));
			}
		}
		applyForceDatas.ForEach (a => a.Apply ());
	}

	private Vector3 CalculateWeightForceFor (Rigidbody targetRigidbody)
	{
		Vector3 force = new Vector3 (0, -weightForce, 0);

		float weightForceScale = weightForceScaleBySlopeAngle.Evaluate (enhancedController.GroundSlopeAngle);

		CharMotorRigidInfo rigidInfo = CharMotorRigidInfoStorage.FindRigidInfoByTag (targetRigidbody.tag);
		if (rigidInfo != null)
			weightForceScale *= rigidInfo.weightForceScale;

		return force * weightForceScale;
	}

	private Vector3 CalculatePushForceFor (Rigidbody targetRigidbody)
	{
		Vector3 force = walker.TargetForward * walker.ForwardStrength;
		force *= walkStrengthToForce;
		CharMotorRigidInfo rigidInfo = CharMotorRigidInfoStorage.FindRigidInfoByTag (targetRigidbody.tag);
		if (rigidInfo != null)
			force *= rigidInfo.pushForceScale;
		return force;
	}

	private bool HasForceDataForRigidbody (Rigidbody targetRigidbody)
	{
		return applyForceDatas.Find (data => data.rigidbody == targetRigidbody) != null;
	}

	private bool IsPushable (Rigidbody targetRigidbody)
	{
		bool isPushable = false;
		if (!targetRigidbody.isKinematic) {
			CharMotorRigidInfo rigidInfo = CharMotorRigidInfoStorage.FindRigidInfoByTag (targetRigidbody.tag);
			isPushable = rigidInfo != null && rigidInfo.IsPushable;
		}
		return isPushable;
	}
}
