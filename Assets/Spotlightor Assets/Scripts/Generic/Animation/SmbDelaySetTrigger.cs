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

#if UNITY_5
public class SmbDelaySetTrigger : StateMachineBehaviour
{
	public RandomRangeFloat delayTimeRange = new RandomRangeFloat (3, 5);
	public string triggerName = "";
	private float delayTime = 3;
	private float enterTime = 0;
	private bool triggered = false;

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		delayTime = delayTimeRange.RandomValue;
		enterTime = Time.time;
		triggered = false;
	}

	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		float timeElapsed = Time.time - enterTime;

		if (timeElapsed >= delayTime && triggered == false) {
			animator.SetTrigger (triggerName);
			triggered = true;
		}
	}
}
#else
public class SmbDelaySetTrigger
{
}
#endif
