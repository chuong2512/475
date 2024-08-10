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

/// <summary>
/// ����sGUI��ťϵ�У������˱�׼�İ�ťSKIN�������ṩ��Ϊ��ť�������ǻ���GUITexture����3D������״̬������۵ķ���
/// </summary>
[System.Serializable]
public class sGUIButtonSkin
{
    #region Fields
	public enum ButtonState
	{
		normal,
		over,
		down,
		disable
	};
	public Texture normalTexture;
	public Color normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture overTexture;
	public Color overColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture downTexture;
	public Color downColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture disableTexture;
	public Color disableColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
    #endregion

    #region Properties
	public Texture overOrNormalTexture { get { return overTexture == null ? normalTexture : overTexture; } }

	public Texture downOrNormalTexture { get { return downTexture == null ? normalTexture : downTexture; } }

	public Texture disableOrNormalTexture { get { return disableTexture == null ? normalTexture : disableTexture; } }

	public static sGUIButtonSkin DefaultSkinForGUITexture {
		get {
			sGUIButtonSkin skin = new sGUIButtonSkin ();
			Color semiTransparentGrey = Color.grey;
			semiTransparentGrey.a = 0.5f;
			skin.normalColor = skin.overColor = skin.downColor = skin.disableColor = semiTransparentGrey;
			return skin;
		}
	}

	public static sGUIButtonSkin DefaultSkinForMesh {
		get {
			sGUIButtonSkin skin = new sGUIButtonSkin ();
			skin.normalColor = skin.overColor = skin.downColor = Color.white;
			skin.disableColor = Color.gray;
			return skin;
		}
	}
    #endregion

    #region Functions
	public void ChangeButtonAppearanceByState (Material buttonMaterial, ButtonState state)
	{
		Texture buttonTexture;
		Color buttonColor;
		switch (state) {
		case ButtonState.over:
			buttonColor = overColor;
			buttonTexture = overOrNormalTexture;
			break;
		case ButtonState.down:
			buttonColor = downColor;
			buttonTexture = downOrNormalTexture;
			break;
		case ButtonState.disable:
			buttonColor = disableColor;
			buttonTexture = disableOrNormalTexture;
			break;
		default:
			buttonColor = normalColor;
			buttonTexture = normalTexture;
			break;
		}
	}


	
	protected void ChangeButtonAppearance (Material buttonMaterial, Color buttonColor, Texture buttonTexture)
	{
		buttonMaterial.color = buttonColor;
		if (buttonTexture != null)
			buttonMaterial.mainTexture = buttonTexture;
	}
    #endregion
}
