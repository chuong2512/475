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

[System.Serializable]
public abstract class InputBindingBase : IBindingContainer
	{
	public bool 
		enabled;

	[System.NonSerialized] 
	InputBindingBase 
		parent;
	

	// ------------------
	public InputBindingBase(InputBindingBase parent)
		{
		this.enabled	= false;
		this.parent		= parent;
		}


	// ------------------
	public InputBindingBase GetParent()
		{
		return this.parent;
		}
	
	// ------------------
	public void Enable()
		{
		for (InputBindingBase b = this; b != null; b = b.parent)
			b.enabled = true;	
		}

	// ----------------
	public bool IsEnabledInHierarchy()
		{
		InputBindingBase binding = this;
		do {
			if (!binding.enabled)
				return false;
			} while ((binding = binding.parent) != null);
		
		return true;
		}


	// -------------------
	public void GetSubBindingDescriptions(
		BindingDescriptionList			descList, 
		Object 							undoObject, 
		string 							parentMenuPath) 
		{
		this.OnGetSubBindingDescriptions(descList, undoObject, parentMenuPath);
		}
		
	// -------------------------
	virtual protected void OnGetSubBindingDescriptions(
		BindingDescriptionList			descList, 
		Object 							undoObject, 
		string 							parentMenuPath) 
		{
		}


	public bool IsBoundToKey			(KeyCode key, InputRig rig)			{ return this.OnIsBoundToKey(key, rig); }
	public bool IsBoundToAxis			(string axisName, InputRig rig)		{ return this.OnIsBoundToAxis(axisName, rig); }
	public bool IsEmulatingTouches		()						{ return this.OnIsEmulatingTouches(); }
	public bool IsEmulatingMousePosition()						{ return this.OnIsEmulatingMousePosition(); }
		
	virtual protected bool OnIsBoundToKey				(KeyCode key, InputRig rig)			{ return false; }
	virtual protected bool OnIsBoundToAxis				(string axisName, InputRig rig)		{ return false; }
	virtual protected bool OnIsEmulatingTouches			()						{ return false; }
	virtual protected bool OnIsEmulatingMousePosition	()						{ return false; }

	}
}

//! \endcond
