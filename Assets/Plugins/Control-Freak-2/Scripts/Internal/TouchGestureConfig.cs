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

namespace ControlFreak2.Internal
{
	
// ----------------------
[System.Serializable]
public class TouchGestureConfig
	{
	public int 
		maxTapCount;
	public bool		
		cleanTapsOnly,	
		detectLongPress,
		detectLongTap,
		//reportAllTaps,
		//dontReportPressDuringTap,
		//dontReportPressDuringLongTap,
		endLongPressWhenMoved,
		endLongPressWhenSwiped;
		
	public DirMode	
		dirMode;
		
	public DirectionState.OriginalDirResetMode
		swipeOriginalDirResetMode;

	public DirConstraint
		swipeConstraint,
		swipeDirConstraint,
		scrollConstraint;



	public enum DirMode
		{	
		FourWay,
		EightWay
		}

	public enum DirConstraint
		{
		None,
		Horizontal,
		Vertical,
		Auto
		}

	// ------------------
	public TouchGestureConfig()
		{
		this.maxTapCount = 1;
		this.cleanTapsOnly = true;

		this.swipeOriginalDirResetMode = DirectionState.OriginalDirResetMode.On180;
		}
	}

}

//! \endcond
