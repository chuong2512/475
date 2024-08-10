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
using System;

// T: owner class
// U: state id class(enum, string, int...)
public class Fsm<T,U> where T : class
{
	public delegate void StateChangedEventHandler (U newStateId,U previousStateId);

	public delegate void StateGenericEventHandler (U stateId);

	public event StateChangedEventHandler StateChange;
	public event StateGenericEventHandler StateBegin;
	public event StateGenericEventHandler StateEnd;

	private IFsmState<T,U> currentState;
	private List<IFsmState<T,U>> stackedStates;
	private Dictionary<U, IFsmState<T,U>> states;
	private T owner;

	// TODO There should be a default state or "NULL" state.
	public IFsmState<T,U> CurrentState {
		get { return currentState;}
	}
	
	public Fsm (T owner)
	{
		this.owner = owner;
		states = new Dictionary<U, IFsmState<T, U>> ();
	}
	
	public void AddStates (IFsmState<T,U>[] states)
	{
		foreach (IFsmState<T,U> state in states)
			AddState (state);
	}

	public void AddState (IFsmState<T,U> state)
	{
		state.Owner = this.owner;
		states [state.StateId] = state;
	}
	
	public void RemoveState (IFsmState<T,U> state)
	{
		if (states.ContainsKey (state.StateId)) {
			states.Remove (state.StateId);
			if (stackedStates != null) {
				int i = 0;
				while (i < stackedStates.Count) {
					if (stackedStates [i] == state)
						stackedStates.RemoveAt (i);
					else
						i++;
				}
			}
			state.Owner = null;
		}
	}
	
	public IFsmState<T, U> GetState (U stateId)
	{
		if (states.ContainsKey (stateId))
			return states [stateId];
		else
			return null;
	}
	
	public void ForceGotoState (U newStateId)
	{
		DoGotoState (newStateId, true);
	}
	
	public void GotoState (U newStateId)
	{
		DoGotoState (newStateId, false);
	}
	
	private void DoGotoState (U newStateId, bool forceStateTransition)
	{
		if (states.ContainsKey (newStateId)) {
			DoStateTransition (states [newStateId], forceStateTransition);
		} else
			Debug.LogError (string.Format ("Fail to go to state[{0}] because no state with this ID added.", newStateId.ToString ()));
	}
	
	private void DoStateTransition (IFsmState<T, U> newState, bool forceStateTransition)
	{
		IFsmState<T, U> previousState = currentState;
		
		if (currentState != newState || forceStateTransition) {
			U newStateId = newState != null ? newState.StateId : default(U);
			U previousStateId = previousState != null ? previousState.StateId : default(U);

			if (StateEnd != null)
				StateEnd (previousStateId);
			if (currentState != null)
				currentState.EndState (newStateId);
			
			currentState = newState;

			if (StateBegin != null)
				StateBegin (newStateId);
			if (StateChange != null)
				StateChange (newStateId, previousStateId);

			if (currentState != null)
				currentState.BeginState (previousStateId);
		}
	}
	
	public void PushState (U NewStateId)
	{
		if (stackedStates == null)
			stackedStates = new List<IFsmState<T, U>> ();
		
		GotoState (NewStateId);
		if (currentState != null)
			stackedStates.Add (currentState);
	}
	
	public IFsmState<T, U> ForcePopState ()
	{
		return DoPopState (true);
	}
	
	public IFsmState<T, U> PopState ()
	{
		return DoPopState (false);
	}
	
	public IFsmState<T, U> DoPopState (bool forceStateTransition)
	{
		if (stackedStates != null && stackedStates.Count > 0) {
			IFsmState<T,U> popedState = stackedStates [stackedStates.Count - 1];
			stackedStates.RemoveAt (stackedStates.Count - 1);
			
			DoStateTransition (popedState, forceStateTransition);
			
			return popedState;
		} else {
			Debug.LogWarning ("No state to pop!");
			return null;
		}
	}
	
	public bool IsInState (U stateId)
	{
		return currentState != null && states.ContainsKey (stateId) && currentState == states [stateId];
	}
}
