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

public class MouseIntro : SmartUnityGuiLabel
{
	private const float SystemCursorWidth = 6;
	private const float SystemCursorHeight = 9;
	private float defaultOffsetX;
	private float defaultOffsetY;
	private bool mouseOverThisFrame = false;// MouseExit is not reliable, let's do it myself.
	private bool mouseOverLastFrame = false;
	
	#region Functions
	
	protected virtual void Awake ()
	{
		defaultOffsetX = offsetX;
		defaultOffsetY = offsetY;
		TransitionOut (true);
	}
	
	void OnMouseEnter ()
	{
		TransitionIn ();
	}
	
	void OnMouseReallyExit ()
	{
		TransitionOut ();
	}

	void OnMouseOver ()
	{
		mouseOverThisFrame = true;
	}

	void Update ()
	{
		if (!mouseOverThisFrame && mouseOverLastFrame) {
			OnMouseReallyExit ();
		}
		mouseOverLastFrame = mouseOverThisFrame;
		mouseOverThisFrame = false;
		
		Vector3 mousePos = ControlFreak2.CF2Input.mousePosition;
		UpdateDrawPositionOnScreen (mousePos);
		
		if (DrawAtRight)
			offsetX = defaultOffsetX + SystemCursorWidth;
		else
			offsetX = defaultOffsetX;
		if (DrawAtBottom)
			offsetY = defaultOffsetY + SystemCursorHeight;
		else
			offsetY = defaultOffsetY;
	}

	public override void TransitionIn (bool instant)
	{
		this.Update();

		base.TransitionIn (instant);
	}
	#endregion
}
