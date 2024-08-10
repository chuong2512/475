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
public class sGUI3DButton : sGUISkinableButton
{
	public Renderer buttonRenderer;

	void Awake ()
	{
		if (buttonRenderer == null)
			buttonRenderer = GetComponent<Renderer>();
		if (buttonSkin.normalTexture == null && buttonRenderer.material.mainTexture != null)
			buttonSkin.normalTexture = buttonRenderer.material.mainTexture;
	}

	protected override void ChangeButtonAppearanceUseSkinByState (sGUIButtonSkin skin, sGUIButtonSkin.ButtonState state)
	{
		if (buttonRenderer)
			skin.ChangeButtonAppearanceByState (buttonRenderer.material, state);
	}
	
	private void Reset ()
	{
		if (GetComponent<Collider>() == null)
			gameObject.AddComponent<BoxCollider> ().isTrigger = true;
		if (GetComponent<Renderer>() != null && buttonRenderer.sharedMaterial != null) {
			buttonSkin.normalColor = buttonRenderer.sharedMaterial.color;
			buttonSkin.overColor = buttonRenderer.sharedMaterial.color;
			buttonSkin.downColor = buttonRenderer.sharedMaterial.color;
		} else {
			buttonSkin.normalColor = Color.white;
			buttonSkin.overColor = Color.white;
			buttonSkin.downColor = Color.white;
		}
	}
}

