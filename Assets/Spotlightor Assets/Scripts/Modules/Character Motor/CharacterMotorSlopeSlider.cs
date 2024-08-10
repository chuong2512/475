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
public class CharacterMotorSlopeSlider : MonoBehaviour
{
	[System.Serializable]
	public class SlopeSliderOverride
	{
		public string groundTag = "Ground Tag";
		public float slideStartSlopeAngle = 61;
	}

	[System.Serializable]
	public class SlopeSettings
	{
		[Range (0, 1f)]
		public float friction = 0.8f;
		public float maxSlideSpeed = 3;
	}
	// Slide before Ground Clamp
	[Header ("Script Order: 4")]
	public float slideStartSlopeAngle = 46;
	public SlopeSettings slideStartSlopeSettings;
	public float slideMaxSlopeAngle = 70f;
	public SlopeSettings slideMaxSlopeSettings;
	[Space ()]
	public List<SlopeSliderOverride> slopeSliderOverrides = new List<SlopeSliderOverride> ();
	public bool logDebugInfo = false;

	private bool isSliding = false;
	private Vector3 slopeSlideVelocity = Vector3.zero;
	private SlopeSliderOverride slopeSliderOverride = null;

	public bool IsSliding {
		get{ return isSliding; }
		private set {
			if (isSliding != value) {
				isSliding = value;
				if (!isSliding)
					slopeSlideVelocity = Vector3.zero;
			}
		}
	}

	public float OverridedSlideStartSlopeAngle {
		get {
			if (slopeSliderOverride != null)
				return slopeSliderOverride.slideStartSlopeAngle;
			else
				return slideStartSlopeAngle;
		}
	}

	void OnEnable ()
	{
		GetComponent<EnhancedCharacterController> ().BeforeApplyMove += HandleBeforeApplyMove;
		GetComponent<EnhancedCharacterController> ().GroundEntered += HandleGroundEntered;
	}

	void OnDisable ()
	{
		GetComponent<EnhancedCharacterController> ().BeforeApplyMove -= HandleBeforeApplyMove;
		GetComponent<EnhancedCharacterController> ().GroundEntered -= HandleGroundEntered;
	}

	void LateUpdate ()
	{
		if (IsSliding) {
			Vector3 charVelocity = GetComponent<EnhancedCharacterController> ().Velocity;
			if (slopeSlideVelocity.y != 0) {
				float clampedSlopeSlideVelocityY = Mathf.Max (slopeSlideVelocity.y, charVelocity.y);
				clampedSlopeSlideVelocityY = Mathf.Min (clampedSlopeSlideVelocityY, 0);
				float velocityScale = clampedSlopeSlideVelocityY / slopeSlideVelocity.y;
				slopeSlideVelocity *= velocityScale;
			}
		}
	}

	void HandleGroundEntered (EnhancedCharacterController enhancedController, Collider ground)
	{
		slopeSliderOverride = null;
		for (int i = 0; i < slopeSliderOverrides.Count; i++) {
			SlopeSliderOverride o = slopeSliderOverrides [i];
			if (ground.CompareTag (o.groundTag) == true) {
				slopeSliderOverride = o;
				break;
			}
		}

		if (IsSliding) {
			if (ShouldEnhancedControllerSlide (enhancedController)) {
				Vector3 slideDownDir = GetSlopeSlideDownDir (enhancedController);
				slopeSlideVelocity = Vector3.Project (slopeSlideVelocity, slideDownDir);
			} else
				IsSliding = false;
		}
	}

	void HandleBeforeApplyMove (EnhancedCharacterController enhancedController, Vector3 totalMotion)
	{
		if (enhancedController.IsGrounded && enhancedController.SpeedY <= 0 && enhancedController.GroundHitNormal.y < 0.99f) {
			float slopeAngle = enhancedController.GroundSlopeAngle;

			if (slopeAngle < OverridedSlideStartSlopeAngle) {
				Vector3 ledgeDetectOrigin = enhancedController.GroundHitPoint;
				float ledgeDetectOffsetLength = 0.1f;
				Vector3 ledgeDetectOffsetDir = new Vector3 (enhancedController.GroundHitNormal.x, 0, enhancedController.GroundHitNormal.z).normalized;
				ledgeDetectOrigin += ledgeDetectOffsetDir * ledgeDetectOffsetLength;

				float ledgeDetectMaxSlopeAngle = Mathf.Min (slopeAngle + 5, 90);
				float detectLength = ledgeDetectOffsetLength / Mathf.Cos (Mathf.Deg2Rad * (ledgeDetectMaxSlopeAngle));

				if (logDebugInfo)
					Debug.DrawLine (ledgeDetectOrigin, ledgeDetectOrigin + Vector3.down * detectLength, Color.cyan);

				if (!Physics.Raycast (ledgeDetectOrigin, Vector3.down, detectLength, enhancedController.nearGroundLayerMask)) {
					slopeAngle = Mathf.Max (OverridedSlideStartSlopeAngle, slopeAngle);
					if (logDebugInfo)
						Debug.LogWarning ("On Ledge.");
				}
			}
				
			if (slopeAngle >= OverridedSlideStartSlopeAngle && slopeAngle <= enhancedController.standMaxSlopeAngle) {
				Vector3 slideDownDir = GetSlopeSlideDownDir (enhancedController);

				slopeSlideVelocity = Vector3.Project (slopeSlideVelocity, slideDownDir);

				float slideDownAcceleration = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * -enhancedController.gravity;
				float slopeLerpT = Mathf.InverseLerp (OverridedSlideStartSlopeAngle, slideMaxSlopeAngle, slopeAngle);
				float friction = Mathf.Lerp (slideStartSlopeSettings.friction, slideMaxSlopeSettings.friction, slopeLerpT);
				slideDownAcceleration *= (1 - friction);

				slopeSlideVelocity += slideDownDir * slideDownAcceleration * Time.deltaTime;

				if (slopeAngle <= slideMaxSlopeAngle) {
					float maxSlideSpeed = Mathf.Lerp (slideStartSlopeSettings.maxSlideSpeed, slideMaxSlopeSettings.maxSlideSpeed, slopeLerpT);
					if (slopeSlideVelocity.magnitude > maxSlideSpeed)
						slopeSlideVelocity = slideDownDir * maxSlideSpeed;
				}

				enhancedController.Move (slopeSlideVelocity * Time.deltaTime);

				IsSliding = true;
			} else
				IsSliding = false;
		} else {
			if (!enhancedController.IsGrounded) {
				if (IsSliding)
					enhancedController.Move (slopeSlideVelocity * Time.deltaTime);
			} else
				IsSliding = false;
		}

		if (logDebugInfo) {
			if (IsSliding)
				Debug.LogFormat ("SlopeSlideVelocity: {0}", slopeSlideVelocity.ToString ("F4"));
		}
	}

	private Vector3 GetSlopeSlideDownDir (EnhancedCharacterController enhancedController)
	{
		if (enhancedController.GroundHitNormal.y != 0) {
			Vector3 groundNormal = enhancedController.GroundHitNormal;
			float groundHitNormalXZLength = new Vector3 (groundNormal.x, 0, groundNormal.z).magnitude;
			Vector3 slideDownDir = groundNormal;
			slideDownDir.y = -Mathf.Tan (enhancedController.GroundSlopeAngle * Mathf.Deg2Rad) * groundHitNormalXZLength;
			return slideDownDir.normalized;
		} else {
			// SlopeAngle = 90 (GroundHitNormal.y == 0)
			return Vector3.down;
		}
	}

	private bool ShouldEnhancedControllerSlide (EnhancedCharacterController enhancedController)
	{
		bool shouldSlide = false;
		if (enhancedController.IsGrounded && enhancedController.SpeedY <= 0 && enhancedController.GroundHitNormal.y < 0.99f) {
			float slopeAngle = enhancedController.GroundSlopeAngle;
	
			if (slopeAngle >= OverridedSlideStartSlopeAngle)
				shouldSlide = true;
		}
		return shouldSlide;
	}
}
