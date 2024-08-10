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

public class ChildrenTransitionsManager : MultiTransitionsManager
{
	[Header ("自动从下一层级（不含更深层）获得Transition，忽略预设ChildTransitions")]
	public bool autoGetChildTransitions = true;
	public float childDelayInStep = 0;
	public float childDelayOutStep = 0;

	protected override void DoTransitionIn (bool instant, StateTypes prevStateType)
	{
		if (autoGetChildTransitions)
			GetChildTransitions ();

		SetupChildTransitionsDelay ();
		base.DoTransitionIn (instant, prevStateType);
	}

	protected override void DoTransitionOut (bool instant, StateTypes prevStateType)
	{
		if (autoGetChildTransitions)
			GetChildTransitions ();

		SetupChildTransitionsDelay ();
		base.DoTransitionOut (instant, prevStateType);
	}

	private void GetChildTransitions ()
	{
		childTransitions.Clear ();
		for (int i = 0; i < transform.childCount; i++) {
			TransitionManager childTransition = transform.GetChild (i).GetComponent<TransitionManager> ();
			if (childTransition != null)
				childTransitions.Add (childTransition);
		}
	}

	private void SetupChildTransitionsDelay ()
	{
		for (int i = 0; i < childTransitions.Count; i++) {
			TransitionManager childTransition = childTransitions [i];
			childTransition.delayIn = childDelayInStep * (float)i;
			childTransition.delayOut = childDelayOutStep * (float)i;
		}
	}
}
