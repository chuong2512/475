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

namespace ControlFreak2.Demos
{

public class DemoMainState : ControlFreak2.GameState
	{
	public MultiDemoManager 
		multiDemoManager;

		
	public HelpBoxState
		helpBox; 

	public KeyCode		
		helpKey = KeyCode.Escape;



	// --------------------
	protected override void OnStartState(ControlFreak2.GameState parentState)
		{
		base.OnStartState(parentState);

		this.gameObject.SetActive(true);
		}


	// --------------------
	protected override void OnExitState()
		{
		base.OnExitState();

		this.gameObject.SetActive(false);
		}



	// -------------------
	protected override void OnUpdateState()
		{
		if ((this.helpBox != null) && !this.helpBox.IsRunning() && (this.helpKey != KeyCode.None) && CF2Input.GetKeyDown(this.helpKey))
			this.ShowHelpBox();				

		base.OnUpdateState();
		}


	// -------------------
	public void ExitToMainMenu()
		{
		if (this.multiDemoManager != null)
			this.multiDemoManager.EnterMainMenu();
		else
			Application.Quit();
		}


	// --------------------
	public void ShowHelpBox()
		{
		if (this.helpBox != null)
			this.helpBox.ShowHelpBox(this);
		}
	}
}