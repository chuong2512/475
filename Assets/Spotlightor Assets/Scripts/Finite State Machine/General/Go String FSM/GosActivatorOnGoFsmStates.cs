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

public class GosActivatorOnGoFsmStates : GosActivatorOnCondition
{
	public List<GoFsmState> activeStates;
	private Fsm<GoFsmController,int> fsm;

	protected override bool HasMetCondition {
		get {
			bool hasMetCondition = false;
			if (fsm != null) {
				foreach (GoFsmState state in activeStates) {
					if ((fsm.CurrentState as GoFsmState) == state) {
						hasMetCondition = true;
					}
				}
			}
			return hasMetCondition;
		}
	}

	void Start ()
	{
		if (activeStates.Count > 0) {
			fsm = activeStates [0].Owner.FiniteStateMachine;

			ActivateObjectsOnCondition ();
			fsm.StateChange += HandleFsmStateChange;
		}
	}

	void OnDisable ()
	{
		if (fsm != null)
			fsm.StateChange -= HandleFsmStateChange;
	}

	void HandleFsmStateChange (int newStateId, int previousStateId)
	{
		ActivateObjectsOnCondition ();
	}
}
