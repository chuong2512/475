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

public class GoFsmController : MonoBehaviour
{
	public GoFsmState initialState;
	private Fsm<GoFsmController, int> finiteStateMachine;

	public Fsm<GoFsmController, int> FiniteStateMachine {
		get {
			if (finiteStateMachine == null) {
				finiteStateMachine = new Fsm<GoFsmController, int> (this);

				List<GoFsmState> states = new List<GoFsmState> (GetComponentsInChildren<GoFsmState> (true));
				if (initialState != null && !states.Contains (initialState))
					states.Add (initialState);

				if (states.Count > 0) {
					states.ForEach (state => state.gameObject.SetActive (false));

					finiteStateMachine.AddStates (states.ToArray ());
					if (initialState == null && states.Count > 0)
						initialState = states [0];

					finiteStateMachine.GotoState (initialState.StateId);
				}
			}
			return finiteStateMachine;
		}
	}

	public void Awake ()
	{
		GotoState (initialState);
	}

	public void GotoState (GoFsmState state)
	{
		FiniteStateMachine.GotoState (state.StateId);
	}

}
