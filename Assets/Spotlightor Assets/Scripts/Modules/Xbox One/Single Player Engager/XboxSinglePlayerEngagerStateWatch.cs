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

public class XboxSinglePlayerEngagerStateWatch : XboxSinglePlayerEngagerState
{
	public override XboxSinglePlayerEngager.StateTypes StateId {
		get { return XboxSinglePlayerEngager.StateTypes.Watch; }
	}

	#if UNITY_XBOXONE
	public override void BeginState (XboxSinglePlayerEngager.StateTypes previousState)
	{
		base.BeginState (previousState);

		if (Owner.ActivePlayer.UserId > -1) {
			if (Owner.ActivePlayer.IsControllerUserPairValid) {
				Owner.ActivePlayer.BecameInvalid += HandleActivePlayerBecameInvalid;
				XboxOnePLM.OnWindowActivatedChangedEvent += HandleWindowActivatedChangedEvent;
			} else {
				this.Log ("ActivePlayer not valid: {0}. Goto ReEngage state.", Owner.ActivePlayer);
				Owner.GotoState (XboxSinglePlayerEngager.StateTypes.ReEngage);
			}
		} else {
			this.Log ("ActivePlayer.UserId is not valid: {0}. Nothing to watch, Goto NotSet state.", Owner.ActivePlayer);
			Owner.GotoState (XboxSinglePlayerEngager.StateTypes.NotSet);
		}
	}

	public override void EndState (XboxSinglePlayerEngager.StateTypes newState)
	{
		Owner.ActivePlayer.BecameInvalid -= HandleActivePlayerBecameInvalid;
		XboxOnePLM.OnWindowActivatedChangedEvent -= HandleWindowActivatedChangedEvent;

		base.EndState (newState);
	}

	void HandleWindowActivatedChangedEvent (XboxOneCoreWindowActivationState windowActivationState)
	{
		if (windowActivationState == XboxOneCoreWindowActivationState.Deactivated) {
			this.Log ("Window activation state = {0}. Goto ReEngage state.", windowActivationState);
			Owner.GotoState (XboxSinglePlayerEngager.StateTypes.ReEngage);
		}
	}

	void HandleActivePlayerBecameInvalid (XboxPlayer xboxPlayer)
	{
		if (Owner.ActivePlayer.UserId > -1) {
			this.Log ("ActivePlayer became invalid: {0}. Goto ReEngage state.", xboxPlayer);
			Owner.GotoState (XboxSinglePlayerEngager.StateTypes.ReEngage);
		} else {
			this.Log ("ActivePlayer became invalid and UserId is invalid: {0}. Goto NotSet state.", xboxPlayer);
			Owner.GotoState (XboxSinglePlayerEngager.StateTypes.NotSet);
		}
	}
	#endif
}
