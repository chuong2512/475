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
using System.Collections.Generic;

#if UNITY_EDITOR
using ControlFreak2Editor;
#endif

namespace ControlFreak2
{

public class TouchMarkerGUI : MonoBehaviour 
	{
	public Texture2D
		fingerMarker,
		pinchHintMarker,
		twistHintMarker;


	public static TouchMarkerGUI
		mInst;


	// -------------------
	public TouchMarkerGUI()
		{
		}

	// --------------------
	void OnEnable()
		{
		mInst = this;

#if UNITY_EDITOR
		if (this.fingerMarker == null)
			this.fingerMarker = CFEditorStyles.Inst.texFinger;

		if (this.pinchHintMarker == null)
			this.pinchHintMarker = CFEditorStyles.Inst.texPinchHint;

		if (this.twistHintMarker == null)
			this.twistHintMarker = CFEditorStyles.Inst.texTwistHint;
#endif

		}

	// ----------------
	void OnDisable()
		{
		if (mInst == this)
			mInst = null;
		}

	// ------------------
	void OnGUI()
		{
		if (CF2Input.activeRig == null)
			return;

		List<TouchControl> cList = CF2Input.activeRig.GetTouchControls();
		for (int i = 0; i < cList.Count; ++i)
			{
			SuperTouchZone c = cList[i] as SuperTouchZone;
			if (c != null)
				c.DrawMarkerGUI();
			}
		}


	
	}
}

//! \endcond 

