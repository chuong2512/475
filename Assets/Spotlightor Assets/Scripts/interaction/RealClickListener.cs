/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class RealClickListener : GenericButton
{
	private const float DragClickPointerPixelOffset = 16;
	private const float DragClickPointerInchOffset = 0.12f;// 0.3cm
	private const float MaxTapTime = 0.5f;
	[FormerlySerializedAs("ignoreMouseMovement")]
	public bool
		allowDragClick = true;
	public bool onlyTapClick = false;
	private Vector2 pressDownPointerPosition = Vector2.zero;
	private float pressDownTime = 0;

	public override bool Interactable {
		get {
			return GetComponent<Collider> () != null && GetComponent<Collider> ().enabled;
		}
		set {
			if (GetComponent<Collider> () != null && Interactable != value) 
				GetComponent<Collider> ().enabled = value;
		}
	}

	protected virtual void OnPressDown (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		this.pressDownPointerPosition = pointerMessageData.pointerPosition;
		this.pressDownTime = Time.time;

		OnPressed ();
	}

	protected virtual void OnSelect (InteractionMessagesSender.PointerMessageData pointerMessageData)
	{
		bool isValidClick = true;
		if (allowDragClick == false) {
			bool isPointerOffsetSmallEnough = false;
			float pixelOffset = Vector2.Distance (this.pressDownPointerPosition, pointerMessageData.pointerPosition);
			if (ControlFreak2.CFScreen.dpi == 0)
				isPointerOffsetSmallEnough = pixelOffset < DragClickPointerPixelOffset;
			else 
				isPointerOffsetSmallEnough = pixelOffset / ControlFreak2.CFScreen.dpi < DragClickPointerInchOffset;

			isValidClick = isValidClick && isPointerOffsetSmallEnough;
		}
		if (onlyTapClick == true) 
			isValidClick = isValidClick && (Time.time - pressDownTime) < MaxTapTime;

		if (isValidClick)
			OnValidClick ();
	}

	protected virtual void OnValidClick ()
	{
		SendMessage ("OnMouseClick", SendMessageOptions.DontRequireReceiver);

		OnClicked ();
	}
}
