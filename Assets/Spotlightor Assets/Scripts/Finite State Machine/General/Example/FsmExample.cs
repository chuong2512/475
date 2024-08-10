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

[RequireComponent(typeof(FsmExampleIdleState))]
[RequireComponent(typeof(FsmExampleAttackState))]
public class FsmExample : MonoBehaviour
{
	public enum StateTypes
	{
		Invalid,
		Idle,
		Attack
	}
	private Fsm<FsmExample, StateTypes> fsm;

	public Fsm<FsmExample, StateTypes> StateMachine {
		get { return this.fsm; }
	}
	
	public FsmExampleState CurrentState {
		get { return fsm != null ? fsm.CurrentState as FsmExampleState : null; }
	}

	// Use this for initialization
	void Start ()
	{
		fsm = new Fsm<FsmExample, FsmExample.StateTypes> (this);
		StateMachine.AddState (GetComponent<FsmExampleIdleState> ());
		StateMachine.AddState (GetComponent<FsmExampleAttackState> ());
		StateMachine.GotoState (StateTypes.Idle);
	}
	
	void Update ()
	{
		if (ControlFreak2.CF2Input.GetKeyDown (KeyCode.Space)) 
			CurrentState.Jump();
	}
}
