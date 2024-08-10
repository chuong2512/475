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

[RequireComponent(typeof(sGUISkinableButton))]
public class sGUIColorTintByButtonState : MonoBehaviour
{
	public Color colorNormal = Color.white;
	public Color colorOver = Color.white;
	public Color colorDown = Color.white;
	public Color colorDisable = Color.white;
	public Renderer targetRenderer;
	private sGUISkinableButton button;
	// Use this for initialization
	void Start ()
	{
		button = GetComponent<sGUISkinableButton> ();
		TintColorByButtonState ();
		
		button.StateChanged += OnButtonStateChanged;
	}

	void OnButtonStateChanged (sGUISkinableButton button)
	{
		TintColorByButtonState ();
	}
	
	private void TintColorByButtonState ()
	{
		if (targetRenderer == null)
			return;
		switch (button.ButtonState) {
		case sGUIButtonSkin.ButtonState.over:
			targetRenderer.material.color = colorOver;
			break;
		case sGUIButtonSkin.ButtonState.down:
			targetRenderer.material.color = colorDown;
			break;
		case sGUIButtonSkin.ButtonState.disable:
			targetRenderer.material.color = colorDisable;
			break;
		default:
			targetRenderer.material.color = colorNormal;
			break;
		}
	}
	
	void Reset ()
	{
		if (targetRenderer != null) {
			colorNormal = colorOver = colorDown = colorDisable = targetRenderer.sharedMaterial.color;
		}
	}
}
