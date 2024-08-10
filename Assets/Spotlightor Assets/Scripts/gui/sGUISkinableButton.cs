/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public abstract class sGUISkinableButton : MouseEventDispatcher
{
	public delegate void StateChangedEventHandler (sGUISkinableButton button);

	public event StateChangedEventHandler StateChanged;
	#region Fields
	public sGUIButtonSkin buttonSkin = new sGUIButtonSkin ();
	protected sGUIButtonSkin.ButtonState state = sGUIButtonSkin.ButtonState.normal;
	#endregion

	#region Properties
	public sGUIButtonSkin.ButtonState ButtonState {
		get { return state; }
		protected set {
			bool stateChange = state != value;
			state = value;
			if (stateChange) {
				if (StateChanged != null)
					StateChanged (this);
			}
		}
	}

	public bool ButtonEnabled {
		get { return enabled; }
		set {
			if (!enabled && value == false) {
				OnDisable ();
			}
			enabled = value;
		}
	}
	#endregion

	#region Functions

	public void ChangeButtonSkin (sGUIButtonSkin newSkin)
	{
		buttonSkin = newSkin;
		ChangeButtonAppearanceUseSkinByState (buttonSkin, state);
	}

	protected abstract void ChangeButtonAppearanceUseSkinByState (sGUIButtonSkin skin, sGUIButtonSkin.ButtonState state);

	void OnEnable ()
	{
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.normal);
		ButtonState = sGUIButtonSkin.ButtonState.normal;
	}

	void OnDisable ()
	{
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.disable);
		ButtonState = sGUIButtonSkin.ButtonState.disable;
	}

	protected override void OnHoverIn (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnHoverIn (pointerMessageData);
		
		if (!enabled)
			return;
		
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.over);
		ButtonState = sGUIButtonSkin.ButtonState.over;
	}

	protected override void OnHoverOut (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnHoverOut (pointerMessageData);
		
		if (!enabled)
			return;
		
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.normal);
		ButtonState = sGUIButtonSkin.ButtonState.normal;
	}

	protected override void OnPressDown (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnPressDown (pointerMessageData);
		
		if (!enabled)
			return;
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.down);
		ButtonState = sGUIButtonSkin.ButtonState.down;
	}

	protected override void OnSelect (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnSelect (pointerMessageData);
		
		if (!enabled)
			return;
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.over);
		ButtonState = sGUIButtonSkin.ButtonState.over;
	}
	
	protected override void OnPressUp (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnPressUp (pointerMessageData);
		
		if (!enabled)
			return;
		ChangeButtonAppearanceUseSkinByState (buttonSkin, sGUIButtonSkin.ButtonState.over);
		ButtonState = sGUIButtonSkin.ButtonState.over;
	}
	#endregion
}
