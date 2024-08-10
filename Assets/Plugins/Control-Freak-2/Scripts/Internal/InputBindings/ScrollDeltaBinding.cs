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

using ControlFreak2;

namespace ControlFreak2.Internal
{


// ----------------------
//! Scroll Delta binding.
// ----------------------

[System.Serializable]
public class ScrollDeltaBinding : InputBindingBase
	{
	public AxisBinding 
		deltaBinding;
		
	public DigitalBinding
		positiveDigitalBinding,	
		negativeDigitalBinding;


	// ------------------ 
	public ScrollDeltaBinding(InputBindingBase parent = null) : base(parent)
 		{
		this.deltaBinding 			= new AxisBinding(InputRig.DEFAULT_VERT_SCROLL_WHEEL_NAME, false, this);
		this.positiveDigitalBinding = new DigitalBinding(this);
		this.negativeDigitalBinding = new DigitalBinding(this);
		}
		

	// ------------------ 
	public ScrollDeltaBinding(string axisName, bool enabled = false, InputBindingBase parent = null) : base(parent)
 		{
		this.enabled				= enabled;
		this.deltaBinding 			= new AxisBinding(axisName, enabled, this);
		this.positiveDigitalBinding = new DigitalBinding(this);
		this.negativeDigitalBinding = new DigitalBinding(this);
		}



	// --------------------
	public void CopyFrom(ScrollDeltaBinding b)
		{
		if (this.enabled = b.enabled)
			{
			this.Enable();

			this.deltaBinding.			CopyFrom(b.deltaBinding);
			this.positiveDigitalBinding.CopyFrom(b.positiveDigitalBinding);
			this.negativeDigitalBinding.CopyFrom(b.negativeDigitalBinding);
			}
		}


	// --------------------
	override protected void OnGetSubBindingDescriptions(BindingDescriptionList descList, 
		Object undoObject, string parentMenuPath) 
		{
		descList.Add(this.deltaBinding, InputRig.InputSource.Scroll,  "Delta Binding", parentMenuPath, undoObject);

		descList.Add(this.positiveDigitalBinding, "Positive Digital", parentMenuPath, undoObject);
		descList.Add(this.negativeDigitalBinding, "Negative Digital", parentMenuPath, undoObject); 
		}


	// -------------------
	public void SyncScrollDelta(int delta, InputRig rig)
		{
		if ((rig == null) || !this.enabled)
			return;
			
		this.deltaBinding.SyncScroll(delta, rig);
		
		if (delta != 0)
			{
			if (delta > 0)
				this.positiveDigitalBinding.Sync(true, rig);
			else
				this.negativeDigitalBinding.Sync(true, rig);
			}
		}

	// --------------------
	override protected bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
		if (!this.enabled)
			return false;

		return (
			this.deltaBinding			.IsBoundToAxis(axisName, rig) ||
			this.positiveDigitalBinding	.IsBoundToAxis(axisName, rig) ||
			this.negativeDigitalBinding	.IsBoundToAxis(axisName, rig) );
		}

	// -----------------
	override protected bool OnIsBoundToKey(KeyCode keyCode, InputRig rig)
		{
		if (!this.enabled)
			return false;

		return (
			this.deltaBinding			.IsBoundToKey(keyCode, rig) ||
			this.positiveDigitalBinding	.IsBoundToKey(keyCode, rig) ||
			this.negativeDigitalBinding	.IsBoundToKey(keyCode, rig) );
		}


	}

}

//! \endcond
