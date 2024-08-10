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

using UnityEngine;

using ControlFreak2;

namespace ControlFreak2.Demos
{

public class MultiDemoManager : ControlFreak2.GameState 
	{	
	const string 
		MAIN_MENU_SCENE_NAME = "CF2-Multi-Demo-Manager";


	public DemoMainState
		mainMenuState;


	// ----------------
	public void EnterMainMenu()
		{
		this.StartSubState(this.mainMenuState);

		//Application.LoadLevel(MAIN_MENU_SCENE_NAME);
		}

	// ------------------	
	private void Start()
		{
		this.OnStartState(null);
		}


	// --------------------
	void Update()
		{	
		if (this.IsRunning())
			this.OnUpdateState();
		}


	// ------------------
	protected override void OnStartState(GameState parentState)
		{		
		base.OnStartState(parentState);

		this.gameObject.SetActive(true);

		this.EnterMainMenu();
		}


	// -----------------
	protected override void OnExitState()
		{
		base.OnExitState();

		this.gameObject.SetActive(false);
		}	

	
	}
}
