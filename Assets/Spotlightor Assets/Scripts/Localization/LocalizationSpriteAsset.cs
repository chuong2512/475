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
using System.Collections.Generic;

public class LocalizationSpriteAsset : ScriptableObject
{
	[System.Serializable]
	public class SpriteLanguageDictionary : SerializableDictionary<LocalizationLanguageTypes, Sprite>
	{

	}

	public SpriteLanguageDictionary sprites;

	public Sprite LocalizedSprite { get { return GetSpriteForLanguage (Localization.CurrentLanguage); } }

	public Sprite GetSpriteForLanguage (LocalizationLanguageTypes language)
	{
		Sprite sprite = null;
		sprites.Dictionary.TryGetValue (language, out sprite);
		return sprite;
	}
}
