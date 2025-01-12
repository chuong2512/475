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

namespace ControlFreak2.Demos.SwipeSlasher
{
public class SwipeSlasherUserControl : MonoBehaviour 
	{
	public SwipeSlasherChara 
		character;


	// ---------------
	void OnEnable()
		{
		if (this.character == null)
			this.character = this.GetComponent<SwipeSlasherChara>();
		}


	// -------------
	void Update()
		{
		if (this.character == null)
			return;


		if 	  (CF2Input.GetButtonDown("Left-Stab"))			this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_STAB);
		else if (CF2Input.GetButtonDown("Left-Slash-U"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_U);
		else if (CF2Input.GetButtonDown("Left-Slash-R"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_R);
		else if (CF2Input.GetButtonDown("Left-Slash-L"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_L);
		else if (CF2Input.GetButtonDown("Left-Slash-D"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_D);

		else if (CF2Input.GetButtonDown("Right-Stab"))			this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_STAB);
		else if (CF2Input.GetButtonDown("Right-Slash-U"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_U);
		else if (CF2Input.GetButtonDown("Right-Slash-R"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_R);
		else if (CF2Input.GetButtonDown("Right-Slash-L"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_L);
		else if (CF2Input.GetButtonDown("Right-Slash-D"))		this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_D);

		else if (CF2Input.GetButtonDown("Dodge-Right"))			this.character.ExecuteAction(SwipeSlasherChara.ActionType.DODGE_RIGHT);
		else if (CF2Input.GetButtonDown("Dodge-Left"))			this.character.ExecuteAction(SwipeSlasherChara.ActionType.DODGE_LEFT);

		}

	}
}
