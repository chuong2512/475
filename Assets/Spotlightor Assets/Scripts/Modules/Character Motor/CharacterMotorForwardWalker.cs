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
public class CharacterMotorForwardWalker : FunctionalMonoBehaviour
{
	[System.Serializable]
	public class PhysicsSettings : System.ICloneable
	{
		[Header ("Speed & Acceleration")]
		public float speed = 5;
		public float groundAcceleration = 30f;
		public float groundDeceleration = 100000f;
		public float airAcceleration = 10f;
		public float airDeceleration = 10f;
		[Header ("Dir & Turning")]
		public float slowTurnSpeed = 800;
		public float slowTurnDeltaAngle = 22.5f;
		public float fastTurnSpeed = 1800;
		public float fastTurnDeltaAngle = 45;
		public float brakeTurnAngle = 120f;
		public float slowFaceTurnDeltaAngle = 5;
		public float slowFaceTurnSpeed = 600;
		public float fastFaceTurnDeltaAngle = 45;
		public float fastFaceTurnSpeed = 1200;
		[Range (0, 1)]
		public float airTurnSpeedScale = 0.8f;

		public List<PhysicsSettingsModifier> modifiers = new List<PhysicsSettingsModifier> ();

		public float ModifiedSpeed {
			get { 
				float modifiedValue = speed;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.speedMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedGroundAcceleration {
			get { 
				float modifiedValue = groundAcceleration;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.groundAccelerationMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedGroundDeceleration {
			get { 
				float modifiedValue = groundDeceleration;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.groundDecelerationMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedAirAcceleration {
			get { 
				float modifiedValue = airAcceleration;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.airAccelerationMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedAirDeceleration {
			get { 
				float modifiedValue = airDeceleration;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.airDecelerationMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedSlowTurnSpeed {
			get { 
				float modifiedValue = slowTurnSpeed;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.slowTurnSpeedMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedFastTurnSpeed {
			get { 
				float modifiedValue = fastTurnSpeed;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.fastTurnSpeedMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedSlowFaceTurnSpeed {
			get { 
				float modifiedValue = slowFaceTurnSpeed;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.slowFaceTurnSpeedMultiplier;
				return modifiedValue;
			}
		}

		public float ModifiedFastFaceTurnSpeed {
			get { 
				float modifiedValue = fastFaceTurnSpeed;
				foreach (PhysicsSettingsModifier modifier in modifiers)
					modifiedValue *= modifier.fastFaceTurnSpeedMultiplier;
				return modifiedValue;
			}
		}

		public object Clone ()
		{
			return this.MemberwiseClone ();
		}

		public void Lerp (PhysicsSettings a, PhysicsSettings b, float t)
		{
			speed = Mathf.Lerp (a.speed, b.speed, t);
			groundAcceleration = Mathf.Lerp (a.groundAcceleration, b.groundAcceleration, t);
			groundDeceleration = Mathf.Lerp (a.groundDeceleration, b.groundDeceleration, t);
			airAcceleration = Mathf.Lerp (a.airAcceleration, b.airAcceleration, t);
			slowTurnSpeed = Mathf.Lerp (a.slowTurnSpeed, b.slowTurnSpeed, t);
			slowTurnDeltaAngle = Mathf.Lerp (a.slowTurnDeltaAngle, b.slowTurnDeltaAngle, t);
			fastTurnSpeed = Mathf.Lerp (a.fastTurnSpeed, b.fastTurnSpeed, t);
			fastTurnDeltaAngle = Mathf.Lerp (a.fastTurnDeltaAngle, b.fastTurnDeltaAngle, t);
			brakeTurnAngle = Mathf.Lerp (a.brakeTurnAngle, b.brakeTurnAngle, t);
			airTurnSpeedScale = Mathf.Lerp (a.airTurnSpeedScale, b.airTurnSpeedScale, t);

			fastFaceTurnDeltaAngle = Mathf.Lerp (a.fastFaceTurnDeltaAngle, b.fastFaceTurnDeltaAngle, t);
			fastFaceTurnSpeed = Mathf.Lerp (a.fastFaceTurnSpeed, b.fastFaceTurnSpeed, t);
			slowFaceTurnDeltaAngle = Mathf.Lerp (a.slowFaceTurnDeltaAngle, b.slowFaceTurnDeltaAngle, t);
			slowFaceTurnSpeed = Mathf.Lerp (a.slowFaceTurnSpeed, b.slowFaceTurnSpeed, t);
		}
	}

	[System.Serializable]
	public class PhysicsSettingsModifier
	{
		[Header ("Speed & Acceleration")]
		public float speedMultiplier = 1;
		public float groundAccelerationMultiplier = 1;
		public float groundDecelerationMultiplier = 1;
		public float airAccelerationMultiplier = 1;
		public float airDecelerationMultiplier = 1;

		[Header ("Dir & Turning")]
		public float slowTurnSpeedMultiplier = 1;
		public float fastTurnSpeedMultiplier = 1;
		public float slowFaceTurnSpeedMultiplier = 1;
		public float fastFaceTurnSpeedMultiplier = 1;
	}

	public class TurningInfo //TODO: Move it to a special smooth float
	{
		public float turnStartDeltaAngle = 10;
		public float turnSpeed = 10;
		private bool isTurning = false;
		private float turnStartTime = 0;
		private float turnEndTime = 0;

		public bool IsTurning {
			get { return isTurning; }
			set {
				if (isTurning != value) {
					isTurning = value;
					if (isTurning)
						turnStartTime = turnEndTime = Time.time;
					else
						turnEndTime = Time.time;
				}
			}
		}

		public float LastTurnTime{ get { return turnEndTime - turnStartTime; } }
	}

	public PhysicsSettings physicsSettings;
	public int fixedDirectionsCount = 0;
	public bool brakeTurnOnGround = true;
	public bool brakeTurnInAir = false;

	[Header ("Slow on Slope")]
	public AnimationCurve slowOnSlopeByAngle = new AnimationCurve (new Keyframe (5, 0), new Keyframe (70, 1));
	[SingleLineLabel ()]
	public float slowOnSlopeAirLastingTime = 0.2f;

	[Range (0, 1), Header ("To implement move along jump start speed in air")]
	public float airTurnForwardStrength = 0.0f;

	[Header ("To help jump without running to jump further")]
	public float airPhysicsDelay = 0.1f;
	public float airPhysicsFadeTime = 0.1f;
	private float forwardStrength = 0;
	private Vector3 speedDir = Vector3.forward;
	private Vector3 targetForward = Vector3.forward;
	private EnhancedCharacterController enhancedController;
	private TurningInfo speedTurningInfo = new TurningInfo ();
	private TurningInfo faceTurningInfo = new TurningInfo ();

	public bool IsBrakeTurning{ get; private set; }

	public float ForwardStrength {
		get { return forwardStrength; }
		set { forwardStrength = Mathf.Clamp (value, 0, 1); }
	}

	public Vector3 SpeedDir { get { return speedDir; } }

	public Vector3 TargetForward {
		get { return targetForward; }
		set {
			value.y = 0;
			if (value != Vector3.zero) {
				if (fixedDirectionsCount <= 0)
					targetForward = value.normalized;
				else {
					float dirSplitAngle = 360f / (float)fixedDirectionsCount;
					float targetAngle = Mathf.Atan2 (value.x, value.z) * Mathf.Rad2Deg;
					targetAngle = (float)Mathf.RoundToInt (targetAngle / dirSplitAngle) * dirSplitAngle;
					targetForward = Quaternion.Euler (0, targetAngle, 0) * Vector3.forward;
				}
			}
		}
	}

	private float AirPhysicsIntensity {
		get {
			float airPhysicsIntensity = 0;
			if (!enhancedController.IsGrounded) {
				float airTime = Time.time - enhancedController.LastLeaveGroundTime;
				if (airTime <= airPhysicsDelay)
					airPhysicsIntensity = 0;
				else if (airTime < airPhysicsDelay + airPhysicsFadeTime) {
					if (airPhysicsFadeTime > 0)
						airPhysicsIntensity = (airTime - airPhysicsDelay) / airPhysicsFadeTime;
				} else
					airPhysicsIntensity = 1;
			}
			return airPhysicsIntensity;
		}
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime)
			enhancedController = GetComponent<EnhancedCharacterController> ();

		if (slowOnSlopeByAngle.keys.Length > 1) {
			Keyframe lastKey = slowOnSlopeByAngle.keys [slowOnSlopeByAngle.keys.Length - 1];
			float maxSlopeAngle = lastKey.time;
			float slopeLimit = enhancedController.Controller.slopeLimit;
			if (slopeLimit < maxSlopeAngle - 1) {
				this.LogWarning ("Slow On SLope By Angle curve max key slope angle [{0}] > CC.SlopeLimit [{1}]-1, increase slope limit!", maxSlopeAngle, slopeLimit);
				enhancedController.Controller.slopeLimit = maxSlopeAngle - 1;
			}
		}

		targetForward = speedDir = enhancedController.Forward = transform.forward;
		enhancedController.BeforeUpdate += HandleBeforeUpdate;
		enhancedController.AfterApplyMove += HandleAfterApplyMove;
	}

	protected override void OnBecameUnFunctional ()
	{
		enhancedController.BeforeUpdate -= HandleBeforeUpdate;
		enhancedController.AfterApplyMove -= HandleAfterApplyMove;
	}

	void HandleBeforeUpdate (EnhancedCharacterController enhancedController)
	{
		UpdateSpeed ();
		UpdateFacing ();

		if (enhancedController.IsGrounded)
			ReduceWalkSpeedByGroundHit ();
		else {
			float slowOnSlopeAirLastingMinAngle = enhancedController.standMaxSlopeAngle;
			float lastSlopeAngle = enhancedController.GroundSlopeAngle;
			float timeSinceAir = Time.time - enhancedController.LastLeaveGroundTime;
			if (lastSlopeAngle > slowOnSlopeAirLastingMinAngle && lastSlopeAngle < 90
			    && timeSinceAir < slowOnSlopeAirLastingTime) {
				/**
				 * 在刚刚离开地面后，如果原SlopeAngle大于指定值，且离地时间在指定范围内，则依然会继续施加 SlowOnSlope坡道减速功能。
				 * 这样做是为了防止角色从陡坡下滑到很陡的坡度后（例如89.5度），在离开地面的下一帧水平移动力迅速恢复，
				 * 导致角色向墙的方向移动、CC陷入地形、并向上产生位移，致使角色只要按住方向键沿着逆坡道行走，就一直无法坠落。
				 * 这类bug在球形、圆柱形表面下滑过程中可以重复出来。
				 */
				ReduceWalkSpeedByGroundHit ();
			}
		}
	}

	private void UpdateSpeed ()
	{
		float currentSpeedAngle = Mathf.Atan2 (speedDir.x, speedDir.z) * Mathf.Rad2Deg;
		float targetForwardAngle = Mathf.Atan2 (targetForward.x, targetForward.z) * Mathf.Rad2Deg;
		float angleSpeedToTarget = Mathf.DeltaAngle (currentSpeedAngle, targetForwardAngle);

		if (angleSpeedToTarget != 0) {
			if (speedTurningInfo.IsTurning == false || speedTurningInfo.turnStartDeltaAngle < Mathf.Abs (angleSpeedToTarget)) {
				speedTurningInfo.IsTurning = true;
				speedTurningInfo.turnStartDeltaAngle = Mathf.Abs (angleSpeedToTarget);
				float slowOrFastTurn = Mathf.InverseLerp (physicsSettings.slowTurnDeltaAngle, physicsSettings.fastTurnDeltaAngle, Mathf.Abs (angleSpeedToTarget));
				speedTurningInfo.turnSpeed = Mathf.Lerp (physicsSettings.ModifiedSlowTurnSpeed, physicsSettings.ModifiedFastTurnSpeed, slowOrFastTurn);

				if (!IsBrakeTurning)
					IsBrakeTurning = Mathf.Abs (angleSpeedToTarget) >= physicsSettings.brakeTurnAngle;
			}
		}
			
		float turnSpeed = speedTurningInfo.turnSpeed;

		if (!enhancedController.IsGrounded)
			turnSpeed *= Mathf.Lerp (1, physicsSettings.airTurnSpeedScale, AirPhysicsIntensity);

		float deltaRotateAngle = turnSpeed * Time.deltaTime;

		bool reachTargetForward = deltaRotateAngle >= Mathf.Abs (angleSpeedToTarget);
		if (!reachTargetForward) {
			if (angleSpeedToTarget < 0)
				deltaRotateAngle = -deltaRotateAngle;
			currentSpeedAngle += deltaRotateAngle;
			speedDir = Quaternion.Euler (0, currentSpeedAngle, 0) * Vector3.forward;
		} else {
			speedDir = TargetForward;
			speedTurningInfo.IsTurning = false;
		}

		if (IsBrakeTurning) {
			bool brakeTurn = false;
			if (enhancedController.IsGrounded)
				brakeTurn = brakeTurnOnGround;
			else
				brakeTurn = brakeTurnInAir;
			
			if (brakeTurn)
				enhancedController.SpeedX = enhancedController.SpeedZ = 0;
			else {
				float currentForwardSpeed = new Vector3 (enhancedController.SpeedX, 0, enhancedController.SpeedZ).magnitude;
				float acceleration = Mathf.Lerp (physicsSettings.ModifiedGroundAcceleration, physicsSettings.ModifiedAirAcceleration, AirPhysicsIntensity);
				currentForwardSpeed = Mathf.Max (currentForwardSpeed - acceleration * Time.deltaTime, 0);

				Vector3 ccSpeedDir = enhancedController.Speed.normalized;
				enhancedController.SpeedX = ccSpeedDir.x * currentForwardSpeed;
				enhancedController.SpeedZ = ccSpeedDir.z * currentForwardSpeed;
			}

			if (reachTargetForward)
				IsBrakeTurning = false;
		} else {
			float targetForwardSpeed = ForwardStrength * physicsSettings.ModifiedSpeed;
			float currentForwardSpeed = new Vector3 (enhancedController.SpeedX, 0, enhancedController.SpeedZ).magnitude;

			if (!enhancedController.IsGrounded && ForwardStrength < airTurnForwardStrength)
				targetForwardSpeed = currentForwardSpeed;

			float acceleration = Mathf.Lerp (physicsSettings.ModifiedGroundAcceleration, physicsSettings.ModifiedAirAcceleration, AirPhysicsIntensity);
			float decceleration = Mathf.Lerp (physicsSettings.ModifiedGroundDeceleration, physicsSettings.ModifiedAirDeceleration, AirPhysicsIntensity);

			if (currentForwardSpeed < targetForwardSpeed)
				currentForwardSpeed = Mathf.Min (currentForwardSpeed + acceleration * Time.deltaTime, targetForwardSpeed);
			else if (currentForwardSpeed > targetForwardSpeed)
				currentForwardSpeed = Mathf.Max (currentForwardSpeed - decceleration * Time.deltaTime, targetForwardSpeed);

			enhancedController.SpeedX = speedDir.x * currentForwardSpeed;
			enhancedController.SpeedZ = speedDir.z * currentForwardSpeed;
		}
	}

	private void UpdateFacing ()
	{
		float targetForwardAngle = Mathf.Atan2 (targetForward.x, targetForward.z) * Mathf.Rad2Deg;
		float currentFaceAngle = Mathf.Atan2 (enhancedController.Forward.x, enhancedController.Forward.z) * Mathf.Rad2Deg;
		float angleFaceToTarget = Mathf.DeltaAngle (currentFaceAngle, targetForwardAngle);

		if (angleFaceToTarget != 0) {
			if (faceTurningInfo.IsTurning == false || faceTurningInfo.turnStartDeltaAngle < Mathf.Abs (angleFaceToTarget)) {
				faceTurningInfo.IsTurning = true;
				faceTurningInfo.turnStartDeltaAngle = Mathf.Abs (angleFaceToTarget);
				float slowOrFastTurn = Mathf.InverseLerp (physicsSettings.slowFaceTurnDeltaAngle, physicsSettings.fastFaceTurnDeltaAngle, Mathf.Abs (angleFaceToTarget));
				faceTurningInfo.turnSpeed = Mathf.Lerp (physicsSettings.ModifiedSlowFaceTurnSpeed, physicsSettings.ModifiedFastFaceTurnSpeed, slowOrFastTurn);
			}
		}

		float faceTurnSpeed = faceTurningInfo.turnSpeed;
		if (!enhancedController.IsGrounded)
			faceTurnSpeed *= physicsSettings.airTurnSpeedScale;

		float deltaFaceRotateAngle = faceTurnSpeed * Time.deltaTime;


		bool faceReachTargetForward = deltaFaceRotateAngle >= Mathf.Abs (angleFaceToTarget);
		if (!faceReachTargetForward) {
			if (angleFaceToTarget < 0)
				deltaFaceRotateAngle = -deltaFaceRotateAngle;

			currentFaceAngle += deltaFaceRotateAngle;
			enhancedController.Forward = Quaternion.Euler (0, currentFaceAngle, 0) * Vector3.forward;
		} else {
			enhancedController.Forward = TargetForward;
			faceTurningInfo.IsTurning = false;
		}
	}

	private void ReduceWalkSpeedByGroundHit ()
	{
		Vector3 antiWalkDir = -enhancedController.GroundHitNormal;
		antiWalkDir.y = 0;
		antiWalkDir.Normalize ();

		Vector3 horizontalSpeed = new Vector3 (enhancedController.SpeedX, 0, enhancedController.SpeedZ);
		if (antiWalkDir != Vector3.zero && Vector3.Angle (antiWalkDir, horizontalSpeed.normalized) < 90) {
			Vector3 intoHitSurfaceDir = -enhancedController.GroundHitNormal;
			if (intoHitSurfaceDir.y < 0.99f) {
				Vector3 reducedSpeed = Vector3.Project (horizontalSpeed, antiWalkDir);
				reducedSpeed.y = 0;
				float slowScale = slowOnSlopeByAngle.Evaluate (enhancedController.GroundSlopeAngle);
				reducedSpeed *= slowScale;

				enhancedController.Speed -= reducedSpeed;
			}
		}
	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		float hitY = hit.point.y;
		if (hitY > enhancedController.UpSphereCenter.y)
			ReduceWalkSpeedByTopHit (hit);
		else if (hitY > enhancedController.BelowSphereCenter.y)
			ReduceWalkSpeedBySideHit (hit);
		else {
			// Below hit speed reduction is applied in BeforeUpdate by ground hit info
		}
	}

	private void ReduceWalkSpeedBySideHit (ControllerColliderHit hit)
	{
		Vector3 antiWalkDir = -hit.normal;
		antiWalkDir.y = 0;
		antiWalkDir.Normalize ();

		if (antiWalkDir != Vector3.zero) {
			Vector3 hitReducedSpeed = Vector3.Project (new Vector3 (enhancedController.SpeedX, 0, enhancedController.SpeedZ), antiWalkDir);
			enhancedController.Speed -= hitReducedSpeed;
		}
	}

	private void ReduceWalkSpeedByTopHit (ControllerColliderHit hit)
	{
		Vector3 antiWalkDir = -hit.normal;
		antiWalkDir.y = 0;
		antiWalkDir.Normalize ();

		Vector3 horizontalSpeed = new Vector3 (enhancedController.SpeedX, 0, enhancedController.SpeedZ);
		if (antiWalkDir != Vector3.zero && Vector3.Angle (antiWalkDir, horizontalSpeed.normalized) < 90) {
			Vector3 intoHitSurfaceDir = -hit.normal;
			if (intoHitSurfaceDir.y < 0.99f) {
				Vector3 intoHitSurfaceSpeed = Vector3.Project (horizontalSpeed, intoHitSurfaceDir);
				enhancedController.SpeedX -= intoHitSurfaceSpeed.x;
				enhancedController.SpeedZ -= intoHitSurfaceSpeed.z;
			}
		}
	}

	void HandleAfterApplyMove (EnhancedCharacterController enhancedController, Vector3 totalMotion)
	{
		// Other components (Moving Platform) may change forward in update.
		// The dir will only be fixed when updated using Forward property
		targetForward = enhancedController.transform.forward;
	}
}
