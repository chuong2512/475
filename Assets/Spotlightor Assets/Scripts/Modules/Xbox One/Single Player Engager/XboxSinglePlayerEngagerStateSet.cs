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

public class XboxSinglePlayerEngagerStateSet : XboxSinglePlayerEngagerState
{
	public override XboxSinglePlayerEngager.StateTypes StateId {
		get { return XboxSinglePlayerEngager.StateTypes.Set; }
	}

	public override void BeginState (XboxSinglePlayerEngager.StateTypes previousState)
	{
		base.BeginState (previousState);

		if (Owner.ActivePlayer.IsControllerUserPairValid) {
			Owner.ActivePlayer.BecameInvalid += HandleActivePlayerBecameInvalid;
		} else {
			this.Log ("ActivePlayer not valid: {0}. Go back to NotSet state.", Owner.ActivePlayer);
			Owner.GotoState (XboxSinglePlayerEngager.StateTypes.NotSet);
		}
	}

	public override void EndState (XboxSinglePlayerEngager.StateTypes newState)
	{
		Owner.ActivePlayer.BecameInvalid -= HandleActivePlayerBecameInvalid;

		base.EndState (newState);
	}

	void HandleActivePlayerBecameInvalid (XboxPlayer xboxPlayer)
	{
		this.Log ("ActivePlayer became invalid: {0}. Goto NotSet state.", xboxPlayer);

		Owner.GotoState (XboxSinglePlayerEngager.StateTypes.NotSet);
	}
}
