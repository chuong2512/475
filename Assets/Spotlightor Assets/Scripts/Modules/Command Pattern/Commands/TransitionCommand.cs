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

public class TransitionCommand : CoroutineCommandBehavior
{
	public enum TransitionTypes
	{
		In,
		Out,
		InAndOut,
	}

	public TransitionManager transition;
	public TransitionTypes transitionType;
	public float inAndOutWaitTime = 3;
	[UnityEngine.Serialization.FormerlySerializedAs ("waitForTransitionOut")]
	public bool waitTransitionComplete = true;

	protected override IEnumerator CoroutineCommand ()
	{
		if (transitionType == TransitionTypes.In || transitionType == TransitionTypes.InAndOut) {
			transition.TransitionIn ();
			if (waitTransitionComplete) {
				while (transition.State != TransitionManager.StateTypes.In)
					yield return null;
			}
		}

		if (transitionType == TransitionTypes.InAndOut)
			yield return new WaitForSeconds (inAndOutWaitTime);

		if (transitionType == TransitionTypes.Out || transitionType == TransitionTypes.InAndOut) {
			transition.TransitionOut ();
			if (waitTransitionComplete) {
				while (transition.State != TransitionManager.StateTypes.Out)
					yield return null;
			}
		}
	}
}
