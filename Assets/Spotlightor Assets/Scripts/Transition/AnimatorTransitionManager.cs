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

public class AnimatorTransitionManager : TransitionManager
{
	private static int inStateHash = Animator.StringToHash ("Base.In");
	private static int outStateHash = Animator.StringToHash ("Base.Out");
	private static int isInHash = Animator.StringToHash ("IsIn");

	protected override void OnEnable ()
	{
		// Animator.Play will take effect at next frame. So instant transition will make Animator stays at the wrong state.
		// Ensure the in & out states when enabled.
		if (State == StateTypes.Out) 
			GetComponent<Animator> ().Play (outStateHash, 0, 1);
		else if (State == StateTypes.In)
			GetComponent<Animator> ().Play (inStateHash, 0, 1);

		base.OnEnable ();
	}

	protected override void DoTransitionIn (bool instant, StateTypes prevStateType)
	{
		StopCoroutine ("WaitForStatePlayEnded");

		if (instant) {
			GetComponent<Animator> ().SetBool (isInHash, true);
			GetComponent<Animator> ().Play (inStateHash, 0, 1);
			TransitionInComplete ();
		} else {
			if (prevStateType == StateTypes.Out)
				GetComponent<Animator> ().Play (inStateHash, 0, 0);

			GetComponent<Animator> ().SetBool (isInHash, true);
			StartCoroutine ("WaitForStatePlayEnded", inStateHash);
		}
	}

	protected override void DoTransitionOut (bool instant, StateTypes prevStateType)
	{
		StopCoroutine ("WaitForStatePlayEnded");

		GetComponent<Animator> ().SetBool (isInHash, false);

		if (instant) {
			GetComponent<Animator> ().Play (outStateHash, 0, 1);
			TransitionOutComplete ();
		} else 
			StartCoroutine ("WaitForStatePlayEnded", outStateHash);
	}

	private IEnumerator WaitForStatePlayEnded (int stateNameHash)
	{
		while (GetComponent<Animator>().IsInTransition(0) || 
#if UNITY_5
		       GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash != stateNameHash || 
#endif
#if !UNITY_5
		       GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash != stateNameHash || 
#endif
		       GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)

			yield return null;

		if (stateNameHash == inStateHash)
			TransitionInComplete ();
		else if (stateNameHash == outStateHash)
			TransitionOutComplete ();
	}
}
