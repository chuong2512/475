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

[RequireComponent(typeof(Collider))]
public class sUiButton : MouseEventDispatcher
{
	public enum StateTypes
	{
		Disable = -1,
		Normal = 0,
		Over = 1,
		Down = 2,
	}

	public delegate void StateChangedEventHandler (sUiButton button,StateTypes stateBefore,StateTypes stateNow);
	
	public event StateChangedEventHandler StateChanged;

	private StateTypes stateType = StateTypes.Normal;

	public StateTypes StateType {
		get { return stateType; }
		private set {
			if (stateType != value) {
				StateTypes stateBefore = stateType;
				stateType = value;

				if (StateChanged != null)
					StateChanged (this, stateBefore, stateType);
			}
		}
	}

	public bool ButtonEnabled {
		get { return this.Interactable;}
		set {
			if (ButtonEnabled != value) {
				this.Interactable = value;
				StateType = value ? StateTypes.Normal : StateTypes.Disable;
			}
		}
	}
	// Called only when collider enabled
	protected override void OnPressDown (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnPressDown (pointerMessageData);

		StateType = StateTypes.Down;
	}

	// Can still be called even the collider is disabled, if OnMouseDown before collider disable.
	protected override void OnPressUp (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnPressUp (pointerMessageData);

		if (ButtonEnabled) 
			StateType = IsHovering ? StateTypes.Over : StateTypes.Normal;
	}

	protected override void OnHoverIn (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnHoverIn (pointerMessageData);
		StateType = IsPressedDown ? StateTypes.Down : StateTypes.Over;
	}

	protected override void OnHoverOut (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		base.OnHoverOut (pointerMessageData);
		if (ButtonEnabled) 
			StateType = StateTypes.Normal;
		else
			StateType = StateTypes.Disable;
	}
}
