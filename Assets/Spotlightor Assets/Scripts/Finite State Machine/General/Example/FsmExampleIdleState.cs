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

public class FsmExampleIdleState : FsmExampleState
{
	public string idleMessage = "Idle";
	#region implemented abstract members of FsmExampleState

	public override FsmExample.StateTypes StateId {
		get {
			return FsmExample.StateTypes.Idle;
		}
	}

	public override void Jump ()
	{
		transform.SetUniformLocalScale (transform.localScale.x * 1.1f);
	}
	#endregion
	
	void OnMouseDown ()
	{
		Owner.StateMachine.GotoState (FsmExample.StateTypes.Attack);
	}
}
