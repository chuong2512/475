/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

//! \cond

using UnityEngine;

namespace ControlFreak2
{
abstract public class GameState : MonoBehaviour 
	{
	protected GameState
		parentState,
		subState;
	protected bool
		isRunning;

		
		
	// -------------------
	public bool IsRunning()
		{ return this.isRunning; }


	// --------------------
	virtual protected void OnStartState(GameState parentState)
		{
		this.parentState	= parentState;		
		this.isRunning		= true;
		}


	// -------------------
	virtual protected void OnExitState()			
		{ 
		this.isRunning = false;

		if (this.subState != null) 
			this.subState.OnExitState();

		}

	virtual protected void OnPreSubStateStart(GameState prevState, GameState nextState) {}
	virtual protected void OnPostSubStateStart(GameState prevState, GameState nextState) {}

	virtual protected void OnUpdateState()			
		{ if (this.subState != null) this.subState.OnUpdateState(); }

	virtual protected void OnFixedUpdateState()	
		{ if (this.subState != null) this.subState.OnFixedUpdateState(); }


	// --------------------
	public void StartSubState(GameState state)
		{
		if (this.FindStateInHierarchy(state))
			{
			throw new System.Exception("Gamestate (" + this.name + ") tries to start sub state (" + state.name + ") that's already running!");
			}

		GameState oldState = this.subState;

		this.OnPreSubStateStart(oldState, state);

		if (this.subState != null)
			this.subState.OnExitState();

		if ((this.subState = state) != null)
			this.subState.OnStartState(this);
		
		this.OnPostSubStateStart(oldState, state);
		}

	// -----------------
	protected bool FindStateInHierarchy(GameState state)
		{
		if (state == null)
			return false;

		for (GameState s = this; s != null; s = s.parentState)
			{
			if (s == state)
				return true;
			}
	
		return false;
		}


	// ------------------
	public void EndState()
		{
		if (this.parentState != null)
			this.parentState.EndSubState();		
		}

	// -------------------
	public void EndSubState()
		{	
		this.StartSubState(null);
		}


	// -----------------
	public GameState GetSubState()
		{ return this.subState; }
		
	// -----------------	
	public bool IsSubStateRunning()
		{ return (this.subState != null); }




	}
}

//! \endcond

