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

[RequireComponent (typeof(EnhancedCharacterController))]
public class CharacterMotorJumper : MonoBehaviour
{
	public delegate void GenericEventHandler (CharacterMotorJumper jumper);

	public event GenericEventHandler JumpStarted;
	public event GenericEventHandler JumpFloatingEnded;
	public event GenericEventHandler JumpEnded;

	public float minJumpHeight = 1.2f;
	public float maxJumpHeight = 2.4f;

	[Space ()]
	public float floatForceDelay = 0.1f;

	[Header ("可跳跃性")]
	public float slopeLimitForJump = 60;
	[SingleLineLabel ()]
	public float exceedSlopeLimitJumpableDelay = 0.3f;
	[SingleLineLabel ()]
	public float leaveGroundJumpableDelay = 0.15f;

	[Header ("极限高跳辅助功能")]
	public bool maxJumpAid = false;
	[SingleLineLabel ()]
	public float enableMaxJumpAidHoldingTime = 0.3f;

	private bool isJumping = false;
	private bool isHoldingJump = false;
	private bool isJumpFloating = false;
	private float jumpTimeElasped = 0;
	private float jumpFloatingTime = 0;
	private bool hasUpdated = false;
	private float lastWithinSlopeLimitTime = 0;

	public bool IsJumping {
		get { return isJumping; }
		set {
			if (isJumping != value) {
				isJumping = value;
				if (isJumping) {
					if (JumpStarted != null)
						JumpStarted (this);
				} else {
					if (JumpEnded != null)
						JumpEnded (this);
				}
			}
		}
	}

	public bool IsHoldingJump {
		get { return isHoldingJump; }
		set {
			if (value != isHoldingJump) {
				isHoldingJump = value;
				if (!isHoldingJump && IsJumpFloating)
					IsJumpFloating = false;
			}
		}
	}

	public bool IsJumpFloating {
		get { return isJumpFloating; }
		set {
			if (value != isJumpFloating) {
				isJumpFloating = value;
				if (!isJumpFloating) {
					jumpFloatingTime = jumpTimeElasped;
					if (JumpFloatingEnded != null)
						JumpFloatingEnded (this);
				}
			}
		}
	}

	public float JumpTimeElasped { get { return jumpTimeElasped; } }

	public float TimeToReachMaxJumpHeight {
		get { 
			float jumpSpeedForMinHeight = this.JumpStartSpeedForMinHeight;
			float floatJumpStartSpeed = jumpSpeedForMinHeight + enhancedController.gravity * floatForceDelay;
			return floatForceDelay - floatJumpStartSpeed / (enhancedController.gravity + JumpFloatAcceleration);
		}
	}

	public float JumpFloatAcceleration {
		get {
			float jumpSpeedForMinHeight = this.JumpStartSpeedForMinHeight;
			float minHeightJumpSpeedDistance = jumpSpeedForMinHeight * floatForceDelay + 0.5f * enhancedController.gravity * floatForceDelay * floatForceDelay;
			float floatJumpDistance = maxJumpHeight - minHeightJumpSpeedDistance;
			float floatJumpStartSpeed = jumpSpeedForMinHeight + enhancedController.gravity * floatForceDelay;
			return -enhancedController.gravity - 0.5f * floatJumpStartSpeed * floatJumpStartSpeed / floatJumpDistance;
		}
	}

	private float TimeToReachMinHeight {
		get { return Mathf.Sqrt (2 * -minJumpHeight / enhancedController.gravity); }
	}

	private float JumpStartSpeedForMinHeight {
		get { return Mathf.Sqrt (2 * -minJumpHeight * enhancedController.gravity); }
	}

	private EnhancedCharacterController enhancedController;

	void Reset ()
	{
		if (GetComponent<CharacterController> () != null)
			slopeLimitForJump = GetComponent<CharacterController> ().slopeLimit;
	}


	void Start ()
	{
		enhancedController = GetComponent<EnhancedCharacterController> ();
	}

	// Before EnhancedController.Update
	private void Update ()
	{
		if (IsJumping
		    && enhancedController.SpeedY <= 0.0f
		    && enhancedController.IsGrounded) {
			IsJumping = false;
		}

		if (IsJumping && IsJumpFloating) {
			if ((enhancedController.Controller.collisionFlags & CollisionFlags.Above) != 0)
				IsJumpFloating = false;
		}

		bool maxJumpAidEnabled = false;
		if (maxJumpAid && jumpFloatingTime >= enableMaxJumpAidHoldingTime && jumpTimeElasped <= TimeToReachMaxJumpHeight)
			maxJumpAidEnabled = true;

		if (IsJumping && (IsJumpFloating || maxJumpAidEnabled)) {
			float a = enhancedController.gravity;
			bool isAccelerating = jumpTimeElasped >= floatForceDelay;
			if (isAccelerating) {
				a += JumpFloatAcceleration;
			}

			float idealJumpHeight = enhancedController.SpeedY * Time.deltaTime + 0.5f * a * Time.deltaTime * Time.deltaTime;
			if (isAccelerating)
				enhancedController.SpeedY += JumpFloatAcceleration * Time.deltaTime;

			float updateJumpHeight = (enhancedController.SpeedY + enhancedController.gravity * Time.deltaTime) * Time.deltaTime;
			enhancedController.Move (Vector3.up * (idealJumpHeight - updateJumpHeight));

			jumpTimeElasped += Time.deltaTime;

			if (IsJumpFloating && jumpTimeElasped > TimeToReachMaxJumpHeight)
				IsJumpFloating = false;
		}

		hasUpdated = true;
	}

	private void LateUpdate ()
	{
		hasUpdated = false;

		if (enhancedController.IsGrounded && enhancedController.GroundSlopeAngle <= slopeLimitForJump)
			lastWithinSlopeLimitTime = Time.time;
	}

	public void Jump ()
	{
		if (CanJump ()) {
			// Instant jump up even when falling down. Good for responsive 2nd jump
			enhancedController.SpeedY = Mathf.Max (0, enhancedController.SpeedY);

			enhancedController.SpeedY += JumpStartSpeedForMinHeight;
			if (hasUpdated && floatForceDelay > 0) {
				// Update() has been called this frame, so FloatAcceleration won't be applied in this frame's EnhancedController.Update().
				// This'll result in a less jump height than target value. (The lower the FPS, the less jump height)
				// So add the single missing FloatAcceleration directly in Jump().
				enhancedController.SpeedY += JumpFloatAcceleration * Time.deltaTime;
			}

			IsHoldingJump = true;
			IsJumpFloating = true;
			IsJumping = true;

			jumpTimeElasped = 0;
			jumpFloatingTime = 0;
		}
	}

	public bool CanJump ()
	{
		string reason = "";
		return CanJump (out reason);
	}

	public bool CanJump (out string cannotJumpReason)
	{
		bool jumpable = false;
		cannotJumpReason = "";
		if (minJumpHeight > 0 && maxJumpHeight > 0) {
			if (!IsJumping) {
				// Jumpable judged by nearGrounded, make sure player could jump even floating on some little gaps above grounds.
				if (enhancedController.IsGrounded) {
					if (enhancedController.GroundSlopeAngle <= slopeLimitForJump)
						jumpable = true;
					else {
						if (Time.time - lastWithinSlopeLimitTime < exceedSlopeLimitJumpableDelay)
							jumpable = true;
						else
							cannotJumpReason = string.Format ("Slope Angle: {0:0.0}. WinthinSlope Time Elapsed: {1:0.000}", 
								enhancedController.GroundSlopeAngle, Time.time - lastWithinSlopeLimitTime);
					}
				} else {
					if (Time.time - enhancedController.LastLeaveGroundTime < leaveGroundJumpableDelay)
						jumpable = true;
					else
						cannotJumpReason = string.Format ("In the Air. LeaveGroundTime Elapsed: {0:0.000}", Time.time - enhancedController.LastLeaveGroundTime);
				}
			} else
				cannotJumpReason = "Is Jumping";
		} else
			cannotJumpReason = "MinMax Jump Height = 0";
		
		return jumpable;
	}
}
