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

using ControlFreak2;

namespace ControlFreak2.Internal
{
// ------------------------
/// Mouse Position binding.
// -------------------------

[System.Serializable]
public class EmuTouchBinding : InputBindingBase
	{
	private int emuTouchId;

	// ----------------------
	public EmuTouchBinding(InputBindingBase parent = null) : base(parent)
		{
		this.enabled		= false;
		this.emuTouchId 	= -1;
		}

		
	// ----------------
	public void CopyFrom(EmuTouchBinding b)
		{
		if ((b == null)) 
			return;
		
		if ((this.enabled = b.enabled))
			{
			this.Enable();
			}
		}		



	// ----------------------
	public void SyncState(TouchGestureBasicState touchState, InputRig rig)
		{
		if (!this.enabled || (rig == null))
			return;
	
		rig.SyncEmuTouch(touchState, ref this.emuTouchId);
		}
		
	// ------------------
	override protected bool OnIsEmulatingTouches() 
		{ return this.enabled; }

	
	}



}

//! \endcond
