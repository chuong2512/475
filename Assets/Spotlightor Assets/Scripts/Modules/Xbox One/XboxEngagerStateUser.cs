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

#if UNITY_XBOXONE
using Users;
#endif

public class XboxEngagerStateUser : XboxEngager.EngageState
{
	public override XboxEngager.StateTypes StateId { get { return XboxEngager.StateTypes.EngageUser; } }

	#if UNITY_XBOXONE
	public AccountPickerOptions accountPikcerOptions = AccountPickerOptions.None;
	#endif

	private ulong controllerId = 0;
	private int targetUserId = -1;
	private int engagedUserId = -1;

	public ulong ControllerId {
		get { return controllerId; }
		set { controllerId = value; }
	}

	public int TargetUserId {
		get { return targetUserId; }
		set { targetUserId = value; }
	}

	public bool ForceRequestUserSignIn {
		get;
		set;
	}

	public int EngagedUserId { get { return engagedUserId; } }

	public override void BeginState (XboxEngager.StateTypes previousState)
	{
		base.BeginState (previousState);

		#if UNITY_XBOXONE
		this.Log ("Engage User started. ControllerId = {0}, ForceRequestUserSignIn = {1}", ControllerId, ForceRequestUserSignIn);

		engagedUserId = -1;
		if (XboxEngager.IsValidGamepad (controllerId)) {
			if (!ForceRequestUserSignIn)
				TryToEngageControllerPairedUser ();

			if (engagedUserId < 0) {
				this.Log ("RequestSignIn");

				UsersManager.OnSignInComplete += HandleAccountPickerSignInComplete;
				XboxOnePLM.OnResourceAvailabilityChangedEvent += HandleResourceAvailabilityChangedEvent;
				UsersManager.RequestSignIn (accountPikcerOptions, controllerId);
			} else {
				this.Log ("User {0} engaged by paired controller.", engagedUserId);
				GotoState (XboxEngager.StateTypes.Idle);
			}
		} else {
			this.Log ("Not a valid gamepad, skip user engage");
			GotoState (XboxEngager.StateTypes.Idle);
		}
		#endif
	}

	void HandleResourceAvailabilityChangedEvent (bool amConstrained)
	{
		this.Log ("Resource Availability Changed when account picking. amConstrained = {0}", amConstrained);
		if (!amConstrained) {
			this.Log ("No more constrained, delay and goto idle.");
			StartCoroutine ("DelayAndGotoIdle");
		}
	}

	private IEnumerator DelayAndGotoIdle ()
	{
		yield return new WaitForSeconds (0.2f);
		this.Log ("Goto Idle after delay");
		GotoState (XboxEngager.StateTypes.Idle);
	}

	#if UNITY_XBOXONE
	private void TryToEngageControllerPairedUser ()
	{
		this.Log ("TryToEngageControllerPairedUser");

		uint joystickId = XboxOneInput.GetJoystickId (controllerId);
		int pairedUserId = XboxOneInput.GetUserIdForGamepad (joystickId);
		User pairedUser = UsersManager.FindUserById (pairedUserId);

		if (pairedUser != null) {
			this.Log ("Find a valid pairedUser {0} for ControllerId: {1}", pairedUserId, controllerId);

			bool canEngageToPairedUser = true;
			if (targetUserId >= 0) {
				User targetUser = UsersManager.FindUserById (targetUserId);
				if (targetUser != null && targetUser.Id != pairedUser.Id) {
					canEngageToPairedUser = false;
					this.Log ("Cannot engage to paired User {0}, because != targetUser {1}", pairedUserId, targetUserId);
				}
			}

			if (canEngageToPairedUser)
				this.engagedUserId = pairedUser.Id;
		} else
			this.Log ("Cannot find a valid pairedUser for ControllerId: {0}", controllerId);
	}

	void HandleAccountPickerSignInComplete (int resultType, int userId)
	{
		this.engagedUserId = userId;

		this.Log ("Sign in complete. Result = {0}. User {1} picked.", resultType, engagedUserId);

		GotoState (XboxEngager.StateTypes.Idle);
	}
	#endif
	public override void EndState (XboxEngager.StateTypes newState)
	{
		CleanUp ();
		base.EndState (newState);
	}

	void OnDestroy ()
	{
		CleanUp ();
	}

	private void CleanUp ()
	{
		#if UNITY_XBOXONE
		UsersManager.OnSignInComplete -= HandleAccountPickerSignInComplete;
		XboxOnePLM.OnResourceAvailabilityChangedEvent -= HandleResourceAvailabilityChangedEvent;
		StopCoroutine ("DelayAndGotoIdle");
		#endif
	}
}
