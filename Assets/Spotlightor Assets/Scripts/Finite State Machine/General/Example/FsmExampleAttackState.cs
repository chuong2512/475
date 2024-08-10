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

public class FsmExampleAttackState : FsmExampleState
{
	public float rotateSpeed = 30;
	public RandomRangeFloat durationRange = new RandomRangeFloat (3, 5);
	#region implemented abstract members of FsmExampleState
	
	public override void BeginState (FsmExample.StateTypes previousState)
	{
		base.BeginState (previousState);
		StartCoroutine ("DelayAndGotoIdle");
	}
	
	public override void EndState (FsmExample.StateTypes newState)
	{
		base.EndState (newState);
		StopCoroutine ("DelayAndGotoIdle");
	}

	public override FsmExample.StateTypes StateId {
		get {
			return FsmExample.StateTypes.Attack;
		}
	}

	public override void Jump ()
	{
		Owner.transform.position += Owner.transform.forward * 0.3f;
	}
	#endregion
	
	void Update ()
	{
		transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
	}
	
	private IEnumerator DelayAndGotoIdle ()
	{
		yield return new WaitForSeconds(durationRange.RandomValue);
		Owner.StateMachine.GotoState (FsmExample.StateTypes.Idle);
	}
}
