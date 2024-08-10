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

public class MultiTransitionsManager : TransitionManager
{
	public List<TransitionManager> childTransitions = new List<TransitionManager> ();
	private bool hasSetupEvents = false;

	protected override void DoTransitionIn (bool instant, StateTypes prevStateType)
	{
		if (!instant)
			SetupEventsIfNeeded ();

		for (int i = 0; i < childTransitions.Count; i++) {
			TransitionManager transition = childTransitions [i];
			transition.TransitionIn (instant);
		}

		if (instant)
			TransitionInComplete ();
	}

	protected override void DoTransitionOut (bool instant, StateTypes prevStateType)
	{
		if (!instant)
			SetupEventsIfNeeded ();

		for (int i = 0; i < childTransitions.Count; i++) {
			TransitionManager transition = childTransitions [i];
			transition.TransitionOut (instant);
		}

		if (instant)
			TransitionOutComplete ();
	}

	private void SetupEventsIfNeeded ()
	{
		if (!hasSetupEvents) {
			for (int i = 0; i < childTransitions.Count; i++) {
				TransitionManager transition = childTransitions [i];
				transition.TransitionInCompleted += HandleChildTransitionInCompleted;
				transition.TransitionOutCompleted += HandleChildTransitionOutCompleted;
			}
			hasSetupEvents = true;
		}
	}

	void HandleChildTransitionInCompleted (TransitionManager source)
	{
		if (State == StateTypes.TransitionIn && AreChildTransitionsInState (StateTypes.In))
			TransitionInComplete ();
	}


	void HandleChildTransitionOutCompleted (TransitionManager source)
	{
		if (State == StateTypes.TransitionOut && AreChildTransitionsInState (StateTypes.Out))
			TransitionOutComplete ();
	}

	private bool AreChildTransitionsInState (TransitionManager.StateTypes stateType)
	{
		for (int i = 0; i < childTransitions.Count; i++) {
			if (childTransitions [i].State != stateType)
				return false;
		}
		return true;
	}

	void Reset ()
	{
		childTransitions = new List<TransitionManager> ();
		childTransitions.AddRange (GetComponentsInChildren<TransitionManager> (true));
		childTransitions.Remove (this);
	}
}
