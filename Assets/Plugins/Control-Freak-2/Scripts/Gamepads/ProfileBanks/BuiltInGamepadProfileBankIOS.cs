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

public class BuiltInGamepadProfileBankIOS : BuiltInGamepadProfileBank
	{
	// ------------------
	public BuiltInGamepadProfileBankIOS() : base()
		{
		this.genericProfile = new GamepadProfile(
				"MFi Controller", 
				"MFi Controller",
				GamepadProfile.ProfileMode.Normal,
				null, null,		
		
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// LS
				GamepadProfile.JoystickSource.Axes(2, true, 3, false),	// RS
				GamepadProfile.JoystickSource.Dpad(4, 5, 6, 7),				// Dpad
	
				GamepadProfile.KeySource.Key(14),		// A
				GamepadProfile.KeySource.Key(13),		// B
				GamepadProfile.KeySource.Key(15),		// X
				GamepadProfile.KeySource.Key(12),		// Y
				
				GamepadProfile.KeySource.Empty(),		// Select	
				GamepadProfile.KeySource.Key(0),		// Start (Pause) - Escape?
	
				GamepadProfile.KeySource.KeyAndPlusAxis(8, 8),		// L1
				GamepadProfile.KeySource.KeyAndPlusAxis(9, 9),		// R1
				GamepadProfile.KeySource.KeyAndPlusAxis(10, 10), 		// L2 (digital only)
				GamepadProfile.KeySource.KeyAndPlusAxis(11, 11),		// R2 (digital only)
				GamepadProfile.KeySource.Key(-1),		// L3
				GamepadProfile.KeySource.Key(-1)			// R3
				);

			
		this.profiles = new GamepadProfile[]
			{
			// Startus XL ------------------------

			new GamepadProfile(
				"Startus XL", 
				"Startus XL",
				GamepadProfile.ProfileMode.Normal,
				null, null,		
		
				GamepadProfile.JoystickSource.Axes(0, true, 1, false),	// LS
				GamepadProfile.JoystickSource.Axes(2, true, 3, false),	// RS
				GamepadProfile.JoystickSource.Dpad(4, 5, 6, 7),				// Dpad
	
				GamepadProfile.KeySource.Key(14),		// A
				GamepadProfile.KeySource.Key(13),		// B
				GamepadProfile.KeySource.Key(15),		// X
				GamepadProfile.KeySource.Key(12),		// Y
				
				GamepadProfile.KeySource.Empty(),		// Select	
				GamepadProfile.KeySource.Key(-1),		// Start (Pause) - Escape?
	
				GamepadProfile.KeySource.KeyAndPlusAxis(8, 8),		// L1
				GamepadProfile.KeySource.KeyAndPlusAxis(9, 9),		// R1
				GamepadProfile.KeySource.Key(10), 		// L2 (digital only)
				GamepadProfile.KeySource.Key(11),		// R2 (digital only)
				GamepadProfile.KeySource.Key(-1),		// L3
				GamepadProfile.KeySource.Key(-1)			// R3
				)	
		
			};

		}
	}
}

//! \endcond
