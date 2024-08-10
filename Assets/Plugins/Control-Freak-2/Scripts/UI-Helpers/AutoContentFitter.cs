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

namespace ControlFreak2.UI
{
[ExecuteInEditMode()]
public class AutoContentFitter : MonoBehaviour
	{
	public RectTransform 
		source;

	

	public bool 
		autoWidth = false,
		autoHeight = false;

	public float 
		horzPadding = 10,
		vertPadding = 10;

	

	// ------------------
	void Update()
		{
		this.UpdatePreferredDimensions();
		}


	// ----------------------
	public void UpdatePreferredDimensions()
		{
		if (!this.autoWidth && !this.autoHeight)
			return;
	
		if (this.source == null)
			return;
	
		RectTransform tr = (RectTransform)this.transform;

		if (this.autoHeight)
				tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.source.rect.height + this.vertPadding);
	
		if (this.autoWidth)
			tr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.source.rect.width + this.horzPadding);

	
		}

	}
}

//! \endcond
