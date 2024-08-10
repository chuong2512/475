/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ChangeFontByLanguage : LanguageChangeAwareBehavior
{
	private delegate void ChangeFontDelegate (Font font);

	public ChangeFontByLanguageSettings fontSettings;
	private ChangeFontDelegate changeFont;

	private ChangeFontDelegate ChangeFont {
		get {
			if (GetComponent<Text> () != null)
				changeFont = ChangeUiTextFont;
			else if (GetComponent<TextMesh> () != null)
				changeFont = ChangeTextMeshFont;
			return changeFont;
		}
	}

	protected override void ResponseToLanguage (LocalizationLanguageTypes language)
	{
		if (fontSettings != null)
			ChangeFont (fontSettings.GetFontForLanguage (language));
	}

	private void ChangeUiTextFont (Font font)
	{
		GetComponent<Text> ().font = font;
	}

	private void ChangeTextMeshFont (Font font)
	{
		GetComponent<TextMesh> ().font = font;
		GetComponent<Renderer> ().material = font.material;
	}
}
