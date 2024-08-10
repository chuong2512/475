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
public class RandomFloatOnStateEnter : StateMachineBehaviour
{
	public float min = 0;
	public float max = 3;
	public bool useIntValue = true;
	[SingleLineLabel ()]
	public string
		floatName = "IdleType";

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		float randomValue = useIntValue ? Random.Range ((int)min, (int)max + 1) : Random.Range (min, max);
		animator.SetFloat (floatName, randomValue);
	}
}
#endif
