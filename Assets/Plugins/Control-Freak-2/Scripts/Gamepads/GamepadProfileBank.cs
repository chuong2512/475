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
public class GamepadProfileBank : ScriptableObject
	{
#if UNITY_EDITOR
	private const string 
		DEFAULT_PROFILE_BANK_ASSET_PATH = "Assets/Plugins/Control-Freak-2/Data/CF2-Gamepad-Profiles.asset";
#endif
	
	public GamepadProfile[] profiles;

	

	// ----------------------
	public GamepadProfile GetProfile(string deviceIdentifier)
		{
		return null;
		}



#if UNITY_EDITOR	

	// ----------------
	static public GamepadProfileBank LoadDefaultBank()
		{
		GamepadProfileBank defaultBank = 
			(GamepadProfileBank)UnityEditor.AssetDatabase.LoadAssetAtPath(DEFAULT_PROFILE_BANK_ASSET_PATH, typeof(GamepadProfileBank));

		return defaultBank;
		}


	// --------------------
	static public void CreateDefaultProfileBank()
		{
		
		}

#endif
		

	}
}

//! \endcond
